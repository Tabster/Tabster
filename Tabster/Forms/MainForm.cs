#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Tabster.Controls;
using Tabster.Core.FileTypes;
using Tabster.Core.Plugins;
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
        private readonly TabsterDocumentProcessor<TablatureDocument> _tablatureProcessor = new TabsterDocumentProcessor<TablatureDocument>(TablatureDocument.FILE_VERSION, true);
        private readonly UpdateQuery _updateQuery = new UpdateQuery();

        public MainForm()
        {
            InitializeComponent();

            Text = string.Format("Tabster v{0}", new Version(Application.ProductVersion).ToShortString());

            //tabviewermanager events
            Program.TabHandler.TabOpened += TabHandler_OnTabOpened;
            Program.TabHandler.TabClosed += TabHandler_OnTabClosed;

            sidemenu.LoadNodes();

            _updateQuery.Completed += _updateQuery_Completed;

            previewToolStrip.Renderer = new ToolStripRenderer();

            if (Settings.Default.ClientState == FormWindowState.Maximized)
                WindowState = FormWindowState.Maximized;
            else
                Size = Settings.Default.ClientSize;

            if (Environment.OSVersion.Version.Major < 6)
            {
                menuStrip1.RenderMode = ToolStripRenderMode.System;
                menuStrip1.Renderer = new MenuStripRenderer();
            }

            txtsearchtype.Items.Add("All Types");
            foreach (TabType type in Enum.GetValues(typeof (TabType)))
            {
                var typeStr = type.ToFriendlyString();
                var str = typeStr.EndsWith("s") ? typeStr : string.Format("{0}s", typeStr);
                txtsearchtype.Items.Add(str);
            }
            txtsearchtype.Text = "All Types";

            CachePluginResources();
        }

        public MainForm(TablatureDocument tabDocument)
            : this()
        {
            _queuedTabfile = tabDocument;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            recentlyViewedToolStripMenuItem.FilePath = Path.Combine(Program.ApplicationDirectory, "recent.dat");
            recentlyViewedToolStripMenuItem.ShowClear = true;
            recentlyViewedToolStripMenuItem.Load();
            recentlyViewedToolStripMenuItem.OnItemClicked += recentlyViewedToolStripMenuItem_OnItemClicked;

            sidemenu.SelectedNode = sidemenu.Nodes[0].FirstNode;

            LoadLibrary();

            PopulatePlaylists();

            LoadSettings(true);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Settings.Default.StartupUpdate)
            {
                _updateQuery.Check(false);
            }

            //loads queued tab after splash
            if (_queuedTabfile != null)
            {
                PopoutTab(_queuedTabfile);
            }
        }

        private void CachePluginResources()
        {
            _tabParsers = new List<ITabParser>(Program.pluginController.GetClassInstances<ITabParser>());
            _searchServices = new List<ISearchService>(Program.pluginController.GetClassInstances<ISearchService>());
        }

        private void recentlyViewedToolStripMenuItem_OnItemClicked(object sender, EventArgs e)
        {
            var path = ((ToolStripMenuItem) sender).ToolTipText;

            var tab = _tablatureProcessor.Load(path);

            if (tab != null)
            {
                PopoutTab(tab);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            Program.libraryManager.Save();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == display_search)
            {
                txtsearchartist.Focus();
            }

            filtertext.Visible = tabControl1.SelectedTab == display_library;
            libraryToolStripMenuItem.Enabled = tabControl1.SelectedTab == display_library;
        }

        private void TogglePreviewPane(object sender, EventArgs e)
        {
            var ts = (ToolStripMenuItem) sender;
            var orientation = PreviewPanelOrientation.Hidden;

            switch (ts.Text)
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

            if (ts.OwnerItem == libraryPreviewPaneToolStripMenuItem)
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

            if (ts.OwnerItem == searchPreviewPaneToolStripMenuItem || ts == previewToolStripMenuItem)
            {
                if (ts == previewToolStripMenuItem && searchSplitContainer.Panel2Collapsed)
                    orientation = PreviewPanelOrientation.Horizontal;

                searchSplitContainer.Panel2Collapsed = orientation == PreviewPanelOrientation.Hidden;
                searchSplitContainer.Orientation = orientation == PreviewPanelOrientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical;

                searchhiddenpreviewToolStripMenuItem.Checked = orientation == PreviewPanelOrientation.Hidden;
                searchhorizontalpreviewToolStripMenuItem.Checked = orientation == PreviewPanelOrientation.Horizontal;
                searchverticalpreviewToolStripMenuItem.Checked = orientation == PreviewPanelOrientation.Vertical;

                Settings.Default.SearchPreviewOrientation = searchSplitContainer.Panel2Collapsed
                                                                ? PreviewPanelOrientation.Hidden
                                                                : (searchSplitContainer.Orientation == Orientation.Horizontal ? PreviewPanelOrientation.Horizontal : PreviewPanelOrientation.Vertical);

                if (orientation != PreviewPanelOrientation.Hidden && SelectedSearchResult() == null)
                {
                    searchSplitContainer.Panel2Collapsed = true;
                }

                previewToolStrip.Enabled = previewToolStripMenuItem.Enabled = searchSplitContainer.Panel2Collapsed;
            }

            Settings.Default.Save();
        }

        private void LoadSettings(bool startup)
        {
            if (startup)
            {
                splitContainer1.Panel1Collapsed = !Settings.Default.SidePanel;
                sidebarToolStripMenuItem.Checked = Settings.Default.SidePanel;

                //librarySplitContainer.Panel2Collapsed = Settings.Default.LibraryPreviewOrientation == PreviewPanelOrientation.Hidden;
                //librarySplitContainer.Orientation = Settings.Default.LibraryPreviewOrientation == PreviewPanelOrientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical;

                /*
                searchSplitContainer.Panel2Collapsed = Settings.Default.SearchPreviewOrientation == PreviewPanelOrientation.Hidden;
                searchSplitContainer.Orientation = Settings.Default.SearchPreviewOrientation == PreviewPanelOrientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical;
                */

                librarySplitContainer.SplitterDistance = Settings.Default.LibraryPreviewPanelDistance;

                statusStrip1.Visible = Settings.Default.StatusBar;
                statusBarToolStripMenuItem.Checked = Settings.Default.StatusBar;

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

        #region Updater

        private void _updateQuery_Completed(object sender, UpdateQueryCompletedEventArgs e)
        {
            var showUpdatedDialog = e.UserState != null && (bool) e.UserState;

            if (_updateQuery.UpdateAvailable)
            {
                var updateDialog = new UpdateDialog(_updateQuery) {StartPosition = FormStartPosition.CenterParent};
                updateDialog.ShowDialog();
            }

            else
            {
                if (showUpdatedDialog)
                {
                    MessageBox.Show("Your version of Tabster is up to date.", "Updated");
                }
            }
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
            _updateQuery.Check(true);
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var p = new PreferencesDialog())
            {
                if (p.ShowDialog() == DialogResult.OK)
                {
                    LoadSettings(false);
                    CachePluginResources();
                }
            }
        }

        #endregion
    }
}