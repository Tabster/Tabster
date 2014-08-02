namespace Tabster.Core.Types
{
    public interface ITablature
    {
        string Artist { get; set; }
        string Title { get; set; }
        string Contents { get; set; }
        TabType Type { get; set; }
    }
}