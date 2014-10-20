#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster.Forms
{
    internal partial class NewPlaylistDialog : Form
    {
        public string PlaylistName;

        public NewPlaylistDialog()
        {
            InitializeComponent();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            if (txtname.Text.Trim().Length > 0)
            {
                PlaylistName = txtname.Text.Trim();
            }
        }

        private void txtartist_TextChanged(object sender, EventArgs e)
        {
            okbtn.Enabled = txtname.Text.Trim().Length > 0;
        }
    }
}