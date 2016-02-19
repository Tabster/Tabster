#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tabster.Properties;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    public partial class PluginManagerDialog : Form
    {
        private readonly Color _disabledColor = Color.Red;
        private readonly Color _enabledColor = Color.Green;
        private readonly List<PluginHost> _pluginHosts = new List<PluginHost>();
        private readonly Dictionary<PluginHost, bool> _pluginStatusMap = new Dictionary<PluginHost, bool>();

        public PluginManagerDialog()
        {
            InitializeComponent();

            _pluginHosts.AddRange(Program.GetPluginController().GetPluginHosts());

            LoadPlugins();
        }

        public bool PluginsModified { get; private set; }

        private void LoadPlugins()
        {
            _pluginStatusMap.Clear();

            listPlugins.Items.Clear();

            foreach (var pluginHost in _pluginHosts)
            {
                var lvi = new ListViewItem
                {
                    Tag = pluginHost.Plugin.Guid.ToString(),
                    Text = pluginHost.Plugin.DisplayName,
                    Checked = pluginHost.Enabled,
                    ForeColor = pluginHost.Enabled ? _enabledColor : _disabledColor
                };

                _pluginStatusMap[pluginHost] = pluginHost.Enabled;

                lvi.SubItems.Add(pluginHost.Enabled ? "Yes" : "No");

                listPlugins.Items.Add(lvi);
            }

            if (listPlugins.Items.Count == 0)
                listPlugins.Dock = DockStyle.Fill;
            else
                listPlugins.Items[0].Selected = true;
        }

        private void pluginsDirectorybtn_Click(object sender, System.EventArgs e)
        {
            Process.Start(Path.Combine(TabsterEnvironment.GetEnvironmentDirectoryPath(TabsterEnvironmentDirectory.CommonApplicationData), "Plugins"));
        }

        private void listPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPlugins.SelectedItems.Count > 0)
            {
                var plugin = _pluginHosts[listPlugins.SelectedItems[0].Index];

                lblPluginFilename.Text = string.Format("{0}...{1}{2}{1}{3}", Path.GetPathRoot(plugin.FileInfo.FullName), Path.DirectorySeparatorChar, Path.GetFileName(Path.GetDirectoryName(plugin.FileInfo.FullName)), plugin.FileInfo.Name);
                lblPluginAuthor.Text = plugin.Plugin.Author ?? "N/A";
                lblPluginVersion.Text = plugin.Plugin.Version != null
                    ? plugin.Plugin.Version.ToString()
                    : "N/A";
                lblPluginDescription.Text = plugin.Plugin.Description ?? "N/A";

                if (plugin.Plugin.Website != null)
                {
                    lblPluginHomepage.Text = plugin.Plugin.Website.ToString();
                    lblPluginHomepage.LinkArea = new LinkArea(0, plugin.Plugin.Website.ToString().Length);
                }

                else
                {
                    lblPluginHomepage.Text = "N/A";
                    lblPluginHomepage.LinkArea = new LinkArea(0, 0);
                }
            }
        }

        private void listPlugins_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = listPlugins.HitTest(e.X, e.Y).Item;

            if (item != null)
            {
                item.Checked = !item.Checked;
                item.ForeColor = item.Checked ? _enabledColor : _disabledColor;
                item.SubItems[colpluginEnabled.Index].Text = item.Checked ? "Yes" : "No";

                var plugin = _pluginHosts[item.Index];
                _pluginStatusMap[plugin] = item.Checked; //set temporary status

                PluginsModified = true;
            }
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel) sender).Text);
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            //plugins
            if (PluginsModified)
            {
                foreach (ListViewItem lvi in listPlugins.Items)
                {
                    var guid = new Guid(lvi.Tag.ToString());
                    var pluginEnabled = lvi.Checked;

                    var pluginHost = Program.GetPluginController().FindPluginByGuid(guid);

                    if (pluginHost.Enabled != pluginEnabled)
                        pluginHost.Enabled = pluginEnabled;

                    Settings.Default.DisabledPlugins.Remove(guid.ToString());

                    if (!pluginEnabled)
                        Settings.Default.DisabledPlugins.Add(guid.ToString());
                }
            }
        }
    }
}