#region

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Processing;
using Tabster.Database;
using Tabster.Plugins;
using Tabster.Utilities;

#endregion

namespace Tabster
{
    internal static class Program
    {
        private static TabsterDatabaseHelper _databaseHelper;
        private static PluginManager _pluginManager;

        public static PluginManager GetPluginController()
        {
            return _pluginManager;
        }

        public static TabsterDatabaseHelper GetDatabaseHelper()
        {
            return _databaseHelper;
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

            var tablatureDirectory = TabsterEnvironment.CreateEnvironmentDirectoryPath(TabsterEnvironmentDirectory.UserData, "Library");

            var databasePath = Path.Combine(TabsterEnvironment.GetEnvironmentDirectoryPath(TabsterEnvironmentDirectory.ApplicatonData), "library.db");

            var databaseMissing = !File.Exists(databasePath);

            var fileProcessor = new TabsterFileProcessor<TablatureFile>(Constants.TablatureFileVersion);

            Logging.GetLogger().Info(string.Format("Initializing database: {0}", databasePath));
            _databaseHelper = new TabsterDatabaseHelper(databasePath);

            var libraryManager = new LibraryManager(_databaseHelper, fileProcessor, tablatureDirectory);
            var playlistManager = new PlaylistManager(_databaseHelper, fileProcessor);

            var pluginDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Plugins");
            var pluginDataDirectory = TabsterEnvironment.CreateEnvironmentDirectoryPath(TabsterEnvironmentDirectory.CommonApplicationData, "Plugins");
            _pluginManager = new PluginManager(new[] {pluginDirectory, pluginDataDirectory});

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var assemblyGuid = ((GuidAttribute) Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (GuidAttribute), true)[0]).Value;
            var filename = Path.Combine(TabsterEnvironment.GetEnvironmentDirectoryPath(TabsterEnvironmentDirectory.ApplicatonData), string.Format("{0}.tmp", assemblyGuid));
            var instanceController = new TabsterSingleInstanceController(filename, libraryManager, playlistManager, databaseMissing);
            instanceController.Start(new ReadOnlyCollection<string>(args));
        }
    }
}