#region

using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using NS_Common;

#endregion

namespace Tabster.UltimateGuitar
{
    public enum TabType
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

    public class UltimateGuitarTab
    {
        private static readonly Regex TitleRegex = new Regex(@"\<title\b[^>]*\>\s*(?<title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex JSArtistSRegex = new Regex(@"tf_artist = ""(?<name>.*?)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex JSSongRegex = new Regex(@"tf_song = ""(?<name>.*?)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public UltimateGuitarTab(string artist, string title, Tabster.TabType type, Uri url, string contents)
        {
            Artist = artist;
            Title = title;
            Type = type;
            URL = url;
            Contents = contents;
        }

        public Uri URL { get; private set; }
        public string Artist { get; private set; }
        public string Title { get; private set; }
        public Tabster.TabType Type { get; private set; }
        public string Contents { get; private set; }
        public DateTime Updated { get; private set; }

        public Tab ConvertToTab()
        {
            return new Tab(Artist, Title, Type, Contents) {RemoteSource = URL, Source = TabSource.Download};
        }

        #region Static Methods

        public static UltimateGuitarTab Download(Uri url, string pageContents = null)
        {
            if (!IsValidUltimateGuitarTabURL(url))
                throw new ArgumentException("Invalid tab URL.");

            var html = pageContents;

            if (pageContents == null)
            {
                using (var client = new TabsterWebClient())
                {
                    html = client.DownloadString(url);
                }
            }

            if (!string.IsNullOrEmpty(html))
            {
                string song = null, artist = null;
                var ultimateGuitarTabType = TabType.GuitarTab;

                var javascriptVars = false;

                var artistMatch = JSArtistSRegex.Match(html);
                var songMatch = JSSongRegex.Match(html);

                //attempt to find and extract data from js variables
                //tf_artist = "Misc Computer Games";
                //tf_song = "Legend Of Zelda - Zeldas Lullaby";
                if (artistMatch.Success && songMatch.Success)
                {
                    artist = artistMatch.Groups["name"].Value;
                    song = songMatch.Groups["name"].Value;
                }

                    //get info from title
                else
                {
                    var title = Common.CollapseSpaces(TitleRegex.Match(html).Groups["title"].Value);
                    var titledata = Regex.Split(title, @" tab by ");
                    song = titledata[0].Trim();
                    artist = titledata[1].Trim();
                }

                //get the tab type
                if (url.ToString().EndsWith("crd.htm") || url.ToString().Contains("crd"))
                    ultimateGuitarTabType = TabType.GuitarChords;
                else if (url.ToString().EndsWith("btab.htm") || url.ToString().Contains("btab"))
                    ultimateGuitarTabType = TabType.BassTab;
                else if (url.ToString().EndsWith("drum_tab.htm"))
                    ultimateGuitarTabType = TabType.DrumTab;

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                //div[@id='cont']
                //var cont = doc.DocumentNode.SelectSingleNode("*[@id='cont']");

                var contentsNode = doc.DocumentNode.SelectSingleNode("//div[@id='cont']/pre");

                if (contentsNode != null)
                {
                    return new UltimateGuitarTab(artist, song, GetTabType(ultimateGuitarTabType), url, contentsNode.InnerHtml);
                }

                //string innerHtml = this.hap_doc.DocumentNode.SelectSingleNode("//div[@id='cont']/pre").InnerHtml;

                /*
                    WARNING: You are trying to view
                    content from Ultimate-Guitar.com
                    in an unauthorized application,
                    which is prohibited.

                    Please use an official Ultimate
                    Guitar Tabs application for iPhone,
                    iPad or Android to access legitimate
                    chords, guitar, bass, and drum tabs
                    from Ultimate-Guitar.com database.

                    Type "ultimate guitar tabs" in Apple
                    App Store's or Android Market's
                    search to find the application.
                 */

                //Common.CollapseSpaces(doc.GetElementsByTagName("title")[0].InnerText).Replace(" @ Ultimate-Guitar.Com", "");
                //var tab = doc.SelectSingleNode("div[@id='cont']").InnerText;

                //\(ver \d+\)
                /*
                var m = Regex.Match(data, @"<title>\s*(.+?)\s*</title>");
                var title = Common.CollapseSpaces(m.Groups[0].Value).Replace(" @ Ultimate-Guitar.Com", "");
                */
            }

            return null;
        }

        public static Tabster.TabType GetTabType(TabType type)
        {
            switch (type)
            {
                case TabType.GuitarTab:
                    return Tabster.TabType.Guitar;
                case TabType.GuitarChords:
                    return Tabster.TabType.Chord;
                case TabType.BassTab:
                    return Tabster.TabType.Bass;
                case TabType.DrumTab:
                    return Tabster.TabType.Drum;
                default:
                    return Tabster.TabType.Guitar;
            }
        }

        public static bool IsValidUltimateGuitarTabURL(Uri url)
        {
            /*
             * URL scheme
             * http://tabs.ultimate-guitar.com/r/rurouni_kenshin/departure_intro_tab.htm
             * 
             * tabs.ultimate-guitar.com/{ALPHA CAT}/{ARTIST}/{SONG}_{TYPE}.htm
             *
             */

            if (url.DnsSafeHost == "ultimate-guitar.com" || url.DnsSafeHost == "www.ultimate-guitar.com" || url.DnsSafeHost == "tabs.ultimate-guitar.com")
            {
                if (url.AbsolutePath.Split('/').Length >= 4)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}