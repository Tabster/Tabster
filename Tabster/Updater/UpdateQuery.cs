#region

using System;
using System.Windows.Forms;
using System.Xml;

#endregion

namespace Tabster.Updater
{
    public class UpdateQuery
    {
        public bool UpdateAvailable { get; private set; }
        public Exception Error { get; private set; }
        public Version Version { get; private set; }
        public string Changelog { get; private set; }
        public Uri DownloadURL { get; private set; }

        public event EventHandler Checked;

        public void Check()
        {
            try
            {
                using (var client = new UpdateWebClient())
                {
                    var data = client.DownloadString("http://client.tabster.me/update");

                    if (!string.IsNullOrEmpty(data))
                    {
                        var doc = new XmlDocument();
                        doc.LoadXml(data);

                        Version = new Version(doc.GetElementsByTagName("version")[0].InnerText);
                        DownloadURL = new Uri(doc.GetElementsByTagName("url")[0].InnerText);
                        Changelog = doc.GetElementsByTagName("changelog")[0].InnerText;
                        UpdateAvailable = Version > new Version(Application.ProductVersion);

                        Error = null;
                    }
                }
            }

            catch (Exception ex)
            {
                Error = ex;
            }

            if (Checked != null)
                Checked(this, EventArgs.Empty);
        }
    }
}