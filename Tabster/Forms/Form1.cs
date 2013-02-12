#region

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tabster.Properties;
using ToolStripRenderer = Tabster.Controls.ToolStripRenderer;

#endregion

namespace Tabster.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Global.libraryManager.Playlists.OnPlaylistAdded += Playlists_OnPlaylistAdded;
            Global.libraryManager.Playlists.OnPlaylistRemoved += Playlists_OnPlaylistRemoved;
            Global.libraryManager.Tabs.OnTabAdded += Tabs_OnTabAdded;
            Global.libraryManager.Tabs.OnTabRemoved += Tabs_OnTabRemoved;

            sidemenu.LoadNodes();

            toolStrip2.Renderer = new ToolStripRenderer();
            toolStrip3.Renderer = new ToolStripRenderer();

            if (Settings.Default.ClientState == FormWindowState.Maximized)
                WindowState = FormWindowState.Maximized;
            else
                Size = Settings.Default.ClientSize;

            if (Environment.OSVersion.Version.Major < 6)
            {
                menuStrip1.RenderMode = ToolStripRenderMode.System;
                menuStrip1.Renderer = new NS_Common.Controls.MenuStripRenderer();
            }

            txtsearchtype.Items.Add("All Types");
            foreach (var s in Constants.TabTypes)
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


            //browser events
            webBrowser1.CanGoBackChanged += webBrowser1_CanGoBackChanged;
            webBrowser1.CanGoForwardChanged += webBrowser1_CanGoForwardChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Settings.Default.StartupUpdate)
            {
                NS_Common.Update.PerformUpdate(null, SystemColors.Control, SystemColors.ControlText, "Tabster", Application.ProductVersion, true, false);
            }

            recentlyViewedToolStripMenuItem.FilePath = string.Format("{0}recent.xml", Global.WorkingDirectory);
            recentlyViewedToolStripMenuItem.OnItemClicked += recentlyViewedToolStripMenuItem_OnItemClicked;

            sidemenu.SelectedNode = sidemenu.Nodes[0].FirstNode;

            if (Global.libraryManager.PlaylistsLoaded)
                PopulatePlaylists();

            LoadSettings(true);
        }

        private void recentlyViewedToolStripMenuItem_OnItemClicked(object sender, EventArgs e)
        {
            var path = ((ToolStripMenuItem) sender).ToolTipText;

            if (File.Exists(path))
            {
                var tab = Global.libraryManager.Tabs.FindTabByPath(path);

                if (tab == null)
                    TabFile.TryParse(path, out tab);

                if (tab != null)
                {
                    PopoutTab(tab);
                    //recentlyViewedToolStripMenuItem.Add(tab);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            Global.libraryManager.CleanupTempFiles();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //redirect to UG
            if (tabControl1.SelectedTab == display_browser && !_firstBrowserLoad && !webBrowser1.Url.ToString().Contains("ultimate-guitar.com"))
            {
                webBrowser1.Navigate(UltimateGuitar.Constants.Tab_Home_0);
            }

            if (tabControl1.SelectedTab == display_search)
            {
                txtsearchartist.Focus();
            }

            filtertext.Visible = tabControl1.SelectedTab == display_library;
        }

        private void TogglePreviewPane(object sender, EventArgs e)
        {
            var ts = (ToolStripMenuItem) sender;
            var orientation = PreviewPanelOrientation.Hidden;

            SplitContainer associatedSplitContainer = null;

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
                associatedSplitContainer = librarySplitContainer;

                foreach (ToolStripMenuItem i in libraryPreviewPaneToolStripMenuItem.DropDownItems)
                {
                    i.Checked = i == sender;
                }
            }

            if (ts.OwnerItem == searchPreviewPaneToolStripMenuItem)
            {
                associatedSplitContainer = searchSplitContainer;

                foreach (ToolStripMenuItem i in searchPreviewPaneToolStripMenuItem.DropDownItems)
                {
                    i.Checked = i == sender;
                }
            }

            associatedSplitContainer.Panel2Collapsed = orientation == PreviewPanelOrientation.Hidden;
            associatedSplitContainer.Orientation = orientation == PreviewPanelOrientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical;

            if (ts.OwnerItem == libraryPreviewPaneToolStripMenuItem)
            {
                Settings.Default.LibraryPreviewOrientation = associatedSplitContainer.Panel2Collapsed
                                                                 ? PreviewPanelOrientation.Hidden
                                                                 : (associatedSplitContainer.Orientation == Orientation.Horizontal ? PreviewPanelOrientation.Horizontal : PreviewPanelOrientation.Vertical);
            }

            if (ts.OwnerItem == searchPreviewPaneToolStripMenuItem)
            {
                Settings.Default.SearchPreviewOrientation = associatedSplitContainer.Panel2Collapsed
                                                                ? PreviewPanelOrientation.Hidden
                                                                : (associatedSplitContainer.Orientation == Orientation.Horizontal ? PreviewPanelOrientation.Horizontal : PreviewPanelOrientation.Vertical);
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
                        hiddenToolStripMenuItem.PerformClick();
                        break;
                    case PreviewPanelOrientation.Horizontal:
                        horizontalToolStripMenuItem.PerformClick();
                        break;
                    case PreviewPanelOrientation.Vertical:
                        verticalToolStripMenuItem.PerformClick();
                        break;
                    default:
                        horizontalToolStripMenuItem.PerformClick();
                        break;
                }

                switch (Settings.Default.SearchPreviewOrientation)
                {
                    case PreviewPanelOrientation.Hidden:
                        toolStripMenuItem2.PerformClick();
                        break;
                    case PreviewPanelOrientation.Horizontal:
                        toolStripMenuItem3.PerformClick();
                        break;
                    case PreviewPanelOrientation.Vertical:
                        toolStripMenuItem4.PerformClick();
                        break;
                    default:
                        toolStripMenuItem2.PerformClick();
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

        private void downloadTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var d = new UGDownload())
            {
                if (d.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var a = new About())
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
            NS_Common.Update.PerformUpdate(null, SystemColors.Control, SystemColors.ControlText, "Tabster", Application.ProductVersion, false, false);
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var p = new Preferences())
            {
                if (p.ShowDialog() == DialogResult.OK)
                {
                    LoadSettings(false);
                }
            }
        }

        #endregion

        private void detailsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (sidemenu.PlaylistNodeSelected())
            {
                using (var pdd = new PlaylistDetailsDialog(sidemenu.SelectedPlaylist()))
                {
                    if (pdd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        
                    }
                }
            }
        }
    }
}