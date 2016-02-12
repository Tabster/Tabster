#region

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Tabster.Data.Binary;
using Tabster.Database;
using Tabster.Forms;
using Tabster.Properties;
using Tabster.Update;

#endregion

namespace Tabster.Utilities
{
    internal class SingleInstanceController : WindowsFormsApplicationBase
    {
        private const int MinSplashTime = 3500;

        private static TablatureFile _queuedTabfile;
        private static FileInfo _queuedFileInfo;
        private static bool _isLibraryOpen;
        private static bool _noSplash;
        private static bool _safeMode;
        private readonly bool _filesNeedScanned;
        private readonly LibraryManager _libraryManager;
        private readonly PlaylistManager _playlistManager;
        private UpdateResponseEventArgs _updateResponse;

        public SingleInstanceController(LibraryManager libraryManager, PlaylistManager playlistManager, bool filesNeedScanned)
        {
            _libraryManager = libraryManager;
            _playlistManager = playlistManager;
            _filesNeedScanned = filesNeedScanned;
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
                if (commandLine.Contains("-safemode"))
                    _safeMode = true;

                var firstArg = commandLine[0];

                if (File.Exists(firstArg))
                {
                    var file = _libraryManager.GetTablatureFileProcessor().Load(firstArg);

                    if (file != null)
                    {
                        _queuedTabfile = file;
                        _queuedFileInfo = new FileInfo(firstArg);

                        if (_isLibraryOpen)
                            Program.TabbedViewer.LoadTablature(file, _queuedFileInfo);
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
                MinimumSplashScreenDisplayTime = MinSplashTime;
                base.SplashScreen = new SplashScreen(_safeMode) {Cursor = Cursors.AppStarting};
            }
        }

        protected override void OnCreateMainForm()
        {
            base.OnCreateMainForm();

            PerformStartupEvents();

            base.MainForm = _queuedTabfile != null ?
                new MainForm(_libraryManager, _playlistManager, _queuedTabfile, _queuedFileInfo, _updateResponse) :
                new MainForm(_libraryManager, _playlistManager, _updateResponse);

            _isLibraryOpen = true;
        }

        private void PerformStartupEvents()
        {
            if (!_safeMode)
            {
                SetSplashStatus("Initializing plugins...");
                Program.LoadPlugins();
            }

            SetSplashStatus("Loading library...");
            _libraryManager.Load(_filesNeedScanned);

            SetSplashStatus("Loading playlists...");
            _playlistManager.Load();

            if (Settings.Default.StartupUpdate)
            {
                SetSplashStatus("Checking for updates...");
                Program.UpdateQuery.Completed += (s, e) => { _updateResponse = e; };
                Program.UpdateQuery.Check(true);
            }
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
    }
}