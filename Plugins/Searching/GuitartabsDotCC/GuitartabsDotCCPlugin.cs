#region

using System;
using System.Runtime.InteropServices;
using Tabster.Core.Plugins;

#endregion

namespace GuitartabsDotCC
{
    public class GuitartabsDotCCPlugin : ITabsterPlugin
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
            get { return "Supports guitartabs.cc tab searching and downloading."; }
        }

        public string DisplayName
        {
            get { return "Guitartabs.cc"; }
        }

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public Uri Website
        {
            get { return new Uri("http://nateshoffner.com"); }
        }

        public Guid Guid
        {
            get { return new Guid(((GuidAttribute)typeof(GuitartabsDotCCPlugin).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value); }
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