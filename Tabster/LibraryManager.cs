#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using Microsoft.VisualBasic.FileIO;
using Tabster.Core.FileTypes;
using Tabster.Utilities;
using SearchOption = System.IO.SearchOption;

#endregion

namespace Tabster
{
    public class LibraryItem
    {
        public LibraryItem(TablatureDocument doc, int views = 0, bool favorited = false)
        {
            Document = doc;
            Views = views;
            Favorited = favorited;
        }

        public TablatureDocument Document { get; private set; }
        public int Views { get; private set; }
        public bool Favorited { get; set; }
    }

    public class LibraryManager : ITabsterDocument, IEnumerable<LibraryItem>
    {
        private static readonly Version FILE_VERSION = new Version("1.0");

        private readonly TabsterXmlDocument _doc = new TabsterXmlDocument("library");
        private readonly string _indexPath;

        private List<LibraryItem> _items = new List<LibraryItem>();
        private List<TablaturePlaylistDocument> _playlists = new List<TablaturePlaylistDocument>();

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

            _indexPath = Path.Combine(ApplicationDirectory, "library.dat");
        }

        #region Active Files

        public List<TablaturePlaylistDocument> GetTablaturePlaylistDocuments()
        {
            var tempList = new List<TablaturePlaylistDocument>();

            var processor = new TabsterDocumentProcessor<TablaturePlaylistDocument>(TablaturePlaylistDocument.FILE_VERSION, true);

            foreach (var file in Directory.GetFiles(PlaylistsDirectory, string.Format("*{0}", TablaturePlaylistDocument.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
            {
                var playlist = processor.Load(file);

                if (playlist != null)
                {
                    tempList.Add(playlist);
                }
            }

            return tempList;
        }

        public List<LibraryItem> GetTablatureDocuments()
        {
            var tempList = new List<LibraryItem>();

            var processor = new TabsterDocumentProcessor<TablatureDocument>(TablatureDocument.FILE_VERSION, true);

            foreach (var file in Directory.GetFiles(TabsDirectory, string.Format("*{0}", TablatureDocument.FILE_EXTENSION), SearchOption.AllDirectories))
            {
                var doc = processor.Load(file);

                if (doc != null)
                {
                    tempList.Add(new LibraryItem(doc));
                }
            }

            return tempList;
        }

        #endregion

        #region Tabs

        public void AddTab(TablatureDocument doc, bool saveCache)
        {
            if (doc.FileInfo == null)
            {
                var uniqueName = doc.GenerateUniqueFilename(TabsDirectory);
                doc.Save(uniqueName);
            }

            _items.Add(new LibraryItem(doc));

            if (saveCache)
                Save(_indexPath);
        }

        public bool RemoveTab(TablatureDocument doc, bool diskDelete)
        {
            try
            {
                _items.RemoveAll(x => x.Document.FileInfo.FullName.Equals(doc.FileInfo.FullName, StringComparison.InvariantCultureIgnoreCase));

                if (diskDelete)
                {
                    FileSystem.DeleteFile(doc.FileInfo.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }

                Save(_indexPath);

                return true;
            }

            catch
            {
                return false;
            }
        }

        public LibraryItem FindTabByPath(string path)
        {
            return FindTab(x => x.Document.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
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

        public LibraryItem FindTab(TablatureDocument tab)
        {
            return _items.Find(x => ReferenceEquals(x.Document, tab));
        }

        #endregion

        #region Playlists

        public void AddPlaylist(TablaturePlaylistDocument doc, bool saveCache)
        {
            if (doc.FileInfo == null)
            {
                var uniqueName = doc.GenerateUniqueFilename(PlaylistsDirectory);
                doc.Save(uniqueName);
            }

            _playlists.Add(doc);

            if (saveCache)
                Save(_indexPath);
        }

        public void RemovePlaylist(TablaturePlaylistDocument p)
        {
            _playlists.Remove(p);

            if (File.Exists(p.FileInfo.FullName))
            {
                File.Delete(p.FileInfo.FullName);
            }

            Save(_indexPath);
        }

        public TablaturePlaylistDocument FindPlaylist(Predicate<TablaturePlaylistDocument> match)
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

        public TablaturePlaylistDocument FindPlaylistByPath(string path)
        {
            return FindPlaylist(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public List<TablaturePlaylistDocument> FindPlaylistsContaining(TablatureDocument doc)
        {
            return _playlists.FindAll(x => x.Contains(doc));
        }

        #endregion

        public string LibraryDirectory { get; private set; }
        public string TabsDirectory { get; private set; }
        public string PlaylistsDirectory { get; private set; }
        public string ApplicationDirectory { get; private set; }
        public string TemporaryDirectory { get; private set; }

        public ReadOnlyCollection<TablaturePlaylistDocument> Playlists
        {
            get { return _playlists.AsReadOnly(); }
        }

        public long DiskUsage
        {
            get
            {
                long total = 0;
                foreach (var tab in _items)
                    total += tab.Document.FileInfo.Length;
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
                foreach (var f in files.GetFiles(string.Format("*.{0}", TablatureDocument.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
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

        public Version FileVersion { get; set; }

        public FileInfo FileInfo { get; set; }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Load(string filename = null)
        {
            if (!File.Exists(_indexPath))
            {
                _items = GetTablatureDocuments();
                _playlists = GetTablaturePlaylistDocuments();
            }

            FileInfo = new FileInfo(_indexPath);

            _items.Clear();
            _playlists.Clear();
            TabsLoaded = false;
            PlaylistsLoaded = false;

            _doc.Load(FileInfo.FullName);

            var tabNodes = _doc.ReadChildNodes("tabs") ?? new List<XmlNode>();
            var playlistPaths = _doc.ReadChildNodeValues("playlists") ?? new List<string>();

            var tabProcessor = new TabsterDocumentProcessor<TablatureDocument>(FILE_VERSION, true);

            foreach (var node in tabNodes)
            {
                if (File.Exists(node.InnerText))
                {
                    var doc = tabProcessor.Load(node.InnerText);

                    if (doc != null)
                    {
                        var favorited = node.Attributes["favorite"] != null && node.Attributes["favorite"].Value == "true";
                        _items.Add(new LibraryItem(doc, favorited: favorited));
                    }
                }
            }

            TabsLoaded = true;

            if (OnTabsLoaded != null)
                OnTabsLoaded(this, EventArgs.Empty);

            var playlistProcessor = new TabsterDocumentProcessor<TablaturePlaylistDocument>(TablaturePlaylistDocument.FILE_VERSION, true);

            foreach (var file in playlistPaths)
            {
                if (File.Exists(file))
                {
                    var playlist = playlistProcessor.Load(file);

                    if (playlist != null)
                    {
                        _playlists.Add(playlist);
                    }
                }
            }

            PlaylistsLoaded = true;

            if (OnPlaylistsLoaded != null)
                OnPlaylistsLoaded(this, EventArgs.Empty);
        }

        public void Save()
        {
            Save(FileInfo.FullName);
        }

        public void Save(string fileName)
        {
            _doc.Version = FILE_VERSION;

            _doc.WriteNode("tabs");

            foreach (var tab in _items)
            {
                if (File.Exists(tab.Document.FileInfo.FullName))
                {
                    _doc.WriteNode("tab",
                                   tab.Document.FileInfo.FullName,
                                   "tabs",
                                   new SortedDictionary<string, string>
                                       {
                                           {"favorite", tab.Favorited ? "true" : "false"},
                                           {"views", tab.Views.ToString()}
                                       });
                }
            }

            _doc.WriteNode("playlists");

            foreach (var playlist in _playlists)
            {
                if (File.Exists(playlist.FileInfo.FullName))
                {
                    _doc.WriteNode("playlist", playlist.FileInfo.FullName, "playlists");
                }
            }

            _doc.Save(fileName);
        }

        #endregion
    }
}