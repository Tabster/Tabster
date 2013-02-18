#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster.Forms
{
    public partial class NewTabDialog : Form
    {
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
            if (txtsong.Text.Trim().Length <= 0 || txtartist.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Please enter a valid song/artist name.", "Tab Data");
            }
        }
    }
}