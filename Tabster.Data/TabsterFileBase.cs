#region

using System;
using System.IO;

#endregion

namespace Tabster.Data
{
    public abstract class TabsterFileBase
    {
        private readonly string _headerString;

        protected TabsterFileBase(string headerString)
        {
            _headerString = headerString;
        }

        protected TabsterFileHeader ReadHeader(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            var headerStr = new string(reader.ReadChars(_headerString.Length));
            if (headerStr != _headerString)
                throw new TabsterFileException("Header string mismatch.");

            var versionStr = reader.ReadString();
            var version = new Version(versionStr);

            var compressedFlag = reader.ReadBoolean();

            return new TabsterFileHeader(headerStr, version, compressedFlag);
        }

        protected void WriteHeader(BinaryWriter writer, TabsterFileHeader header)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (header == null)
                throw new ArgumentNullException("header");

            writer.Write(header.HeaderString.ToCharArray());
            writer.Write(header.FormatVersion.ToString());
            writer.Write(header.Compressed);
        }
    }
}