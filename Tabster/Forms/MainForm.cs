#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Tabster.Core.Data;
using Tabster.Core.Data.Processing;
using Tabster.Core.Searching;
using Tabster.Core.Types;
using Tabster.Properties;
using Tabster.Updater;
using Tabster.Utilities;
using ToolStripRenderer = Tabster.Controls.ToolStripRenderer;

#endregion

namespace Tabster.Forms
{
    public partial class MainForm : Form
    {
        private readonly TablatureDocument _queuedTabfile;
        private readonly string _recentFilesPath = Path.Combine(Program.ApplicationDirectory, "recent.dat");
        private readonly TabsterDocumentProcessor<TablatureDocument> _tablatureProcessor = new TabsterDocumentProcessor<TablatureDocument>(TablatureDocument.FILE_VERSION, true, false);
        private bool _initialLibraryLoaded;

        public MainForm()
        {
            InitializeComponent();

            Text = string.Format("{0} v{1}", Application.ProductName, new Version(Application.ProductVersion).ToShortString());

            PopulateTabTypeControls();

            //tabviewermanager events
            Program.TabHandler.TabOpened += TabHandler_OnTabOpened;
            Program.TabHandler.TabClosed += TabHandler_OnTabClosed;

            //libarymanager events
            Program.libraryManager.TabRemoved += libraryManager_TabRemoved;

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
            _queuedTabfile = tabDocument;
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
            if (_queuedTabfile != null)
            {
                PopoutTab(_queuedTabfile);
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

        private void PopulateTabTypeControls()
        {
            foreach (TabType t in Enum.GetValues(typeof (TabType)))
            {
                var typeStr = t.ToFriendlyString();
                var str = typeStr.EndsWith("s") ? typeStr : string.Format("{0}s", typeStr);

                //library menu
                sidemenu.FirstNode.Nodes.Add(new TreeNode(str) {NodeFont = sidemenu.FirstNode.FirstNode.NodeFont, Tag = t.ToString()});
            }
        }

        private void CachePluginResources()
        {
            _webImporters = new List<ITablatureWebpageImporter>(Program.pluginController.GetClassInstances<ITablatureWebpageImporter>());
            _searchServices = new List<ISearchService>(Program.pluginController.GetClassInstances<ISearchService>());

            _searchServices.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));

            InitializeSearchControls();
        }

        private void OpenRecentFile(MenuItem item)
        {
            var path = item.Tag.ToString();

            var tab = _tablatureProcessor.Load(path);

            if (tab != null)
            {
                PopoutTab(tab, false);
            }
        }

        private void recentlyViewedMenuItem_OnItemClicked(object sender, EventArgs e)
        {
            OpenRecentFile((MenuItem)sender);
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

        private void libraryManager_TabRemoved(object sender, EventArgs e)
        {
            if (_initialLibraryLoaded)
                BuildSearchSuggestions();
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

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var p = new PreferencesDialog())
            {
                if (p.ShowDialog() == DialogResult.OK)
                {
                    LoadSettings(false);

                    if (p.PluginsModified)
                        CachePluginResources();
                }
            }
        }

        #endregion

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
    }
}