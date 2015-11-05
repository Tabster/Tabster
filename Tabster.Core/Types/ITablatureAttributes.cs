namespace Tabster.Core.Types
{
    public interface ITablatureAttributes
    {
        /// <summary>
        /// The tablature artist.
        /// </summary>
        string Artist { get; set; }

        /// <summary>
        /// The tablature title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The tablature type.
        /// </summary>
        TablatureType Type { get; set; }
    }

    public static class ITablatureAttributesExtensions
    {
        public static string ToFriendlyString(this ITablatureAttributes tablatureAttributes)
        {
            return string.Format("{0} - {1} ({2})", tablatureAttributes.Artist, tablatureAttributes.Title, tablatureAttributes.Type.ToFriendlyString());
        }
    }
}