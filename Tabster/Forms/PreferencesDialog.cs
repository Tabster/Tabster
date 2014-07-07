#region

using System;
using System.IO;
using System.Windows.Forms;
using Tabster.Properties;

#endregion

namespace Tabster.Forms
{
    public partial class PreferencesDialog : Form
    {
        public PreferencesDialog()
        {
            InitializeComponent();
            chkupdatestartup.Checked = Settings.Default.StartupUpdate;

            LoadPlugins();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            //save plugins
            foreach (ListViewItem lvi in listView1.Items)
            {
                var guid = new Guid(lvi.Tag.ToString());
                var pluginEnabled = lvi.Checked;

                Program.pluginController.SetStatus(guid, pluginEnabled);

                if (pluginEnabled)
                    Settings.Default.DisabledPlugins.Remove(guid.ToString());
                else
                    Settings.Default.DisabledPlugins.Add(guid.ToString());
            }

            Settings.Default.StartupUpdate = chkupdatestartup.Checked;
            Settings.Default.Save();
        }

        private void LoadPlugins()
        {
            foreach (var plugin in Program.pluginController)
            {
                if (plugin.GUID != Guid.Empty)
                {
                    var lvi = new ListViewItem
                                  {
                                      ToolTipText = plugin.Interface.Description,
                                      Tag = plugin.GUID.ToString(),
                                      Checked = Program.pluginController.IsEnabled(plugin.GUID)
                                  };

                    lvi.SubItems.Add(plugin.Interface.DisplayName);
                    lvi.SubItems.Add(plugin.Interface.Version.ToString());
                    lvi.SubItems.Add(Path.GetFileName(plugin.Assembly.Location));

                    listView1.Items.Add(lvi);
                }
            }
        }
    }
}