#region

using System;
using System.Collections;
using System.Collections.Generic;
using Tabster.Core;

#endregion

namespace Tabster
{
    public class Playlist : IEnumerable<TabFile>
    {
        private readonly List<TabFile> _tabs = new List<TabFile>();

        public Playlist(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public int Count
        {
            get { return _tabs.Count; }
        }

        public bool Contains(TabFile tabFile)
        {
            return Contains(tabFile.FileInfo.FullName);
        }

        public bool Contains(string filePath)
        {
            return _tabs.Find(t => t.FileInfo.FullName.Equals(filePath, StringComparison.InvariantCultureIgnoreCase)) != null;
        }

        public void Add(TabFile tabFile)
        {
            Remove(tabFile);
            _tabs.Add(tabFile);
        }

        public void Clear()
        {
            _tabs.Clear();
        }

        public void Remove(TabFile tabFile)
        {
            var existing = _tabs.Find(t => t.FileInfo.FullName.Equals(tabFile.FileInfo.FullName, StringComparison.InvariantCultureIgnoreCase));

            if (existing != null)
                _tabs.Remove(existing);
        }

        #region Implementation of IEnumerable

        public IEnumerator<TabFile> GetEnumerator()
        {
            foreach (var t in _tabs)
            {
                yield return t;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}