#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Tabster.Data
{
    public class TabsterDocumentCollection<T> : ICollection<T> where T : class, ITabsterDocument
    {
        private readonly List<T> _documents = new List<T>();

        #region Implementation of IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) _documents).GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<ITabsterDocument>

        public int Count
        {
            get { return _documents.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(T item)
        {
            Remove(item);
            _documents.Add(item);
        }

        public void Clear()
        {
            _documents.Clear();
        }

        public bool Contains(T item)
        {
            return _documents.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _documents.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _documents.Remove(item);
        }

        #endregion

        public bool Contains(string path)
        {
            return Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase)) != null;
        }

        public T Find(Predicate<T> match)
        {
            return _documents.Find(match);
        }

        public List<T> FindAll(Predicate<T> match)
        {
            return _documents.FindAll(match);
        }

        public T Find(string path)
        {
            return Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public bool Remove(string path)
        {
            var existing = _documents.Find(t => t.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
            return existing != null && _documents.Remove(existing);
        }
    }
}