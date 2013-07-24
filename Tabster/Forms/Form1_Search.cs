#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Tabster.UltimateGuitar;

#endregion

namespace Tabster.Forms
{
    partial class Form1
    {
        private readonly Image Rating0, Rating1, Rating2, Rating3, Rating4, Rating5;
        private readonly SearchManager searchManager = new SearchManager();
        private readonly Dictionary<Uri, UltimateGuitarTab> _ugTabCache = new Dictionary<Uri, UltimateGuitarTab>(); 

        private Image GetRating(int rating)
        {
            switch (rating)
            {
                case 1:
                    return Rating1;
                case 2:
                    return Rating2;
                case 3:
                    return Rating3;
                case 4:
                    return Rating4;
                case 5:
                    return Rating5;
                default:
                    return Rating0;
            }
        }

        private SearchResult SelectedSearchResult()
        {
             var selectedURL = dataGridViewExtended1.SelectedRows.Count > 0 ? new Uri(dataGridViewExtended1.SelectedRows[0].Tag.ToString()) : null;
             return selectedURL != null ? searchManager.Find(x => x.URL.Equals(selectedURL)) : null;
        }

        private void onlinesearchbtn_Click(object sender, EventArgs e)
        {
            if (txtsearchartist.Text.Trim().Length > 0 || txtsearchsong.Text.Trim().Length > 0)
            {
                pictureBox1.Visible = true;

                searchManager.Artist = txtsearchartist.Text;
                searchManager.Title = txtsearchsong.Text;

                var searchType = UltimateGuitar.TabType.Undefined;

                switch (txtsearchtype.SelectedIndex)
                {
                    case 0:
                        searchType = UltimateGuitar.TabType.Undefined;
                        break;
                    case 1:
                        searchType = UltimateGuitar.TabType.GuitarTab;
                        break;
                    case 2:
                        searchType = UltimateGuitar.TabType.GuitarChords;
                        break;
                    case 3:
                        searchType = UltimateGuitar.TabType.BassTab;
                        break;
                    case 4:
                        searchType = UltimateGuitar.TabType.DrumTab;
                        break;
                }

                searchManager.Type = searchType;

                searchManager.Search();
            }
        }

        private void dataGridViewExtended1_MouseClick(object sender, MouseEventArgs e)
        {
            var currentMouseOverRow = dataGridViewExtended1.HitTest(e.X, e.Y).RowIndex;

            if (e.Button == MouseButtons.Right && (currentMouseOverRow >= 0 && currentMouseOverRow < dataGridViewExtended1.Rows.Count))
            {
                dataGridViewExtended1.Rows[currentMouseOverRow].Selected = true;
                SearchMenu.Show(dataGridViewExtended1.PointToScreen(e.Location));
            }
        }

        private void dataGridViewExtended1_SelectionChanged(object sender, EventArgs e)
        {
            LoadSelectedPreview();
        }

        private void saveTabToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var selectedResult = SelectedSearchResult();

            if (selectedResult != null)
            {
                using (var nt = new NewTabDialog(selectedResult.Artist, selectedResult.Title, UltimateGuitarTab.GetTabType(selectedResult.Type)))
                {
                    if (nt.ShowDialog() == DialogResult.OK)
                    {
                        var ugTab = _ugTabCache.ContainsKey(selectedResult.URL) ? _ugTabCache[selectedResult.URL] : UltimateGuitarTab.Download(selectedResult.URL);

                        if (ugTab != null)
                        {
                            var tab = new Tab(nt.txtartist.Text, nt.txtsong.Text, Tab.GetTabType(nt.txttype.Text), ugTab.ConvertToTab().Contents)
                                          {RemoteSource = selectedResult.URL, Source = TabSource.Download};

                            var tabFile = TabFile.Create(tab, Program.libraryManager.TabsDirectory);
                            Program.libraryManager.AddTab(tabFile, true);
                            UpdateLibraryItem(tabFile);
                        }
                    }
                }
            }
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = SelectedSearchResult();

            if (result != null)
            {
                Clipboard.SetDataObject(result.URL.ToString());
            }
        }

        private void LoadSelectedPreview()
        {
            var selectedResult = SelectedSearchResult();

            if (selectedResult != null)
            {
                if (SearchPreviewBackgroundWorker.IsBusy)
                    SearchPreviewBackgroundWorker.CancelAsync();

                searchPreviewEditor.SetDocumentText("Loading Preview...");

                if (!SearchPreviewBackgroundWorker.IsBusy)
                    SearchPreviewBackgroundWorker.RunWorkerAsync(selectedResult.URL);
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TogglePreviewPane(sender, e);
            LoadSelectedPreview();
        }

        void searchSession_OnCompleted(object sender, EventArgs e)
        {
            dataGridViewExtended1.SuspendLayout();
            dataGridViewExtended1.Rows.Clear();

            foreach (var result in searchManager)
            {
                if (searchManager.Type == UltimateGuitar.TabType.Undefined || searchManager.Type == result.Type)
                {
                    var newRow = new DataGridViewRow {Tag = result.URL.ToString()};
                    newRow.CreateCells(dataGridViewExtended1, result.Artist, result.Title, Tab.GetTabString(UltimateGuitarTab.GetTabType(result.Type)), GetRating(result.Rating), result.Votes);
                    dataGridViewExtended1.Rows.Add(newRow);
                }
            }

            dataGridViewExtended1.ResumeLayout();

            lblsearchresults.Visible = true;
            lblsearchresults.Text = string.Format("Results: {0}", dataGridViewExtended1.Rows.Count);
            pictureBox1.Visible = false;
        }

        private void SearchPreviewBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var url = (Uri) e.Argument;

            e.Result = url;

            if (!_ugTabCache.ContainsKey(url))
            {
                var ugTab = UltimateGuitarTab.Download(url);
                _ugTabCache.Add(url, ugTab);
            }
        }

        private void SearchPreviewBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == false && e.Error == null)
            {
                var url = (Uri) e.Result;

                if (_ugTabCache.ContainsKey(url))
                {
                    var ugTab = _ugTabCache[url];
                    var tab = ugTab.ConvertToTab();
                    searchPreviewEditor.LoadTab(tab);
                }
            }
        }
    }
}