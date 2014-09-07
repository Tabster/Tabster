#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Tabster.Core.Data
{
    public sealed class FileExtensionCollection : ICollection<string>
    {
        private readonly List<string> _extensions = new List<string>();

        public FileExtensionCollection(IEnumerable<string> extensions)
        {
            _extensions = new List<string>(extensions);
        }

        public FileExtensionCollection(string extension)
            : this(new[] {extension})
        {
        }

        #region Implementation of IEnumerable

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var extension in _extensions)
            {
                yield return extension;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<string>

        public void Add(string extension)
        {
            _extensions.Add(extension);
        }

        public void Clear()
        {
            _extensions.Clear();
        }

        public bool Contains(string extension)
        {
            return _extensions.Contains(extension);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            _extensions.CopyTo(array, arrayIndex);
        }

        public bool Remove(string extension)
        {
            return _extensions.Remove(extension);
        }

        public int Count
        {
            get { return _extensions.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        public string this[int index]
        {
            get { return _extensions[index]; }
        }

        public bool Contains(string extension, StringComparison comparison)
        {
            return _extensions.Find(x => x.Equals(extension, comparison)) != null;
        }

        public string[] ToArray()
        {
            return _extensions.ToArray();
        }
    }
}