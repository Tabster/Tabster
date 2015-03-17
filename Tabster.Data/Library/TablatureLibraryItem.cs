#region

using System;

#endregion

namespace Tabster.Data.Library
{
    public class TablatureLibraryItem<TTablatureFile> : LibraryItem<TTablatureFile> where TTablatureFile : class, ITablatureFile
    {
        public bool Favorited { get; set; }
        public int Views { get; set; }
        public DateTime? LastViewed { get; set; }
        public DateTime Added { get; set; }
    }
}