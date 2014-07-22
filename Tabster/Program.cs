#region

using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Tabster.Plugins;

#endregion

namespace Tabster
{
    internal static class Program
    {
        public static TabViewerManager TabHandler;
        public static readonly LibraryManager libraryManager = new LibraryManager();
        public static SingleInstanceController instanceController;
        public static PluginController pluginController;

        [STAThread]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var pluginDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Plugins");

            pluginController = new PluginController(pluginDirectory);
            pluginController.LoadPlugins();

            libraryManager.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            instanceController = new SingleInstanceController();
            TabHandler = new TabViewerManager();

            instanceController.Run(args);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = ((Exception)e.ExceptionObject).GetBaseException();

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

            File.AppendAllText(Path.Combine(libraryManager.ApplicationDirectory, "error.log"), sb.ToString());
        }
    }
}