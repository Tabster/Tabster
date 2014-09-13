#region

using System;
using System.Collections.Generic;
using Tabster.Core.Data;

#endregion

namespace Tabster.Library
{
    public interface ITablatureLibrary : IEnumerable<LibraryItem>
    {
        int TotalItems { get; }

        void Add(LibraryItem item);
        LibraryItem Add(TablatureDocument doc);
        bool Remove(LibraryItem item);
        LibraryItem GetLibraryItem(TablatureDocument doc);

        LibraryItem Find(Predicate<LibraryItem> match);
        List<LibraryItem> FindAll(Predicate<LibraryItem> match);
        LibraryItem Find(string path);
    }
}