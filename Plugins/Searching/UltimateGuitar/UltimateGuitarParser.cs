#region

using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Tabster.Core.Types;
using Tabster.Data.Processing;

#endregion

namespace UltimateGuitar
{
    public class UltimateGuitarParser : ITablatureWebImporter
    {
        private static readonly Regex NewlineRegex = new Regex("(?<!\r)\n", RegexOptions.Compiled);

        public UltimateGuitarParser()
        {
            Homepage = new Uri("http://ultimate-guitar.com");
        }

        #region Implementation of ITablatureWebImporter

        public string SiteName
        {
            get { return "Ultimate Guitar"; }
        }

        public Uri Homepage { get; private set; }

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public bool IsUrlParsable(Uri url)
        {
            return url.DnsSafeHost == "ultimate-guitar.com" || url.DnsSafeHost == "www.ultimate-guitar.com" ||
                   url.DnsSafeHost == "tabs.ultimate-guitar.com";
        }

        public AttributedTablature Parse(Uri url, WebProxy proxy)
        {
            string html;

            using (var client = new WebClient() {Proxy = proxy, Encoding = Encoding.UTF8})
            {
                html = client.DownloadString(url);
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var titleNode = doc.DocumentNode.SelectSingleNode("//div[starts-with(@class, 't_title')]");
            var titleNodeValue = titleNode.SelectSingleNode(".//h1").InnerText; //contains title and type

            string title;
            TablatureType type = null;

            if (titleNodeValue.EndsWith("Bass Tab"))
            {
                type = TablatureType.Bass;
                title = titleNodeValue.Substring(0, titleNodeValue.Length - "Bass Tab".Length);
            }

            else if (titleNodeValue.EndsWith("Drum Tab"))
            {
                type = TablatureType.Drum;
                title = titleNodeValue.Substring(0, titleNodeValue.Length - "Drum Tab".Length);
            }

            else if (titleNodeValue.EndsWith("Ukulele Chords"))
            {
                type = TablatureType.Ukulele;
                title = titleNodeValue.Substring(0, titleNodeValue.Length - "Ukulele Chords".Length);
            }

            else if (titleNodeValue.EndsWith("Chords"))
            {
                type = TablatureType.Chords;
                title = titleNodeValue.Substring(0, titleNodeValue.Length - "Chords".Length);
            }

            else
            {
                type = TablatureType.Guitar;
                title = titleNodeValue.Substring(0, titleNodeValue.Length - "Tab".Length);
            }

            var artist = titleNode.SelectSingleNode(".//div[@class='t_autor']/a").InnerText;

            var contentsNode = doc.DocumentNode.SelectSingleNode("//div[@id='cont']/pre[2]");

            if (contentsNode != null)
            {
                var contents = ConvertNewlines(StripHtml(contentsNode.InnerHtml));
                return new AttributedTablature(artist, title, type) {Contents = contents};
            }

            return null;
        }

        #endregion

        private static string ConvertNewlines(string source)
        {
            return NewlineRegex.Replace(source, Environment.NewLine);
        }

        /// <summary>
        ///     Used to strip HTML tags from chord-based tabs.
        /// </summary>
        private static string StripHtml(string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            for (var i = 0; i < source.Length; i++)
            {
                var let = source[i];

                if (let == '<')
                {
                    inside = true;
                    continue;
                }

                if (let == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }

            return new string(array, 0, arrayIndex);
        }
    }
}