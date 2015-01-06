#region

using System.Net;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Searching
{
    /// <summary>
    ///   Tab service which enables searching.
    /// </summary>
    public interface ITablatureSearchEngine
    {
        /// <summary>
        ///   Service name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   Service flags.
        /// </summary>
        TablatureSearchEngineFlags Flags { get; }

        /// <summary>
        ///   Determines whether the service supports ratings.
        /// </summary>
        bool SupportsRatings { get; }

        /// <summary>
        ///   Queries service and returns results based on search parameters.
        /// </summary>
        /// <param name="query"> Search query. </param>
        /// <param name="proxy"> Optional proxy settings.</para>
        TablatureSearchResult[] Search(TablatureSearchQuery query, WebProxy proxy = null);

        ///<summary>
        ///  Determines whether a specific TabType is supported by the service.
        ///</summary>
        ///<param name="type"> The type to check. </param>
        ///<returns> True if the type is supported by the service; otherwise, False. </returns>
        bool SupportsTabType(TablatureType type);
    }
}