#region

using System;
using System.Collections;
using System.Collections.Generic;
using Tabster.Core.FileTypes;

#endregion

namespace Tabster.Core
{
    public class TablatureDocumentCollection : ICollection<TablatureDocument>
    {
        private readonly List<TablatureDocument> _TablatureDocuments = new List<TablatureDocument>();

        #region Implementation of IEnumerable

        public IEnumerator<TablatureDocument> GetEnumerator()
        {
            foreach (var tab in _TablatureDocuments)
            {
                yield return tab;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<TablatureDocument>

        public void Add(TablatureDocument item)
        {
            Remove(item);
            _TablatureDocuments.Add(item);
        }

        public void Clear()
        {
            _TablatureDocuments.Clear();
        }

        public bool Contains(TablatureDocument item)
        {
            return _TablatureDocuments.Contains(item);
        }

        public void CopyTo(TablatureDocument[] array, int arrayIndex)
        {
            _TablatureDocuments.CopyTo(array, arrayIndex);
        }

        public bool Remove(TablatureDocument item)
        {
            return _TablatureDocuments.Remove(item);
        }

        public int Count
        {
            get { return _TablatureDocuments.Count; }
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

        public TablatureDocument Find(Predicate<TablatureDocument> match)
        {
            return _TablatureDocuments.Find(match);
        }

        public List<TablatureDocument> FindAll(Predicate<TablatureDocument> match)
        {
            return _TablatureDocuments.FindAll(match);
        }

        public TablatureDocument FindByPath(string path)
        {
            return Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public bool RemoveByPath(string path)
        {
            var existing = _TablatureDocuments.Find(t => t.FileInfo.FullName.Equals(path, StringComparison.InvariantCultureIgnoreCase));
            return existing != null && _TablatureDocuments.Remove(existing);
        }
    }
}