#region

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Tabster.Core.Data;
using Tabster.Core.Data.Processing;
using Tabster.Forms;
using Tabster.Properties;

#endregion

namespace Tabster.LocalUtilities
{
    public class SingleInstanceController : WindowsFormsApplicationBase
    {
#if DEBUG
        private const int MIN_SPLASH_TIME = 1000;
#else
        private const int MIN_SPLASH_TIME = 3500;
#endif
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

                if (File.Exists(firstArg))
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
                MinimumSplashScreenDisplayTime = MIN_SPLASH_TIME;
                base.SplashScreen = new SplashScreen {Cursor = Cursors.AppStarting};
            }
        }

        protected override void OnCreateMainForm()
        {
            base.OnCreateMainForm();

            PerformStartupEvents();

            base.MainForm = _queuedTabfile != null ? new MainForm(_queuedTabfile) : new MainForm();
            _isLibraryOpen = true;
        }

        #region Splash Screen

        private void SetSplashStatus(string status)
        {
            var splash = ((SplashScreen) SplashScreen);

            try
            {
                splash.SetStatus(status);
            }

            catch (InvalidOperationException)
            {
                //sometimes happens "randomly"
            }
        }

        #endregion

        private void PerformStartupEvents()
        {
            var splashStatuses = new[] {"Initializing plugins...", "Loading library...", "Checking for updates..."};

            var sleepDuration = MIN_SPLASH_TIME/splashStatuses.Length/2;

            SetSplashStatus(splashStatuses[0]);

#if DEBUG
            Thread.Sleep(sleepDuration);
#endif

            Program.pluginController.LoadPlugins();

            SetSplashStatus(splashStatuses[1]);

            Program.tablatureLibrary.Load();

#if DEBUG
            Thread.Sleep(sleepDuration);
#endif

            if (Settings.Default.StartupUpdate)
            {
                SetSplashStatus(splashStatuses[2]);

#if DEBUG
                Thread.Sleep(sleepDuration);
#endif
                Program.updateQuery.Check(false);
            }
        }
    }
}