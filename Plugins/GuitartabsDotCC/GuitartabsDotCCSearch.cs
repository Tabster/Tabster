#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using Tabster.Core;
using Tabster.Core.Plugins;

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

        public ITabParser Parser
        {
            get { return new GuitartabsDotCCParser(); }
        }

        public SearchServiceFlags Flags
        {
            get { return SearchServiceFlags.RequiresTitleParameter; }
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

                    if (tabType.HasValue)
                    {
                        rowTitle = RemoveTypeFromTitle(rowTitle, tabType.Value);

                        var tab = new Tab(rowArtist, rowTitle, tabType.Value, null) {Source = new Uri(string.Format("http://guitartabs.cc{0}", rowUrl))};
                        results.Add(new SearchResult(query, tab));
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
            if (str.Equals("Chords", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Chords;
            if (str.Equals("Bass", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Bass;
            if (str.Equals("Drum", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Drum;
            if (str.Equals("Tab", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Guitar;

            return null;
        }

        #endregion
    }
}