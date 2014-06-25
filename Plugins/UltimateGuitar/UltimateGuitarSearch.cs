#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using Tabster.Core;
using Tabster.Core.Plugins;

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

        public IRemoteTab[] Search(string artist, string title, TabType type)
        {
            var results = new List<IRemoteTab>();

            var searchString = (artist + " " + title).Trim().Replace(" ", "+");

            var urlString = string.Format("http://www.ultimate-guitar.com/search.php?w=songs&s={0}", searchString);

            if (type != TabType.Undefined)
            {
                urlString += string.Format("&type={0}", (int) type);
            }

            var url = new Uri(urlString);

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

                            var ugTabType = GetTabType(columns[colIndexType].InnerText);
                            var rowType = ConvertTabType(ugTabType);


                            var rowArtist = columns[colIndexArtist].InnerText;

                            if ((string.IsNullOrEmpty(loopArtist) || loopArtist != rowArtist) && rowArtist != "&nbsp;")
                            {
                                loopArtist = HttpUtility.HtmlDecode(rowArtist);
                            }

                            var rowURL = columns[colIndexSong].ChildNodes["a"].Attributes["href"].Value;
                            var rowSong = HttpUtility.HtmlDecode(columns[colIndexSong].ChildNodes["a"].InnerText);

                            var ratingColumn = columns[colIndexRating];

                            //todo rating and votes

                            var rowRating = 0;
                            var rowVotes = 0;

                            //get rating/votes
                            if (ratingColumn.InnerText.Contains("["))
                            {
                                var starSpan = ratingColumn.ChildNodes["span"].ChildNodes["span"];
                                var voteSpan = ratingColumn.LastChild;

                                if (starSpan != null)
                                {
                                    Int32.TryParse(starSpan.Attributes["class"].Value.Replace("r_", ""), out rowRating);
                                }

                                if (voteSpan != null)
                                {
                                    Int32.TryParse(voteSpan.InnerText.Replace("[", "").Replace("]", ""), out rowVotes);
                                }
                            }

                            if (rowType == TabType.Guitar || rowType == TabType.Chords || rowType == TabType.Bass || rowType == TabType.Drum || rowType == TabType.Ukulele)
                            {
                                var tab = new UltimateGuitarTab(new Uri(rowURL), loopArtist, rowSong, rowType, null);
                                results.Add(tab);
                            }
                        }

                        count++;
                    }
                }
            }

            return results.ToArray();
        }

        #endregion

        #region Static Methods

        private static TabType ConvertTabType(UltimateGuitarTabType type)
        {
            switch (type)
            {
                case UltimateGuitarTabType.GuitarTab:
                    return TabType.Guitar;
                case UltimateGuitarTabType.GuitarChords:
                    return TabType.Chords;
                case UltimateGuitarTabType.BassTab:
                    return TabType.Bass;
                case UltimateGuitarTabType.DrumTab:
                    return TabType.Drum;
                case UltimateGuitarTabType.Ukulele:
                    return TabType.Ukulele;
                default:
                    return TabType.Undefined;
            }
        }

        private static UltimateGuitarTabType ConvertTabType(TabType type)
        {
            switch (type)
            {
                case TabType.Guitar:
                    return UltimateGuitarTabType.GuitarTab;
                case TabType.Chords:
                    return UltimateGuitarTabType.GuitarChords;
                case TabType.Bass:
                    return UltimateGuitarTabType.BassTab;
                case TabType.Drum:
                    return UltimateGuitarTabType.DrumTab;
                case TabType.Ukulele:
                    return UltimateGuitarTabType.Ukulele;
                default:
                    return UltimateGuitarTabType.Undefined;
            }
        }

        private static UltimateGuitarTabType GetTabType(string str)
        {
            if (str.Equals("tab", StringComparison.InvariantCultureIgnoreCase))
                return UltimateGuitarTabType.GuitarTab;
            if (str.Equals("chords", StringComparison.InvariantCultureIgnoreCase))
                return UltimateGuitarTabType.GuitarChords;
            if (str.Equals("bass", StringComparison.InvariantCultureIgnoreCase))
                return UltimateGuitarTabType.BassTab;
            if (str.Equals("drums", StringComparison.InvariantCultureIgnoreCase))
                return UltimateGuitarTabType.DrumTab;
            if (str.Equals("guitar pro", StringComparison.InvariantCultureIgnoreCase))
                return UltimateGuitarTabType.GuitarPro;
            if (str.Equals("power tab", StringComparison.InvariantCultureIgnoreCase))
                return UltimateGuitarTabType.PowerTab;
            if (str.Equals("video lesson", StringComparison.InvariantCultureIgnoreCase))
                return UltimateGuitarTabType.Video;
            if (str.Equals("tab pro", StringComparison.InvariantCultureIgnoreCase))
                return UltimateGuitarTabType.TabPro;
            if (str.Equals("ukulele", StringComparison.InvariantCultureIgnoreCase))
                return UltimateGuitarTabType.Ukulele;

            return UltimateGuitarTabType.Undefined;
        }

        #endregion
    }
}