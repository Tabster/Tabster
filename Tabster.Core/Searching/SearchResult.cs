#region

using Tabster.Core.Data;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Searching
{
    /// <summary>
    ///   Tab search result.
    /// </summary>
    public class SearchResult
    {
        public SearchResult(SearchQuery query, TablatureDocument tab, TablatureRating? rating)
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
        public TablatureRating? Rating { get; private set; }
    }
}