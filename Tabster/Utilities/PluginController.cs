#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tabster.Core.Plugins;

#endregion

namespace Tabster.Utilities
{
    public class PluginController
    {
        private readonly List<Guid> _disabledPlugins = new List<Guid>();
        private readonly List<PluginHost> _pluginHosts = new List<PluginHost>();

        public PluginController(string pluginsDirectory)
        {
            WorkingDirectory = pluginsDirectory;
        }

        public string WorkingDirectory { get; private set; }

        public void LoadPlugins()
        {
            if (!Directory.Exists(WorkingDirectory))
            {
                try
                {
                    Directory.CreateDirectory(WorkingDirectory);
                }

                catch
                {
                    //unhandled
                }
            }

            if (Directory.Exists(WorkingDirectory))
            {
                var pluginFiles = Directory.GetFiles(WorkingDirectory, "*.dll", SearchOption.AllDirectories);

                foreach (var pluginPath in pluginFiles)
                {
                    var pluginHost = LoadPluginFromDisk(pluginPath);

                    if (pluginHost != null)
                    {
                        try
                        {
                            pluginHost.Plugin.Initialize();
                        }

                        catch (Exception ex)
                        {
                            Logging.GetLogger().Error(string.Format("Error occured while initializing plugin: {0}", Path.GetFileName(pluginHost.Assembly.Location)), ex);
                        }
                    }
                }
            }

            foreach (var pluginHost in _pluginHosts.Where(pluginHost => IsEnabled(pluginHost.Plugin.Guid)))
            {
                try
                {
                    pluginHost.Plugin.Activate();
                }

                catch (Exception ex)
                {
                    Logging.GetLogger().Error(string.Format("Error occured while activating plugin: {0}", Path.GetFileName(pluginHost.Assembly.Location)), ex);
                }
            }
        }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            foreach (var plugin in _pluginHosts.Where(plugin => IsEnabled(plugin.Plugin.Guid)))
            {
                instances.AddRange(plugin.GetClassInstances<T>());
            }

            return instances;
        }

        public PluginHost GetHostByType(Type type)
        {
            return _pluginHosts.Find(x => x.Contains(type));
        }

        public PluginHost LoadPluginFromDisk(string path)
        {
            try
            {
                var assembly = Assembly.LoadFrom(path);

                if (assembly != null)
                {
                    var pluginType = assembly.GetTypes().FirstOrDefault(objType => typeof (ITabsterPlugin).IsAssignableFrom(objType));

                    if (pluginType != null)
                    {
                        var instance = Activator.CreateInstance(pluginType);
                        var plugin = (ITabsterPlugin) instance;

                        //require plugin guid
                        if (plugin.Guid == Guid.Empty)
                            return null;

                        //prevent guid collisions
                        if (_pluginHosts.Find(x => x.Plugin.Guid == plugin.Guid) != null)
                            return null;

                        var host = new PluginHost(assembly, plugin);
                        _pluginHosts.Add(host);

                        return host;
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetLogger().Error(string.Format("Error occured while loading plugin assembly: {0}", path), ex);
            }

            return null;
        }

        public void SetStatus(Guid guid, bool enabled)
        {
            var plugin = FindPluginByGuid(guid);

            if (enabled)
            {
                _disabledPlugins.Remove(guid);

                try
                {
                    plugin.Plugin.Activate();
                }

                catch (Exception ex)
                {
                    Logging.GetLogger().Error(string.Format("Error occured while activating plugin: {0}", Path.GetFileName(plugin.Assembly.Location)), ex);
                }
            }

            else
            {
                _disabledPlugins.Add(guid);

                try
                {
                    plugin.Plugin.Deactivate();
                }

                catch (Exception ex)
                {
                    Logging.GetLogger().Error(string.Format("Error occured while deactivating plugin: {0}", Path.GetFileName(plugin.Assembly.Location)), ex);
                }
            }
        }

        public bool IsEnabled(Guid guid)
        {
            return !_disabledPlugins.Contains(guid);
        }

        public PluginHost FindPluginByGuid(Guid guid)
        {
            return _pluginHosts.Find(x => x.Plugin.Guid == guid);
        }

        public PluginHost[] GetPluginHosts()
        {
            return _pluginHosts.ToArray();
        }
    }
}