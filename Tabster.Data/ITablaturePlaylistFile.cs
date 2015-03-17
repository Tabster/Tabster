#region

using System;
using System.Collections.Generic;

#endregion

namespace Tabster.Data
{
    public interface ITablaturePlaylistFile : IEnumerable<TablaturePlaylistItem>, ITabsterFile
    {
        string Name { get; set; }
        bool Contains(string filePath);
        bool Contains(TablaturePlaylistItem file);
        void Add(TablaturePlaylistItem item);
        bool Remove(TablaturePlaylistItem item);
        void Clear();
        TablaturePlaylistItem Find(Predicate<TablaturePlaylistItem> match);
    }
}