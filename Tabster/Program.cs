#region

using System;
using System.IO;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Processing;
using Tabster.Data.Xml;
using Tabster.Database;
using Tabster.Forms;
using Tabster.LocalUtilities;
using Tabster.Properties;
using Tabster.Update;
using PluginController = Tabster.LocalUtilities.PluginController;

#endregion

namespace Tabster
{
    internal static class Program
    {
        public static SingleInstanceController InstanceController;
        public static PluginController PluginController;
        public static string ApplicationDataDirectory;
        public static string UserDataDirectory;
        public static UpdateQuery UpdateQuery = new UpdateQuery();
        private static ExternalViewerForm _tabbedViewer;
        private static TabsterDatabaseHelper _databaseHelper;

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

        public static TabsterDatabaseHelper GetDatabaseHelper()
        {
            return _databaseHelper;
        }

        [STAThread]
        public static void Main(string[] args)
        {
            InitializeWorkingDirectories();

            //prepare logging
            var logDirectory = Path.Combine(ApplicationDataDirectory, "Logs");
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            Logging.SetLogDirectory(logDirectory);

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = (Exception) e.ExceptionObject;
                Logging.GetLogger().Error(ex);
            };

            var tablatureDirectory = Path.Combine(UserDataDirectory, "Library");
            var playlistsDirectory = Path.Combine(UserDataDirectory, "Playlists"); // no longer used, just for legacy support

            var databasePath = Path.Combine(ApplicationDataDirectory, "library.db");

            var filesNeedScanned = !File.Exists(databasePath);

            Logging.GetLogger().Info(string.Format("Initializing database: {0}", databasePath));
            _databaseHelper = new TabsterDatabaseHelper(databasePath);

            Logging.GetLogger().Info("Initializing library...");
            var libraryManager = new LibraryManager(_databaseHelper, new TabsterFileProcessor<TablatureFile>(Constants.TablatureFileVersion), tablatureDirectory);

            Logging.GetLogger().Info("Initializing playlists...");
            var playlistManager = new PlaylistManager(_databaseHelper);

            // database file deleted or possible pre-2.0 version, convert existing files
            if (filesNeedScanned)
            {
                Logging.GetLogger().Info("Converting over XML files...");
                ConvertXmlFiles(libraryManager, playlistManager, tablatureDirectory, playlistsDirectory);
            }

            Logging.GetLogger().Info("Loading plugins...");
            LoadPlugins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InstanceController = new SingleInstanceController(libraryManager, playlistManager, filesNeedScanned);
            InstanceController.Run(args);
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
        private static void ConvertXmlFiles(LibraryManager libraryManager, PlaylistManager playlistManager, string tablatureDirectory, string playlistsDirectory)
        {
            // playlists are no longer stored as files, but are now stored in database
            if (Directory.Exists(playlistsDirectory))
            {
#pragma warning disable 612
                var playlistProcessor = new TabsterFileProcessor<TablaturePlaylistDocument>(TablaturePlaylistDocument.FileVersion);
#pragma warning restore 612

                foreach (var file in Directory.GetFiles(playlistsDirectory, string.Format("*{0}", Constants.TablaturePlaylistFileExtension), SearchOption.AllDirectories))
                {
                    var playlistFile = playlistProcessor.Load(file);

                    if (playlistFile != null)
                    {
                        var playlist = new TablaturePlaylist(playlistFile.Name) {Created = playlistFile.FileAttributes.Created};

                        foreach (var item in playlistFile)
                        {
                            playlist.Add(item);
                        }

                        playlistManager.Update(playlist);
                    }
                }
            }

            if (Directory.Exists(tablatureDirectory))
            {
                foreach (var file in Directory.GetFiles(tablatureDirectory, string.Format("*{0}", Constants.TablatureFileExtension), SearchOption.AllDirectories))
                {
                    var tablatureFile = TabsterXmlFileConverter.ConvertTablatureDocument(file);

                    if (tablatureFile != null)
                        tablatureFile.Save(file);
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