#region

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Tabster.Properties;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    internal partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            lblVersion.Text = string.Format("Version {0}", new Version(Application.ProductVersion).ToShortVersionString());
            lblCopyright.Text = BrandingUtilities.GetCopyrightString(Assembly.GetExecutingAssembly());
            txtLicense.Text = Resources.ApplicationLicense;
            txtFontLicense.Text = Resources.SourceCodeProLicense;

            LoadPlugins();
        }

        private void LoadPlugins()
        {
            foreach (var pluginHost in Program.PluginController)
            {
                if (pluginHost.Plugin.Guid != Guid.Empty && Program.PluginController.IsEnabled(pluginHost.Plugin.Guid))
                {
                    var lvi = new ListViewItem { Text = pluginHost.Plugin.DisplayName ?? "N/A" };

                    lvi.SubItems.Add(pluginHost.Plugin.Version != null ? pluginHost.Plugin.Version.ToString() : "N/A");
                    lvi.SubItems.Add(pluginHost.Plugin.Author ?? "N/A");
                    lvi.SubItems.Add(Path.GetFileName(pluginHost.Assembly.Location));

                    listPlugins.Items.Add(lvi);
                }
            }

            if (listPlugins.Items.Count > 0)
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
            Process.Start("http://tabster.org");
        }
    }
}