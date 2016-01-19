#region

using System;
using System.Collections.Generic;
using System.Reflection;
using Tabster.Core.Plugins;

#endregion

namespace Tabster.Utilities
{
    public class PluginHost
    {
        public PluginHost(Assembly assembly, ITabsterPlugin plugin, Guid guid)
        {
            Assembly = assembly;
            Plugin = plugin;
            Guid = guid;
        }

        public Assembly Assembly { get; private set; }
        public ITabsterPlugin Plugin { get; private set; }
        public Guid Guid { get; private set; }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            var cType = typeof (T);

            foreach (var type in Plugin.Types)
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
            return Array.IndexOf(Plugin.Types, type) >= 0;
        }
    }
}