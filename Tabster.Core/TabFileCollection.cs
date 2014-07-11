#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Tabster.Core
{
    public class TabFileCollection : ICollection<TabFile>
    {
        private readonly List<TabFile> _tabFiles = new List<TabFile>();

        #region Implementation of IEnumerable

        public IEnumerator<TabFile> GetEnumerator()
        {
            foreach (var tab in _tabFiles)
            {
                yield return tab;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<TabFile>

        public void Add(TabFile item)
        {
            Remove(item);
            _tabFiles.Add(item);
        }

        public void Clear()
        {
            _tabFiles.Clear();
        }

        public bool Contains(TabFile item)
        {
            return _tabFiles.Contains(item);
        }

        public void CopyTo(TabFile[] array, int arrayIndex)
        {
            _tabFiles.CopyTo(array, arrayIndex);
        }

        public bool Remove(TabFile item)
        {
            return _tabFiles.Remove(item);
        }

        public int Count
        {
            get { return _tabFiles.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        public bool ContainsPath(string path)
        {
            return Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase)) != null;
        }

        public TabFile Find(Predicate<TabFile> match)
        {
            return _tabFiles.Find(match);
        }

        public List<TabFile> FindAll(Predicate<TabFile> match)
        {
            return _tabFiles.FindAll(match);
        }

        public TabFile FindByPath(string path)
        {
            return Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public bool RemoveByPath(string path)
        {
            var existing = _tabFiles.Find(t => t.FileInfo.FullName.Equals(path, StringComparison.InvariantCultureIgnoreCase));
            return existing != null && _tabFiles.Remove(existing);
        }
    }
}