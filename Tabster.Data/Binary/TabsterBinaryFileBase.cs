#region

using System;
using System.IO;
using System.Text;

#endregion

namespace Tabster.Data.Binary
{
    public abstract class TabsterBinaryFileBase
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private readonly string _headerString;

        protected TabsterBinaryFileBase(string headerString)
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

            var compression = (CompressionMode) reader.ReadByte();

            return new TabsterFileHeader(version, compression);
        }

        protected void WriteHeader(BinaryWriter writer, string headerStr, TabsterFileHeader header)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (headerStr == null)
                throw new ArgumentNullException("headerStr");
            if (header == null)
                throw new ArgumentNullException("header");

            writer.Write(headerStr.ToCharArray());
            writer.Write(header.Version.ToString());
            writer.Write((byte) header.Compression);
        }

        protected TabsterFileAttributes ReadFileAttributes(BinaryReader reader)
        {
            var encoding = Encoding.GetEncoding(reader.ReadInt32());
            var created = FromUnixTime(reader.ReadInt64());
            return new TabsterFileAttributes(created, encoding);
        }

        protected void WriteFileAttributes(BinaryWriter writer, TabsterFileAttributes attributes)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (attributes == null)
                throw new ArgumentNullException("attributes");

            writer.Write(attributes.Encoding.CodePage);
            writer.Write(ToUnixTime(attributes.Created));
        }

        private static long ToUnixTime(DateTime date)
        {
            return (long) (date.ToUniversalTime() - UnixEpoch).TotalSeconds;
        }

        private static DateTime FromUnixTime(long seconds)
        {
            return UnixEpoch.AddSeconds(seconds);
        }
    }
}