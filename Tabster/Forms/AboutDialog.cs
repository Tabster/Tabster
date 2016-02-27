#region

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Tabster.Core.Types;
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
            lblVersion.Text = string.Format("Version {0}",
                new TabsterVersion(Application.ProductVersion).ToString(TabsterVersionFormatFlags.Truncated));
            lblCopyright.Text = BrandingUtilities.GetCopyrightString(Assembly.GetExecutingAssembly());
            txtLicense.Text = Resources.ApplicationLicense;
            txtFontLicense.Text = Resources.SourceCodeProLicense;

            LoadPlugins();
        }

        private void LoadPlugins()
        {
            foreach (var pluginHost in Program.GetPluginController().GetPluginHosts())
            {
                if (pluginHost.Plugin.Guid != Guid.Empty && pluginHost.Enabled)
                {
                    var lvi = new ListViewItem {Text = pluginHost.Plugin.DisplayName ?? "N/A"};

                    lvi.SubItems.Add(pluginHost.Plugin.Version != null ? pluginHost.Plugin.Version.ToString() : "N/A");
                    lvi.SubItems.Add(pluginHost.Plugin.Author ?? "N/A");
                    lvi.SubItems.Add(pluginHost.FileInfo.FullName);

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