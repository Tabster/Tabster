#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Tabster.Core.Types;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Data.Library
{
    public class TablatureFileLibrary<TTablatureFile, TTablaturePlaylistFile> : IEnumerable<TablatureLibraryItem>
        where TTablatureFile : class, ITablatureFile, new()
        where TTablaturePlaylistFile : class, ITablaturePlaylistFile, new()
    {
        public readonly TabsterFileProcessor<TTablatureFile> TablatureFileProcessor;
        public readonly TabsterFileProcessor<TTablaturePlaylistFile> TablaturePlaylistFileProcessor;
        private readonly List<ITablaturePlaylistFile> _playlists = new List<ITablaturePlaylistFile>();
        private readonly List<TablatureLibraryItem> _tablatureLibraryItems = new List<TablatureLibraryItem>();

        public TablatureFileLibrary(string libraryDirectory, string playlistDirectory,
            TabsterFileProcessor<TTablatureFile> tablatureFileProcessor, TabsterFileProcessor<TTablaturePlaylistFile> tablaturePlaylistFileProcessor)
        {
            LibraryDirectory = libraryDirectory;
            PlaylistDirectory = playlistDirectory;
            TablatureFileProcessor = tablatureFileProcessor;
            TablaturePlaylistFileProcessor = tablaturePlaylistFileProcessor;

            if (!Directory.Exists(LibraryDirectory))
                Directory.CreateDirectory(LibraryDirectory);

            if (!Directory.Exists(PlaylistDirectory))
                Directory.CreateDirectory(PlaylistDirectory);
        }

        public string LibraryDirectory { get; private set; }
        public string PlaylistDirectory { get; private set; }

        public ReadOnlyCollection<ITablaturePlaylistFile> Playlists
        {
            get { return _playlists.AsReadOnly(); }
        }

        #region Playlist Methods

        public virtual void Add(ITablaturePlaylistFile tablaturePlaylist)
        {
            _playlists.Add(tablaturePlaylist);
        }

        public virtual bool Remove(ITablaturePlaylistFile tablaturePlaylist)
        {
            return _playlists.Remove(tablaturePlaylist);
        }

        public virtual ITablaturePlaylistFile Find(Predicate<ITablaturePlaylistFile> match)
        {
            return _playlists.FirstOrDefault(playlist => match(playlist));
        }

        public virtual ITablaturePlaylistFile FindPlaylistByPath(string path)
        {
            return _playlists.Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        public void LoadTablatureFiles()
        {
            foreach (var file in Directory.GetFiles(LibraryDirectory, string.Format("*{0}", Constants.TablatureFileExtension),
                SearchOption.AllDirectories).Select(file => TablatureFileProcessor.Load(file)).Where(doc => doc != null).ToList())
            {
                Add(file);
            }
        }

        public virtual void Add(TablatureLibraryItem item)
        {
            _tablatureLibraryItems.Add(item);
        }

        public virtual TablatureLibraryItem Add(ITablatureFile file)
        {
            var item = new TablatureLibraryItem(file) {Added = DateTime.UtcNow};
            Add(item);
            return item;
        }

        public virtual TablatureLibraryItem Add(AttributedTablature tablature)
        {
            var file = new TTablatureFile();
            file.Artist = tablature.Artist;
            file.Title = tablature.Title;
            file.Type = tablature.Type;
            file.SourceType = tablature.SourceType;
            file.Source = tablature.Source;

            var fileName = GenerateUniqueFilename(LibraryDirectory, Path.Combine(file.ToFriendlyString(), Constants.TablatureFileExtension));
            file.Save(fileName);

            return Add(file);
        }

        public virtual bool Remove(TablatureLibraryItem item)
        {
            return _tablatureLibraryItems.Remove(item);
        }

        public virtual TablatureLibraryItem GetLibraryItem(ITablatureFile file)
        {
            return _tablatureLibraryItems.Find(x => x.File.Equals(file));
        }

        public virtual TablatureLibraryItem Find(Predicate<TablatureLibraryItem> match)
        {
            return _tablatureLibraryItems.Find(match);
        }

        public virtual List<TablatureLibraryItem> FindAll(Predicate<TablatureLibraryItem> match)
        {
            return _tablatureLibraryItems.FindAll(match);
        }

        public virtual TablatureLibraryItem FindByPath(string path)
        {
            return _tablatureLibraryItems.Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

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

        public IEnumerable<TTablaturePlaylistFile> LoadPlaylistFiles()
        {
            return Directory.GetFiles(PlaylistDirectory, string.Format("*{0}", Constants.TablaturePlaylistFileExtension), SearchOption.AllDirectories).Select(file => TablaturePlaylistFileProcessor.Load(file)).Where(doc => doc != null).ToList();
        }

        #region Implementation of IEnumerable

        public IEnumerator<TablatureLibraryItem> GetEnumerator()
        {
            return ((IEnumerable<TablatureLibraryItem>) _tablatureLibraryItems).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}