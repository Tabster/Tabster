#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Tabster.Controls;
using Tabster.Core.Data;
using Tabster.Core.Types;
using Tabster.Properties;
using Tabster.Utilities;

#endregion

namespace Tabster
{
    public enum PreviewPanelOrientation
    {
        Hidden,
        Horizontal,
        Vertical
    }
}

namespace Tabster.Forms
{
    partial class MainForm
    {
        #region LibraryType enum

        public enum LibraryType
        {
            AllTabs,
            MyDownloads,
            MyTabs,
            MyImports,
            MyFavorites,
            TabType,
            Playlist
        }

        #endregion

        private readonly ToolStripMenuItem newplaylistmenuitem = new ToolStripMenuItem
                                                                     {
                                                                         Text = "New Playlist",
                                                                     };

        private TablatureDocument SelectedTab;

        //used to prevent double-triggering of OnSelectedIndexChanged for tablibrary when using navigation menu
        private bool _switchingNavigationOption;
        private TablatureEditor _tabPreviewEditor;

        //time (in ms) where tab is displayed in preview editor after being selected
        private const int PREVIEW_DISPLAY_DELAY_DURATION = 100;

        //time (in ms) where a tab is considered having been "viewed" while in preview editor
        private const int PREVIEW_DISPLAY_VIEWED_DURATION = 5000;

        private bool IsViewingLibrary()
        {
            return tabControl1.SelectedTab == display_library;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (SelectedTab != null)
            {
                PopoutTab(SelectedTab);
            }
        }

        private void tablibrary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (SelectedTab != null)
                {
                    PopoutTab(SelectedTab);
                }
            }
        }

        private void tablibrary_DragDrop(object sender, DragEventArgs e)
        {
            var data = (string[]) e.Data.GetData(DataFormats.FileDrop, false);

            var playlist = GetSelectedPlaylist();

            if (data != null)
            {
                foreach (var str in data)
                {
                    ImportTab(str, playlist);
                }
            }
        }

        private void tablibrary_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void ExportTab(object sender, EventArgs e)
        {
            if (SelectedTab != null)
            {
                using (var sfd = new SaveFileDialog
                                     {
                                         Title = "Export Tab - Tabster",
                                         AddExtension = true,
                                         Filter = string.Format("Tabster File (*{0})|*{0}|Text File (*.txt)|*.txt|HTML File (*.html)|*.html", TablatureDocument.FILE_EXTENSION),
                                         FileName = SelectedTab.ToFriendlyString()
                                     })
                {
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        switch (sfd.FilterIndex)
                        {
                            case 1:
                                File.Copy(SelectedTab.FileInfo.FullName, sfd.FileName);
                                break;
                            case 2:
                                File.WriteAllText(sfd.FileName, SelectedTab.Contents);
                                break;
                            case 3:
                                File.WriteAllText(sfd.FileName, Resources.HTML_Export_Template.Replace("{TAB_CONTENTS}", SelectedTab.Contents));
                                break;
                        }
                    }
                }
            }
        }

        private void UpdateTabControls(bool beginPreviewLoadTimer)
        {
            if (tablibrary.SelectedRows.Count > 0)
            {
                var selectedTabLocation = tablibrary.SelectedRows[0].Cells[tablibrary.Columns.Count - 1].Value.ToString();
                SelectedTab = Program.libraryManager.FindByPath(selectedTabLocation);

                var openedExternally = Program.TabHandler.IsOpenedExternally(SelectedTab);

                deleteTabToolStripMenuItem.Enabled = librarycontextdelete.Enabled = !openedExternally;
                detailsToolStripMenuItem.Enabled = librarycontextdetails.Enabled = !openedExternally;
            }

            else
            {
                SelectedTab = null;
                deleteTabToolStripMenuItem.Enabled = false;
                detailsToolStripMenuItem.Enabled = false;
            }

            menuItem3.Enabled = SelectedTab != null;

            if (beginPreviewLoadTimer)
            {
                PreviewDisplayDelay.Stop();
                PreviewDisplayDelay.Start();
            }
        }

        private void printbtn_Click(object sender, EventArgs e)
        {
            if (_tabPreviewEditor != null)
                _tabPreviewEditor.Print();
        }

        private void tablibrary_SelectionChanged(object sender, EventArgs e)
        {
            //ignore events triggered by prior sublibrary
            if (_switchingNavigationOption)
            {
                //first load, instantly load tab preview
                if (!_initialLibraryLoaded)
                {
                    UpdateTabControls(false);
                    LoadTabPreview(false);
                }

                _switchingNavigationOption = false;
            }

                //normal event
            else
            {
                //load tab preview with delay
                UpdateTabControls(true);
            }
        }

        private void NewTab(object sender, EventArgs e)
        {
            using (var n = new NewTabDialog())
            {
                if (n.ShowDialog() == DialogResult.OK)
                {
                    Program.libraryManager.Add(n.Tab, true);
                    UpdateLibraryItem(n.Tab);
                    PopoutTab(n.Tab);
                }
            }
        }

        private void PopoutTab(TablatureDocument tab, bool updateRecentFiles = true)
        {
            Program.TabHandler.LoadExternally(tab, true);

            if (updateRecentFiles)
                recentlyViewedMenuItem.Add(tab.FileInfo, tab.ToFriendlyString());

            UpdateTabControls(false);
            LoadTabPreview();
        }

        private void SearchSimilarTabs(object sender, EventArgs e)
        {
            if (SelectedTab != null)
            {
                txtSearchArtist.Text = sender == searchByArtistToolStripMenuItem || sender == searchByArtistAndTitleToolStripMenuItem
                                           ? SelectedTab.Artist
                                           : "";

                txtSearchTitle.Text = sender == searchByTitleToolStripMenuItem || sender == searchByArtistAndTitleToolStripMenuItem
                                          ? RemoveVersionConventionFromTitle(SelectedTab.Title)
                                          : "";

                txtSearchType.SelectedIndex = 0;
                tabControl1.SelectedTab = display_search;
                onlinesearchbtn.PerformClick();
            }
        }

        private void DeleteTab(object sender, EventArgs e)
        {
            if (!IsViewingLibrary())
                return;

            if (SelectedTab != null)
            {
                var removed = false;

                if (SelectedLibrary() == LibraryType.Playlist)
                {
                    var selectedPlaylist = GetSelectedPlaylist();

                    if (selectedPlaylist != null)
                    {
                        if (MessageBox.Show(string.Format("Are you sure you want to remove this tab from the playlist?{0}{0}{1}", Environment.NewLine, SelectedTab.ToFriendlyString()), "Remove Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            selectedPlaylist.Remove(SelectedTab);
                            removed = true;
                            selectedPlaylist.Save();
                        }
                    }
                }

                else
                {
                    if (MessageBox.Show(string.Format("Are you sure you want to delete this tab?{0}{0}{1}", Environment.NewLine, SelectedTab.ToFriendlyString()), "Delete Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Program.libraryManager.Remove(SelectedTab, true);
                        removed = true;
                    }
                }

                if (removed)
                {
                    RemoveSelectedLibraryItem();
                }
            }
        }

        private void OpenTabLocation(object sender, EventArgs e)
        {
            if (!IsViewingLibrary())
                return;

            if (SelectedTab != null)
            {
                Process.Start("explorer.exe ", @"/select, " + SelectedTab.FileInfo.FullName);
            }
        }

        private void BrowseTab(object sender, EventArgs e)
        {
            if (!IsViewingLibrary())
                return;

            using (var ofd = new OpenFileDialog
                                 {
                                     Title = "Open File - Tabster",
                                     AddExtension = true,
                                     Multiselect = false,
                                     Filter = string.Format("Tabster Files (*{0})|*{0}", TablatureDocument.FILE_EXTENSION)
                                 })
            {
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    var tab = _tablatureProcessor.Load(ofd.FileName);

                    if (tab != null)
                    {
                        PopoutTab(tab);
                    }
                }
            }
        }

        private void autoScrollChange(object sender, EventArgs e)
        {
            if (_tabPreviewEditor != null)
            {
                var item = ((ToolStripMenuItem) sender);
                var text = item.Text;

                foreach (ToolStripMenuItem menuItem in toolStripButton3.DropDownItems)
                {
                    menuItem.Checked = menuItem.Text == item.Text;
                }

                _tabPreviewEditor.AutoScroll = text == "On";
            }
        }

        private void sidemenu_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            _switchingNavigationOption = true;
        }

        private void sidemenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            filtertext.Reset(true);
            LoadLibrary();
        }

        private void ToggleFavorite(object sender, EventArgs e)
        {
            if (SelectedTab != null)
            {
                var attributes = Program.libraryManager.GetLibraryAttributes(SelectedTab);
                attributes.Favorited = !attributes.Favorited;

                Program.libraryManager.Save();

                //remove item from favorites display
                if (!attributes.Favorited && SelectedLibrary() == LibraryType.MyFavorites)
                {
                    RemoveSelectedLibraryItem();
                }
            }
        }

        private void sidemenu_MouseClick(object sender, MouseEventArgs e)
        {
            var selectedNode = sidemenu.HitTest(e.X, e.Y).Node;

            if (selectedNode != null)
            {
                if (e.Button == MouseButtons.Right && SelectedLibrary() == LibraryType.Playlist)
                {
                    sidemenu.SelectedNode = selectedNode;
                    deleteplaylistcontextmenuitem.Visible = true;
                    PlaylistMenu.Show(sidemenu.PointToScreen(e.Location));
                }
            }
        }

        private void tablibrary_MouseClick(object sender, MouseEventArgs e)
        {
            var currentMouseOverRow = tablibrary.HitTest(e.X, e.Y).RowIndex;

            if (e.Button == MouseButtons.Right && (currentMouseOverRow >= 0 && currentMouseOverRow < tablibrary.Rows.Count) && SelectedTab != null)
            {
                tablibrary.Rows[currentMouseOverRow].Selected = true;

                LibraryMenu.Show(tablibrary.PointToScreen(e.Location));

                //check if playlists already contain 
                foreach (var item in librarycontextaddtoplaylist.DropDownItems)
                {
                    var toolItem = item as ToolStripMenuItem;

                    if (toolItem != null && toolItem.Tag != null)
                    {
                        var playlistPath = toolItem.Tag.ToString();
                        var associatedPlaylist = Program.libraryManager.FindPlaylistByPath(playlistPath);
                        var alreadyExistsInPlaylist = associatedPlaylist.Contains(SelectedTab);

                        toolItem.Enabled = !alreadyExistsInPlaylist;
                    }
                }

                var attributes = Program.libraryManager.GetLibraryAttributes(SelectedTab);
                librarycontextfavorites.Text = attributes.Favorited ? "Remove from favorites" : "Add to favorites";
            }
        }

        private LibraryType SelectedLibrary()
        {
            var selectedNode = sidemenu.SelectedNode;

            if (selectedNode == null)
                return LibraryType.AllTabs;

            if (selectedNode.Parent != null && selectedNode.Parent.Name == "node_playlists")
                return LibraryType.Playlist;

            switch (sidemenu.SelectedNode.Name)
            {
                case "node_alltabs":
                    return LibraryType.AllTabs;
                case "node_mytabs":
                    return LibraryType.MyTabs;
                case "node_mydownloads":
                    return LibraryType.MyDownloads;
                case "node_myimports":
                    return LibraryType.MyImports;
                case "node_myfavorites":
                    return LibraryType.MyFavorites;
            }

            return LibraryType.TabType;
        }

        private IEnumerable<TablatureDocument> GetLibraryCollection(LibraryType libraryType)
        {
            var tabCollection = new List<TablatureDocument>();

            if (libraryType == LibraryType.Playlist)
            {
                var selectedPlaylist = GetSelectedPlaylist();
                tabCollection = new List<TablatureDocument>(selectedPlaylist);
            }

            else
            {
                foreach (var doc in Program.libraryManager)
                {
                    tabCollection.Add(doc);
                }
            }

            return tabCollection.ToArray();
        }

        private bool LibraryItemVisible(LibraryType selectedLibrary, TablatureDocument tab, string searchValue)
        {
            var libraryMatch =
                selectedLibrary == LibraryType.Playlist ||
                selectedLibrary == LibraryType.AllTabs ||
                (selectedLibrary == LibraryType.MyTabs && tab.SourceType == TablatureSourceType.UserCreated) ||
                (selectedLibrary == LibraryType.MyDownloads && tab.SourceType == TablatureSourceType.Download) ||
                (selectedLibrary == LibraryType.MyImports && tab.SourceType == TablatureSourceType.FileImport) ||
                (selectedLibrary == LibraryType.MyFavorites && Program.libraryManager.GetLibraryAttributes(tab).Favorited) ||
                (selectedLibrary == LibraryType.TabType && sidemenu.SelectedNode.Tag.ToString() == tab.Type.ToString());

            if (libraryMatch)
            {
                return searchValue == null || (tab.Artist.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               tab.Title.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               tab.FileInfo.FullName.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               tab.Contents.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               tab.Comment.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return false;
        }

        public void LoadLibrary(string searchValue = null)
        {
            var selectedLibrary = SelectedLibrary();

            var tabCollection = GetLibraryCollection(selectedLibrary);

            tablibrary.SuspendLayout();

            tablibrary.Rows.Clear();

            foreach (var tab in tabCollection)
            {
                var visible = LibraryItemVisible(selectedLibrary, tab, searchValue);

                if (visible)
                    UpdateLibraryItem(tab);
            }

            tablibrary.ResumeLayout();

            _initialLibraryLoaded = true;
        }

        private void ImportTab(string path, TablaturePlaylistDocument playlist = null)
        {
            var doc = _tablatureProcessor.Load(path);

            if (doc != null)
            {
                var alreadyExists = playlist != null ? playlist.ContainsPath(path) : Program.libraryManager.FindByPath(path) != null;

                if (!alreadyExists)
                {
                    if (playlist != null)
                    {
                        playlist.Add(doc);
                        UpdateLibraryItem(doc);
                        playlist.Save();
                    }

                    else
                    {
                        Program.libraryManager.Add(doc, true);
                        UpdateLibraryItem(doc);
                    }
                }
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var i = new ImportDialog())
            {
                if (i.ShowDialog() == DialogResult.OK)
                {
                    Program.libraryManager.Add(i.Tab, true);
                    UpdateLibraryItem(i.Tab);
                }
            }
        }

        private void viewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedTab != null)
            {
                PopoutTab(SelectedTab);
            }
        }

        private void UpdateDetails()
        {
            lblcount.Text = string.Format("Total Tabs: {0}", Program.libraryManager.Count);
            lblplaylists.Text = string.Format("Playlists: {0}", Program.libraryManager.Playlists.Count);
        }

        public void UpdateLibraryItem(TablatureDocument tab, bool append = true)
        {
            var attributes = Program.libraryManager.GetLibraryAttributes(tab);

            if (attributes != null)
            {
                var objValues = new object[]
                                    {
                                        tab.Title,
                                        tab.Artist,
                                        tab.Type.ToFriendlyString(),
                                        tab.Created,
                                        tab.FileInfo.LastWriteTime,
                                        attributes.Views,
                                        tab.FileInfo.FullName
                                    };

                if (append)
                {
                    tablibrary.Rows.Add(objValues);

                    if (tablibrary.SortedColumn != null)
                        tablibrary.Sort(tablibrary.SortedColumn, tablibrary.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
                }

                else if (tablibrary.SelectedRows.Count > 0)
                {
                    var selectedIndex = tablibrary.SelectedRows[0].Index;
                    tablibrary.Rows[selectedIndex].SetValues(objValues);
                }
            }
        }

        private void RemoveSelectedLibraryItem()
        {
            SelectedTab = null;
            RemoveLibraryItem(tablibrary.SelectedRows[0].Index);
        }

        private void RemoveLibraryItem(int index)
        {
            tablibrary.Rows.RemoveAt(index);

            if (tablibrary.Rows.Count > 0)
            {
                var newIndex = index > 0 ? index - 1 : 0;
                tablibrary.Rows[newIndex].Selected = true;
            }

            UpdateDetails();
        }

        private void TabDetails(object sender, EventArgs e)
        {
            if (!IsViewingLibrary())
                return;

            if (SelectedTab != null)
            {
                using (var details = new TabDetailsDialog(SelectedTab) {Icon = Icon})
                {
                    if (details.ShowDialog() == DialogResult.OK)
                    {
                        UpdateLibraryItem(SelectedTab, false);
                        LoadTabPreview();
                    }
                }
            }
        }

        private void PlaylistDetails(object sender, EventArgs e)
        {
            if (SelectedLibrary() == LibraryType.Playlist)
            {
                var playlist = GetSelectedPlaylist();
                var selectedNode = sidemenu.SelectedNode;

                using (var pdd = new PlaylistDetailsDialog(playlist))
                {
                    if (pdd.ShowDialog() == DialogResult.OK)
                    {
                        if (pdd.PlaylistRenamed)
                        {
                            selectedNode.Text = playlist.Name;
                        }
                    }
                }
            }
        }

        private void PreviewDisplayDelay_Tick(object sender, EventArgs e)
        {
            PreviewDisplayDelay.Stop();
            LoadTabPreview();
        }

        private void LoadTabPreview(bool startViewCountTimer = true)
        {
            PreviewDisplayTimer.Stop();

            if (SelectedTab != null)
            {
                lblpreviewtitle.Text = SelectedTab.ToFriendlyString();

                bool openedExternally, isNew;
                var editor = Program.TabHandler.TryGetEditor(SelectedTab, out openedExternally, out isNew);

                if (openedExternally)
                {
                    lblopenedexternally.BringToFront();
                    lblopenedexternally.Visible = true;
                    editor.BringToFront();
                }

                else
                {
                    editor.LoadTab(SelectedTab);

                    lblopenedexternally.Visible = false;

                    librarySplitContainer.Panel2.Controls.Add(editor);

                    editor.BringToFront();
                    editor.Visible = true;
                    editor.ReadOnly = true;

                    //cancel autoscroll of existing editor
                    if (_tabPreviewEditor != null)
                    {
                        _tabPreviewEditor.AutoScroll = false;
                        _tabPreviewEditor.ScrollToLine(0);
                        offToolStripMenuItem.PerformClick();
                    }

                    if (startViewCountTimer)
                    {
                        PreviewDisplayTimer.Start();
                    }

                    _tabPreviewEditor = editor;
                }

                librarySplitContainer.Panel2.Enabled = !openedExternally;
                previewToolStrip.Enabled = true;
            }

            else
            {
                previewToolStrip.Enabled = false;
                lblpreviewtitle.Text = "";

                if (_tabPreviewEditor != null)
                {
                    _tabPreviewEditor.SetText(string.Empty);
                }
            }
        }

        #region Tab Viewer Manager Events

        private void TabHandler_OnTabClosed(object sender, TablatureDocument tabDocument)
        {
            if (SelectedTab != null)
            {
                LoadTabPreview();
            }
        }

        private void TabHandler_OnTabOpened(object sender, TablatureDocument tabDocument)
        {
            if (SelectedTab != null)
            {
                LoadTabPreview();
            }
        }

        #endregion

        #region Searching

        private void filtertext_OnNewSearch(object sender, string value)
        {
            if (filtertext.IsFilterSet || filtertext.FilterReset)
            {
                LoadLibrary(value);
            }
        }

        #endregion

        #region Playlists

        private void DeletePlaylist(object sender, EventArgs e)
        {
            if (SelectedLibrary() == LibraryType.Playlist)
            {
                var playlist = GetSelectedPlaylist();

                if (playlist != null && MessageBox.Show("Are you sure you want to delete this playlist?", "Delete Playlist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Program.libraryManager.Remove(playlist, true);
                    RemovePlaylistNode(playlist);
                    UpdateDetails();
                }
            }
        }

        private void NewPlaylist(object sender, EventArgs e)
        {
            using (var p = new NewPlaylistDialog())
            {
                if (p.ShowDialog() == DialogResult.OK)
                {
                    var name = p.PlaylistName;

                    if (!string.IsNullOrEmpty(name))
                    {
                        var playlist = new TablaturePlaylistDocument(name);
                        Program.libraryManager.Add(playlist);

                        AddPlaylistNode(playlist);

                        //add tab to new playlist
                        if (sender == newplaylistmenuitem && SelectedTab != null)
                        {
                            playlist.Add(SelectedTab);
                            playlist.Save();
                        }
                    }
                }
            }
        }

        private TablaturePlaylistDocument GetSelectedPlaylist()
        {
            var playlistPath = sidemenu.SelectedNode.Tag.ToString();
            return Program.libraryManager.FindPlaylistByPath(playlistPath);
        }

        private void AddPlaylistNode(TablaturePlaylistDocument playlist)
        {
            sidemenu.Nodes["node_playlists"].Nodes.Add(new TreeNode(playlist.Name) {NodeFont = sidemenu.FirstNode.FirstNode.NodeFont, Tag = playlist.FileInfo.FullName});
        }

        private void RemovePlaylistNode(TablaturePlaylistDocument playlist)
        {
            foreach (TreeNode node in sidemenu.Nodes["node_playlists"].Nodes)
            {
                if (node.Tag.ToString().Equals(playlist.FileInfo.FullName))
                {
                    sidemenu.Nodes.Remove(node);
                    break;
                }
            }
        }

        private void PopulatePlaylists()
        {
            var playlistNode = sidemenu.Nodes["node_playlists"];

            if (playlistNode.Nodes.Count > 0)
                playlistNode.Nodes.Clear();

            if (librarycontextaddtoplaylist.DropDownItems.Count > 0)
                librarycontextaddtoplaylist.DropDownItems.Clear();

            librarycontextaddtoplaylist.DropDownItems.Clear();

            foreach (var playlist in Program.libraryManager.Playlists)
            {
                AddPlaylistNode(playlist);

                var menuItem = new ToolStripMenuItem(playlist.Name) {Tag = playlist.FileInfo.FullName};

                menuItem.Click += (s, e) =>
                                      {
                                          var path = ((ToolStripMenuItem) s).Tag.ToString();

                                          var pf = Program.libraryManager.FindPlaylistByPath(path);

                                          if (pf != null)
                                          {
                                              pf.Add(SelectedTab);
                                              pf.Save();
                                          }
                                      };

                librarycontextaddtoplaylist.DropDownItems.Add(menuItem);
            }

            sidemenu.ExpandAll();

            if (librarycontextaddtoplaylist.DropDownItems.Count > 0)
            {
                librarycontextaddtoplaylist.DropDownItems.Add(new ToolStripSeparator());
            }

            newplaylistmenuitem.Click -= NewPlaylist;
            newplaylistmenuitem.Click += NewPlaylist;
            librarycontextaddtoplaylist.DropDownItems.Add(newplaylistmenuitem);

            UpdateDetails();
        }

        #endregion

        #region Preview Display

        private void PreviewDisplayTimer_Tick(object sender, EventArgs e)
        {
            if (SelectedTab != null)
            {
                Program.libraryManager.IncrementViewCount(SelectedTab);
                Program.libraryManager.SetLastViewed(SelectedTab, DateTime.Now);
                UpdateLibraryItem(SelectedTab, false);
            }

            PreviewDisplayTimer.Stop();
        }

        #endregion
    }
}