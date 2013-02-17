#region

using System;
using System.IO;

#endregion

namespace Tabster
{
    public class PlaylistFile : TabsterFile, ITabsterFile
    {
        public const string FILE_EXTENSION = ".tablist";
        public const string FILE_VERSION = "1.0";

        public PlaylistFile(string filePath)
        {
            FileInfo = new FileInfo(filePath);
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

            if (FileFormatOutdated)
            {
                //todo update format    
            }

            var name = ReadNodeValue("name");

            var files = ReadChildValues("files");

            PlaylistData = new Playlist(name);

            foreach (var file in files)
            {
                TabFile tab;
                if (TabFile.TryParse(file, out tab))
                {
                    PlaylistData.Add(tab);
                }

            }
        }

        public void Save()
        {
            BeginFileWrite("tablist", FILE_VERSION);
            WriteNode("name", PlaylistData.Name);
            var files = WriteNode("files");

            foreach (var tab in PlaylistData)
            {
                WriteNode("file", tab.FileInfo.FullName, files);
            }

            FinishFileWrite();
        }

        #endregion

        public void Rename(string newname, bool renameFile)
        {
            PlaylistData.Name = newname;
            Save();

            if (renameFile)
            {
                RenameFile(newname);
                Load();
            }
        }

        public static bool TryParse(string filePath, out PlaylistFile p)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    p = null;
                    return false;
                }

                var fi = new FileInfo(filePath);

                if (fi.Length > 0)
                {
                    p = new PlaylistFile(filePath);
                    return true;
                }
            }

            catch (Exception)
            {
                p = null;
                return false;
            }

            p = null;
            return false;
        }
    }
}