﻿#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Tabster.Controls;
using Tabster.Properties;
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
        public enum LibraryType
        {
            AllTabs,
            MyDownloads,
            MyTabs,
            MyImports,
            MyFavorites,
            GuitarTabs,
            GuitarChords,
            BassTabs,
            DrumTabs,
            Playlist
        }

        private readonly ToolStripMenuItem newplaylistmenuitem = new ToolStripMenuItem
                                                                     {
                                                                         Text = "New Playlist",
                                                                     };

        private TabFile SelectedTab;

        private TabEditor _currentEditor;

        private bool IsViewingLibrary()
        {
            return tabControl1.SelectedTab == display_library;
        }

        #region Tab Viewer Manager Events

        void TabHandler_OnTabClosed(object sender, TabFile tabFile)
        {
            if (SelectedTab != null)
            {
                LoadTabPreview();
            }
        }

        void TabHandler_OnTabOpened(object sender, TabFile tabFile)
        {
            if (SelectedTab != null)
            {
                LoadTabPreview();
            }
        }

        #endregion

        #region Searching

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filtertext.Focus();
            filtertext.SelectAll();
        }

        private void filtertext_OnNewSearch(object sender, string value)
        {
            if (filtertext.IsFilterSet || filtertext.FilterReset)
            {
                LoadLibrary(value);
            }
        }

        #endregion

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (SelectedTab != null)
            {
                PopoutTab(SelectedTab);
            }
        }

        private void dataGridViewExtended2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

            var playlist = sidemenu.SelectedPlaylist();

            if (data != null)
            {
                foreach (var str in data)
                {
                    ImportTab(str, playlist);
                }
            }
        }

        private void DeletePlaylist(object sender, EventArgs e)
        {
            if (sidemenu.PlaylistNodeSelected())
            {
                var playlist = sidemenu.SelectedPlaylist();

                if (playlist != null && MessageBox.Show("Are you sure you want to delete this playlist?", "Delete Playlist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Program.libraryManager.RemovePlaylist(playlist);
                    PopulatePlaylists();
                    UpdateDetails();
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
                                         Filter = string.Format("Tabster File (*{0})|*{0}|Text File (*.txt)|*.txt", TabFile.FILE_EXTENSION),
                                         FileName = SelectedTab.TabData.ToString()
                                     })
                {
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        var format = sfd.FilterIndex == 1 ? ExportFormat.Tabster : ExportFormat.Text;
                        SelectedTab.Export(format, sfd.FileName);
                    }
                }
            }
        }

        private void UpdateTabControls(bool beginPreviewLoad)
        {
            if (tablibrary.SelectedRows.Count > 0)
            {
                var selectedTabLocation = tablibrary.SelectedRows[0].Cells[tablibrary.Columns.Count - 1].Value.ToString();
                SelectedTab = Program.libraryManager.FindTabByPath(selectedTabLocation).File;

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

            viewTabToolStripMenuItem.Enabled = SelectedTab != null;
            exportToolStripMenuItem.Enabled = SelectedTab != null;
            openTabLocationToolStripMenuItem.Enabled = SelectedTab != null;
            searchUltimateGuitarToolStripMenuItem.Enabled = SelectedTab != null;

            if (beginPreviewLoad)
            {
                PreviewDelay.Stop();
                PreviewDelay.Start();
            }
        }

        private void printbtn_Click(object sender, EventArgs e)
        {
            if (_currentEditor != null)
                _currentEditor.Print();
        }

        private void dataGridViewExtended2_SelectionChanged(object sender, EventArgs e)
        {
            UpdateTabControls(true);
        }

        private void NewTab(object sender, EventArgs e)
        {
            using (var n = new NewTabDialog())
            {
                if (n.ShowDialog() == DialogResult.OK)
                {
                    var tabFile = TabFile.Create(n.TabData, Program.libraryManager.TabsDirectory);
                    Program.libraryManager.AddTab(tabFile, true);
                    UpdateLibraryItem(tabFile);
                }
            }
        }
        private void PopoutTab(TabFile tab)
        {
            Program.TabHandler.LoadExternally(tab, true);

            recentlyViewedToolStripMenuItem.Add(tab);
            UpdateTabControls(false);
            LoadTabPreview();
        }

        private void SearchSimilarTabs(object sender, EventArgs e)
        {
            if (SelectedTab != null)
            {
                txtsearchartist.Text = SelectedTab.TabData.Artist;
                txtsearchsong.Text = SelectedTab.TabData.Title;
                txtsearchtype.SelectedIndex = 0;
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

                if (sidemenu.PlaylistNodeSelected())
                {
                    var selectedPlaylist = sidemenu.SelectedPlaylist();

                    if (selectedPlaylist != null)
                    {
                        if (MessageBox.Show("Are you sure you want to remove this tab from the playlist?", "Remove Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            selectedPlaylist.PlaylistData.Remove(SelectedTab);
                            removed = true;
                        }
                    }
                }

                else
                {
                    if (MessageBox.Show("Are you sure you want to delete this tab?", "Delete Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        Program.libraryManager.RemoveTab(SelectedTab, true);
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
                                     Filter = string.Format("Tabster Files (*{0})|*{0}", TabFile.FILE_EXTENSION)
                                 })
            {
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    TabFile t;

                    if (TabFile.TryParse(ofd.FileName, out t))
                    {
                        //Load("Browse", ofd.FileName);
                        PopoutTab(t);
                    }
                }
            }
        }

        private void autoScrollChange(object sender, EventArgs e)
        {
            if (_currentEditor != null)
            {
                var item = ((ToolStripMenuItem) sender);
                var text = item.Text;

                foreach (ToolStripMenuItem menuItem in toolStripButton3.DropDownItems)
                {
                    menuItem.Checked = menuItem.Text == item.Text;
                }    

                _currentEditor.AutoScroll = text == "On";
            }
        }

        private void sidemenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            filtertext.Reset(true);
            LoadLibrary();
            //libraryViewer1.ClearSelection();
        }

        private void ToggleFavorite(object sender, EventArgs e)
        {
            if (SelectedTab != null)
            {
                var libraryItem = Program.libraryManager.FindTab(SelectedTab);
                libraryItem.Favorited = !libraryItem.Favorited;
                Program.libraryManager.Save();

                //remove item from favorites display
                if (!libraryItem.Favorited && SelectedLibrary() == LibraryType.MyFavorites)
                {
                    RemoveSelectedLibraryItem();
                }
            }
        }

        private void sidemenu_MouseClick(object sender, MouseEventArgs e)
        {
            var selectednode = sidemenu.HitTest(e.X, e.Y).Node;
            if (selectednode != null)
            {
                if (e.Button == MouseButtons.Right && sidemenu.PlaylistNodeSelected())
                {
                    sidemenu.SelectedNode = selectednode;
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

                UpdateTabControls(false);

                LibraryMenu.Show(tablibrary.PointToScreen(e.Location));

                //check if playlists already contain 
                foreach (var item in librarycontextaddtoplaylist.DropDownItems)
                {
                    var toolItem = item as ToolStripMenuItem;

                    if (toolItem != null && toolItem.Tag != null)
                    {
                        var playlistPath = toolItem.Tag.ToString();
                        var associatedPlaylist = Program.libraryManager.FindPlaylistByPath(playlistPath);

                        var alreadyExistsInPlaylist = associatedPlaylist.PlaylistData.Contains(SelectedTab);

                        toolItem.Enabled = !alreadyExistsInPlaylist;
                    }
                }

                var libraryItem = Program.libraryManager.FindTab(SelectedTab);
                librarycontextfavorites.Text = libraryItem.Favorited ? "Remove from favorites" : "Add to favorites";
            }
        }

        public LibraryType SelectedLibrary()
        {
            if (sidemenu.PlaylistNodeSelected())
            {
                return LibraryType.Playlist;
            }

            var selectedNode = sidemenu.SelectedNode;

            if (selectedNode != null)
            {
                if (selectedNode == sidemenu.NodeAllTabs)
                    return LibraryType.AllTabs;
                if (selectedNode == sidemenu.NodeMyTabs)
                    return LibraryType.MyTabs;
                if (selectedNode == sidemenu.NodeMyDownloads)
                    return LibraryType.MyDownloads;
                if (selectedNode == sidemenu.NodeMyImports)
                    return LibraryType.MyImports;
                if (selectedNode == sidemenu.NodeMyFavorites)
                    return LibraryType.MyFavorites;
                if (selectedNode == sidemenu.NodeGuitarTabs)
                    return LibraryType.GuitarTabs;
                if (selectedNode == sidemenu.NodeGuitarChords)
                    return LibraryType.GuitarChords;
                if (selectedNode == sidemenu.NodeBassTabs)
                    return LibraryType.BassTabs;
                if (selectedNode == sidemenu.NodeDrumTabs)
                    return LibraryType.DrumTabs;
            }
            return LibraryType.AllTabs;
        }

        public void LoadLibrary(string searchValue = null)
        {
            var selectedPlaylist = sidemenu.SelectedPlaylist();

            //nothing to filter
            if ((selectedPlaylist != null && selectedPlaylist.PlaylistData.Count == 0) || (selectedPlaylist == null && Program.libraryManager.TabCount == 0))
            {
                tablibrary.Rows.Clear();
                return;
            }

            tablibrary.Rows.Clear();
            tablibrary.SuspendLayout();

            var selectedLibrary = SelectedLibrary();

            var tabCollection = new List<TabFile>();

            if (selectedLibrary == LibraryType.Playlist)
            {
                tabCollection = new List<TabFile>(selectedPlaylist.PlaylistData);
            }

            else
            {
                foreach (var item in Program.libraryManager)
                {
                    tabCollection.Add(item.File);
                }
            }

            foreach (var tab in tabCollection)
            {
                var libraryMatch = 
                    selectedLibrary == LibraryType.Playlist || 
                    selectedLibrary == LibraryType.AllTabs ||
                   (selectedLibrary == LibraryType.MyTabs && tab.TabData.SourceType == TabSource.UserCreated) ||
                   (selectedLibrary == LibraryType.MyDownloads && tab.TabData.SourceType == TabSource.Download) ||  
                   (selectedLibrary == LibraryType.MyImports && tab.TabData.SourceType == TabSource.FileImport) ||
                   (selectedLibrary == LibraryType.GuitarTabs && tab.TabData.Type == TabType.Guitar) ||
                   (selectedLibrary == LibraryType.GuitarChords && tab.TabData.Type == TabType.Chord) ||
                   (selectedLibrary == LibraryType.BassTabs && tab.TabData.Type == TabType.Bass) ||
                   (selectedLibrary == LibraryType.DrumTabs && tab.TabData.Type == TabType.Drum) || 
                   (selectedLibrary == LibraryType.MyFavorites && Program.libraryManager.FindTab(tab).Favorited);

                if (libraryMatch)
                {
                    var searchMatch = searchValue == null || (tab.TabData.Artist.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                              tab.TabData.Title.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                              (
                                                                   tab.FileInfo.FullName.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                                   tab.TabData.Contents.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                                   tab.TabData.Lyrics.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                                   tab.TabData.Comment.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                                                              );

                    if (searchMatch)
                    {
                        UpdateLibraryItem(tab);
                    }
                }
            }

            tablibrary.ResumeLayout();

            //UpdateDetails();

            //update tab counts next to library sections
            /*
            sidemenu.UpdateLibraryCount(LibraryType.AllTabs, MyLibrary.Tabs.Count);
            sidemenu.UpdateLibraryCount(LibraryType.GuitarTabs, MyLibrary.GuitarTabs);
            sidemenu.UpdateLibraryCount(LibraryType.GuitarChords, MyLibrary.GuitarChords);
            sidemenu.UpdateLibraryCount(LibraryType.BassTabs, MyLibrary.BassTabs);
            sidemenu.UpdateLibraryCount(LibraryType.DrumTabs, MyLibrary.DrumTabs);
            */
            //sidemenu.Nodes["node_library"].Text = string.Format("Library ({0})", total);


            //display corrupted tab count
            /*
             * var corrupted = Global.libraryManager.Tabs.GetCorruptedTabs();
            toolStripSeparator1.Visible = corrupted.Count > 0;
            toolStripStatusLabel1.Text = string.Format("Corrupted: {0}", corrupted.Count);
            toolStripStatusLabel1.Visible = corrupted.Count > 0;
            */

            //reset sort order
            //if (sortedColumn != null)
            //    libraryViewer1.Sort(sortedColumn, sortOrder);

            //if (tablibrary.Rows.Count > rowindex) tablibrary.Rows[rowindex].Selected = true;
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
                        var playlistFile = PlaylistFile.Create(new Playlist(name), Program.libraryManager.PlaylistsDirectory);
                        Program.libraryManager.AddPlaylist(playlistFile, true);

                        PopulatePlaylists();

                        //add tab to new playlist
                        if (sender == newplaylistmenuitem && SelectedTab != null)
                        {
                            playlistFile.PlaylistData.Add(SelectedTab);
                            playlistFile.Save();
                        }
                    }
                }
            }
        }

        private void ImportTab(string path, PlaylistFile playlist = null)
        {
            TabFile source;
            if (TabFile.TryParse(path, out source))
            {
                var alreadyExists = playlist != null ? playlist.PlaylistData.Contains(path) : Program.libraryManager.FindTabByPath(path) != null;

                if (!alreadyExists)
                {
                    if (playlist != null)
                    {
                        playlist.PlaylistData.Add(source);
                        UpdateLibraryItem(source);
                        playlist.Save();
                    }

                    else
                    {
                        var import = TabFile.Create(source.TabData, Program.libraryManager.TabsDirectory);
                        Program.libraryManager.AddTab(import, true);
                        UpdateLibraryItem(import);
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
                    var tabFile = TabFile.Create(i.TabData, Program.libraryManager.TabsDirectory);
                    Program.libraryManager.AddTab(tabFile, true);
                    UpdateLibraryItem(tabFile);
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

        private void sidebarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sidebarToolStripMenuItem.Checked = !sidebarToolStripMenuItem.Checked;
            splitContainer1.Panel1Collapsed = !sidebarToolStripMenuItem.Checked;
            Settings.Default.SidePanel = !splitContainer1.Panel1Collapsed;
            Settings.Default.Save();
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusBarToolStripMenuItem.Checked = !statusBarToolStripMenuItem.Checked;
            statusStrip1.Visible = statusBarToolStripMenuItem.Checked;
            Settings.Default.StatusBar = statusBarToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void PopulatePlaylists()
        {
            if (sidemenu.NodePlaylists.Nodes.Count > 0)
                sidemenu.NodePlaylists.Nodes.Clear();

            if (librarycontextaddtoplaylist.DropDownItems.Count > 0)
                librarycontextaddtoplaylist.DropDownItems.Clear();

            librarycontextaddtoplaylist.DropDownItems.Clear();

            foreach (var playlist in Program.libraryManager.Playlists)
            {
                sidemenu.AddPlaylist(playlist);

                var menuItem = new ToolStripMenuItem(playlist.PlaylistData.Name) { Tag = playlist.FileInfo.FullName };

                menuItem.Click += (s, e) =>
                                      {
                                          var path = ((ToolStripMenuItem) s).Tag.ToString();

                                          var pf = Program.libraryManager.FindPlaylistByPath(path);

                                          if (pf != null)
                                          {
                                              pf.PlaylistData.Add(SelectedTab);
                                              pf.Save();
                                          }
                                      };

                librarycontextaddtoplaylist.DropDownItems.Add(menuItem);
            }

            if (librarycontextaddtoplaylist.DropDownItems.Count > 0)
            {
                librarycontextaddtoplaylist.DropDownItems.Add(new ToolStripSeparator());
            }

            newplaylistmenuitem.Click -= NewPlaylist;
            newplaylistmenuitem.Click += NewPlaylist;
            librarycontextaddtoplaylist.DropDownItems.Add(newplaylistmenuitem);

            UpdateDetails();
        }

        private void UpdateDetails()
        {
            lblcount.Text = string.Format("Total Tabs: {0}", Program.libraryManager.TabCount);
            lblplaylists.Text = string.Format("Playlists: {0}", Program.libraryManager.PlaylistCount);

            var usage = Program.libraryManager.DiskUsage;
            var kilobytes = usage / 1024d;
            var megabytes = usage / 1024d / 1024d;

            lbldisk.Text = string.Format("Disk Space: {0}KB ({1}MB)",
                                         (usage == 0 ? "0" : kilobytes.ToString("#.##")),
                                         (usage == 0 ? "0" : megabytes.ToString("#.##")));
        }

        public void UpdateLibraryItem(TabFile tab, bool append = true)
        {
            var objValues = new object[]
                                {
                                    tab.TabData.Title, 
                                    tab.TabData.Artist, 
                                    Tab.GetTabString(tab.TabData.Type), 
                                    tab.TabData.Created, 
                                    string.Format("{0:0.##} KB", tab.FileInfo.Length / 1024d),
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

            UpdateDetails();
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
                using (var details = new TabDetailsDialog(SelectedTab) { Icon = Icon })
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
            if (sidemenu.PlaylistNodeSelected())
            {
                var playlist = sidemenu.SelectedPlaylist();
                var selectedNode = sidemenu.SelectedNode;

                using (var pdd = new PlaylistDetailsDialog(playlist))
                {
                    if (pdd.ShowDialog() == DialogResult.OK)
                    {
                        if (pdd.PlaylistRenamed)
                        {
                            selectedNode.Text = playlist.PlaylistData.Name;
                        }
                    }
                }
            }
        }

        private void PreviewDelay_Tick(object sender, EventArgs e)
        {
            PreviewDelay.Stop();
            LoadTabPreview();
        }

        private void LoadTabPreview()
        {
            if (SelectedTab != null)
            {
                lblpreviewtitle.Text = SelectedTab.TabData.ToString();

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
                    editor.LoadTab(SelectedTab.TabData);

                    lblopenedexternally.Visible = false;
                    
                    librarySplitContainer.Panel2.Controls.Add(editor);

                    editor.BringToFront();
                    editor.Visible = true;
                    editor.ReadOnly = true;

                    //cancel autoscroll of existing editor
                    if (_currentEditor != null)
                    {
                        _currentEditor.AutoScroll = false;
                        _currentEditor.ScrollToLine(0);
                        offToolStripMenuItem.PerformClick();
                    }

                    _currentEditor = editor;
                }

                librarySplitContainer.Panel2.Enabled = !openedExternally;
                previewToolStrip.Enabled = true;
            }

            else
            {
                previewToolStrip.Enabled = false;
                lblpreviewtitle.Text = "";
            }
        }
    }
}