#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tabster.Plugins;
using Tabster.Properties;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    public partial class PluginManagerDialog : Form
    {
        private readonly Color _disabledColor = Color.Red;
        private readonly Color _enabledColor = Color.Green;
        private readonly List<PluginInstance> _pluginHosts = new List<PluginInstance>();
        private readonly Dictionary<PluginInstance, bool> _pluginStatusMap = new Dictionary<PluginInstance, bool>();

        public PluginManagerDialog()
        {
            InitializeComponent();

            _pluginHosts.AddRange(Program.GetPluginController().GetPluginHosts());

            LoadPlugins();

            FeaturedPluginChecker.Completed += FeaturedPluginChecker_Completed;
            FeaturedPluginChecker.Check();
        }

        public bool PluginsModified { get; private set; }

        private void FeaturedPluginChecker_Completed(object sender, FeaturedPluginChecker.FeaturedPluginsResponseEventArgs e)
        {
            if (e.Error == null)
            {
                listBox1.DisplayMember = "Name";
                listBox1.DataSource = e.Response.Plugins;
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
            Process.Start(Path.Combine(TabsterEnvironment.GetEnvironmentDirectoryPath(TabsterEnvironmentDirectory.UserData), "Plugins"));
        }

        private void listPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pluginHost = _pluginHosts.Count > listPlugins.SelectedItems[0].Index ? _pluginHosts[listPlugins.SelectedItems[0].Index] : null;
            LoadPluginInformation(pluginHost);
        }

        private void listPlugins_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = listPlugins.HitTest(e.X, e.Y).Item;

            if (item != null)
            {
                item.Checked = !item.Checked;
                item.ForeColor = item.Checked ? _enabledColor : _disabledColor;
                item.SubItems[colpluginEnabled.Index].Text = item.Checked ? Resources.Yes : Resources.No;

                var plugin = _pluginHosts[item.Index];
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

                    var pluginHost = Program.GetPluginController().FindPluginByGuid(guid);

                    if (pluginHost.Enabled != pluginEnabled)
                        pluginHost.Enabled = pluginEnabled;

                    Settings.Default.DisabledPlugins.Remove(guid.ToString());

                    if (!pluginEnabled)
                        Settings.Default.DisabledPlugins.Add(guid.ToString());
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                var featuredPlugin = (listBox1.SelectedItem as FeaturedPlugin);
                LoadPluginInformation(featuredPlugin);
            }
            else
            {
                LoadPluginInformation((FeaturedPlugin) null);
            }
        }
    }
}