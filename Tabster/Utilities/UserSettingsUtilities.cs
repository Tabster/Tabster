#region

using System;
using System.Collections.Generic;
using System.Net;
using Tabster.Core.Searching;
using Tabster.Plugins;
using Tabster.Properties;

#endregion

namespace Tabster.Utilities
{
    internal class UserSettingsUtilities
    {
        private static CustomProxyController _proxySettings;

        public static CustomProxyController ProxySettings
        {
            get
            {
                if (_proxySettings == null)
                {
                    var proxyConfig = (ProxyConfiguration) Enum.Parse(typeof (ProxyConfiguration), Settings.Default.ProxyConfig);

                    ManualProxyParameters manualProxyParams = null;

                    if (!string.IsNullOrEmpty((Settings.Default.ProxyHost)))
                    {
                        manualProxyParams = new ManualProxyParameters(Settings.Default.ProxyHost, Settings.Default.ProxyPort);

                        if (!string.IsNullOrEmpty(Settings.Default.ProxyUsername) && !string.IsNullOrEmpty(Settings.Default.ProxyPassword))
                            manualProxyParams.Credentials = new NetworkCredential(Settings.Default.ProxyUsername, Settings.Default.ProxyPassword);
                    }

                    _proxySettings = new CustomProxyController(proxyConfig, manualProxyParams);
                }

                return _proxySettings;
            }

            set { _proxySettings = value; }
        }

        public static ITablatureSearchEngine[] GetEnabledSearchEngines()
        {
            var engines = new List<ITablatureSearchEngine>();

            foreach (var plugin in Program.GetPluginController().GetPluginHosts())
            {
                foreach (var engine in plugin.GetClassInstances<ITablatureSearchEngine>())
                {
                    var id = GetSearchEngineIdentifier(plugin, engine);

                    if (id != null && !Settings.Default.DisabledSearchEngines.Contains(id))
                    {
                        engines.Add(engine);
                    }
                }
            }

            return engines.ToArray();
        }

        public static string GetSearchEngineIdentifier(PluginInstance pluginInstance, ITablatureSearchEngine engine)
        {
            if (string.IsNullOrEmpty(engine.Name))
                return null;

            return string.Format("{0}:{1}", pluginInstance.Plugin.Guid, engine.Name);
        }
    }
}