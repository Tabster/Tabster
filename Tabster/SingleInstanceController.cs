#region

using System.Collections.ObjectModel;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Tabster.Forms;

#endregion

namespace Tabster
{
    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        private static TabFile _queuedTabfile;
        private static bool _isLibraryOpen;
        private static bool _noSplash;

        public SingleInstanceController()
        {
            IsSingleInstance = true;
            StartupNextInstance += this_StartupNextInstance;
        }

        private static void ProcessCommandLine(ReadOnlyCollection<string> commandLine)
        {
            if (commandLine.Count > 0)
            {
                TabFile t;
                if (TabFile.TryParse(commandLine[0], out t))
                {
                    _queuedTabfile = t;

                    if (_isLibraryOpen)
                        Program.TabHandler.LoadTab(t, true);
                }

                if (commandLine.Contains("-nosplash"))
                {
                    _noSplash = true;
                }
            }
        }

        private static void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            ProcessCommandLine(e.CommandLine);
        }

        protected override bool OnStartup(StartupEventArgs e)
        {
            ProcessCommandLine(e.CommandLine);
            return base.OnStartup(e);
        }

        protected override void OnCreateSplashScreen()
        {
            base.OnCreateSplashScreen();

            if (!_noSplash)
            {
                MinimumSplashScreenDisplayTime = 3500; //seems to make MainForm show prematurely
                SplashScreen = new Splash {Cursor = Cursors.AppStarting};
            }
        }

        protected override void OnCreateMainForm()
        {
            base.OnCreateMainForm();

            MainForm = _queuedTabfile != null ? new Form1(_queuedTabfile) : new Form1();
            _isLibraryOpen = true;
        }
    }
}