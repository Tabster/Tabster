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
using Tabster.Update;
using Tabster.Utilities;

#endregion

namespace Tabster
{
    internal static class Program
    {
        public static SingleInstanceController InstanceController;
        public static PluginController PluginController;
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
            TabsterEnvironmentUtilities.CreateDirectories();

            // prepare logging
            var logDirectory = TabsterEnvironmentUtilities.CreateEnvironmentDirectoryPath(TabsterEnvironmentDirectory.ApplicatonData, "Logs");
            Logging.SetLogDirectory(logDirectory);

            // log all the errors
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = (Exception) e.ExceptionObject;
                Logging.GetLogger().Error(ex);
            };

            var tablatureDirectory = TabsterEnvironmentUtilities.CreateEnvironmentDirectoryPath(TabsterEnvironmentDirectory.UserData, "Library");
            var playlistsDirectory = Path.Combine(TabsterEnvironmentUtilities.GetEnvironmentDirectoryPath(TabsterEnvironmentDirectory.UserData), "Playlists"); // no longer used, just for legacy support

            var databasePath = Path.Combine(TabsterEnvironmentUtilities.GetEnvironmentDirectoryPath(TabsterEnvironmentDirectory.ApplicatonData), "library.db");

            var filesNeedScanned = !File.Exists(databasePath);

            var fileProcessor = new TabsterFileProcessor<TablatureFile>(Constants.TablatureFileVersion);

            Logging.GetLogger().Info(string.Format("Initializing database: {0}", databasePath));
            _databaseHelper = new TabsterDatabaseHelper(databasePath);

            var libraryManager = new LibraryManager(_databaseHelper, fileProcessor, tablatureDirectory);
            var playlistManager = new PlaylistManager(_databaseHelper, fileProcessor);

            // database file deleted or possible pre-2.0 version, convert existing files
            if (filesNeedScanned)
            {
                Logging.GetLogger().Info("Converting over XML files...");
                ConvertXmlFiles(playlistManager, tablatureDirectory, playlistsDirectory);
            }

            var pluginDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Plugins");
            var pluginDataDirectory = TabsterEnvironmentUtilities.CreateEnvironmentDirectoryPath(TabsterEnvironmentDirectory.CommonApplicationData, "Plugins");
            PluginController = new PluginController(new[] {pluginDirectory, pluginDataDirectory});

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InstanceController = new SingleInstanceController(libraryManager, playlistManager, filesNeedScanned);
            InstanceController.Run(args);
        }

        /// <summary>
        ///     Convert Xml-based files to binary.
        /// </summary>
        private static void ConvertXmlFiles(PlaylistManager playlistManager, string tablatureDirectory, string playlistsDirectory)
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
    }
}