#region

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Tabster.Properties;
using Tabster.Utilities.Extensions;
using Tabster.Utilities.Reflection;

#endregion

namespace Tabster.Forms
{
    internal partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            lblName.Text = string.Format("{0} {1}", Application.ProductName, new Version(Application.ProductVersion).ToShortString());
            lblCopyright.Text = AssemblyUtilities.GetCopyrightString(Assembly.GetExecutingAssembly());
            txtLicense.Text = Resources.ApplicationLicense;
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.fatcow.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.iconshock.com/");
        }
    }
}