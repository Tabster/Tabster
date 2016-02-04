#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tabster.Core.Plugins;

#endregion

namespace Tabster.Utilities
{
    public class PluginHost
    {
        public PluginHost(Assembly assembly, ITabsterPlugin plugin)
        {
            Assembly = assembly;
            Plugin = plugin;
        }

        public Assembly Assembly { get; private set; }
        public ITabsterPlugin Plugin { get; private set; }

        private List<Type> _types = new List<Type>(); 

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            var cType = typeof (T);

            _types = Assembly.GetTypes().Where(x => !x.IsAbstract && !x.IsInterface).ToList();

            foreach (var type in _types)
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
            return _types.Contains(type);
        }
    }
}