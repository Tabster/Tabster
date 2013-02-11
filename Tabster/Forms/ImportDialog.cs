#region

using System;
using System.IO;
using System.Windows.Forms;

#endregion

namespace Tabster.Forms
{
    public partial class ImportDialog : Form
    {
        public string tab_content;

        public ImportDialog()
        {
            InitializeComponent();
            txttype.DataSource = Constants.TabTypes;
            txtartist.Select(txtartist.Text.Length, 0);
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            if (txtsong.Text.Trim().Length <= 0 || txtartist.Text.Trim().Length <= 0 ||
                txtimportfile.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Please enter a valid song/artist name/file.", "Tab Data");
            }

            else
            {
                tab_content = File.ReadAllText(txtimportfile.Text);
            }

        }

        private void browsebtn_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
                                 {
                                     Title = "Import File - Tabster",
                                     AddExtension = true,
                                     Multiselect = false,
                                     Filter = "Text Files (*.txt)|*.txt"
                                 })
            {

                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    txtimportfile.Text = ofd.FileName;
                }
            }
        }

        private void ValidateData()
        {
            okbtn.Enabled = okbtn.Enabled = txtartist.Text.Trim().Length > 0 && txtsong.Text.Trim().Length > 0 && txtimportfile.Text.Trim().Length > 0;
        }

        private void txtimportfile_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtsong_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtartist_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }
    }
}