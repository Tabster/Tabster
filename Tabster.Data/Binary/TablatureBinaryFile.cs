#region

using System;
using System.IO;
using Tabster.Core.Types;

#endregion

namespace Tabster.Data.Binary
{
    public class TablatureFile : TabsterBinaryFileBase, ITablatureFile
    {
        private const string HeaderString = "TABSTER";
        private static readonly Version HeaderVersion = new Version("1.0");

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
                    var header = new TabsterBinaryFileHeader(HeaderVersion, false);
                    WriteHeader(writer, HeaderString, header);
                    WriteFileAttributes(writer, FileAttributes ?? new TabsterFileAttributes(DateTime.Now));

                    //core attributes
                    writer.Write(Artist);
                    writer.Write(Title);
                    writer.Write(Type.Name);

                    //source attributes
                    writer.Write((int) SourceType);
                    writer.Write(Source != null ? Source.ToString() : string.Empty);

                    writer.Write(Comment ?? string.Empty);

                    writer.Write(Contents ?? string.Empty);
                }
            }
        }

        public TabsterFileAttributes FileAttributes { get; set; }
        public ITabsterFileHeader FileHeader { get; private set; }

        public void Load(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(fs))
                {
                    FileHeader = ReadHeader(reader);

                    var created = new DateTime(reader.ReadInt64());
                    FileAttributes = new TabsterFileAttributes(created);

                    Artist = reader.ReadString();
                    Title = reader.ReadString();
                    Type = new TablatureType(reader.ReadString());
                    SourceType = (TablatureSourceType) reader.ReadInt32();
                    Source = new Uri(reader.ReadString());
                    Comment = reader.ReadString();
                    Contents = reader.ReadString();
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