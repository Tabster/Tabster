#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using SearchOption = System.IO.SearchOption;

#endregion

namespace Tabster
{
    public class LibraryItem<T>
    {
        public LibraryItem(T file, int views, bool favorited)
        {
            File = file;
            Views = views;
            Favorited = favorited;
        }

        public T File { get; private set; }
        public int Views { get; private set; }
        public bool Favorited { get; private set; }
    }

    public class LibraryManager : TabsterFile, ITabsterFile, IEnumerable<TabFile>
    {
        public const string FILE_VERSION = "1.0";

        private readonly List<PlaylistFile> _playlists = new List<PlaylistFile>();
        private readonly List<TabFile> _tabs = new List<TabFile>();
        private readonly BackgroundWorker playlistWorker = new BackgroundWorker();
        private readonly BackgroundWorker tabWorker = new BackgroundWorker();

        private List<string> _tabPaths, _playlistPaths = new List<string>();

        public string LibraryDirectory { get; private set; }
        public string TabsDirectory { get; private set; }
        public string PlaylistsDirectory { get; private set; }
        public string ApplicationDirectory { get; private set; }
        public string TemporaryDirectory { get; private set; }

        public ReadOnlyCollection<PlaylistFile> Playlists
        {
            get { return _playlists.AsReadOnly(); }
        }

        public long DiskUsage
        {
            get
            {
                long total = 0;
                foreach (var tab in _tabs)
                    total += tab.FileInfo.Length;
                return total;
            }
        }

        public LibraryManager()
        {
            LibraryDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tabster");
            TabsDirectory = Path.Combine(LibraryDirectory, "Library");
            PlaylistsDirectory = Path.Combine(LibraryDirectory, "Playlists");
            ApplicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tabster");
            TemporaryDirectory = Path.Combine(ApplicationDirectory, "Temp");

            if (!Directory.Exists(LibraryDirectory))
                Directory.CreateDirectory(LibraryDirectory);

            if (!Directory.Exists(TabsDirectory))
                Directory.CreateDirectory(TabsDirectory);

            if (!Directory.Exists(PlaylistsDirectory))
                Directory.CreateDirectory(PlaylistsDirectory);

            if (!Directory.Exists(ApplicationDirectory))
                Directory.CreateDirectory(ApplicationDirectory);

            if (!Directory.Exists(TemporaryDirectory))
                Directory.CreateDirectory(TemporaryDirectory);

            FileInfo = new FileInfo(Path.Combine(ApplicationDirectory, "library.dat"));

            tabWorker.DoWork += tabWorker_DoWork;
            tabWorker.RunWorkerCompleted += tabWorker_RunWorkerCompleted;
            playlistWorker.RunWorkerCompleted += playlistWorker_RunWorkerCompleted;
            playlistWorker.DoWork += playlistWorker_DoWork;
        }

        #region Background Workers

        private void tabWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _tabs.Clear();

            foreach (var file in _tabPaths)
            {
                if (File.Exists(file))
                {
                    TabFile tabFile;

                    if (TabFile.TryParse(file, out tabFile))
                    {
                        AddTab(tabFile, false);
                    }
                }
            }
        }

        private void tabWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TabsLoaded = e.Error == null;

            if (OnTabsLoaded != null)
                OnTabsLoaded(this, EventArgs.Empty);
        }

        private void playlistWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _playlists.Clear();

            foreach (var file in _playlistPaths)
            {
                if (File.Exists(file))
                {
                    PlaylistFile playlistFile;

                    if (PlaylistFile.TryParse(file, out playlistFile))
                    {
                        AddPlaylist(playlistFile, false);
                    }
                }
            }
        }

        private void playlistWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PlaylistsLoaded = e.Error == null;

            if (OnPlaylistsLoaded != null)
                OnPlaylistsLoaded(this, EventArgs.Empty);
        }

        #endregion

        #region Active Files

        public List<PlaylistFile> GetPlaylistfiles()
        {
            var tempList = new List<PlaylistFile>();

            foreach (var file in new DirectoryInfo(PlaylistsDirectory).GetFiles(string.Format("*{0}", PlaylistFile.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
            {
                PlaylistFile playlistFile;
                if (file.Length > 0 && PlaylistFile.TryParse(file.FullName, out playlistFile))
                {
                    tempList.Add(playlistFile);
                }
            }

            return tempList;
        }

        public List<TabFile> GetTabFiles()
        {
            var tempList = new List<TabFile>();

            foreach (var file in new DirectoryInfo(TabsDirectory).GetFiles(string.Format("*{0}", TabFile.FILE_EXTENSION), SearchOption.AllDirectories))
            {
                TabFile tabFile;
                if (file.Length > 0 && TabFile.TryParse(file.FullName, out tabFile))
                {
                    tempList.Add(tabFile);
                }
            }

            return tempList;
        }

        #endregion

        #region Tabs

        public void ImportTab(TabFile tabFile)
        {
            var uniquePath = GenerateUniqueFilename(LibraryDirectory, tabFile.FileInfo.Name);

            File.Copy(tabFile.FileInfo.FullName, uniquePath);

            var importedTab = new TabFile(uniquePath);
            AddTab(importedTab, true);
        }

        public void AddTab(TabFile tabFile, bool saveCache)
        {
            _tabs.Add(tabFile);

            if (saveCache)
                Save();

            if (OnTabAdded != null)
                OnTabAdded(this, EventArgs.Empty);
        }

        public bool RemoveTab(TabFile tabFile, bool diskDelete)
        {
            try
            {
                _tabs.Remove(tabFile);

                if (diskDelete)
                {
                    FileSystem.DeleteFile(tabFile.FileInfo.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }

                Save();

                if (OnTabRemoved != null)
                    OnTabRemoved(this, EventArgs.Empty);

                return true;
            }

            catch
            {
                return false;
            }
        }

        public TabFile FindTabByPath(string path)
        {
            return FindTab(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public TabFile FindTab(Predicate<TabFile> match)
        {
            foreach (var tab in _tabs)
            {
                if (match(tab))
                {
                    return tab;
                }
            }

            return null;
        }

        #endregion

        #region Playlists

        public PlaylistFile CreatePlaylist(string name)
        {
            var playlist = new Playlist(name);
            var uniqueName = GenerateUniqueFilename(PlaylistsDirectory, name + PlaylistFile.FILE_EXTENSION);
            var pf = new PlaylistFile(playlist, uniqueName);
            _playlists.Add(pf);
            return pf;
        }

        public void AddPlaylist(PlaylistFile p, bool saveCache)
        {
            _playlists.Add(p);

            if (saveCache)
                Save();
        }

        public void RemovePlaylist(PlaylistFile p)
        {
            _playlists.Remove(p);

            if (File.Exists(p.FileInfo.FullName))
            {
                File.Delete(p.FileInfo.FullName);
            }

            Save();
        }

        public PlaylistFile FindPlaylist(Predicate<PlaylistFile> match)
        {
            foreach (var playlist in _playlists)
            {
                if (match(playlist))
                {
                    return playlist;
                }
            }

            return null;
        }

        public PlaylistFile FindPlaylistByPath(string path)
        {
            return FindPlaylist(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public List<PlaylistFile> FindPlaylistsContaining(TabFile tabFile)
        {
            return _playlists.FindAll(x => x.PlaylistData.Contains(tabFile));
        }

        #endregion

        public bool TabsLoaded { get; private set; }
        public bool PlaylistsLoaded { get; private set; }

        public int TabCount
        {
            get { return _tabs.Count; }
        }

        public int PlaylistCount
        {
            get { return _playlists.Count; }
        }

        public event EventHandler OnTabsLoaded;
        public event EventHandler OnPlaylistsLoaded;

        public event EventHandler OnTabAdded;
        public event EventHandler OnTabRemoved;

        public void CleanupTempFiles()
        {
            try
            {
                var files = new DirectoryInfo(TemporaryDirectory);
                foreach (var f in files.GetFiles(string.Format("*.{0}", TabFile.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
                {
                    f.Delete();
                }
            }

            catch
            {
            }
        }

        public void Cleanup()
        {
            try
            {
                //organize contents based on their type.
                var globallisting = new DirectoryInfo(LibraryDirectory);

                //move tabs to tab directory
                foreach (var file in globallisting.GetFiles(string.Format("*{0}", TabFile.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
                {
                    file.MoveTo(TabsDirectory + file.Name);
                }

                //move playlists to playlist directory
                foreach (var file in globallisting.GetFiles(string.Format("*{0}", PlaylistFile.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
                {
                    file.MoveTo(PlaylistsDirectory + file.Name);
                }
            }

            catch
            {
            }
        }

        #region Implementation of IEnumerable

        public IEnumerator<TabFile> GetEnumerator()
        {
            foreach (var t in _tabs)
            {
                yield return t;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ITabsterFile

        public void Load()
        {
            if (!File.Exists(FileInfo.FullName))
            {
                Save(true);
            }

            BeginFileRead(new Version(FILE_VERSION));

            _tabPaths = ReadChildValues("tabs") ?? new List<string>();
            _playlistPaths = ReadChildValues("playlists") ?? new List<string>();

            if (FileFormatOutdated)
            {
                //todo update format    
            }

            if (!tabWorker.IsBusy)
                tabWorker.RunWorkerAsync();

            if (!playlistWorker.IsBusy)
                playlistWorker.RunWorkerAsync();
        }

        public void Save()
        {
            Save(false);
        }

        private void Save(bool useActiveFiles)
        {
            BeginFileWrite("library", FILE_VERSION);

            var tabsNode = WriteNode("tabs");

            var tabs = useActiveFiles ? GetTabFiles() : _tabs;

            foreach (var tab in tabs)
            {
                WriteNode("tab", tab.FileInfo.FullName, tabsNode, overwriteDuplicates: false);
            }

            var playlistsNode = WriteNode("playlists");

            var playlists = useActiveFiles ? GetPlaylistfiles() : _playlists;

            foreach (var playlist in playlists)
            {
                WriteNode("playlist", playlist.FileInfo.FullName, playlistsNode, overwriteDuplicates: false);
            }

            FinishFileWrite();
        }

        #endregion
    }
}