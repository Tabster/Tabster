namespace Tabster.Core.Types
{
    public interface ITablatureAttributes
    {
        string Artist { get; set; }
        string Title { get; set; }
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