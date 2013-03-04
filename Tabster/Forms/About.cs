#region

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Tabster.Properties;

#endregion

namespace Tabster.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            var version = new Version(Application.ProductVersion);
            lblname.Text = string.Format("Tabster {0}", version);
            pictureBox1.Image = Resources.guitar128;
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel)sender).Text);
        }
    }
}