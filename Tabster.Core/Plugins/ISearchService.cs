#region

using System;
using Tabster.Core.FileTypes;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Plugins
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
        ITabParser Parser { get; }

        /// <summary>
        ///   Service flags.
        /// </summary>
        SearchServiceFlags Flags { get; }

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

    /// <summary>
    ///   Tab search query.
    /// </summary>
    public class SearchQuery
    {
        public SearchQuery(ISearchService service, string artist, string title, TabType? type)
        {
            Service = service;
            Artist = artist;
            Title = title;
            Type = type;
        }

        /// <summary>
        ///   The associated search service.
        /// </summary>
        public ISearchService Service { get; private set; }

        /// <summary>
        ///   Artist search parameter.
        /// </summary>
        public string Artist { get; private set; }

        /// <summary>
        ///   Title search parameter.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///   Type search parameter.
        /// </summary>
        public TabType? Type { get; private set; }
    }

    /// <summary>
    ///   Tab search result.
    /// </summary>
    public class SearchResult
    {
        public SearchResult(SearchQuery query, TablatureDocument tab, TabRating? rating)
        {
            Query = query;
            Tab = tab;
            Rating = rating;
        }

        /// <summary>
        ///   The initial search query.
        /// </summary>
        public SearchQuery Query { get; private set; }

        /// <summary>
        ///   Tab result.
        /// </summary>
        public TablatureDocument Tab { get; private set; }

        /// <summary>
        ///   Search result rating.
        /// </summary>
        public TabRating? Rating { get; private set; }
    }

    public class SearchServiceException : Exception
    {
        public SearchServiceException()
        {
        }

        public SearchServiceException(string message)
            : base(message)
        {
        }
    }
}