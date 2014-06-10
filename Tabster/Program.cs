#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster
{
    internal static class Program
    {
        public static TabViewerManager TabHandler;
        public static readonly LibraryManager libraryManager = new LibraryManager();
        public static SingleInstanceController instanceController;

        [STAThread]
        public static void Main(string[] args)
        {
            libraryManager.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            instanceController = new SingleInstanceController();
            TabHandler = new TabViewerManager();

            instanceController.Run(args);
        }
    }
}