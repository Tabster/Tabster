#region

using System;
using System.ComponentModel;
using System.IO;
using System.Xml;

#endregion

namespace Tabster
{
    public class LibraryItem
    {
        public TabFile TabFile { get; private set; }
        public int Views { get; private set; }
        public bool Favorited { get; private set; }

        public LibraryItem(TabFile tabfile, int views, bool favorited)
        {
            TabFile = tabfile;
            Views = views;
            Favorited = favorited;
        }
    }

    public class LibraryManager
    {
        private readonly BackgroundWorker playlistWorker = new BackgroundWorker();
        private readonly BackgroundWorker tabWorker = new BackgroundWorker();
        public PlaylistFileCollection Playlists = new PlaylistFileCollection();
        public TabFileCollection Tabs = new TabFileCollection();

        public bool TabsLoaded { get; private set; }
        public bool PlaylistsLoaded { get; private set; }

        public LibraryManager()
        {
            tabWorker.DoWork += tabWorker_DoWork;
            tabWorker.RunWorkerCompleted += tabWorker_RunWorkerCompleted;
            playlistWorker.RunWorkerCompleted += playlistWorker_RunWorkerCompleted;
            playlistWorker.DoWork += playlistWorker_DoWork;
        }

        #region Background Workers

        private void tabWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!File.Exists(Global.CachePath))
            {
                BuildCache();
            }

            var doc = new XmlDocument();
            doc.Load(Global.CachePath);

            Tabs.Clear();

            var itemsNode = doc.GetElementsByTagName("items");

            if (itemsNode.Count > 0)
            {
                foreach (XmlNode item in itemsNode[0].ChildNodes)
                {

                    if (File.Exists(item.InnerText))
                    {
                        TabFile tabFile;

                        if (TabFile.TryParse(item.InnerText, out tabFile))
                        {
                            Tabs.Add(tabFile);
                        }
                    }
                }
            }
        }

        private void tabWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TabsLoaded = e.Error == null;
        }

        private void playlistWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Playlists.Clear();

            var fileListing = new DirectoryInfo(Global.PlaylistDirectory);
            foreach (var file in fileListing.GetFiles(string.Format("*{0}", PlaylistFile.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
            {
                PlaylistFile p;
                if (PlaylistFile.TryParse(file.FullName, out p))
                {
                    Playlists.Add(p);
                }
            }
        }

        private void playlistWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PlaylistsLoaded = e.Error == null;
        }

        #endregion

        #region Active Directory

        public FileInfo[] GetTabFiles()
        {
            return new DirectoryInfo(Global.LibraryDirectory).GetFiles("*" + TabFile.FILE_EXTENSION, SearchOption.AllDirectories);
        }

        public FileInfo[] GetPlaylistfiles()
        {
            return new DirectoryInfo(Global.PlaylistDirectory).GetFiles("*" + PlaylistFile.FILE_EXTENSION, SearchOption.TopDirectoryOnly);
        }

        #endregion

        #region Cache

        /// <summary>
        ///   Builds the cache file by scanning the tab directory.
        /// </summary>
        public void BuildCache()
        {
            var doc = new XmlDocument();

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "ISO-8859-1", null));

            XmlNode xmlRoot = doc.CreateElement("library");
            var versionAttribute = doc.CreateAttribute("version");
            versionAttribute.Value = Constants.LibraryFormatVersion;
            xmlRoot.Attributes.Append(versionAttribute);
            doc.AppendChild(xmlRoot);

            var tabsNode = doc.CreateElement("tabs");
            xmlRoot.AppendChild(tabsNode);

            foreach (var file in GetTabFiles())
            {
                TabFile tabFile;
                if (file.Length > 0 && TabFile.TryParse(file.FullName, out tabFile))
                {
                    var itemNode = doc.CreateElement("tab");
                    itemNode.InnerText = tabFile.FileInfo.FullName;
                    tabsNode.AppendChild(itemNode);
                }
            }

            var playlistsnode = doc.CreateElement("playlists");
            xmlRoot.AppendChild(playlistsnode);

            foreach (var file in GetPlaylistfiles())
            {
                PlaylistFile playlistFile;
                if (file.Length > 0 && PlaylistFile.TryParse(file.FullName, out playlistFile))
                {
                    var itemNode = doc.CreateElement("playlist");
                    itemNode.InnerText = playlistFile.FileInfo.FullName;
                    playlistsnode.AppendChild(itemNode);
                }
            }

            doc.Save(Global.CachePath);
        }

        public void SaveCache()
        {
            var doc = new XmlDocument();

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "ISO-8859-1", null));

            XmlNode xmlRoot = doc.CreateElement("library");
            var versionAttribute = doc.CreateAttribute("version");
            versionAttribute.Value = Constants.LibraryFormatVersion;
            xmlRoot.Attributes.Append(versionAttribute);
            doc.AppendChild(xmlRoot);

            var tabsNode = doc.CreateElement("tabs");
            xmlRoot.AppendChild(tabsNode);

            foreach (var tab in Tabs)
            {
                if (File.Exists(tab.FileInfo.FullName))
                {
                    var tabNode = doc.CreateElement("tab");
                    tabNode.InnerText = tab.FileInfo.FullName;
                    /*
                    tab.FileInfo.Refresh();
                    
                    var songAtt = doc.CreateAttribute("song");
                    songAtt.Value = tab.Song;
                    tabNode.Attributes.Append(songAtt);

                    var artistAtt = doc.CreateAttribute("artist");
                    artistAtt.Value = tab.Artist;
                    tabNode.Attributes.Append(artistAtt);

                    var addedAtt = doc.CreateAttribute("added");
                    addedAtt.Value = DateTime.Now.ToString();
                    tabNode.Attributes.Append(addedAtt);

                    var modifiedAttribute = doc.CreateAttribute("modified");
                    modifiedAttribute.Value = tab.FileInfo.LastWriteTime.ToString();
                    tabNode.Attributes.Append(modifiedAttribute);*/

                    tabsNode.AppendChild(tabNode);
                }
            }

            var playlistsNode = doc.CreateElement("playlists");
            xmlRoot.AppendChild(playlistsNode);

            foreach (var playlist in Playlists)
            {
                if (File.Exists(playlist.FileInfo.FullName))
                {
                    var playlistNode = doc.CreateElement("playlist");
                    playlistNode.InnerText = playlist.FileInfo.FullName;
                    playlistsNode.AppendChild(playlistNode);
                }
            }

            doc.Save(Global.CachePath);
        }

        #endregion

        public void LoadTabs()
        {
            if (!tabWorker.IsBusy)
                tabWorker.RunWorkerAsync();
        }

        public void LoadPlaylists()
        {
            if (!playlistWorker.IsBusy)
                playlistWorker.RunWorkerAsync();
        }

        public TabFile CreateTempFile(string artist, string title, TabType type, string contents)
        {
            var t = new Tab(artist, title, type, contents);
            var guid = Guid.NewGuid();
            var path = Path.Combine(Global.TempDirectory, guid.ToString() + TabFile.FILE_EXTENSION);
            var tabFile = new TabFile(t, path);
            return tabFile;
        }

        public void CleanupTempFiles()
        {
            try
            {
                var files = new DirectoryInfo(Global.TempDirectory);
                foreach (var f in files.GetFiles(string.Format("*.{0}", TabFile.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
                {
                    f.Delete();
                }
            }

            catch
            {
            }
        }

        public void Cleanup()
        {
            try
            {
                //organize contents based on their type.
                var globallisting = new DirectoryInfo(Global.UserDirectory);

                //move tabs to tab directory
                foreach (var file in globallisting.GetFiles(string.Format("*{0}", TabFile.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
                {
                    file.MoveTo(Global.LibraryDirectory + file.Name);
                }

                //move playlists to playlist directory
                foreach (var file in globallisting.GetFiles(string.Format("*{0}", PlaylistFile.FILE_EXTENSION), SearchOption.TopDirectoryOnly))
                {
                    file.MoveTo(Global.PlaylistDirectory + file.Name);
                }
            }

            catch
            {
            }
        }
    }
}