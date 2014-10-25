﻿#region

using System;
using Tabster.Core.Plugins;

#endregion

namespace PngFile
{
    public class PngFilePlugin : ITabsterPlugin
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
            get { return "Supports exporting to Portable Network Graphics (.png) files. "; }
        }

        public string DisplayName
        {
            get { return "PNG export support"; }
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
            get { return new[] {typeof (PngFileExporter)}; }
        }

        #endregion
    }
}