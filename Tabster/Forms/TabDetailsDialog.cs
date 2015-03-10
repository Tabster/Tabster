#region

using System;
using System.Linq;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Library;

#endregion

namespace Tabster.Forms
{
    internal partial class TabDetailsDialog : Form
    {
        private readonly ITablatureFile _file;
        private readonly TablatureFileLibrary _library;

        public TabDetailsDialog(ITablatureFile file, TablatureFileLibrary library = null)
        {
            InitializeComponent();
            _file = file;
            _library = library;

            LoadTablatureData();
            LoadLibraryInformation();
        }

        private void LoadTablatureData()
        {
            txtlocation.Text = _file.FileInfo.FullName;

            txtartist.Text = _file.Artist;
            txtsong.Text = _file.Title;
            typeList.SelectedType = _file.Type;
            txtcomment.Text = _file.Comment;

            var header = _file.GetHeader();
            lblFormat.Text += header.Version.ToString();
            lblLength.Text += string.Format(" {0:n0} bytes", _file.FileInfo.Length);
            lblCreated.Text += string.Format(" {0}", _file.FileInfo.CreationTime);
            lblModified.Text += string.Format(" {0}", _file.FileInfo.LastWriteTime);
        }

        private void SaveTablatureData()
        {
            _file.Artist = txtartist.Text;
            _file.Title = txtsong.Text;
            _file.Type = typeList.SelectedType;
            _file.Comment = txtcomment.Text;
            _file.Save(_file.FileInfo.FullName);
        }

        private void LoadLibraryInformation()
        {
            groupBoxLibrary.Visible = _library != null;

            if (_library == null)
                return;

            var libraryItem = _library.GetLibraryItem(_file);

            if (libraryItem != null)
            {
                lblfavorited.Text = string.Format("Favorited: {0}", (libraryItem.Favorited ? "Yes" : "No"));
                lblViewCount.Text = string.Format("Views: {0}", libraryItem.Views);
                lblLastViewed.Text = string.Format("Last Viewed: {0}", libraryItem.LastViewed.HasValue ? libraryItem.LastViewed.Value.ToString() : "Never");

                var playlistCount = _library.Playlists.Count(playlist => playlist.Contains(_file.FileInfo.FullName));

                lblPlaylistCount.Text = string.Format("Founds in {0} playlist{1}.", playlistCount, playlistCount == 1 ? "" : "s");
            }
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            SaveTablatureData();
        }
    }
}