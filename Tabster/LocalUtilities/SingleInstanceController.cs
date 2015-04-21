#region

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Tabster.Data.Binary;
using Tabster.Forms;
using Tabster.Properties;

#endregion

namespace Tabster.LocalUtilities
{
    internal class SingleInstanceController : WindowsFormsApplicationBase
    {
        private static SqliteTabsterLibrary<TablatureFile, TablaturePlaylistFile> _library;
#if DEBUG
        private const int MIN_SPLASH_TIME = 1000;
#else
        private const int MIN_SPLASH_TIME = 3500;
#endif
        private static TablatureFile _queuedTabfile;
        private static TablaturePlaylistFile _queuedTablaturePlaylistFile;
        private static FileInfo _queuedFileInfo;
        private static bool _isLibraryOpen;
        private static bool _noSplash;
        private static bool _safeMode;

        public SingleInstanceController(SqliteTabsterLibrary<TablatureFile, TablaturePlaylistFile> library)
        {
            _library = library;
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

        private void ProcessCommandLine(ReadOnlyCollection<string> commandLine)
        {
            if (commandLine.Count > 0)
            {
                if (commandLine.Contains("-nosplash"))
                    _noSplash = true;
                if (commandLine.Contains("-safe-mode"))
                    _safeMode = true;

                var firstArg = commandLine[0];

                if (File.Exists(firstArg))
                {
                    _queuedFileInfo = new FileInfo(firstArg);
                    var tablatureDocument = _library.TablatureFileProcessor.Load(firstArg);

                    if (tablatureDocument != null)
                    {
                        _queuedTabfile = tablatureDocument;

                        if (_isLibraryOpen)
                            Program.TabbedViewer.LoadTablature(tablatureDocument, _queuedFileInfo);
                    }

                    else
                    {
                        var playlistDocument = _library.TablaturePlaylistFileProcessor.Load(firstArg);

                        if (playlistDocument != null)
                        {
                            _queuedTablaturePlaylistFile = playlistDocument;
                        }
                    }
                }
            }
        }

        private void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
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

            if (_queuedTabfile != null)
                base.MainForm = new MainForm(_library, _queuedTabfile, _queuedFileInfo);
            else if (_queuedTablaturePlaylistFile != null)
                base.MainForm = new MainForm(_library, _queuedTablaturePlaylistFile, _queuedFileInfo);
            else
                base.MainForm = new MainForm(_library);

            _isLibraryOpen = true;
        }

        #region Splash Screen

        private void SetSplashStatus(string status)
        {
            if (SplashScreen == null)
                return;

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

            if (!_safeMode)
            {
#if DEBUG
            Thread.Sleep(sleepDuration);
#endif

                Program.PluginController.LoadPlugins();
                SetSplashStatus(splashStatuses[1]);
            }

            _library.Load();

#if DEBUG
            Thread.Sleep(sleepDuration);
#endif

            if (Settings.Default.StartupUpdate)
            {
                SetSplashStatus(splashStatuses[2]);

#if DEBUG
                Thread.Sleep(sleepDuration);
#endif

#if !PORTABLE
                Program.UpdateQuery.Check(false);
#endif
            }
        }
    }
}