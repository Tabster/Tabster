#region

using System;
using System.IO;

#endregion

namespace Tabster.Data.Binary
{
    public abstract class TabsterBinaryFileBase
    {
        private readonly string _headerString;

        protected TabsterBinaryFileBase(string headerString)
        {
            _headerString = headerString;
        }

        protected TabsterBinaryFileHeader ReadHeader(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            var headerStr = new string(reader.ReadChars(_headerString.Length));
            if (headerStr != _headerString)
                throw new TabsterFileException("Header string mismatch.");

            var versionStr = reader.ReadString();
            var version = new Version(versionStr);

            var compressedFlag = reader.ReadBoolean();

            return new TabsterBinaryFileHeader(version, compressedFlag);
        }

        protected void WriteHeader(BinaryWriter writer, string headerStr, TabsterBinaryFileHeader header)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (header == null)
                throw new ArgumentNullException("header");

            writer.Write(headerStr.ToCharArray());
            writer.Write(header.Version.ToString());
            writer.Write(header.Compressed);
        }
    }
}