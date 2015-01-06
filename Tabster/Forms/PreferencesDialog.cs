#region

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Tabster.Properties;
using Tabster.Utilities.Net;

#endregion
namespace Tabster.Forms
{
    

    internal partial class PreferencesDialog : Form
    {
        public PreferencesDialog(string tab = null)
        {
            InitializeComponent();

            LoadPreferences();

            LoadPlugins();

            if (!string.IsNullOrEmpty(tab))
            {
                var tp = tabControl1.TabPages.Cast<TabPage>()
                    .FirstOrDefault(t => t.Text.Equals(tab, StringComparison.OrdinalIgnoreCase));
                if (tp != null)
                    tabControl1.SelectedTab = tp;
            }
        }

        public bool PluginsModified { get; private set; }

        private void LoadPreferences()
        {
            chkupdatestartup.Checked = Settings.Default.StartupUpdate;

            //printing
            printColorPreview.BackColor = Settings.Default.PrintColor;
            chkPrintPageNumbers.Checked = Settings.Default.PrintPageNumbers;
            chkPrintTimestamp.Checked = Settings.Default.PrintTimestamp;

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
                txtProxyAddress.Text = Settings.Default.ProxyHost;
                numProxyPort.Value = Settings.Default.ProxyPort;
                txtProxyUsername.Text = Settings.Default.ProxyUsername;
                txtProxyPassword.Text = Settings.Default.ProxyPassword;
            }
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

                    lvi.SubItems.Add(plugin.PluginAttributes.DisplayName);
                    lvi.SubItems.Add(plugin.PluginAttributes.Version.ToString());
                    lvi.SubItems.Add(Path.GetFileName(plugin.Assembly.Location));
                    lvi.SubItems.Add(plugin.PluginAttributes.Description);

                    listPlugins.Items.Add(lvi);
                }
            }

            listPlugins.AutoResizeColumn(listPlugins.Columns.Count - 1, ColumnHeaderAutoResizeStyle.ColumnContent);
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

                    Program.pluginController.SetStatus(guid, pluginEnabled);

                    Settings.Default.DisabledPlugins.Remove(guid.ToString());

                    if (!pluginEnabled)
                        Settings.Default.DisabledPlugins.Add(guid.ToString());
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
                Settings.Default.ProxyPort = (ushort)customProxy.Address.Port;

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

                Program.CustomProxyController.ManualProxyParameters = new ManualProxyParameters(customProxy.Address.Host, (ushort)customProxy.Address.Port,
                    customProxy.Credentials);
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

        private void okbtn_Click(object sender, EventArgs e)
        {
            if (!ValidateProxyInput())
            {
                MessageBox.Show("Invalid proxy settings.", "Proxy Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }

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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pluginsDirectorybtn.Visible = tabControl1.SelectedTab == tabPlugins;
        }

        private void pluginsDirectorybtn_Click(object sender, EventArgs e)
        {
            Process.Start(Program.pluginController.WorkingDirectory);
        }

        private void printColorPreview_Click(object sender, EventArgs e)
        {
            if (printColorDialog.ShowDialog() == DialogResult.OK)
            {
                printColorPreview.BackColor = printColorDialog.Color;
            }
        }
    }
}