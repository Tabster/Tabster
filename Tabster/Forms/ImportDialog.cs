#region

using System;
using System.IO;
using System.Windows.Forms;
using Tabster.Core;

#endregion

namespace Tabster.Forms
{
    public partial class ImportDialog : Form
    {
        public ImportDialog()
        {
            InitializeComponent();
            txttype.DataSource = Tab.TabTypes;
            txtartist.Select(txtartist.Text.Length, 0);
        }

        public Tab TabData { get; private set; }

        private void okbtn_Click(object sender, EventArgs e)
        {
            TabData = new Tab(txtartist.Text.Trim(), txtsong.Text.Trim(), Tab.GetTabType(txttype.Text), File.ReadAllText(txtimportfile.Text)) {Source = new Uri(txtimportfile.Text), SourceType = TabSource.FileImport};
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