#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tabster.Core;
using Tabster.UltimateGuitar;

#endregion

namespace Tabster.Forms
{
    public partial class DownloadDialog : Form
    {
        private readonly MainForm _parent;
        private readonly List<UltimateGuitarTab> _downloadedTabs = new List<UltimateGuitarTab>();
        private readonly List<string> _urls = new List<string>();


        public DownloadDialog(MainForm parent)
        {      
            InitializeComponent();
            _parent = parent;
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                _urls.Clear();
                _downloadedTabs.Clear();

                listView1.Items.Clear();

                foreach (var line in txturls.Lines)
                {
                    if (!string.IsNullOrEmpty(line) && UltimateGuitarTab.IsValidUltimateGuitarTabURL((line)))
                    {
                        _urls.Add(line);

                        var lvi = new ListViewItem {Text = "N/A"};
                        lvi.SubItems.Add("Waiting...");
                        lvi.SubItems.Add(line);
                        listView1.Items.Add(lvi);
                    }
                }

                if (listView1.Items.Count > 0)
                {

                    listView1.BringToFront();
                    startbtn.Visible = false;
                    progressBar1.Value = progressBar1.Minimum;
                    progressBar1.Maximum = _urls.Count;
                    progressBar1.Visible = true;
                    lblprogress.Text = "";
                    lblprogress.Visible = true;

                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }
        
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var tabIndex = e.ProgressPercentage;
            var tab = _downloadedTabs[tabIndex];
            lblprogress.Text = string.Format("Downloading: {0}", _urls[tabIndex]);
            progressBar1.Value += 1; // e.ProgressPercentage * (progressBar1.Maximum / URLs.Length);

            var lvi = listView1.Items[tabIndex];
            lvi.Text = tab.ConvertToTab().GetName();
            lvi.SubItems[1].Text = "Done";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            addtolibrarybtn.Visible = _downloadedTabs.Count > 0;
            addtolibrarybtn.BringToFront();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (var i = 0; i < _urls.Count; i++)
            {
                var url = new Uri(_urls[i]);
                var ugtab = UltimateGuitarTab.Download(url);
                _downloadedTabs.Add(ugtab);
                backgroundWorker1.ReportProgress(i);
                System.Threading.Thread.Sleep(200);
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            txturls.Clear();
            txturls.BringToFront();
            startbtn.Visible = true;
            lblprogress.Visible = false;
            progressBar1.Visible = false;
            addtolibrarybtn.Visible = false;
            resetbtn.Visible = false;
        }

        private void txturls_TextChanged(object sender, EventArgs e)
        {
            startbtn.Enabled = txturls.Lines.Length > 0;
        }

        private void addtolibrarybtn_Click(object sender, EventArgs e)
        {
            foreach (var tab in _downloadedTabs)
            {
                var newTab = TabFile.Create(tab.ConvertToTab(), Program.libraryManager.TabsDirectory);
                newTab.TabData.SourceType = TabSource.Download;
                newTab.Save();
                Program.libraryManager.AddTab(newTab, true);
                _parent.UpdateLibraryItem(newTab, true);
            }
            resetbtn.Visible = true;
            resetbtn.BringToFront();
        }
    }
}