#region

using System;

#endregion

namespace Tabster.Data.Xml
{
    [Obsolete]
    public class TabsterXmlFileHeader : ITabsterFileHeader
    {
        #region Implementation of ITabsterFileHeader

        public Version Version { get; private set; }

        #endregion

        public TabsterXmlFileHeader(Version version)
        {
            Version = version;
        }
    }
}