#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Tabster.Controls.Extensions;
using Tabster.Core.Data;
using Tabster.Core.Data.Processing;
using Tabster.Core.Types;
using Tabster.LocalUtilities;

#endregion

namespace Tabster.Forms
{
    public partial class ImportDialog : Form
    {
        private readonly List<ITablatureFileImporter> _importers = new List<ITablatureFileImporter> {new TabsterFileProcessor()};

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
            openFileDialog1.SetTabsterFilter(_importers);

            txtartist.Select(txtartist.Text.Length, 0);
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            Tab = new TablatureDocument(txtartist.Text.Trim(), txttitle.Text.Trim(), typeList.SelectedType, File.ReadAllText(txtimportfile.Text))
                      {
                          Source = new Uri(txtimportfile.Text),
                          SourceType = chkusertab.Checked ? TablatureSourceType.UserCreated : TablatureSourceType.FileImport,
                          Method = Common.GetTablatureDocumentMethodString()
                      };
        }

        private void browsebtn_Click(object sender, EventArgs e)
        {
            txtartist.Text = string.Empty;
            txttitle.Text = string.Empty;

            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
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

        private void ValidateData(object sender = null, EventArgs e = null)
        {
            okbtn.Enabled = txtartist.Text.Trim().Length > 0 && txttitle.Text.Trim().Length > 0 && txtimportfile.Text.Trim().Length > 0;
        }
    }

    internal class TabsterFileProcessor : ITablatureFileImporter
    {
        private readonly TabsterDocumentProcessor<TablatureDocument> _processor = new TabsterDocumentProcessor<TablatureDocument>(TablatureDocument.FILE_VERSION, true);

        public TabsterFileProcessor()
        {
            FileType = new FileType("Tabster File", TablatureDocument.FILE_EXTENSION);
        }

        #region Implementation of ITablatureDocumentImporter

        public TablatureDocument Import(string fileName)
        {
            return _processor.Load(fileName);
        }

        public TablatureDocument Import(string fileName, string artist, string title, TablatureType type)
        {
            var doc = Import(fileName);

            if (doc != null)
            {
                doc.Artist = artist;
                doc.Title = title;
                doc.Type = type;
            }

            return doc;
        }

        #endregion

        #region ITablatureFileImporter Members

        public FileType FileType { get; private set; }

        #endregion
    }
}