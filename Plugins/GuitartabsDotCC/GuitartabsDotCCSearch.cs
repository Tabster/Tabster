#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using Tabster.Core.FileTypes;
using Tabster.Core.Parsing;
using Tabster.Core.Searching;
using Tabster.Core.Types;

#endregion

namespace GuitartabsDotCC
{
    public class GuitartabsDotCCSearch : ISearchService
    {
        #region Implementation of ISearchService

        public string Name
        {
            get { return "Guitartabs.cc"; }
        }

        public IWebTabParser Parser
        {
            get { return new GuitartabsDotCCParser(); }
        }

        public SearchServiceFlags Flags
        {
            get { return SearchServiceFlags.RequiresTitleParameter; }
        }

        public bool SupportsRatings
        {
            get { return true; }
        }

        public SearchResult[] Search(SearchQuery query)
        {
            var results = new List<SearchResult>();

            var urlEncodedArtist = HttpUtility.UrlEncode(query.Artist);
            var urlEncodedTitle = HttpUtility.UrlEncode(query.Title);

            var typeStr = "any";

            if (query.Type.HasValue)
            {
                switch (query.Type.Value)
                {
                    case TabType.Guitar:
                    case TabType.Chords:
                        typeStr = "guitar";
                        break;
                    case TabType.Bass:
                        typeStr = "bass";
                        break;
                    case TabType.Drum:
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

            var client = new WebClient {Proxy = null};
            {
                data = client.DownloadString(url);
            }

            if (data != string.Empty)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(data);

                var tabslist = doc.DocumentNode.SelectSingleNode("//table[@class='tabslist fs-12']");

                var rows = tabslist.SelectNodes("tr");

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

                        var tab = new TablatureDocument(rowArtist, rowTitle, tabType.Value, null) {Source = new Uri(string.Format("http://guitartabs.cc{0}", rowUrl))};
                        results.Add(new SearchResult(query, tab, rowRating));
                    }
                }
            }

            return results.ToArray();
        }

        public bool SupportsTabType(TabType type)
        {
            switch (type)
            {
                case TabType.Guitar:
                case TabType.Chords:
                case TabType.Bass:
                case TabType.Drum:
                    return true;
                default:
                    return false;
            }
        }

        #endregion

        #region Static Methods

        private static TabRating? GetRating(string style)
        {
            switch (style)
            {
                case "background-position: -60px 5%;":
                    return null;
                case "background-position: -48px 50%;":
                    return TabRating.Stars1;
                case "background-position: -36px 50%;":
                    return TabRating.Stars2;
                case "background-position: -24px 50%;":
                    return TabRating.Stars3;
                case "background-position: -12px 50%;":
                    return TabRating.Stars4;
                case "background-position: -0px 50%;":
                    return TabRating.Stars5;
                default:
                    return null;
            }
        }

        private static string RemoveTypeFromTitle(string title, TabType type)
        {
            switch (type)
            {
                case TabType.Guitar:
                    return title.Remove(title.Length - " Tab".Length);
                case TabType.Chords:
                    return title.Remove(title.Length - " Chords".Length);
                case TabType.Bass:
                    return title.Remove(title.Length - " Bass Tab".Length);
                case TabType.Drum:
                    return title.Remove(title.Length - " Drum Tab".Length);
            }

            return title;
        }

        private static TabType? GetTabType(string str)
        {
            if (str.IndexOf("Chords", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return TabType.Chords;
            if (str.IndexOf("Bass", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return TabType.Bass;
            if (str.IndexOf("Drum", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return TabType.Drum;
            if (str.IndexOf("Tab", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return TabType.Guitar;

            return null;
        }

        #endregion
    }
}