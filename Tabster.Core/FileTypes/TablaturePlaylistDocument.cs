#region

using System;
using System.IO;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.FileTypes
{
    public class TablaturePlaylistDocument : TabsterDocumentCollection<TablatureDocument>, ITablaturePlaylist, ITabsterDocument
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

            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    var doc = _processor.Load(file);

                    if (doc != null)
                    {
                        Add(doc);
                    }
                }
            }
        }

        public void Save()
        {
            Save(FileInfo.FullName);
        }

        public void Save(string fileName)
        {
            _doc.Version = FILE_VERSION;
            _doc.WriteNode("name", Name);
            _doc.WriteNode("files");

            foreach (var tab in this)
            {
                if (File.Exists(tab.FileInfo.FullName))
                {
                    _doc.WriteNode("file", tab.FileInfo.FullName, "files");
                }
            }

            _doc.Save(fileName);

            FileInfo = new FileInfo(fileName);
        }

        public void Update()
        {
            if (FileVersion != FILE_VERSION)
                Save();
        }

        #endregion

        #region Implementation of IEquatable<ITabsterDocument>

        public bool Equals(ITabsterDocument other)
        {
            return Equals((object)other);
        }

        public override bool Equals(object other)
        {
            var doc = other as TablaturePlaylistDocument;
            return doc != null && FileInfo.FullName.Equals(doc.FileInfo.FullName, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}