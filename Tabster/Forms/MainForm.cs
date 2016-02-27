#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BrightIdeasSoftware;
using RecentFilesMenuItem;
using Tabster.Controls;
using Tabster.Core.Searching;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Library;
using Tabster.Data.Processing;
using Tabster.Database;
using Tabster.Properties;
using Tabster.Update;
using Tabster.Utilities;
using ToolStripRenderer = Tabster.Controls.ToolStripRenderer;

#endregion

namespace Tabster.Forms
{
    internal partial class MainForm : Form
    {
        private readonly TabsterDatabaseHelper _databaseHelper;
        private readonly LibraryManager _libraryManager;
        private readonly PlaylistManager _playlistManager;
        private readonly FileInfo _queuedFileInfo;
        private readonly TablatureFile _queuedTablatureFile;
        private readonly RecentFilesManager _recentFilesManager;
        private UpdateResponseEventArgs _queuedUpdateResponse;
        private SplashScreen _splashScreen;

        public MainForm()
        {
            var tablatureDirectory = TabsterEnvironment.CreateEnvironmentDirectoryPath(TabsterEnvironmentDirectory.UserData, "Library");

            var databasePath = Path.Combine(TabsterEnvironment.GetEnvironmentDirectoryPath(TabsterEnvironmentDirectory.ApplicatonData), "library.db");

            var fileProcessor = new TabsterFileProcessor<TablatureFile>(Constants.TablatureFileVersion);

            Logging.GetLogger().Info(string.Format("Initializing database: {0}", databasePath));
            _databaseHelper = new TabsterDatabaseHelper(databasePath);

            _libraryManager = new LibraryManager(_databaseHelper, fileProcessor, tablatureDirectory);
            _playlistManager = new PlaylistManager(_databaseHelper, fileProcessor);
            _recentFilesManager = new RecentFilesManager(_databaseHelper, fileProcessor, Settings.Default.MaxRecentItems);

            InitializeComponent();

            Text = string.Format("{0} v{1}", Application.ProductName,
                new TabsterVersion(Application.ProductVersion).ToString(TabsterVersionFormatFlags.BuildString | TabsterVersionFormatFlags.Truncated));
#if PORTABLE
            Text = Text.Replace("Build", "Portable Build");
#endif
            InitAspectGetters();

            UpdateSortColumnMenu(true);

            //tabviewermanager events
            TablatureViewForm.GetInstance(this).TabClosed += TabHandler_OnTabClosed;

            recentlyViewedMenuItem.MaxDisplayItems = Settings.Default.MaxRecentItems;
            recentlyViewedMenuItem.ClearItemClicked += recentlyViewedMenuItem_OnClearItemClicked;

            previewToolStrip.Renderer = new ToolStripRenderer();

            if (Settings.Default.ClientState == FormWindowState.Maximized)
                WindowState = FormWindowState.Maximized;
            else
                Size = Settings.Default.ClientSize;

            CachePluginResources();

            ToggleEmptyLibraryOverlay(listViewLibrary, true);
            ToggleEmptyLibraryOverlay(listViewSearch, true);

            PreviewEditor.Font = TablatureFontManager.GetFont();
            searchPreviewEditor.Font = TablatureFontManager.GetFont();
        }

        public MainForm(TablatureFile tablatureFile, FileInfo fileInfo)
            : this()
        {
            _queuedTablatureFile = tablatureFile;
            _queuedFileInfo = fileInfo;
        }

        private void updateQuery_Completed(object sender, UpdateResponseEventArgs e)
        {
            var isStartupCheck = (bool) e.UserState;

            if (isStartupCheck)
                _queuedUpdateResponse = e;
            else
                OnUpdateResponse(e);
        }

        private void recentlyViewedMenuItem_OnClearItemClicked(object sender, EventArgs e)
        {
            _recentFilesManager.Clear();
        }

        /// <summary>
        ///     Initializes aspect-getters for library columns.
        /// </summary>
        private void InitAspectGetters()
        {
            olvColArtist.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).File.Artist;
            olvColTitle.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).File.Title;
            olvColType.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).File.Type.Name;
            olvColCreated.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).File.FileAttributes.Created;
            olvColModified.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).FileInfo.LastWriteTime;
            olvColLocation.AspectGetter = x => ((TablatureLibraryItem<TablatureFile>) x).FileInfo.FullName;

            //search
            olvColumn1.AspectGetter = x => ((TablatureSearchResult) x).Tab.Artist;
            olvColumn2.AspectGetter = x => ((TablatureSearchResult) x).Tab.Title;
            olvColumn3.AspectGetter = x => ((TablatureSearchResult) x).Tab.Type.Name;

            olvColumn4.AspectGetter = x =>
            {
                var rating = ((TablatureSearchResult) x).Rating;
                return rating == TablatureRating.None ? "" : new string('\u2605', (int) rating - 1).PadRight(5, '\u2606');
            };

            olvColumn5.AspectGetter = x => ((TablatureSearchResult) x).Engine.Name;
            olvColumn6.AspectGetter = x => ((TablatureSearchResult) x).Source.ToString();
        }

        private static void ToggleEmptyLibraryOverlay(ObjectListView olv, bool enabled)
        {
            var textOverlay = olv.EmptyListMsgOverlay as TextOverlay;
            textOverlay.TextColor = enabled ? SystemColors.InactiveCaptionText : Color.Transparent;
            textOverlay.BackColor = Color.Transparent;
            textOverlay.BorderWidth = 0;
        }

        private static void OnUpdateResponse(UpdateResponseEventArgs e)
        {
            var isStartupCheck = (bool) e.UserState;

            if (e.Response != null && e.Response.LatestVersion > new Version(Application.ProductVersion))
            {
                var updateDialog = new UpdateDialog(e.Response, new Version(Application.ProductVersion)) {StartPosition = FormStartPosition.CenterParent};
                updateDialog.ShowDialog();
            }

            else if (!isStartupCheck)
            {
                MessageBox.Show("Your version of Tabster is up to date.", "No Updates Available");
            }
        }

        private void ToggleVisibility(bool visible)
        {
            if (visible)
            {
                Invoke((Action) delegate
                {
                    Opacity = 1.0f;
                    ShowInTaskbar = true;
                });
            }

            else
            {
                Invoke((Action) delegate
                {
                    Opacity = 0.0f;
                    ShowInTaskbar = true;
                });
            }
        }

        private const int SplashTime = 3500;

        private void Form1_Load(object sender, EventArgs e)
        {
            var done = false;

            if (!TabsterEnvironment.NoSplash)
            {
                ToggleVisibility(false);

                ThreadPool.QueueUserWorkItem((x) =>
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    using (_splashScreen = new SplashScreen())
                    {
                        _splashScreen.Show();

                        long elapsed;

                        do
                        {
                            sw.Stop();
                            elapsed = sw.ElapsedMilliseconds;
                            sw.Start();
                            Application.DoEvents();
                        } while (!done || elapsed < SplashTime);

                        _splashScreen.Close();
                        ToggleVisibility(true);
                    }
                });
            }

            PerformStartupEvents();
            done = true;

            Activate();

            sidemenu.SelectedNode = sidemenu.Nodes[0].FirstNode;

            UpdateDetails();
        }

        private void UpdateSplash(string status)
        {
            if (_splashScreen != null && !_splashScreen.IsDisposed)
            {
                _splashScreen.UpdateStatus(status);
            }
        }

        private void PerformStartupEvents()
        {
            if (!TabsterEnvironment.SafeMode)
            {
                UpdateSplash("Initializing plugins...");
                Logging.GetLogger().Info("Loading plugins...");

                Program.GetPluginController().LoadPlugins();

                var disabledGuids = new List<Guid>();
                foreach (var guid in Settings.Default.DisabledPlugins)
                {
                    disabledGuids.Add(new Guid(guid));
                }

                foreach (var pluginHost in Program.GetPluginController().GetPluginHosts().Where(pluginHost => !disabledGuids.Contains(pluginHost.Plugin.Guid)))
                {
                    pluginHost.Enabled = true;
                }
            }

            // database file deleted or possible pre-2.0 version, convert existing files
            if (_databaseHelper.DatabaseCreated)
            {
                Logging.GetLogger().Info("Converting old file types...");
                UpdateSplash("Converting old file types...");
                XmlFileConverter.ConvertXmlFiles(_playlistManager, _libraryManager);
            }

            Logging.GetLogger().Info("Initializing library...");
            UpdateSplash("Loading library...");
            _libraryManager.Load(_databaseHelper.DatabaseCreated);

            Logging.GetLogger().Info("Initializing playlists...");
            UpdateSplash("Loading playlists...");
            _playlistManager.Load();

            foreach (var playlist in _playlistManager.GetPlaylists())
            {
                AddPlaylistNode(playlist);
            }

            PopulatePlaylistMenu();

            Logging.GetLogger().Info("Preparing search functions...");
            UpdateSplash("Preparing search functions...");
            InitializeSearchControls(true);
            BuildSearchSuggestions();

            Logging.GetLogger().Info("Loading user environment...");
            UpdateSplash("Loading user environment...");
            _recentFilesManager.Load();
            LoadRecentItems();

            LoadSettings(true);
            PopulateTabTypeControls();

            UpdateCheck.Completed += updateQuery_Completed;
            if (Settings.Default.StartupUpdate)
            {
                UpdateSplash("Checking for updates...");
                UpdateCheck.Check(true);
            }

            if (_queuedUpdateResponse != null)
            {
                OnUpdateResponse(_queuedUpdateResponse);
            }

            if (_queuedTablatureFile != null)
            {
                PopoutTab(_queuedTablatureFile, _queuedFileInfo);
            }
        }

        private void LoadRecentItems()
        {
            recentlyViewedMenuItem.Clear();

            foreach (var item in _recentFilesManager.GetItems())
            {
                recentlyViewedMenuItem.Add(new RecentMenuItem(item.FileInfo) { DisplayText = item.TablatureFile.ToFriendlyString() });
            }
        }

        private void UpdateSortColumnMenu(bool populateItems = false)
        {
            if (populateItems)
            {
                sortByToolStripMenuItem.DropDownItems.Clear();

                foreach (OLVColumn column in listViewLibrary.Columns)
                {
                    var item = new ToolStripMenuItem(column.Text);
                    item.Click += SortByColumnMenuItem_Click;
                    sortByToolStripMenuItem.DropDownItems.Add(item);
                }

                sortByToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

                sortByToolStripMenuItem.DropDownItems.Add(ascendingToolStripMenuItem);
                sortByToolStripMenuItem.DropDownItems.Add(descendingToolStripMenuItem);
            }

            var sortedColumn = listViewLibrary.PrimarySortColumn ?? listViewLibrary.GetColumn(0);

            for (var i = 0; i < listViewLibrary.Columns.Count; i++)
            {
                var col = listViewLibrary.GetColumn(i);

                var menuItem = sortByToolStripMenuItem.DropDownItems[i];

                menuItem.Visible = col.IsVisible;
                ((ToolStripMenuItem) menuItem).Checked = sortedColumn == col;
            }

            ascendingToolStripMenuItem.Checked = listViewLibrary.PrimarySortOrder == SortOrder.Ascending;
            descendingToolStripMenuItem.Checked = !ascendingToolStripMenuItem.Checked;
        }

        private void SortByColumnMenuItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;

            var index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);

            listViewLibrary.Sort(index);
        }

        private void SortByDirectionMenuItem_Click(object sender, EventArgs e)
        {
            var direction = sender == descendingToolStripMenuItem ? SortOrder.Descending : SortOrder.Ascending;
            var col = listViewLibrary.PrimarySortColumn ?? listViewLibrary.GetColumn(0);
            listViewLibrary.Sort(col, direction);
        }

        private void PopulateTabTypeControls()
        {
            foreach (var t in TablatureType.GetKnownTypes())
            {
                var typeStr = t.ToFriendlyString();

                //library menu
                sidemenu.FirstNode.Nodes.Add(new TreeNode(typeStr) {NodeFont = sidemenu.FirstNode.FirstNode.NodeFont, Tag = t.ToString()});
            }
        }

        private void CachePluginResources()
        {
            _fileExporters = new List<ITablatureFileExporter>(Program.GetPluginController().GetClassInstances<ITablatureFileExporter>());
            _fileImporters = new List<ITablatureFileImporter>(Program.GetPluginController().GetClassInstances<ITablatureFileImporter>());
            _webImporters = new List<ITablatureWebImporter>(Program.GetPluginController().GetClassInstances<ITablatureWebImporter>());
        }

        private void OpenRecentFile(MenuItem item)
        {
            var recentMenuItem = (RecentMenuItem)item;

            var tab = _libraryManager.GetTablatureFileProcessor().Load(recentMenuItem.FileInfo.FullName);

            if (tab != null)
            {
                TablatureViewForm.GetInstance(this).LoadTablature(tab, new FileInfo(recentMenuItem.FileInfo.FullName));
            }
        }

        private void recentlyViewedMenuItem_OnItemClicked(object sender, EventArgs e)
        {
            OpenRecentFile((MenuItem) sender);
        }

        private void recentlyViewedMenuItem_OnAllItemsOpened(object sender, EventArgs e)
        {
            foreach (var item in recentlyViewedMenuItem.Items)
            {
                OpenRecentFile(item);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _libraryManager.Save();
            _recentFilesManager.Save();

            SaveSettings();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
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

                if (orientation != PreviewPanelOrientation.Hidden && GetSelectedSearchResult() == null)
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

        private void batchDownloaderMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new DownloadDialog(_webImporters))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var tab in dialog.DownloadedTabs)
                    {
                        var libraryItem = _libraryManager.Add(tab);

                        //todo use objectliveview filtering instead of manual
                        if (TablatureLibraryItemVisible(SelectedLibrary(), libraryItem))
                        {
                            listViewLibrary.AddObject(libraryItem);
                        }
                    }
                }
            }
        }

        private void PreviewEditor_ContentsModified(object sender, EventArgs e)
        {
            toolStripButton3.Enabled = PreviewEditor.HasScrollableContents;
        }

        private void listViewLibrary_AfterSorting(object sender, AfterSortingEventArgs e)
        {
            UpdateSortColumnMenu();
        }

        private void listViewLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_changingLibraryView)
                return;

            UpdateTabControls(true);
        }

        private void listViewLibrary_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = GetSelectedLibraryItem();

            if (item != null)
            {
                PopoutTab(item.File, item.FileInfo);
            }
        }

        private void listViewLibrary_DragDrop(object sender, DragEventArgs e)
        {
            var data = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
            var selectedPlaylist = GetSelectedPlaylist();

            var needsSaved = false;

            foreach (var str in data)
            {
                var file = _libraryManager.GetTablatureFileProcessor().Load(str);

                //copy to playlist
                if (selectedPlaylist != null)
                {
                    if (selectedPlaylist.Find(str) == null)
                    {
                        selectedPlaylist.Add(new TablaturePlaylistItem(file, new FileInfo(str)));
                        needsSaved = true;
                    }
                }

                    //copy to library
                else
                {
                    if (_libraryManager.FindTablatureItemByPath(str) == null)
                    {
                        _libraryManager.Add(file);

                        needsSaved = true;
                    }
                }
            }

            if (needsSaved)
            {
                if (selectedPlaylist != null)
                    _playlistManager.Update(selectedPlaylist);
            }
        }

        private void listViewLibrary_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            //e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void listViewSearch_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            if (GetSelectedSearchResult() == null)
                return;

            e.MenuStrip = SearchMenu;
        }

        private void listViewSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedSearchResultPreview();

            var selectedResult = GetSelectedSearchResult();
            saveTabToolStripMenuItem1.Enabled = selectedResult != null && _searchResultsCache.ContainsKey(selectedResult.Source);
        }

        private void btnSearchOptions_Click(object sender, EventArgs e)
        {
            OpenPreferences(PreferencesDialog.PreferencesSection.Searching);
        }

        private void listViewSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (GetSelectedSearchResult() != null)
            {
                SaveSelectedSearchResult();
            }
        }

        private void saveTabToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveSelectedSearchResult();
        }

        private void tabsCurrentTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            var viewingPreview = tabsCurrentTab.SelectedIndex == 0;

            toolStripButton3.Enabled = viewingPreview;
            printbtn.Enabled = viewingPreview;
        }

        private void OpenPluginManager()
        {
            if (TabsterEnvironment.SafeMode)
            {
                MessageBox.Show("Plugins are not configurable while running in safe mode.", "Safe Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var p = new PluginManagerDialog())
            {
                if (p.ShowDialog() == DialogResult.OK)
                {
                    LoadSettings(false);

                    if (p.PluginsModified)
                        CachePluginResources();
                }
            }
        }

        private void pluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPluginManager();
        }

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
            UpdateCheck.Check(false);
        }

        private void OpenPreferences(PreferencesDialog.PreferencesSection section)
        {
            using (var p = new PreferencesDialog(_recentFilesManager, section))
            {
                if (p.ShowDialog() == DialogResult.OK)
                {
                    LoadSettings(false);

                    if (p.MaxRecentItemsModified)
                    {
                        recentlyViewedMenuItem.MaxDisplayItems = Settings.Default.MaxRecentItems;
                        _recentFilesManager.MaxItems = Settings.Default.MaxRecentItems;
                    }

                    if (p.RecentItemsCleared)
                         recentlyViewedMenuItem.Clear();
                }
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPreferences(PreferencesDialog.PreferencesSection.General);
        }

        #endregion
    }
}