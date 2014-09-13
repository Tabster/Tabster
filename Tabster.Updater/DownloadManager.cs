#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using Tabster.Utilities.Net;

#endregion

namespace Tabster.Updater
{
    internal class DownloadManager
    {
        private readonly string _downloadLocation;
        private readonly Uri _downloadURL;

        public DownloadManager(Uri url)
        {
            _downloadURL = url;

            var temp = Path.GetTempPath();
            var guid = Guid.NewGuid();

            _downloadLocation = Path.Combine(temp, string.Format("{0}.exe", guid));
        }

        public event DownloadProgressChangedEventHandler DownloadProgressChanged;
        public event AsyncCompletedEventHandler DownloadFileCompleted;

        public void Start()
        {
            using (var client = new TabsterWebClient())
            {
                client.DownloadFileCompleted += client_DownloadFileCompleted;
                client.DownloadProgressChanged += client_DownloadProgressChanged;
                client.DownloadFileAsync(_downloadURL, _downloadLocation);
            }
        }

        public void RunInstaller(bool silent)
        {
            if (_downloadLocation != null)
            {
                var installPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var psi = new ProcessStartInfo
                              {
                                  //Verb = "runas",
                                  //UseShellExecute = true,
                                  WorkingDirectory = Path.GetDirectoryName(_downloadLocation),
                                  FileName = Path.GetFileName(_downloadLocation),
                                  Arguments = silent ? string.Format("/S /D={0}", installPath) : "",
                              };

                var process = Process.Start(psi);

                if (silent)
                    process.WaitForExit();
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (DownloadProgressChanged != null)
                DownloadProgressChanged(sender, e);
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (DownloadFileCompleted != null)
                DownloadFileCompleted(sender, e);
        }
    }
}