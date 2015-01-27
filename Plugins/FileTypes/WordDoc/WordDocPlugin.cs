#region

using System;
using Tabster.Core.Plugins;

#endregion

namespace WordDoc
{
    public class WordDocPlugin : TabsterPluginBase, ITabsterPluginAttributes
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
            get { return "Supports importing and exporting to/from Microsoft Word (.docx) files. "; }
        }

        public string DisplayName
        {
            get { return "Word Document support"; }
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
            get { return new[] {typeof (WordDocExporter), typeof (WordDocImporter)}; }
        }

        #endregion

        #region Implementation of ITabsterPluginAttributes

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}