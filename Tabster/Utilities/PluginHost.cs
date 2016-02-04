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
    public class PluginHost
    {
        private List<Type> _types = new List<Type>();

        public PluginHost(Assembly assembly, ITabsterPlugin plugin)
        {
            Assembly = assembly;
            Plugin = plugin;
        }

        public Assembly Assembly { get; private set; }
        public ITabsterPlugin Plugin { get; private set; }

        public IEnumerable<T> GetClassInstances<T>()
        {
            var instances = new List<T>();

            var cType = typeof (T);

            _types.Clear();

            try
            {
                _types = Assembly.GetTypes().Where(x => !x.IsAbstract && !x.IsInterface).ToList();
            }

            catch (Exception ex)
            {
                Logging.GetLogger().Error(string.Format("Error occured while loading plugin types: {0}", Path.GetFileName(Assembly.Location)), ex);
            }

            foreach (var type in _types)
            {
                if (cType.IsAssignableFrom(type))
                {
                    try
                    {
                        var instance = (T) Activator.CreateInstance(type);
                        instances.Add(instance);
                    }

                    catch (Exception ex)
                    {
                        Logging.GetLogger().Error(string.Format("Error occured while creating plugin type instance: '{0}' in {1}", type.FullName, Path.GetFileName(Assembly.Location)), ex);
                    }
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