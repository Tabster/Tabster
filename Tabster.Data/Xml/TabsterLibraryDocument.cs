#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Tabster.Data.Library;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Data.Xml
{
    internal class TabsterLibraryDocument : ITabsterFile
    {
        private const string RootNode = "library";
        public static readonly Version FileVersion = new Version("1.0");

        private readonly List<PlaylistLibraryItem<TablaturePlaylistDocument>> _playlistItems = new List<PlaylistLibraryItem<TablaturePlaylistDocument>>();
        private readonly List<TablatureLibraryItem<TablatureDocument>> _tablatureItems = new List<TablatureLibraryItem<TablatureDocument>>();

        private readonly TabsterFileProcessor<TablatureDocument> _tablatureProcessor = new TabsterFileProcessor<TablatureDocument>(FileVersion);

        public ReadOnlyCollection<PlaylistLibraryItem<TablaturePlaylistDocument>> PlaylistItems
        {
            get { return _playlistItems.AsReadOnly(); }
        }

        public ReadOnlyCollection<TablatureLibraryItem<TablatureDocument>> TablatureItems
        {
            get { return _tablatureItems.AsReadOnly(); }
        } 

        #region Implementation of ITabsterFile

        public void Load(string fileName)
        {
            var fi = new FileInfo(fileName);

            var xmlDoc = new TabsterXmlDocument(RootNode);
            xmlDoc.Load(fileName);

            FileHeader = new TabsterXmlFileHeader(xmlDoc.Version);
            FileAttributes = new TabsterFileAttributes(fi.CreationTime);

            var tabNodes = xmlDoc.ReadChildNodes("tabs");

            foreach (var tabNode in tabNodes)
            {
                var path = tabNode.InnerText;

                if (File.Exists(path))
                {
                    var doc = _tablatureProcessor.Load(path);

                    if (doc != null)
                    {
                        var item = new TablatureLibraryItem<TablatureDocument>(doc, new FileInfo(path))
                        {
                            Favorited = bool.Parse(tabNode.GetElementAttributeValue("favorite", "false")),
                            Views = int.Parse(tabNode.GetElementAttributeValue("views", "0"))
                        };
                        _tablatureItems.Add(item);
                    }
                }
            }

            var playlistNodes = xmlDoc.ReadChildNodes("tabs");

            foreach (var playlistNode in playlistNodes)
            {
                var path = playlistNode.InnerText;

                if (File.Exists(path))
                {
                    var doc = _tablatureProcessor.Load(path);

                    if (doc != null)
                    {
                        _tablatureItems.Add(new TablatureLibraryItem<TablatureDocument>(doc, new FileInfo(path)));
                    }
                }
            }
        }

        public void Save(string fileName)
        {
            var xmlDoc = new TabsterXmlDocument(RootNode) {Version = FileVersion};

            xmlDoc.WriteNode("tabs");
            foreach (var item in _tablatureItems)
            {
                xmlDoc.WriteNode("tab", item.FileInfo.FullName, "tabs", new SortedDictionary<string, string>()
                {
                    {"favorite", item.Favorited.ToString()},
                    {"views", item.Views.ToString()}
                });
            }

            xmlDoc.WriteNode("playlists");
            foreach (var item in _playlistItems)
            {
                xmlDoc.WriteNode("playlist", item.FileInfo.FullName, "playlists");
            }

            xmlDoc.Save(fileName);

            FileHeader = new TabsterXmlFileHeader(FileVersion);
            FileAttributes = new TabsterFileAttributes(DateTime.UtcNow);
        }

        public TabsterFileAttributes FileAttributes { get; private set; }
        public ITabsterFileHeader FileHeader { get; private set; }

        #endregion
    }
}