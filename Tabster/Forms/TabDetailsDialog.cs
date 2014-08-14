#region

using System;
using System.Windows.Forms;
using Tabster.Core.Data;
using Tabster.Core.Types;

#endregion

namespace Tabster.Forms
{
    public partial class TabDetailsDialog : Form
    {
        private readonly TablatureDocument _tabDocument;

        public TabDetailsDialog(TablatureDocument tab)
        {
            InitializeComponent();
            _tabDocument = tab;

            txttype.DataSource = TabTypeUtilities.FriendlyStrings();

            LoadData();
        }

        private void LoadData()
        {
            txtlocation.Text = _tabDocument.FileInfo.FullName;

            txtartist.Text = _tabDocument.Artist;
            txtsong.Text = _tabDocument.Title;
            txttype.Text = _tabDocument.Type.ToFriendlyString();
            txtcomment.Text = _tabDocument.Comment;

            lblFormat.Text += _tabDocument.FileVersion;
            lblLength.Text += string.Format(" {0:n0} bytes", _tabDocument.FileInfo.Length);
            lblCreated.Text += string.Format(" {0}", _tabDocument.FileInfo.CreationTime);
            lblModified.Text += string.Format(" {0}", _tabDocument.FileInfo.LastWriteTime);

            var attributes = Program.libraryManager.GetLibraryAttributes(_tabDocument);

            lblfavorited.Text = string.Format("Favorited: {0}", (attributes.Favorited ? "Yes" : "No"));
            lblViewCount.Text = string.Format("Views: {0}", attributes.Views);

            var playlistCount = 0;

            foreach (var playlist in Program.libraryManager.Playlists)
            {
                if (playlist.ContainsPath(_tabDocument.FileInfo.FullName))
                    playlistCount++;
            }

            lblPlaylistCount.Text = string.Format("Founds in {0} playlist{1}.", playlistCount, playlistCount == 1 ? "" : "s");
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            _tabDocument.Artist = txtartist.Text;
            _tabDocument.Title = txtsong.Text;
            _tabDocument.Type = TabTypeUtilities.FromFriendlyString(txttype.Text).Value;
            _tabDocument.Comment = txtcomment.Text;
            _tabDocument.Save();
        }
    }
}