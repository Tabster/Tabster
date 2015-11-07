#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using BrightIdeasSoftware;
using Tabster.Core.Searching;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Library;
using Tabster.Data.Processing;
using Tabster.Database;
using Tabster.LocalUtilities;
using Tabster.Properties;
using Tabster.Updater;
using Tabster.Utilities.Extensions;
using ToolStripRenderer = Tabster.Controls.ToolStripRenderer;

#endregion

namespace Tabster.Forms
{
    internal partial class MainForm : Form
    {
        private readonly FileInfo _queuedFileInfo;
        private readonly TablatureFile _queuedTablatureFile;
        private readonly string _recentFilesPath = Path.Combine(Program.ApplicationDataDirectory, "recent.dat");

        private readonly LibraryManager _libraryManager;
        private readonly PlaylistManager _playlistManager;

        public MainForm(LibraryManager libraryManager, PlaylistManager playlistManager)
        {
            _libraryManager = libraryManager;
            _playlistManager = playlistManager;

            InitializeComponent();

            Text = string.Format("{0} v{1}", Application.ProductName, new Version(Application.ProductVersion).ToShortString());

#if PORTABLE

            Text += " (Portable)";
            checkForUpdatesMenuItem.Visible = false;

#endif
            InitAspectGetters();

            PopulateTabTypeControls();

            UpdateSortColumnMenu(true);

            //tabviewermanager events
            Program.TabbedViewer.TabClosed += TabHandler_OnTabClosed;

            Program.UpdateQuery.Completed += updateQuery_Completed;

            previewToolStrip.Renderer = new ToolStripRenderer();

            if (Settings.Default.ClientState == FormWindowState.Maximized)
                WindowState = FormWindowState.Maximized;
            else
                Size = Settings.Default.ClientSize;

            CachePluginResources();

            InitializeSearchControls(true);

            BuildSearchSuggestions();

            ToggleEmptyLibraryOverlay(listViewLibrary, true);
            ToggleEmptyLibraryOverlay(listViewSearch, true);
        }

        public MainForm(LibraryManager libraryManager, PlaylistManager playlistManager, 
            TablatureFile tablatureFile, FileInfo fileInfo) : this(libraryManager, playlistManager)
        {
            _queuedTablatureFile = tablatureFile;
            _queuedFileInfo = fileInfo;
        }

        /// <summary>
        ///     Initializes aspect-getters for library columns.
        /// </summary>
        private void InitAspectGetters()
        {
            olvColArtist.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).File.Artist;
            olvColTitle.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).File.Title;
            olvColType.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).File.Type.Name;
            olvColCreated.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).File.FileAttributes.Created;
            olvColModified.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).FileInfo.LastWriteTime;
            olvColLocation.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).FileInfo.FullName;

            //search
            olvColumn1.AspectGetter = x => ((TablatureSearchResult) x).Tab.Artist;
            olvColumn2.AspectGetter = x => ((TablatureSearchResult) x).Tab.Title;
            olvColumn3.AspectGetter = x => ((TablatureSearchResult) x).Tab.Type.Name;

            olvColumn4.AspectGetter = x =>
            {
                var rating = ((TablatureSearchResult) x).Rating;
                return rating == TablatureRating.None ? "" : new string('\u2605', (int) rating - 1).PadRight(5, '\u2606');
            };

            olvColumn5.AspectGetter = x => ((TablatureSearchResult) x).Engine.Name;
            olvColumn6.AspectGetter = x => ((TablatureSearchResult) x).Source.ToString();
        }

        private void ToggleEmptyLibraryOverlay(ObjectListView olv, bool enabled)
        {
            var textOverlay = olv.EmptyListMsgOverlay as TextOverlay;
            textOverlay.TextColor = enabled ? SystemColors.InactiveCaptionText : Color.Transparent;
            textOverlay.BackColor = Color.Transparent;
            textOverlay.BorderWidth = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRecentFilesList();

            sidemenu.SelectedNode = sidemenu.Nodes[0].FirstNode;

            foreach (var playlist in _playlistManager.GetPlaylists())
            {
                AddPlaylistNode(playlist);
            }

            PopulatePlaylistMenu();

            LoadSettings(true);

            UpdateDetails();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Program.UpdateQuery.UpdateAvailable)
            {
                ShowUpdateDialog();
            }

            //loads queued tab after splash
            if (_queuedTablatureFile != null)
            {
                PopoutTab(_queuedTablatureFile, _queuedFileInfo);
            }
        }

        private void LoadRecentFilesList()
        {
            if (!File.Exists(_recentFilesPath))
                return;

            var xml = new XmlDocument();
            xml.Load(_recentFilesPath);

            var pathNodes = xml.GetElementsByTagName("recent")[0].ChildNodes;

            var count = 0;
            foreach (XmlNode pathNode in pathNodes)
            {
                var path = pathNode.InnerText;
                var file = _libraryManager.GetTablatureFileProcessor().Load(path);

                if (file != null)
                {
                    var fileInfo = new FileInfo(path);

                    //only update display on last document
                    var updateDisplay = count == pathNodes.Count - 1;

                    recentlyViewedMenuItem.Add(fileInfo, file.ToFriendlyString(), updateDisplay);

                    count++;
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

                foreach (OLVColumn column in listViewLibrary.Columns)
                {
                    var item = new ToolStripMenuItem(column.Text);
                    item.Click += SortByColumnMenuItem_Click;
                    sortByToolStripMenuItem.DropDownItems.Add(item);
                }

                sortByToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

                sortByToolStripMenuItem.DropDownItems.Add(ascendingToolStripMenuItem);
                sortByToolStripMenuItem.DropDownItems.Add(descendingToolStripMenuItem);
            }

            var sortedColumn = listViewLibrary.PrimarySortColumn ?? listViewLibrary.GetColumn(0);

            for (var i = 0; i < listViewLibrary.Columns.Count; i++)
            {
                var col = listViewLibrary.GetColumn(i);

                var menuItem = sortByToolStripMenuItem.DropDownItems[i];

                menuItem.Visible = col.IsVisible;
                ((ToolStripMenuItem) menuItem).Checked = sortedColumn == col;
            }

            ascendingToolStripMenuItem.Checked = listViewLibrary.PrimarySortOrder == SortOrder.Ascending;
            descendingToolStripMenuItem.Checked = !ascendingToolStripMenuItem.Checked;
        }

        private void SortByColumnMenuItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;

            var index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);

            listViewLibrary.Sort(index);
        }

        private void SortByDirectionMenuItem_Click(object sender, EventArgs e)
        {
            var direction = sender == descendingToolStripMenuItem ? SortOrder.Descending : SortOrder.Ascending;
            var col = listViewLibrary.PrimarySortColumn ?? listViewLibrary.GetColumn(0);
            listViewLibrary.Sort(col, direction);
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
            _fileExporters = new List<ITablatureFileExporter>(Program.PluginController.GetClassInstances<ITablatureFileExporter>());
            _fileImporters = new List<ITablatureFileImporter>(Program.PluginController.GetClassInstances<ITablatureFileImporter>());
            _webImporters = new List<ITablatureWebImporter>(Program.PluginController.GetClassInstances<ITablatureWebImporter>());
        }

        private void OpenRecentFile(MenuItem item)
        {
            var path = item.Tag.ToString();

            var tab = _libraryManager.GetTablatureFileProcessor().Load(path);

            if (tab != null)
            {
                Program.TabbedViewer.LoadTablature(tab, new FileInfo(path));
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
            _libraryManager.Save();

            SaveRecentFilesList();
            SaveSettings();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
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

                if (orientation != PreviewPanelOrientation.Hidden && GetSelectedSearchResult() == null)
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

        private void batchDownloaderMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new DownloadDialog(_webImporters))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var tab in dialog.DownloadedTabs)
                    {
                        var libraryItem = _libraryManager.Add(tab);
                    }
                }
            }
        }

        private void PreviewEditor_ContentsModified(object sender, EventArgs e)
        {
            toolStripButton3.Enabled = PreviewEditor.HasScrollableContents;
        }

        private void listViewLibrary_AfterSorting(object sender, AfterSortingEventArgs e)
        {
            UpdateSortColumnMenu();
        }

        private void listViewLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_changingLibraryView)
                return;

            UpdateTabControls(true);
        }

        private void listViewLibrary_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = GetSelectedLibraryItem();

            if (item != null)
            {
                PopoutTab(item.File, item.FileInfo);
            }
        }

        private void listViewLibrary_DragDrop(object sender, DragEventArgs e)
        {
            var data = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
            var selectedPlaylist = GetSelectedPlaylist();

            var needsSaved = false;

            foreach (var str in data)
            {
                var file = _libraryManager.GetTablatureFileProcessor().Load(str);

                //copy to playlist
                if (selectedPlaylist != null)
                {
                    if (selectedPlaylist.Find(str) == null)
                    {
                        selectedPlaylist.Add(new TablaturePlaylistItem(file, new FileInfo(str)));
                        needsSaved = true;
                    }
                }

                    //copy to library
                else
                {
                    if (_libraryManager.FindTablatureItemByPath(str) == null)
                    {
                        _libraryManager.Add(file);

                        needsSaved = true;
                    }
                }
            }

            if (needsSaved)
            {
                if (selectedPlaylist != null)
                    _playlistManager.Update(selectedPlaylist);
            }
        }

        private void listViewLibrary_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            //e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void listViewSearch_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            if (GetSelectedSearchResult() == null)
                return;

            e.MenuStrip = SearchMenu;
        }

        private void listViewSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedSearchResultPreview();

            var selectedResult = GetSelectedSearchResult();
            saveTabToolStripMenuItem1.Enabled = selectedResult != null && _searchResultsCache.ContainsKey(selectedResult.Source);
        }

        private void btnSearchOptions_Click(object sender, EventArgs e)
        {
            OpenPreferences(PreferencesDialog.PreferencesSection.Searching);
        }

        private void listViewSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (GetSelectedSearchResult() != null)
            {
                SaveSelectedSearchResult();
            }
        }

        private void saveTabToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveSelectedSearchResult();
        }

        #region Updater

        private static void updateQuery_Completed(object sender, UpdateQueryCompletedEventArgs e)
        {
            var showUpdatedDialog = e.UserState != null && (bool) e.UserState;

            if (Program.UpdateQuery.UpdateAvailable)
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
            var updateDialog = new UpdateDialog(Program.UpdateQuery) {StartPosition = FormStartPosition.CenterParent};
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
            Program.UpdateQuery.Check(true);
        }

        private void OpenPreferences(PreferencesDialog.PreferencesSection section)
        {
            using (var p = new PreferencesDialog(section))
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
            OpenPreferences(PreferencesDialog.PreferencesSection.General);
        }

        #endregion
    }
}