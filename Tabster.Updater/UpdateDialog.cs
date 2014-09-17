#region

using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

#endregion

namespace Tabster.Updater
{
    public partial class UpdateDialog : Form
    {
        private readonly UpdateQuery _updateQuery;
        private DownloadManager downloadManager;

        public UpdateDialog(UpdateQuery query)
        {
            InitializeComponent();

            _updateQuery = query;
            LoadUpdateInformation();
        }

        private void LoadUpdateInformation()
        {
            if (_updateQuery != null)
            {
                //need to convert to char array because hostgator is dumb with newlines it seems
                txtchangelog.Lines = _updateQuery.Changelog.Split(Environment.NewLine.ToCharArray());
                txtchangelog.Visible = true;
                updatebtn.Enabled = true;
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            updatebtn.Enabled = false;

            downloadManager = new DownloadManager(_updateQuery.DownloadURL);
            downloadManager.DownloadProgressChanged += downloadManager_DownloadProgressChanged;
            downloadManager.DownloadFileCompleted += downloadManager_DownloadFileCompleted;
            downloadManager.Start();

            lblprogress.Visible = true;
            progressBar1.Visible = true;
        }

        private void downloadManager_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblprogress.Text = string.Format("Downloaded {0}kb of {1}kb...", (e.BytesReceived/1024), (e.TotalBytesToReceive/1024));
        }

        private void downloadManager_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var successful = e.Error == null && !e.Cancelled;
            lblprogress.Text = successful ? "Download complete!" : "Download error!";
            progressBar1.Style = ProgressBarStyle.Marquee;

            if (successful)
            {
                downloadManager.RunInstaller(false);
                Application.Exit();
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}