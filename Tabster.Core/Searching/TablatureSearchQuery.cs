#region

using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Searching
{
    /// <summary>
    ///   Tablature search query.
    /// </summary>
    public class TablatureSearchQuery
    {
        public TablatureSearchQuery(string artist, string title, TablatureType type, TablatureRating rating)
        {
            Artist = artist;
            Title = title;
            Type = type;
            Rating = rating;
        }

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

        /// <summary>
        /// Rating search parameter.
        /// </summary>
        public TablatureRating Rating { get; private set; }
    }
}