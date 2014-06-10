#region

using System;
using System.IO;
using System.Xml;

#endregion

namespace Tabster.Core
{
    public class PlaylistFile : TabsterFile, ITabsterFile
    {
        public const string FILE_EXTENSION = ".tablist";
        public const string FILE_VERSION = "1.0";

        public PlaylistFile(string filePath, bool autoLoad)
        {
            FileInfo = new FileInfo(filePath);

            if (autoLoad)
                Load();
        }

        public PlaylistFile(Playlist playlist, string filePath)
        {
            PlaylistData = playlist;
            FileInfo = new FileInfo(filePath);
            Save(filePath);
            FileInfo.Refresh();
        }

        public Playlist PlaylistData { get; private set; }

        #region ITabsterFile Members

        public void Load()
        {
            BeginFileRead(new Version(FILE_VERSION));

            var name = ReadNodeValue("name");
            var files = ReadChildValues("files");

            PlaylistData = new Playlist(name);

            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    TabFile tab;
                    if (TabFile.TryParse(file, out tab))
                    {
                        PlaylistData.Add(tab);
                    }
                }
            }

            if (FileFormatOutdated)
            {
                Save();
                Load();
            }
        }

        public void Save()
        {
            BeginFileWrite("tablist", FILE_VERSION);
            WriteNode("name", PlaylistData.Name);
            var files = WriteNode("files");

            foreach (var tab in PlaylistData)
            {
                if (File.Exists(tab.FileInfo.FullName))
                {
                    WriteNode("file", tab.FileInfo.FullName, files, overwriteDuplicates: false);
                }
            }

            FinishFileWrite();
        }

        #endregion

        #region Static Methods

        public static PlaylistFile Create(Playlist playlist, string directory)
        {
            var filePath = GenerateUniqueFilename(directory, string.Format("{0}{1}", playlist.Name, FILE_EXTENSION));
            var playlistFile = new PlaylistFile(filePath, false) {PlaylistData = playlist, FileInfo = new FileInfo(filePath)};
            playlistFile.Save();
            return playlistFile;
        }

        public static bool TryParse(string filePath, out PlaylistFile playlistfile)
        {
            var success = false;
            PlaylistFile pf = null;

            try
            {
                pf = new PlaylistFile(filePath, true);
                success = true;
            }

            catch (FileNotFoundException)
            {
            }

            catch (IOException)
            {
            }

            catch (XmlException)
            {
            }

            playlistfile = pf;
            return success;
        }

        #endregion
    }
}