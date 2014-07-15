#region

using System;
using System.Windows.Forms;
using Tabster.Core;

#endregion

namespace Tabster.Forms
{
    public partial class NewTabDialog : Form
    {
        public NewTabDialog()
        {
            InitializeComponent();

            txtartist.Text = Environment.UserName;
            txtartist.Select(txtartist.Text.Length, 0);

            foreach (TabType type in Enum.GetValues(typeof(TabType)))
                txttype.Items.Add(type.ToFriendlyString());
        }

        public NewTabDialog(string artist, string song, TabType type) : this()
        {
            txtartist.Text = artist;
            txtsong.Text = song;
            txttype.SelectedIndex = (int) type;

            ValidateInput();
        }

        public Tab TabData { get; private set; }

        private void ValidateInput(object sender = null, EventArgs e = null)
        {
            okbtn.Enabled = okbtn.Enabled = txtartist.Text.Trim().Length > 0 && txtsong.Text.Trim().Length > 0 && txttype.SelectedIndex > 0;
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            TabData = new Tab(txtartist.Text.Trim(), txtsong.Text.Trim(), Tab.GetTabType(txttype.Text), "") {SourceType = TabSource.UserCreated};
        }
    }
}