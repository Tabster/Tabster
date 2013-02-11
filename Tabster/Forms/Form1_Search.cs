#region

using System;
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
        private SearchQuery searchQuery;
        private SearchResult selectedResult;

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

        private UltimateGuitarTabType GetSearchType()
        {
            switch (txtsearchtype.SelectedIndex)
            {
                case 0:
                    return UltimateGuitarTabType.Undefined;
                case 1:
                    return UltimateGuitarTabType.GuitarTab;
                case 2:
                    return UltimateGuitarTabType.GuitarChords;
                case 3:
                    return UltimateGuitarTabType.BassTab;
                case 4:
                    return UltimateGuitarTabType.DrumTab;
                default:
                    return UltimateGuitarTabType.Undefined;
            }
        }

        private void onlinesearchbtn_Click(object sender, EventArgs e)
        {
            if (!SearchBackgroundWorker.IsBusy && txtsearchartist.Text.Trim() != "" && txtsearchsong.Text.Trim() != "")
            {
                pictureBox1.Visible = true;

                searchQuery = new SearchQuery(txtsearchartist.Text, txtsearchsong.Text, GetSearchType());

                SearchBackgroundWorker.RunWorkerAsync();
            }
        }

        private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _firstBrowserLoad = true;
            //TODO
            //embeddedWebBrowser1.webBrowser1.Navigate(dataGridViewExtended1.SelectedRows[0].Cells[searchcol_url.Index].Value.ToString());
            tabControl1.SelectedTab = display_browser;
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
            if (dataGridViewExtended1.SelectedRows.Count > 0)
            {
                var selectedURL = dataGridViewExtended1.SelectedRows[0].Cells[searchcol_url.Index].Value.ToString();
                selectedResult = searchQuery.Find(x => x.URL.Equals(selectedURL));
                LoadSearchPreview();
            }
        }

        private void saveTabToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var url = new Uri(dataGridViewExtended1.SelectedRows[0].Cells[searchcol_url.Index].Value.ToString());
            var ugtab = new UltimateGuitarTab(url);

            using (var nt = new NewTabDialog(ugtab.Artist, ugtab.Title, ugtab.Type))
            {
                if (nt.ShowDialog() == DialogResult.OK)
                {

                }
            }

            /*
            Global.Tabs.Save(dataGridViewExtended1.SelectedRows[0].Cells[url.Index].Value.ToString());
            LoadLibrary(true);*/
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedResult != null)
            {
                Clipboard.SetDataObject(selectedResult.URL);
            }
        }

        private void LoadSearchPreview()
        {
            searchSplitContainer.Panel2Collapsed = false;
            searchSplitContainer.Orientation = Orientation.Vertical;
            var ugtab = new UltimateGuitarTab(new Uri(selectedResult.URL));
            var temp = Global.libraryManager.CreateTempFile(ugtab.Artist, ugtab.Title, ugtab.Type, ugtab.Contents);
            searchPreviewEditor.LoadTab(temp);
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedResult != null)
            {
                LoadSearchPreview();
            }
        }

        private void SearchBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (searchQuery != null)
            {
                dataGridViewExtended1.Rows.Clear();

                foreach (var result in searchQuery)
                {
                    if (searchQuery.Type == UltimateGuitarTabType.Undefined || searchQuery.Type == result.Type)
                    {
                        dataGridViewExtended1.Rows.Add(result.Artist, result.Song, Global.GetTabString(UltimateGuitarTab.GetTabType(result.Type)), GetRating(result.Rating), result.Votes, result.URL);
                    }
                }

                lblsearchresults.Visible = true;
                lblsearchresults.Text = string.Format("Results: {0}", dataGridViewExtended1.Rows.Count);
                pictureBox1.Visible = false;
            }
        }

        private void SearchBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            searchQuery.BeginSearch();
        }
    }
}