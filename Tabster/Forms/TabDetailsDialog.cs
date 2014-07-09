#region

using System;
using System.Windows.Forms;
using Tabster.Core;

#endregion

namespace Tabster.Forms
{
    public partial class TabDetailsDialog : Form
    {
        private readonly TabFile _tabFile;

        public TabDetailsDialog(TabFile tab)
        {
            InitializeComponent();
            _tabFile = tab;

            foreach (TabType type in Enum.GetValues(typeof(TabType)))
                txttype.Items.Add(type.ToFriendlyString());

            LoadData();
        }

        private void LoadData()
        {
            txtlocation.Text = _tabFile.FileInfo.FullName;

            txtartist.Text = _tabFile.TabData.Artist;
            txtsong.Text = _tabFile.TabData.Title;
            txttype.Text = _tabFile.TabData.Type.ToFriendlyString();
            txtcomment.Text = _tabFile.TabData.Comment;

            lblFormat.Text += _tabFile.FileVersion;
            lblLength.Text += string.Format(" {0:n0} bytes", _tabFile.FileInfo.Length);
            lblCreated.Text += string.Format(" {0}", _tabFile.FileInfo.CreationTime);
            lblModified.Text += string.Format(" {0}", _tabFile.FileInfo.LastWriteTime);

            var favorited = Program.libraryManager.FindTab(_tabFile).Favorited;
            lblfavorited.Text = string.Format("Favorited: {0}", (favorited ? "Yes" : "No"));

            var playlistCount = Program.libraryManager.FindPlaylistsContaining(_tabFile).Count;
            lblPlaylistCount.Text = string.Format("Founds in {0} playlist{1}.", playlistCount, playlistCount == 1 ? "" : "s");

            txtlyrics.Text = _tabFile.TabData.Lyrics;
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            _tabFile.TabData.Artist = txtartist.Text;
            _tabFile.TabData.Title = txtsong.Text;
            _tabFile.TabData.Type = Tab.GetTabType(txttype.Text);
            _tabFile.TabData.Lyrics = txtlyrics.Text;
            _tabFile.TabData.Comment = txtcomment.Text;
            _tabFile.Save();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        }
    }
}