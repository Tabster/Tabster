#region

using System;
using Tabster.Core.Plugins;

#endregion

namespace GuitartabsDotCC
{
    public class GuitartabsDotCCPlugin : TabsterPluginBase, ITabsterPluginAttributes
    {
        #region Implementation of ITabsterPluginAttributes

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

        public Type[] Types
        {
            get { return new[] {typeof (GuitartabsDotCCSearchEngine), typeof (GuitartabsDotCCParser)}; }
        }

        #endregion
    }
}