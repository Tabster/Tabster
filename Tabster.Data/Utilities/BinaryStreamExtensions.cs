#region

using System.IO;
using System.IO.Compression;
using System.Text;

#endregion

namespace Tabster.Data.Utilities
{
    internal static class BinaryStreamExtensions
    {
        #region String Encoding

        public static string ReadString(this BinaryReader reader, Encoding encoding)
        {
            var length = reader.ReadInt32();
            var bytes = reader.ReadBytes(length);
            return encoding.GetString(bytes);
        }

        public static void Write(this BinaryWriter writer, string str, Encoding encoding)
        {
            var bytes = encoding.GetBytes(str);
            writer.Write(bytes.Length);
            writer.Write(bytes);
        }

        #endregion

        #region String Compression

        /// <summary>
        ///   Writes a length-prefixed Gzipped string.
        /// </summary>
        public static void Write(this BinaryWriter writer, string str, bool compressed)
        {
            if (compressed)
            {
                var zipped = ZipText(str, Encoding.Unicode);
                writer.Write(zipped.Length);
                writer.Write(zipped);
            }

            else
            {
                writer.Write(str);
            }
        }

        /// <summary>
        ///   Reads a length-prefixed Gzipped string.
        /// </summary>
        public static string ReadString(this BinaryReader reader, bool compreseed)
        {
            if (compreseed)
            {
                var length = reader.ReadInt32();
                var zipped = reader.ReadBytes(length);
                return Unzip(zipped, Encoding.Unicode);
            }

            return reader.ReadString();
        }

        private static void CopyTo(Stream src, Stream dest)
        {
            var bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        private static byte[] ZipText(string text, Encoding encoding)
        {
            var bytes = encoding.GetBytes(text);

            using (var msi = new MemoryStream(bytes))
            {
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(mso, CompressionMode.Compress))
                    {
                        CopyTo(msi, gs);
                    }

                    return mso.ToArray();
                }
            }
        }

        private static string Unzip(byte[] bytes, Encoding encoding)
        {
            using (var msi = new MemoryStream(bytes))
            {
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                    {
                        CopyTo(gs, mso);
                    }

                    return encoding.GetString(mso.ToArray());
                }
            }
        }

        #endregion
    }
}