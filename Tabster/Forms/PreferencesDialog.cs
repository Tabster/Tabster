#region

using System;
using System.Windows.Forms;
using Tabster.Properties;

#endregion

namespace Tabster.Forms
{
    public partial class PreferencesDialog : Form
    {
        public PreferencesDialog()
        {
            InitializeComponent();
            chkupdatestartup.Checked = Settings.Default.StartupUpdate;
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            Settings.Default.StartupUpdate = chkupdatestartup.Checked;
            Settings.Default.Save();
        }
    }
}