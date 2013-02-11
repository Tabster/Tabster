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

        [STAThread]
        public static void Main(string[] args)
        {
            //create the working directories if they don't already exist
            if (!Directory.Exists(Global.UserDirectory))
                Directory.CreateDirectory(Global.UserDirectory);

            if (!Directory.Exists(Global.LibraryDirectory))
                Directory.CreateDirectory(Global.LibraryDirectory);

            if (!Directory.Exists(Global.PlaylistDirectory))
                Directory.CreateDirectory(Global.PlaylistDirectory);

            if (!Directory.Exists(Global.WorkingDirectory))
                Directory.CreateDirectory(Global.WorkingDirectory);

            if (!Directory.Exists(Global.TempDirectory))
                Directory.CreateDirectory(Global.TempDirectory);

            Global.libraryManager.LoadTabs();
            Global.libraryManager.LoadPlaylists();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var tab = new Tab("artist", "title", TabType.Bass, "gvsckvlsjdfklsdfjsd") {Comment = "comment", Audio = "aduio", Lyrics = "FDASFSD"};
            var tf = new TabFile(tab, "C:\\test.tabster");

            var playlist = new Playlist("test");
            var pf = new PlaylistFile(playlist, "C:\\test1.tablist");
            pf.Save();

            /*
            var controller = new SingleInstanceController();

            TabHandler = new TabViewerManager();

            controller.Run(args);
            */
        }
    }
}