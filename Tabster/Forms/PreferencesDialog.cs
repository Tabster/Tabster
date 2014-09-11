#region

using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Tabster.Properties;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    public partial class PreferencesDialog : Form
    {
        public PreferencesDialog()
        {
            InitializeComponent();

            LoadPreferences();
        }

        public bool PluginsModified { get; private set; }

        private void LoadPreferences()
        {
            chkupdatestartup.Checked = Settings.Default.StartupUpdate;

            //proxy settings

            var manualProxy = Program.CustomProxyController.GetProxy();

            if (Program.CustomProxyController.Configuration == ProxyConfiguration.System)
                radioSystemProxy.Checked = true;
            else if (Program.CustomProxyController.Configuration == ProxyConfiguration.Manual && manualProxy != null)
                radioManualProxy.Checked = true;
            else
                radioNoProxy.Checked = true;

            if (manualProxy != null)
            {
                Uri proxyAddress;

                if (Uri.TryCreate(Settings.Default.ProxyAddress, UriKind.Absolute, out proxyAddress))
                {
                    var address = new Uri(Settings.Default.ProxyAddress);

                    txtProxyAddress.Text = address.Host;
                    numProxyPort.Value = address.Port;
                    txtProxyUsername.Text = Settings.Default.ProxyUsername;
                    txtProxyPassword.Text = Settings.Default.ProxyPassword;
                }
            }

            //plugins
            LoadPlugins();
        }

        private void LoadPlugins()
        {
            foreach (var plugin in Program.pluginController)
            {
                if (plugin.GUID != Guid.Empty)
                {
                    var lvi = new ListViewItem
                                  {
                                      Tag = plugin.GUID.ToString(),
                                      Checked = Program.pluginController.IsEnabled(plugin.GUID)
                                  };

                    lvi.SubItems.Add(plugin.Interface.DisplayName);
                    lvi.SubItems.Add(plugin.Interface.Version.ToString());
                    lvi.SubItems.Add(Path.GetFileName(plugin.Assembly.Location));
                    lvi.SubItems.Add(plugin.Interface.Description);

                    listPlugins.Items.Add(lvi);
                }
            }

            listPlugins.AutoResizeColumn(listPlugins.Columns.Count - 1, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void SavePreferences()
        {
            if (!ValidateProxyInput())
            {
                MessageBox.Show("Invalid proxy settings.", "Proxy Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //plugins
            if (PluginsModified)
            {
                foreach (ListViewItem lvi in listPlugins.Items)
                {
                    var guid = new Guid(lvi.Tag.ToString());
                    var pluginEnabled = lvi.Checked;

                    Program.pluginController.SetStatus(guid, pluginEnabled);

                    if (pluginEnabled)
                        Settings.Default.DisabledPlugins.Remove(guid.ToString());
                    else
                        Settings.Default.DisabledPlugins.Add(guid.ToString());
                }
            }

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
                Settings.Default.ProxyAddress = customProxy.Address.ToString();

                if (customProxy.Credentials != null)
                {
                    var credentials = (NetworkCredential) customProxy.Credentials;

                    Settings.Default.ProxyUsername = credentials.UserName != null ? ((NetworkCredential) customProxy.Credentials).UserName : null;
                    Settings.Default.ProxyPassword = credentials.Password != null ? ((NetworkCredential) customProxy.Credentials).Password : null;
                }

                Program.CustomProxyController.ManualProxyParameters = new ManualProxyParameters(customProxy.Address, customProxy.Credentials);
            }

            //apply settings to active proxy config
            Program.CustomProxyController.Configuration = proxyConfig;

            Settings.Default.StartupUpdate = chkupdatestartup.Checked;

            Settings.Default.Save();
        }

        private bool ValidateProxyInput()
        {
            return !radioManualProxy.Checked || GetProxyFromInput() != null;
        }

        /// <summary>
        ///   Attemps to create a proxy object based off user iput.
        /// </summary>
        private WebProxy GetProxyFromInput()
        {
            if (string.IsNullOrEmpty(txtProxyAddress.Text))
                return null;

            Uri proxyAddress;

            var proxyAddressInput = txtProxyAddress.Text;

            //add http scheme if it's an ip
            IPAddress proxyIP;
            if (IPAddress.TryParse(proxyAddressInput, out proxyIP))
                proxyAddressInput = string.Format("http://{0}", proxyIP);

            proxyAddressInput += string.Format(":{0}", numProxyPort.Value);

            if (Uri.TryCreate(proxyAddressInput, UriKind.Absolute, out proxyAddress))
            {
                var proxy = new WebProxy(proxyAddress);

                if (!string.IsNullOrEmpty(txtProxyUsername.Text) && !string.IsNullOrEmpty(txtProxyPassword.Text))
                {
                    var creds = new NetworkCredential {UserName = txtProxyUsername.Text, Password = txtProxyPassword.Text};
                    proxy.Credentials = creds;
                }

                return proxy;
            }

            return null;
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            SavePreferences();
        }

        private void listPlugins_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            PluginsModified = true;
        }

        private void radioCustomProxy_CheckedChanged(object sender, EventArgs e)
        {
            customProxyPanel.Enabled = radioManualProxy.Checked;
        }

        private void chkProxyAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            txtProxyUsername.Enabled = txtProxyPassword.Enabled = chkProxyAuthentication.Checked;
        }

        private void chkShowProxyPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtProxyPassword.PasswordChar = chkShowProxyPassword.Checked ? '\0' : '*';
        }
    }
}