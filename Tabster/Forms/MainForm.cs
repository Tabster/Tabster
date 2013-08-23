﻿#region

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tabster.Controls;
using Tabster.Properties;
using Tabster.UltimateGuitar;
using ToolStripRenderer = Tabster.Controls.ToolStripRenderer;

#endregion

namespace Tabster.Forms
{
    public partial class MainForm : Form
    {
        private readonly TabFile _queuedTabfile;

        public MainForm()
        {
            InitializeComponent();

            Text = string.Format("Tabster v{0}", Common.TruncateVersion(Application.ProductVersion));

            searchManager.Completed += searchSession_OnCompleted;

            //tabviewermanager events
            Program.TabHandler.TabOpened += TabHandler_OnTabOpened;
            Program.TabHandler.TabClosed += TabHandler_OnTabClosed;

            sidemenu.LoadNodes();

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
            foreach (var s in Tab.TabTypes)
            {
                var str = s.EndsWith("s") ? s : string.Format("{0}s", s);
                txtsearchtype.Items.Add(str);
            }
            txtsearchtype.Text = "All Types";

            Rating0 = new Bitmap(1, 1);
            Rating1 = Resources.r1;
            Rating2 = Resources.r2;
            Rating3 = Resources.r3;
            Rating4 = Resources.r4;
            Rating5 = Resources.r5;
        }

        public MainForm(TabFile tabFile) : this()
        {
            _queuedTabfile = tabFile;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Settings.Default.StartupUpdate)
            {
                checkForUpdatesToolStripMenuItem.PerformClick();
            }

            recentlyViewedToolStripMenuItem.FilePath = Path.Combine(Program.libraryManager.ApplicationDirectory, "recent.dat");
            recentlyViewedToolStripMenuItem.ShowClear = true;
            recentlyViewedToolStripMenuItem.Load();
            recentlyViewedToolStripMenuItem.OnItemClicked += recentlyViewedToolStripMenuItem_OnItemClicked;

            sidemenu.SelectedNode = sidemenu.Nodes[0].FirstNode;

            if (Program.libraryManager.TabsLoaded)
                LoadLibrary();

            if (Program.libraryManager.PlaylistsLoaded)
                PopulatePlaylists();

            LoadSettings(true);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //loads queued tab after splash
            if (_queuedTabfile != null)
            {
                PopoutTab(_queuedTabfile);
            }
        }

        private void recentlyViewedToolStripMenuItem_OnItemClicked(object sender, EventArgs e)
        {
            TabFile tab;

            var path = ((ToolStripMenuItem) sender).ToolTipText;

            if (TabFile.TryParse(path, out tab))
            {
                PopoutTab(tab);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            Program.libraryManager.CleanupTempFiles();
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

        #region Menu Items

        private void multiDownloaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var d = new DownloadDialog(this))
            {
                d.ShowDialog();
            }
        }

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
            var updateQuery = new Updater.UpdateQuery();

            updateQuery.Check();

            if (updateQuery.UpdateAvailable)
            {
                var updateDialog = new Updater.UpdateDialog(updateQuery);
                updateDialog.ShowDialog();
            }

            else
            {
                MessageBox.Show("Your version of Tabster is up to date.", "Updated");
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var p = new PreferencesDialog())
            {
                if (p.ShowDialog() == DialogResult.OK)
                {
                    LoadSettings(false);
                }
            }
        }

        #endregion

        private void txtsearchartist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                onlinesearchbtn.PerformClick();
            }
        }
    }
}