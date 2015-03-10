#region

using System;

#endregion

namespace Tabster.Data.Binary
{
    public class TabsterBinaryFileHeader : ITabsterFileHeader
    {
        #region Implementation of ITabsterFileHeader

        public bool Compressed { get; private set; }
        public Version Version { get; private set; }

        #endregion

        public TabsterBinaryFileHeader(Version version, bool compressed)
        {
            Version = version;
            Compressed = compressed;
        }
    }
}