#region

using System.Net;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Searching
{
    /// <summary>
    ///   Tablature search engine service.
    /// </summary>
    public interface ITablatureSearchEngine
    {
        /// <summary>
        ///   Search engine name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   Search engine options.
        /// </summary>
        TablatureSearchEngineOptions Options { get; }

        /// <summary>
        ///   Determines whether the search engine supports ratings.
        /// </summary>
        bool SupportsRatings { get; }

        /// <summary>
        ///   Queries Search engine and returns results based on search parameters.
        /// </summary>
        /// <param name="query"> Search query. </param>
        /// <param name="proxy"> Optional proxy settings.</param>
        TablatureSearchResult[] Search(TablatureSearchQuery query, WebProxy proxy = null);

        ///<summary>
        ///  Determines whether a specific TablatureType is supported by the search engine.
        ///</summary>
        ///<param name="type"> The type to check. </param>
        ///<returns> True if the type is supported by the search engine; otherwise, False. </returns>
        bool SupportsTabType(TablatureType type);
    }
}