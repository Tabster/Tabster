#region

using System;
using System.IO;
using System.Windows.Forms;
using Tabster.Data;

#endregion

namespace Tabster.Forms
{
    internal partial class PlaylistDetailsDialog : Form
    {
        private readonly ITablaturePlaylistFile _file;
        private readonly FileInfo _fileInfo;
        private string _originalName;

        public PlaylistDetailsDialog(ITablaturePlaylistFile file, FileInfo fileInfo)
        {
            InitializeComponent();

            _file = file;
            _fileInfo = fileInfo;

            LoadData();
        }

        public bool PlaylistRenamed { get; private set; }

        private void LoadData()
        {
            _originalName = _file.Name;

            txtlocation.Text = _fileInfo.FullName;
            txtname.Text = _file.Name;

            lblFormat.Text += _file.FileHeader.Version.ToString();
            lblLength.Text += string.Format(" {0:n0} bytes", _fileInfo.Length);
            lblCreated.Text += string.Format(" {0}", _fileInfo.CreationTime);
            lblModified.Text += string.Format(" {0}", _fileInfo.LastWriteTime);

            foreach (var tab in _file)
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
            _file.Name = txtname.Text.Trim();
            _file.Save(_fileInfo.FullName);

            PlaylistRenamed = _file.Name != _originalName;
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            okbtn.Enabled = txtname.Text.Trim().Length > 0;
        }
    }
}