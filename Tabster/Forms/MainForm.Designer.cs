using System.Windows.Forms;
using BrightIdeasSoftware;
using Tabster.Controls;
using Tabster.WinForms;

namespace Tabster.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("All Tabs");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("My Tabs");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Downloads");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Imports");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Favorites");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Library", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Playlists");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.deletePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renamePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.display_library = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sidemenu = new Tabster.Controls.StaticTreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.librarySplitContainer = new System.Windows.Forms.SplitContainer();
            this.listViewLibrary = new BrightIdeasSoftware.ObjectListView();
            this.olvColTitle = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColArtist = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColCreated = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColModified = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColViews = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColLocation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtLibraryFilter = new Tabster.Controls.TextBoxExtended();
            this.tabsCurrentTab = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PreviewEditor = new Tabster.WinForms.BasicTablatureTextEditor();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCurrentEncoding = new System.Windows.Forms.Label();
            this.lblCurrentCompressed = new System.Windows.Forms.Label();
            this.lblCurrentModified = new System.Windows.Forms.Label();
            this.lblCurrentCreated = new System.Windows.Forms.Label();
            this.lblCurrentLength = new System.Windows.Forms.Label();
            this.lblCurrentFormat = new System.Windows.Forms.Label();
            this.lblCurrentLocation = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblFormat = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCurrentComment = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblCurrentRating = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurrentGenre = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblCurrentAlbum = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCurrentCopyright = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCurrentAuthor = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCurrentDifficulty = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurrentSubtitle = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCurrentTuning = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentType = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCurrentTitle = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCurrentArtist = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtLyrics = new Tabster.Controls.TextBoxExtended();
            this.TablaturePanelsImageList = new System.Windows.Forms.ImageList(this.components);
            this.previewToolStrip = new System.Windows.Forms.ToolStrip();
            this.lblpreviewtitle = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.detailsbtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.printbtn = new System.Windows.Forms.ToolStripSplitButton();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblLibraryPreview = new System.Windows.Forms.Label();
            this.display_search = new System.Windows.Forms.TabPage();
            this.searchSplitContainer = new System.Windows.Forms.SplitContainer();
            this.listViewSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.searchPreviewEditor = new Tabster.WinForms.BasicTablatureTextEditor();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbSearchRating = new Tabster.WinForms.TablatureRatingDropdown();
            this.btnSearchOptions = new System.Windows.Forms.Button();
            this.txtSearchTitle = new Tabster.Controls.TextBoxExtended();
            this.onlinesearchbtn = new System.Windows.Forms.Button();
            this.searchTypeList = new Tabster.WinForms.TabTypeDropdown();
            this.txtSearchArtist = new Tabster.Controls.TextBoxExtended();
            this.tabimagelist = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblcount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblplaylists = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.SearchMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveTabToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshbtn = new System.Windows.Forms.ToolStripButton();
            this.LibraryMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.librarycontextdetails = new System.Windows.Forms.ToolStripMenuItem();
            this.librarycontextdelete = new System.Windows.Forms.ToolStripMenuItem();
            this.librarycontextexport = new System.Windows.Forms.ToolStripMenuItem();
            this.librarycontextbrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.searchSimilarTabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchByArtistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchByTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchByArtistAndTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.librarycontextaddtoplaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.newPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.librarycontextfavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ascendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlaylistMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteplaylistcontextmenuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchPreviewBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SearchBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.PreviewDisplayDelay = new System.Windows.Forms.Timer(this.components);
            this.PreviewDisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.newTabMenuItem = new System.Windows.Forms.MenuItem();
            this.newPlaylistMenuItem = new System.Windows.Forms.MenuItem();
            this.openTabMenuItem = new System.Windows.Forms.MenuItem();
            this.importMenuItem = new System.Windows.Forms.MenuItem();
            this.recentlyViewedMenuItem = new Tabster.Controls.RecentToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.libraryPreviewPaneToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.libraryhiddenpreviewToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.libraryhorizontalpreviewToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.libraryverticalpreviewToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.searchPreviewPaneToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.searchhiddenpreviewToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.searchhorizontalpreviewToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.searchverticalpreviewToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.viewTabToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteTabToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.openTabLocationToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.batchDownloaderMenuItem = new System.Windows.Forms.MenuItem();
            this.pluginsMenuItem = new System.Windows.Forms.MenuItem();
            this.preferencesMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.checkForUpdatesMenuItem = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.tabControl1.SuspendLayout();
            this.display_library.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.librarySplitContainer.Panel1.SuspendLayout();
            this.librarySplitContainer.Panel2.SuspendLayout();
            this.librarySplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewLibrary)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabsCurrentTab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.previewToolStrip.SuspendLayout();
            this.display_search.SuspendLayout();
            this.searchSplitContainer.Panel1.SuspendLayout();
            this.searchSplitContainer.Panel2.SuspendLayout();
            this.searchSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewSearch)).BeginInit();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SearchMenu.SuspendLayout();
            this.LibraryMenu.SuspendLayout();
            this.PlaylistMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // deletePlaylistToolStripMenuItem
            // 
            this.deletePlaylistToolStripMenuItem.Name = "deletePlaylistToolStripMenuItem";
            this.deletePlaylistToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.deletePlaylistToolStripMenuItem.Text = "Delete Playlist";
            // 
            // renamePlaylistToolStripMenuItem
            // 
            this.renamePlaylistToolStripMenuItem.Name = "renamePlaylistToolStripMenuItem";
            this.renamePlaylistToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.renamePlaylistToolStripMenuItem.Text = "Rename Playlist";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.display_library);
            this.tabControl1.Controls.Add(this.display_search);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.tabimagelist;
            this.tabControl1.ItemSize = new System.Drawing.Size(80, 20);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(15, 4);
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1034, 438);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // display_library
            // 
            this.display_library.BackColor = System.Drawing.SystemColors.Control;
            this.display_library.Controls.Add(this.splitContainer1);
            this.display_library.ImageIndex = 0;
            this.display_library.Location = new System.Drawing.Point(4, 24);
            this.display_library.Name = "display_library";
            this.display_library.Size = new System.Drawing.Size(1026, 410);
            this.display_library.TabIndex = 1;
            this.display_library.Text = "Library";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.sidemenu);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel1MinSize = 140;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.librarySplitContainer);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(1026, 410);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 9;
            // 
            // sidemenu
            // 
            this.sidemenu.AllowRootNodeSelection = false;
            this.sidemenu.AutoSelectChildNode = false;
            this.sidemenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidemenu.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F);
            this.sidemenu.FullRowSelect = true;
            this.sidemenu.HideSelection = false;
            this.sidemenu.Indent = 15;
            this.sidemenu.ItemHeight = 24;
            this.sidemenu.LineColor = System.Drawing.Color.White;
            this.sidemenu.Location = new System.Drawing.Point(0, 0);
            this.sidemenu.Name = "sidemenu";
            treeNode1.Name = "node_alltabs";
            treeNode1.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            treeNode1.Text = "All Tabs";
            treeNode2.Name = "node_mytabs";
            treeNode2.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            treeNode2.Text = "My Tabs";
            treeNode3.Name = "node_mydownloads";
            treeNode3.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            treeNode3.Text = "Downloads";
            treeNode4.Name = "node_myimports";
            treeNode4.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            treeNode4.Text = "Imports";
            treeNode5.Name = "node_myfavorites";
            treeNode5.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            treeNode5.Text = "Favorites";
            treeNode6.Name = "node_library";
            treeNode6.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode6.Text = "Library";
            treeNode7.Name = "node_playlists";
            treeNode7.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode7.Text = "Playlists";
            this.sidemenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            this.sidemenu.ShowLines = false;
            this.sidemenu.ShowPlusMinus = false;
            this.sidemenu.ShowRootLines = false;
            this.sidemenu.Size = new System.Drawing.Size(140, 354);
            this.sidemenu.TabIndex = 0;
            this.sidemenu.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.sidemenu_BeforeSelect);
            this.sidemenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.sidemenu_AfterSelect);
            this.sidemenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sidemenu_MouseClick);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 354);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(140, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "New Tab...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.NewTab);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(0, 382);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(140, 28);
            this.button2.TabIndex = 2;
            this.button2.Text = "New Playlist...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.NewPlaylist);
            // 
            // librarySplitContainer
            // 
            this.librarySplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.librarySplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.librarySplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.librarySplitContainer.Location = new System.Drawing.Point(0, 0);
            this.librarySplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.librarySplitContainer.Name = "librarySplitContainer";
            this.librarySplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // librarySplitContainer.Panel1
            // 
            this.librarySplitContainer.Panel1.Controls.Add(this.listViewLibrary);
            this.librarySplitContainer.Panel1.Controls.Add(this.panel2);
            this.librarySplitContainer.Panel1MinSize = 140;
            // 
            // librarySplitContainer.Panel2
            // 
            this.librarySplitContainer.Panel2.Controls.Add(this.tabsCurrentTab);
            this.librarySplitContainer.Panel2.Controls.Add(this.previewToolStrip);
            this.librarySplitContainer.Panel2.Controls.Add(this.lblLibraryPreview);
            this.librarySplitContainer.Panel2MinSize = 140;
            this.librarySplitContainer.Size = new System.Drawing.Size(885, 410);
            this.librarySplitContainer.SplitterDistance = 140;
            this.librarySplitContainer.TabIndex = 25;
            // 
            // listViewLibrary
            // 
            this.listViewLibrary.AllColumns.Add(this.olvColTitle);
            this.listViewLibrary.AllColumns.Add(this.olvColArtist);
            this.listViewLibrary.AllColumns.Add(this.olvColType);
            this.listViewLibrary.AllColumns.Add(this.olvColCreated);
            this.listViewLibrary.AllColumns.Add(this.olvColModified);
            this.listViewLibrary.AllColumns.Add(this.olvColViews);
            this.listViewLibrary.AllColumns.Add(this.olvColLocation);
            this.listViewLibrary.AllowDrop = true;
            this.listViewLibrary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColTitle,
            this.olvColArtist,
            this.olvColType,
            this.olvColCreated,
            this.olvColModified,
            this.olvColViews,
            this.olvColLocation});
            this.listViewLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLibrary.EmptyListMsg = "No tablature found";
            this.listViewLibrary.FullRowSelect = true;
            this.listViewLibrary.GridLines = true;
            this.listViewLibrary.HasCollapsibleGroups = false;
            this.listViewLibrary.HeaderUsesThemes = true;
            this.listViewLibrary.HideSelection = false;
            this.listViewLibrary.Location = new System.Drawing.Point(0, 29);
            this.listViewLibrary.MenuLabelGroupBy = "Group by \'{1}\'";
            this.listViewLibrary.MultiSelect = false;
            this.listViewLibrary.Name = "listViewLibrary";
            this.listViewLibrary.RowHeight = 18;
            this.listViewLibrary.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.ModelDialog;
            this.listViewLibrary.ShowGroups = false;
            this.listViewLibrary.ShowHeaderInAllViews = false;
            this.listViewLibrary.Size = new System.Drawing.Size(883, 109);
            this.listViewLibrary.TabIndex = 21;
            this.listViewLibrary.TintSortColumn = true;
            this.listViewLibrary.UseCompatibleStateImageBehavior = false;
            this.listViewLibrary.UseExplorerTheme = true;
            this.listViewLibrary.View = System.Windows.Forms.View.Details;
            this.listViewLibrary.AfterSorting += new System.EventHandler<BrightIdeasSoftware.AfterSortingEventArgs>(this.listViewLibrary_AfterSorting);
            this.listViewLibrary.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.listViewLibrary_CellRightClick);
            this.listViewLibrary.SelectedIndexChanged += new System.EventHandler(this.listViewLibrary_SelectedIndexChanged);
            this.listViewLibrary.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewLibrary_DragDrop);
            this.listViewLibrary.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewLibrary_DragEnter);
            this.listViewLibrary.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewLibrary_MouseDoubleClick);
            // 
            // olvColTitle
            // 
            this.olvColTitle.AspectName = "Title";
            this.olvColTitle.Text = "Title";
            this.olvColTitle.Width = 242;
            // 
            // olvColArtist
            // 
            this.olvColArtist.AspectName = "Artist";
            this.olvColArtist.Text = "Artist";
            this.olvColArtist.Width = 156;
            // 
            // olvColType
            // 
            this.olvColType.AspectName = "Type";
            this.olvColType.Text = "Type";
            this.olvColType.Width = 99;
            // 
            // olvColCreated
            // 
            this.olvColCreated.AspectName = "Created";
            this.olvColCreated.Text = "Created";
            this.olvColCreated.Width = 125;
            // 
            // olvColModified
            // 
            this.olvColModified.AspectName = "LastModified";
            this.olvColModified.Text = "Last Modified";
            this.olvColModified.Width = 125;
            // 
            // olvColViews
            // 
            this.olvColViews.AspectName = "Views";
            this.olvColViews.Text = "Views";
            // 
            // olvColLocation
            // 
            this.olvColLocation.AspectName = "Location";
            this.olvColLocation.FillsFreeSpace = true;
            this.olvColLocation.Text = "Location";
            this.olvColLocation.Width = 128;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtLibraryFilter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(883, 29);
            this.panel2.TabIndex = 20;
            // 
            // txtLibraryFilter
            // 
            this.txtLibraryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLibraryFilter.Location = new System.Drawing.Point(738, 3);
            this.txtLibraryFilter.Name = "txtLibraryFilter";
            this.txtLibraryFilter.PlaceholderForecolor = System.Drawing.Color.DarkGray;
            this.txtLibraryFilter.PlaceholderText = " Search Library";
            this.txtLibraryFilter.SelectOnFocus = true;
            this.txtLibraryFilter.Size = new System.Drawing.Size(137, 20);
            this.txtLibraryFilter.TabIndex = 0;
            this.txtLibraryFilter.TextChangedDelay = 250;
            this.txtLibraryFilter.TextChanged += new System.EventHandler(this.txtLibraryFilter_TextChanged);
            // 
            // tabsCurrentTab
            // 
            this.tabsCurrentTab.Controls.Add(this.tabPage2);
            this.tabsCurrentTab.Controls.Add(this.tabPage1);
            this.tabsCurrentTab.Controls.Add(this.tabPage3);
            this.tabsCurrentTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsCurrentTab.ImageList = this.TablaturePanelsImageList;
            this.tabsCurrentTab.Location = new System.Drawing.Point(0, 25);
            this.tabsCurrentTab.Name = "tabsCurrentTab";
            this.tabsCurrentTab.SelectedIndex = 0;
            this.tabsCurrentTab.Size = new System.Drawing.Size(883, 239);
            this.tabsCurrentTab.TabIndex = 26;
            this.tabsCurrentTab.SelectedIndexChanged += new System.EventHandler(this.tabsCurrentTab_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.PreviewEditor);
            this.tabPage2.ImageIndex = 0;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(875, 212);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Preview";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // PreviewEditor
            // 
            this.PreviewEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewEditor.FontSize = 9F;
            this.PreviewEditor.Location = new System.Drawing.Point(3, 3);
            this.PreviewEditor.Name = "PreviewEditor";
            this.PreviewEditor.ReadOnly = true;
            this.PreviewEditor.Size = new System.Drawing.Size(869, 206);
            this.PreviewEditor.TabIndex = 25;
            this.PreviewEditor.ContentsModified += new System.EventHandler(this.PreviewEditor_ContentsModified);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.ImageIndex = 1;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(875, 212);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblCurrentEncoding);
            this.groupBox2.Controls.Add(this.lblCurrentCompressed);
            this.groupBox2.Controls.Add(this.lblCurrentModified);
            this.groupBox2.Controls.Add(this.lblCurrentCreated);
            this.groupBox2.Controls.Add(this.lblCurrentLength);
            this.groupBox2.Controls.Add(this.lblCurrentFormat);
            this.groupBox2.Controls.Add(this.lblCurrentLocation);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.lblFormat);
            this.groupBox2.Location = new System.Drawing.Point(6, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 80);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Information:";
            // 
            // lblCurrentEncoding
            // 
            this.lblCurrentEncoding.AutoSize = true;
            this.lblCurrentEncoding.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentEncoding.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentEncoding.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentEncoding.Location = new System.Drawing.Point(629, 61);
            this.lblCurrentEncoding.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentEncoding.Name = "lblCurrentEncoding";
            this.lblCurrentEncoding.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentEncoding.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentEncoding.TabIndex = 65;
            this.lblCurrentEncoding.Text = "N/A";
            // 
            // lblCurrentCompressed
            // 
            this.lblCurrentCompressed.AutoSize = true;
            this.lblCurrentCompressed.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentCompressed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentCompressed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentCompressed.Location = new System.Drawing.Point(395, 61);
            this.lblCurrentCompressed.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentCompressed.Name = "lblCurrentCompressed";
            this.lblCurrentCompressed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentCompressed.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentCompressed.TabIndex = 64;
            this.lblCurrentCompressed.Text = "N/A";
            // 
            // lblCurrentModified
            // 
            this.lblCurrentModified.AutoSize = true;
            this.lblCurrentModified.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentModified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentModified.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentModified.Location = new System.Drawing.Point(73, 61);
            this.lblCurrentModified.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentModified.Name = "lblCurrentModified";
            this.lblCurrentModified.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentModified.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentModified.TabIndex = 63;
            this.lblCurrentModified.Text = "N/A";
            // 
            // lblCurrentCreated
            // 
            this.lblCurrentCreated.AutoSize = true;
            this.lblCurrentCreated.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentCreated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentCreated.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentCreated.Location = new System.Drawing.Point(629, 40);
            this.lblCurrentCreated.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentCreated.Name = "lblCurrentCreated";
            this.lblCurrentCreated.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentCreated.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentCreated.TabIndex = 62;
            this.lblCurrentCreated.Text = "N/A";
            // 
            // lblCurrentLength
            // 
            this.lblCurrentLength.AutoSize = true;
            this.lblCurrentLength.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentLength.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentLength.Location = new System.Drawing.Point(395, 40);
            this.lblCurrentLength.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentLength.Name = "lblCurrentLength";
            this.lblCurrentLength.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentLength.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentLength.TabIndex = 61;
            this.lblCurrentLength.Text = "N/A";
            // 
            // lblCurrentFormat
            // 
            this.lblCurrentFormat.AutoSize = true;
            this.lblCurrentFormat.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentFormat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentFormat.Location = new System.Drawing.Point(73, 40);
            this.lblCurrentFormat.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentFormat.Name = "lblCurrentFormat";
            this.lblCurrentFormat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentFormat.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentFormat.TabIndex = 60;
            this.lblCurrentFormat.Text = "N/A";
            // 
            // lblCurrentLocation
            // 
            this.lblCurrentLocation.AutoSize = true;
            this.lblCurrentLocation.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentLocation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentLocation.Location = new System.Drawing.Point(73, 18);
            this.lblCurrentLocation.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentLocation.Name = "lblCurrentLocation";
            this.lblCurrentLocation.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentLocation.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentLocation.TabIndex = 59;
            this.lblCurrentLocation.Text = "N/A";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(568, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Encoding:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(321, 61);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(68, 13);
            this.label25.TabIndex = 4;
            this.label25.Text = "Compressed:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(6, 19);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "Location:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 61);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(50, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Modified:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(568, 40);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(47, 13);
            this.label24.TabIndex = 2;
            this.label24.Text = "Created:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(321, 40);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(43, 13);
            this.label27.TabIndex = 1;
            this.label27.Text = "Length:";
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(6, 40);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(61, 13);
            this.lblFormat.TabIndex = 0;
            this.lblFormat.Text = "File Format:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblCurrentComment);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.lblCurrentRating);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblCurrentGenre);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.lblCurrentAlbum);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblCurrentCopyright);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblCurrentAuthor);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblCurrentDifficulty);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCurrentSubtitle);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblCurrentTuning);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblCurrentType);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblCurrentTitle);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblCurrentArtist);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(727, 140);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tablature Information:";
            // 
            // lblCurrentComment
            // 
            this.lblCurrentComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentComment.AutoEllipsis = true;
            this.lblCurrentComment.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentComment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentComment.Location = new System.Drawing.Point(66, 103);
            this.lblCurrentComment.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentComment.Name = "lblCurrentComment";
            this.lblCurrentComment.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentComment.Size = new System.Drawing.Size(655, 34);
            this.lblCurrentComment.TabIndex = 69;
            this.lblCurrentComment.Text = "N/A";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(6, 19);
            this.label16.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label16.Name = "label16";
            this.label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 36;
            this.label16.Text = "Artist:";
            // 
            // lblCurrentRating
            // 
            this.lblCurrentRating.AutoSize = true;
            this.lblCurrentRating.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentRating.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentRating.Location = new System.Drawing.Point(381, 82);
            this.lblCurrentRating.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentRating.Name = "lblCurrentRating";
            this.lblCurrentRating.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentRating.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentRating.TabIndex = 68;
            this.lblCurrentRating.Text = "N/A";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(6, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Comment:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Tuning:";
            // 
            // lblCurrentGenre
            // 
            this.lblCurrentGenre.AutoSize = true;
            this.lblCurrentGenre.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentGenre.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentGenre.Location = new System.Drawing.Point(66, 82);
            this.lblCurrentGenre.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentGenre.Name = "lblCurrentGenre";
            this.lblCurrentGenre.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentGenre.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentGenre.TabIndex = 67;
            this.lblCurrentGenre.Text = "N/A";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(321, 19);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label17.Name = "label17";
            this.label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label17.Size = new System.Drawing.Size(30, 13);
            this.label17.TabIndex = 37;
            this.label17.Text = "Title:";
            // 
            // lblCurrentAlbum
            // 
            this.lblCurrentAlbum.AutoSize = true;
            this.lblCurrentAlbum.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentAlbum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentAlbum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentAlbum.Location = new System.Drawing.Point(624, 61);
            this.lblCurrentAlbum.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentAlbum.Name = "lblCurrentAlbum";
            this.lblCurrentAlbum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentAlbum.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentAlbum.TabIndex = 66;
            this.lblCurrentAlbum.Text = "N/A";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(568, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Difficulty:";
            // 
            // lblCurrentCopyright
            // 
            this.lblCurrentCopyright.AutoSize = true;
            this.lblCurrentCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentCopyright.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentCopyright.Location = new System.Drawing.Point(381, 61);
            this.lblCurrentCopyright.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentCopyright.Name = "lblCurrentCopyright";
            this.lblCurrentCopyright.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentCopyright.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentCopyright.TabIndex = 65;
            this.lblCurrentCopyright.Text = "N/A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(321, 40);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 49;
            this.label6.Text = "Subtitle:";
            // 
            // lblCurrentAuthor
            // 
            this.lblCurrentAuthor.AutoSize = true;
            this.lblCurrentAuthor.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentAuthor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentAuthor.Location = new System.Drawing.Point(66, 61);
            this.lblCurrentAuthor.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentAuthor.Name = "lblCurrentAuthor";
            this.lblCurrentAuthor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentAuthor.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentAuthor.TabIndex = 64;
            this.lblCurrentAuthor.Text = "N/A";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(6, 61);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "Author:";
            // 
            // lblCurrentDifficulty
            // 
            this.lblCurrentDifficulty.AutoSize = true;
            this.lblCurrentDifficulty.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentDifficulty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentDifficulty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentDifficulty.Location = new System.Drawing.Point(624, 40);
            this.lblCurrentDifficulty.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentDifficulty.Name = "lblCurrentDifficulty";
            this.lblCurrentDifficulty.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentDifficulty.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentDifficulty.TabIndex = 63;
            this.lblCurrentDifficulty.Text = "N/A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(568, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Type:";
            // 
            // lblCurrentSubtitle
            // 
            this.lblCurrentSubtitle.AutoSize = true;
            this.lblCurrentSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentSubtitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentSubtitle.Location = new System.Drawing.Point(381, 40);
            this.lblCurrentSubtitle.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentSubtitle.Name = "lblCurrentSubtitle";
            this.lblCurrentSubtitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentSubtitle.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentSubtitle.TabIndex = 62;
            this.lblCurrentSubtitle.Text = "N/A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(321, 61);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Copyright:";
            // 
            // lblCurrentTuning
            // 
            this.lblCurrentTuning.AutoSize = true;
            this.lblCurrentTuning.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentTuning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTuning.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentTuning.Location = new System.Drawing.Point(66, 40);
            this.lblCurrentTuning.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentTuning.Name = "lblCurrentTuning";
            this.lblCurrentTuning.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentTuning.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentTuning.TabIndex = 61;
            this.lblCurrentTuning.Text = "N/A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(321, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Rating:";
            // 
            // lblCurrentType
            // 
            this.lblCurrentType.AutoSize = true;
            this.lblCurrentType.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentType.Location = new System.Drawing.Point(624, 19);
            this.lblCurrentType.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentType.Name = "lblCurrentType";
            this.lblCurrentType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentType.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentType.TabIndex = 60;
            this.lblCurrentType.Text = "N/A";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(568, 61);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 56;
            this.label10.Text = "Album:";
            // 
            // lblCurrentTitle
            // 
            this.lblCurrentTitle.AutoSize = true;
            this.lblCurrentTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentTitle.Location = new System.Drawing.Point(381, 19);
            this.lblCurrentTitle.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentTitle.Name = "lblCurrentTitle";
            this.lblCurrentTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentTitle.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentTitle.TabIndex = 59;
            this.lblCurrentTitle.Text = "N/A";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(6, 82);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 57;
            this.label9.Text = "Genre:";
            // 
            // lblCurrentArtist
            // 
            this.lblCurrentArtist.AutoSize = true;
            this.lblCurrentArtist.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentArtist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentArtist.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentArtist.Location = new System.Drawing.Point(66, 19);
            this.lblCurrentArtist.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCurrentArtist.Name = "lblCurrentArtist";
            this.lblCurrentArtist.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentArtist.Size = new System.Drawing.Size(27, 13);
            this.lblCurrentArtist.TabIndex = 58;
            this.lblCurrentArtist.Text = "N/A";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtLyrics);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(875, 212);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Lyrics";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtLyrics
            // 
            this.txtLyrics.BackColor = System.Drawing.SystemColors.Window;
            this.txtLyrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLyrics.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtLyrics.Location = new System.Drawing.Point(3, 3);
            this.txtLyrics.Multiline = true;
            this.txtLyrics.Name = "txtLyrics";
            this.txtLyrics.PlaceholderForecolor = System.Drawing.SystemColors.GrayText;
            this.txtLyrics.PlaceholderText = null;
            this.txtLyrics.ReadOnly = true;
            this.txtLyrics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLyrics.SelectOnFocus = false;
            this.txtLyrics.Size = new System.Drawing.Size(869, 206);
            this.txtLyrics.TabIndex = 1;
            this.txtLyrics.TextChangedDelay = 0;
            // 
            // TablaturePanelsImageList
            // 
            this.TablaturePanelsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TablaturePanelsImageList.ImageStream")));
            this.TablaturePanelsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.TablaturePanelsImageList.Images.SetKeyName(0, "preview");
            this.TablaturePanelsImageList.Images.SetKeyName(1, "info");
            this.TablaturePanelsImageList.Images.SetKeyName(2, "music");
            // 
            // previewToolStrip
            // 
            this.previewToolStrip.Enabled = false;
            this.previewToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.previewToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblpreviewtitle,
            this.toolStripButton1,
            this.toolStripSeparator4,
            this.detailsbtn,
            this.toolStripSeparator5,
            this.printbtn,
            this.toolStripSeparator8,
            this.toolStripButton3});
            this.previewToolStrip.Location = new System.Drawing.Point(0, 0);
            this.previewToolStrip.Name = "previewToolStrip";
            this.previewToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.previewToolStrip.Size = new System.Drawing.Size(883, 25);
            this.previewToolStrip.TabIndex = 23;
            // 
            // lblpreviewtitle
            // 
            this.lblpreviewtitle.Name = "lblpreviewtitle";
            this.lblpreviewtitle.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.Image = global::Tabster.Properties.Resources.arrow_out;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton1.Text = "Pop Out";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // detailsbtn
            // 
            this.detailsbtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.detailsbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.detailsbtn.Image = ((System.Drawing.Image)(resources.GetObject("detailsbtn.Image")));
            this.detailsbtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.detailsbtn.Name = "detailsbtn";
            this.detailsbtn.Size = new System.Drawing.Size(85, 22);
            this.detailsbtn.Text = "Edit Details";
            this.detailsbtn.Click += new System.EventHandler(this.TabDetails);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // printbtn
            // 
            this.printbtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.printbtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printPreviewToolStripMenuItem,
            this.printSettingsToolStripMenuItem});
            this.printbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.printbtn.Image = global::Tabster.Properties.Resources.printer;
            this.printbtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printbtn.Name = "printbtn";
            this.printbtn.Size = new System.Drawing.Size(64, 22);
            this.printbtn.Text = "Print";
            this.printbtn.ButtonClick += new System.EventHandler(this.printbtn_Click);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Preview...";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // printSettingsToolStripMenuItem
            // 
            this.printSettingsToolStripMenuItem.Name = "printSettingsToolStripMenuItem";
            this.printSettingsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.printSettingsToolStripMenuItem.Text = "Print Settings...";
            this.printSettingsToolStripMenuItem.Click += new System.EventHandler(this.printSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.offToolStripMenuItem,
            this.onToolStripMenuItem});
            this.toolStripButton3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton3.Text = "Auto-Scroll";
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Checked = true;
            this.offToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.offToolStripMenuItem.Text = "Off";
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem.Text = "On";
            // 
            // lblLibraryPreview
            // 
            this.lblLibraryPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLibraryPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLibraryPreview.Location = new System.Drawing.Point(0, 0);
            this.lblLibraryPreview.Name = "lblLibraryPreview";
            this.lblLibraryPreview.Size = new System.Drawing.Size(883, 264);
            this.lblLibraryPreview.TabIndex = 24;
            this.lblLibraryPreview.Text = "Tab is open in external viewer.";
            this.lblLibraryPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLibraryPreview.Visible = false;
            // 
            // display_search
            // 
            this.display_search.BackColor = System.Drawing.SystemColors.Control;
            this.display_search.Controls.Add(this.searchSplitContainer);
            this.display_search.Controls.Add(this.panel3);
            this.display_search.ImageIndex = 1;
            this.display_search.Location = new System.Drawing.Point(4, 24);
            this.display_search.Name = "display_search";
            this.display_search.Size = new System.Drawing.Size(1026, 410);
            this.display_search.TabIndex = 5;
            this.display_search.Text = "Search";
            // 
            // searchSplitContainer
            // 
            this.searchSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchSplitContainer.Location = new System.Drawing.Point(0, 29);
            this.searchSplitContainer.Name = "searchSplitContainer";
            this.searchSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // searchSplitContainer.Panel1
            // 
            this.searchSplitContainer.Panel1.Controls.Add(this.listViewSearch);
            this.searchSplitContainer.Panel1MinSize = 200;
            // 
            // searchSplitContainer.Panel2
            // 
            this.searchSplitContainer.Panel2.Controls.Add(this.searchPreviewEditor);
            this.searchSplitContainer.Panel2Collapsed = true;
            this.searchSplitContainer.Panel2MinSize = 100;
            this.searchSplitContainer.Size = new System.Drawing.Size(1026, 381);
            this.searchSplitContainer.SplitterDistance = 212;
            this.searchSplitContainer.TabIndex = 29;
            // 
            // listViewSearch
            // 
            this.listViewSearch.AllColumns.Add(this.olvColumn1);
            this.listViewSearch.AllColumns.Add(this.olvColumn2);
            this.listViewSearch.AllColumns.Add(this.olvColumn3);
            this.listViewSearch.AllColumns.Add(this.olvColumn4);
            this.listViewSearch.AllColumns.Add(this.olvColumn5);
            this.listViewSearch.AllColumns.Add(this.olvColumn6);
            this.listViewSearch.AllowDrop = true;
            this.listViewSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6});
            this.listViewSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSearch.EmptyListMsg = "No search results";
            this.listViewSearch.FullRowSelect = true;
            this.listViewSearch.GridLines = true;
            this.listViewSearch.HasCollapsibleGroups = false;
            this.listViewSearch.HeaderUsesThemes = true;
            this.listViewSearch.HideSelection = false;
            this.listViewSearch.Location = new System.Drawing.Point(0, 0);
            this.listViewSearch.MenuLabelGroupBy = "Group by \'{1}\'";
            this.listViewSearch.MultiSelect = false;
            this.listViewSearch.Name = "listViewSearch";
            this.listViewSearch.RowHeight = 18;
            this.listViewSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.ModelDialog;
            this.listViewSearch.ShowGroups = false;
            this.listViewSearch.ShowHeaderInAllViews = false;
            this.listViewSearch.Size = new System.Drawing.Size(1024, 379);
            this.listViewSearch.TabIndex = 22;
            this.listViewSearch.TintSortColumn = true;
            this.listViewSearch.UseCompatibleStateImageBehavior = false;
            this.listViewSearch.UseExplorerTheme = true;
            this.listViewSearch.View = System.Windows.Forms.View.Details;
            this.listViewSearch.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.listViewSearch_CellRightClick);
            this.listViewSearch.SelectedIndexChanged += new System.EventHandler(this.listViewSearch_SelectedIndexChanged);
            this.listViewSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSearch_MouseDoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Title";
            this.olvColumn1.Text = "Title";
            this.olvColumn1.Width = 242;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Artist";
            this.olvColumn2.Text = "Artist";
            this.olvColumn2.Width = 156;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Type";
            this.olvColumn3.Text = "Type";
            this.olvColumn3.Width = 99;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Rating";
            this.olvColumn4.Text = "Rating";
            this.olvColumn4.Width = 125;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Search Engine";
            this.olvColumn5.Text = "Search Engine";
            this.olvColumn5.Width = 129;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Source";
            this.olvColumn6.FillsFreeSpace = true;
            this.olvColumn6.Text = "Source";
            this.olvColumn6.Width = 265;
            // 
            // searchPreviewEditor
            // 
            this.searchPreviewEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPreviewEditor.FontSize = 9F;
            this.searchPreviewEditor.Location = new System.Drawing.Point(0, 0);
            this.searchPreviewEditor.Margin = new System.Windows.Forms.Padding(0);
            this.searchPreviewEditor.Name = "searchPreviewEditor";
            this.searchPreviewEditor.ReadOnly = true;
            this.searchPreviewEditor.Size = new System.Drawing.Size(148, 23);
            this.searchPreviewEditor.TabIndex = 24;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbSearchRating);
            this.panel3.Controls.Add(this.btnSearchOptions);
            this.panel3.Controls.Add(this.txtSearchTitle);
            this.panel3.Controls.Add(this.onlinesearchbtn);
            this.panel3.Controls.Add(this.searchTypeList);
            this.panel3.Controls.Add(this.txtSearchArtist);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1026, 29);
            this.panel3.TabIndex = 38;
            // 
            // cbSearchRating
            // 
            this.cbSearchRating.DefaultText = "All Ratings";
            this.cbSearchRating.DisplayDefault = true;
            this.cbSearchRating.Location = new System.Drawing.Point(437, 4);
            this.cbSearchRating.Name = "cbSearchRating";
            this.cbSearchRating.Size = new System.Drawing.Size(137, 21);
            this.cbSearchRating.TabIndex = 40;
            // 
            // btnSearchOptions
            // 
            this.btnSearchOptions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearchOptions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchOptions.Location = new System.Drawing.Point(661, 3);
            this.btnSearchOptions.Name = "btnSearchOptions";
            this.btnSearchOptions.Size = new System.Drawing.Size(103, 23);
            this.btnSearchOptions.TabIndex = 39;
            this.btnSearchOptions.Text = "Search Options";
            this.btnSearchOptions.UseVisualStyleBackColor = true;
            this.btnSearchOptions.Click += new System.EventHandler(this.btnSearchOptions_Click);
            // 
            // txtSearchTitle
            // 
            this.txtSearchTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSearchTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearchTitle.Location = new System.Drawing.Point(151, 5);
            this.txtSearchTitle.Name = "txtSearchTitle";
            this.txtSearchTitle.PlaceholderForecolor = System.Drawing.Color.DarkGray;
            this.txtSearchTitle.PlaceholderText = " Title";
            this.txtSearchTitle.SelectOnFocus = true;
            this.txtSearchTitle.Size = new System.Drawing.Size(137, 20);
            this.txtSearchTitle.TabIndex = 2;
            this.txtSearchTitle.TextChangedDelay = 250;
            this.txtSearchTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearchartist_KeyDown);
            // 
            // onlinesearchbtn
            // 
            this.onlinesearchbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.onlinesearchbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.onlinesearchbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.onlinesearchbtn.Location = new System.Drawing.Point(580, 3);
            this.onlinesearchbtn.Name = "onlinesearchbtn";
            this.onlinesearchbtn.Size = new System.Drawing.Size(75, 23);
            this.onlinesearchbtn.TabIndex = 27;
            this.onlinesearchbtn.Text = "Search";
            this.onlinesearchbtn.UseVisualStyleBackColor = true;
            this.onlinesearchbtn.Click += new System.EventHandler(this.onlinesearchbtn_Click);
            // 
            // searchTypeList
            // 
            this.searchTypeList.DefaultText = "All Types";
            this.searchTypeList.DisplayDefault = true;
            this.searchTypeList.Location = new System.Drawing.Point(294, 4);
            this.searchTypeList.Name = "searchTypeList";
            this.searchTypeList.Size = new System.Drawing.Size(137, 21);
            this.searchTypeList.TabIndex = 37;
            this.searchTypeList.UsePluralizedNames = false;
            // 
            // txtSearchArtist
            // 
            this.txtSearchArtist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSearchArtist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearchArtist.Location = new System.Drawing.Point(8, 5);
            this.txtSearchArtist.Name = "txtSearchArtist";
            this.txtSearchArtist.PlaceholderForecolor = System.Drawing.Color.DarkGray;
            this.txtSearchArtist.PlaceholderText = " Artist";
            this.txtSearchArtist.SelectOnFocus = true;
            this.txtSearchArtist.Size = new System.Drawing.Size(137, 20);
            this.txtSearchArtist.TabIndex = 1;
            this.txtSearchArtist.TextChangedDelay = 250;
            this.txtSearchArtist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearchartist_KeyDown);
            // 
            // tabimagelist
            // 
            this.tabimagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabimagelist.ImageStream")));
            this.tabimagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.tabimagelist.Images.SetKeyName(0, "application_view_detail.png");
            this.tabimagelist.Images.SetKeyName(1, "search_plus.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblcount,
            this.toolStripSeparator2,
            this.lblplaylists,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 438);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1034, 23);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 24;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblcount
            // 
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(74, 18);
            this.lblcount.Text = "Total Tabs: 0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // lblplaylists
            // 
            this.lblplaylists.Name = "lblplaylists";
            this.lblplaylists.Size = new System.Drawing.Size(91, 18);
            this.lblplaylists.Text = "Total Playlists: 0";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(848, 18);
            this.lblStatus.Spring = true;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SearchMenu
            // 
            this.SearchMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTabToolStripMenuItem1,
            this.previewToolStripMenuItem,
            this.copyURLToolStripMenuItem});
            this.SearchMenu.Name = "librarycontextmenu";
            this.SearchMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.SearchMenu.ShowImageMargin = false;
            this.SearchMenu.ShowItemToolTips = false;
            this.SearchMenu.Size = new System.Drawing.Size(102, 70);
            // 
            // saveTabToolStripMenuItem1
            // 
            this.saveTabToolStripMenuItem1.Enabled = false;
            this.saveTabToolStripMenuItem1.Name = "saveTabToolStripMenuItem1";
            this.saveTabToolStripMenuItem1.Size = new System.Drawing.Size(101, 22);
            this.saveTabToolStripMenuItem1.Text = "Save Tab";
            this.saveTabToolStripMenuItem1.Click += new System.EventHandler(this.saveTabToolStripMenuItem1_Click);
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.previewToolStripMenuItem.Text = "Preview";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.previewToolStripMenuItem_Click);
            // 
            // copyURLToolStripMenuItem
            // 
            this.copyURLToolStripMenuItem.Name = "copyURLToolStripMenuItem";
            this.copyURLToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.copyURLToolStripMenuItem.Text = "Copy URL";
            this.copyURLToolStripMenuItem.Click += new System.EventHandler(this.copyURLToolStripMenuItem_Click);
            // 
            // refreshbtn
            // 
            this.refreshbtn.Name = "refreshbtn";
            this.refreshbtn.Size = new System.Drawing.Size(23, 20);
            // 
            // LibraryMenu
            // 
            this.LibraryMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.librarycontextdetails,
            this.librarycontextdelete,
            this.librarycontextexport,
            this.librarycontextbrowse,
            this.searchSimilarTabsToolStripMenuItem,
            this.librarycontextaddtoplaylist,
            this.librarycontextfavorites,
            this.sortByToolStripMenuItem});
            this.LibraryMenu.Name = "contextMenuStrip1";
            this.LibraryMenu.ShowImageMargin = false;
            this.LibraryMenu.ShowItemToolTips = false;
            this.LibraryMenu.Size = new System.Drawing.Size(152, 180);
            // 
            // librarycontextdetails
            // 
            this.librarycontextdetails.Name = "librarycontextdetails";
            this.librarycontextdetails.Size = new System.Drawing.Size(151, 22);
            this.librarycontextdetails.Text = "Details...";
            this.librarycontextdetails.Click += new System.EventHandler(this.TabDetails);
            // 
            // librarycontextdelete
            // 
            this.librarycontextdelete.Name = "librarycontextdelete";
            this.librarycontextdelete.Size = new System.Drawing.Size(151, 22);
            this.librarycontextdelete.Text = "Delete";
            this.librarycontextdelete.Click += new System.EventHandler(this.DeleteTab);
            // 
            // librarycontextexport
            // 
            this.librarycontextexport.Name = "librarycontextexport";
            this.librarycontextexport.Size = new System.Drawing.Size(151, 22);
            this.librarycontextexport.Text = "Export...";
            this.librarycontextexport.Click += new System.EventHandler(this.ExportTab);
            // 
            // librarycontextbrowse
            // 
            this.librarycontextbrowse.Name = "librarycontextbrowse";
            this.librarycontextbrowse.Size = new System.Drawing.Size(151, 22);
            this.librarycontextbrowse.Text = "Open Tab Location";
            this.librarycontextbrowse.Click += new System.EventHandler(this.OpenTabLocation);
            // 
            // searchSimilarTabsToolStripMenuItem
            // 
            this.searchSimilarTabsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchByArtistToolStripMenuItem,
            this.searchByTitleToolStripMenuItem,
            this.searchByArtistAndTitleToolStripMenuItem});
            this.searchSimilarTabsToolStripMenuItem.Name = "searchSimilarTabsToolStripMenuItem";
            this.searchSimilarTabsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.searchSimilarTabsToolStripMenuItem.Text = "Search Similar Tabs";
            // 
            // searchByArtistToolStripMenuItem
            // 
            this.searchByArtistToolStripMenuItem.Name = "searchByArtistToolStripMenuItem";
            this.searchByArtistToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.searchByArtistToolStripMenuItem.Text = "Search By Artist";
            this.searchByArtistToolStripMenuItem.Click += new System.EventHandler(this.SearchSimilarTabs);
            // 
            // searchByTitleToolStripMenuItem
            // 
            this.searchByTitleToolStripMenuItem.Name = "searchByTitleToolStripMenuItem";
            this.searchByTitleToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.searchByTitleToolStripMenuItem.Text = "Search By Title";
            this.searchByTitleToolStripMenuItem.Click += new System.EventHandler(this.SearchSimilarTabs);
            // 
            // searchByArtistAndTitleToolStripMenuItem
            // 
            this.searchByArtistAndTitleToolStripMenuItem.Name = "searchByArtistAndTitleToolStripMenuItem";
            this.searchByArtistAndTitleToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.searchByArtistAndTitleToolStripMenuItem.Text = "Search By Artist and Title";
            this.searchByArtistAndTitleToolStripMenuItem.Click += new System.EventHandler(this.SearchSimilarTabs);
            // 
            // librarycontextaddtoplaylist
            // 
            this.librarycontextaddtoplaylist.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPlaylistToolStripMenuItem});
            this.librarycontextaddtoplaylist.Name = "librarycontextaddtoplaylist";
            this.librarycontextaddtoplaylist.Size = new System.Drawing.Size(151, 22);
            this.librarycontextaddtoplaylist.Text = "Add to Playlist...";
            // 
            // newPlaylistToolStripMenuItem
            // 
            this.newPlaylistToolStripMenuItem.Name = "newPlaylistToolStripMenuItem";
            this.newPlaylistToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.newPlaylistToolStripMenuItem.Text = "New Playlist...";
            this.newPlaylistToolStripMenuItem.Click += new System.EventHandler(this.NewPlaylist);
            // 
            // librarycontextfavorites
            // 
            this.librarycontextfavorites.Name = "librarycontextfavorites";
            this.librarycontextfavorites.Size = new System.Drawing.Size(151, 22);
            this.librarycontextfavorites.Text = "Add to Favorites...";
            this.librarycontextfavorites.Click += new System.EventHandler(this.ToggleFavorite);
            // 
            // sortByToolStripMenuItem
            // 
            this.sortByToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ascendingToolStripMenuItem,
            this.descendingToolStripMenuItem});
            this.sortByToolStripMenuItem.Name = "sortByToolStripMenuItem";
            this.sortByToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.sortByToolStripMenuItem.Text = "Sort By";
            // 
            // ascendingToolStripMenuItem
            // 
            this.ascendingToolStripMenuItem.Name = "ascendingToolStripMenuItem";
            this.ascendingToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.ascendingToolStripMenuItem.Text = "Ascending";
            this.ascendingToolStripMenuItem.Click += new System.EventHandler(this.SortByDirectionMenuItem_Click);
            // 
            // descendingToolStripMenuItem
            // 
            this.descendingToolStripMenuItem.Name = "descendingToolStripMenuItem";
            this.descendingToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.descendingToolStripMenuItem.Text = "Descending";
            this.descendingToolStripMenuItem.Click += new System.EventHandler(this.SortByDirectionMenuItem_Click);
            // 
            // PlaylistMenu
            // 
            this.PlaylistMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteplaylistcontextmenuitem,
            this.playlistInformationToolStripMenuItem});
            this.PlaylistMenu.Name = "librarycontextmenu";
            this.PlaylistMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.PlaylistMenu.ShowImageMargin = false;
            this.PlaylistMenu.ShowItemToolTips = false;
            this.PlaylistMenu.Size = new System.Drawing.Size(153, 48);
            // 
            // deleteplaylistcontextmenuitem
            // 
            this.deleteplaylistcontextmenuitem.Name = "deleteplaylistcontextmenuitem";
            this.deleteplaylistcontextmenuitem.Size = new System.Drawing.Size(152, 22);
            this.deleteplaylistcontextmenuitem.Text = "Delete Playlist";
            this.deleteplaylistcontextmenuitem.Click += new System.EventHandler(this.DeletePlaylist);
            // 
            // playlistInformationToolStripMenuItem
            // 
            this.playlistInformationToolStripMenuItem.Name = "playlistInformationToolStripMenuItem";
            this.playlistInformationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.playlistInformationToolStripMenuItem.Text = "Playlist Information";
            this.playlistInformationToolStripMenuItem.Click += new System.EventHandler(this.PlaylistDetails);
            // 
            // SearchPreviewBackgroundWorker
            // 
            this.SearchPreviewBackgroundWorker.WorkerSupportsCancellation = true;
            this.SearchPreviewBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SearchPreviewBackgroundWorker_DoWork);
            this.SearchPreviewBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SearchPreviewBackgroundWorker_RunWorkerCompleted);
            // 
            // SearchBackgroundWorker
            // 
            this.SearchBackgroundWorker.WorkerReportsProgress = true;
            this.SearchBackgroundWorker.WorkerSupportsCancellation = true;
            this.SearchBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SearchBackgroundWorker_DoWork);
            this.SearchBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SearchBackgroundWorker_ProgressChanged);
            this.SearchBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SearchBackgroundWorker_RunWorkerCompleted);
            // 
            // PreviewDisplayDelay
            // 
            this.PreviewDisplayDelay.Tick += new System.EventHandler(this.PreviewDisplayDelay_Tick);
            // 
            // PreviewDisplayTimer
            // 
            this.PreviewDisplayTimer.Interval = 5000;
            this.PreviewDisplayTimer.Tick += new System.EventHandler(this.PreviewDisplayTimer_Tick);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.newTabMenuItem,
            this.newPlaylistMenuItem,
            this.openTabMenuItem,
            this.importMenuItem,
            this.recentlyViewedMenuItem,
            this.exitMenuItem});
            this.menuItem1.Text = "&File";
            // 
            // newTabMenuItem
            // 
            this.newTabMenuItem.Index = 0;
            this.newTabMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.newTabMenuItem.Text = "New Tab...";
            this.newTabMenuItem.Click += new System.EventHandler(this.NewTab);
            // 
            // newPlaylistMenuItem
            // 
            this.newPlaylistMenuItem.Index = 1;
            this.newPlaylistMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftN;
            this.newPlaylistMenuItem.Text = "New Playlist...";
            this.newPlaylistMenuItem.Click += new System.EventHandler(this.NewPlaylist);
            // 
            // openTabMenuItem
            // 
            this.openTabMenuItem.Index = 2;
            this.openTabMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.openTabMenuItem.Text = "Open...";
            this.openTabMenuItem.Click += new System.EventHandler(this.BrowseTab);
            // 
            // importMenuItem
            // 
            this.importMenuItem.Index = 3;
            this.importMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.importMenuItem.Text = "Import...";
            this.importMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // recentlyViewedMenuItem
            // 
            this.recentlyViewedMenuItem.ClearOptionText = "Clear All Recent Items";
            this.recentlyViewedMenuItem.DisplayClearOption = true;
            this.recentlyViewedMenuItem.DisplayMode = Tabster.Controls.RecentToolStripMenuItem.RecentFilesDisplayMode.Consecutive;
            this.recentlyViewedMenuItem.DisplayOpenAllOption = true;
            this.recentlyViewedMenuItem.Enabled = false;
            this.recentlyViewedMenuItem.Index = 4;
            this.recentlyViewedMenuItem.MaxDisplayItems = 10;
            this.recentlyViewedMenuItem.OpenAllOptionText = "Open All Recent Items";
            this.recentlyViewedMenuItem.PrependItemNumbers = true;
            this.recentlyViewedMenuItem.Text = "Open Recent";
            this.recentlyViewedMenuItem.Visible = false;
            this.recentlyViewedMenuItem.OnItemClicked += new System.EventHandler(this.recentlyViewedMenuItem_OnItemClicked);
            this.recentlyViewedMenuItem.OnAllItemsOpened += new System.EventHandler(this.recentlyViewedMenuItem_OnAllItemsOpened);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 5;
            this.exitMenuItem.Text = "&Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.libraryPreviewPaneToolStripMenuItem,
            this.searchPreviewPaneToolStripMenuItem});
            this.menuItem2.Text = "&View";
            // 
            // libraryPreviewPaneToolStripMenuItem
            // 
            this.libraryPreviewPaneToolStripMenuItem.Index = 0;
            this.libraryPreviewPaneToolStripMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.libraryhiddenpreviewToolStripMenuItem,
            this.libraryhorizontalpreviewToolStripMenuItem,
            this.libraryverticalpreviewToolStripMenuItem});
            this.libraryPreviewPaneToolStripMenuItem.Text = "Library Preview Pane";
            // 
            // libraryhiddenpreviewToolStripMenuItem
            // 
            this.libraryhiddenpreviewToolStripMenuItem.Index = 0;
            this.libraryhiddenpreviewToolStripMenuItem.Text = "Hidden";
            this.libraryhiddenpreviewToolStripMenuItem.Click += new System.EventHandler(this.TogglePreviewPane);
            // 
            // libraryhorizontalpreviewToolStripMenuItem
            // 
            this.libraryhorizontalpreviewToolStripMenuItem.Index = 1;
            this.libraryhorizontalpreviewToolStripMenuItem.Text = "Horizontal";
            this.libraryhorizontalpreviewToolStripMenuItem.Click += new System.EventHandler(this.TogglePreviewPane);
            // 
            // libraryverticalpreviewToolStripMenuItem
            // 
            this.libraryverticalpreviewToolStripMenuItem.Index = 2;
            this.libraryverticalpreviewToolStripMenuItem.Text = "Vertical";
            this.libraryverticalpreviewToolStripMenuItem.Click += new System.EventHandler(this.TogglePreviewPane);
            // 
            // searchPreviewPaneToolStripMenuItem
            // 
            this.searchPreviewPaneToolStripMenuItem.Index = 1;
            this.searchPreviewPaneToolStripMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.searchhiddenpreviewToolStripMenuItem,
            this.searchhorizontalpreviewToolStripMenuItem,
            this.searchverticalpreviewToolStripMenuItem});
            this.searchPreviewPaneToolStripMenuItem.Text = "Search Preview Pane";
            // 
            // searchhiddenpreviewToolStripMenuItem
            // 
            this.searchhiddenpreviewToolStripMenuItem.Index = 0;
            this.searchhiddenpreviewToolStripMenuItem.Text = "Hidden";
            this.searchhiddenpreviewToolStripMenuItem.Click += new System.EventHandler(this.TogglePreviewPane);
            // 
            // searchhorizontalpreviewToolStripMenuItem
            // 
            this.searchhorizontalpreviewToolStripMenuItem.Index = 1;
            this.searchhorizontalpreviewToolStripMenuItem.Text = "Horizontal";
            this.searchhorizontalpreviewToolStripMenuItem.Click += new System.EventHandler(this.TogglePreviewPane);
            // 
            // searchverticalpreviewToolStripMenuItem
            // 
            this.searchverticalpreviewToolStripMenuItem.Index = 2;
            this.searchverticalpreviewToolStripMenuItem.Text = "Vertical";
            this.searchverticalpreviewToolStripMenuItem.Click += new System.EventHandler(this.TogglePreviewPane);
            // 
            // menuItem3
            // 
            this.menuItem3.Enabled = false;
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.viewTabToolStripMenuItem,
            this.detailsToolStripMenuItem,
            this.deleteTabToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.openTabLocationToolStripMenuItem});
            this.menuItem3.Text = "&Library";
            // 
            // viewTabToolStripMenuItem
            // 
            this.viewTabToolStripMenuItem.Index = 0;
            this.viewTabToolStripMenuItem.Text = "Popout Tab";
            this.viewTabToolStripMenuItem.Click += new System.EventHandler(this.viewTabToolStripMenuItem_Click);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Index = 1;
            this.detailsToolStripMenuItem.Text = "Details...";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.TabDetails);
            // 
            // deleteTabToolStripMenuItem
            // 
            this.deleteTabToolStripMenuItem.Index = 2;
            this.deleteTabToolStripMenuItem.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.deleteTabToolStripMenuItem.Text = "Delete";
            this.deleteTabToolStripMenuItem.Click += new System.EventHandler(this.DeleteTab);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Index = 3;
            this.exportToolStripMenuItem.Text = "Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportTab);
            // 
            // openTabLocationToolStripMenuItem
            // 
            this.openTabLocationToolStripMenuItem.Index = 4;
            this.openTabLocationToolStripMenuItem.Text = "Open Tab Location";
            this.openTabLocationToolStripMenuItem.Click += new System.EventHandler(this.OpenTabLocation);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.batchDownloaderMenuItem,
            this.pluginsMenuItem,
            this.preferencesMenuItem});
            this.menuItem4.Text = "&Tools";
            // 
            // batchDownloaderMenuItem
            // 
            this.batchDownloaderMenuItem.Index = 0;
            this.batchDownloaderMenuItem.Text = "Batch Downloader";
            this.batchDownloaderMenuItem.Click += new System.EventHandler(this.batchDownloaderMenuItem_Click);
            // 
            // pluginsMenuItem
            // 
            this.pluginsMenuItem.Index = 1;
            this.pluginsMenuItem.Text = "Plugin Manager";
            this.pluginsMenuItem.Click += new System.EventHandler(this.pluginsToolStripMenuItem_Click);
            // 
            // preferencesMenuItem
            // 
            this.preferencesMenuItem.Index = 2;
            this.preferencesMenuItem.Text = "Preferences";
            this.preferencesMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.checkForUpdatesMenuItem,
            this.aboutMenuItem});
            this.menuItem5.Text = "&Help";
            // 
            // checkForUpdatesMenuItem
            // 
            this.checkForUpdatesMenuItem.Index = 0;
            this.checkForUpdatesMenuItem.Text = "Check for Updates...";
            this.checkForUpdatesMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 1;
            this.aboutMenuItem.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.aboutMenuItem.Text = "About...";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.onlinesearchbtn;
            this.ClientSize = new System.Drawing.Size(1034, 461);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(960, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tabster";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.display_library.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.librarySplitContainer.Panel1.ResumeLayout(false);
            this.librarySplitContainer.Panel2.ResumeLayout(false);
            this.librarySplitContainer.Panel2.PerformLayout();
            this.librarySplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listViewLibrary)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabsCurrentTab.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.previewToolStrip.ResumeLayout(false);
            this.previewToolStrip.PerformLayout();
            this.display_search.ResumeLayout(false);
            this.searchSplitContainer.Panel1.ResumeLayout(false);
            this.searchSplitContainer.Panel2.ResumeLayout(false);
            this.searchSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listViewSearch)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.SearchMenu.ResumeLayout(false);
            this.LibraryMenu.ResumeLayout(false);
            this.PlaylistMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem deletePlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renamePlaylistToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage display_library;
        private System.Windows.Forms.ToolStripButton refreshbtn;
        private System.Windows.Forms.ContextMenuStrip LibraryMenu;
        private System.Windows.Forms.ToolStripMenuItem librarycontextaddtoplaylist;
        private System.Windows.Forms.ContextMenuStrip PlaylistMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteplaylistcontextmenuitem;
        private System.Windows.Forms.ImageList tabimagelist;
        private System.Windows.Forms.TabPage display_search;
        private System.Windows.Forms.Button onlinesearchbtn;
        private System.Windows.Forms.ContextMenuStrip SearchMenu;
        private System.Windows.Forms.ToolStripMenuItem saveTabToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyURLToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblcount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel lblplaylists;
        private ToolStripMenuItem librarycontextdetails;
        private ToolStripMenuItem librarycontextdelete;
        private ToolStripMenuItem librarycontextexport;
        private ToolStripMenuItem librarycontextbrowse;
        private Button button2;
        private Button button1;
        private StaticTreeView sidemenu;
        private SplitContainer librarySplitContainer;
        private ToolStrip previewToolStrip;
        private ToolStripLabel lblpreviewtitle;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton detailsbtn;
        private ToolStripMenuItem previewToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripDropDownButton toolStripButton3;
        private ToolStripMenuItem offToolStripMenuItem;
        private ToolStripMenuItem playlistInformationToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker SearchPreviewBackgroundWorker;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem librarycontextfavorites;
        private Label lblLibraryPreview;
        private ToolStripMenuItem onToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker SearchBackgroundWorker;
        private ToolStripStatusLabel lblStatus;
        private ToolStripMenuItem searchSimilarTabsToolStripMenuItem;
        private ToolStripMenuItem searchByArtistToolStripMenuItem;
        private ToolStripMenuItem searchByTitleToolStripMenuItem;
        private ToolStripMenuItem searchByArtistAndTitleToolStripMenuItem;
        private Timer PreviewDisplayDelay;
        private Timer PreviewDisplayTimer;
        private MainMenu mainMenu1;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private MenuItem newTabMenuItem;
        private MenuItem newPlaylistMenuItem;
        private MenuItem openTabMenuItem;
        private MenuItem importMenuItem;
        private MenuItem exitMenuItem;
        private MenuItem checkForUpdatesMenuItem;
        private MenuItem aboutMenuItem;
        private MenuItem preferencesMenuItem;
        private RecentToolStripMenuItem recentlyViewedMenuItem;
        private MenuItem viewTabToolStripMenuItem;
        private MenuItem detailsToolStripMenuItem;
        private MenuItem deleteTabToolStripMenuItem;
        private MenuItem exportToolStripMenuItem;
        private MenuItem openTabLocationToolStripMenuItem;
        private MenuItem libraryPreviewPaneToolStripMenuItem;
        private MenuItem searchPreviewPaneToolStripMenuItem;
        private MenuItem libraryhiddenpreviewToolStripMenuItem;
        private MenuItem libraryhorizontalpreviewToolStripMenuItem;
        private MenuItem libraryverticalpreviewToolStripMenuItem;
        private MenuItem searchhiddenpreviewToolStripMenuItem;
        private MenuItem searchhorizontalpreviewToolStripMenuItem;
        private MenuItem searchverticalpreviewToolStripMenuItem;
        private Panel panel2;
        private TextBoxExtended txtLibraryFilter;
        private TabTypeDropdown searchTypeList;
        private ToolStripMenuItem sortByToolStripMenuItem;
        private MenuItem batchDownloaderMenuItem;
        private BasicTablatureTextEditor PreviewEditor;
        private ToolStripSplitButton printbtn;
        private ToolStripMenuItem printPreviewToolStripMenuItem;
        private ToolStripMenuItem printSettingsToolStripMenuItem;
        private ObjectListView listViewLibrary;
        private OLVColumn olvColTitle;
        private OLVColumn olvColArtist;
        private OLVColumn olvColType;
        private OLVColumn olvColCreated;
        private OLVColumn olvColModified;
        private OLVColumn olvColViews;
        private OLVColumn olvColLocation;
        private ToolStripMenuItem newPlaylistToolStripMenuItem;
        private ToolStripMenuItem ascendingToolStripMenuItem;
        private ToolStripMenuItem descendingToolStripMenuItem;
        private SplitContainer searchSplitContainer;
        private BasicTablatureTextEditor searchPreviewEditor;
        private Panel panel3;
        private TextBoxExtended txtSearchArtist;
        private ObjectListView listViewSearch;
        private OLVColumn olvColumn1;
        private OLVColumn olvColumn2;
        private OLVColumn olvColumn3;
        private OLVColumn olvColumn4;
        private TextBoxExtended txtSearchTitle;
        private Button btnSearchOptions;
        private OLVColumn olvColumn5;
        private OLVColumn olvColumn6;
        private TablatureRatingDropdown cbSearchRating;
        private TabControl tabsCurrentTab;
        private TabPage tabPage1;
        private TabPage tabPage3;
        public TextBoxExtended txtLyrics;
        private TabPage tabPage2;
        private Label label9;
        private Label label10;
        private Label label8;
        private Label label7;
        private Label label6;
        public Label label4;
        public Label label2;
        private Label label1;
        private Label label16;
        private Label label17;
        public Label label5;
        private Label label3;
        private Label label11;
        private Label lblCurrentType;
        private Label lblCurrentTitle;
        private Label lblCurrentArtist;
        private Label lblCurrentRating;
        private Label lblCurrentGenre;
        private Label lblCurrentAlbum;
        private Label lblCurrentCopyright;
        private Label lblCurrentAuthor;
        private Label lblCurrentDifficulty;
        private Label lblCurrentSubtitle;
        private Label lblCurrentTuning;
        private GroupBox groupBox1;
        private Label lblCurrentComment;
        private GroupBox groupBox2;
        private Label label18;
        private Label label25;
        private Label label22;
        private Label label24;
        private Label label27;
        private Label lblFormat;
        private Label lblCurrentEncoding;
        private Label lblCurrentCompressed;
        private Label lblCurrentModified;
        private Label lblCurrentCreated;
        private Label lblCurrentLength;
        private Label lblCurrentFormat;
        private Label lblCurrentLocation;
        private ImageList TablaturePanelsImageList;
        private MenuItem pluginsMenuItem;
    }
}

