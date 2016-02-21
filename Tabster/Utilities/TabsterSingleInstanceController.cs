#region

using System.Collections.ObjectModel;
using System.IO;
using Tabster.Data;
using Tabster.Data.Binary;
using Tabster.Data.Processing;
using Tabster.Forms;

#endregion

namespace Tabster.Utilities
{
    internal class TabsterSingleInstanceController : SingleInstanceControllerBase
    {
        private FileInfo _queuedFileInfo;
        private TablatureFile _queuedTablatureFile;

        public TabsterSingleInstanceController(string fileName)
            : base(fileName)
        {
        }

        #region Overrides of SingleInstanceControllerBase

        public override bool Start(ReadOnlyCollection<string> args)
        {
            ProcessCommandLine(args);
            return base.Start(args);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainForm = _queuedTablatureFile != null ?
                new MainForm(_queuedTablatureFile, _queuedFileInfo) :
                new MainForm();

            MainForm.Visible = false;

            MainForm.Closed += (s, ev) => Stop();

            base.OnStartup(e);
        }

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

        private void ProcessFirstArgForFile(string arg)
        {
            if (File.Exists(arg))
            {
                var tablatureFileProcessor = new TabsterFileProcessor<TablatureFile>(Constants.TablatureFileVersion);
                var file = tablatureFileProcessor.Load(arg);

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
                    TabsterEnvironment.NoSplash = true;
                if (commandLine.Contains("-safemode"))
                    TabsterEnvironment.SafeMode = true;

                ProcessFirstArgForFile(commandLine[0]);
            }
        }
    }
}