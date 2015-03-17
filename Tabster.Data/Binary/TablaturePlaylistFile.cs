#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Data.Binary
{
    public class TablaturePlaylistFile : TabsterBinaryFileBase, ITablaturePlaylistFile
    {
        private const string HeaderString = "TABSTER";
        private static readonly Version HeaderVersion = new Version("1.0");

        private readonly List<TablaturePlaylistItem> _items = new List<TablaturePlaylistItem>();
        private readonly TabsterFileProcessor<TablatureFile> _processor = new TabsterFileProcessor<TablatureFile>(Constants.TablatureFileVersion);

        #region Constructors

        public TablaturePlaylistFile() : base(HeaderString)
        {
        }

        #endregion

        #region Implementation of ITabsterFile

        public void Load(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(fs))
                {
                    FileHeader = ReadHeader(reader);

                    var created = new DateTime(reader.ReadInt64());
                    FileAttributes = new TabsterFileAttributes(created);

                    var count = reader.ReadInt32();

                    for (var i = 0; i < count; i++)
                    {
                        var path = reader.ReadString();

                        if (File.Exists(path))
                        {
                            var file = _processor.Load(path);

                            if (file != null)
                            {
                                _items.Add(new TablaturePlaylistItem(file, new FileInfo(path)));
                            }
                        }
                    }
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
                    WriteFileAttributes(writer, FileAttributes ?? new TabsterFileAttributes(DateTime.Now));

                    writer.Write(Name);
                    writer.Write(_items.Count);

                    foreach (var item in _items)
                    {
                        writer.Write(item.FileInfo.FullName);
                    }
                }
            }
        }

        public TabsterFileAttributes FileAttributes { get; set; }
        public ITabsterFileHeader FileHeader { get; set; }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<TablaturePlaylistItem> GetEnumerator()
        {
            return ((IEnumerable<TablaturePlaylistItem>)_items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ITablaturePlaylistFile

        public string Name { get; set; }

        public bool Contains(string filePath)
        {
            return _items.Any(x => x.FileInfo.FullName.Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }

        public bool Contains(TablaturePlaylistItem item)
        {
            return _items.Contains(item);
        }

        public void Add(TablaturePlaylistItem item)
        {
            _items.Add(item);
        }

        public bool Remove(TablaturePlaylistItem item)
        {
            return _items.Remove(item);
        }

        public bool Remove(ITablatureFile file)
        {
            return _items.RemoveAll(x => x.File.Equals(file)) > 0;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public TablaturePlaylistItem Find(Predicate<TablaturePlaylistItem> match)
        {
            return _items.Find(match);
        }

        #endregion
    }
}