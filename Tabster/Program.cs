#region

using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Library;
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
        public static TablatureFileLibrary<TablatureFile, TablaturePlaylistFile> TablatureFileLibrary;
        public static SingleInstanceController InstanceController;
        public static PluginController PluginController;
        public static string ApplicationDataDirectory;
        public static string UserDirectory;
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

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            LoadPlugins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InstanceController = new SingleInstanceController();
            InstanceController.Run(args);
        }

        private static void InitializeWorkingDirectories()
        {
#if PORTABLE
            ApplicationDataDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AppData");
            UserDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "UserData");
#else
            ApplicationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tabster");
            UserDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tabster");
#endif

            if (!Directory.Exists(ApplicationDataDirectory))
                Directory.CreateDirectory(ApplicationDataDirectory);

            if (!Directory.Exists(UserDirectory))
                Directory.CreateDirectory(UserDirectory);

            TablatureFileLibrary = new TablatureFileLibrary<TablatureFile, TablaturePlaylistFile>(
                Path.Combine(UserDirectory, "Library"),
                Path.Combine(UserDirectory, "Playlists"),
                new TabsterFileProcessor<TablatureFile>(Constants.TablatureFileVersion),
                new TabsterFileProcessor<TablaturePlaylistFile>(Constants.TablaturePlaylistFileVersion));
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

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            //todo save on exit
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