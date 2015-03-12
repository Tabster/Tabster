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

        public const string FILE_EXTENSION = ".tablist";
        public static readonly Version FILE_VERSION = new Version("1.0");

        #endregion

        private const string ROOT_NODE = "tablist";
        private readonly List<ITabsterFile> _documents = new List<ITabsterFile>();
        private readonly TabsterFileProcessor<TablatureDocument> _processor = new TabsterFileProcessor<TablatureDocument>(FILE_VERSION);

        #region Constructors

        public TablaturePlaylistDocument()
        {
        }

        public TablaturePlaylistDocument(string name)
        {
            Name = name;
        }

        #endregion

        public string Name { get; set; }

        public bool Contains(string path)
        {
            return Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase)) != null;
        }

        #region Implementation of ITabsterDocument

        public FileInfo FileInfo { get; private set; }

        public void Save(string fileName)
        {
            var doc = new TabsterXmlDocument(ROOT_NODE) {Version = FILE_VERSION};
            doc.WriteNode("name", Name);
            doc.WriteNode("files");

            foreach (var tab in this.Where(tab => File.Exists(tab.FileInfo.FullName)))
            {
                doc.WriteNode("file", tab.FileInfo.FullName, "files");
            }

            doc.Save(fileName);

            if (FileInfo == null)
                FileInfo = new FileInfo(fileName);
        }

        public ITabsterFileHeader GetHeader()
        {
            var doc = new TabsterXmlDocument(ROOT_NODE);
            doc.Load(FileInfo.FullName);
            return new TabsterXmlFileHeader(doc.Version);
        }

        public TabsterFileAttributes FileAttributes { get; private set; }

        public ITabsterFileHeader Load(string filename)
        {
            Clear();

            FileInfo = new FileInfo(filename);

            var doc = new TabsterXmlDocument(ROOT_NODE);
            doc.Load(filename);

            Name = doc.TryReadNodeValue("name");
            if (string.IsNullOrEmpty(Name))
                throw new TabsterFileException("Missing playlist name");

            //playlist format never had created property, use filesystem
            FileAttributes = new TabsterFileAttributes(FileInfo.CreationTime);

            var files = doc.ReadChildNodeValues("files");

            foreach (var d in files.Where(File.Exists).Select(file => _processor.Load(file)).Where(d => d != null))
            {
                Add(d);
            }

            return new TabsterXmlFileHeader(doc.Version);
        }

        public void Save()
        {
            Save(FileInfo.FullName);
            FileInfo.Refresh();
        }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<ITablatureFile> GetEnumerator()
        {
            return _documents.Cast<ITablatureFile>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<TablatureDocument>

        public int Count
        {
            get { return _documents.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(ITablatureFile file)
        {
            Remove(file);
            _documents.Add(file);
        }

        public void Clear()
        {
            _documents.Clear();
        }

        public bool Contains(ITablatureFile file)
        {
            return _documents.Contains(file);
        }

        public void CopyTo(ITablatureFile[] array, int arrayIndex)
        {
            _documents.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITablatureFile file)
        {
            return _documents.Remove(file);
        }

        #endregion

        public ITabsterFile Find(Predicate<ITabsterFile> match)
        {
            return _documents.Find(match);
        }
    }
}