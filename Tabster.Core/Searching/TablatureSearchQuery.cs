#region

using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Searching
{
    /// <summary>
    ///   Tab search query.
    /// </summary>
    public class TablatureSearchQuery
    {
        public TablatureSearchQuery(ITablatureSearchEngine engine, string artist, string title, TablatureType type)
        {
            Engine = engine;
            Artist = artist;
            Title = title;
            Type = type;
        }

        /// <summary>
        ///   The associated search service.
        /// </summary>
        public ITablatureSearchEngine Engine { get; private set; }

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