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

namespace Tabster.Utilities.Plugins
{
    public class PluginController : IEnumerable<TabsterPluginHost>
    {
        private readonly List<Guid> _disabledPlugins = new List<Guid>();
        private readonly List<TabsterPluginHost> _plugins = new List<TabsterPluginHost>();

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
                    LoadPluginFromDisk(pluginPath);
                }
            }

            foreach (var plugin in this.Where(plugin => IsEnabled(plugin.GUID)))
            {
                plugin.Plugin.Activate();
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

        public TabsterPluginHost GetHostByType(Type type)
        {
            return _plugins.Find(x => x.Contains(type));
        }

        public void LoadPluginFromDisk(string path)
        {
            try
            {
                var assembly = Assembly.LoadFrom(path);

                if (assembly != null)
                {
                    Guid assemblyGuid;

                    //require assembly guid
                    if (!AssemblyHasGuid(assembly, out assemblyGuid))
                        return;

                    //prevent guid collisions
                    if (_plugins.Find(x => x.GUID == assemblyGuid) != null)
                        return;

                    var pluginType = assembly.GetTypes().FirstOrDefault(objType =>
                        typeof (ITabsterPluginAttributes).IsAssignableFrom(objType) &&
                        typeof (TabsterPluginBase).IsAssignableFrom(objType));

                    if (pluginType != null)
                    {
                        var instance = Activator.CreateInstance(pluginType);
                        var attributes = (ITabsterPluginAttributes) instance;
                        var plugin = (TabsterPluginBase) instance;

                        var host = new TabsterPluginHost(assembly, attributes, plugin, assemblyGuid);
                        _plugins.Add(host);
                    }
                }
            }

            catch
            {
                //unhandled
            }
        }

        public void SetStatus(Guid guid, bool enabled)
        {
            _disabledPlugins.Remove(guid);

            if (!enabled)
                _disabledPlugins.Add(guid);
        }

        public bool IsEnabled(Guid guid)
        {
            return !_disabledPlugins.Contains(guid);
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

        public IEnumerator<TabsterPluginHost> GetEnumerator()
        {
            foreach (var plugin in _plugins)
            {
                yield return plugin;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}