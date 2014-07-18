using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Tabster.Core.Plugins;

namespace Tabster.Plugins
{
    public class TabsterPlugin
    {
        public Assembly Assembly { get; private set; }
        public ITabsterPlugin Interface { get; private set; }

        private Guid _guid;

        public Guid GUID
        {
            get
            {
                if (_guid == Guid.Empty)
                {
                    var attributes = Assembly.GetCustomAttributes(typeof(GuidAttribute), false);

                    if (attributes.Length > 0)
                    {
                        _guid = new Guid(((GuidAttribute)attributes[0]).Value);

                    }
                }

                return _guid;
            }
        }

        public TabsterPlugin(Assembly assembly, ITabsterPlugin pluginInterface)
        {
            Interface = pluginInterface;
            Assembly = assembly;
        }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            var cType = typeof(T);

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

}