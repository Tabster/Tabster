#region

using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Processing;
using Tabster.Data.Xml;
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
        public static SingleInstanceController InstanceController;
        public static PluginController PluginController;
        public static string ApplicationDataDirectory;
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
            var library = InitializeWorkingDirectories();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            LoadPlugins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InstanceController = new SingleInstanceController(library);
            InstanceController.Run(args);
        }

        private static SqliteTabsterLibrary<TablatureFile, TablaturePlaylistFile> InitializeWorkingDirectories()
        {
            string userDirectory;

#if PORTABLE
            ApplicationDataDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AppData");
            userDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "UserData");
#else
            ApplicationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tabster");
            userDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tabster");
#endif

            if (!Directory.Exists(ApplicationDataDirectory))
                Directory.CreateDirectory(ApplicationDataDirectory);

            if (!Directory.Exists(userDirectory))
                Directory.CreateDirectory(userDirectory);

            var tablatureDirectory = Path.Combine(userDirectory, "Library");
            var playlistsDirectory = Path.Combine(userDirectory, "Playlists");

            var libraryDatabase = Path.Combine(ApplicationDataDirectory, "library.dat");

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

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = ((Exception) e.ExceptionObject).GetBaseException();

            var assembly = Assembly.GetExecutingAssembly();

            var sb = new StringBuilder();

            sb.AppendLine("---- State Data ----");
            sb.AppendLine(string.Format("Date/Time: {0}", DateTime.Now));
            sb.AppendLine(string.Format("Platform: {0}", Environment.OSVersion));
            sb.AppendLine(string.Format("CLR Version: {0}", Environment.Version));
            sb.AppendLine();

            sb.AppendLine("---- Assembly Data ----");
            sb.AppendLine(string.Format("Assembly Version: {0}", assembly.GetName().Version));
            sb.AppendLine(string.Format("Assembly Full Name: {0}", assembly.FullName));
            sb.AppendLine(string.Format("Assembly Path: {0}", assembly.Location));
            sb.AppendLine();

            sb.AppendLine("---- Exception Data ----");
            sb.AppendLine(string.Format("Exception Message: {0}", exception.Message));
            sb.AppendLine(string.Format("Exception Type: {0}", exception.GetType()));
            sb.AppendLine(string.Format("Exception Source: {0}", exception.Source));
            sb.AppendLine();

            sb.AppendLine("---- Stack Trace ----");
            sb.AppendLine(exception.StackTrace);

            sb.AppendLine();
            sb.AppendLine();

            File.AppendAllText(Path.Combine(ApplicationDataDirectory, "error.log"), sb.ToString());
        }
    }
}