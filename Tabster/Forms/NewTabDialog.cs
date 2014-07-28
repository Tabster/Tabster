#region

using System;
using System.Windows.Forms;
using Tabster.Core.FileTypes;
using Tabster.Core.Types;

#endregion

namespace Tabster.Forms
{
    public partial class NewTabDialog : Form
    {
        public NewTabDialog()
        {
            InitializeComponent();

            txtartist.Text = Environment.UserName;
            txtartist.Select(txtartist.Text.Length, 0);
            txttype.DataSource = TabTypeUtilities.FriendlyStrings();
        }

        public NewTabDialog(string artist, string song, TabType type)
            : this()
        {
            txtartist.Text = artist;
            txttitle.Text = song;
            txttype.SelectedIndex = (int) type;

            ValidateInput();
        }

        public TablatureDocument Tab { get; private set; }

        private void ValidateInput(object sender = null, EventArgs e = null)
        {
            okbtn.Enabled = okbtn.Enabled = txtartist.Text.Trim().Length > 0 && txttitle.Text.Trim().Length > 0;
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            Tab = new TablatureDocument(txtartist.Text.Trim(), txttitle.Text.Trim(), TabTypeUtilities.FromFriendlyString(txttype.Text).Value, "")
                      {
                          SourceType = TablatureSourceType.UserCreated
                      };
        }
    }
}