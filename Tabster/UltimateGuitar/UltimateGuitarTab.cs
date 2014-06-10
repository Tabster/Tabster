#region

using System;
using HtmlAgilityPack;
using Tabster.Core;

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
        Ukulele = 800,
        Undefined = 0
    }

    public class UltimateGuitarTab
    {
        public UltimateGuitarTab(string artist, string title, Core.TabType type, Uri url, string contents)
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
        public Core.TabType Type { get; private set; }
        public string Contents { get; private set; }

        public Tab ConvertToTab()
        {
            return new Tab(Artist, Title, Type, Contents) {Source = URL, SourceType = TabSource.Download};
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
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var ultimateGuitarTabType = TabType.GuitarTab;
                string song = null, artist = null;

                //get values from meta keywords
                var metaNodes = doc.DocumentNode.SelectNodes("/html/head/meta");
                foreach (var mn in metaNodes)
                {
                    if (mn.HasAttributes && mn.Attributes.Contains("name") && mn.Attributes["name"].Value == "keywords")
                    {
                        var split = mn.Attributes["content"].Value.Split(',');
                        song = split[0].Trim();

                        var typeStr = split[1].Trim();

                        if (typeStr.IndexOf("bass", StringComparison.OrdinalIgnoreCase) > -1)
                            ultimateGuitarTabType = TabType.BassTab;
                        else if (typeStr.IndexOf("chord", StringComparison.OrdinalIgnoreCase) > -1)
                            ultimateGuitarTabType = TabType.GuitarChords;
                        else if (typeStr.IndexOf("drum", StringComparison.OrdinalIgnoreCase) > -1)
                            ultimateGuitarTabType = TabType.DrumTab;
                        else if (typeStr.IndexOf("ukulele", StringComparison.OrdinalIgnoreCase) > -1)
                            ultimateGuitarTabType = TabType.Ukulele;

                        artist = split[2].Trim();
                        break;
                    }
                }

                var contentsNode = doc.DocumentNode.SelectSingleNode("//div[@id='cont']/pre[2]");

                if (contentsNode != null)
                {
                    var contents = Common.StripHTML(contentsNode.InnerHtml);
                    contents = Common.ConvertNewlines(contents);
                    return new UltimateGuitarTab(artist, song, GetTabType(ultimateGuitarTabType), url, contents);
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
            }

            return null;
        }

        public static Core.TabType GetTabType(TabType type)
        {
            switch (type)
            {
                case TabType.GuitarTab:
                    return Core.TabType.Guitar;
                case TabType.GuitarChords:
                    return Core.TabType.Chord;
                case TabType.BassTab:
                    return Core.TabType.Bass;
                case TabType.DrumTab:
                    return Core.TabType.Drum;
                case TabType.Ukulele:
                    return Core.TabType.Ukulele;
                default:
                    return Core.TabType.Guitar;
            }
        }

        public static bool IsValidUltimateGuitarTabURL(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute) && IsValidUltimateGuitarTabURL(new Uri(url));
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