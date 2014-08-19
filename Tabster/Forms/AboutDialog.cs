#region

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Tabster.Properties;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            lblName.Text = string.Format("{0} {1}", Application.ProductName, new Version(Application.ProductVersion).ToShortString());
            lblCopyright.Text = Common.GetCopyrightString();
            txtLicense.Text = Resources.ApplicationLicense;
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel) sender).Text);
        }
    }
}