#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster.Forms
{
    public partial class SearchLibraryDialog : Form
    {
        private readonly Form1 MainForm;
        private string previousSearch;

        public SearchLibraryDialog()
        {
            InitializeComponent();
        }

        public SearchLibraryDialog(Form1 mainForm) : this()
        {
            MainForm = mainForm;
        }

        private void txtartist_TextChanged(object sender, EventArgs e)
        {
            if (MainForm != null)
            {
                var currentSearch = txtname.Text.Trim();

                var validSearch = (previousSearch == null) || (previousSearch != null && currentSearch.Equals(previousSearch, StringComparison.OrdinalIgnoreCase));

                if (validSearch)
                {
                    previousSearch = currentSearch;
                    //MainForm.SetFilter(txtname.Text);
                }
            }
        }
    }
}