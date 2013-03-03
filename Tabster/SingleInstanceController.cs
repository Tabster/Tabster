#region

using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Tabster.Forms;
using Tabster.Properties;

#endregion

namespace Tabster
{
    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        private bool _openLibrary = true;

        public SingleInstanceController()
        {
            IsSingleInstance = true;
            StartupNextInstance += this_StartupNextInstance;
        }

        private void ProcessCommandLine(ReadOnlyCollection<string> commandLine)
        {
            if (commandLine.Count > 0)
            {
                TabFile t;
                if (TabFile.TryParse(commandLine[0], out t))
                {
                    _openLibrary = false;
                    Program.TabHandler.LoadTab(t, true);
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
            if (Settings.Default.ShowSplash)
            {
                MinimumSplashScreenDisplayTime = 3500;
                SplashScreen = new Splash {Cursor = Cursors.AppStarting};
            }
        }

        protected override void OnCreateMainForm()
        {
            if (_openLibrary)
            {
                MainForm = new Form1();
            }
        }
    }
}