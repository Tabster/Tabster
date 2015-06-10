#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
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
        private static readonly Encoding DefaultEncoding = Encoding.GetEncoding("ISO-8859-1");
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

        public bool Contains(string filePath)
        {
            return _items.Any(x => x.FileInfo.FullName.Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }

        #region Implementation of ITabsterFile

        public void Save(string fileName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", DefaultEncoding.EncodingName, null));
            var rootNode = xmlDoc.CreateElement(RootNode);
            xmlDoc.AppendChild(rootNode);

            xmlDoc.SetAttributeValue(rootNode, "version", FileVersion.ToString());

            xmlDoc.WriteNode("name", Name);
            xmlDoc.WriteNode("files");

            foreach (var item in _items)
            {
                xmlDoc.WriteNode("file", item.FileInfo.FullName, "files");
            }

            xmlDoc.Save(fileName);

            FileHeader = new TabsterFileHeader(FileVersion, CompressionMode.None);
            FileAttributes = new TabsterFileAttributes(DateTime.UtcNow, FileAttributes != null ? FileAttributes.Encoding : DefaultEncoding);
        }

        public TabsterFileAttributes FileAttributes { get; private set; }
        public TabsterFileHeader FileHeader { get; private set; }

        public void Load(string fileName)
        {
            Clear();

            var fi = new FileInfo(fileName);

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.GetElementByTagName(RootNode);

            FileHeader = new TabsterFileHeader(new Version(rootNode.GetAttributeValue("version")), CompressionMode.None);

            Name = xmlDoc.GetNodeValue("name");
            if (string.IsNullOrEmpty(Name))
                throw new TabsterFileException("Missing playlist name");

            //playlist format never had created property, use filesystem
            FileAttributes = new TabsterFileAttributes(fi.CreationTime, Encoding.GetEncoding(xmlDoc.GetXmlDeclaration().Encoding));

            var fileNodes = xmlDoc.GetChildNodes(xmlDoc.GetElementByTagName("files"));

            foreach (var fileNode in fileNodes)
            {
                var path = fileNode.InnerText;

                if (File.Exists(path))
                {
                    var doc = _processor.Load(path);

                    if (doc != null)
                    {
                        _items.Add(new TablaturePlaylistItem(doc, new FileInfo(path)));
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