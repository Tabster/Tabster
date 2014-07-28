#region

using System.Collections.ObjectModel;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Tabster.Core.FileTypes;
using Tabster.Forms;

#endregion

namespace Tabster.Utilities
{
    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        private static TablatureDocument _queuedTabfile;
        private static bool _isLibraryOpen;
        private static bool _noSplash;

        public SingleInstanceController()
        {
            IsSingleInstance = true;
            StartupNextInstance += this_StartupNextInstance;
        }

        public new Form MainForm
        {
            get { return base.MainForm; }
        }

        public new Form SplashScreen
        {
            get { return base.SplashScreen; }
        }

        private static void ProcessCommandLine(ReadOnlyCollection<string> commandLine)
        {
            if (commandLine.Count > 0)
            {
                if (commandLine.Contains("-nosplash"))
                {
                    _noSplash = true;
                }

                var firstArg = commandLine[0];

                if (Common.IsFilePath(firstArg, true))
                {
                    var processor = new TabsterDocumentProcessor<TablatureDocument>(TablatureDocument.FILE_VERSION, true);

                    var tab = processor.Load(firstArg);

                    if (tab != null)
                    {
                        _queuedTabfile = tab;

                        if (_isLibraryOpen)
                            Program.TabHandler.LoadExternally(tab, true);
                    }
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
                base.SplashScreen = new SplashScreen {Cursor = Cursors.AppStarting};
            }
        }

        protected override void OnCreateMainForm()
        {
            base.OnCreateMainForm();
            base.MainForm = _queuedTabfile != null ? new MainForm(_queuedTabfile) : new MainForm();
            _isLibraryOpen = true;
        }
    }
}