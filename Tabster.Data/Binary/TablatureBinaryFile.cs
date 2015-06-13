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
                    writer.Write((byte) SourceType);
                    writer.Write(Source != null ? Source.ToString() : string.Empty);
                    writer.Write(SourceTag ?? string.Empty, FileAttributes.Encoding);

                    //extended attributes
                    writer.Write(Difficulty != null ? Difficulty.Name : TablatureDifficulty.Undefined.Name, FileAttributes.Encoding);
                    writer.Write(Tuning != null ? Tuning.Name : TablatureTuning.Undefined.Name, FileAttributes.Encoding);
                    writer.Write(Subtitle ?? string.Empty, FileAttributes.Encoding);
                    writer.Write(Album ?? string.Empty, FileAttributes.Encoding);
                    writer.Write(Genre ?? string.Empty, FileAttributes.Encoding);
                    writer.Write(Author ?? string.Empty, FileAttributes.Encoding);
                    writer.Write(Copyright ?? string.Empty, FileAttributes.Encoding);
                    writer.Write(Comment ?? string.Empty, FileAttributes.Encoding);

                    //lyrics
                    if (FileHeader.Compression == CompressionMode.None)
                        writer.Write(Lyrics ?? string.Empty, FileAttributes.Encoding);
                    else if (FileHeader.Compression == CompressionMode.Gzip)
                        writer.WriteCompressedString(Lyrics ?? string.Empty, FileAttributes.Encoding);

                    //body
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

                    //core attributes
                    Artist = reader.ReadString(fileEncoding);
                    Title = reader.ReadString(fileEncoding);
                    Type = new TablatureType(reader.ReadString(fileEncoding));

                    //source attributes
                    SourceType = (TablatureSourceType) reader.ReadByte();
                    var sourceString = reader.ReadString();
                    Source = string.IsNullOrEmpty(sourceString) ? null : new Uri(sourceString);
                    SourceTag = reader.ReadString(fileEncoding);

                    //extended attributes
                    Difficulty = new TablatureDifficulty(reader.ReadString(fileEncoding));
                    Tuning = new TablatureTuning(reader.ReadString(fileEncoding));
                    Subtitle = reader.ReadString(fileEncoding);
                    Album = reader.ReadString(fileEncoding);
                    Genre = reader.ReadString(fileEncoding);
                    Author = reader.ReadString(fileEncoding);
                    Copyright = reader.ReadString(fileEncoding);
                    Comment = reader.ReadString(fileEncoding);

                    //lyrics
                    if (FileHeader.Compression == CompressionMode.None)
                        Lyrics = reader.ReadString(fileEncoding);
                    else if (FileHeader.Compression == CompressionMode.Gzip)
                        Lyrics = reader.ReadCompressedString(fileEncoding);

                    //body
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

        public string Subtitle { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string Copyright { get; set; }
        public string Lyrics { get; set; }
        public string Comment { get; set; }
        public TablatureDifficulty Difficulty { get; set; }
        public TablatureTuning Tuning { get; set; }
        public TablatureSourceType SourceType { get; set; }
        public Uri Source { get; set; }
        public string SourceTag { get; set; }

        #endregion
    }
}