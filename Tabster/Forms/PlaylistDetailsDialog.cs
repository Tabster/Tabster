#region

using System;
using System.Windows.Forms;
using Tabster.Data;

#endregion

namespace Tabster.Forms
{
    internal partial class PlaylistDetailsDialog : Form
    {
        private readonly TablaturePlaylist _playlist;
        private string _originalName;

        public PlaylistDetailsDialog(TablaturePlaylist playlist)
        {
            InitializeComponent();

            _playlist = playlist;

            LoadData();
        }

        public bool PlaylistRenamed { get; private set; }

        private void LoadData()
        {
            _originalName = _playlist.Name;

            txtname.Text = _playlist.Name;

            lblCreated.Text += string.Format(" {0}", _playlist.Created.Value.ToLocalTime());

            foreach (var tab in _playlist)
            {
                listView1.Items.Add(string.Format("{0} - {1}", tab.File.Artist, tab.File.Title));
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            _playlist.Name = txtname.Text.Trim();

            PlaylistRenamed = _playlist.Name != _originalName;
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            okbtn.Enabled = txtname.Text.Trim().Length > 0;
        }
    }
}