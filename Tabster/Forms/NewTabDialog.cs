#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster.Forms
{
    public partial class NewTabDialog : Form
    {
        public string tab_content;

        public NewTabDialog()
        {
            InitializeComponent();
            txttype.DataSource = Constants.TabTypes;
            txtartist.Text = Environment.UserName;
            txtartist.Select(txtartist.Text.Length, 0);
        }

        public NewTabDialog(string artist, string song, TabType type)
        {
            txttype.DataSource = Constants.TabTypes;
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