#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster.Forms
{
    internal partial class RenameDialog : Form
    {
        public RenameDialog(string currentName)
        {
            InitializeComponent();
            Text = string.Format("Rename {0}", currentName);
        }

        public string NewName { get; private set; }

        private void okbtn_Click(object sender, EventArgs e)
        {
            if (txtname.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Please enter a valid song/artist name.", "Tab Data");
            }

            NewName = txtname.Text.Trim();
        }

        private void txtartist_TextChanged(object sender, EventArgs e)
        {
            okbtn.Enabled = txtname.Text.Trim().Length > 0;
        }
    }
}