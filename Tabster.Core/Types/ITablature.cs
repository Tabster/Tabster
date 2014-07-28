namespace Tabster.Core.Types
{
    public interface ITablature
    {
        string Artist { get; set; }
        string Title { get; set; }
        string Contents { get; set; }
        TabType Type { get; set; }
    }

    public static class ITablatureExtensions
    {
        public static string ToFriendlyString(this ITablature tab)
        {
            return string.Format("{0} - {1} ({2})", tab.Artist, tab.Title, tab.Type.ToFriendlyString());
        }
    }
}