#region

using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Searching
{
    /// <summary>
    ///   Tab search query.
    /// </summary>
    public class SearchQuery
    {
        public SearchQuery(ISearchService service, string artist, string title, TablatureType type)
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
        public TablatureType Type { get; private set; }
    }
}