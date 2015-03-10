#region

using System;
using System.IO;

#endregion

namespace Tabster.Data.Library
{
    public interface ITablatureLibraryItem
    {
        FileInfo FileInfo { get; }
        bool Favorited { get; set; }
        int Views { get; set; }
        DateTime? LastViewed { get; set; }
        DateTime Added { get; }
        ITablatureFile File { get; }
    }
}