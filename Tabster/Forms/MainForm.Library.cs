#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Tabster.Core.Printing;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Library;
using Tabster.Data.Processing;
using Tabster.Properties;
using Tabster.WinForms.Extensions;

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
        private readonly List<TablatureLibraryItem<TablatureFile>> _libraryCache = new List<TablatureLibraryItem<TablatureFile>>();
        private bool _changingLibraryView;
        private List<ITablatureFileExporter> _fileExporters = new List<ITablatureFileExporter>();
        private List<ITablatureFileImporter> _fileImporters = new List<ITablatureFileImporter>();

        //used to prevent double-triggering of OnSelectedIndexChanged for tablibrary when using navigation menu

        /// <summary>
        ///     Returns whether the library tab is currently focused.
        /// </summary>
        private bool IsViewingLibrary()
        {
            return tabControl1.SelectedTab == display_library;
        }

        private TablatureLibraryItem<TablatureFile> GetSelectedLibraryItem()
        {
            return listViewLibrary.SelectedObject != null ? (TablatureLibraryItem<TablatureFile>) listViewLibrary.SelectedObject : null;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var item = GetSelectedLibraryItem();

            if (item != null)
            {
                PopoutTab(item.File, item.FileInfo);
            }
        }

        private void ExportTab(object sender, EventArgs e)
        {
            if (GetSelectedLibraryItem() != null)
            {
                using (var sfd = new SaveFileDialog
                {
                    Title = "Export Tab - Tabster",
                    AddExtension = true,
                    Filter = string.Format("Tabster File (*{0})|*{0}", Constants.TablatureFileExtension),
                    FileName = GetSelectedLibraryItem().File.ToFriendlyString()
                })
                {
                    sfd.SetTabsterFilter(_fileExporters, alphabeticalOrder: true);

                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        //native file format
                        if (sfd.FilterIndex == 1)
                        {
                            File.Copy(GetSelectedLibraryItem().FileInfo.FullName, sfd.FileName);
                        }

                        else
                        {
                            var exporter = _fileExporters[sfd.FilterIndex - 2]; //FilterIndex is not 0-based and native Tabster format uses first index
                            exporter.Export(GetSelectedLibraryItem().File, sfd.FileName);
                        }
                    }
                }
            }
        }

        private void UpdateTabControls(bool beginPreviewLoadTimer)
        {
            if (listViewLibrary.SelectedItems.Count > 0)
            {
                var pathCol = listViewLibrary.SelectedItem.SubItems[olvColLocation.Index];

                if (pathCol != null)
                {
                    if (pathCol.Text != null)
                    {
                        var openedExternally = Program.TabbedViewer.IsFileOpen(GetSelectedLibraryItem().FileInfo);

                        deleteTabToolStripMenuItem.Enabled = librarycontextdelete.Enabled = !openedExternally;
                        detailsToolStripMenuItem.Enabled = librarycontextdetails.Enabled = !openedExternally;
                    }
                }
            }

            else
            {
                deleteTabToolStripMenuItem.Enabled = false;
                detailsToolStripMenuItem.Enabled = false;
            }

            menuItem3.Enabled = GetSelectedLibraryItem() != null;

            if (beginPreviewLoadTimer)
            {
                PreviewDisplayDelay.Stop();
                PreviewDisplayDelay.Start();
            }
        }

        private TablaturePrintDocumentSettings GetPrintSettings()
        {
            return new TablaturePrintDocumentSettings
            {
                Title = GetSelectedLibraryItem().File.ToFriendlyString(),
                PrintColor = Settings.Default.PrintColor,
                DisplayTitle = true,
                DisplayPrintTime = Settings.Default.PrintTimestamp,
                DisplayPageNumbers = Settings.Default.PrintPageNumbers
            };
        }

        private void printbtn_Click(object sender, EventArgs e)
        {
            PreviewEditor.Print(GetPrintSettings());
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewEditor.PrintPreview(GetPrintSettings());
        }

        private void printSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPreferences("Printing");
        }

        private void NewTab(object sender, EventArgs e)
        {
            using (var n = new NewTabDialog())
            {
                if (n.ShowDialog() == DialogResult.OK)
                {
                    var item = _tablatureLibrary.Add(n.Tab);
                    PopoutTab(item.File, item.FileInfo);
                }
            }
        }

        private void PopoutTab(TablatureFile file, FileInfo fileInfo, bool updateRecentFiles = true)
        {
            Program.TabbedViewer.LoadTablature(file, fileInfo);

            if (updateRecentFiles)
                recentlyViewedMenuItem.Add(fileInfo, file.ToFriendlyString());

            var libraryItem = _tablatureLibrary.FindTablatureItemByFile(file);
            if (libraryItem != null)
            {
                libraryItem.Views += 1;
                libraryItem.LastViewed = DateTime.UtcNow;
            }

            LoadTabPreview();
        }

        private void SearchSimilarTabs(object sender, EventArgs e)
        {
            if (GetSelectedLibraryItem() != null)
            {
                txtSearchArtist.Text = sender == searchByArtistToolStripMenuItem || sender == searchByArtistAndTitleToolStripMenuItem
                    ? GetSelectedLibraryItem().File.Artist
                    : "";

                txtSearchTitle.Text = sender == searchByTitleToolStripMenuItem || sender == searchByArtistAndTitleToolStripMenuItem
                    ? RemoveVersionConventionFromTitle(GetSelectedLibraryItem().File.Title)
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

            var selectedItem = GetSelectedLibraryItem();

            if (selectedItem != null)
            {
                var removed = false;

                if (SelectedLibrary() == LibraryType.Playlist)
                {
                    var selectedPlaylist = GetSelectedPlaylist();

                    if (selectedPlaylist != null)
                    {
                        if (MessageBox.Show(string.Format("Are you sure you want to remove this tab from the tablaturePlaylist?{0}{0}{1}",
                            Environment.NewLine, selectedItem.File.ToFriendlyString()), "Remove Tab",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            selectedPlaylist.File.Remove(selectedItem.File);
                            removed = true;
                            selectedPlaylist.File.Save(selectedPlaylist.FileInfo.FullName);
                        }
                    }
                }

                else
                {
                    if (MessageBox.Show(string.Format("Are you sure you want to delete this tab?{0}{0}{1}",
                        Environment.NewLine, selectedItem.File.ToFriendlyString()), "Delete Tab",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _tablatureLibrary.RemoveTablatureItem(selectedItem);
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

            if (GetSelectedLibraryItem() != null)
            {
                Process.Start("explorer.exe ", @"/select, " + GetSelectedLibraryItem().FileInfo.FullName);
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
                Filter = string.Format("Tabster Files (*{0})|*{0}", Constants.TablatureFileExtension)
            })
            {
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    var tab = _tablatureLibrary.TablatureFileProcessor.Load(ofd.FileName);

                    if (tab != null)
                    {
                        var item = _tablatureLibrary.Add(tab);
                        PopoutTab(item.File, item.FileInfo);
                    }
                }
            }
        }

        private void sidemenu_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            _changingLibraryView = true;
        }

        private void sidemenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _changingLibraryView = false;

            var viewsColumnVisible = olvColViews.IsVisible;

            var shouldViewsColumnBeVisible = SelectedLibrary() != LibraryType.Playlist;

            if (viewsColumnVisible != shouldViewsColumnBeVisible)
            {
                //hide visibility toggle from user
                olvColViews.Hideable = shouldViewsColumnBeVisible;
                olvColViews.IsVisible = shouldViewsColumnBeVisible;
                listViewLibrary.RebuildColumns();
            }

            if (!string.IsNullOrEmpty(txtLibraryFilter.Text))
            {
                txtLibraryFilter.Clear();
            }

            else
            {
                BuildLibraryCache(false);
            }
        }

        private void ToggleFavorite(object sender, EventArgs e)
        {
            if (GetSelectedLibraryItem() != null)
            {
                GetSelectedLibraryItem().Favorited = !GetSelectedLibraryItem().Favorited;

                //remove item from favorites display
                if (!GetSelectedLibraryItem().Favorited && SelectedLibrary() == LibraryType.MyFavorites)
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

        private void listViewLibrary_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            var selectedItem = GetSelectedLibraryItem();

            if (selectedItem == null)
                return;

            //check if playlists already contain 
            foreach (var item in librarycontextaddtoplaylist.DropDownItems)
            {
                var toolItem = item as ToolStripMenuItem;

                if (toolItem != null && toolItem.Tag != null)
                {
                    var playlistPath = toolItem.Tag.ToString();
                    var associatedPlaylist = _tablatureLibrary.FindPlaylistItemByPath(playlistPath);
                    var alreadyExistsInPlaylist = associatedPlaylist.File.Contains(selectedItem.FileInfo.FullName);

                    toolItem.Enabled = !alreadyExistsInPlaylist;
                }
            }

            librarycontextfavorites.Text = GetSelectedLibraryItem().Favorited ? "Remove from favorites" : "Add to favorites";

            e.MenuStrip = LibraryMenu;
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

        private bool TablatureLibraryItemVisible(LibraryType selectedLibrary, TablatureLibraryItem<TablatureFile> item)
        {
            var libraryMatch =
                selectedLibrary == LibraryType.Playlist ||
                selectedLibrary == LibraryType.AllTabs ||
                (selectedLibrary == LibraryType.MyTabs && item.File.SourceType == TablatureSourceType.UserCreated) ||
                (selectedLibrary == LibraryType.MyDownloads && item.File.SourceType == TablatureSourceType.Download) ||
                (selectedLibrary == LibraryType.MyImports && item.File.SourceType == TablatureSourceType.FileImport) ||
                (selectedLibrary == LibraryType.MyFavorites && item.Favorited) ||
                (selectedLibrary == LibraryType.TabType && sidemenu.SelectedNode.Tag.ToString() == item.File.Type.ToString());

            var searchValue = txtLibraryFilter.Text;

            if (libraryMatch)
            {
                return searchValue == null || (item.File.Artist.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               item.File.Title.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               item.FileInfo.FullName.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               (item.File.Comment != null && item.File.Comment.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                               item.File.Contents.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return false;
        }

        private void BuildLibraryCache(bool persistSelectedItem = true)
        {
            var selectedLibrary = SelectedLibrary();

            var items = new List<TablatureLibraryItem<TablatureFile>>();

            if (selectedLibrary == LibraryType.Playlist)
            {
                var selectedPlaylist = GetSelectedPlaylist();

                //todo improve this so we aren't creating arbitary items
                foreach (var tab in selectedPlaylist.File)
                {
                    var file = (TablatureFile) tab.File;

                    var dummyItem = new TablatureLibraryItem<TablatureFile>(file, tab.FileInfo);
                    items.Add(dummyItem);
                }
            }

            else
            {
                items.AddRange(_tablatureLibrary.GetTablatureItems());
            }

            var currentItem = GetSelectedLibraryItem();

            _libraryCache.Clear();

            foreach (var item in items)
            {
                var visible = TablatureLibraryItemVisible(selectedLibrary, item);

                if (visible)
                    _libraryCache.Add(item);
            }

            listViewLibrary.SetObjects(_libraryCache);

            if (listViewLibrary.Items.Count > 0)
            {
                //persistant library selection
                if (persistSelectedItem && currentItem != null && _libraryCache.Contains(currentItem))
                {
                    listViewLibrary.SelectObject(currentItem);
                }

                else
                {
                    listViewLibrary.Items[0].Selected = true;
                }
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Title = "Import Tab - Tabster",
                Filter = string.Format("Tabster File (*{0})|*{0}", Constants.TablatureFileExtension),
                Multiselect = false
            })
            {
                ofd.SetTabsterFilter(_fileImporters, allSupportedTypesOption: false, alphabeticalOrder: true); //todo implement "all supported types" handling

                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    //native file format
                    if (ofd.FilterIndex == 1)
                    {
                        var file = _tablatureLibrary.TablatureFileProcessor.Load(ofd.FileName);

                        if (file != null)
                        {
                            _tablatureLibrary.Add(file);
                        }
                    }

                    else // third-party format
                    {
                        var importer = _fileImporters[ofd.FilterIndex - 2]; //FilterIndex is not 0-based and native Tabster format uses first index

                        AttributedTablature importedTab = null;

                        try
                        {
                            importedTab = importer.Import(ofd.FileName);
                        }

                        catch
                        {
                            MessageBox.Show("Error occured while importing.", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (importedTab != null)
                        {
                            using (var nd = new NewTabDialog(importedTab.Artist, importedTab.Title, importedTab.Type))
                            {
                                if (nd.ShowDialog() == DialogResult.OK)
                                {
                                    var tab = nd.Tab;
                                    tab.Contents = importedTab.Contents;
                                    tab.Source = new Uri(ofd.FileName);
                                    tab.SourceType = TablatureSourceType.FileImport;
                                    _tablatureLibrary.Add(tab);
                                    UpdateDetails();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void viewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = GetSelectedLibraryItem();

            if (item != null)
            {
                PopoutTab(item.File, item.FileInfo);
            }
        }

        private void UpdateDetails()
        {
            lblcount.Text = string.Format("Total Tabs: {0}", _tablatureLibrary.GetTablatureItems().Count);
            lblplaylists.Text = string.Format("Playlists: {0}", _tablatureLibrary.GetPlaylistItems().Count);
        }

        private void RemoveSelectedTablatureLibraryItem()
        {
            RemoveTablatureLibraryItem(listViewLibrary.SelectedIndex);
        }

        private void RemoveTablatureLibraryItem(int index)
        {
            var item = _libraryCache[index];
            listViewLibrary.RemoveObject(item);

            _libraryCache.Remove(item);

            listViewLibrary.SelectedIndex = index > 0 ? index - 1 : 0;

            UpdateDetails();
        }

        private void TabDetails(object sender, EventArgs e)
        {
            if (!IsViewingLibrary())
                return;

            if (GetSelectedLibraryItem() != null)
            {
                using (var details = new TabDetailsDialog(GetSelectedLibraryItem().File, _tablatureLibrary) {Icon = Icon})
                {
                    if (details.ShowDialog() == DialogResult.OK)
                    {
                        listViewLibrary.UpdateObject(listViewLibrary.SelectedObject);
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

                using (var pdd = new PlaylistDetailsDialog(playlist.File, playlist.FileInfo))
                {
                    if (pdd.ShowDialog() == DialogResult.OK)
                    {
                        if (pdd.PlaylistRenamed)
                        {
                            selectedNode.Text = playlist.File.Name;
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

        private void ClearTabPreview()
        {
            previewToolStrip.Enabled = false;
            lblpreviewtitle.Text = "";
            PreviewEditor.Clear();
        }

        private void LoadTabPreview(bool startViewCountTimer = true)
        {
            PreviewDisplayTimer.Stop();

            if (GetSelectedLibraryItem() != null)
            {
                lblpreviewtitle.Text = GetSelectedLibraryItem().File.ToFriendlyString();

                var openedExternally = Program.TabbedViewer.IsFileOpen(GetSelectedLibraryItem().FileInfo);

                PreviewEditor.Visible = !openedExternally;

                if (openedExternally)
                {
                    lblLibraryPreview.Visible = true;
                }

                else
                {
                    lblLibraryPreview.Visible = false;

                    PreviewEditor.LoadTablature(GetSelectedLibraryItem().File);

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
                ClearTabPreview();
            }
        }

        #region Tab Viewer Manager Events

        private void TabHandler_OnTabClosed(object sender, ITablatureFile file)
        {
            if (GetSelectedLibraryItem() != null)
            {
                LoadTabPreview();
                UpdateTabControls(false);
            }
        }

        #endregion

        #region Searching

        private void txtLibraryFilter_TextChanged(object sender, EventArgs e)
        {
            BuildLibraryCache();
        }

        #endregion

        #region Playlists

        private void DeletePlaylist(object sender, EventArgs e)
        {
            if (SelectedLibrary() == LibraryType.Playlist)
            {
                var playlistItem = GetSelectedPlaylist();

                if (playlistItem != null && MessageBox.Show("Are you sure you want to delete this playlist?", "Delete Playlist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _tablatureLibrary.RemovePlaylistItem(playlistItem);
                    RemovePlaylistNode(playlistItem);
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
                    if (string.IsNullOrEmpty(p.PlaylistName))
                    {
                        MessageBox.Show("Please enter a valid playlist name.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var playlist = new TablaturePlaylistFile {Name = p.PlaylistName};

                    var item = GetSelectedLibraryItem();

                    // new playlist
                    if (sender == newPlaylistToolStripMenuItem)
                    {
                        // 'add to' new playlist
                        if (item != null)
                        {
                            playlist.Add(new TablaturePlaylistItem(item.File, item.FileInfo));
                        }
                    }

                    var playlistItem = _tablatureLibrary.Add(playlist);

                    AddPlaylistNode(playlistItem.File, playlistItem.FileInfo);
                    PopulatePlaylistMenu();
                    UpdateDetails();
                }
            }
        }

        private PlaylistLibraryItem<TablaturePlaylistFile> GetSelectedPlaylist()
        {
            var playlistPath = sidemenu.SelectedNode.Tag.ToString();
            return _tablatureLibrary.FindPlaylistItemByPath(playlistPath);
        }

        private void AddPlaylistNode(TablaturePlaylistFile playlistFile, FileInfo fileInfo, bool select = false)
        {
            var playlistRootNode = sidemenu.Nodes["node_playlists"];

            //check if tablaturePlaylist node already exists
            var node = playlistRootNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Tag.ToString().Equals(fileInfo.FullName, StringComparison.OrdinalIgnoreCase));

            if (node == null)
            {
                node = new TreeNode(playlistFile.Name) {NodeFont = sidemenu.FirstNode.FirstNode.NodeFont, Tag = fileInfo.FullName};
                playlistRootNode.Nodes.Add(node);

                if (!playlistRootNode.IsExpanded)
                    playlistRootNode.ExpandAll();
            }

            if (select)
                sidemenu.SelectedNode = node;
        }

        private void RemovePlaylistNode(PlaylistLibraryItem<TablaturePlaylistFile> playlistItem)
        {
            foreach (TreeNode node in sidemenu.Nodes["node_playlists"].Nodes)
            {
                if (node.Tag.ToString().Equals(playlistItem.FileInfo.FullName))
                {
                    sidemenu.Nodes.Remove(node);
                    break;
                }
            }
        }

        /// <summary>
        ///     Populates 'add to' playlist menu.
        /// </summary>
        private void PopulatePlaylistMenu()
        {
            if (librarycontextaddtoplaylist.DropDownItems.Count > 0)
                librarycontextaddtoplaylist.DropDownItems.Clear();

            librarycontextaddtoplaylist.DropDownItems.Clear();

            foreach (var playlist in _tablatureLibrary.GetPlaylistItems())
            {
                var menuItem = new ToolStripMenuItem(playlist.File.Name) {Tag = playlist.FileInfo.FullName};

                menuItem.Click += (s, e) =>
                {
                    var path = ((ToolStripMenuItem) s).Tag.ToString();

                    var playlistItem = _tablatureLibrary.FindPlaylistItemByPath(path);
                    var playlistFile = playlistItem.File;

                    if (playlistFile != null)
                    {
                        var libraryItem = GetSelectedLibraryItem();

                        playlistFile.Add(new TablaturePlaylistItem(libraryItem.File, libraryItem.FileInfo));
                        playlistFile.Save(playlistItem.FileInfo.FullName);
                    }
                };

                librarycontextaddtoplaylist.DropDownItems.Add(menuItem);
            }

            if (librarycontextaddtoplaylist.DropDownItems.Count > 0)
                librarycontextaddtoplaylist.DropDownItems.Add(new ToolStripSeparator());

            librarycontextaddtoplaylist.DropDownItems.Add(newPlaylistToolStripMenuItem);
        }

        #endregion

        #region Preview Display

        private void PreviewDisplayTimer_Tick(object sender, EventArgs e)
        {
            if (GetSelectedLibraryItem() != null)
            {
                GetSelectedLibraryItem().Views += 1;
                GetSelectedLibraryItem().LastViewed = DateTime.UtcNow;
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