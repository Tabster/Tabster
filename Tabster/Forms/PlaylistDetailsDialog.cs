#region

using System;
using System.Windows.Forms;
using Tabster.Data;

#endregion

namespace Tabster.Forms
{
    internal partial class PlaylistDetailsDialog : Form
    {
        private readonly ITablaturePlaylistFile _tablaturePlaylistFile;
        private string _originalName;

        public PlaylistDetailsDialog(ITablaturePlaylistFile tablaturePlaylist)
        {
            InitializeComponent();
            _tablaturePlaylistFile = tablaturePlaylist;
            LoadData();
        }

        public bool PlaylistRenamed { get; private set; }

        private void LoadData()
        {
            _originalName = _tablaturePlaylistFile.Name;

            txtlocation.Text = _tablaturePlaylistFile.FileInfo.FullName;
            txtname.Text = _tablaturePlaylistFile.Name;

            lblFormat.Text += _tablaturePlaylistFile.FileHeader.Version.ToString();
            lblLength.Text += string.Format(" {0:n0} bytes", _tablaturePlaylistFile.FileInfo.Length);
            lblCreated.Text += string.Format(" {0}", _tablaturePlaylistFile.FileInfo.CreationTime);
            lblModified.Text += string.Format(" {0}", _tablaturePlaylistFile.FileInfo.LastWriteTime);

            foreach (ITablatureFile tab in _tablaturePlaylistFile)
            {
                listView1.Items.Add(string.Format("{0} - {1}", tab.Artist, tab.Title));
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            _tablaturePlaylistFile.Name = txtname.Text.Trim();
            _tablaturePlaylistFile.Save(_tablaturePlaylistFile.FileInfo.FullName);

            PlaylistRenamed = _tablaturePlaylistFile.Name != _originalName;
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            okbtn.Enabled = txtname.Text.Trim().Length > 0;
        }
    }
}