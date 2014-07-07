#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tabster.Core;
using Tabster.Core.Plugins;

#endregion

namespace Tabster.Forms
{
    partial class MainForm
    {
        private readonly List<SearchResult> _searchResults = new List<SearchResult>();
        private readonly Dictionary<Uri, Tab> _searchResultsCache = new Dictionary<Uri, Tab>();
        private List<ISearchService> _searchServices = new List<ISearchService>();
        private List<ITabParser> _tabParsers = new List<ITabParser>();

        private SearchResult SelectedSearchResult()
        {
            var selectedURL = searchDisplay.SelectedRows.Count > 0 ? new Uri(searchDisplay.SelectedRows[0].Tag.ToString()) : null;
            return selectedURL != null ? _searchResults.Find(x => x.Tab.Source.Equals(selectedURL)) : null;
        }

        private void onlinesearchbtn_Click(object sender, EventArgs e)
        {
            if (txtsearchartist.Text.Trim().Length > 0 || txtsearchsong.Text.Trim().Length > 0)
            {
                if (SearchBackgroundWorker.IsBusy)
                    SearchBackgroundWorker.CancelAsync();

                pictureBox1.Visible = true;

                var searchArtist = txtsearchartist.Text.Trim();
                var searchTitle = txtsearchsong.Text.Trim();

                TabType? searchType = null;

                //ignore "all tabs"
                if (txtsearchtype.SelectedIndex > 0)
                    searchType = (TabType) (txtsearchtype.SelectedIndex - 1);

                var searchQueries = new List<SearchQuery>();

                foreach (var service in _searchServices)
                {
                    if (((service.Flags & SearchServiceFlags.RequiresArtistParameter) == SearchServiceFlags.RequiresArtistParameter && string.IsNullOrEmpty(searchArtist)) ||
                        (((service.Flags & SearchServiceFlags.RequiresTitleParameter) == SearchServiceFlags.RequiresTitleParameter && string.IsNullOrEmpty(searchTitle))) ||
                        (((service.Flags & SearchServiceFlags.RequiresTypeParamter) == SearchServiceFlags.RequiresTypeParamter && !searchType.HasValue)))
                    {
                        continue;
                    }

                    searchQueries.Add(new SearchQuery(service, searchArtist, searchTitle, searchType));
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
                    var newRow = new DataGridViewRow {Tag = result.Tab.Source.ToString()};

                    var ratingString = "";

                    if (result.Rating != SearchResultRating.None)
                        ratingString = new string('\u2605', (int) result.Rating - 1).PadRight(5, '\u2606');

                    newRow.CreateCells(searchDisplay, result.Tab.Artist, result.Tab.Title, Tab.GetTabString(result.Tab.Type), ratingString, result.Query.Service.Name);
                    searchDisplay.Rows.Add(newRow);
                }
            }

            searchDisplay.ResumeLayout();

            lblsearchresults.Visible = true;
            lblsearchresults.Text = string.Format("Results: {0}", searchDisplay.Rows.Count);
            pictureBox1.Visible = false;
            lblSearchStatus.Visible = false;
        }

        private void SearchBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblSearchStatus.Visible = true;

            var serviceName = _searchServices[e.ProgressPercentage - 1].Name;
            if (serviceName != null)
                lblSearchStatus.Text = string.Format("Searching: {0}", serviceName);
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

                        cachedTab.SourceType = TabSource.Download;

                        var tabFile = TabFile.Create(cachedTab, Program.libraryManager.TabsDirectory);
                        Program.libraryManager.AddTab(tabFile, true);
                        UpdateLibraryItem(tabFile);
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

                Tab tab;

                try
                {
                    tab = parser.ParseTabFromSource(urlSource, null);
                }

                catch (Exception ex)
                {
                    throw new TabParserException(ex.Message);
                }

                if (tab != null)
                {
                    tab.Source = result.Tab.Source;
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
    }
}