namespace Tabster.Core.Types
{
    public interface ITablatureRatedAttribute : ITablatureAttributes
    {
        TablatureRating Rating { get; set; }
    }
}