#region

using System.Text.RegularExpressions;

#endregion

namespace Tabster.UltimateGuitar
{
    public enum UltimateGuitarTabType
    {
        GuitarTab = 200,
        GuitarChords = 300,
        BassTab = 400,
        DrumTab = 700,
        PowerTab = 600,
        GuitarPro = 500,
        Video = 100,
        TabPro = 666,
        Undefined = 0
    }

    public class Constants
    {
        public const string Tab_Home_0 = "http://tabs.ultimate-guitar.com/";
        public const string Tab_Home_1 = "http://www.ultimate-guitar.com/tabs/";
        internal static Regex TitleRegex = new Regex(@"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        internal static Regex JSArtistSRegex = new Regex(@"tf_artist = ""(?<name>.*?)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        internal static Regex JSSongRegex = new Regex(@"tf_song = ""(?<name>.*?)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}