#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Tabster.Controls.Extensions;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Forms
{
    internal partial class ImportDialog : Form
    {
        private readonly TabsterDocumentProcessor<TablatureDocument> _documentProcessor = new TabsterDocumentProcessor<TablatureDocument>(TablatureDocument.FILE_VERSION, true);
        private readonly List<ITablatureFileImporter> _importers = new List<ITablatureFileImporter>();

        public ImportDialog()
        {
            InitializeComponent();
        }

        public ImportDialog(List<ITablatureFileImporter> importers)
            : this()
        {
            _importers = importers;
        }

        public TablatureDocument Tab { get; private set; }

        private void ImportDialog_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = string.Format("Tabster Files (*{0})|*{0}", TablatureDocument.FILE_EXTENSION);
            openFileDialog1.SetTabsterFilter(_importers);

            txtartist.Select(txtartist.Text.Length, 0);
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            Tab = new TablatureDocument(txtartist.Text.Trim(), txttitle.Text.Trim(), typeList.SelectedType, File.ReadAllText(txtimportfile.Text))
            {
                Source = new Uri(txtimportfile.Text),
                SourceType = chkusertab.Checked ? TablatureSourceType.UserCreated : TablatureSourceType.FileImport
            };
        }

        private void browsebtn_Click(object sender, EventArgs e)
        {
            txtartist.Text = string.Empty;
            txttitle.Text = string.Empty;

            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //tabster file
                if (openFileDialog1.FilterIndex == 1)
                {
                    var doc = _documentProcessor.Load(openFileDialog1.FileName);

                    if (doc != null)
                    {
                        txtartist.Text = doc.Artist;
                        txttitle.Text = doc.Title;
                        typeList.SelectedType = doc.Type;
                    }

                    else
                    {
                        MessageBox.Show("Error occured processing the document.", "Document Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else //custom importer
                {
                    var importer = _importers[openFileDialog1.FilterIndex - 1];

                    try
                    {
                        txtimportfile.Text = openFileDialog1.FileName;

                        var doc = importer.Import(openFileDialog1.FileName);

                        if (!string.IsNullOrEmpty(doc.Artist))
                            txtartist.Text = doc.Artist;

                        if (!string.IsNullOrEmpty(doc.Title))
                            txttitle.Text = doc.Title;

                        typeList.SelectedType = doc.Type;

                        ValidateData();
                    }

                    catch
                    {
                        MessageBox.Show("Error occured while importing.", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ValidateData(object sender = null, EventArgs e = null)
        {
            okbtn.Enabled = txtartist.Text.Trim().Length > 0 && txttitle.Text.Trim().Length > 0 && txtimportfile.Text.Trim().Length > 0;
        }
    }
}