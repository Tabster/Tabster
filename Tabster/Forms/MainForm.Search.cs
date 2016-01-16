#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using Tabster.Core.Searching;
using Tabster.Core.Types;
using Tabster.Data.Processing;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    internal class TablatureSearchRequest : TablatureSearchQuery
    {
        private readonly List<ITablatureSearchEngine> _searchEngines;
        public List<TablatureSearchResult> Results = new List<TablatureSearchResult>();

        public TablatureSearchRequest(string artist, string title, TablatureType type, TablatureRating rating, List<ITablatureSearchEngine> searchEngines)
            : base(artist, title, type, rating)
        {
            _searchEngines = searchEngines;
        }

        public ReadOnlyCollection<ITablatureSearchEngine> SearchEngines
        {
            get { return _searchEngines.AsReadOnly(); }
        }
    }

    internal partial class MainForm
    {
        private readonly Dictionary<Uri, AttributedTablature> _searchResultsCache = new Dictionary<Uri, AttributedTablature>();

        private List<ITablatureWebImporter> _webImporters = new List<ITablatureWebImporter>();

        private TablatureSearchResult GetSelectedSearchResult()
        {
            return listViewSearch.SelectedObject != null ? (TablatureSearchResult) listViewSearch.SelectedObject : null;
        }

        private TablatureSearchRequest CreateSearchRequest()
        {
            var artist = txtSearchArtist.Text.Trim();
            var title = txtSearchTitle.Text.Trim();
            var type = searchTypeList.HasTypeSelected ? searchTypeList.SelectedType : null;
            var rating = cbSearchRating.SelectedRating;

            var searchEngines = new List<ITablatureSearchEngine>();

            foreach (var engine in UserSettingsUtilities.GetEnabledSearchEngines())
            {
                try
                {
                    //check engine requirements
                    if ((engine.RequiresArtistParameter && string.IsNullOrEmpty(artist)) ||
                        (engine.RequiresTitleParameter && string.IsNullOrEmpty(title)) ||
                        (engine.RequiresTypeParamter && type == null) ||
                        (!engine.SupportsRatings && rating != TablatureRating.None))
                        continue;

                    //tablature type checking
                    if (type != null && !engine.SupportsTabType(type))
                        continue;

                    searchEngines.Add(engine);
                }

                catch
                {
                    //unhandled
                }
            }

            return new TablatureSearchRequest(artist, title, type, rating, searchEngines);
        }

        private void SearchBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var request = (TablatureSearchRequest) e.Argument;

            var proxy = UserSettingsUtilities.ProxySettings.Proxy;

            foreach (var engine in request.SearchEngines)
            {
                try
                {
                    var results = engine.Search(request, proxy);

                    if (results != null)
                    {
                        request.Results.AddRange(results);

                        for (var i = 0; i < results.Length; i++)
                        {
                            SearchBackgroundWorker.ReportProgress(i, results[i]);
                        }
                    }
                }

                catch
                {
                    //unhandled
                }
            }
        }

        private void SearchBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var result = (TablatureSearchResult) e.UserState;

            //missing source url
            if (result.Source == null)
                return;

            //subpar rating
            if (result.Query.Rating != TablatureRating.None && (result.Rating <= result.Query.Rating))
                return;

            //tab type mismatch
            if (result.Query.Type != null && result.Tab.Type != result.Query.Type)
                return;

            listViewSearch.AddObject(e.UserState);
        }

        private void SearchBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            onlinesearchbtn.Enabled = true;
            lblStatus.Text = string.Format("Search Results: {0}", listViewSearch.Items.Count);
        }

        private void SaveSelectedSearchResult()
        {
            var selectedResult = GetSelectedSearchResult();

            if (selectedResult != null)
            {
                using (var nt = new NewTabDialog(selectedResult.Tab.Artist, selectedResult.Tab.Title, selectedResult.Tab.Type))
                {
                    if (nt.ShowDialog() == DialogResult.OK)
                    {
                        var cachedTab = _searchResultsCache[selectedResult.Source];

                        var libraryItem = _libraryManager.Add(cachedTab);

                        //todo use objectliveview filtering instead of manual
                        if (TablatureLibraryItemVisible(SelectedLibrary(), libraryItem))
                        {
                            listViewLibrary.AddObject(libraryItem);
                        }
                    }
                }
            }
        }

        private void CopyGetSelectedSearchResultUrl()
        {
            var result = GetSelectedSearchResult();

            if (result != null)
            {
                Clipboard.SetDataObject(result.Source.ToString());
            }
        }

        private void LoadSelectedSearchResultPreview()
        {
            var selectedResult = GetSelectedSearchResult();

            if (selectedResult != null)
            {
                if (SearchPreviewBackgroundWorker.IsBusy)
                    SearchPreviewBackgroundWorker.CancelAsync();

                searchPreviewEditor.Text = "Loading Preview...";

                if (!SearchPreviewBackgroundWorker.IsBusy)
                    SearchPreviewBackgroundWorker.RunWorkerAsync(selectedResult);
            }
        }

        private void SearchPreviewBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var result = (TablatureSearchResult) e.Argument;
            e.Result = result;

            if (!_searchResultsCache.ContainsKey(result.Source))
            {
                var parser = _webImporters.Find(x => x.IsUrlParsable(result.Source));

                if (parser == null)
                {
                    throw new TablatureProcessorException(string.Format("No parser found for URL: {0}", result.Source));
                }

                var proxy = UserSettingsUtilities.ProxySettings.Proxy;

                AttributedTablature tab;

                try
                {
                    tab = parser.Parse(result.Source, proxy);
                }

                catch (Exception ex)
                {
                    throw new TablatureProcessorException(ex.Message);
                }

                if (tab != null)
                {
                    tab.Source = result.Source;
                    tab.SourceType = TablatureSourceType.Download;
                    _searchResultsCache[result.Source] = tab;
                }
            }
        }

        private void SearchPreviewBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                searchPreviewEditor.Text = string.Format("Tab preview failed:{0}{0}{1}", Environment.NewLine, e.Error.Message);
            }

            if (!e.Cancelled && e.Error == null)
            {
                var result = (TablatureSearchResult) e.Result;

                if (_searchResultsCache.ContainsKey(result.Source))
                {
                    var tab = _searchResultsCache[result.Source];
                    searchPreviewEditor.LoadTablature(tab);

                    if (!searchhiddenpreviewToolStripMenuItem.Checked && searchSplitContainer.Panel2Collapsed)
                    {
                        searchSplitContainer.Panel2Collapsed = false;
                        previewToolStrip.Enabled =
                            previewToolStripMenuItem.Enabled = searchSplitContainer.Panel2Collapsed;
                    }

                    //enable save tab option
                    var selectedResult = GetSelectedSearchResult();

                    if (selectedResult != null && selectedResult.Source == result.Source)
                    {
                        saveTabToolStripMenuItem1.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        ///     Removes version convention from tablature titles.
        /// </summary>
        private static string RemoveVersionConventionFromTitle(string title)
        {
            var versionConventionIndex = title.IndexOf(" (ver ", StringComparison.OrdinalIgnoreCase);

            if (versionConventionIndex >= 0)
                title = title.Remove(versionConventionIndex);

            return title;
        }

        private void BuildSearchSuggestions()
        {
            var artistStrings = new List<string>();
            var titleStrings = new List<string>();

            foreach (var item in _libraryManager.GetTablatureItems())
            {
                if (artistStrings.Find(x => x.Equals(item.File.Artist, StringComparison.OrdinalIgnoreCase)) == null)
                    artistStrings.Add(item.File.Artist);

                var title = RemoveVersionConventionFromTitle(item.File.Title);
                if (titleStrings.Find(x => x.Equals(title, StringComparison.OrdinalIgnoreCase)) == null)
                    titleStrings.Add(title);
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

        private void InitializeSearchControls(bool resetUserFields = false)
        {
            if (resetUserFields)
            {
                txtSearchArtist.Text = "";
                txtSearchTitle.Text = "";

                searchTypeList.SelectDefault();
                cbSearchRating.SelectedRating = TablatureRating.None;
            }
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyGetSelectedSearchResultUrl();
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (searchSplitContainer.Panel2Collapsed)
                TogglePreviewPane(sender, e);

            LoadSelectedSearchResultPreview();
        }

        private void onlinesearchbtn_Click(object sender, EventArgs e)
        {
            if (txtSearchArtist.Text.Trim().Length > 0 || txtSearchTitle.Text.Trim().Length > 0)
            {
                onlinesearchbtn.Enabled = false;

                listViewSearch.ClearObjects();

                searchPreviewEditor.Clear();

                var request = CreateSearchRequest();

                if (request != null)
                    SearchBackgroundWorker.RunWorkerAsync(request);
            }
        }
    }
}