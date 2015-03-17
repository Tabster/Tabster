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
        private readonly List<TablaturePlaylistItem> _items = new List<TablaturePlaylistItem>();
        private readonly TabsterFileProcessor<TablatureDocument> _processor = new TabsterFileProcessor<TablatureDocument>(FileVersion);

        public string Name { get; set; }

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

        public void Clear()
        {
            _items.Clear();
        }

        public TablaturePlaylistItem Find(Predicate<TablaturePlaylistItem> match)
        {
            return _items.Find(match);
        }

        public bool Contains(string filePath)
        {
            return _items.Any(x => x.FileInfo.FullName.Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }

        #region Implementation of ITabsterFile

        public void Save(string fileName)
        {
            var xmlDoc = new TabsterXmlDocument(RootNode) {Version = FileVersion};
            xmlDoc.WriteNode("name", Name);
            xmlDoc.WriteNode("files");

            foreach (var item in _items)
            {
                xmlDoc.WriteNode("file", item.FileInfo.FullName, "files");
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
                       _items.Add(new TablaturePlaylistItem(doc, new FileInfo(file)));
                    }
                }
            }
        }

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
    }
}