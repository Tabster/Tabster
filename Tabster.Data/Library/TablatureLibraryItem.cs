#region

using System;
using System.IO;
using Tabster.Core.Types;

#endregion

namespace Tabster.Data.Library
{
    public class TablatureLibraryItem : ITablatureLibraryItem
    {
        private TablatureDocument _document;

        public TablatureLibraryItem(FileInfo fileInfo, string artist, string title, TablatureType type)
        {
            FileInfo = fileInfo;
            Artist = artist;
            Title = title;
            Type = type;
        }

        public TablatureLibraryItem(TablatureDocument doc)
            : this(doc.FileInfo, doc.Artist, doc.Title, doc.Type)
        {
            _document = doc;
        }

        #region Implementation of ITablatureAttributes

        public string Artist { get; set; }

        public string Title { get; set; }

        public TablatureType Type { get; set; }

        #endregion

        #region Implementation of LibraryEntryAttributes

        public FileInfo FileInfo { get; private set; }

        public bool Favorited { get; set; }

        public int Views { get; set; }

        public DateTime? LastViewed { get; set; }

        public DateTime Added { get; set; }

        #endregion

        public TablatureDocument Document
        {
            get
            {
                if (_document == null)
                {
                    _document = new TablatureDocument();
                    _document.Load(FileInfo.FullName);
                }

                return _document;
            }
        }

        public void IncrementViewcount()
        {
            Views++;
        }
    }
}