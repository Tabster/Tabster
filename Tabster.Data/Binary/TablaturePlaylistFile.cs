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

        private readonly SortedDictionary<ITablatureFile, FileInfo> _fileMap = new SortedDictionary<ITablatureFile, FileInfo>();
        private readonly List<ITablatureFile> _files = new List<ITablatureFile>();
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

                    var files = new List<TablatureFile>();

                    for (var i = 0; i < count; i++)
                    {
                        var path = reader.ReadString();

                        if (File.Exists(path))
                        {
                            var file = _processor.Load(path);

                            if (file != null)
                            {
                                files.Add(file);
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
                    writer.Write(_files.Count);

                    foreach (var path in _files.Select(doc => _fileMap[doc].FullName))
                    {
                        writer.Write(path);
                    }
                }
            }
        }

        public TabsterFileAttributes FileAttributes { get; set; }
        public ITabsterFileHeader FileHeader { get; set; }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<ITablatureFile> GetEnumerator()
        {
            return ((IEnumerable<ITablatureFile>) _files).GetEnumerator();
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
            return _fileMap.Any(x => x.Value.FullName.Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }

        public bool Contains(ITablatureFile file)
        {
            return _files.Contains(file);
        }

        public void Add(ITablatureFile file, FileInfo fileInfo)
        {
            if (!_fileMap.ContainsKey(file))
                _files.Add(file);

            _fileMap[file] = fileInfo;
        }

        public bool Remove(ITablatureFile file)
        {
            var dictResult = _fileMap.Remove(file);
            var listResult = _files.Remove(file);
            return dictResult && listResult;
        }

        public void Clear()
        {
            _fileMap.Clear();
            _files.Clear();
        }

        public ITablatureFile Find(Predicate<ITablatureFile> match)
        {
            throw new NotImplementedException();
        }

        public FileInfo GetFileInfo(ITablatureFile file)
        {
            return _fileMap.ContainsKey(file) ? _fileMap[file] : null;
        }

        #endregion
    }
}