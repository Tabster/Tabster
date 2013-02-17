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

        [STAThread]
        public static void Main(string[] args)
        {
            libraryManager.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var controller = new SingleInstanceController();

            TabHandler = new TabViewerManager();

            controller.Run(args);       
        }
    }
}