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
    internal class DownloadState
    {
        public DownloadState(Uri url)
        {
            Url = url;
        }

        public Uri Url { get; private set; }
        public ITabParser Parser { get; set; }
        public Tab Tab { get; set; }
        public Exception Error { get; set; }
        public bool Cancelled { get; set; }
    }

    partial class MainForm
    {
        private readonly List<Tab> downloadedTabs = new List<Tab>();

        private Uri[] GetUrls()
        {
            var urls = new List<Uri>();

            foreach (var line in txtDownloadURLs.Lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    Uri url;
                    var validURL = Uri.TryCreate(line, UriKind.Absolute, out url) && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps);

                    if (validURL)
                    {
                        urls.Add(url);
                    }
                }
            }

            return urls.ToArray();
        }

        private void txtDownloadURLs_TextChanged(object sender, EventArgs e)
        {
            downloadUrlsbtn.Enabled = GetUrls().Length > 0;
        }

        private void UpdateDownloadControls()
        {
            if (DownloadBackgroundWorker.IsBusy)
            {
                downloadUrlsbtn.Text = "Cancel";
                lblprogress.Visible = true;
                progressBar1.Visible = true;
            }

            else
            {
                downloadUrlsbtn.Text = "Begin Download";
                lblprogress.Visible = false;
                progressBar1.Visible = false;
                progressBar1.Value = 0;
            }
        }

        private void downloadUrlsbtn_Click(object sender, EventArgs e)
        {
            if (DownloadBackgroundWorker.IsBusy)
            {
                DownloadBackgroundWorker.CancelAsync();
            }

            else
            {
                var urls = GetUrls();

                if (urls.Length > 0)
                {
                    var states = new List<DownloadState>();

                    foreach (var url in urls)
                    {
                        var state = new DownloadState(url) {Parser = _tabParsers.Find(x => x.MatchesUrlPattern(url))};
                        states.Add(state);
                    }

                    txtDownloadURLs.Clear();
                    listView1.Items.Clear();
                    progressBar1.Maximum = states.Count;

                    DownloadBackgroundWorker.RunWorkerAsync(states);
                }
            }

            UpdateDownloadControls();
        }

        private void DownloadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var states = (List<DownloadState>) e.Argument;

            var count = 0;

            foreach (var state in states)
            {
                if (DownloadBackgroundWorker.CancellationPending)
                {
                    state.Cancelled = true;
                }

                else
                {
                    if (state.Parser == null)
                        continue;

                    using (var client = new TabsterWebClient())
                    {
                        try
                        {
                            var src = client.DownloadString(state.Url);

                            var tab = state.Parser.ParseTabFromSource(src, null);

                            if (tab != null)
                            {
                                tab.Source = state.Url;
                                tab.SourceType = TabSource.Download;
                            }

                            state.Tab = tab;
                        }

                        catch (Exception ex)
                        {
                            state.Error = ex;
                        }
                    }

                    count++;
                    DownloadBackgroundWorker.ReportProgress(count, state);
                }
            }

            e.Result = states;
        }

        private void DownloadBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var state = (DownloadState) e.UserState;
            lblprogress.Text = string.Format("Downloading {0}", state.Url);

            progressBar1.Value = e.ProgressPercentage;
        }

        private void DownloadBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateDownloadControls();

            downloadedTabs.Clear();

            if (e.Error == null)
            {
                var states = (List<DownloadState>) e.Result;

                foreach (var state in states)
                {
                    downloadedTabs.Add(state.Tab);

                    var lvi = new ListViewItem {Text = state.Url.ToString()};

                    lvi.SubItems.Add(state.Parser != null ? state.Parser.Name : "N/A");

                    string status;

                    if (state.Cancelled)
                        status = "Cancelled";
                    else if (state.Parser == null)
                        status = "Missing parser";
                    else if (state.Tab == null)
                        status = "Parsing error";
                    else
                        status = "Completed";

                    lvi.SubItems.Add(status);

                    if (state.Tab != null)
                    {
                        lvi.SubItems.Add(state.Tab.Artist);
                        lvi.SubItems.Add(state.Tab.Title);
                        lvi.SubItems.Add(Tab.GetTabString(state.Tab.Type));
                    }

                    listView1.Items.Add(lvi);
                }

                if (listView1.Items.Count > 0)
                {
                    addtolibrarybtn.Enabled = true;
                }
            }
        }

        private void addtolibrarybtn_Click(object sender, EventArgs e)
        {
            foreach (var tab in downloadedTabs)
            {
                var tabFile = TabFile.Create(tab, Program.libraryManager.TabsDirectory);
                Program.libraryManager.AddTab(tabFile, true);
                UpdateLibraryItem(tabFile);
            }

            downloadedTabs.Clear();
            listView1.Items.Clear();
            addtolibrarybtn.Enabled = false;
        }
    }
}