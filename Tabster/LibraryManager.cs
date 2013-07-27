#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using SearchOption = System.IO.SearchOption;

#endregion

namespace Tabster
{
    public class LibraryItem
    {
        public LibraryItem(TabFile file, int views = 0, bool favorited = false)
        {
            File = file;
            Views = views;
            Favorited = favorited;
        }

        public TabFile File { get; private set; }
        public int Views { get; private set; }
        public bool Favorited { get; set; }
    }

    public class LibraryManager : TabsterFile, ITabsterFile, IEnumerable<LibraryItem>
    {
        public const string FILE_VERSION = "1.0";
        private readonly List<LibraryItem> _items = new List<LibraryItem>();

        private readonly List<PlaylistFile> _playlists = new List<PlaylistFile>();
        private readonly List<TabFile> _tbs = new List<TabFile>();

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
        }

        #region Active Files

        public List<PlaylistFile> GetPlaylistFiles()
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

        public List<LibraryItem> GetTabFiles()
        {
            var tempList = new List<LibraryItem>();

            foreach (var file in new DirectoryInfo(TabsDirectory).GetFiles(string.Format("*{0}", TabFile.FILE_EXTENSION), SearchOption.AllDirectories))
            {
                TabFile tabFile;
                if (file.Length > 0 && TabFile.TryParse(file.FullName, out tabFile))
                {
                    tempList.Add(new LibraryItem(tabFile));
                }
            }

            return tempList;
        }

        #endregion

        #region Tabs

        public void AddTab(TabFile tabFile, bool saveCache)
        {
            _items.Add(new LibraryItem(tabFile));

            if (saveCache)
                Save();
        }

        public bool RemoveTab(TabFile tabFile, bool diskDelete)
        {
            try
            {
                _items.RemoveAll(x => x.File.FileInfo.FullName.Equals(tabFile.FileInfo.FullName, StringComparison.InvariantCultureIgnoreCase));

                if (diskDelete)
                {
                    FileSystem.DeleteFile(tabFile.FileInfo.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }

                Save();

                return true;
            }

            catch
            {
                return false;
            }
        }

        public LibraryItem FindTabByPath(string path)
        {
            return FindTab(x => x.File.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public LibraryItem FindTab(Predicate<LibraryItem> match)
        {
            foreach (var tab in _items)
            {
                if (match(tab))
                {
                    return tab;
                }
            }

            return null;
        }

        public LibraryItem FindTab(TabFile tab)
        {
            return _items.Find(x => ReferenceEquals(x.File, tab));
        }

        #endregion

        #region Playlists

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
                foreach (var tab in _items)
                    total += tab.File.FileInfo.Length;
                return total;
            }
        }

        public bool TabsLoaded { get; private set; }
        public bool PlaylistsLoaded { get; private set; }

        public int TabCount
        {
            get { return _items.Count; }
        }

        public int PlaylistCount
        {
            get { return _playlists.Count; }
        }

        public event EventHandler OnTabsLoaded;
        public event EventHandler OnPlaylistsLoaded;

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

        #region Implementation of IEnumerable

        public IEnumerator<LibraryItem> GetEnumerator()
        {
            foreach (var t in _items)
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

            _items.Clear();
            _playlists.Clear();
            TabsLoaded = false;
            PlaylistsLoaded = false;

            BeginFileRead(new Version(FILE_VERSION));

            var tabPaths = ReadChildNodes("tabs") ?? new List<ElementNode>();
            var playlistPaths = ReadChildValues("playlists") ?? new List<string>();

            if (FileFormatOutdated)
            {
                Save();
                Load();
                return;
            }

            foreach (var node in tabPaths)
            {
                if (File.Exists(node.Value))
                {
                    TabFile tabFile;

                    if (TabFile.TryParse(node.Value, out tabFile))
                    {
                        var favorited = node.Attributes.ContainsKey("favorite") && node.Attributes["favorite"] == "true";
                        _items.Add(new LibraryItem(tabFile, favorited: favorited));
                    }
                }
            }

            TabsLoaded = true;

            if (OnTabsLoaded != null)
                OnTabsLoaded(this, EventArgs.Empty);

            foreach (var file in playlistPaths)
            {
                if (File.Exists(file))
                {
                    PlaylistFile playlistFile;

                    if (PlaylistFile.TryParse(file, out playlistFile))
                    {
                        _playlists.Add(playlistFile);
                    }
                }
            }

            PlaylistsLoaded = true;

            if (OnPlaylistsLoaded != null)
                OnPlaylistsLoaded(this, EventArgs.Empty);
        }

        public void Save()
        {
            Save(false);
        }

        private void Save(bool useActiveFiles)
        {
            BeginFileWrite("library", FILE_VERSION);

            var tabsNode = WriteNode("tabs");

            var tabs = useActiveFiles ? GetTabFiles() : _items;

            foreach (var tab in tabs)
            {
                if (File.Exists(tab.File.FileInfo.FullName))
                {
                    WriteNode("tab",
                              tab.File.FileInfo.FullName,
                              tabsNode,
                              new SortedDictionary<string, string>
                                  {
                                      {"favorite", tab.Favorited ? "true" : "false"},
                                      {"views", tab.Views.ToString()}
                                  },
                              false);
                }
            }

            var playlistsNode = WriteNode("playlists");

            var playlists = useActiveFiles ? GetPlaylistFiles() : _playlists;

            foreach (var playlist in playlists)
            {
                if (File.Exists(playlist.FileInfo.FullName))
                {
                    WriteNode("playlist", playlist.FileInfo.FullName, playlistsNode, overwriteDuplicates: false);
                }
            }

            FinishFileWrite();
        }

        #endregion
    }
}