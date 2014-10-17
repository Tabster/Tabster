﻿#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using Tabster.Core.Searching;
using Tabster.Core.Types;
using Tabster.Data.Processing;

#endregion

namespace GuitartabsDotCC
{
    public class GuitartabsDotCCSearch : ISearchService
    {
        #region Implementation of ISearchService

        public ITablatureWebpageImporter Parser
        {
            get { return new GuitartabsDotCCParser(); }
        }

        public string Name
        {
            get { return "Guitartabs.cc"; }
        }

        public SearchServiceFlags Flags
        {
            get { return SearchServiceFlags.RequiresTitleParameter; }
        }

        public WebProxy Proxy { get; set; }

        public bool SupportsRatings
        {
            get { return true; }
        }

        public SearchResult[] Search(SearchQuery query)
        {
            var results = new List<SearchResult>();

            //sanitize parameters prior to encoding
            var urlEncodedArtist = HttpUtility.UrlEncode(SanitizeParameter(query.Artist));
            var urlEncodedTitle = HttpUtility.UrlEncode(SanitizeParameter(query.Title));

            var typeStr = "any";

            if (query.Type.HasValue)
            {
                switch (query.Type.Value)
                {
                    case TablatureType.Guitar:
                    case TablatureType.Chords:
                        typeStr = "guitar";
                        break;
                    case TablatureType.Bass:
                        typeStr = "bass";
                        break;
                    case TablatureType.Drum:
                        typeStr = "drum";
                        break;
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

            var client = new WebClient {Proxy = Proxy};
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

                        if (tabType.HasValue)
                        {
                            rowTitle = RemoveTypeFromTitle(rowTitle, tabType.Value);

                            var tab = new AttributedTablature(rowArtist, rowTitle, tabType.Value);
                            results.Add(new SearchResult(query, tab, new Uri(string.Format("http://guitartabs.cc{0}", rowUrl)), rowRating));
                        }
                    }
                }
            }

            return results.ToArray();
        }

        public bool SupportsTabType(TablatureType type)
        {
            switch (type)
            {
                case TablatureType.Guitar:
                case TablatureType.Chords:
                case TablatureType.Bass:
                case TablatureType.Drum:
                    return true;
                default:
                    return false;
            }
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
            switch (type)
            {
                case TablatureType.Guitar:
                    return title.Remove(title.Length - " Tab".Length);
                case TablatureType.Chords:
                    return title.Remove(title.Length - " Chords".Length);
                case TablatureType.Bass:
                    return title.Remove(title.Length - " Bass Tab".Length);
                case TablatureType.Drum:
                    return title.Remove(title.Length - " Drum Tab".Length);
            }

            return title;
        }

        private static TablatureType? GetTabType(string str)
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