#region

using System;
using System.Linq;
using System.Windows.Forms;
using Tabster.Data;

#endregion

namespace Tabster.Forms
{
    internal partial class TabDetailsDialog : Form
    {
        private readonly TablatureDocument _tabDocument;

        public TabDetailsDialog(TablatureDocument tab, bool showLibraryInformation = true)
        {
            InitializeComponent();
            _tabDocument = tab;

            LoadTablatureData();

            if (showLibraryInformation)
                LoadLibraryInformation();
        }

        private void LoadTablatureData()
        {
            txtlocation.Text = _tabDocument.FileInfo.FullName;

            txtartist.Text = _tabDocument.Artist;
            txtsong.Text = _tabDocument.Title;
            typeList.SelectedType = _tabDocument.Type;
            txtcomment.Text = _tabDocument.Comment;

            lblFormat.Text += _tabDocument.FileVersion;
            lblLength.Text += string.Format(" {0:n0} bytes", _tabDocument.FileInfo.Length);
            lblCreated.Text += string.Format(" {0}", _tabDocument.FileInfo.CreationTime);
            lblModified.Text += string.Format(" {0}", _tabDocument.FileInfo.LastWriteTime);
        }

        private void LoadLibraryInformation()
        {
            var libraryItem = Program.tablatureLibrary.GetLibraryItem(_tabDocument);

            if (libraryItem != null)
            {
                lblfavorited.Text = string.Format("Favorited: {0}", (libraryItem.Favorited ? "Yes" : "No"));
                lblViewCount.Text = string.Format("Views: {0}", libraryItem.Views);
                lblLastViewed.Text = string.Format("Last Viewed: {0}", libraryItem.LastViewed.HasValue ? libraryItem.LastViewed.Value.ToString() : "Never");

                var playlistCount = Program.tablatureLibrary.Playlists.Count(playlist => playlist.Contains(_tabDocument.FileInfo.FullName));

                lblPlaylistCount.Text = string.Format("Founds in {0} playlist{1}.", playlistCount, playlistCount == 1 ? "" : "s");
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            _tabDocument.Artist = txtartist.Text;
            _tabDocument.Title = txtsong.Text;
            _tabDocument.Type = typeList.SelectedType;
            _tabDocument.Comment = txtcomment.Text;
            _tabDocument.Save();
        }
    }
}