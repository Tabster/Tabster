#region

using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Tabster.Data.Library;
using Tabster.Forms;
using Tabster.LocalUtilities;
using Tabster.Properties;
using Tabster.Updater;
using Tabster.Utilities.Net;
using Tabster.Utilities.Plugins;

#endregion

namespace Tabster
{
    internal static class Program
    {
        public static TablatureFileLibrary TablatureFileLibrary;
        public static SingleInstanceController instanceController;
        public static PluginController pluginController;
        public static string ApplicationDataDirectory;
        public static string UserDirectory;
        public static UpdateQuery updateQuery = new UpdateQuery();
        public static CustomProxyController CustomProxyController;

        private static TabbedViewer _tabbedViewer;

        public static TabbedViewer TabbedViewer
        {
            get
            {
                if (_tabbedViewer == null || _tabbedViewer.IsDisposed)
                {
                    var mainForm = instanceController.MainForm;
                    _tabbedViewer = new TabbedViewer(mainForm);
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

            LoadProxySettings();

            LoadPlugins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            instanceController = new SingleInstanceController();
            instanceController.Run(args);
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

            TablatureFileLibrary = new TablatureFileLibrary(Path.Combine(ApplicationDataDirectory, "library.dat"),
                Path.Combine(UserDirectory, "Library"),
                Path.Combine(UserDirectory, "Playlists"));
        }

        private static void LoadProxySettings()
        {
            var proxyConfig = (ProxyConfiguration) Enum.Parse(typeof (ProxyConfiguration), Settings.Default.ProxyConfig);

            ManualProxyParameters manualProxyParams = null;

            if (!string.IsNullOrEmpty((Settings.Default.ProxyHost)))
            {
                manualProxyParams = new ManualProxyParameters(Settings.Default.ProxyHost, Settings.Default.ProxyPort);

                if (!string.IsNullOrEmpty(Settings.Default.ProxyUsername) && !string.IsNullOrEmpty(Settings.Default.ProxyPassword))
                    manualProxyParams.Credentials = new NetworkCredential(Settings.Default.ProxyUsername, Settings.Default.ProxyPassword);
            }

            CustomProxyController = new CustomProxyController(proxyConfig, manualProxyParams);
        }

        private static void LoadPlugins()
        {
            var pluginDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Plugins");

            pluginController = new PluginController(pluginDirectory);

            foreach (var guid in Settings.Default.DisabledPlugins)
            {
                pluginController.SetStatus(new Guid(guid), false);
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            TablatureFileLibrary.Save();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = ((Exception) e.ExceptionObject).GetBaseException();

            var assembly = Assembly.GetExecutingAssembly();

            var sb = new StringBuilder();

            sb.AppendLine("---- State Data ----");
            sb.AppendLine(string.Format("Date/Time: {0}", DateTime.Now.ToString()));
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