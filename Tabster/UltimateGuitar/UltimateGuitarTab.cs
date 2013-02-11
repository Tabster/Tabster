#region

using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using NS_Common;

#endregion

namespace Tabster.UltimateGuitar
{
    public class UltimateGuitarTab
    {
        public Uri URL { get; private set; }
        public bool IsVersioned { get; private set; }
        public string Artist { get; private set; }
        public string Title { get; private set; }
        public TabType Type { get; private set; }
        public string Contents { get; private set; }

        public UltimateGuitarTab(string pageContents)
        {
            rawHTML = pageContents;
            GetTab();
        }

        public UltimateGuitarTab(Uri url)
        {
            if (!IsValidUltimateGuitarTabURL(url))
                throw new ArgumentException("Invalid tab URL.");

            URL = url;

            using (var client = new TabsterWebClient())
            {
                rawHTML = client.DownloadString(URL);
            }

            GetTab();
        }

        private readonly string rawHTML;

        public static TabType GetTabType(UltimateGuitarTabType type)
        {
            switch (type)
            {
                case UltimateGuitarTabType.GuitarTab:
                    return TabType.Guitar;
                case UltimateGuitarTabType.GuitarChords:
                    return TabType.Chord;
                case UltimateGuitarTabType.BassTab:
                    return TabType.Bass;
                case UltimateGuitarTabType.DrumTab:
                    return TabType.Drum;
                default:
                    return TabType.Guitar;
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

        private void GetTab()
        {
            if (rawHTML.Length > 0)
            {
                string song = null, artist = null;
                var ultimateGuitarTabType = UltimateGuitarTabType.GuitarTab;

                var javascriptVars = false;

                var artistMatch = Constants.JSArtistSRegex.Match(rawHTML);
                var songMatch = Constants.JSSongRegex.Match(rawHTML);

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
                    var title = Common.CollapseSpaces(Constants.TitleRegex.Match(rawHTML).Groups["Title"].Value);
                    var titledata = Regex.Split(title, @" tab by ");
                    song = titledata[0].Trim();
                    artist = titledata[1].Trim();
                }

                //get the tab type
                if (URL.ToString().EndsWith("crd.htm") || URL.ToString().Contains("crd"))
                    ultimateGuitarTabType = UltimateGuitarTabType.GuitarChords;
                else if (URL.ToString().EndsWith("btab.htm") || URL.ToString().Contains("btab"))
                    ultimateGuitarTabType = UltimateGuitarTabType.BassTab;
                else if (URL.ToString().EndsWith("drum_tab.htm"))
                    ultimateGuitarTabType = UltimateGuitarTabType.DrumTab;

                var doc = new HtmlDocument();
                doc.LoadHtml(rawHTML);

                //div[@id='cont']
                //var cont = doc.DocumentNode.SelectSingleNode("*[@id='cont']");

                var contentsNode = doc.DocumentNode.SelectSingleNode("//div[@id='cont']/pre");

                if (contentsNode != null)
                {
                    Artist = artist;
                    Title = song;
                    Type = GetTabType(ultimateGuitarTabType);
                    Contents = contentsNode.InnerHtml;
                }

                //Console.WriteLine(doc.DocumentNode.SelectSingleNode("//div[@id='cont']/pre").InnerHtml);

                //string innerHtml = this.hap_doc.DocumentNode.SelectSingleNode("//div[@id='cont']/pre").InnerHtml;

                /*                WARNING: You are trying to view                content from Ultimate-Guitar.com                in an unauthorized application,                which is prohibited.                Please use an official Ultimate                Guitar Tabs application for iPhone,                iPad or Android to access legitimate                chords, guitar, bass, and drum tabs                from Ultimate-Guitar.com database.                Type "ultimate guitar tabs" in Apple                App Store's or Android Market's                search to find the application.
                */

                //Common.CollapseSpaces(doc.GetElementsByTagName("title")[0].InnerText).Replace(" @ Ultimate-Guitar.Com", "");
                //var tab = doc.SelectSingleNode("div[@id='cont']").InnerText;

                //\(ver \d+\)
                /*
                var m = Regex.Match(data, @"<title>\s*(.+?)\s*</title>");
                var title = Common.CollapseSpaces(m.Groups[0].Value).Replace(" @ Ultimate-Guitar.Com", "");
                */

                // var tab = Tab.Create(artist, song, tabType, fasdf )
            }
        }
    }
}