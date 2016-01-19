#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Tabster.Core.Types;
using Tabster.Data.Processing;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    public partial class DownloadDialog : Form
    {
        public enum DownloadState
        {
            MissingParser,
            Cancelled,
            Completed,
            Failed
        }

        #region Nested type: DownloadProcedure

        internal class DownloadProcedure
        {
            public DownloadProcedure(Uri url)
            {
                Url = url;
            }

            public Uri Url { get; private set; }
            public ITablatureWebImporter Parser { get; set; }
            public AttributedTablature Tab { get; set; }
            public DownloadState State { get; set; }
        }

        #endregion

        private readonly List<AttributedTablature> _downloadedTabs = new List<AttributedTablature>();
        private readonly List<ITablatureWebImporter> _importers = new List<ITablatureWebImporter>();
        private bool _closePending;
        private bool _completed = true;

        public DownloadDialog(List<ITablatureWebImporter> importers)
        {
            InitializeComponent();
            _importers = importers;

            splitContainer1.Panel2Collapsed = true;
        }

        public ReadOnlyCollection<AttributedTablature> DownloadedTabs
        {
            get { return _downloadedTabs.AsReadOnly(); }
        }

        private Uri[] GetUrls()
        {
            var urls = new List<Uri>();

            foreach (var line in txtUrls.Lines.Where(line => !string.IsNullOrEmpty(line)))
            {
                Uri url;
                var validUrl = Uri.TryCreate(line, UriKind.Absolute, out url) &&
                               (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps);

                if (validUrl)
                {
                    urls.Add(url);
                }
            }

            return urls.ToArray();
        }

        private void txtUrls_TextChanged(object sender, EventArgs e)
        {
            btnDownload.Enabled = GetUrls().Length > 0;
        }

        private static void PerformQueuedDownload(DownloadProcedure download, WebProxy proxy)
        {
            try
            {
                var tab = download.Parser.Parse(download.Url, proxy);

                if (tab != null)
                {
                    tab.Source = download.Url;
                    tab.SourceType = TablatureSourceType.Download;
                }

                download.Tab = tab;

                download.State = DownloadState.Completed;
            }

            catch
            {
                download.State = DownloadState.Failed;
            }
        }

        private void DownloadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var queuedDownloads = (List<DownloadProcedure>) e.Argument;

            var count = 0;

            var proxy = UserSettingsUtilities.ProxySettings.Proxy;

            foreach (var queuedDownload in queuedDownloads)
            {
                if (DownloadBackgroundWorker.CancellationPending)
                {
                    queuedDownload.State = DownloadState.Cancelled;
                }

                else if (queuedDownload.Parser == null)
                {
                    queuedDownload.State = DownloadState.MissingParser;
                }

                else
                {
                    PerformQueuedDownload(queuedDownload, proxy);
                }

                count++;
                DownloadBackgroundWorker.ReportProgress(count, queuedDownload);
            }

            e.Result = queuedDownloads;
        }

        private void DownloadBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

            var download = (DownloadProcedure) e.UserState;

            if (download.State == DownloadState.Completed)
                _downloadedTabs.Add(download.Tab);

            var lvi = listDownloads.Items[_downloadedTabs.Count - 1];
            lvi.SubItems[colStatus.Index].Text =
                GetDownloadStateString(download.State);

            listDownloads.EnsureVisible(lvi.Index);
        }

        private void DownloadBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _completed = true;
            if (_closePending)
                Close();

            if (e.Error != null || e.Cancelled)
                return;

            progressBar1.Visible = false;
            btnUrls.BringToFront();

            if (listDownloads.Items.Count > 0)
            {
                btnLibrary.BringToFront();
                listDownloads.Items[0].Selected = true;
            }

            splitContainer1.Panel2Collapsed = listDownloads.Items.Count == 0;
        }

        private static string GetDownloadStateString(DownloadState state)
        {
            switch (state)
            {
                case DownloadState.MissingParser:
                    return "No suitable parser found";
                case DownloadState.Cancelled:
                    return "Cancelled";
                case DownloadState.Completed:
                    return "Completed";
                case DownloadState.Failed:
                    return "Failed";
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DisplayDownloadList();

            var urls = GetUrls();

            txtUrls.Clear();

            var queuedDownloads = urls.Select(url => new DownloadProcedure(url) {Parser = _importers.Find(x => x.IsUrlParsable(url))}).ToList();

            foreach (
                var lvi in
                    queuedDownloads.Select(queuedDownload => new ListViewItem {Text = queuedDownload.Url.ToString()}))
            {
                lvi.SubItems.Add("Idle");
                listDownloads.Items.Add(lvi);
            }

            progressBar1.Maximum = queuedDownloads.Count;

            btnCancel.BringToFront();
            progressBar1.Visible = true;

            _completed = false;

            DownloadBackgroundWorker.RunWorkerAsync(queuedDownloads);
        }

        private void txtUrls_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.A))
            {
                txtUrls.SelectAll();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void DownloadDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_completed)
            {
                DownloadBackgroundWorker.CancelAsync();
                Enabled = false;
                e.Cancel = true;
                _closePending = true;
            }
        }

        private void DisplayUrlBox()
        {
            label4.Visible = true;

            txtUrls.BringToFront();
            btnDownload.BringToFront();
            btnCancel.BringToFront();
        }

        private void DisplayDownloadList()
        {
            label4.Visible = false;
            listDownloads.BringToFront();
        }

        private void btnUrls_Click(object sender, EventArgs e)
        {
            DisplayUrlBox();
        }

        private void listDownloads_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttributedTablature tab = null;

            if (listDownloads.SelectedItems.Count > 0)
                tab = _downloadedTabs[listDownloads.SelectedItems[0].Index];

            PopulateTablatureFields(tab);
        }

        private void PopulateTablatureFields(AttributedTablature tab)
        {
            txtArtist.Text = tab != null ? tab.Artist : string.Empty;
            txtTitle.Text = tab != null ? tab.Title : string.Empty;
            typeList.SelectedType = tab != null ? tab.Type : null;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var tab = _downloadedTabs[listDownloads.SelectedItems[0].Index];

            tab.Artist = txtArtist.Text;
            tab.Title = txtTitle.Text;
            tab.Type = typeList.SelectedType;
        }
    }
}