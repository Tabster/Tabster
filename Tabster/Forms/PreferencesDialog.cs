#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Tabster.Core.Searching;
using Tabster.Core.Types;
using Tabster.Properties;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    internal partial class PreferencesDialog : Form
    {
        public enum PreferencesSection
        {
            General,
            Printing,
            Plugins,
            Network,
            Searching
        }

        private readonly Color _disabledColor = Color.Red;
        private readonly Color _enabledColor = Color.Green;
        private readonly Dictionary<PluginHost, bool> _pluginStatusMap = new Dictionary<PluginHost, bool>();
        private readonly List<PluginHost> _pluginHosts = new List<PluginHost>();


        public PreferencesDialog()
        {
            InitializeComponent();

            _pluginHosts.AddRange(Program.PluginController.GetPluginHosts());

            LoadPreferences();

            LoadPlugins();

            LoadSearchEngines(false);
        }

        public PreferencesDialog(PreferencesSection section) : this()
        {
            tabControl1.SelectedIndex = (int) section;
        }

        public bool PluginsModified { get; private set; }
        public bool SearchEnginesModified { get; private set; }

        private void LoadPreferences()
        {
            chkupdatestartup.Checked = Settings.Default.StartupUpdate;

            //printing
            printColorPreview.BackColor = Settings.Default.PrintColor;
            chkPrintPageNumbers.Checked = Settings.Default.PrintPageNumbers;
            chkPrintTimestamp.Checked = Settings.Default.PrintTimestamp;

            //proxy settings
            var proxySettings = UserSettingsUtilities.ProxySettings;

            if (proxySettings.Configuration == ProxyConfiguration.System)
                radioSystemProxy.Checked = true;
            else if (proxySettings.Configuration == ProxyConfiguration.Manual && proxySettings.Proxy != null)
                radioManualProxy.Checked = true;
            else
                radioNoProxy.Checked = true;

            if (proxySettings.Proxy != null)
            {
                txtProxyAddress.Text = Settings.Default.ProxyHost;
                numProxyPort.Value = Settings.Default.ProxyPort;
                txtProxyUsername.Text = Settings.Default.ProxyUsername;
                txtProxyPassword.Text = Settings.Default.ProxyPassword;
            }
        }

        private void SavePreferences()
        {
            //plugins
            if (PluginsModified)
            {
                foreach (ListViewItem lvi in listPlugins.Items)
                {
                    var guid = new Guid(lvi.Tag.ToString());
                    var pluginEnabled = lvi.Checked;

                    Program.PluginController.SetStatus(guid, pluginEnabled);

                    Settings.Default.DisabledPlugins.Remove(guid.ToString());

                    if (!pluginEnabled)
                        Settings.Default.DisabledPlugins.Add(guid.ToString());
                }
            }

            //search engines
            if (SearchEnginesModified)
            {
                foreach (ListViewItem lvi in listSearchEngines.Items)
                {
                    var engine = _searchEngines[lvi.Index];
                    var plugin = Program.PluginController.GetHostByType(engine.GetType());
                    var id = UserSettingsUtilities.GetSearchEngineIdentifier(plugin, engine);

                    if (id == null)
                        continue;

                    var engineEnabled = lvi.Checked;

                    Settings.Default.DisabledSearchEngines.Remove(id);

                    if (!engineEnabled)
                        Settings.Default.DisabledSearchEngines.Add(id);
                }
            }

            //printing
            Settings.Default.PrintColor = printColorPreview.BackColor;
            Settings.Default.PrintPageNumbers = chkPrintPageNumbers.Checked;
            Settings.Default.PrintTimestamp = chkPrintTimestamp.Checked;

            //network
            var proxyConfig = ProxyConfiguration.None;

            if (radioNoProxy.Checked)
                proxyConfig = ProxyConfiguration.None;
            else if (radioSystemProxy.Checked)
                proxyConfig = ProxyConfiguration.System;
            else if (radioManualProxy.Checked)
                proxyConfig = ProxyConfiguration.Manual;

            var customProxy = GetProxyFromInput();

            Settings.Default.ProxyConfig = proxyConfig.ToString();

            if (customProxy != null)
            {
                Settings.Default.ProxyHost = customProxy.Address.Host;
                Settings.Default.ProxyPort = (ushort) customProxy.Address.Port;

                if (customProxy.Credentials != null)
                {
                    var credentials = (NetworkCredential) customProxy.Credentials;

                    Settings.Default.ProxyUsername = credentials.UserName != null
                        ? ((NetworkCredential) customProxy.Credentials).UserName
                        : null;
                    Settings.Default.ProxyPassword = credentials.Password != null
                        ? ((NetworkCredential) customProxy.Credentials).Password
                        : null;
                }

                UserSettingsUtilities.ProxySettings.ManualProxyParameters = new ManualProxyParameters(
                    customProxy.Address.Host, (ushort) customProxy.Address.Port,
                    customProxy.Credentials);
            }

            //apply settings to active proxy config
            UserSettingsUtilities.ProxySettings.Configuration = proxyConfig;

            Settings.Default.StartupUpdate = chkupdatestartup.Checked;

            Settings.Default.Save();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            if (!ValidateProxyInput())
            {
                MessageBox.Show("Invalid proxy settings.", "Proxy Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }

            SavePreferences();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pluginsDirectorybtn.Visible = tabControl1.SelectedTab == tabPlugins;
        }


        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel) sender).Text);
        }

        #region Network

        private bool ValidateProxyInput()
        {
            return !radioManualProxy.Checked || GetProxyFromInput() != null;
        }

        /// <summary>
        ///     Attempts to create a proxy object based off user iput.
        /// </summary>
        private WebProxy GetProxyFromInput()
        {
            if (string.IsNullOrEmpty(txtProxyAddress.Text))
                return null;
            var proxyAddressInput = txtProxyAddress.Text;

            proxyAddressInput += string.Format(":{0}", numProxyPort.Value);

            var proxy = new WebProxy(proxyAddressInput);

            if (!string.IsNullOrEmpty(txtProxyUsername.Text) && !string.IsNullOrEmpty(txtProxyPassword.Text))
            {
                var creds = new NetworkCredential
                {
                    UserName = txtProxyUsername.Text,
                    Password = txtProxyPassword.Text
                };
                proxy.Credentials = creds;
            }

            return proxy;
        }

        private void radioCustomProxy_CheckedChanged(object sender, EventArgs e)
        {
            customProxyPanel.Enabled = radioManualProxy.Checked;
        }

        private void chkProxyAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            txtProxyUsername.Enabled =
                txtProxyPassword.Enabled = chkShowProxyPassword.Enabled = chkProxyAuthentication.Checked;
        }

        private void chkShowProxyPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtProxyPassword.PasswordChar = chkShowProxyPassword.Checked ? '\0' : '*';
        }

        private void btnEditSystemProxy_Click(object sender, EventArgs e)
        {
            //thanks to google chrome
            Process.Start("inetcpl.cpl", ",4");
        }

        #endregion

        #region Printing

        private void printColorPreview_Click(object sender, EventArgs e)
        {
            if (printColorDialog.ShowDialog() == DialogResult.OK)
            {
                printColorPreview.BackColor = printColorDialog.Color;
            }
        }

        #endregion

        #region Plugins

        private void pluginsDirectorybtn_Click(object sender, EventArgs e)
        {
            Process.Start(Program.PluginController.WorkingDirectory);
        }

        private void listPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPlugins.SelectedItems.Count > 0)
            {
                var plugin = _pluginHosts[listPlugins.SelectedItems[0].Index];

                lblPluginFilename.Text = Path.GetFileName(plugin.Assembly.Location);
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

                //if it contains search engines, we need to reload search engine list
                var isSearchPlugin = plugin.GetClassInstances<ITablatureSearchEngine>().Any();

                if (isSearchPlugin)
                {
                    LoadSearchEngines(true);
                    SearchEnginesModified = true;
                }

                PluginsModified = true;
            }
        }

        private void LoadPlugins()
        {
            _pluginStatusMap.Clear();

            listPlugins.Items.Clear();

            foreach (var pluginHost in _pluginHosts)
            {
                var enabled = Program.PluginController.IsEnabled(pluginHost.Plugin.Guid);

                var lvi = new ListViewItem
                {
                    Tag = pluginHost.Plugin.Guid.ToString(),
                    Text = pluginHost.Plugin.DisplayName,
                    Checked = enabled,
                    ForeColor = enabled ? _enabledColor : _disabledColor
                };

                _pluginStatusMap[pluginHost] = enabled;

                lvi.SubItems.Add(enabled ? "Yes" : "No");

                listPlugins.Items.Add(lvi);
            }

            if (listPlugins.Items.Count == 0)
                listPlugins.Dock = DockStyle.Fill;
            else
                listPlugins.Items[0].Selected = true;
        }

        #endregion

        #region Search Engines

        private readonly List<ITablatureSearchEngine> _searchEngines = new List<ITablatureSearchEngine>();

        private void LoadSearchEngines(bool useUnsavedSettings)
        {
            _searchEngines.Clear();

            listSearchEngines.Items.Clear();

            var searchPluginMap = new Dictionary<ITablatureSearchEngine, PluginHost>();

            foreach (var plugin in _pluginHosts)
            {
                foreach (var engine in plugin.GetClassInstances<ITablatureSearchEngine>())
                {
                    _searchEngines.Add(engine);
                    searchPluginMap[engine] = plugin;
                }
            }

            foreach (var searchPluginPair in searchPluginMap)
            {
                var enabled = useUnsavedSettings ?
                    _pluginStatusMap[searchPluginPair.Value] :
                    !Settings.Default.DisabledSearchEngines.Contains(UserSettingsUtilities.GetSearchEngineIdentifier(searchPluginPair.Value, searchPluginPair.Key));

                var lvi = new ListViewItem
                {
                    Text = searchPluginPair.Key.Name,
                    Checked = enabled,
                    ForeColor = enabled ? _enabledColor : _disabledColor
                };

                lvi.SubItems.Add(enabled ? "Yes" : "No");

                listSearchEngines.Items.Add(lvi);
            }

            if (listSearchEngines.Items.Count == 0)
                listSearchEngines.Dock = DockStyle.Fill;
            else
                listSearchEngines.Items[0].Selected = true;
        }

        private void listSearchEngines_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = listSearchEngines.HitTest(e.X, e.Y).Item;

            if (item != null)
            {
                var engine = _searchEngines[item.Index];

                var isPluginEnabled = _pluginStatusMap[Program.PluginController.GetHostByType(engine.GetType())];

                if (isPluginEnabled)
                {
                    item.Checked = !item.Checked;
                    item.ForeColor = item.Checked ? _enabledColor : _disabledColor;
                    item.SubItems[colpluginEnabled.Index].Text = item.Checked ? "Yes" : "No";

                    SearchEnginesModified = true;
                }

                else
                {
                    MessageBox.Show(
                        string.Format("The owner plugin for this search engine is disabled.{0}Please enable it to enable this search engine.", Environment.NewLine),
                        "Plugin Disabled");
                }
            }
        }

        private void listSearchEngines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSearchEngines.SelectedItems.Count > 0)
            {
                var engine = _searchEngines[listSearchEngines.SelectedItems[0].Index];

                if (engine.Homepage != null)
                {
                    lblSearchEngineHomepage.Text = engine.Homepage.ToString();
                    lblSearchEngineHomepage.LinkArea = new LinkArea(0, engine.Homepage.ToString().Length);
                }

                else
                {
                    lblSearchEngineHomepage.Text = "N/A";
                    lblSearchEngineHomepage.LinkArea = new LinkArea(0, 0);
                }

                lblSearchEngineSupportsRatings.Text = engine.SupportsRatings ? "Yes" : "No";

                listBox1.Items.Clear();

                foreach (var t in TablatureType.GetKnownTypes().Where(engine.SupportsTabType))
                {
                    listBox1.Items.Add(t.Name);
                }
            }
        }

        #endregion
    }
}