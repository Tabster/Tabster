#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Tabster.UltimateGuitar;

#endregion

namespace Tabster.Forms
{
    public partial class DownloadDialog : Form
    {
        private string[] URLs;
        private readonly List<UltimateGuitarTab> _downloadedTabs = new List<UltimateGuitarTab>();

        private readonly List<TabFile> _newTabs = new List<TabFile>();

        public ReadOnlyCollection<TabFile> NewTabs
        {
            get { return _newTabs.AsReadOnly(); }
        }

        public DownloadDialog()
        {
            InitializeComponent();
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                URLs = textBox1.Lines;
                listView1.BringToFront();
                backgroundWorker1.RunWorkerAsync();
            }
        }
        
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var tab = _downloadedTabs[e.ProgressPercentage - 1];
            label1.Text = string.Format("Downloading ...{0}", tab.URL.AbsolutePath); // Microsoft.VisualBasic.Strings.Right(url.AbsolutePath, 30);
            progressBar1.Value = e.ProgressPercentage * (progressBar1.Maximum/URLs.Length);
            var lvi = new ListViewItem(tab.ConvertToTab().GetName());
            lvi.SubItems.Add("Ready");
            listView1.Items.Add(lvi);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = "Complete!";
            progressBar1.Value = progressBar1.Maximum;
            textBox1.Clear();
            /*
            listView1.SendToBack();
            listView1.Clear();
            */
            label1.Text = "Adding tab(s) to library.";

            for (var i = 0; i < _downloadedTabs.Count; i++)
            {
                var tab = _downloadedTabs[i];
                var tf = TabFile.Create(tab.ConvertToTab(), Program.libraryManager.TabsDirectory);
                Program.libraryManager.AddTab(tf, true);
                _newTabs.Add(tf);
                listView1.Items[i].SubItems[1].Text = "Added to Library";
                Thread.Sleep(80);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _downloadedTabs.Clear();

            for (var i = 1; i <= URLs.Length; i++)
            {
                var line = URLs[i - 1];

                if (!string.IsNullOrEmpty(line) && Uri.IsWellFormedUriString(line, UriKind.RelativeOrAbsolute))
                {
                    var url = new Uri(line);

                    if (UltimateGuitarTab.IsValidUltimateGuitarTabURL(url))
                    {
                        var ugtab = UltimateGuitarTab.Download(url);
                        _downloadedTabs.Add(ugtab);
                        backgroundWorker1.ReportProgress(i);
                        Thread.Sleep(80);
                    }
                }
            }
        }
    }
}