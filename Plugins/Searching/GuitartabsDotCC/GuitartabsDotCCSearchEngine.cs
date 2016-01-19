#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using Tabster.Core.Searching;
using Tabster.Core.Types;

#endregion

namespace GuitartabsDotCC
{
    public class GuitartabsDotCCSearchEngine : ITablatureSearchEngine
    {
        public GuitartabsDotCCSearchEngine()
        {
            Homepage = new Uri("http://guitartabs.cc");
        }

        #region Implementation of ISearchService

        public string Name
        {
            get { return "Guitartabs.cc"; }
        }

        public Uri Homepage { get; private set; }

        public bool RequiresArtistParameter
        {
            get { return false; }
        }

        public bool RequiresTitleParameter
        {
            get { return true; }
        }

        public bool RequiresTypeParamter
        {
            get { return false; }
        }

        public bool SupportsRatings
        {
            get { return true; }
        }

        public bool SupportsPrefilteredTypes { get; private set; }

        public TablatureSearchResult[] Search(TablatureSearchQuery query, WebProxy proxy = null)
        {
            var results = new List<TablatureSearchResult>();

            //sanitize parameters prior to encoding
            var urlEncodedArtist = HttpUtility.UrlEncode(SanitizeParameter(query.Artist));
            var urlEncodedTitle = HttpUtility.UrlEncode(SanitizeParameter(query.Title));

            var typeStr = "any";

            if (query.Type != null)
            {
                if (query.Type == TablatureType.Guitar || query.Type == TablatureType.Chords)
                {
                    typeStr = "guitar";
                }
                else if (query.Type == TablatureType.Bass)
                {
                    typeStr = "bass";
                }
                else if (query.Type == TablatureType.Drum)
                {
                    typeStr = "drum";
                }
            }

            var urlBuilder = new StringBuilder();
            urlBuilder.AppendFormat("http://www.guitartabs.cc/search.php?tabtype={0}", typeStr);

            if (!string.IsNullOrEmpty(urlEncodedArtist))
                urlBuilder.AppendFormat("&band={0}", urlEncodedArtist);

            if (!string.IsNullOrEmpty(urlEncodedTitle))
                urlBuilder.AppendFormat("&song={0}", urlEncodedTitle);

            var url = new Uri(urlBuilder.ToString());

            string data;

            using (var client = new WebClient() {Proxy = proxy, Encoding = Encoding.UTF8})
            {
                data = client.DownloadString(url);
            }

            if (data != string.Empty)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(data);

                var tabslist = doc.DocumentNode.SelectSingleNode("//table[@class='tabslist fs-12']");

                var rows = tabslist.SelectNodes("tr");

                if (rows != null)
                {
                    for (var i = 1; i <= rows.Count - 1; i++)
                    {
                        var row = rows[i];
                        var columns = row.SelectNodes("td");

                        var rowArtist = columns[2].InnerText;

                        var rowHref = columns[3].ChildNodes["a"];

                        var rowUrl = rowHref.Attributes["href"].Value;
                        var rowTitle = rowHref.InnerText;
                        var rowType = columns[5].InnerText;

                        var tabType = GetTabType(rowType);

                        var rowRating = GetRating(columns[4].FirstChild.Attributes["style"].Value);

                        if (tabType != null)
                        {
                            rowTitle = RemoveTypeFromTitle(rowTitle, tabType);

                            var tab = new AttributedTablature(rowArtist, rowTitle, tabType);
                            results.Add(new TablatureSearchResult(query, this, tab,
                                new Uri(string.Format("http://guitartabs.cc{0}", rowUrl)), rowRating));
                        }
                    }
                }
            }

            return results.ToArray();
        }

        public bool SupportsTabType(TablatureType type)
        {
            return type == TablatureType.Guitar || type == TablatureType.Chords || type == TablatureType.Bass ||
                   type == TablatureType.Drum;
        }

        #endregion

        #region Static Methods

        private static Regex _mappingRegex;

        private static readonly Dictionary<string, string> _customCharacterMappings = new Dictionary<string, string>
        {
            {"//", "_"}
        };

        private static string SanitizeParameter(string str, string defaultReplacement = "_")
        {
            if (_mappingRegex == null)
            {
                var pattern = new StringBuilder("[");
                foreach (var key in _customCharacterMappings.Keys)
                    pattern.Append(Regex.Escape(key));
                pattern.Append("]");

                _mappingRegex = new Regex(pattern.ToString(), RegexOptions.Compiled);
            }

            return _mappingRegex.Replace(str, match => _customCharacterMappings.ContainsKey(match.Value)
                ? _customCharacterMappings[match.Value]
                : defaultReplacement);
        }

        private static TablatureRating GetRating(string style)
        {
            switch (style)
            {
                case "background-position: -48px 50%;":
                    return TablatureRating.Stars1;
                case "background-position: -36px 50%;":
                    return TablatureRating.Stars2;
                case "background-position: -24px 50%;":
                    return TablatureRating.Stars3;
                case "background-position: -12px 50%;":
                    return TablatureRating.Stars4;
                case "background-position: -0px 50%;":
                    return TablatureRating.Stars5;
                default:
                    return TablatureRating.None;
            }
        }

        private static string RemoveTypeFromTitle(string title, TablatureType type)
        {
            if (type == TablatureType.Guitar)
                return title.Remove(title.Length - " Tab".Length);
            if (type == TablatureType.Chords)
                return title.Remove(title.Length - " Chords".Length);
            if (type == TablatureType.Bass)
                return title.Remove(title.Length - " Bass Tab".Length);
            if (type == TablatureType.Drum)
                return title.Remove(title.Length - " Drum Tab".Length);

            return title;
        }

        private static TablatureType GetTabType(string str)
        {
            if (str.IndexOf("Chords", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return TablatureType.Chords;
            if (str.IndexOf("Bass", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return TablatureType.Bass;
            if (str.IndexOf("Drum", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return TablatureType.Drum;
            if (str.IndexOf("Tab", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return TablatureType.Guitar;

            return null;
        }

        #endregion
    }
}