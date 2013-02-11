#region

using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Tabster.UltimateGuitar;

#endregion

namespace Tabster.Forms
{
    public partial class UGDownload : Form
    {
        private string[] URLs;

        public UGDownload()
        {
            InitializeComponent();
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                URLs = textBox1.Lines;
                backgroundWorker1.RunWorkerAsync();
            }
        }
        
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var url = new Uri(URLs[e.ProgressPercentage - 1]);
            var truncated = "Downloading ..." + url.AbsolutePath; // Microsoft.VisualBasic.Strings.Right(url.AbsolutePath, 30);
            label1.Text = truncated;
            progressBar1.Value = e.ProgressPercentage * (progressBar1.Maximum/URLs.Length);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = "Complete!";
            progressBar1.Value = progressBar1.Maximum;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (var i = 1; i <= URLs.Length; i++)
            {
                var line = URLs[i - 1];

                var url = new Uri(line);

                if (UltimateGuitarTab.IsValidUltimateGuitarTabURL(url))
                {
                    backgroundWorker1.ReportProgress(i);
                    Thread.Sleep(150);
                    var ugtab = new UltimateGuitarTab(url);

                    Console.WriteLine("{0} - {1} ({2})", ugtab.Artist, ugtab.Title, Global.GetTabString(ugtab.Type));
                }
            }
        }
    }
}