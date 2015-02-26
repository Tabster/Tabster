#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using Tabster.Core.Searching;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Xml;

#endregion

namespace Songsterr
{
    public class SongsterrSearch : ITablatureSearchEngine
    {
        public SongsterrSearch()
        {
            Homepage = new Uri("http://www.songsterr.com/");
        }

        #region Implementation of ITablatureSearchEngine

        public string Name
        {
            get { return "Songsterr"; }
        }

        public Uri Homepage { get; private set; }
        public bool RequiresArtistParameter { get; private set; }
        public bool RequiresTitleParameter { get; private set; }
        public bool RequiresTypeParamter { get; private set; }

        public bool SupportsRatings
        {
            get { return false; }
        }

        public bool SupportsPrefilteredTypes { get; private set; }

        public TablatureSearchResult[] Search(TablatureSearchQuery query, WebProxy proxy)
        {
            var results = new List<TablatureSearchResult>();

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            var urlBuilder = new UriBuilder("http://www.songsterr.com/a/wa/search?pattern=");

            var searchQuery = "";

            if (!string.IsNullOrEmpty(query.Artist))
                searchQuery += query.Artist;

            if (!string.IsNullOrEmpty(query.Title))
                searchQuery += string.Format("{0}{1}", searchQuery.Length > 0 ? " " : "", query.Title);

            queryString["pattern"] = HttpUtility.UrlEncode(searchQuery);

            if (query.Type != null)
            {
                var typeStr = "";

                if (query.Type == TablatureType.Guitar)
                    typeStr = "gtr";
                if (query.Type == TablatureType.Bass)
                    typeStr = "bass";
                if (query.Type == TablatureType.Drum)
                    typeStr = "drum";


                if (typeStr != string.Empty)
                    queryString["inst"] = typeStr;
            }

            //songsterr hides tabs when this filter is set, for some reason
            //queryString["tabType"] = "text";

            urlBuilder.Query = queryString.ToString();

            string data;

            var client = new WebClient {Proxy = proxy};
            {
                data = client.DownloadString(urlBuilder.Uri);
            }

            if (data.Length > 0 && !data.Contains("Sorry, we couldn't find"))
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(data);

                var container = doc.DocumentNode.SelectSingleNode("//div[@class='yui-gb']");

                if (container != null)
                {
                    var items = container.SelectNodes(".//div[@class='item']");

                    foreach (var item in items)
                    {
                        var textIcon = item.SelectSingleNode(".//div/a[@class='text-guitar-tab']");

                        if (textIcon != null)
                        {
                            var tabURL = new Uri(new Uri("http://songsterr.com/"), textIcon.Attributes["href"].Value);
                            var tabType = Common.GetTabTypeFromURL(tabURL);

                            if (tabType != null && query.Type != null && tabType != query.Type)
                                continue;

                            if (tabType != null)
                            {
                                var tabLink = item.SelectSingleNode("a");
                                var tabArtist = tabLink.SelectSingleNode("strong").InnerText.Trim();
                                var tabTitle = tabLink.InnerHtml.Substring(tabLink.InnerHtml.IndexOf(" - ") + 6).Trim();

                                var tab = new TablatureDocument(tabArtist, tabTitle, tabType) {Source = tabURL};
                                results.Add(new TablatureSearchResult(query, this, tab, tabURL));
                            }
                        }
                    }
                }
            }

            return results.ToArray();
        }

        public bool SupportsTabType(TablatureType type)
        {
            return type == TablatureType.Guitar ||
                   type == TablatureType.Bass ||
                   type == TablatureType.Drum;
        }

        #endregion
    }
}