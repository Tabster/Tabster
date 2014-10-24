#region

using System;
using System.IO;
using Tabster.Core.Types;

#endregion

namespace Tabster.Data.Library
{
    public interface ITablatureLibraryItem : ITablatureAttributes
    {
        FileInfo FileInfo { get; }
        bool Favorited { get; set; }
        int Views { get; set; }
        DateTime? LastViewed { get; set; }
        DateTime Added { get; }
    }
}