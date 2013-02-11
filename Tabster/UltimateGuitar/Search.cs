#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using HtmlAgilityPack;

#endregion

namespace Tabster.UltimateGuitar
{
    public class SearchResult
    {
        internal SearchResult(string artst, string song, UltimateGuitarTabType type, int rating, int votes, string url)
        {
            Artist = artst;
            Song = song;
            Type = type;
            Rating = rating;
            Votes = votes;
            URL = url;
        }

        public string Artist { get; private set; }
        public string Song { get; private set; }
        public UltimateGuitarTabType Type { get; private set; }
        public string URL { get; private set; }
        public int Rating { get; private set; }
        public int Votes { get; private set; }
    }

    public class SearchQuery : IEnumerable<SearchResult>
    {
        private readonly List<SearchResult> _results = new List<SearchResult>();

        private string Artist { get; set; }
        private string Song { get; set; }
        public UltimateGuitarTabType Type { get; set; }
        private Uri URL { get; set; }

        internal SearchQuery(string artist, string song, UltimateGuitarTabType type)
        {
            Artist = artist.Trim();
            Song = song.Trim();
            Type = type;
        }

        public SearchResult Find(Predicate<SearchResult> match)
        {
            foreach (var result in _results)
            {
                if (match(result))
                {
                    return result;
                }
            }

            return null;
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

            return UltimateGuitarTabType.Undefined;
        }

        public void BeginSearch()
        {
            var searchString = (Artist + " " + Song).Trim().Replace(" ", "+"); // (Artist.Trim().Replace(" ", "+") + " " + Song.Trim().Replace(" ", "+")).Trim();

            //Console.WriteLine("serachstring: " + searchString);

            URL = Type == UltimateGuitarTabType.Undefined
                      ? new Uri(string.Format("http://www.ultimate-guitar.com/search.php?w=songs&s={0}", searchString))
                      : new Uri(string.Format("http://www.ultimate-guitar.com/search.php?w=songs&s={0}&type={1}", searchString, (int) Type));

            string data;

            //Console.WriteLine(URL);

            using (var client = new TabsterWebClient())
            {
                data = client.DownloadString(URL);
            }

            if (data.Length == 0)
                return;

            if (data.Contains("No results"))
                return;

            var doc = new HtmlDocument();
            doc.LoadHtml(data);

            var tresults = doc.DocumentNode.SelectSingleNode("//table[@class='tresults']");

            if (tresults == null)
            {
                return;
            }

            //Console.WriteLine(tresults.InnerHtml);

            var count = 0;
            var rows = tresults.SelectNodes("tr");
            var loopArtist = ""; //store last artist

            _results.Clear();

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

                    /*
                    if (row.FirstChild.Attributes["id"] != null)
                    {
                        if (firstChild.Attributes["id"].Value == "npd77")
                        {
                            attemptedBreaking = true;
                        }
                    }

                    else
                    {
                        Console.WriteLine(firstChild.InnerText);
                        attemptedBreaking = firstChild.InnerText.Equals("THIS APP DOESN'T HAVE RIGHTS TO DISPLAY TABS", StringComparison.InvariantCultureIgnoreCase);
                    }
                    */
                    if (attemptedBreaking)
                    {
                        //Console.WriteLine("ATTEMPTED PARSE BREAK DETECTED, UPDATING INDEXES");
                        colIndexArtist += 1;
                        colIndexSong += 1;
                        colIndexRating += 1;
                        colIndexType += 1;
                    }

                    //check if row has attempted parsing breaking
                    //THIS APP DOESN'T HAVE RIGHTS TO DISPLAY TABS
                    /*
                    if (row.Attributes["class"].Value != null)
                    {
                        if (row.Attributes["class"].Value != "npd77")
                        {
                            continue;
                        }
                    }*/

                    var rowType = GetTabType(columns[colIndexType].InnerText);

                    var rowArtist = columns[colIndexArtist].InnerText;

                    if ((string.IsNullOrEmpty(loopArtist) || loopArtist != rowArtist) && rowArtist != "&nbsp;")
                    {
                        //Console.WriteLine("Loop artist updated from " + (loopArtist ?? "NULL") + " to " + rowArtist);
                        loopArtist = HttpUtility.HtmlDecode(rowArtist);
                    }

                    var rowURL = columns[colIndexSong].ChildNodes["a"].Attributes["href"].Value;
                    var rowSong = HttpUtility.HtmlDecode(columns[colIndexSong].ChildNodes["a"].InnerText);

                    var ratingColumn = columns[colIndexRating];
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

                    if (rowType == UltimateGuitarTabType.GuitarTab || rowType == UltimateGuitarTabType.GuitarChords || rowType == UltimateGuitarTabType.BassTab || rowType == UltimateGuitarTabType.DrumTab)
                    {
                        var tab = new SearchResult(loopArtist, rowSong, rowType, rowRating, rowVotes, rowURL);
                        _results.Add(tab);
                    }
                }

                count++;
            }
        }

        #region Implementation of IEnumerable

        public IEnumerator<SearchResult> GetEnumerator()
        {
            foreach (var r in _results)
            {
                yield return r;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}