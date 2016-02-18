#region

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace Tabster.Utilities
{
    internal class SingleInstanceControllerBase : IDisposable
    {
        private static FileSystemWatcher _fileSystemWatcher;

        private readonly string _uniqueFilePath;
        private Form _mainForm;

        public SingleInstanceControllerBase(string fileName)
        {
            _uniqueFilePath = fileName;
            _fileSystemWatcher = new FileSystemWatcher {Path = Path.GetDirectoryName(fileName), Filter = Path.GetFileName(fileName)};
            _fileSystemWatcher.Changed += _fileSystemWatcher_Changed;
            _fileSystemWatcher.SynchronizingObject = MainForm;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        protected Form MainForm
        {
            get { return _mainForm; }
            set
            {
                _mainForm = value;
                _fileSystemWatcher.SynchronizingObject = _mainForm;
            }
        }

        public virtual bool Start(ReadOnlyCollection<string> args)
        {
            var status = ReadInstanceStatus();

            if (status != null)
            {
                // check if process is still running
                // if not, it means the previous instance likely didn't exit cleanly
                if (IsProcessRunning(status.ProcessId))
                {
                    // write command line args to file for existing instance to use
                    _fileSystemWatcher.EnableRaisingEvents = false;
                    WriteInstanceStatus(new InstanceStatus(status.ProcessId, args));
                    _fileSystemWatcher.EnableRaisingEvents = true;
                    return false;
                }
            }

            OnStartup(new StartupEventArgs(new ReadOnlyCollection<string>(args)));

            return true;
        }

        public virtual void Stop()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;

            if (File.Exists(_uniqueFilePath))
                File.Delete(_uniqueFilePath);
        }

        protected virtual void OnStartup(StartupEventArgs e)
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            WriteInstanceStatus(new InstanceStatus(Process.GetCurrentProcess().Id, e.CommandLineArguments));
            _fileSystemWatcher.EnableRaisingEvents = true;
            Application.Run(MainForm);
        }

        protected virtual void OnStartupNextInstance(StartupEventArgs e)
        {
        }

        private static bool IsProcessRunning(int id)
        {
            try
            {
                Process.GetProcessById(id);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        private InstanceStatus ReadInstanceStatus()
        {
            if (File.Exists(_uniqueFilePath))
            {
                // a bit hacky, but we need to wait until file is readable
                while (true)
                {
                    try
                    {
                        using (var fs = new FileStream(_uniqueFilePath, FileMode.Open))
                        {
                            using (var reader = new BinaryReader(fs))
                            {
                                var processId = reader.ReadInt32();
                                var argCount = reader.ReadInt32();

                                var args = new string[argCount];

                                for (var i = 0; i < argCount; i++)
                                {
                                    args[i] = reader.ReadString();
                                }

                                return new InstanceStatus(processId, new ReadOnlyCollection<string>(args));
                            }
                        }
                    }

                    catch (FileNotFoundException)
                    {
                    }
                    catch (IOException)
                    {
                    }
                    Thread.Sleep(50);
                }
            }

            return null;
        }

        private void WriteInstanceStatus(InstanceStatus status)
        {
            using (var fs = new FileStream(_uniqueFilePath, FileMode.Create))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    writer.Write(status.ProcessId);
                    writer.Write(status.CommandLineArgs.Count);

                    foreach (var arg in status.CommandLineArgs)
                    {
                        writer.Write(arg);
                    }
                }
            }
        }

        private void _fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            // temporarliy disable events to avoid duplicate event
            _fileSystemWatcher.EnableRaisingEvents = false;

            var status = ReadInstanceStatus();

            if (status != null)
            {
                OnStartupNextInstance(new StartupEventArgs(status.CommandLineArgs));
            }

            // renable events
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (_fileSystemWatcher != null)
                _fileSystemWatcher.Dispose();
        }

        #endregion

        internal class InstanceStatus
        {
            public InstanceStatus(int processId, ReadOnlyCollection<string> commandLineArgs)
            {
                ProcessId = processId;
                CommandLineArgs = commandLineArgs;
            }

            public int ProcessId { get; private set; }
            public ReadOnlyCollection<string> CommandLineArgs { get; private set; }
        }

        internal class StartupEventArgs : EventArgs
        {
            public StartupEventArgs(ReadOnlyCollection<string> commandLineArguments)
            {
                CommandLineArguments = commandLineArguments;
            }

            public ReadOnlyCollection<string> CommandLineArguments { get; set; }
        }
    }
}