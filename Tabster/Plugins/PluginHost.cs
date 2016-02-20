#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tabster.Core.Plugins;
using Tabster.Utilities;

#endregion

namespace Tabster.Plugins
{
    public class PluginHost
    {
        private bool _enabled;
        private List<Type> _types = new List<Type>();

        public PluginHost(Assembly assembly, ITabsterPlugin plugin, FileInfo fileInfo)
        {
            Assembly = assembly;
            Plugin = plugin;
            FileInfo = fileInfo;
        }

        public Assembly Assembly { get; private set; }
        public ITabsterPlugin Plugin { get; private set; }
        public FileInfo FileInfo { get; private set; }

        public Boolean Enabled
        {
            get { return _enabled; }
            set
            {
                if (value)
                {
                    try
                    {
                        Plugin.Activate();
                    }

                    catch (Exception ex)
                    {
                        Logging.GetLogger().Error(string.Format("Error occured while activating plugin: {0}", FileInfo.FullName), ex);
                    }
                }

                else
                {
                    try
                    {
                        Plugin.Deactivate();
                    }

                    catch (Exception ex)
                    {
                        Logging.GetLogger().Error(string.Format("Error occured while deactivating plugin: {0}", FileInfo.FullName), ex);
                    }
                }

                _enabled = value;
            }
        }

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
                Logging.GetLogger().Error(string.Format("Error occured while loading plugin types: {0}", FileInfo.FullName), ex);
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
                        Logging.GetLogger().Error(string.Format("Error occured while creating plugin type instance: '{0}' in {1}", type.FullName, FileInfo.FullName), ex);
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