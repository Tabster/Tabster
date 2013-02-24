#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster.Forms
{
    public partial class PlaylistDetailsDialog : Form
    {
        private readonly PlaylistFile _playlistFile;

        public PlaylistDetailsDialog(PlaylistFile playlist)
        {
            InitializeComponent();
            _playlistFile = playlist;
            LoadData();
        }

        private void LoadData()
        {
            txtlocation.Text = _playlistFile.FileInfo.FullName;

            txtname.Text = _playlistFile.PlaylistData.Name;

            lblFormat.Text += _playlistFile.FileVersion;
            lblLength.Text += string.Format(" {0:n0} bytes", _playlistFile.FileInfo.Length);
            lblCreated.Text += string.Format(" {0}", _playlistFile.FileInfo.CreationTime);
            lblModified.Text += string.Format(" {0}", _playlistFile.FileInfo.LastWriteTime);

            foreach (var tab in _playlistFile.PlaylistData)
            {
                listView1.Items.Add(tab.TabData.Artist + " - " + tab.TabData.Title);
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            _playlistFile.PlaylistData.Name = txtname.Text;
            _playlistFile.Save();
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            okbtn.Enabled = txtname.Text.Trim().Length > 0;
        }
    }
}