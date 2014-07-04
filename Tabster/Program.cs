#region

using System;
using System.IO;
using System.Windows.Forms;

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
    }
}