#region

using System;

#endregion

namespace Tabster.Core.Types
{
    public interface ITablatureSourceAttribute : ITablatureAttributes
    {
        TablatureSourceType SourceType { get; set; }
        Uri Source { get; set; }
        string Method { get; set; }
    }
}