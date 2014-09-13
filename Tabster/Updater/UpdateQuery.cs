#region

using System;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using Tabster.Utilities.Net;

#endregion

namespace Tabster.Updater
{
    public class UpdateQueryCompletedEventArgs : EventArgs
    {
        public UpdateQueryCompletedEventArgs(object userState)
        {
            UserState = userState;
        }

        public object UserState { get; private set; }
    }

    public class UpdateQuery
    {
        public bool UpdateAvailable { get; private set; }
        public Exception Error { get; private set; }
        public Version Version { get; private set; }
        public string Changelog { get; private set; }
        public Uri DownloadURL { get; private set; }

        public event EventHandler<UpdateQueryCompletedEventArgs> Completed;

        public void Check(object userToken = null)
        {
            try
            {
                using (var client = new TabsterWebClient())
                {
                    client.DownloadStringCompleted += client_DownloadStringCompleted;
                    client.DownloadStringAsync(new Uri("http://client.tabster.me/update"), userToken);
                }
            }

            catch (Exception ex)
            {
                Error = ex;
            }
        }

        private void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Error = e.Error;

            if (e.Error == null && !e.Cancelled && !string.IsNullOrEmpty(e.Result))
            {
                var doc = new XmlDocument();
                doc.LoadXml(e.Result);

                Version = new Version(doc.GetElementsByTagName("version")[0].InnerText);
                DownloadURL = new Uri(doc.GetElementsByTagName("url")[0].InnerText);
                Changelog = doc.GetElementsByTagName("changelog")[0].InnerText;

                UpdateAvailable = Version > new Version(Application.ProductVersion);
            }

            if (Completed != null)
                Completed(this, new UpdateQueryCompletedEventArgs(e.UserState));
        }
    }
}