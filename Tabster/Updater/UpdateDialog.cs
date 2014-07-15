#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace Tabster.Updater
{
    public partial class UpdateDialog : Form
    {
        private readonly Size fullSize;
        private readonly UpdateQuery updateQuery;
        private DownloadManager downloadManager;

        public UpdateDialog()
        {
            InitializeComponent();

            fullSize = Size;
            Size = new Size(433, 128);
        }

        public UpdateDialog(UpdateQuery query) : this()
        {
            updateQuery = query;
            LoadUpdateInformation();
        }

        private void LoadUpdateInformation()
        {
            if (updateQuery.Error != null)
            {
                lblstatus.Text = "Update check failed.";
            }

            else
            {
                if (updateQuery.UpdateAvailable)
                {
                    Size = fullSize;
                    lblstatus.Text = string.Format("An update is available! You are running v{0} and v{1} is available.", Application.ProductVersion.Substring(0, Application.ProductVersion.Length - 2), updateQuery.Version);

                    //need to convert to char array because hostgator is dumb with newlines it seems
                    txtchangelog.Lines = updateQuery.Changelog.Split(Environment.NewLine.ToCharArray());
                    txtchangelog.Visible = true;
                    updatebtn.Enabled = true;
                }

                else
                {
                    lblstatus.Text = "No updates available.";
                }
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            updatebtn.Enabled = false;

            downloadManager = new DownloadManager(updateQuery.DownloadURL);
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