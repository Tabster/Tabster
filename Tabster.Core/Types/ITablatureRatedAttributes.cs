namespace Tabster.Core.Types
{
    public interface ITablatureRatedAttributes : ITablatureAttributes
    {
        TablatureRating Rating { get; set; }
    }
}