#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Tabster.Core.Data;
using Tabster.Core.Data.Processing;
using Tabster.Core.Types;
using Tabster.Utilities;
using SearchOption = System.IO.SearchOption;

#endregion

namespace Tabster
{
    internal class LibraryAttributes
    {
        public bool Favorited { get; set; }
        public int Views { get; set; }
        public DateTime? LastViewed { get; set; }
    }

    internal class LibraryManager : TabsterDocumentCollection<TablatureDocument>
    {
        private static readonly Version INDEX_VERSION = new Version("1.0");

        private readonly Dictionary<TablatureDocument, LibraryAttributes> FileAttributes = new Dictionary<TablatureDocument, LibraryAttributes>();

        private readonly TabsterDocumentProcessor<TablatureDocument> _documentProcessor = new TabsterDocumentProcessor<TablatureDocument>(TablatureDocument.FILE_VERSION, true);
        private readonly TabsterXmlDocument _indexDoc = new TabsterXmlDocument("library");
        private readonly string _indexPath;
        private readonly TabsterDocumentProcessor<TablaturePlaylistDocument> _playlistProcessor = new TabsterDocumentProcessor<TablaturePlaylistDocument>(TablaturePlaylistDocument.FILE_VERSION, true);

        private readonly List<TablaturePlaylistDocument> _playlists = new List<TablaturePlaylistDocument>();

        public LibraryManager(string indexPath, string libraryDirectory, string playlistDirectory)
        {
            _indexPath = indexPath;

            LibraryDirectory = libraryDirectory;
            PlaylistDirectory = playlistDirectory;

            if (!Directory.Exists(LibraryDirectory))
                Directory.CreateDirectory(LibraryDirectory);

            if (!Directory.Exists(PlaylistDirectory))
                Directory.CreateDirectory(PlaylistDirectory);
        }

        public ReadOnlyCollection<TablaturePlaylistDocument> Playlists
        {
            get { return _playlists.AsReadOnly(); }
        }

        public string LibraryDirectory { get; private set; }
        public string PlaylistDirectory { get; private set; }

        #region Events

        public event EventHandler Loaded;
        public event EventHandler TabAdded;
        public event EventHandler TabRemoved;
        public event EventHandler PlaylistAdded;
        public event EventHandler PlaylistRemoved;

        #endregion

        public void Load()
        {
            Clear();
            _playlists.Clear();

            var loadFromCache = File.Exists(_indexPath);

            if (loadFromCache)
            {
                _indexDoc.Load(_indexPath);

                var tabNodes = _indexDoc.ReadChildNodes("tabs");

                if (tabNodes != null)
                {
                    foreach (var node in tabNodes)
                    {
                        if (File.Exists(node.InnerText))
                        {
                            var doc = _documentProcessor.Load(node.InnerText);

                            if (doc != null)
                            {
                                var attributes = node.Attributes;

                                if (attributes != null)
                                {
                                    var favorited = attributes["favorite"] != null && attributes["favorite"].Value == "true";

                                    var views = 0;
                                    if (attributes["views"] != null)
                                        int.TryParse(attributes["views"].Value, out views);

                                    DateTime? lastViewed = null;
                                    DateTime dt;
                                    if (attributes["last_viewed"] != null && DateTime.TryParse(attributes["last_viewed"].Value, out dt))
                                        lastViewed = dt;

                                    var libraryAttributes = new LibraryAttributes {Favorited = favorited, Views = views, LastViewed = lastViewed};

                                    Add(doc);

                                    FileAttributes[doc] = libraryAttributes;
                                }
                            }
                        }
                    }
                }

                var playlistPaths = _indexDoc.ReadChildNodeValues("playlists");

                if (playlistPaths != null)
                {
                    foreach (var file in playlistPaths)
                    {
                        if (File.Exists(file))
                        {
                            var playlist = _playlistProcessor.Load(file);

                            if (playlist != null)
                            {
                                _playlists.Add(playlist);
                            }
                        }
                    }
                }
            }

            else
            {
                foreach (var doc in LoadTablatureDocumentsFromDisk())
                {
                    Add(doc);
                }

                foreach (var playlist in LoadTablaturePlaylistsFromDisk())
                {
                    _playlists.Add(playlist);
                }

                Save();
            }

            if (Loaded != null)
                Loaded(this, EventArgs.Empty);
        }

        public void Save()
        {
            _indexDoc.Version = INDEX_VERSION;

            _indexDoc.WriteNode("tabs");

            foreach (var tab in this)
            {
                if (File.Exists(tab.FileInfo.FullName))
                {
                    var attributes = GetLibraryAttributes(tab);

                    _indexDoc.WriteNode("tab",
                                        tab.FileInfo.FullName,
                                        "tabs",
                                        new SortedDictionary<string, string>
                                            {
                                                {"favorite", attributes.Favorited ? "true" : "false"},
                                                {"views", attributes.Views.ToString()},
                                                {"last_viewed", attributes.LastViewed == null ? string.Empty : attributes.LastViewed.Value.ToString()}
                                            }, false);
                }
            }

            _indexDoc.WriteNode("playlists");

            foreach (var playlist in _playlists)
            {
                if (File.Exists(playlist.FileInfo.FullName))
                {
                    _indexDoc.WriteNode("playlist", playlist.FileInfo.FullName, "playlists", preventNodeDuplication: false);
                }
            }

            _indexDoc.Save(_indexPath);
        }

        public LibraryAttributes GetLibraryAttributes(TablatureDocument doc)
        {
            return FileAttributes.ContainsKey(doc) ? FileAttributes[doc] : new LibraryAttributes();
        }

        public void SetViewCount(TablatureDocument doc, int count)
        {
            var att = GetLibraryAttributes(doc);

            if (att != null)
                att.Views = count;
        }

        public void IncrementViewCount(TablatureDocument doc)
        {
            var att = GetLibraryAttributes(doc);

            if (att != null)
                att.Views += 1;
        }

        public void SetLastViewed(TablatureDocument doc, DateTime date)
        {
            var att = GetLibraryAttributes(doc);

            if (att != null)
                att.LastViewed = date;
        }

        #region Tablature Methods

        private IEnumerable<TablatureDocument> LoadTablatureDocumentsFromDisk()
        {
            var tempList = new List<TablatureDocument>();

            foreach (var file in Directory.GetFiles(LibraryDirectory, string.Format("*{0}", TablatureDocument.FILE_EXTENSION), SearchOption.AllDirectories))
            {
                var doc = _documentProcessor.Load(file);

                if (doc != null)
                {
                    tempList.Add(doc);
                }
            }

            return tempList;
        }

        public void Add(TablatureDocument doc, bool saveIndex = false)
        {
            if (doc.FileInfo == null)
            {
                var uniqueName = doc.GenerateUniqueFilename(LibraryDirectory);
                doc.Save(uniqueName);
            }

            base.Add(doc);

            if (saveIndex)
                Save();

            if (TabAdded != null)
                TabAdded(this, EventArgs.Empty);
        }

        public bool Remove(TablatureDocument doc, bool diskDelete, bool saveIndex = false)
        {
            var result = false;

            try
            {
                var success = Remove(doc.FileInfo.FullName);

                if (success)
                {
                    FileAttributes.Remove(doc);

                    if (File.Exists(doc.FileInfo.FullName) && diskDelete)
                    {
                        FileSystem.DeleteFile(doc.FileInfo.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    }

                    if (saveIndex)
                        Save();

                    result = true;
                }
            }

            catch
            {
                //unhandled
            }

            if (TabRemoved != null)
                TabRemoved(this, EventArgs.Empty);

            return result;
        }

        #endregion

        #region Playlist Methods

        private IEnumerable<TablaturePlaylistDocument> LoadTablaturePlaylistsFromDisk()
        {
            var tempList = new List<TablaturePlaylistDocument>();

            foreach (var file in Directory.GetFiles(PlaylistDirectory, string.Format("*{0}", TablaturePlaylistDocument.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
            {
                var doc = _playlistProcessor.Load(file);

                if (doc != null)
                {
                    tempList.Add(doc);
                }
            }

            return tempList;
        }

        public void Add(TablaturePlaylistDocument playlist)
        {
            if (playlist.FileInfo == null)
            {
                var uniqueName = playlist.GenerateUniqueFilename(PlaylistDirectory);
                playlist.Save(uniqueName);
            }

            _playlists.Add(playlist);

            Save();

            if (PlaylistAdded != null)
                PlaylistAdded(this, EventArgs.Empty);
        }

        public bool Remove(TablaturePlaylistDocument playlist, bool diskDelete)
        {
            var result = _playlists.Remove(playlist);

            if (result && diskDelete && File.Exists(playlist.FileInfo.FullName))
                File.Delete(playlist.FileInfo.FullName);

            Save();

            if (PlaylistRemoved != null)
                PlaylistRemoved(this, EventArgs.Empty);

            return result;
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
            return FindPlaylist(x => x.FileInfo.FullName.Equals(path, StringComparison.InvariantCultureIgnoreCase));
        }

        #endregion
    }
}