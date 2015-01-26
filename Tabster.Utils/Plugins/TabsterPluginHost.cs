#region

using System;
using System.Collections.Generic;
using System.Reflection;
using Tabster.Core.Plugins;

#endregion

namespace Tabster.Utilities.Plugins
{
    public class TabsterPluginHost
    {
        public TabsterPluginHost(Assembly assembly, ITabsterPluginAttributes attributes, TabsterPluginBase plugin,
            Guid guid)
        {
            Assembly = assembly;
            PluginAttributes = attributes;
            Plugin = plugin;
            GUID = guid;
        }

        public Assembly Assembly { get; private set; }
        public ITabsterPluginAttributes PluginAttributes { get; private set; }
        public TabsterPluginBase Plugin { get; private set; }
        public Guid GUID { get; private set; }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            var cType = typeof (T);

            foreach (var type in PluginAttributes.Types)
            {
                if (cType.IsAssignableFrom(type))
                {
                    var instance = (T) Activator.CreateInstance(type);
                    instances.Add(instance);
                }
            }

            return instances;
        }

        public bool Contains(Type type)
        {
            return Array.IndexOf(PluginAttributes.Types, type) >= 0;
        }
    }
}