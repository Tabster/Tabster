#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Tabster.Data.Binary;
using Tabster.Database;
using Tabster.Forms;
using Tabster.Properties;
using Tabster.Update;

#endregion

namespace Tabster.Utilities
{
    internal class TabsterSingleInstanceController : SingleInstanceControllerBase
    {
        private readonly bool _databaseMissing;
        private readonly LibraryManager _libraryManager;
        private readonly PlaylistManager _playlistManager;
        private bool _noSplash;
        private FileInfo _queuedFileInfo;
        private TablatureFile _queuedTablatureFile;
        private bool _safeMode;
        private SplashScreenController _splashScreenController;
        private UpdateResponseEventArgs _updateResponse;

        public TabsterSingleInstanceController(string fileName, LibraryManager libraryManager, PlaylistManager playlistManager, bool databaseMissing)
            : base(fileName)
        {
            _libraryManager = libraryManager;
            _playlistManager = playlistManager;
            _databaseMissing = databaseMissing;
        }

        #region Overrides of SingleInstanceControllerBase

        public override bool Start(ReadOnlyCollection<string> args)
        {
            ProcessCommandLine(args);
            _splashScreenController = new SplashScreenController(new SplashScreen(_safeMode));

            return base.Start(args);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!_noSplash)
            {
                _splashScreenController.Start(3000);
            }

            MainForm = _queuedTablatureFile != null ?
                new MainForm(_libraryManager, _playlistManager, _queuedTablatureFile, _queuedFileInfo, _updateResponse) :
                new MainForm(_libraryManager, _playlistManager, _updateResponse);

            MainForm.Closed += (s, ev) => Stop();

            PerformStartupEvents();

            base.OnStartup(e);
        }

        #region Overrides of SingleInstanceControllerBase

        protected override void OnStartupNextInstance(StartupEventArgs e)
        {
            if (e.CommandLineArguments.Count > 0)
            {
                ProcessFirstArgForFile(e.CommandLineArguments[0]);

                if (_queuedTablatureFile != null)
                {
                    TablatureViewForm.GetInstance(MainForm).LoadTablature(_queuedTablatureFile, _queuedFileInfo);
                }
            }

            base.OnStartupNextInstance(e);
        }

        #endregion

        #endregion

        private void ProcessFirstArgForFile(string arg)
        {
            if (File.Exists(arg))
            {
                var file = _libraryManager.GetTablatureFileProcessor().Load(arg);

                if (file != null)
                {
                    _queuedTablatureFile = file;
                    _queuedFileInfo = new FileInfo(arg);
                }
            }
        }

        private void ProcessCommandLine(ReadOnlyCollection<string> commandLine)
        {
            if (commandLine.Count > 0)
            {
                if (commandLine.Contains("-nosplash"))
                    _noSplash = true;
                if (commandLine.Contains("-safemode"))
                    _safeMode = true;

                ProcessFirstArgForFile(commandLine[0]);
            }
        }

        private void PerformStartupEvents()
        {
            if (!_safeMode)
            {
                _splashScreenController.Update("Initializing plugins...");
                Logging.GetLogger().Info("Loading plugins...");

                Program.GetPluginController().LoadPlugins();

                var disabledGuids = new List<Guid>();
                foreach (var guid in Settings.Default.DisabledPlugins)
                {
                    disabledGuids.Add(new Guid(guid));
                }

                foreach (var pluginHost in Program.GetPluginController().GetPluginHosts().Where(pluginHost => !disabledGuids.Contains(pluginHost.Plugin.Guid)))
                {
                    pluginHost.Enabled = true;
                }
            }

            Logging.GetLogger().Info("Initializing library...");
            _splashScreenController.Update("Loading library...");
            _libraryManager.Load(_databaseMissing);

            Logging.GetLogger().Info("Initializing playlists...");
            _splashScreenController.Update("Loading playlists...");
            _playlistManager.Load();

            _updateResponse = null;

            if (Settings.Default.StartupUpdate)
            {
                _splashScreenController.Update("Checking for updates...");
                UpdateCheck.Completed += (s, e) => { _updateResponse = e; };
                UpdateCheck.Check(true);
            }

            _splashScreenController.Stop();
        }
    }
}