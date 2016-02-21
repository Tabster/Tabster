#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Processing;
using Tabster.Data.Xml;
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
            _splashScreenController = new SplashScreenController(new SplashScreen(TabsterEnvironment.SafeMode));

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
                    TabsterEnvironment.SafeMode = true;

                ProcessFirstArgForFile(commandLine[0]);
            }
        }

        private void PerformStartupEvents()
        {
            if (!TabsterEnvironment.SafeMode)
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

            // database file deleted or possible pre-2.0 version, convert existing files
            if (_databaseMissing)
            {
                Logging.GetLogger().Info("Converting old file types...");
                _splashScreenController.Update("Converting old file types...");
                ConvertXmlFiles();
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

        /// <summary>
        ///     Convert Xml-based files to binary.
        /// </summary>
        private void ConvertXmlFiles()
        {
            var playlistsDirectory = Path.Combine(TabsterEnvironment.GetEnvironmentDirectoryPath(TabsterEnvironmentDirectory.UserData), "Playlists");

            // playlists are no longer stored as files, but are now stored in database
            if (Directory.Exists(playlistsDirectory))
            {
#pragma warning disable 612
                var playlistProcessor = new TabsterFileProcessor<TablaturePlaylistDocument>(TablaturePlaylistDocument.FileVersion);
#pragma warning restore 612

                foreach (var file in Directory.GetFiles(playlistsDirectory, string.Format("*{0}", Constants.TablaturePlaylistFileExtension), SearchOption.AllDirectories))
                {
                    var playlistFile = playlistProcessor.Load(file);

                    if (playlistFile != null)
                    {
                        var playlist = new TablaturePlaylist(playlistFile.Name) {Created = playlistFile.FileAttributes.Created};

                        foreach (var item in playlistFile)
                        {
                            playlist.Add(item);
                        }

                        _playlistManager.Update(playlist);

                        try
                        {
                            File.Delete(file);
                        }

                        catch
                        {
                            // unhandled
                        }
                    }
                }
            }

            if (Directory.Exists(_libraryManager.TablatureDirectory))
            {
                foreach (var file in Directory.GetFiles(_libraryManager.TablatureDirectory, string.Format("*{0}", Constants.TablatureFileExtension), SearchOption.AllDirectories))
                {
                    var tablatureFile = TabsterXmlFileConverter.ConvertTablatureDocument(file);

                    if (tablatureFile != null)
                        tablatureFile.Save(file);
                }
            }
        }
    }
}