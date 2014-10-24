#region

using System;

#endregion

namespace Tabster.Core.Types
{
    public interface ITablatureFileAttributes : ITablatureAttributes
    {
        string Comment { get; set; }
        DateTime Created { get; set; }
    }
}