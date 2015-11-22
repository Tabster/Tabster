#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#endregion

namespace Tabster.Data
{
    public class TablaturePlaylistItem
    {
        public TablaturePlaylistItem(ITablatureFile file, FileInfo fileInfo)
        {
            File = file;
            FileInfo = fileInfo;
        }

        public ITablatureFile File { get; private set; }
        public FileInfo FileInfo { get; private set; }
    }

    public class TablaturePlaylist : IEnumerable<TablaturePlaylistItem>
    {
        private readonly List<TablaturePlaylistItem> _items = new List<TablaturePlaylistItem>();

        public TablaturePlaylist(string name)
        {
            Name = name;
        }

        #region Tablature Methods

        public void Add(TablaturePlaylistItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _items.Add(item);
        }

        public bool Remove(TablaturePlaylistItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            return _items.Remove(item);
        }

        public bool Remove(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            return Remove(Find(x => x.FileInfo.FullName.Equals(path)));
        }

        public TablaturePlaylistItem Find(Predicate<TablaturePlaylistItem> match)
        {
            if (match == null)
                throw new ArgumentNullException("match");

            return _items.Find(match);
        }

        public List<TablaturePlaylistItem> FindAll(Predicate<TablaturePlaylistItem> match)
        {
            if (match == null)
                throw new ArgumentNullException("match");

            return _items.FindAll(match);
        }

        public TablaturePlaylistItem Find(ITablatureFile file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            return _items.Find(x => x.File.Equals(file));
        }

        public TablaturePlaylistItem Find(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            return _items.Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public List<TablaturePlaylistItem> GetTablatureItems()
        {
            return new List<TablaturePlaylistItem>(_items);
        }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<TablaturePlaylistItem> GetEnumerator()
        {
            return ((IEnumerable<TablaturePlaylistItem>) _items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public string Name { get; set; }

        public long? ID { get; set; }

        public DateTime? Created { get; set; }

        public int Count
        {
            get { return _items.Count; }
        }
    }
}