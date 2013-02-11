#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#endregion

namespace Tabster
{
    public class PlaylistFileCollection : IEnumerable<PlaylistFile>
    {
        private readonly List<PlaylistFile> _playlists = new List<PlaylistFile>();

        public event EventHandler OnPlaylistAdded;
        public event EventHandler OnPlaylistRemoved;

        public int Count
        {
            get { return _playlists.Count; }
        }

        public PlaylistFile Find(Predicate<PlaylistFile> match)
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

        public PlaylistFile FindByPath(string path)
        {
            return Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public List<PlaylistFile> FindPlaylistsContaining(TabFile tabFile)
        {
            return _playlists.FindAll(x => x.PlaylistData.Contains(tabFile));
        }

        public void Add(PlaylistFile p)
        {
            _playlists.Add(p);

            if (OnPlaylistAdded != null)
                OnPlaylistAdded(this, EventArgs.Empty);
        }

        public void Remove(PlaylistFile p)
        {
            _playlists.Remove(p);

            if (File.Exists(p.FileInfo.FullName))
            {
                File.Delete(p.FileInfo.FullName);
            }

            if (OnPlaylistRemoved != null)
                OnPlaylistRemoved(this, EventArgs.Empty);
        }

        public void Clear()
        {
            _playlists.Clear();
        }

        public void Load()
        {
            _playlists.Clear();
        }

        #region Implementation of IEnumerable

        public IEnumerator<PlaylistFile> GetEnumerator()
        {
            foreach (var p in _playlists)
            {
                yield return p;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}