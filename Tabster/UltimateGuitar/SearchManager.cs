#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using HtmlAgilityPack;

#endregion

namespace Tabster.UltimateGuitar
{
    public class SearchResult
    {
        internal SearchResult(string artst, string title, TabType type, int rating, int votes, Uri url)
        {
            Artist = artst;
            Title = title;
            Type = type;
            Rating = rating;
            Votes = votes;
            URL = url;
        }

        public string Artist { get; private set; }
        public string Title { get; private set; }
        public TabType Type { get; private set; }
        public Uri URL { get; private set; }
        public int Rating { get; private set; }
        public int Votes { get; private set; }
    }

    public class SearchEventArgs : EventArgs
    {
        public readonly Exception Error;

        public SearchEventArgs(Exception error = null)
        {
            Error = error;
        }
    }

    public class SearchManager : IEnumerable<SearchResult>
    {
        #region Delegates

        public delegate void SearchHandler(object sender, SearchEventArgs e);

        #endregion

        private static readonly BackgroundWorker _bgWorker = new BackgroundWorker();
        private readonly List<SearchResult> _results = new List<SearchResult>();
        private Uri URL;
        private TabType _type = TabType.Undefined;

        public SearchManager()
        {
            _bgWorker.WorkerSupportsCancellation = true;
            _bgWorker.DoWork += bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
        }

        public string Artist { get; set; }
        public string Title { get; set; }

        public TabType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public event SearchHandler Completed;

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Completed != null)
                Completed(this, new SearchEventArgs(e.Error));
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SearchTab();
            }

            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        private void SearchTab()
        {
            _results.Clear();

            var searchString = (Artist + " " + Title).Trim().Replace(" ", "+");

            URL = Type == TabType.Undefined
                      ? new Uri(string.Format("http://www.ultimate-guitar.com/search.php?w=songs&s={0}", searchString))
                      : new Uri(string.Format("http://www.ultimate-guitar.com/search.php?w=songs&s={0}&type={1}", searchString, (int) Type));

            string data;

            var client = new TabsterWebClient();
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

                    var rowType = GetTabType(columns[colIndexType].InnerText);


                    var rowArtist = columns[colIndexArtist].InnerText;

                    if ((string.IsNullOrEmpty(loopArtist) || loopArtist != rowArtist) && rowArtist != "&nbsp;")
                    {
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

                    if (rowType == TabType.GuitarTab || rowType == TabType.GuitarChords || rowType == TabType.BassTab || rowType == TabType.DrumTab || rowType == TabType.Ukulele)
                    {
                        var tab = new SearchResult(loopArtist, rowSong, rowType, rowRating, rowVotes, new Uri(rowURL));
                        _results.Add(tab);
                    }
                }

                count++;
            }
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

        public void Search()
        {
            if (_bgWorker.IsBusy)
                _bgWorker.CancelAsync();

            if (!_bgWorker.IsBusy)
                _bgWorker.RunWorkerAsync();
        }

        private static TabType GetTabType(string str)
        {
            if (str.Equals("tab", StringComparison.InvariantCultureIgnoreCase))
                return TabType.GuitarTab;
            if (str.Equals("chords", StringComparison.InvariantCultureIgnoreCase))
                return TabType.GuitarChords;
            if (str.Equals("bass", StringComparison.InvariantCultureIgnoreCase))
                return TabType.BassTab;
            if (str.Equals("drums", StringComparison.InvariantCultureIgnoreCase))
                return TabType.DrumTab;
            if (str.Equals("guitar pro", StringComparison.InvariantCultureIgnoreCase))
                return TabType.GuitarPro;
            if (str.Equals("power tab", StringComparison.InvariantCultureIgnoreCase))
                return TabType.PowerTab;
            if (str.Equals("video lesson", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Video;
            if (str.Equals("tab pro", StringComparison.InvariantCultureIgnoreCase))
                return TabType.TabPro;
            if (str.Equals("ukulele", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Ukulele;

            return TabType.Undefined;
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