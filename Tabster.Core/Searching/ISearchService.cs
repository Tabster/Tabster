#region

using System.Net;
using Tabster.Core.Data.Processing;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Searching
{
    /// <summary>
    ///   Tab service which enables searching.
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        ///   Service name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   Associated parser.
        /// </summary>
        ITablatureWebpageImporter Parser { get; }

        /// <summary>
        ///   Service flags.
        /// </summary>
        SearchServiceFlags Flags { get; }

        /// <summary>
        /// Proxy settings.
        /// </summary>
        WebProxy Proxy { get; }

        /// <summary>
        /// Determines whether the service supports ratings.
        /// </summary>
        bool SupportsRatings { get; }

        /// <summary>
        ///   Queries service and returns results based on search parameters.
        /// </summary>
        /// <param name="query"> Search query. </param>
        SearchResult[] Search(SearchQuery query);

        ///<summary>
        ///  Determines whether a specific TabType is supported by the service.
        ///</summary>
        ///<param name="type"> The type to check. </param>
        ///<returns> True if the type is supported by the service; otherwise, False. </returns>
        bool SupportsTabType(TabType type);
    }
}