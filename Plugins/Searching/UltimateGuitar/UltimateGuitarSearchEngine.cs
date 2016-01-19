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

namespace UltimateGuitar
{
    public class UltimateGuitarSearchEngine : ITablatureSearchEngine
    {
        public UltimateGuitarSearchEngine()
        {
            Homepage = new Uri("http://ultimate-guitar.com");
        }

        #region Implementation of ISearchService

        public string Name
        {
            get { return "Ultimate Guitar"; }
        }

        public Uri Homepage { get; private set; }

        public bool RequiresArtistParameter
        {
            get { return false; }
        }

        public bool RequiresTitleParameter
        {
            get { return false; }
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

            var regex = new Regex("[']", RegexOptions.Compiled);

            var urlEncodedQuery = Uri.EscapeDataString(string.Format("{0} {1}", regex.Replace(query.Artist, ""),
                regex.Replace(query.Title, "")));

            string typeStr = null;

            if (query.Type != null)
            {
                if (query.Type == TablatureType.Guitar)
                {
                    typeStr = "200";
                }
                else if (query.Type == TablatureType.Chords)
                {
                    typeStr = "300";
                }
                else if (query.Type == TablatureType.Bass)
                {
                    typeStr = "400";
                }
                else if (query.Type == TablatureType.Drum)
                {
                    typeStr = "700";
                }
                else if (query.Type == TablatureType.Ukulele)
                {
                    typeStr = "800";
                }
            }

            var urlBuilder = new StringBuilder();
            urlBuilder.AppendFormat("http://www.ultimate-guitar.com/search.php?search_type=title&value={0}",
                urlEncodedQuery);

            if (!string.IsNullOrEmpty(typeStr))
                urlBuilder.AppendFormat("&type={0}", typeStr);

            var url = new Uri(urlBuilder.ToString());

            string data;

            using (var client = new WebClient {Proxy = proxy, Encoding = Encoding.UTF8})
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

                            var attemptedBreaking =
                                row.InnerHtml.Contains("THIS APP DOESN'T HAVE RIGHTS TO DISPLAY TABS");

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

                            if (rowType != null)
                            {
                                var rowURL = columns[colIndexSong].ChildNodes["a"].Attributes["href"].Value;
                                var rowSong = HttpUtility.HtmlDecode(columns[colIndexSong].ChildNodes["a"].InnerText);

                                var rating = TablatureRating.None;

                                var ratingColumn = columns[colIndexRating];

                                if (ratingColumn.InnerText.Contains("["))
                                {
                                    var ratingSpan = ratingColumn.ChildNodes["span"].ChildNodes["span"];

                                    if (ratingSpan != null)
                                    {
                                        int rowRating;
                                        if (Int32.TryParse(ratingSpan.Attributes["class"].Value.Replace("r_", ""),
                                            out rowRating))
                                        {
                                            rating = GetRating(rowRating);
                                        }
                                    }
                                }

                                if (query.Type == null || rowType == query.Type)
                                {
                                    var tab = new AttributedTablature(loopArtist, rowSong, rowType);
                                    results.Add(new TablatureSearchResult(query, this, tab, new Uri(rowURL), rating));
                                }
                            }
                        }

                        count++;
                    }
                }
            }

            return results.ToArray();
        }

        public bool SupportsTabType(TablatureType type)
        {
            return type == TablatureType.Guitar || type == TablatureType.Chords || type == TablatureType.Bass ||
                   type == TablatureType.Drum || type == TablatureType.Ukulele;
        }

        #endregion

        #region Static Methods

        private static TablatureRating GetRating(int value)
        {
            switch (value)
            {
                case 1:
                    return TablatureRating.Stars1;
                case 2:
                    return TablatureRating.Stars2;
                case 3:
                    return TablatureRating.Stars3;
                case 4:
                    return TablatureRating.Stars4;
                case 5:
                    return TablatureRating.Stars5;
            }

            return TablatureRating.None;
        }

        private static TablatureType GetTabType(string str)
        {
            if (str.Equals("tab", StringComparison.InvariantCultureIgnoreCase))
                return TablatureType.Guitar;
            if (str.Equals("chords", StringComparison.InvariantCultureIgnoreCase))
                return TablatureType.Chords;
            if (str.Equals("bass", StringComparison.InvariantCultureIgnoreCase))
                return TablatureType.Bass;
            if (str.Equals("drums", StringComparison.InvariantCultureIgnoreCase))
                return TablatureType.Drum;
            if (str.Equals("ukulele", StringComparison.InvariantCultureIgnoreCase))
                return TablatureType.Ukulele;

            return null;
        }

        #endregion
    }
}