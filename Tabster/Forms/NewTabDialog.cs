#region

using System;
using System.Windows.Forms;
using Tabster.Core.Types;

#endregion

namespace Tabster.Forms
{
    internal partial class NewTabDialog : Form
    {
        public NewTabDialog()
        {
            InitializeComponent();

            txtArtist.Text = Environment.UserName;
            txtArtist.Select(txtArtist.Text.Length, 0);
        }

        public NewTabDialog(string artist, string title, TablatureType type)
            : this()
        {
            txtArtist.Text = artist;
            txtTitle.Text = title;
            typeList.SelectedType = type;

            ValidateInput();
        }

        public AttributedTablature Tab { get; private set; }

        private void ValidateInput(object sender = null, EventArgs e = null)
        {
            okbtn.Enabled = okbtn.Enabled = txtArtist.Text.Trim().Length > 0 && txtTitle.Text.Trim().Length > 0;
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            Tab = new AttributedTablature(txtArtist.Text.Trim(), txtTitle.Text.Trim(), typeList.SelectedType);
        }
    }
}