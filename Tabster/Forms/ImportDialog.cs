#region

using System;
using System.IO;
using System.Windows.Forms;
using Tabster.Core.Data;
using Tabster.Core.Types;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    public partial class ImportDialog : Form
    {
        public ImportDialog()
        {
            InitializeComponent();

            txtartist.Select(txtartist.Text.Length, 0);
            txttype.DataSource = TabTypeUtilities.FriendlyStrings();
        }

        public TablatureDocument Tab { get; private set; }

        private void okbtn_Click(object sender, EventArgs e)
        {
            Tab = new TablatureDocument(txtartist.Text.Trim(), txttitle.Text.Trim(), TabTypeUtilities.FromFriendlyString(txttype.Text).Value, File.ReadAllText(txtimportfile.Text))
                      {
                          Source = new Uri(txtimportfile.Text),
                          SourceType = chkusertab.Checked ? TablatureSourceType.UserCreated : TablatureSourceType.FileImport,
                          Method = Common.GetTablatureDocumentMethodString()
                      };
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

        private void ValidateData(object sender = null, EventArgs e = null)
        {
            okbtn.Enabled = okbtn.Enabled = txtartist.Text.Trim().Length > 0 && txttitle.Text.Trim().Length > 0 && txtimportfile.Text.Trim().Length > 0;
        }
    }
}