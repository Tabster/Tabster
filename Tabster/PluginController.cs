#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Tabster.Core.Plugins;

#endregion

namespace Tabster
{
    public class TabsterPlugin
    {
        public Assembly Assembly { get; private set; }
        public ITabsterPlugin Interface { get; private set; }

        public TabsterPlugin(Assembly assembly, ITabsterPlugin pluginInterface)
        {
            Interface = pluginInterface;
            Assembly = assembly;
        }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            var cType = typeof (T);

            foreach (var type in Interface.Types)
            {
                if (cType.IsAssignableFrom(type))
                {
                    var instance = (T)Activator.CreateInstance(type);
                    instances.Add(instance);
                }
            }

            return instances;
        }
    }

    public class PluginController : IEnumerable<TabsterPlugin>
    {
        private readonly string _pluginsDirectory;
        private readonly List<TabsterPlugin> _plugins = new List<TabsterPlugin>();

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
                var pluginFiles = Directory.GetFiles(_pluginsDirectory, "*.dll");

                foreach (var pluginPath in pluginFiles)
                {
                    LoadPluginFromDisk(pluginPath);
                }
            }
        }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            //todo cache instances?
            foreach(var plugin in _plugins)
            {
                instances.AddRange(plugin.GetClassInstances<T>());
            }

            return instances;
        }

        private void LoadPluginFromDisk(string path)
        {
            try
            {
                var assembly = Assembly.LoadFrom(path);

                if (assembly != null)
                {
                    Type pluginType = null;

                    foreach (var objType in assembly.GetTypes())
                    {
                        if (typeof(ITabsterPlugin).IsAssignableFrom(objType))
                        {
                            pluginType = objType;
                            break;
                        }
                    }

                    if (pluginType != null)
                    {
                        var plguinInterface = (ITabsterPlugin) Activator.CreateInstance(pluginType);

                        var plugin = new TabsterPlugin(assembly, plguinInterface);
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
    }
}