#region

using System;
using System.IO;
using System.Windows.Forms;

#endregion

namespace Tabster.Forms
{
    public partial class NewTabDialog : Form
    {
        public Tab TabData { get; private set; }

        public NewTabDialog()
        {
            InitializeComponent();
            txttype.DataSource = Tab.TabTypes;
            txtartist.Text = Environment.UserName;
            txtartist.Select(txtartist.Text.Length, 0);
        }

        public NewTabDialog(string artist, string song, TabType type)
        {
            InitializeComponent();
            txttype.DataSource = Tab.TabTypes;
            txtartist.Text = artist;
            txtsong.Text = song;
            txttype.SelectedIndex = (int)type;
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            TabData = new Tab(txtartist.Text.Trim(), txtsong.Text.Trim(), Tab.GetTabType(txttype.Text), "") { SourceType = TabSource.UserCreated };
        }

        private void ValidateData()
        {
            okbtn.Enabled = okbtn.Enabled = txtartist.Text.Trim().Length > 0 && txtsong.Text.Trim().Length > 0;
        }

        private void txtartist_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtsong_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }
    }
}