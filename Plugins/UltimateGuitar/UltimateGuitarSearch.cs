#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using Tabster.Core.FileTypes;
using Tabster.Core.Plugins;
using Tabster.Core.Types;

#endregion

namespace UltimateGuitar
{
    public class UltimateGuitarSearch : ISearchService
    {
        #region Implementation of ISearchService

        public string Name
        {
            get { return "Ultimate Guitar"; }
        }

        public ITabParser Parser
        {
            get { return new UltimateGuitarParser(); }
        }

        public SearchServiceFlags Flags
        {
            get { return SearchServiceFlags.None; }
        }

        public SearchResult[] Search(SearchQuery query)
        {
            var results = new List<SearchResult>();

            var urlEncodedQuery = HttpUtility.UrlEncode(string.Format("{0} {1}", query.Artist, query.Title));

            string typeStr = null;

            if (query.Type.HasValue)
            {
                switch (query.Type)
                {
                    case TabType.Guitar:
                        typeStr = "200";
                        break;
                    case TabType.Chords:
                        typeStr = "300";
                        break;
                    case TabType.Bass:
                        typeStr = "400";
                        break;
                    case TabType.Drum:
                        typeStr = "700";
                        break;
                    case TabType.Ukulele:
                        typeStr = "800";
                        break;
                }
            }

            var urlBuilder = new StringBuilder();
            urlBuilder.AppendFormat("http://www.ultimate-guitar.com/search.php?w=songs&s={0}", urlEncodedQuery);

            if (!string.IsNullOrEmpty(typeStr))
                urlBuilder.AppendFormat("&type={0}", typeStr);

            var url = new Uri(urlBuilder.ToString());

            string data;

            var client = new WebClient {Proxy = null};
            {
                data = client.DownloadString(url);
            }

            if (data.Length > 0 && !data.Contains("No results"))
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(data);

                var tresults = doc.DocumentNode.SelectSingleNode("//table[@class='tresults']");

                if (tresults != null)
                {
                    var count = 0;
                    var rows = tresults.SelectNodes("tr");
                    var loopArtist = ""; //store last artist

                    foreach (var row in rows)
                    {
                        //skip first (header) row
                        if (count > 0)
                        {
                            var columns = row.SelectNodes("td");

                            //column indexes
                            var colIndexArtist = 0;
                            var colIndexSong = 1;
                            var colIndexRating = 2;
                            var colIndexType = 3;

                            var attemptedBreaking = row.InnerHtml.Contains("THIS APP DOESN'T HAVE RIGHTS TO DISPLAY TABS");

                            if (attemptedBreaking)
                            {
                                colIndexArtist += 1;
                                colIndexSong += 1;
                                colIndexRating += 1;
                                colIndexType += 1;
                            }

                            var rowArtist = columns[colIndexArtist].InnerText;

                            if ((string.IsNullOrEmpty(loopArtist) || loopArtist != rowArtist) && rowArtist != "&nbsp;")
                            {
                                loopArtist = HttpUtility.HtmlDecode(rowArtist);
                            }

                            var rowType = GetTabType(columns[colIndexType].InnerText);

                            if (rowType.HasValue)
                            {
                                var rowURL = columns[colIndexSong].ChildNodes["a"].Attributes["href"].Value;
                                var rowSong = HttpUtility.HtmlDecode(columns[colIndexSong].ChildNodes["a"].InnerText);

                                var rating = SearchResultRating.None;
                                var ratingColumn = columns[colIndexRating];

                                if (ratingColumn.InnerText.Contains("["))
                                {
                                    var ratingSpan = ratingColumn.ChildNodes["span"].ChildNodes["span"];

                                    if (ratingSpan != null)
                                    {
                                        int rowRating;
                                        Int32.TryParse(ratingSpan.Attributes["class"].Value.Replace("r_", ""), out rowRating);
                                        rating = (SearchResultRating) rowRating + 1;

                                        switch (rowRating)
                                        {
                                            case 1:
                                                rating = SearchResultRating.Stars1;
                                                break;
                                            case 2:
                                                rating = SearchResultRating.Stars2;
                                                break;
                                            case 3:
                                                rating = SearchResultRating.Stars3;
                                                break;
                                            case 4:
                                                rating = SearchResultRating.Stars4;
                                                break;
                                            case 5:
                                                rating = SearchResultRating.Stars5;
                                                break;
                                        }
                                    }
                                }

                                if (!query.Type.HasValue || rowType == query.Type)
                                {
                                    var tab = new TablatureDocument(loopArtist, rowSong, rowType.Value, null) {Source = new Uri(rowURL)};
                                    results.Add(new SearchResult(query, tab, rating));
                                }
                            }
                        }

                        count++;
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
                case TabType.Ukulele:
                    return true;
                default:
                    return false;
            }
        }

        #endregion

        #region Static Methods

        private static TabType? GetTabType(string str)
        {
            if (str.Equals("tab", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Guitar;
            if (str.Equals("chords", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Chords;
            if (str.Equals("bass", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Bass;
            if (str.Equals("drums", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Drum;
            if (str.Equals("ukulele", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Ukulele;

            return null;
        }

        #endregion
    }
}