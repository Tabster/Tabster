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
            Text = string.Format("Tabster v{0}", new Version(Application.ProductVersion).ToShortString());
            lblname.Text = string.Format("Tabster {0}", new Version(Application.ProductVersion).ToShortString());
            pictureBox1.Image = Resources.guitar128;
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel) sender).Text);
        }
    }
}