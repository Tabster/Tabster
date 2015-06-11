namespace Tabster.Core.Types
{
    public interface ITablatureExtendedAttributes
    {
        string Subtitle { get; set; }
        string Album { get; set; }
        string Genre { get; set; }
        string Author { get; set; }
        string Copyright { get; set; }
        string Lyrics { get; set; }
        string Comment { get; set; }
        TablatureDifficulty Difficulty { get; set; }
        TablatureTuning Tuning { get; set; }
    }
}