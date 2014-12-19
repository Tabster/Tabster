#region

using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace UltimateGuitar
{
    public class UltimateGuitarParser : ITablatureWebpageImporter
    {
        #region Implementation of ITabParser

        public string SiteName
        {
            get { return "Ultimate Guitar"; }
        }

        public TablatureDocument Parse(string text, TablatureType type)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(text);

            var titleNode = doc.DocumentNode.SelectSingleNode("//div[starts-with(@class, 't_title')]");
            var titleNodeValue = titleNode.SelectSingleNode(".//h1").InnerText; //contains title and type

            var title = "";

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
                var contents = StripHTML(contentsNode.InnerHtml);
                contents = ConvertNewlines(contents);
                return new TablatureDocument(artist, title, type, contents);
            }

            return null;
        }

        public bool MatchesUrlPattern(Uri url)
        {
            return url.IsWellFormedOriginalString() &&
                   ((url.DnsSafeHost == "ultimate-guitar.com" || url.DnsSafeHost == "www.ultimate-guitar.com" ||
                     url.DnsSafeHost == "tabs.ultimate-guitar.com") && url.AbsolutePath.Split('/').Length >= 4);
        }

        #endregion

        private static readonly Regex NewlineRegex = new Regex("(?<!\r)\n", RegexOptions.Compiled);

        private static string ConvertNewlines(string source)
        {
            return NewlineRegex.Replace(source, Environment.NewLine);
        }

        private static string StripHTML(string source)
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