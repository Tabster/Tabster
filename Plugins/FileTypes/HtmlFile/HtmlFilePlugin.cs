#region

using System;
using Tabster.Core.Plugins;

#endregion

namespace HtmlFile
{
    public class HtmlFilePlugin : ITabsterPlugin
    {
        #region Implementation of ITabsterPlugin

        public string Author
        {
            get { return "Nate Shoffner"; }
        }

        public string Copyright
        {
            get { return "Copyright © Nate Shoffner 2014"; }
        }

        public string Description
        {
            get { return "Supports exporting to HTML files. "; }
        }

        public string DisplayName
        {
            get { return "HTML file support"; }
        }

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public Uri Website
        {
            get { return new Uri("http://nateshoffner.com"); }
        }

        public Type[] Types
        {
            get { return new[] {typeof (HtmlFileExporter)}; }
        }

        #endregion
    }
}