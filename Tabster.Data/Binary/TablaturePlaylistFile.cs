#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace Tabster.Data.Binary
{
    public class TablaturePlaylistFile : TabsterBinaryFileBase, ITablaturePlaylistFile
    {
        private const string HeaderString = "TABSTER";
        private static readonly Version HeaderVersion = new Version("1.0");

        private readonly List<ITablatureFile> _files = new List<ITablatureFile>();

        #region Constructors

        public TablaturePlaylistFile() : base(HeaderString)
        {
        }

        public TablaturePlaylistFile(string name)
            : this()
        {
            Name = name;
        }

        #endregion

        #region Implementation of ITabsterFile

        public FileInfo FileInfo { get; private set; }

        public ITabsterFileHeader Load(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(fs))
                {
                    var header = ReadHeader(reader);

                    var created = new DateTime(reader.ReadInt64());
                    FileAttributes = new TabsterFileAttributes(created);

                    var count = reader.ReadInt32();

                    var items = new List<string>();

                    for (var i = 0; i < count; i++)
                    {
                        items.Add(reader.ReadString());
                    }

                    return header;
                }
            }
        }

        public void Save(string fileName)
        {
            var header = new TabsterBinaryFileHeader(HeaderVersion, false);

            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    WriteHeader(writer, HeaderString, header);
                    WriteFileAttributes(writer, FileAttributes);

                    foreach (var file in _files)
                    {
                        writer.Write(file.FileInfo.FullName);
                    }
                }
            }
        }

        public ITabsterFileHeader GetHeader()
        {
            using (var fs = new FileStream(FileInfo.FullName, FileMode.Open))
            {
                using (var reader = new BinaryReader(fs))
                {
                    return ReadHeader(reader);
                }
            }
        }

        public TabsterFileAttributes FileAttributes { get; set; }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<ITablatureFile> GetEnumerator()
        {
            return _files.Cast<ITablatureFile>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<ITabsterFile>

        public void Add(ITablatureFile file)
        {
            _files.Add(file);
        }

        public void Clear()
        {
            _files.Clear();
        }

        public bool Contains(ITablatureFile file)
        {
            return _files.Contains(file);
        }

        public void CopyTo(ITablatureFile[] array, int arrayIndex)
        {
            _files.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITablatureFile file)
        {
            return _files.Contains(file);
        }

        public int Count
        {
            get { return _files.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region Implementation of ITablaturePlaylist

        public string Name { get; set; }

        public bool Contains(string fileName)
        {
            return _files.Find(x => x.FileInfo.FullName.Equals(fileName, StringComparison.OrdinalIgnoreCase)) != null;
        }

        #endregion
    }
}