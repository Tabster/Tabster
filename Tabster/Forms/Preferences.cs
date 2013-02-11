#region

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Tabster.Properties;

#endregion

namespace Tabster.Forms
{
    public partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();
            chkupdatestartup.Checked = Settings.Default.StartupUpdate;
            chksplash.Checked = Settings.Default.ShowSplash;
        }

        private void internetpropertiesbtn_Click(object sender, EventArgs e)
        {
            Process.Start(@"inetcpl.cpl");
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            Settings.Default.StartupUpdate = chkupdatestartup.Checked;
            Settings.Default.ShowSplash = chksplash.Checked;
            Settings.Default.Save();
        }
    }
}