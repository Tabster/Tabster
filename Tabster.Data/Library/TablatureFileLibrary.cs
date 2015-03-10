#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Tabster.Core.Types;
using Tabster.Data.Binary;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Data.Library
{
    public class TablatureFileLibrary : IEnumerable<TablatureLibraryItem>
    {
        public readonly TabsterFileProcessor<TablatureFile> TablatureFileProcessor;
        public readonly TabsterFileProcessor<TablaturePlaylistFile> TablaturePlaylistFileProcessor;
        private readonly List<ITablaturePlaylistFile> _playlists = new List<ITablaturePlaylistFile>();
        private readonly List<TablatureLibraryItem> _tablatureLibraryItems = new List<TablatureLibraryItem>();

        public TablatureFileLibrary(string libraryDirectory, string playlistDirectory, TabsterFileProcessor<TablatureFile> tablatureFileProcessor, TabsterFileProcessor<TablaturePlaylistFile> tablaturePlaylistFileProcessor)
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

        public void Load()
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
            //todo create file
            return null;
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

        public void Save()
        {
            //todo remove this
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