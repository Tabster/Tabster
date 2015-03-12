#region

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace Tabster.Data
{
    public interface ITablaturePlaylistFile : IEnumerable<ITablatureFile>, ITabsterFile
    {
        string Name { get; set; }
        bool Contains(string filePath);
        bool Contains(ITablatureFile file);
        void Add(ITablatureFile file, FileInfo fileInfo);
        bool Remove(ITablatureFile file);
        void Clear();
        ITablatureFile Find(Predicate<ITablatureFile> match);
        FileInfo GetFileInfo(ITablatureFile file);
    }
}