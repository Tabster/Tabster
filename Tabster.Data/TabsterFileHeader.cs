#region

using System;

#endregion

namespace Tabster.Data
{
    public class TabsterFileHeader
    {
        public TabsterFileHeader(Version version, CompressionMode compression)
        {
            Version = version;
            Compression = compression;
        }

        public Version Version { get; private set; }
        public CompressionMode Compression { get; private set; }
    }
}