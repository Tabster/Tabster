#region

using System;

#endregion

namespace Tabster.Core.Types
{
    public interface ITablatureSourceAttributes
    {
        TablatureSourceType SourceType { get; set; }
        Uri Source { get; set; }
        string SourceTag { get; set; }
    }
}