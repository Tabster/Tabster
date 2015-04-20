#region

using System;
using System.Linq;
using System.Windows.Forms;
using Tabster.Data.Binary;
using Tabster.Data.Library;

#endregion

namespace Tabster.Forms
{
    internal partial class TabDetailsDialog : Form
    {
        private readonly TablatureFile _file;
        private readonly TabsterFileSystemLibraryBase<TablatureFile, TablaturePlaylistFile> _library;

        public TabDetailsDialog(TablatureFile file, TabsterFileSystemLibraryBase<TablatureFile, TablaturePlaylistFile> library = null)
        {
            InitializeComponent();
            _file = file;
            _library = library;

            LoadTablatureData();
            LoadLibraryInformation();
        }

        private void LoadTablatureData()
        {
            var item = _library.FindTablatureItemByFile(_file);

            txtlocation.Text = item.FileInfo.FullName;

            txtartist.Text = _file.Artist;
            txtsong.Text = _file.Title;
            typeList.SelectedType = _file.Type;
            txtcomment.Text = _file.Comment;

            lblFormat.Text += _file.FileHeader.Version.ToString();
            lblLength.Text += string.Format(" {0:n0} bytes", item.FileInfo.Length);
            lblCreated.Text += string.Format(" {0}", item.FileInfo.CreationTime);
            lblModified.Text += string.Format(" {0}", item.FileInfo.LastWriteTime);
        }

        private void SaveTablatureData()
        {
            var item = _library.FindTablatureItemByFile(_file);

            _file.Artist = txtartist.Text;
            _file.Title = txtsong.Text;
            _file.Type = typeList.SelectedType;
            _file.Comment = txtcomment.Text;
            _file.Save(item.FileInfo.FullName);
        }

        private void LoadLibraryInformation()
        {
            groupBoxLibrary.Visible = _library != null;

            if (_library == null)
                return;

            var libraryItem = _library.FindTablatureItemByFile(_file);

            if (libraryItem != null)
            {
                lblfavorited.Text = string.Format("Favorited: {0}", (libraryItem.Favorited ? "Yes" : "No"));
                lblViewCount.Text = string.Format("Views: {0}", libraryItem.Views);
                lblLastViewed.Text = string.Format("Last Viewed: {0}", libraryItem.LastViewed.HasValue ? libraryItem.LastViewed.Value.ToString() : "Never");

                var playlistCount = _library.GetPlaylistItems().SelectMany(item => item.File).Count(playlistItem => playlistItem.File.Equals(_file));

                lblPlaylistCount.Text = string.Format("Founds in {0} playlist{1}.", playlistCount, playlistCount == 1 ? "" : "s");
            }
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            SaveTablatureData();
        }
    }
}