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
    public class TablaturePlaylistDocument : ICollection<TablatureDocument>, ITabsterDocument
    {
        #region Constants

        public const string FILE_EXTENSION = ".tablist";
        public static readonly Version FILE_VERSION = new Version("1.0");

        #endregion

        private readonly TabsterXmlDocument _doc = new TabsterXmlDocument("tablist");
        private readonly List<TablatureDocument> _documents = new List<TablatureDocument>();
        private readonly TabsterDocumentProcessor<TablatureDocument> _processor = new TabsterDocumentProcessor<TablatureDocument>(FILE_VERSION, true);

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

        #region Implementation of ITabsterDocument

        public FileInfo FileInfo { get; private set; }

        public Version FileVersion
        {
            get { return _doc.Version; }
        }

        public void Load(string filename)
        {
            FileInfo = new FileInfo(filename);

            _doc.Load(filename);

            Name = _doc.TryReadNodeValue("name", string.Empty);
            var files = _doc.ReadChildNodeValues("files");

            foreach (var doc in files.Where(File.Exists).Select(file => _processor.Load(file)).Where(doc => doc != null))
            {
                Add(doc);
            }
        }

        public void Save()
        {
            Save(FileInfo.FullName);
            FileInfo.Refresh();
        }

        public void Update()
        {
            if (FileVersion != FILE_VERSION)
                Save();
        }

        public void Save(string fileName)
        {
            _doc.Version = FILE_VERSION;
            _doc.WriteNode("name", Name);
            _doc.WriteNode("files");

            foreach (var tab in this.Where(tab => File.Exists(tab.FileInfo.FullName)))
            {
                _doc.WriteNode("file", tab.FileInfo.FullName, "files");
            }

            _doc.Save(fileName);

            if (FileInfo == null)
                FileInfo = new FileInfo(fileName);
        }

        #endregion

        #region Implementation of IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TablatureDocument> GetEnumerator()
        {
            return ((IEnumerable<TablatureDocument>) _documents).GetEnumerator();
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

        public void Add(TablatureDocument item)
        {
            Remove(item);
            _documents.Add(item);
        }

        public void Clear()
        {
            _documents.Clear();
        }

        public bool Contains(TablatureDocument item)
        {
            return _documents.Contains(item);
        }

        public void CopyTo(TablatureDocument[] array, int arrayIndex)
        {
            _documents.CopyTo(array, arrayIndex);
        }

        public bool Remove(TablatureDocument item)
        {
            return _documents.Remove(item);
        }

        #endregion

        public bool Contains(string path)
        {
            return Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase)) != null;
        }

        public TablatureDocument Find(Predicate<TablatureDocument> match)
        {
            return _documents.Find(match);
        }
    }
}