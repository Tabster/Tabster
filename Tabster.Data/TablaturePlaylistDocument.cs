#region

using System;
using System.IO;
using System.Linq;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Data
{
    public class TablaturePlaylistDocument : TabsterDocumentCollection<TablatureDocument>, ITabsterDocument
    {
        #region Constants

        public const string FILE_EXTENSION = ".tablist";
        public static readonly Version FILE_VERSION = new Version("1.0");

        #endregion

        private readonly TabsterXmlDocument _doc = new TabsterXmlDocument("tablist");
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

        #region Implementation of ITablaturePlaylist

        public string Name { get; set; }

        #endregion

        #region Implementation of ITabsterDocument

        public Version FileVersion
        {
            get { return _doc.Version; }
        }

        public FileInfo FileInfo { get; private set; }

        public void Load(string filename)
        {
            FileInfo = new FileInfo(filename);

            _doc.Load(filename);

            Name = _doc.TryReadNodeValue("name", string.Empty);
            var files = _doc.ReadChildNodeValues("files");

            foreach (var doc in files.Select(file => _processor.Load(file)).Where(doc => doc != null))
            {
                Add(doc);
            }
        }

        public void Save()
        {
            Save(FileInfo.FullName);
            FileInfo.Refresh();
        }

        public ITabsterDocument SaveAs(string fileName)
        {
            Save(fileName);
            var doc = new TablaturePlaylistDocument();
            doc.Load(fileName);
            return doc;
        }

        public void Update()
        {
            if (FileVersion != FILE_VERSION)
                Save();
        }

        private void Save(string fileName)
        {
            _doc.Version = FILE_VERSION;
            _doc.WriteNode("name", Name);
            _doc.WriteNode("files");

            foreach (var tab in this.Where(tab => File.Exists(tab.FileInfo.FullName)))
            {
                _doc.WriteNode("file", tab.FileInfo.FullName, "files");
            }

            _doc.Save(fileName);
        }

        #endregion
    }
}