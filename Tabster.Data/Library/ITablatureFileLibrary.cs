#region

using System;
using System.Collections.Generic;

#endregion

namespace Tabster.Data.Library
{
    public interface ITablatureFileLibrary : IEnumerable<TablatureLibraryItem>
    {
        int TotalItems { get; }

        void Add(TablatureLibraryItem item);
        TablatureLibraryItem Add(TablatureDocument doc);
        bool Remove(TablatureLibraryItem item);
        TablatureLibraryItem GetLibraryItem(TablatureDocument doc);

        TablatureLibraryItem Find(Predicate<TablatureLibraryItem> match);
        List<TablatureLibraryItem> FindAll(Predicate<TablatureLibraryItem> match);
        TablatureLibraryItem Find(string path);
    }
}