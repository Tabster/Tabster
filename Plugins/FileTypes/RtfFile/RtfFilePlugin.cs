#region

using System;
using Tabster.Core.Plugins;

#endregion

namespace RtfFile
{
    public class RtfFilePlugin : ITabsterPlugin
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
            get { return "Supports importing and exporting to/from Rich Text Format (.rtf) files. "; }
        }

        public string DisplayName
        {
            get { return "RTF support"; }
        }

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public Uri Website
        {
            get { return new Uri("http://nateshoffner.com"); }
        }

        public void Activate()
        {
            // not implemented
        }

        public void Deactivate()
        {
            // not implemented
        }

        public void Initialize()
        {
            // not implemented
        }

        #endregion
    }
}