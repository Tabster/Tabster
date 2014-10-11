#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Tabster.Core.Utilities
{
    public class CustomTypeCollection<T> : ICollection<T> where T : class
    {
        private readonly List<T> _items = new List<T>();

        internal CustomTypeCollection()
        {
        }

        internal CustomTypeCollection(IEnumerable<T> items)
        {
            _items = new List<T>(items);
        }

        internal CustomTypeCollection(T item)
            : this(new[] {item})
        {
        }

        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) _items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<T>

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        public T this[int index]
        {
            get { return _items[index]; }
        }

        public bool Contains(T item, StringComparison comparison)
        {
            return _items.Find(x => Equals(item, comparison)) != null;
        }

        public T[] ToArray()
        {
            return _items.ToArray();
        }

        public void Reverse()
        {
            _items.Reverse();
        }
    }
}