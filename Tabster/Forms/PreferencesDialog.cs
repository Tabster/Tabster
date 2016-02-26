#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Tabster.Core.Searching;
using Tabster.Core.Types;
using Tabster.Database;
using Tabster.Plugins;
using Tabster.Properties;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    internal partial class PreferencesDialog : Form
    {
        private readonly RecentFilesManager _recentFilesManager;

        public enum PreferencesSection
        {
            General,
            Printing,
            Network,
            Searching
        }

        private readonly Color _disabledColor = Color.Red;
        private readonly Color _enabledColor = Color.Green;

        public PreferencesDialog(RecentFilesManager recentFilesManager)
        {
            _recentFilesManager = recentFilesManager;
            InitializeComponent();

            LoadPreferences();

            LoadSearchEngines();

            radioSystemProxy.Visible = btnEditSystemProxy.Visible = MonoUtilities.GetPlatform() == MonoUtilities.Platform.Windows;
        }

        public PreferencesDialog(RecentFilesManager recentFilesManager, PreferencesSection section) : this(recentFilesManager)
        {
            tabControl1.SelectedIndex = (int) section;
        }

        public bool SearchEnginesModified { get; private set; }
        public bool MaxRecentItemsModified { get; private set; }
        public bool RecentItemsCleared { get; private set; }

        private void LoadPreferences()
        {
            chkUpdates.Checked = Settings.Default.StartupUpdate;
            chkStripVersionedNames.Checked = Settings.Default.StripVersionedNames;
            numMaxRecentItems.Value = Settings.Default.MaxRecentItems;

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
            //search engines
            if (SearchEnginesModified)
            {
                foreach (ListViewItem lvi in listSearchEngines.Items)
                {
                    var engine = _searchEngines[lvi.Index];
                    var plugin = Program.GetPluginController().GetHostByType(engine.GetType());
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

            if (numMaxRecentItems.Value != Settings.Default.MaxRecentItems)
            {
                MaxRecentItemsModified = true;
                Settings.Default.MaxRecentItems = (int) numMaxRecentItems.Value;
            }

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

            Settings.Default.StartupUpdate = chkUpdates.Checked;
            Settings.Default.StripVersionedNames = chkStripVersionedNames.Checked;

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

        #region Search Engines

        private readonly List<ITablatureSearchEngine> _searchEngines = new List<ITablatureSearchEngine>();

        private void LoadSearchEngines()
        {
            _searchEngines.Clear();

            listSearchEngines.Items.Clear();

            var searchPluginMap = new Dictionary<ITablatureSearchEngine, PluginHost>();

            foreach (var plugin in Program.GetPluginController().GetPluginHosts())
            {
                foreach (var engine in plugin.GetClassInstances<ITablatureSearchEngine>())
                {
                    _searchEngines.Add(engine);
                    searchPluginMap[engine] = plugin;
                }
            }

            foreach (var searchPluginPair in searchPluginMap)
            {
                var enabled = !Settings.Default.DisabledSearchEngines.Contains(UserSettingsUtilities.GetSearchEngineIdentifier(searchPluginPair.Value, searchPluginPair.Key));

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
                item.Checked = !item.Checked;
                item.ForeColor = item.Checked ? _enabledColor : _disabledColor;
                item.SubItems[colSearchEngineEnabled.Index].Text = item.Checked ? "Yes" : "No";

                SearchEnginesModified = true;
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

        private void btnClearRecentItems_Click(object sender, EventArgs e)
        {
            _recentFilesManager.Clear();
            RecentItemsCleared = true;
        }
    }
}