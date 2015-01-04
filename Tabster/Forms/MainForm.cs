#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Tabster.Core.Searching;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;
using Tabster.Properties;
using Tabster.Updater;
using Tabster.Utilities.Extensions;
using ToolStripRenderer = Tabster.Controls.ToolStripRenderer;

#endregion

namespace Tabster.Forms
{
    internal partial class MainForm : Form
    {
        private readonly TabsterDocumentProcessor<TablaturePlaylistDocument> _playlistProcessor = new TabsterDocumentProcessor<TablaturePlaylistDocument>(TablaturePlaylistDocument.FILE_VERSION, true);
        private readonly TablatureDocument _queuedTablatureDocument;
        private readonly TablaturePlaylistDocument _queuedTablaturePlaylist;
        private readonly string _recentFilesPath = Path.Combine(Program.ApplicationDataDirectory, "recent.dat");
        private readonly TabsterDocumentProcessor<TablatureDocument> _tablatureProcessor = new TabsterDocumentProcessor<TablatureDocument>(TablatureDocument.FILE_VERSION, true);

        private readonly ToolStripMenuItem ascendingMenuItem = new ToolStripMenuItem {Text = "Ascending"};
        private readonly ToolStripMenuItem descendingMenuItem = new ToolStripMenuItem {Text = "Descending"};
        private bool _initialLibraryLoaded;

        public MainForm()
        {
            InitializeComponent();

            Text = string.Format("{0} v{1}", Application.ProductName, new Version(Application.ProductVersion).ToShortString());

#if PORTABLE

            Text += " (Portable)";
            checkForUpdatesMenuItem.Visible = false;

#endif

            PopulateTabTypeControls();

            UpdateSortColumnMenu(true);

            ascendingMenuItem.Click += SortByDirectionMenuItem_Click;
            descendingMenuItem.Click += SortByDirectionMenuItem_Click;

            //tabviewermanager events
            Program.TabbedViewer.TabClosed += TabHandler_OnTabClosed;

            Program.updateQuery.Completed += updateQuery_Completed;

            previewToolStrip.Renderer = new ToolStripRenderer();

            if (Settings.Default.ClientState == FormWindowState.Maximized)
                WindowState = FormWindowState.Maximized;
            else
                Size = Settings.Default.ClientSize;

            PreviewDisplayDelay.Interval = PREVIEW_DISPLAY_DELAY_DURATION;
            PreviewDisplayTimer.Interval = PREVIEW_DISPLAY_VIEWED_DURATION;

            CachePluginResources();

            InitializeSearchControls(true);

            BuildSearchSuggestions();
        }

        public MainForm(TablatureDocument tabDocument)
            : this()
        {
            _queuedTablatureDocument = tabDocument;
        }

        public MainForm(TablaturePlaylistDocument playlistDocument)
            : this()
        {
            _queuedTablaturePlaylist = playlistDocument;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRecentFilesList();

            sidemenu.SelectedNode = sidemenu.Nodes[0].FirstNode;

            PopulatePlaylists();

            LoadSettings(true);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Program.updateQuery.UpdateAvailable)
            {
                ShowUpdateDialog();
            }

            //loads queued tab after splash
            if (_queuedTablatureDocument != null)
            {
                PopoutTab(_queuedTablatureDocument);
            }

            //loads queued playlist after splash
            if (_queuedTablaturePlaylist != null)
            {
                AddPlaylistNode(_queuedTablaturePlaylist, true);
            }
        }

        private void LoadRecentFilesList()
        {
            if (!File.Exists(_recentFilesPath))
                return;

            var xml = new XmlDocument();
            xml.Load(_recentFilesPath);

            var files = xml.GetElementsByTagName("recent")[0].ChildNodes;

            if (files.Count > 0)
            {
                var documents = new List<TablatureDocument>();

                foreach (XmlNode file in files)
                {
                    var doc = _tablatureProcessor.Load(file.InnerText);

                    if (doc != null)
                    {
                        //keep document view order by inserting
                        documents.Insert(0, doc);
                    }
                }

                for (var i = 0; i < documents.Count; i++)
                {
                    var doc = documents[i];

                    //only update display on last document
                    var updateDisplay = i == files.Count - 1;

                    recentlyViewedMenuItem.Add(doc.FileInfo, doc.ToFriendlyString(), updateDisplay);
                }
            }
        }

        private void SaveRecentFilesList()
        {
            var doc = new XmlDocument();

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));

            var root = doc.CreateElement("recent");
            doc.AppendChild(root);

            foreach (var item in recentlyViewedMenuItem.Items)
            {
                var elem = doc.CreateElement("item");
                elem.InnerText = item.File.FullName;
                root.AppendChild(elem);
            }

            doc.Save(_recentFilesPath);
        }

        private void UpdateSortColumnMenu(bool populateItems = false)
        {
            if (populateItems)
            {
                sortByToolStripMenuItem.DropDownItems.Clear();

                foreach (DataGridViewColumn column in tablibrary.Columns)
                {
                    var item = new ToolStripMenuItem(column.HeaderText);
                    item.Click += SortByColumnMenuItem_Click;
                    sortByToolStripMenuItem.DropDownItems.Add(item);
                }

                sortByToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

                sortByToolStripMenuItem.DropDownItems.Add(ascendingMenuItem);
                sortByToolStripMenuItem.DropDownItems.Add(descendingMenuItem);
            }

            var sortedColumn = tablibrary.SortedColumn ?? tablibrary.Columns[0];

            foreach (var item in sortByToolStripMenuItem.DropDownItems)
            {
                if (item is ToolStripSeparator)
                    break;

                var menuItem = (ToolStripMenuItem) item;

                var col = tablibrary.GetColumnByHeaderText(menuItem.Text);

                if (col != null)
                    menuItem.Checked = sortedColumn == col;
            }

            ascendingMenuItem.Checked = tablibrary.SortOrder == SortOrder.Ascending || tablibrary.SortOrder == SortOrder.None;
            descendingMenuItem.Checked = !ascendingMenuItem.Checked;
        }

        private void SortByColumnMenuItem_Click(object sender, EventArgs e)
        {
            var direction = descendingMenuItem.Checked ? ListSortDirection.Descending : ListSortDirection.Ascending;

            var item = (ToolStripMenuItem) sender;

            var col = tablibrary.GetColumnByHeaderText(item.Text);

            tablibrary.Sort(col, direction);
        }

        private void SortByDirectionMenuItem_Click(object sender, EventArgs e)
        {
            var col = tablibrary.SortedColumn ?? tablibrary.Columns[0];

            var direction = ListSortDirection.Ascending;

            if (sender == descendingMenuItem)
                direction = ListSortDirection.Descending;

            tablibrary.Sort(col, direction);
        }

        private void PopulateTabTypeControls()
        {
            foreach (var t in TablatureType.GetKnownTypes())
            {
                var typeStr = t.ToFriendlyString();

                //library menu
                sidemenu.FirstNode.Nodes.Add(new TreeNode(typeStr) {NodeFont = sidemenu.FirstNode.FirstNode.NodeFont, Tag = t.ToString()});
            }
        }

        private void CachePluginResources()
        {
            _fileExporters = new List<ITablatureFileExporter>(Program.pluginController.GetClassInstances<ITablatureFileExporter>());
            _fileImporters = new List<ITablatureFileImporter>(Program.pluginController.GetClassInstances<ITablatureFileImporter>());

            _webImporters = new List<ITablatureWebpageImporter>(Program.pluginController.GetClassInstances<ITablatureWebpageImporter>());
            _searchServices = new List<ITablatureSearchEngine>(Program.pluginController.GetClassInstances<ITablatureSearchEngine>());

            _searchServices.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));

            InitializeSearchControls();
        }

        private void OpenRecentFile(MenuItem item)
        {
            var path = item.Tag.ToString();

            var tab = _tablatureProcessor.Load(path);

            if (tab != null)
            {
                PopoutTab(tab, updateRecentFiles: false);
            }
        }

        private void recentlyViewedMenuItem_OnItemClicked(object sender, EventArgs e)
        {
            OpenRecentFile((MenuItem) sender);
        }

        private void recentlyViewedMenuItem_OnAllItemsOpened(object sender, EventArgs e)
        {
            foreach (var item in recentlyViewedMenuItem.Items)
            {
                OpenRecentFile(item.MenuItem);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveRecentFilesList();
            SaveSettings();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == display_search)
            {
                txtSearchArtist.Focus();
            }

            txtLibraryFilter.Visible = tabControl1.SelectedTab == display_library;
            menuItem3.Enabled = tabControl1.SelectedTab == display_library;
        }

        private void SetLibraryPreviewPanelOrientation(PreviewPanelOrientation orientation)
        {
            librarySplitContainer.Panel2Collapsed = orientation == PreviewPanelOrientation.Hidden;
            librarySplitContainer.Orientation = orientation == PreviewPanelOrientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical;

            libraryhiddenpreviewToolStripMenuItem.Checked = orientation == PreviewPanelOrientation.Hidden;
            libraryhorizontalpreviewToolStripMenuItem.Checked = orientation == PreviewPanelOrientation.Horizontal;
            libraryverticalpreviewToolStripMenuItem.Checked = orientation == PreviewPanelOrientation.Vertical;

            Settings.Default.LibraryPreviewOrientation = librarySplitContainer.Panel2Collapsed
                                                             ? PreviewPanelOrientation.Hidden
                                                             : (librarySplitContainer.Orientation == Orientation.Horizontal ? PreviewPanelOrientation.Horizontal : PreviewPanelOrientation.Vertical);
        }

        private void SetSearchPreviewPanelOrientation(PreviewPanelOrientation orientation)
        {
            searchSplitContainer.Panel2Collapsed = orientation == PreviewPanelOrientation.Hidden;
            searchSplitContainer.Orientation = orientation == PreviewPanelOrientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical;

            searchhiddenpreviewToolStripMenuItem.Checked = orientation == PreviewPanelOrientation.Hidden;
            searchhorizontalpreviewToolStripMenuItem.Checked = orientation == PreviewPanelOrientation.Horizontal;
            searchverticalpreviewToolStripMenuItem.Checked = orientation == PreviewPanelOrientation.Vertical;

            Settings.Default.SearchPreviewOrientation = searchSplitContainer.Panel2Collapsed
                                                            ? PreviewPanelOrientation.Hidden
                                                            : (searchSplitContainer.Orientation == Orientation.Horizontal ? PreviewPanelOrientation.Horizontal : PreviewPanelOrientation.Vertical);
        }

        private void TogglePreviewPane(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            //mainmenu item
            if (menuItem != null)
            {
                var orientation = PreviewPanelOrientation.Hidden;

                switch (menuItem.Text)
                {
                    case "Hidden":
                        orientation = PreviewPanelOrientation.Hidden;
                        break;
                    case "Horizontal":
                        orientation = PreviewPanelOrientation.Horizontal;
                        break;
                    case "Vertical":
                        orientation = PreviewPanelOrientation.Vertical;
                        break;
                }

                if (menuItem.Parent == libraryPreviewPaneToolStripMenuItem)
                    SetLibraryPreviewPanelOrientation(orientation);
                if (menuItem.Parent == searchPreviewPaneToolStripMenuItem)
                    SetSearchPreviewPanelOrientation(orientation);
            }

                //search context menu
            else if (sender == previewToolStripMenuItem)
            {
                var orientation = PreviewPanelOrientation.Hidden;

                if (searchSplitContainer.Panel2Collapsed)
                    orientation = PreviewPanelOrientation.Horizontal;

                if (orientation != PreviewPanelOrientation.Hidden && SelectedSearchResult() == null)
                    searchSplitContainer.Panel2Collapsed = true;

                SetSearchPreviewPanelOrientation(orientation);
            }

            Settings.Default.Save();
        }

        private void LoadSettings(bool startup)
        {
            if (startup)
            {
                librarySplitContainer.SplitterDistance = Settings.Default.LibraryPreviewPanelDistance;

                switch (Settings.Default.LibraryPreviewOrientation)
                {
                    case PreviewPanelOrientation.Hidden:
                        libraryhiddenpreviewToolStripMenuItem.PerformClick();
                        break;
                    case PreviewPanelOrientation.Horizontal:
                        libraryhorizontalpreviewToolStripMenuItem.PerformClick();
                        break;
                    case PreviewPanelOrientation.Vertical:
                        libraryverticalpreviewToolStripMenuItem.PerformClick();
                        break;
                    default:
                        libraryhorizontalpreviewToolStripMenuItem.PerformClick();
                        break;
                }

                switch (Settings.Default.SearchPreviewOrientation)
                {
                    case PreviewPanelOrientation.Hidden:
                        searchhiddenpreviewToolStripMenuItem.PerformClick();
                        break;
                    case PreviewPanelOrientation.Horizontal:
                        searchhorizontalpreviewToolStripMenuItem.PerformClick();
                        break;
                    case PreviewPanelOrientation.Vertical:
                        searchverticalpreviewToolStripMenuItem.PerformClick();
                        break;
                    default:
                        searchhiddenpreviewToolStripMenuItem.PerformClick();
                        break;
                }
            }
        }

        private void SaveSettings()
        {
            Settings.Default.LibraryPreviewPanelDistance = librarySplitContainer.SplitterDistance;
            Settings.Default.ClientSize = Size;
            Settings.Default.ClientState = WindowState == FormWindowState.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
            Settings.Default.Save();
        }

        private void txtsearchartist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                onlinesearchbtn.PerformClick();
            }
        }

        private void TablatureLibraryTabRemoved(object sender, EventArgs e)
        {
            if (_initialLibraryLoaded)
                BuildSearchSuggestions();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.F)
                {
                    txtLibraryFilter.Focus();
                }
            }
        }

        private void tablibrary_Sorted(object sender, EventArgs e)
        {
            UpdateSortColumnMenu();
        }

        private void openPlaylistMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
                                 {
                                     Title = "Open Plylist",
                                     AddExtension = true,
                                     Multiselect = false,
                                     Filter = string.Format("Tabster Playlist Files (*{0})|*{0}", TablaturePlaylistDocument.FILE_EXTENSION)
                                 })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var playlist = _playlistProcessor.Load(ofd.FileName);

                    if (playlist != null)
                    {
                        Program.TablatureFileLibrary.Add(playlist);
                        AddPlaylistNode(playlist, true);
                    }
                }
            }
        }

        #region Updater

        private static void updateQuery_Completed(object sender, UpdateQueryCompletedEventArgs e)
        {
            var showUpdatedDialog = e.UserState != null && (bool) e.UserState;

            if (Program.updateQuery.UpdateAvailable)
            {
                ShowUpdateDialog();
            }

            else
            {
                if (showUpdatedDialog)
                {
                    MessageBox.Show("Your version of Tabster is up to date.", "Updated");
                }
            }
        }

        private static void ShowUpdateDialog()
        {
            var updateDialog = new UpdateDialog(Program.updateQuery) {StartPosition = FormStartPosition.CenterParent};
            updateDialog.ShowDialog();
        }

        #endregion

        #region Menu Items

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var a = new AboutDialog())
            {
                a.ShowDialog();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.updateQuery.Check(true);
        }

        private void OpenPreferences(string tab = null)
        {
            using (var p = new PreferencesDialog(tab))
            {
                if (p.ShowDialog() == DialogResult.OK)
                {
                    LoadSettings(false);

                    if (p.PluginsModified)
                        CachePluginResources();
                }
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPreferences();
        }

        #endregion

        private void batchDownloaderMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new DownloadDialog(_webImporters))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var tab in dialog.DownloadedTabs)
                    {
                        var libraryItem = Program.TablatureFileLibrary.Add(tab);
                        Program.TablatureFileLibrary.Save();
                        UpdateLibraryItem(libraryItem);
                    }
                }
            }
        }

        private void PreviewEditor_ContentsModified(object sender, EventArgs e)
        {
            toolStripButton3.Enabled = PreviewEditor.HasScrollableContents;
        }
    }
}