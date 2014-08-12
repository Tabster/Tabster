#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tabster.Core.FileTypes;
using Tabster.Core.Parsing;
using Tabster.Core.Plugins;
using Tabster.Core.Searching;
using Tabster.Core.Types;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    partial class MainForm
    {
        private readonly List<SearchResult> _searchResults = new List<SearchResult>();
        private readonly Dictionary<Uri, TablatureDocument> _searchResultsCache = new Dictionary<Uri, TablatureDocument>();
        private TabRating? _activeSearchRating;
        private TabType? _activeSearchType;
        private List<ISearchService> _searchServices = new List<ISearchService>();
        private List<IWebTabParser> _tabParsers = new List<IWebTabParser>();

        //used for filtering after search is complete

        private SearchResult SelectedSearchResult()
        {
            var selectedURL = searchDisplay.SelectedRows.Count > 0 ? new Uri(searchDisplay.SelectedRows[0].Tag.ToString()) : null;
            return selectedURL != null ? _searchResults.Find(x => x.Tab.Source.Equals(selectedURL)) : null;
        }

        private void onlinesearchbtn_Click(object sender, EventArgs e)
        {
            if (listSearchServices.SelectedItems.Count > 0 && (txtSearchArtist.Text.Trim().Length > 0 || txtSearchTitle.Text.Trim().Length > 0))
            {
                if (SearchBackgroundWorker.IsBusy)
                    SearchBackgroundWorker.CancelAsync();

                var searchArtist = txtSearchArtist.Text.Trim();
                var searchTitle = txtSearchTitle.Text.Trim();

                _activeSearchType = null;

                //todo don't use TabType->int cast
                //ignore "all tabs"
                if (txtSearchType.SelectedIndex > 0)
                    _activeSearchType = (TabType) (txtSearchType.SelectedIndex - 1);

                _activeSearchRating = null;

                if (cbSearchRating.SelectedIndex > 0)
                    _activeSearchRating = (TabRating)(cbSearchRating.SelectedIndex);

                var searchQueries = new List<SearchQuery>();

                var selectedServices = new List<ISearchService>();

                for (var i = 0; i < listSearchServices.Items.Count; i++)
                {
                    if (listSearchServices.GetSelected(i))
                    {
                        selectedServices.Add(_searchServices[i]);
                    }
                }

                foreach (var service in selectedServices)
                {
                    if (((service.Flags & SearchServiceFlags.RequiresArtistParameter) == SearchServiceFlags.RequiresArtistParameter && string.IsNullOrEmpty(searchArtist)) ||
                        (((service.Flags & SearchServiceFlags.RequiresTitleParameter) == SearchServiceFlags.RequiresTitleParameter && string.IsNullOrEmpty(searchTitle))) ||
                        (((service.Flags & SearchServiceFlags.RequiresTypeParamter) == SearchServiceFlags.RequiresTypeParamter && !_activeSearchType.HasValue)))
                    {
                        continue;
                    }

                    searchQueries.Add(new SearchQuery(service, searchArtist, searchTitle, _activeSearchType));
                }

                if (searchQueries.Count > 0)
                {
                    SearchBackgroundWorker.RunWorkerAsync(searchQueries);
                }
            }
        }

        private void SearchBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var totalResults = new List<SearchResult>();

            var count = 0;

            var queries = (List<SearchQuery>) e.Argument;

            foreach (var query in queries)
            {
                try
                {
                    if (!query.Type.HasValue || query.Service.SupportsTabType(query.Type.Value))
                    {
                        var results = query.Service.Search(query);

                        if (results != null)
                        {
                            totalResults.AddRange(results);
                        }
                    }
                }

                catch
                {
                    //unhandled
                }

                count++;

                SearchBackgroundWorker.ReportProgress(count);
            }

            e.Result = totalResults;
        }

        private void SearchBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            searchDisplay.SuspendLayout();
            searchDisplay.Rows.Clear();
            _searchResults.Clear();

            if (e.Result != null)
            {
                var results = (List<SearchResult>) e.Result;

                _searchResults.AddRange(results);

                foreach (var result in results)
                {
                    //subpar rating
                    if (_activeSearchRating.HasValue && (!result.Rating.HasValue || result.Rating.Value <= _activeSearchRating.Value))
                        continue;

                    //tab type mismatch
                    if (_activeSearchType.HasValue && result.Tab.Type != _activeSearchType.Value)
                        continue;

                    var newRow = new DataGridViewRow {Tag = result.Tab.Source.ToString()};

                    var ratingString = "";

                    if (result.Rating.HasValue)
                        ratingString = new string('\u2605', (int) result.Rating - 1).PadRight(5, '\u2606');

                    newRow.CreateCells(searchDisplay, result.Tab.Artist, result.Tab.Title, result.Tab.Type.ToFriendlyString(), ratingString, result.Query.Service.Name);
                    searchDisplay.Rows.Add(newRow);
                }
            }

            searchDisplay.ResumeLayout();

            lblStatus.Text = string.Format("Search Results: {0}", searchDisplay.Rows.Count);
        }

        private void SearchBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var serviceName = _searchServices[e.ProgressPercentage - 1].Name;
            if (serviceName != null)
                lblStatus.Text = string.Format("Searching: {0}", serviceName);
        }

        private void dataGridViewExtended1_MouseClick(object sender, MouseEventArgs e)
        {
            var currentMouseOverRow = searchDisplay.HitTest(e.X, e.Y).RowIndex;

            if (e.Button == MouseButtons.Right && (currentMouseOverRow >= 0 && currentMouseOverRow < searchDisplay.Rows.Count))
            {
                searchDisplay.Rows[currentMouseOverRow].Selected = true;
                SearchMenu.Show(searchDisplay.PointToScreen(e.Location));
            }
        }

        private void dataGridViewExtended1_SelectionChanged(object sender, EventArgs e)
        {
            LoadSelectedPreview();

            var selectedResult = SelectedSearchResult();
            saveTabToolStripMenuItem1.Enabled = selectedResult != null && _searchResultsCache.ContainsKey(selectedResult.Tab.Source);
        }

        private void SaveSelectedTab(object sender, EventArgs e)
        {
            var selectedResult = SelectedSearchResult();

            if (selectedResult != null)
            {
                using (var nt = new NewTabDialog(selectedResult.Tab.Artist, selectedResult.Tab.Title, selectedResult.Tab.Type))
                {
                    if (nt.ShowDialog() == DialogResult.OK)
                    {
                        var cachedTab = _searchResultsCache[selectedResult.Tab.Source];

                        Program.libraryManager.Add(cachedTab, true);
                        UpdateLibraryItem(cachedTab);
                    }
                }
            }
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = SelectedSearchResult();

            if (result != null)
            {
                Clipboard.SetDataObject(result.Tab.Source.ToString());
            }
        }

        private void LoadSelectedPreview()
        {
            var selectedResult = SelectedSearchResult();

            if (selectedResult != null)
            {
                if (SearchPreviewBackgroundWorker.IsBusy)
                    SearchPreviewBackgroundWorker.CancelAsync();

                searchPreviewEditor.SetText("Loading Preview...");

                if (!SearchPreviewBackgroundWorker.IsBusy)
                    SearchPreviewBackgroundWorker.RunWorkerAsync(selectedResult.Tab.Source);
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (searchSplitContainer.Panel2Collapsed)
                TogglePreviewPane(sender, e);

            LoadSelectedPreview();
        }

        private void SearchPreviewBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var url = (Uri) e.Argument;

            e.Result = url;

            var result = _searchResults.Find(x => x.Tab.Source == url);

            if (!_searchResultsCache.ContainsKey(url))
            {
                var parser = result.Query.Service.Parser ?? _tabParsers.Find(x => x.MatchesUrlPattern(result.Tab.Source));

                if (parser == null)
                {
                    throw new TabParserException(string.Format("No parser found for URL: {0}", url));
                }

                string urlSource;

                using (var client = new TabsterWebClient())
                {
                    urlSource = client.DownloadString(result.Tab.Source);
                }

                TablatureDocument tab;

                try
                {
                    tab = parser.Parse(urlSource, null);
                }

                catch (Exception ex)
                {
                    throw new TabParserException(ex.Message);
                }

                if (tab != null)
                {
                    tab.Source = result.Tab.Source;
                    tab.SourceType = TablatureSourceType.Download;
                    tab.Method = Common.GetTablatureDocumentMethodString(parser);
                    _searchResultsCache[result.Tab.Source] = tab;
                }
            }
        }

        private void SearchPreviewBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                searchPreviewEditor.SetText(string.Format("Tab preview failed:{0}{0}{1}", Environment.NewLine, e.Error.Message));
            }

            if (!e.Cancelled && e.Error == null)
            {
                var url = (Uri) e.Result;

                if (_searchResultsCache.ContainsKey(url))
                {
                    var tab = _searchResultsCache[url];
                    searchPreviewEditor.LoadTab(tab);

                    if (!searchhiddenpreviewToolStripMenuItem.Checked && searchSplitContainer.Panel2Collapsed)
                    {
                        searchSplitContainer.Panel2Collapsed = false;
                        previewToolStrip.Enabled = previewToolStripMenuItem.Enabled = searchSplitContainer.Panel2Collapsed;
                    }

                    //enable save tab option
                    var selectedResult = SelectedSearchResult();

                    if (selectedResult != null && selectedResult.Tab.Source == url)
                    {
                        saveTabToolStripMenuItem1.Enabled = true;
                    }
                }
            }
        }

        private static string RemoveVersionConventionFromTitle(string title)
        {
            var versionConventionIndex = title.IndexOf(" (ver ", StringComparison.InvariantCultureIgnoreCase);

            if (versionConventionIndex >= 0)
                title = title.Remove(versionConventionIndex);

            return title;
        }

        private void BuildSearchSuggestions()
        {
            var artistStrings = new List<string>();
            var titleStrings = new List<string>();

            var tabs = new List<TablatureDocument>();

            foreach (var tab in Program.libraryManager)
                tabs.Add(tab);

            foreach (var playlist in Program.libraryManager.Playlists)
            {
                foreach (var tab in playlist)
                {
                    tabs.Add(tab);
                }
            }

            foreach (var tab in tabs)
            {
                if (artistStrings.Find(x => x.Equals(tab.Artist, StringComparison.InvariantCultureIgnoreCase)) == null)
                {
                    artistStrings.Add(tab.Artist);
                }

                var title = RemoveVersionConventionFromTitle(tab.Title);

                if (titleStrings.Find(x => x.Equals(title, StringComparison.InvariantCultureIgnoreCase)) == null)
                {
                    titleStrings.Add(title);
                }
            }

            var artistSuggestions = new AutoCompleteStringCollection();
            var titleSuggestions = new AutoCompleteStringCollection();

            foreach (var str in artistStrings)
                artistSuggestions.Add(str);

            foreach (var str in titleStrings)
                titleSuggestions.Add(str);

            txtSearchArtist.AutoCompleteCustomSource = artistSuggestions;
            txtSearchTitle.AutoCompleteCustomSource = titleSuggestions;
        }

        private void InitializeSearchControls()
        {
            txtSearchArtist.Text = "";
            txtSearchTitle.Text = "";

            txtSearchType.SelectedIndex = 0;

            listSearchServices.Items.Clear();

            cbSearchRating.SelectedIndex = 0;

            for (var i = 0; i < _searchServices.Count; i++)
            {
                var service = _searchServices[i];
                listSearchServices.Items.Add(service.Name);
                listSearchServices.SetSelected(i, true);
            }
        }

        private void resetSearchbtn_Click(object sender, EventArgs e)
        {
            InitializeSearchControls();
        }
    }
}