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
        private readonly List<PluginHost> _pluginHosts = new List<PluginHost>();

        public PluginController(string[] pluginDirectories)
        {
            WorkingDirectories = pluginDirectories;
        }

        public string[] WorkingDirectories { get; private set; }

        public void LoadPlugins()
        {
            _pluginHosts.Clear();

            foreach (var dir in WorkingDirectories)
            {
                LoadPluginDirectory(dir);
            }
        }

        private void LoadPluginDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                try
                {
                    Directory.CreateDirectory(directory);
                }

                catch
                {
                    //unhandled
                }
            }

            if (Directory.Exists(directory))
            {
                var pluginFiles = Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories);

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
                            Logging.GetLogger().Error(string.Format("Error occured while initializing plugin: {0}", pluginHost.FileInfo.FullName), ex);
                        }
                    }
                }
            }
        }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            foreach (var plugin in _pluginHosts.Where(plugin => plugin.Enabled))
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
                var fileInfo = new FileInfo(path);
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

                        var host = new PluginHost(assembly, plugin, fileInfo);
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