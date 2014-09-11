namespace Tabster.Core.Types
{
    public interface ITablatureAttributes
    {
        string Artist { get; set; }
        string Title { get; set; }
        TabType Type { get; set; }
    }
}