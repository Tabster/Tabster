#region

using System;
using System.IO;
using Tabster.Core.Data;
using Tabster.Core.Types;

#endregion

namespace Tabster.Library
{
    public class LibraryItem : ILibraryItem
    {
        private TablatureDocument _document;

        public LibraryItem(FileInfo fileInfo, string artist, string title, TabType type)
        {
            FileInfo = fileInfo;
            Artist = artist;
            Title = title;
            Type = type;
        }

        public LibraryItem(TablatureDocument doc) : this(doc.FileInfo, doc.Artist, doc.Title, doc.Type)
        {
            _document = doc;
        }

        #region Implementation of ITablatureAttributes

        public string Artist { get; set; }

        public string Title { get; set; }

        public TabType Type { get; set; }

        #endregion

        #region Implementation of LibraryEntryAttributes

        public FileInfo FileInfo { get; private set; }

        public bool Favorited { get; set; }

        public int Views { get; set; }

        public DateTime? LastViewed { get; set; }

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