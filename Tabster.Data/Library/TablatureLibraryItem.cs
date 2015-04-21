#region

using System;
using System.IO;

#endregion

namespace Tabster.Data.Library
{
    public class TablatureLibraryItem<TTablatureFile> : LibraryItem where TTablatureFile : ITablatureFile
    {
        public TablatureLibraryItem(TTablatureFile file, FileInfo fileInfo)
            : base(file, fileInfo)
        {
            File = file;
        }

        public new TTablatureFile File { get; private set; }
        public bool Favorited { get; set; }
        public int Views { get; set; }
        public DateTime? LastViewed { get; set; }
        public DateTime Added { get; set; }
    }
}