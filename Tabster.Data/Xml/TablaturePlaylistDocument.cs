#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Data.Xml
{
    [Obsolete]
    public class TablaturePlaylistDocument : ITablaturePlaylistFile
    {
        #region Constants

        public const string FileExtension = ".tablist";
        public static readonly Version FileVersion = new Version("1.0");

        #endregion

        private const string RootNode = "tablist";
        private readonly SortedDictionary<ITablatureFile, FileInfo> _documentMap = new SortedDictionary<ITablatureFile, FileInfo>();
        private readonly List<ITablatureFile> _documents = new List<ITablatureFile>();
        private readonly TabsterFileProcessor<TablatureDocument> _processor = new TabsterFileProcessor<TablatureDocument>(FileVersion);

        public string Name { get; set; }

        public bool Contains(ITablatureFile file)
        {
            return _documents.Contains(file);
        }

        public void Add(ITablatureFile file, FileInfo fileInfo)
        {
            if (!_documentMap.ContainsKey(file))
                _documents.Add(file);

            _documentMap[file] = fileInfo;
        }

        public bool Remove(ITablatureFile file)
        {
            var dictResult = _documentMap.Remove(file);
            var listResult = _documents.Remove(file);
            return dictResult && listResult;
        }

        public void Clear()
        {
            _documentMap.Clear();
            _documents.Clear();
        }

        public ITablatureFile Find(Predicate<ITablatureFile> match)
        {
            return _documents.Find(match);
        }

        public FileInfo GetFileInfo(ITablatureFile file)
        {
            return _documentMap.ContainsKey(file) ? _documentMap[file] : null;
        }

        public bool Contains(string filePath)
        {
            return _documentMap.Any(x => x.Value.FullName.Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }

        #region Implementation of ITabsterFile

        public void Save(string fileName)
        {
            var xmlDoc = new TabsterXmlDocument(RootNode) {Version = FileVersion};
            xmlDoc.WriteNode("name", Name);
            xmlDoc.WriteNode("files");

            foreach (var path in _documents.Select(doc => _documentMap[doc].FullName))
            {
                xmlDoc.WriteNode("file", path, "files");
            }

            xmlDoc.Save(fileName);
        }

        public TabsterFileAttributes FileAttributes { get; private set; }
        public ITabsterFileHeader FileHeader { get; private set; }

        public void Load(string filename)
        {
            Clear();

            var fi = new FileInfo(filename);

            var xmlDoc = new TabsterXmlDocument(RootNode);
            xmlDoc.Load(filename);

            FileHeader = new TabsterXmlFileHeader(xmlDoc.Version);

            Name = xmlDoc.TryReadNodeValue("name");
            if (string.IsNullOrEmpty(Name))
                throw new TabsterFileException("Missing playlist name");

            //playlist format never had created property, use filesystem
            FileAttributes = new TabsterFileAttributes(fi.CreationTime);

            var files = xmlDoc.ReadChildNodeValues("files");

            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    var doc = _processor.Load(file);

                    if (doc != null)
                    {
                        Add(doc, new FileInfo(file));
                    }
                }
            }
        }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<ITablatureFile> GetEnumerator()
        {
            return ((IEnumerable<ITablatureFile>) _documents).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}