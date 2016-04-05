#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tabster.Plugins;
using Tabster.Properties;

#endregion

namespace Tabster.Forms
{
    public partial class PluginManagerDialog : Form
    {
        private readonly Color _disabledColor = Color.Red;
        private readonly Color _enabledColor = Color.Green;
        private readonly List<PluginInstance> _pluginInstances = new List<PluginInstance>();
        private readonly Dictionary<PluginInstance, bool> _pluginStatusMap = new Dictionary<PluginInstance, bool>();
        private List<FeaturedPlugin> _featuredPlugins;

        public PluginManagerDialog()
        {
            InitializeComponent();

            _pluginInstances.AddRange(Program.GetPluginManager().GetPluginHosts());

            LoadPlugins();

            FeaturedPluginChecker.Completed += FeaturedPluginChecker_Completed;
            FeaturedPluginChecker.Check();
        }

        public bool PluginsModified { get; private set; }

        private Boolean IsPluginInstalled(string pluginName)
        {
            return _pluginInstances.Find(x => x.Plugin.DisplayName.Equals(pluginName, StringComparison.OrdinalIgnoreCase)) != null;
        }

        private void FeaturedPluginChecker_Completed(object sender, FeaturedPluginChecker.FeaturedPluginsResponseEventArgs e)
        {
            if (e.Error == null)
            {
                _featuredPlugins = e.Response.Plugins;
                foreach (var plugin in _featuredPlugins)
                {
                    var lvi = new ListViewItem
                    {
                        Tag = plugin,
                        Text = plugin.Name,
                        Checked = IsPluginInstalled(plugin.Name),
                        ForeColor = IsPluginInstalled(plugin.Name) ? _enabledColor : _disabledColor
                    };

                    lvi.SubItems.Add(IsPluginInstalled(plugin.Name) ? Resources.Yes : Resources.No);
                    lvi.SubItems.Add(plugin.Version.ToString());
                    listFeatured.Items.Add(lvi);

                }
            }

            else
            {
                MessageBox.Show(Resources.FeaturedPluginsErrorDialogCaption, Resources.FeaturedPlugins, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPlugins()
        {
            _pluginStatusMap.Clear();

            listPlugins.Items.Clear();

            foreach (var pluginHost in _pluginInstances)
            {
                var lvi = new ListViewItem
                {
                    Tag = pluginHost.Plugin.Guid.ToString(),
                    Text = pluginHost.Plugin.DisplayName,
                    Checked = pluginHost.Enabled,
                    ForeColor = pluginHost.Enabled ? _enabledColor : _disabledColor
                };

                _pluginStatusMap[pluginHost] = pluginHost.Enabled;

                lvi.SubItems.Add(pluginHost.Enabled ? Resources.Yes : Resources.No);

                listPlugins.Items.Add(lvi);
            }

            if (listPlugins.Items.Count > 0)
                listPlugins.Items[0].Selected = true;
            else
                LoadPluginInformation((PluginInstance) null);
        }

        private void LoadPluginInformation(FeaturedPlugin featuredPlugin)
        {
            lblPlaceholder.Visible = tabControl1.SelectedTab != tabFeatured || featuredPlugin == null;

            if (featuredPlugin != null)
            {
                lblPluginFilename.Text = Resources.NotAvailableAbbreviation;
                lblPluginAuthor.Text = featuredPlugin.Author ?? Resources.NotAvailableAbbreviation;
                lblPluginVersion.Text = featuredPlugin.Version != null ? featuredPlugin.Version.ToString() : Resources.NotAvailableAbbreviation;
                lblPluginDescription.Text = featuredPlugin.Description ?? Resources.NotAvailableAbbreviation;
                lblPluginHomepage.Text = featuredPlugin.Website != null ? featuredPlugin.Website.DnsSafeHost : Resources.NotAvailableAbbreviation;
                lblPluginHomepage.Tag = featuredPlugin.Website;
            }
        }

        private void LoadPluginInformation(PluginInstance pluginInstance)
        {
            lblPlaceholder.Visible = pluginInstance == null;

            if (pluginInstance != null)
            {
                lblPluginFilename.Text = string.Format("{0}...{1}{2}{1}{3}", Path.GetPathRoot(pluginInstance.FileInfo.FullName), Path.DirectorySeparatorChar, Path.GetFileName(Path.GetDirectoryName(pluginInstance.FileInfo.FullName)), pluginInstance.FileInfo.Name);
                lblPluginAuthor.Text = pluginInstance.Plugin.Author ?? Resources.NotAvailableAbbreviation;
                lblPluginVersion.Text = pluginInstance.Plugin.Version != null
                    ? pluginInstance.Plugin.Version.ToString()
                    : Resources.NotAvailableAbbreviation;
                lblPluginDescription.Text = pluginInstance.Plugin.Description ?? Resources.NotAvailableAbbreviation;

                lblPluginHomepage.Tag = pluginInstance.Plugin.Website.DnsSafeHost;

                if (pluginInstance.Plugin.Website != null)
                {
                    lblPluginHomepage.Text = pluginInstance.Plugin.Website.DnsSafeHost;
                    lblPluginHomepage.LinkArea = new LinkArea(0, lblPluginHomepage.Text.Length);
                }

                else
                {
                    lblPluginHomepage.Text = Resources.NotAvailableAbbreviation;
                    lblPluginHomepage.LinkArea = new LinkArea(0, 0);
                }
            }
        }

        private void pluginsDirectorybtn_Click(object sender, EventArgs e)
        {
            Process.Start(Program.GetPluginManager().WorkingDirectory);
        }

        private void listPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPlugins.SelectedItems.Count > 0)
            {
                var pluginHost = _pluginInstances.Count > listPlugins.SelectedItems[0].Index ? _pluginInstances[listPlugins.SelectedItems[0].Index] : null;
                LoadPluginInformation(pluginHost);
            }
        }

        private void listPlugins_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = listPlugins.HitTest(e.X, e.Y).Item;

            if (item != null)
            {
                item.Checked = !item.Checked;
                item.ForeColor = item.Checked ? _enabledColor : _disabledColor;
                item.SubItems[colpluginEnabled.Index].Text = item.Checked ? Resources.Yes : Resources.No;

                var plugin = _pluginInstances[item.Index];
                _pluginStatusMap[plugin] = item.Checked; //set temporary status

                PluginsModified = true;
            }
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel) sender).Tag.ToString());
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

                    var pluginHost = Program.GetPluginManager().FindPluginByGuid(guid);

                    if (pluginHost.Enabled != pluginEnabled)
                        pluginHost.Enabled = pluginEnabled;

                    Settings.Default.DisabledPlugins.Remove(guid.ToString());

                    if (!pluginEnabled)
                        Settings.Default.DisabledPlugins.Add(guid.ToString());
                }
            }
        }

        private void listFeatured_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFeatured.SelectedItems.Count > 0)
            {
                var plugin = _featuredPlugins.Count > listFeatured.SelectedItems[0].Index ? 
                    _featuredPlugins[listFeatured.SelectedItems[0].Index] : 
                    null;
                LoadPluginInformation(plugin);
            }
        }
    }
}