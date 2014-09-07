#region

using System;
using System.Collections.Generic;
using System.Reflection;
using Tabster.Core.Plugins;

#endregion

namespace Tabster.Plugins
{
    public class TabsterPlugin
    {
        public TabsterPlugin(Assembly assembly, ITabsterPlugin pluginInterface, Guid guid)
        {
            Interface = pluginInterface;
            Assembly = assembly;
            GUID = guid;
        }

        public Assembly Assembly { get; private set; }
        public ITabsterPlugin Interface { get; private set; }
        public Guid GUID { get; private set; }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            var cType = typeof (T);

            foreach (var type in Interface.Types)
            {
                if (cType.IsAssignableFrom(type))
                {
                    var instance = (T) Activator.CreateInstance(type);
                    instances.Add(instance);
                }
            }

            return instances;
        }
    }
}