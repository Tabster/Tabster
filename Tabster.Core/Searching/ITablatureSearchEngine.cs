#region

using System;
using System.Net;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Searching
{
    /// <summary>
    ///     Tablature search engine service.
    /// </summary>
    public interface ITablatureSearchEngine
    {
        /// <summary>
        ///     Search engine name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Search engine homepage.
        /// </summary>
        Uri Homepage { get; }

        /// <summary>
        ///     Determines whether the search engine requires an artist parameter be set.
        /// </summary>
        bool RequiresArtistParameter { get; }

        /// <summary>
        ///     Determines whether the search engine requires a title parameter be set;
        /// </summary>
        bool RequiresTitleParameter { get; }

        /// <summary>
        ///     Determines whether the search engine requires a type parameter be set.
        /// </summary>
        bool RequiresTypeParamter { get; }

        /// <summary>
        ///     Determines whether the search engine supports ratings.
        /// </summary>
        bool SupportsRatings { get; }

        /// <summary>
        ///     Determines whether the search engine supports pre-filtering of specific tablature types.
        /// </summary>
        bool SupportsPrefilteredTypes { get; }

        /// <summary>
        ///     Indicates the maximum number of requests to make.
        /// </summary>
        int MaximumConsecutiveRequests { get; }

        /// <summary>
        ///     Queries Search engine and returns results based on search parameters.
        /// </summary>
        /// <param name="query"> Search query. </param>
        /// <param name="proxy"> Optional proxy settings.</param>
        TablatureSearchResult[] Search(TablatureSearchQuery query, WebProxy proxy);

        /// <summary>
        ///     Determines whether a specific TablatureType is supported by the search engine.
        /// </summary>
        /// <param name="type"> The type to check. </param>
        /// <returns> True if the type is supported by the search engine; otherwise, False. </returns>
        bool SupportsTabType(TablatureType type);
    }
}