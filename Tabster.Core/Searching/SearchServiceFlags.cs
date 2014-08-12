using System;

namespace Tabster.Core.Searching
{
    /// <summary>
    ///   Search service flags.
    /// </summary>
    [Flags]
    public enum SearchServiceFlags
    {
        None = 1,
        RequiresArtistParameter = 2,
        RequiresTitleParameter = 4,
        RequiresTypeParamter = 8,
    }
}