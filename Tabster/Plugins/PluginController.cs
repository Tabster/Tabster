#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Tabster.Core.Plugins;

#endregion

namespace Tabster.Plugins
{
    public class PluginController : IEnumerable<TabsterPlugin>
    {
        private readonly List<Guid> _disabledPlugins = new List<Guid>();
        private readonly List<TabsterPlugin> _plugins = new List<TabsterPlugin>();
        private readonly string _pluginsDirectory;

        public PluginController(string pluginsDirectory)
        {
            _pluginsDirectory = pluginsDirectory;
        }

        public void LoadPlugins()
        {
            if (!Directory.Exists(_pluginsDirectory))
            {
                try
                {
                    Directory.CreateDirectory(_pluginsDirectory);
                }

                catch
                {
                    //unhandled
                }
            }

            if (Directory.Exists(_pluginsDirectory))
            {
                var pluginFiles = Directory.GetFiles(_pluginsDirectory, "*.dll", SearchOption.AllDirectories);

                foreach (var pluginPath in pluginFiles)
                {
                    LoadPluginFromDisk(pluginPath);
                }
            }
        }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            foreach (var plugin in _plugins)
            {
                if (IsEnabled(plugin.GUID))
                    instances.AddRange(plugin.GetClassInstances<T>());
            }

            return instances;
        }

        public void LoadPluginFromDisk(string path)
        {
            try
            {
                var assembly = Assembly.LoadFrom(path);


                if (assembly != null)
                {
                    Guid assemblyGuid;

                    if (!AssemblyHasGuid(assembly, out assemblyGuid))
                        return;

                    Type pluginType = null;

                    foreach (var objType in assembly.GetTypes())
                    {
                        if (typeof (ITabsterPlugin).IsAssignableFrom(objType))
                        {
                            pluginType = objType;
                            break;
                        }
                    }

                    if (pluginType != null)
                    {
                        var pluginInterface = (ITabsterPlugin) Activator.CreateInstance(pluginType);

                        var plugin = new TabsterPlugin(assembly, pluginInterface, assemblyGuid);
                        _plugins.Add(plugin);
                    }
                }
            }

            catch
            {
                //unhandled
            }
        }

        public TabsterPlugin[] GetPlugins()
        {
            return _plugins.ToArray();
        }

        public void SetStatus(Guid guid, bool enabled)
        {
            if (enabled)
                _disabledPlugins.Remove(guid);
            else
                _disabledPlugins.Add(guid);
        }

        public bool IsEnabled(Guid guid)
        {
            return !_disabledPlugins.Contains(guid);
        }

        #region Implementation of IEnumerable

        public IEnumerator<TabsterPlugin> GetEnumerator()
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

        private static bool AssemblyHasGuid(Assembly assembly, out Guid guid)
        {
            var attributes = assembly.GetCustomAttributes(typeof(GuidAttribute), false);

            if (attributes.Length > 0)
            {
                guid = new Guid(((GuidAttribute)attributes[0]).Value);
                return true;
            }

            guid = Guid.Empty;
            return false;
        }
    }
}