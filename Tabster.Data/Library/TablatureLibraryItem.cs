#region

using System;
using System.IO;

#endregion

namespace Tabster.Data.Library
{
    public class TablatureLibraryItem : ITablatureLibraryItem
    {
        public TablatureLibraryItem(TablatureDocument doc)
        {
            Document = doc;
        }

        #region Implementation of LibraryEntryAttributes

        public FileInfo FileInfo
        {
            get { return Document.FileInfo; }
        }

        public bool Favorited { get; set; }

        public int Views { get; set; }

        public DateTime? LastViewed { get; set; }

        public DateTime Added { get; set; }

        public TablatureDocument Document { get; private set; }

        #endregion
    }
}