#region

using System;
using System.IO;
using System.Text;
using Tabster.Core.Types;
using Tabster.Data.Utilities;

#endregion

namespace Tabster.Data.Binary
{
    public class TablatureFile : TabsterBinaryFileBase, ITablatureFile
    {
        private const string HeaderString = "TABSTER";
        private static readonly Version HeaderVersion = new Version("1.0");
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;

        public TablatureFile()
            : base(HeaderString)
        {
        }

        #region Implementation of ITabsterFile

        public void Save(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    if (FileHeader == null)
                        FileHeader = new TabsterFileHeader(HeaderVersion, CompressionMode.Gzip);

                    if (FileAttributes == null)
                        FileAttributes = new TabsterFileAttributes(DateTime.UtcNow, DefaultEncoding);

                    WriteHeader(writer, HeaderString, FileHeader);
                    WriteFileAttributes(writer, FileAttributes);

                    //core attributes
                    writer.Write(Artist, FileAttributes.Encoding);
                    writer.Write(Title, FileAttributes.Encoding);
                    writer.Write(Type.Name, FileAttributes.Encoding);

                    //source attributes
                    writer.Write((int) SourceType);
                    writer.Write(Source != null ? Source.ToString() : string.Empty);

                    writer.Write(Comment ?? string.Empty, FileAttributes.Encoding);

                    if (FileHeader.Compression == CompressionMode.None)
                        writer.Write(Contents ?? string.Empty, FileAttributes.Encoding);
                    else if (FileHeader.Compression == CompressionMode.Gzip)
                        writer.WriteCompressedString(Contents ?? string.Empty, FileAttributes.Encoding);  
                }
            }
        }

        public TabsterFileAttributes FileAttributes { get; set; }
        public TabsterFileHeader FileHeader { get; private set; }

        public void Load(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(fs, DefaultEncoding))
                {
                    FileHeader = ReadHeader(reader);

                    FileAttributes = ReadFileAttributes(reader);

                    var fileEncoding = FileAttributes != null ? FileAttributes.Encoding : DefaultEncoding;

                    Artist = reader.ReadString(fileEncoding);
                    Title = reader.ReadString(fileEncoding);
                    Type = new TablatureType(reader.ReadString(fileEncoding));
                    SourceType = (TablatureSourceType) reader.ReadInt32();
                    var sourceString = reader.ReadString();
                    Source = string.IsNullOrEmpty(sourceString) ? null : new Uri(sourceString);
                    Comment = reader.ReadString(fileEncoding);

                    if (FileHeader.Compression == CompressionMode.None)
                        Contents = reader.ReadString(fileEncoding);
                    else if (FileHeader.Compression == CompressionMode.Gzip)
                        Contents = reader.ReadCompressedString(fileEncoding);
                }
            }
        }

        #endregion

        #region Implementation of ITablatureAttributes

        public string Artist { get; set; }
        public string Title { get; set; }
        public TablatureType Type { get; set; }

        #endregion

        #region Implementation of ITablature

        public string Contents { get; set; }

        #endregion

        #region Implementation of ITablatureFile

        public string Comment { get; set; }
        public TablatureSourceType SourceType { get; set; }
        public Uri Source { get; set; }

        #endregion
    }
}