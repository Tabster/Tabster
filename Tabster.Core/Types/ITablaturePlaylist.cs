#region

using System.Collections.Generic;
using Tabster.Core.Data;

#endregion

namespace Tabster.Core.Types
{
    public interface ITablaturePlaylist : ICollection<TablatureDocument>
    {
        string Name { get; set; }
    }
}