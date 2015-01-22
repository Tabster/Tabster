using System.Windows.Forms;
using BrightIdeasSoftware;
using Tabster.Controls;
using DataGridViewExtended = Tabster.Controls.DataGridViewExtended;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.PreviewEditor = new Tabster.Controls.BasicTablatureTextEditor();
            this.lblLibraryPreview = new System.Windows.Forms.Label();
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
            this.display_search = new System.Windows.Forms.TabPage();
            this.searchSplitContainer = new System.Windows.Forms.SplitContainer();
            this.searchDisplay = new Tabster.Controls.DataGridViewExtended();
            this.searchcol_artist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchcol_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchcol_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_rating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchcol_service = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchPreviewEditor = new Tabster.Controls.BasicTablatureTextEditor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchTypeList = new Tabster.Controls.TabTypeDropdown();
            this.resetSearchbtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSearchRating = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listSearchServices = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.onlinesearchbtn = new System.Windows.Forms.Button();
            this.txtSearchArtist = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearchTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.openPlaylistMenuItem = new System.Windows.Forms.MenuItem();
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
            this.previewToolStrip.SuspendLayout();
            this.display_search.SuspendLayout();
            this.searchSplitContainer.Panel1.SuspendLayout();
            this.searchSplitContainer.Panel2.SuspendLayout();
            this.searchSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchDisplay)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
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
            this.librarySplitContainer.Panel2.Controls.Add(this.PreviewEditor);
            this.librarySplitContainer.Panel2.Controls.Add(this.lblLibraryPreview);
            this.librarySplitContainer.Panel2.Controls.Add(this.previewToolStrip);
            this.librarySplitContainer.Panel2MinSize = 140;
            this.librarySplitContainer.Size = new System.Drawing.Size(885, 410);
            this.librarySplitContainer.SplitterDistance = 237;
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
            this.listViewLibrary.Size = new System.Drawing.Size(883, 206);
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
            this.txtLibraryFilter.Location = new System.Drawing.Point(739, 3);
            this.txtLibraryFilter.Name = "txtLibraryFilter";
            this.txtLibraryFilter.PlaceholderForecolor = System.Drawing.Color.DarkGray;
            this.txtLibraryFilter.PlaceholderText = " Search Library";
            this.txtLibraryFilter.SelectOnFocus = true;
            this.txtLibraryFilter.Size = new System.Drawing.Size(137, 20);
            this.txtLibraryFilter.TabIndex = 0;
            this.txtLibraryFilter.TextChangedDelay = 250;
            this.txtLibraryFilter.TextChanged += new System.EventHandler(this.txtLibraryFilter_TextChanged);
            // 
            // PreviewEditor
            // 
            this.PreviewEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewEditor.Location = new System.Drawing.Point(0, 25);
            this.PreviewEditor.Name = "PreviewEditor";
            this.PreviewEditor.ReadOnly = true;
            this.PreviewEditor.Size = new System.Drawing.Size(883, 142);
            this.PreviewEditor.TabIndex = 25;
            this.PreviewEditor.ContentsModified += new System.EventHandler(this.PreviewEditor_ContentsModified);
            // 
            // lblLibraryPreview
            // 
            this.lblLibraryPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLibraryPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLibraryPreview.Location = new System.Drawing.Point(0, 25);
            this.lblLibraryPreview.Name = "lblLibraryPreview";
            this.lblLibraryPreview.Size = new System.Drawing.Size(883, 142);
            this.lblLibraryPreview.TabIndex = 24;
            this.lblLibraryPreview.Text = "Tab is open in external viewer.";
            this.lblLibraryPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLibraryPreview.Visible = false;
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
            this.detailsbtn.Text = "Tab Details";
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
            this.offToolStripMenuItem.Click += new System.EventHandler(this.AutoScrollChange);
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.AutoScrollChange);
            // 
            // display_search
            // 
            this.display_search.BackColor = System.Drawing.SystemColors.Control;
            this.display_search.Controls.Add(this.searchSplitContainer);
            this.display_search.Controls.Add(this.panel1);
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
            this.searchSplitContainer.Location = new System.Drawing.Point(199, 0);
            this.searchSplitContainer.Name = "searchSplitContainer";
            this.searchSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // searchSplitContainer.Panel1
            // 
            this.searchSplitContainer.Panel1.Controls.Add(this.searchDisplay);
            this.searchSplitContainer.Panel1MinSize = 200;
            // 
            // searchSplitContainer.Panel2
            // 
            this.searchSplitContainer.Panel2.Controls.Add(this.searchPreviewEditor);
            this.searchSplitContainer.Panel2Collapsed = true;
            this.searchSplitContainer.Panel2MinSize = 100;
            this.searchSplitContainer.Size = new System.Drawing.Size(827, 410);
            this.searchSplitContainer.SplitterDistance = 200;
            this.searchSplitContainer.TabIndex = 29;
            // 
            // searchDisplay
            // 
            this.searchDisplay.AllowDrop = true;
            this.searchDisplay.AllowUserToAddRows = false;
            this.searchDisplay.AllowUserToDeleteRows = false;
            this.searchDisplay.AllowUserToResizeColumns = false;
            this.searchDisplay.AllowUserToResizeRows = false;
            this.searchDisplay.BackgroundColor = System.Drawing.Color.White;
            this.searchDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.searchDisplay.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.searchDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.searchDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.searchcol_artist,
            this.searchcol_title,
            this.searchcol_type,
            this.col_rating,
            this.searchcol_service});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.searchDisplay.DefaultCellStyle = dataGridViewCellStyle3;
            this.searchDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchDisplay.GridColor = System.Drawing.SystemColors.ControlLight;
            this.searchDisplay.Location = new System.Drawing.Point(0, 0);
            this.searchDisplay.MultiSelect = false;
            this.searchDisplay.Name = "searchDisplay";
            this.searchDisplay.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.searchDisplay.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.searchDisplay.RowHeadersVisible = false;
            this.searchDisplay.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.searchDisplay.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.searchDisplay.RowTemplate.Height = 18;
            this.searchDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.searchDisplay.ShowCellErrors = false;
            this.searchDisplay.ShowCellToolTips = false;
            this.searchDisplay.ShowEditingIcon = false;
            this.searchDisplay.ShowRowErrors = false;
            this.searchDisplay.Size = new System.Drawing.Size(825, 408);
            this.searchDisplay.TabIndex = 20;
            this.searchDisplay.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.searchDisplay_CellDoubleClick);
            this.searchDisplay.SelectionChanged += new System.EventHandler(this.dataGridViewExtended1_SelectionChanged);
            this.searchDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewExtended1_MouseClick);
            // 
            // searchcol_artist
            // 
            this.searchcol_artist.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.searchcol_artist.HeaderText = "Artist";
            this.searchcol_artist.Name = "searchcol_artist";
            this.searchcol_artist.ReadOnly = true;
            this.searchcol_artist.Width = 185;
            // 
            // searchcol_title
            // 
            this.searchcol_title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.searchcol_title.HeaderText = "Title";
            this.searchcol_title.Name = "searchcol_title";
            this.searchcol_title.ReadOnly = true;
            this.searchcol_title.Width = 225;
            // 
            // searchcol_type
            // 
            this.searchcol_type.HeaderText = "Type";
            this.searchcol_type.Name = "searchcol_type";
            this.searchcol_type.ReadOnly = true;
            // 
            // col_rating
            // 
            this.col_rating.HeaderText = "Rating";
            this.col_rating.Name = "col_rating";
            this.col_rating.ReadOnly = true;
            // 
            // searchcol_service
            // 
            this.searchcol_service.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Format = "N0";
            this.searchcol_service.DefaultCellStyle = dataGridViewCellStyle2;
            this.searchcol_service.HeaderText = "Service";
            this.searchcol_service.Name = "searchcol_service";
            this.searchcol_service.ReadOnly = true;
            this.searchcol_service.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // searchPreviewEditor
            // 
            this.searchPreviewEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPreviewEditor.Location = new System.Drawing.Point(0, 0);
            this.searchPreviewEditor.Margin = new System.Windows.Forms.Padding(0);
            this.searchPreviewEditor.Name = "searchPreviewEditor";
            this.searchPreviewEditor.ReadOnly = true;
            this.searchPreviewEditor.Size = new System.Drawing.Size(148, 23);
            this.searchPreviewEditor.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.searchTypeList);
            this.panel1.Controls.Add(this.resetSearchbtn);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cbSearchRating);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.listSearchServices);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.onlinesearchbtn);
            this.panel1.Controls.Add(this.txtSearchArtist);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtSearchTitle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 410);
            this.panel1.TabIndex = 28;
            // 
            // searchTypeList
            // 
            this.searchTypeList.DefaultText = "All Types";
            this.searchTypeList.DisplayDefault = true;
            this.searchTypeList.Location = new System.Drawing.Point(56, 70);
            this.searchTypeList.Name = "searchTypeList";
            this.searchTypeList.Size = new System.Drawing.Size(137, 21);
            this.searchTypeList.TabIndex = 37;
            this.searchTypeList.UsePluralizedNames = false;
            // 
            // resetSearchbtn
            // 
            this.resetSearchbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.resetSearchbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.resetSearchbtn.Location = new System.Drawing.Point(11, 259);
            this.resetSearchbtn.Name = "resetSearchbtn";
            this.resetSearchbtn.Size = new System.Drawing.Size(75, 23);
            this.resetSearchbtn.TabIndex = 36;
            this.resetSearchbtn.Text = "Reset";
            this.resetSearchbtn.UseVisualStyleBackColor = true;
            this.resetSearchbtn.Click += new System.EventHandler(this.resetSearchbtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(4, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Rating:";
            // 
            // cbSearchRating
            // 
            this.cbSearchRating.BackColor = System.Drawing.SystemColors.Window;
            this.cbSearchRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchRating.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbSearchRating.FormattingEnabled = true;
            this.cbSearchRating.ItemHeight = 13;
            this.cbSearchRating.Items.AddRange(new object[] {
            "All Ratings",
            "★",
            "★★",
            "★★★",
            "★★★★",
            "★★★★★"});
            this.cbSearchRating.Location = new System.Drawing.Point(56, 97);
            this.cbSearchRating.Name = "cbSearchRating";
            this.cbSearchRating.Size = new System.Drawing.Size(137, 21);
            this.cbSearchRating.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(8, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Services:";
            // 
            // listSearchServices
            // 
            this.listSearchServices.FormattingEnabled = true;
            this.listSearchServices.Location = new System.Drawing.Point(11, 145);
            this.listSearchServices.Name = "listSearchServices";
            this.listSearchServices.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listSearchServices.Size = new System.Drawing.Size(172, 108);
            this.listSearchServices.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(5, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Artist:";
            // 
            // onlinesearchbtn
            // 
            this.onlinesearchbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.onlinesearchbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.onlinesearchbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.onlinesearchbtn.Location = new System.Drawing.Point(108, 259);
            this.onlinesearchbtn.Name = "onlinesearchbtn";
            this.onlinesearchbtn.Size = new System.Drawing.Size(75, 23);
            this.onlinesearchbtn.TabIndex = 27;
            this.onlinesearchbtn.Text = "Search";
            this.onlinesearchbtn.UseVisualStyleBackColor = true;
            this.onlinesearchbtn.Click += new System.EventHandler(this.onlinesearchbtn_Click);
            // 
            // txtSearchArtist
            // 
            this.txtSearchArtist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSearchArtist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearchArtist.BackColor = System.Drawing.SystemColors.Window;
            this.txtSearchArtist.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSearchArtist.Location = new System.Drawing.Point(44, 18);
            this.txtSearchArtist.Name = "txtSearchArtist";
            this.txtSearchArtist.Size = new System.Drawing.Size(149, 20);
            this.txtSearchArtist.TabIndex = 21;
            this.txtSearchArtist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearchartist_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(4, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Type:";
            // 
            // txtSearchTitle
            // 
            this.txtSearchTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSearchTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearchTitle.BackColor = System.Drawing.SystemColors.Window;
            this.txtSearchTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSearchTitle.Location = new System.Drawing.Point(44, 44);
            this.txtSearchTitle.Name = "txtSearchTitle";
            this.txtSearchTitle.Size = new System.Drawing.Size(150, 20);
            this.txtSearchTitle.TabIndex = 22;
            this.txtSearchTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearchartist_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(8, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Title:";
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
            this.librarycontextexport.Text = "Export";
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
            this.openPlaylistMenuItem,
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
            // openPlaylistMenuItem
            // 
            this.openPlaylistMenuItem.Index = 3;
            this.openPlaylistMenuItem.Text = "Open Playlist...";
            this.openPlaylistMenuItem.Click += new System.EventHandler(this.openPlaylistMenuItem_Click);
            // 
            // importMenuItem
            // 
            this.importMenuItem.Index = 4;
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
            this.recentlyViewedMenuItem.Index = 5;
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
            this.exitMenuItem.Index = 6;
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
            this.preferencesMenuItem});
            this.menuItem4.Text = "&Tools";
            // 
            // batchDownloaderMenuItem
            // 
            this.batchDownloaderMenuItem.Index = 0;
            this.batchDownloaderMenuItem.Text = "Batch Downloader";
            this.batchDownloaderMenuItem.Click += new System.EventHandler(this.batchDownloaderMenuItem_Click);
            // 
            // preferencesMenuItem
            // 
            this.preferencesMenuItem.Index = 1;
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
            this.Shown += new System.EventHandler(this.Form1_Shown);
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
            this.previewToolStrip.ResumeLayout(false);
            this.previewToolStrip.PerformLayout();
            this.display_search.ResumeLayout(false);
            this.searchSplitContainer.Panel1.ResumeLayout(false);
            this.searchSplitContainer.Panel2.ResumeLayout(false);
            this.searchSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchDisplay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private DataGridViewExtended searchDisplay;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtSearchTitle;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtSearchArtist;
        private System.Windows.Forms.Button onlinesearchbtn;
        private System.Windows.Forms.Panel panel1;
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
        private SplitContainer searchSplitContainer;
        private BasicTablatureTextEditor searchPreviewEditor;
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
        private Label label5;
        private ListBox listSearchServices;
        private DataGridViewTextBoxColumn searchcol_artist;
        private DataGridViewTextBoxColumn searchcol_title;
        private DataGridViewTextBoxColumn searchcol_type;
        private DataGridViewTextBoxColumn col_rating;
        private DataGridViewTextBoxColumn searchcol_service;
        private Label label6;
        public ComboBox cbSearchRating;
        private ToolStripStatusLabel lblStatus;
        private Button resetSearchbtn;
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
        private MenuItem openPlaylistMenuItem;
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
    }
}

