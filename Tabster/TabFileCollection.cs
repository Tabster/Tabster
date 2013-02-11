#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using SearchOption = System.IO.SearchOption;

#endregion

namespace Tabster
{
    public class TabFileCollection : IEnumerable<TabFile>
    {
        private readonly List<TabFile> _tabs = new List<TabFile>();

        public int BassTabs, DrumTabs, GuitarChords, GuitarTabs;

        public event EventHandler OnTabAdded;
        public event EventHandler OnTabRemoved;

        public long DiskSpace { get; private set; }

        public int Count
        {
            get { return _tabs.Count; }
        }

        public TabFileCollection()
        {
            DiskSpace = 0;
        }

        public TabFile FindTab(Predicate<TabFile> match)
        {
            foreach (var tab in _tabs)
            {
                if (match(tab))
                {
                    return tab;
                }
            }

            return null;
        }

        public TabFile FindTabByPath(string path)
        {
            return FindTab(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public List<TabFile> FindAllTabs(Predicate<TabFile> match)
        {
            var tempList = new List<TabFile>();

            foreach (var tabFile in _tabs)
            {
                if (match(tabFile))
                {
                    tempList.Add(tabFile);
                }
            }

            return tempList;
        }

        public void ImportTab(TabFile tabFile)
        {
            var uniquePath = Global.GenerateUniqueFilename(Global.LibraryDirectory, tabFile.FileInfo.Name);

            File.Copy(tabFile.FileInfo.FullName, uniquePath);

            var importedTab = new TabFile(uniquePath);
            Add(importedTab);
        }

        public bool Remove(TabFile tabFile, bool diskDelete)
        {
            try
            {
                _tabs.Remove(tabFile);

                DiskSpace -= tabFile.FileInfo.Length;

                if (diskDelete)
                {
                    FileSystem.DeleteFile(tabFile.FileInfo.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }

                if (OnTabRemoved != null)
                    OnTabRemoved(this, EventArgs.Empty);

                return true;
            }

            catch
            {
                return false;
            }
        }

        public void Clear()
        {
            _tabs.Clear();

            DiskSpace = 0;
            GuitarTabs = 0;
            GuitarChords = 0;
            BassTabs = 0;
            DrumTabs = 0;
        }

        public void Add(TabFile tabFile)
        {
            _tabs.Add(tabFile);

            DiskSpace += tabFile.FileInfo.Length;

            switch (tabFile.TabData.Type)
            {
                case TabType.Guitar:
                    GuitarTabs++;
                    break;
                case TabType.Chord:
                    GuitarChords++;
                    break;
                case TabType.Bass:
                    BassTabs++;
                    break;
                case TabType.Drum:
                    DrumTabs++;
                    break;
            }

            if (OnTabAdded != null)
                OnTabAdded(this, EventArgs.Empty);
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