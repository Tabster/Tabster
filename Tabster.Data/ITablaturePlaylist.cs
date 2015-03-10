#region

using System.Collections.Generic;

#endregion

namespace Tabster.Data
{
    public interface ITablaturePlaylist : ICollection<ITablatureFile>
    {
        string Name { get; set; }
        bool Contains(string fileName);
    }
}