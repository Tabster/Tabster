#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Tabster.Core.Plugins;

#endregion

namespace Tabster.Utilities
{
    public class PluginController : IEnumerable<PluginHost>
    {
        private readonly List<Guid> _disabledPlugins = new List<Guid>();
        private readonly List<PluginHost> _plugins = new List<PluginHost>();

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

            foreach (var plugin in this.Where(plugin => IsEnabled(plugin.GUID)))
            {
                try
                {
                    plugin.Plugin.Activate();
                }

                catch (Exception ex)
                {
                    Logging.GetLogger().Error(string.Format("Error occured while activating plugin: {0}", Path.GetFileName(plugin.Assembly.Location)), ex);
                }
            }
        }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            foreach (var plugin in _plugins.Where(plugin => IsEnabled(plugin.GUID)))
            {
                instances.AddRange(plugin.GetClassInstances<T>());
            }

            return instances;
        }

        public PluginHost GetHostByType(Type type)
        {
            return _plugins.Find(x => x.Contains(type));
        }

        public PluginHost LoadPluginFromDisk(string path)
        {
            try
            {
                var assembly = Assembly.LoadFrom(path);

                if (assembly != null)
                {
                    Guid assemblyGuid;

                    //require assembly guid
                    if (!AssemblyHasGuid(assembly, out assemblyGuid))
                        return null;

                    //prevent guid collisions
                    if (_plugins.Find(x => x.GUID == assemblyGuid) != null)
                        return null;

                    var pluginType = assembly.GetTypes().FirstOrDefault(objType => typeof (ITabsterPlugin).IsAssignableFrom(objType));

                    if (pluginType != null)
                    {
                        var instance = Activator.CreateInstance(pluginType);
                        var plugin = (ITabsterPlugin) instance;

                        var host = new PluginHost(assembly, plugin, assemblyGuid);
                        _plugins.Add(host);

                        return host;
                    }
                }
            }

            catch
            {
                //unhandled
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
            return _plugins.Find(x => x.GUID == guid);
        }

        private static bool AssemblyHasGuid(Assembly assembly, out Guid guid)
        {
            var attributes = assembly.GetCustomAttributes(typeof (GuidAttribute), false);

            if (attributes.Length > 0)
            {
                guid = new Guid(((GuidAttribute) attributes[0]).Value);
                return true;
            }

            guid = Guid.Empty;
            return false;
        }

        #region Implementation of IEnumerable

        public IEnumerator<PluginHost> GetEnumerator()
        {
            return ((IEnumerable<PluginHost>) _plugins).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}