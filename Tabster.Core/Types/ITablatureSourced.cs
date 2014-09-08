#region

using System;

#endregion

namespace Tabster.Core.Types
{
    public interface ITablatureSourced : ITablature
    {
        TablatureSourceType SourceType { get; set; }
        Uri Source { get; set; }
        string Method { get; set; }
    }
}