#region

using System;

#endregion

namespace Tabster.Data
{
    public class TabsterFileHeader
    {
        public TabsterFileHeader(string headerString, Version formatVersion, bool compressed)
        {
            HeaderString = headerString;
            FormatVersion = formatVersion;
            Compressed = compressed;
        }

        public string HeaderString { get; private set; }
        public Version FormatVersion { get; private set; }
        public bool Compressed { get; private set; }
        //todo Encoding
    }
}