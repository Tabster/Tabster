#region

using System;
using System.Linq;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Library;
using Tabster.Data.Xml;

#endregion

namespace Tabster.Forms
{
    internal partial class TabDetailsDialog : Form
    {
        private readonly TablatureFileLibrary _library;
        private readonly TablatureDocument _tabDocument;

        public TabDetailsDialog(TablatureDocument tab, TablatureFileLibrary library = null)
        {
            InitializeComponent();
            _tabDocument = tab;
            _library = library;

            LoadTablatureData();
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

        private void SaveTablatureData()
        {
            _tabDocument.Artist = txtartist.Text;
            _tabDocument.Title = txtsong.Text;
            _tabDocument.Type = typeList.SelectedType;
            _tabDocument.Comment = txtcomment.Text;
            _tabDocument.Save();
        }

        private void LoadLibraryInformation()
        {
            groupBoxLibrary.Visible = _library != null;

            if (_library == null)
                return;

            var libraryItem = _library.GetLibraryItem(_tabDocument);

            if (libraryItem != null)
            {
                lblfavorited.Text = string.Format("Favorited: {0}", (libraryItem.Favorited ? "Yes" : "No"));
                lblViewCount.Text = string.Format("Views: {0}", libraryItem.Views);
                lblLastViewed.Text = string.Format("Last Viewed: {0}", libraryItem.LastViewed.HasValue ? libraryItem.LastViewed.Value.ToString() : "Never");

                var playlistCount = _library.Playlists.Count(playlist => playlist.Contains(_tabDocument.FileInfo.FullName));

                lblPlaylistCount.Text = string.Format("Founds in {0} playlist{1}.", playlistCount, playlistCount == 1 ? "" : "s");
            }
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            SaveTablatureData();
        }
    }
}