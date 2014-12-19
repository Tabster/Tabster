#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using Tabster.Data.Processing;
using SearchOption = System.IO.SearchOption;

#endregion

namespace Tabster.Data.Library
{
    public class TablatureFileLibrary : ITablatureFileLibrary
    {
        private readonly TabsterDocumentProcessor<TablatureDocument> _documentProcessor = new TabsterDocumentProcessor<TablatureDocument>(TablatureDocument.FILE_VERSION, true);
        private readonly TablatureLibraryIndexFile _indexFile;
        private readonly string _indexPath;
        private readonly TabsterDocumentProcessor<TablaturePlaylistDocument> _playlistProcessor = new TabsterDocumentProcessor<TablaturePlaylistDocument>(TablaturePlaylistDocument.FILE_VERSION, true);
        private List<TablatureLibraryItem> _TablatureLibraryItems = new List<TablatureLibraryItem>();

        private List<TablaturePlaylistDocument> _playlists = new List<TablaturePlaylistDocument>();

        public TablatureFileLibrary(string indexPath, string libraryDirectory, string playlistDirectory)
        {
            _indexFile = new TablatureLibraryIndexFile(this);
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

        #region Index File

        public void Load()
        {
            if (File.Exists(_indexPath))
            {
                _indexFile.Load(_indexPath);
                _TablatureLibraryItems = new List<TablatureLibraryItem>(_indexFile.LibraryItems);
                _playlists = new List<TablaturePlaylistDocument>(_indexFile.Playlists);
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
        }

        public void Save()
        {
            _indexFile.Save(_indexPath);
        }

        #endregion

        #region Tablature Methods

        private IEnumerable<TablatureDocument> LoadTablatureDocumentsFromDisk()
        {
            return Directory.GetFiles(LibraryDirectory, string.Format("*{0}", TablatureDocument.FILE_EXTENSION), SearchOption.AllDirectories).Select(file => _documentProcessor.Load(file)).Where(doc => doc != null).ToList();
        }

        public bool Remove(TablatureLibraryItem item, bool diskDelete, bool saveIndex = false)
        {
            var result = false;
            try
            {
                var success = _TablatureLibraryItems.Remove(item);

                if (success)
                {
                    if (File.Exists(item.FileInfo.FullName) && diskDelete)
                    {
                        FileSystem.DeleteFile(item.FileInfo.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
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

            return result;
        }

        #endregion

        #region Playlist Methods

        private IEnumerable<TablaturePlaylistDocument> LoadTablaturePlaylistsFromDisk()
        {
            return Directory.GetFiles(PlaylistDirectory, string.Format("*{0}", TablaturePlaylistDocument.FILE_EXTENSION), SearchOption.TopDirectoryOnly).Select(file => _playlistProcessor.Load(file)).Where(doc => doc != null).ToList();
        }

        public void Add(TablaturePlaylistDocument playlist)
        {
            if (playlist.FileInfo == null)
            {
                var uniqueName = GenerateUniqueFilename(PlaylistDirectory, playlist.Name + TablaturePlaylistDocument.FILE_EXTENSION);
                playlist.Save(uniqueName);
            }

            _playlists.Add(playlist);

            Save();
        }

        public bool Remove(TablaturePlaylistDocument playlist, bool diskDelete)
        {
            var result = _playlists.Remove(playlist);

            if (result && diskDelete && File.Exists(playlist.FileInfo.FullName))
                File.Delete(playlist.FileInfo.FullName);

            Save();

            return result;
        }

        public TablaturePlaylistDocument FindPlaylist(Predicate<TablaturePlaylistDocument> match)
        {
            return _playlists.FirstOrDefault(playlist => match(playlist));
        }

        public TablaturePlaylistDocument FindPlaylistByPath(string path)
        {
            return FindPlaylist(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Implementation of ITablatureLibrary

        public int TotalItems
        {
            get { return _TablatureLibraryItems.Count; }
        }

        public void Add(TablatureLibraryItem item)
        {
            _TablatureLibraryItems.Add(item);
        }

        public TablatureLibraryItem Add(TablatureDocument doc)
        {
            if (doc.FileInfo == null)
            {
                var filename = string.Format("{0} - {1} ({2})", doc.Artist, doc.Title, doc.Type.ToFriendlyString());
                var uniqueName = GenerateUniqueFilename(LibraryDirectory, filename + TablatureDocument.FILE_EXTENSION);

                doc.Save(uniqueName);
            }

            var item = new TablatureLibraryItem(doc) { Added = DateTime.UtcNow };
            Add(item);

            return item;
        }

        public bool Remove(TablatureLibraryItem item)
        {
            return _TablatureLibraryItems.Remove(item);
        }

        public TablatureLibraryItem GetLibraryItem(TablatureDocument doc)
        {
            return _TablatureLibraryItems.Find(x => x.Document.Equals(doc));
        }

        public TablatureLibraryItem Find(Predicate<TablatureLibraryItem> match)
        {
            return _TablatureLibraryItems.Find(match);
        }

        public List<TablatureLibraryItem> FindAll(Predicate<TablatureLibraryItem> match)
        {
            return _TablatureLibraryItems.FindAll(match);
        }

        public TablatureLibraryItem Find(string path)
        {
            return _TablatureLibraryItems.Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<TablatureLibraryItem> GetEnumerator()
        {
            return ((IEnumerable<TablatureLibraryItem>) _TablatureLibraryItems).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private static string GenerateUniqueFilename(string directory, string filename)
        {
            //remove invalid file path characters
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var sanitized = new Regex(String.Format("[{0}]", Regex.Escape(regexSearch))).Replace(filename, "");

            var fileName = Path.GetFileNameWithoutExtension(sanitized);
            var fileExt = Path.GetExtension(sanitized);

            var firstTry = Path.Combine(directory, String.Format("{0}{1}", fileName, fileExt));
            if (!File.Exists(firstTry))
                return firstTry;

            for (var i = 1;; ++i)
            {
                var appendedPath = Path.Combine(directory, String.Format("{0} ({1}){2}", fileName, i, fileExt));

                if (!File.Exists(appendedPath))
                    return appendedPath;
            }
        }
    }
}