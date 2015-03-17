#region

using System;
using System.Collections.Generic;

#endregion

namespace Tabster.Data.Library
{
    public class TabsterLibraryBase<TTablatureFile, TTablaturePlaylistFile> 
        where TTablatureFile : class, ITablatureFile where TTablaturePlaylistFile : ITablaturePlaylistFile
    {
        private readonly List<PlaylistLibraryItem<TTablaturePlaylistFile>> _playlistLibraryItems = new List<PlaylistLibraryItem<TTablaturePlaylistFile>>();
        private readonly List<TablatureLibraryItem<TTablatureFile>> _tablatureLibraryItems = new List<TablatureLibraryItem<TTablatureFile>>();

        #region Playlist Methods

        public virtual void AddPlaylistItem(PlaylistLibraryItem<TTablaturePlaylistFile> item)
        {
            _playlistLibraryItems.Add(item);
        }

        public virtual bool RemovePlaylistItem(PlaylistLibraryItem<TTablaturePlaylistFile> item)
        {
            return _playlistLibraryItems.Remove(item);
        }

        public virtual PlaylistLibraryItem<TTablaturePlaylistFile> FindPlaylistItem(Predicate<PlaylistLibraryItem<TTablaturePlaylistFile>> match)
        {
            return _playlistLibraryItems.Find(match);
        }

        public List<PlaylistLibraryItem<TTablaturePlaylistFile>> FindAllPlaylistItems(Predicate<PlaylistLibraryItem<TTablaturePlaylistFile>> match)
        {
            return _playlistLibraryItems.FindAll(match);
        }

        public virtual PlaylistLibraryItem<TTablaturePlaylistFile> FindPlaylistItemsByFile(TTablaturePlaylistFile file)
        {
            return _playlistLibraryItems.Find(x => x.File.Equals(file));
        }

        public virtual PlaylistLibraryItem<TTablaturePlaylistFile> FindPlaylistItemByPath(string path)
        {
            return _playlistLibraryItems.Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public virtual List<PlaylistLibraryItem<TTablaturePlaylistFile>> GetPlaylistItems()
        {
            return new List<PlaylistLibraryItem<TTablaturePlaylistFile>>(_playlistLibraryItems);
        }

        #endregion

        #region Tablature Methods

        public virtual void AddTablatureItem(TablatureLibraryItem<TTablatureFile> item)
        {
            _tablatureLibraryItems.Add(item);
        }

        public virtual bool RemoveTablatureItem(TablatureLibraryItem<TTablatureFile> item)
        {
            return _tablatureLibraryItems.Remove(item);
        }

        public virtual TablatureLibraryItem<TTablatureFile> FindTablatureItem(Predicate<TablatureLibraryItem<TTablatureFile>> match)
        {
            return _tablatureLibraryItems.Find(match);
        }

        public List<TablatureLibraryItem<TTablatureFile>> FindAllTablatureItems(Predicate<TablatureLibraryItem<TTablatureFile>> match)
        {
            return _tablatureLibraryItems.FindAll(match);
        }

        public virtual TablatureLibraryItem<TTablatureFile> FindTablatureItemByFile(TTablatureFile file)
        {
            return _tablatureLibraryItems.Find(x => x.File.Equals(file));
        }

        public virtual TablatureLibraryItem<TTablatureFile> FindTablatureItemByPath(string path)
        {
            return _tablatureLibraryItems.Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public virtual List<TablatureLibraryItem<TTablatureFile>> GetTablatureItems()
        {
            return new List<TablatureLibraryItem<TTablatureFile>>(_tablatureLibraryItems);
        }

        #endregion
    }
}