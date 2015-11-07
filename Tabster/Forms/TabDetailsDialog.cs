#region

using System;
using System.Linq;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Library;
using Tabster.Database;

#endregion

namespace Tabster.Forms
{
    internal partial class TabDetailsDialog : Form
    {
        private readonly TablatureLibraryItem<TablatureFile> _libraryItem;
        private readonly PlaylistManager _playlistManager;

        public TabDetailsDialog(TablatureLibraryItem<TablatureFile> libraryItem, PlaylistManager playlistManager)
        {
            InitializeComponent();
            _libraryItem = libraryItem;
            _playlistManager = playlistManager;

            LoadTablatureData();
            LoadLibraryInformation();
        }

        private void LoadTablatureData()
        {
            txtlocation.Text = _libraryItem.FileInfo.FullName;

            //tablature information
            txtArtist.Text = _libraryItem.File.Artist;
            txtTitle.Text = _libraryItem.File.Title;
            typeList.SelectedType = _libraryItem.File.Type;
            tablatureTuningDropdown1.SelectedTuning = _libraryItem.File.Tuning;
            txtSubtitle.Text = _libraryItem.File.Subtitle;
            tablatureDifficultyDropdown1.SelectedDifficulty = _libraryItem.File.Difficulty;
            txtAuthor.Text = _libraryItem.File.Author;
            txtCopyright.Text = _libraryItem.File.Copyright;
            txtAlbum.Text = _libraryItem.File.Album;
            txtGenre.Text = _libraryItem.File.Genre;
            tablatureRatingDropdown1.SelectedRating = _libraryItem.Rating;
            txtComment.Text = _libraryItem.File.Comment;

            txtLyrics.Text = _libraryItem.File.Lyrics;

            //file information
            lblFormat.Text += _libraryItem.File.FileHeader.Version.ToString();
            lblLength.Text += string.Format(" {0:n0} bytes", _libraryItem.FileInfo.Length);
            lblCreated.Text += string.Format(" {0}", _libraryItem.FileInfo.CreationTime);
            lblModified.Text += string.Format(" {0}", _libraryItem.FileInfo.LastWriteTime);
            lblCompressed.Text += string.Format(" {0}", _libraryItem.File.FileHeader.Compression == CompressionMode.None ? "No" : "Yes");
            lblEncoding.Text += string.Format(" {0}", _libraryItem.File.FileAttributes.Encoding.EncodingName);
        }

        private void SaveTablatureData()
        {
            //tablature information
            _libraryItem.File.Artist = txtArtist.Text;
            _libraryItem.File.Title = txtTitle.Text;
            _libraryItem.File.Type = typeList.SelectedType;
            _libraryItem.File.Tuning = tablatureTuningDropdown1.SelectedTuning;
            _libraryItem.File.Subtitle = txtSubtitle.Text;
            _libraryItem.File.Difficulty = tablatureDifficultyDropdown1.SelectedDifficulty;
            _libraryItem.File.Author = txtAuthor.Text;
            _libraryItem.File.Copyright = txtCopyright.Text;
            _libraryItem.File.Album = txtAlbum.Text;
            _libraryItem.File.Genre = txtGenre.Text;
            _libraryItem.Rating = tablatureRatingDropdown1.SelectedRating;
            _libraryItem.File.Comment = txtComment.Text;

            _libraryItem.File.Lyrics = txtLyrics.Text;

            _libraryItem.File.Save(_libraryItem.FileInfo.FullName);
        }

        private void LoadLibraryInformation()
        {
            lblfavorited.Text = string.Format("Favorited: {0}", (_libraryItem.Favorited ? "Yes" : "No"));
            lblViewCount.Text = string.Format("Views: {0}", _libraryItem.Views);
            lblLastViewed.Text = string.Format("Last Viewed: {0}", _libraryItem.LastViewed.HasValue ? _libraryItem.LastViewed.Value.ToString() : "Never");

            var playlistCount = _playlistManager.GetPlaylists().Count(playlist => playlist.Find(_libraryItem.FileInfo.FullName) != null);

            lblPlaylistCount.Text = string.Format("Founds in {0} playlist{1}.", playlistCount, playlistCount == 1 ? "" : "s");
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            SaveTablatureData();
        }
    }
}