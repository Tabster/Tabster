#region

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tabster.Plugins;
using Tabster.Utilities;

#endregion

namespace Tabster
{
    internal static class Program
    {
        private static PluginManager _pluginManager;

        public static PluginManager GetPluginManager()
        {
            return _pluginManager;
        }

        [STAThread]
        public static void Main(string[] args)
        {
            TabsterEnvironment.CreateDirectories();

            // prepare logging
            var logDirectory = TabsterEnvironment.CreateEnvironmentDirectoryPath(TabsterEnvironmentDirectory.ApplicatonData, "Logs");
            Logging.SetLogDirectory(logDirectory);

            // log all the errors
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = (Exception) e.ExceptionObject;
                Logging.GetLogger().Error(ex);
            };

            _pluginManager = new PluginManager(TabsterEnvironment.CreateEnvironmentDirectoryPath(TabsterEnvironmentDirectory.ApplicatonData, "Plugins"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var assemblyGuid = ((GuidAttribute) Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (GuidAttribute), true)[0]).Value;
            var filename = Path.Combine(TabsterEnvironment.GetEnvironmentDirectoryPath(TabsterEnvironmentDirectory.ApplicatonData), string.Format("{0}.tmp", assemblyGuid));

            using (var instanceController = new TabsterSingleInstanceController(filename))
            {
                instanceController.Start(new ReadOnlyCollection<string>(args));
            }
        }
    }
}