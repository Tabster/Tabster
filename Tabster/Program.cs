#region

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using log4net.Config;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Processing;
using Tabster.Forms;
using Tabster.LocalUtilities;
using Tabster.Properties;
using Tabster.Updater;
using Tabster.Utilities.Plugins;

#endregion

namespace Tabster
{
    internal static class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static SingleInstanceController InstanceController;
        public static PluginController PluginController;
        public static string ApplicationDataDirectory;
        public static string UserDataDirectory;
        public static UpdateQuery UpdateQuery = new UpdateQuery();

        private static ExternalViewerForm _tabbedViewer;

        public static ExternalViewerForm TabbedViewer
        {
            get
            {
                if (_tabbedViewer == null || _tabbedViewer.IsDisposed)
                {
                    var mainForm = InstanceController.MainForm;
                    _tabbedViewer = new ExternalViewerForm(mainForm);
                }

                return _tabbedViewer;
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            InitializeWorkingDirectories();

            //prepare logging
            var logDirectory = Path.Combine(ApplicationDataDirectory, "Logs");
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            log4net.GlobalContext.Properties["HeaderInfo"] = string.Format("Tabster {0}", Application.ProductVersion);
            log4net.GlobalContext.Properties["LogDirectory"] = logDirectory;

            XmlConfigurator.Configure();

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = (Exception) e.ExceptionObject;
                log.Error(ex);
            };

            log.Info("Initializing library...");
            var library = InitializeLibrary();

            log.Info("Loading plugins...");
            LoadPlugins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InstanceController = new SingleInstanceController(library);
            InstanceController.Run(args);
        }

        private static SqliteTabsterLibrary<TablatureFile, TablaturePlaylistFile> InitializeLibrary()
        {
            var tablatureDirectory = Path.Combine(UserDataDirectory, "Library");
            var playlistsDirectory = Path.Combine(UserDataDirectory, "Playlists");

            var libraryDatabase = Path.Combine(ApplicationDataDirectory, "library.db");

            if (!File.Exists(libraryDatabase))
                ConvertXmlFiles(tablatureDirectory, playlistsDirectory);

            var library = new SqliteTabsterLibrary<TablatureFile, TablaturePlaylistFile>(
                libraryDatabase,
                tablatureDirectory,
                playlistsDirectory,
                new TabsterFileProcessor<TablatureFile>(Constants.TablatureFileVersion),
                new TabsterFileProcessor<TablaturePlaylistFile>(Constants.TablaturePlaylistFileVersion));

            return library;
        }

        private static void InitializeWorkingDirectories()
        {
#if PORTABLE
            ApplicationDataDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AppData");
            UserDataDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "UserData");
#else
            ApplicationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tabster");
            UserDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tabster");
#endif

            if (!Directory.Exists(ApplicationDataDirectory))
                Directory.CreateDirectory(ApplicationDataDirectory);

            if (!Directory.Exists(UserDataDirectory))
                Directory.CreateDirectory(UserDataDirectory);
        }

        /// <summary>
        ///     Convert Xml-based files to binary.
        /// </summary>
        private static void ConvertXmlFiles(string tablatureDirectory, string playlistsDirectory)
        {
            if (Directory.Exists(tablatureDirectory))
            {
                foreach (var file in Directory.GetFiles(tablatureDirectory, string.Format("*{0}", Constants.TablatureFileExtension), SearchOption.AllDirectories))
                {
                    var tablatureFile = TabsterXmlFileConverter.ConvertTablatureDocument(file);

                    if (tablatureFile != null)
                        tablatureFile.Save(file);
                }
            }

            if (Directory.Exists(playlistsDirectory))
            {
                foreach (var file in Directory.GetFiles(playlistsDirectory, string.Format("*{0}", Constants.TablaturePlaylistFileExtension), SearchOption.AllDirectories))
                {
                    var playlistFile = TabsterXmlFileConverter.ConvertTablaturePlaylist(file);

                    if (playlistFile != null)
                        playlistFile.Save(file);
                }
            }
        }

        private static void LoadPlugins()
        {
            var pluginDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Plugins");

            PluginController = new PluginController(pluginDirectory);

            foreach (var guid in Settings.Default.DisabledPlugins)
            {
                PluginController.SetStatus(new Guid(guid), false);
            }
        }
    }
}