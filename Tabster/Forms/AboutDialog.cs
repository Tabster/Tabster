﻿#region

using System;
using System.Diagnostics;
using System.IO;
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
            lblVersion.Text = string.Format("Version {0}", new Version(Application.ProductVersion).ToShortString());
            lblCopyright.Text = AssemblyUtilities.GetCopyrightString(Assembly.GetExecutingAssembly());
            txtLicense.Text = Resources.ApplicationLicense;

            LoadPlugins();
        }

        private void LoadPlugins()
        {
            foreach (var plugin in Program.pluginController)
            {
                if (plugin.GUID != Guid.Empty && Program.pluginController.IsEnabled(plugin.GUID))
                {
                    var lvi = new ListViewItem() {Text = plugin.PluginAttributes.DisplayName ?? "N/A"};

                    lvi.SubItems.Add(plugin.PluginAttributes.Version != null ? plugin.PluginAttributes.Version.ToString() : "N/A");
                    lvi.SubItems.Add(plugin.PluginAttributes.Author ?? "N/A");
                    lvi.SubItems.Add(Path.GetFileName(plugin.Assembly.Location));

                    listPlugins.Items.Add(lvi);
                }
            }

            if (listPlugins.Columns.Count > 0)
                listPlugins.AutoResizeColumn(listPlugins.Columns.Count - 1, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.fatcow.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.iconshock.com/");
        }

        private void btnHomepage_Click(object sender, EventArgs e)
        {
            Process.Start("http://nateshoffner.com");
        }
    }
}