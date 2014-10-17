#region

using System;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Searching
{
    /// <summary>
    ///   Tablature search result.
    /// </summary>
    public class SearchResult
    {
        public SearchResult(SearchQuery query, AttributedTablature tab, Uri source, TablatureRating rating = TablatureRating.None)
        {
            Query = query;
            Tab = tab;
            Source = source;
            Rating = rating;
        }

        /// <summary>
        ///   The initial search query.
        /// </summary>
        public SearchQuery Query { get; private set; }

        /// <summary>
        ///   Tablature result.
        /// </summary>
        public AttributedTablature Tab { get; private set; }

        /// <summary>
        ///   Tablature Url source.
        /// </summary>
        public Uri Source { get; private set; }

        /// <summary>
        ///   Search result rating.
        /// </summary>
        public TablatureRating Rating { get; private set; }
    }
}