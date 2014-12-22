#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Tabster.Controls;
using Tabster.Controls.Extensions;
using Tabster.Core.Printing;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Library;
using Tabster.Data.Processing;

#endregion

namespace Tabster
{
    internal enum PreviewPanelOrientation
    {
        Hidden,
        Horizontal,
        Vertical
    }
}

namespace Tabster.Forms
{
    internal partial class MainForm
    {
        private const int PREVIEW_DISPLAY_DELAY_DURATION = 100;

        //time (in ms) where a tab is considered having been "viewed" while in preview editor
        private const int PREVIEW_DISPLAY_VIEWED_DURATION = 5000;

        private readonly ToolStripMenuItem newplaylistmenuitem = new ToolStripMenuItem
                                                                     {
                                                                         Text = "New Playlist",
                                                                     };

        private TablatureLibraryItem SelectedLibraryItem;
        private List<ITablatureFileExporter> _fileExporters = new List<ITablatureFileExporter>();
        private List<ITablatureFileImporter> _fileImporters = new List<ITablatureFileImporter>();

        //used to prevent double-triggering of OnSelectedIndexChanged for tablibrary when using navigation menu
        private bool _switchingNavigationOption;

        //time (in ms) where tab is displayed in preview editor after being selected

        private bool IsViewingLibrary()
        {
            return tabControl1.SelectedTab == display_library;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (SelectedLibraryItem != null)
            {
                PopoutTab(SelectedLibraryItem.Document);
            }
        }

        private void tablibrary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (SelectedLibraryItem != null)
                {
                    PopoutTab(SelectedLibraryItem.Document);
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
            if (SelectedLibraryItem != null)
            {
                using (var sfd = new SaveFileDialog
                                     {
                                         Title = "Export Tab - Tabster",
                                         AddExtension = true,
                                         Filter = string.Format("Tabster File (*{0})|*{0}", TablatureDocument.FILE_EXTENSION),
                                         FileName = SelectedLibraryItem.Document.ToFriendlyString()
                                     })
                {
                    sfd.SetTabsterFilter(_fileExporters);

                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        //native TablatureDocument format
                        if (sfd.FilterIndex == 1)
                        {
                            File.Copy(SelectedLibraryItem.FileInfo.FullName, sfd.FileName);
                        }

                        else
                        {
                            var exporter = _fileExporters[sfd.FilterIndex - 2]; //FilterIndex is not 0-based and native Tabster format uses first index
                            exporter.Export(SelectedLibraryItem.Document, sfd.FileName);
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
                SelectedLibraryItem = Program.TablatureFileLibrary.Find(selectedTabLocation);

                var openedExternally = Program.TabbedViewer.IsFileOpen(SelectedLibraryItem.Document);

                deleteTabToolStripMenuItem.Enabled = librarycontextdelete.Enabled = !openedExternally;
                detailsToolStripMenuItem.Enabled = librarycontextdetails.Enabled = !openedExternally;
            }

            else
            {
                SelectedLibraryItem = null;
                deleteTabToolStripMenuItem.Enabled = false;
                detailsToolStripMenuItem.Enabled = false;
            }

            menuItem3.Enabled = SelectedLibraryItem != null;

            if (beginPreviewLoadTimer)
            {
                PreviewDisplayDelay.Stop();
                PreviewDisplayDelay.Start();
            }
        }

        private void printbtn_Click(object sender, EventArgs e)
        {
            var settings = new TablaturePrintDocumentSettings
            {
                Title = SelectedLibraryItem.Document.ToFriendlyString(),
                DisplayTitle = true,
                DisplayPrintTime = true,
                DisplayPageNumbers = true,
            };

            PreviewEditor.Print(settings);
        }

        private void tablibrary_SelectionChanged(object sender, EventArgs e)
        {
            //ignore events triggered by prior sublibrary
            if (_switchingNavigationOption)
            {
                UpdateTabControls(false);
                LoadTabPreview(false);

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
                    var TablatureLibraryItem = Program.TablatureFileLibrary.Add(n.Tab);
                    Program.TablatureFileLibrary.Save();
                    UpdateLibraryItem(TablatureLibraryItem);
                    PopoutTab(n.Tab, isReadOnly: false);
                }
            }
        }

        private void PopoutTab(TablatureDocument tab, bool isReadOnly = true, bool updateRecentFiles = true)
        {
            Program.TabbedViewer.LoadTablature(tab, isReadOnly);

            if (updateRecentFiles)
                recentlyViewedMenuItem.Add(tab.FileInfo, tab.ToFriendlyString());

            var libraryItem = Program.TablatureFileLibrary.GetLibraryItem(tab);

            libraryItem.IncrementViewcount();
            libraryItem.LastViewed = DateTime.UtcNow;

            Program.TablatureFileLibrary.Save();

            UpdateTabControls(false);
            LoadTabPreview();
        }

        private void SearchSimilarTabs(object sender, EventArgs e)
        {
            if (SelectedLibraryItem != null)
            {
                txtSearchArtist.Text = sender == searchByArtistToolStripMenuItem || sender == searchByArtistAndTitleToolStripMenuItem
                                           ? SelectedLibraryItem.Artist
                                           : "";

                txtSearchTitle.Text = sender == searchByTitleToolStripMenuItem || sender == searchByArtistAndTitleToolStripMenuItem
                                          ? RemoveVersionConventionFromTitle(SelectedLibraryItem.Title)
                                          : "";

                searchTypeList.SelectDefault();
                tabControl1.SelectedTab = display_search;
                onlinesearchbtn.PerformClick();
            }
        }

        private void DeleteTab(object sender, EventArgs e)
        {
            if (!IsViewingLibrary())
                return;

            if (SelectedLibraryItem != null)
            {
                var removed = false;

                if (SelectedLibrary() == LibraryType.Playlist)
                {
                    var selectedPlaylist = GetSelectedPlaylist();

                    if (selectedPlaylist != null)
                    {
                        if (MessageBox.Show(string.Format("Are you sure you want to remove this tab from the playlist?{0}{0}{1}", Environment.NewLine, SelectedLibraryItem.Document.ToFriendlyString()), "Remove Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            selectedPlaylist.Remove(SelectedLibraryItem.Document);
                            removed = true;
                            selectedPlaylist.Save();
                        }
                    }
                }

                else
                {
                    if (MessageBox.Show(string.Format("Are you sure you want to delete this tab?{0}{0}{1}", Environment.NewLine, SelectedLibraryItem.Document.ToFriendlyString()), "Delete Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Program.TablatureFileLibrary.Remove(SelectedLibraryItem, true);
                        removed = true;
                    }
                }

                if (removed)
                {
                    RemoveSelectedTablatureLibraryItem();
                }
            }
        }

        private void OpenTabLocation(object sender, EventArgs e)
        {
            if (!IsViewingLibrary())
                return;

            if (SelectedLibraryItem != null)
            {
                Process.Start("explorer.exe ", @"/select, " + SelectedLibraryItem.FileInfo.FullName);
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
            var item = ((ToolStripMenuItem) sender);
            var text = item.Text;

            foreach (ToolStripMenuItem menuItem in toolStripButton3.DropDownItems)
            {
                menuItem.Checked = menuItem.Text == item.Text;
            }

            PreviewEditor.AutoScroll = text == "On";
        }

        private void sidemenu_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            _switchingNavigationOption = true;
        }

        private void sidemenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLibraryFilter.Text))
            {
                txtLibraryFilter.Clear();
            }

            else
            {
                LoadLibrary();
            }
        }

        private void ToggleFavorite(object sender, EventArgs e)
        {
            if (SelectedLibraryItem != null)
            {
                SelectedLibraryItem.Favorited = !SelectedLibraryItem.Favorited;

                Program.TablatureFileLibrary.Save();

                //remove item from favorites display
                if (!SelectedLibraryItem.Favorited && SelectedLibrary() == LibraryType.MyFavorites)
                {
                    RemoveSelectedTablatureLibraryItem();
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

            if (e.Button == MouseButtons.Right && (currentMouseOverRow >= 0 && currentMouseOverRow < tablibrary.Rows.Count) && SelectedLibraryItem != null)
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
                        var associatedPlaylist = Program.TablatureFileLibrary.FindPlaylistByPath(playlistPath);
                        var alreadyExistsInPlaylist = associatedPlaylist.Contains(SelectedLibraryItem.Document);

                        toolItem.Enabled = !alreadyExistsInPlaylist;
                    }
                }
                librarycontextfavorites.Text = SelectedLibraryItem.Favorited ? "Remove from favorites" : "Add to favorites";
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
                foreach (var doc in Program.TablatureFileLibrary)
                {
                    tabCollection.Add(doc.Document);
                }
            }

            return tabCollection.ToArray();
        }

        private bool TablatureLibraryItemVisible(LibraryType selectedLibrary, TablatureDocument tab, string searchValue)
        {
            var libraryMatch =
                selectedLibrary == LibraryType.Playlist ||
                selectedLibrary == LibraryType.AllTabs ||
                (selectedLibrary == LibraryType.MyTabs && tab.SourceType == TablatureSourceType.UserCreated) ||
                (selectedLibrary == LibraryType.MyDownloads && tab.SourceType == TablatureSourceType.Download) ||
                (selectedLibrary == LibraryType.MyImports && tab.SourceType == TablatureSourceType.FileImport) ||
                (selectedLibrary == LibraryType.MyFavorites && Program.TablatureFileLibrary.GetLibraryItem(tab).Favorited) ||
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
                var visible = TablatureLibraryItemVisible(selectedLibrary, tab, searchValue);

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
                var alreadyExists = playlist != null ? playlist.Contains(path) : Program.TablatureFileLibrary.Find(path) != null;

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
                        var TablatureLibraryItem = Program.TablatureFileLibrary.Add(doc);
                        Program.TablatureFileLibrary.Save();
                        UpdateLibraryItem(TablatureLibraryItem);
                    }
                }
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var i = new ImportDialog(_fileImporters))
            {
                if (i.ShowDialog() == DialogResult.OK)
                {
                    Program.TablatureFileLibrary.Add(i.Tab);
                    Program.TablatureFileLibrary.Save();
                    UpdateLibraryItem(Program.TablatureFileLibrary.GetLibraryItem(i.Tab));
                }
            }
        }

        private void viewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedLibraryItem != null)
            {
                PopoutTab(SelectedLibraryItem.Document);
            }
        }

        private void UpdateDetails()
        {
            lblcount.Text = string.Format("Total Tabs: {0}", Program.TablatureFileLibrary.TotalItems);
            lblplaylists.Text = string.Format("Playlists: {0}", Program.TablatureFileLibrary.Playlists.Count);
        }

        public void UpdateLibraryItem(TablatureDocument doc, bool append = true)
        {
            var item = new TablatureLibraryItem(doc);
            UpdateLibraryItem(item, append);
        }

        private void UpdateLibraryItem(TablatureLibraryItem item, bool append = true)
        {
            var objValues = new object[]
                                {
                                    item.Title,
                                    item.Artist,
                                    item.Type.ToFriendlyString(),
                                    item.Document.Created,
                                    item.FileInfo.LastWriteTime,
                                    item.Views,
                                    item.FileInfo.FullName
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

        private void RemoveSelectedTablatureLibraryItem()
        {
            SelectedLibraryItem = null;
            RemoveTablatureLibraryItem(tablibrary.SelectedRows[0].Index);
        }

        private void RemoveTablatureLibraryItem(int index)
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

            if (SelectedLibraryItem != null)
            {
                using (var details = new TabDetailsDialog(SelectedLibraryItem.Document) {Icon = Icon})
                {
                    if (details.ShowDialog() == DialogResult.OK)
                    {
                        UpdateLibraryItem(SelectedLibraryItem, false);
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

            if (SelectedLibraryItem != null)
            {
                lblpreviewtitle.Text = SelectedLibraryItem.Document.ToFriendlyString();

                var openedExternally = Program.TabbedViewer.IsFileOpen(SelectedLibraryItem.Document);

                PreviewEditor.Visible = !openedExternally;

                if (openedExternally)
                {
                    lblLibraryPreview.Text = "Tab is open in external viewer.";
                    lblLibraryPreview.Visible = true;
                }

                else
                {
                    lblLibraryPreview.Visible = false;

                    PreviewEditor.LoadTablature(SelectedLibraryItem.Document);

                    if (startViewCountTimer)
                    {
                        PreviewDisplayTimer.Start();
                    }
                }

                librarySplitContainer.Panel2.Enabled = !openedExternally;
                previewToolStrip.Enabled = true;
            }

            else
            {
                previewToolStrip.Enabled = false;
                lblpreviewtitle.Text = "";
                lblLibraryPreview.Text = "";
            }
        }

        #region Tab Viewer Manager Events

        private void TabHandler_OnTabClosed(object sender, TablatureDocument tabDocument)
        {
            if (SelectedLibraryItem != null)
            {
                LoadTabPreview();
                UpdateTabControls(false);
            }
        }

        #endregion

        #region Searching

        private void txtLibraryFilter_TextChanged(object sender, EventArgs e)
        {
            LoadLibrary(txtLibraryFilter.Text);
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
                    Program.TablatureFileLibrary.Remove(playlist, true);
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
                        Program.TablatureFileLibrary.Add(playlist);

                        AddPlaylistNode(playlist);

                        //add tab to new playlist
                        if (sender == newplaylistmenuitem && SelectedLibraryItem != null)
                        {
                            playlist.Add(SelectedLibraryItem.Document);
                            playlist.Save();
                        }
                    }
                }
            }
        }

        private TablaturePlaylistDocument GetSelectedPlaylist()
        {
            var playlistPath = sidemenu.SelectedNode.Tag.ToString();
            return Program.TablatureFileLibrary.FindPlaylistByPath(playlistPath);
        }

        private void AddPlaylistNode(TablaturePlaylistDocument playlist, bool select = false)
        {
            TreeNode node = null;

            foreach (TreeNode n in sidemenu.Nodes["node_playlists"].Nodes)
            {
                if (n.Tag.ToString().Equals(playlist.FileInfo.FullName, StringComparison.OrdinalIgnoreCase))
                {
                    node = n;
                    break;
                }
            }

            if (node == null)
            {
                node = new TreeNode(playlist.Name) {NodeFont = sidemenu.FirstNode.FirstNode.NodeFont, Tag = playlist.FileInfo.FullName};
                sidemenu.Nodes["node_playlists"].Nodes.Add(node);
            }

            if (select)
                sidemenu.SelectedNode = node;
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

            foreach (var playlist in Program.TablatureFileLibrary.Playlists)
            {
                AddPlaylistNode(playlist);

                var menuItem = new ToolStripMenuItem(playlist.Name) {Tag = playlist.FileInfo.FullName};

                menuItem.Click += (s, e) =>
                                      {
                                          var path = ((ToolStripMenuItem) s).Tag.ToString();

                                          var pf = Program.TablatureFileLibrary.FindPlaylistByPath(path);

                                          if (pf != null)
                                          {
                                              pf.Add(SelectedLibraryItem.Document);
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
            if (SelectedLibraryItem != null)
            {
                SelectedLibraryItem.IncrementViewcount();
                SelectedLibraryItem.LastViewed = DateTime.UtcNow;

                UpdateLibraryItem(SelectedLibraryItem, false);
            }

            PreviewDisplayTimer.Stop();
        }

        #endregion

        #region Nested type: LibraryType

        private enum LibraryType
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
    }
}