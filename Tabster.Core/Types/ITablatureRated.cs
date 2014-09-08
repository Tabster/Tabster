namespace Tabster.Core.Types
{
    public interface ITablatureRated : ITablature
    {
        TabRating Rating { get; set; }
    }
}