#region

using System.Collections.Generic;
using Tabster.Core.FileTypes;

#endregion

namespace Tabster.Core.Types
{
    public interface ITablaturePlaylist : ICollection<TablatureDocument>
    {
        string Name { get; set; }
    }
}