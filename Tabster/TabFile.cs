#region

using System;
using System.IO;

#endregion

namespace Tabster
{
    public enum ExportFormat
    {
        Tabster,
        Text
    }

    public class TabFile : TabsterFile, ITabsterFile
    {
        public const string FILE_EXTENSION = ".tabster";
        public const string FILE_VERSION = "1.4";

        public TabFile(string filePath)
        {
            FileInfo = new FileInfo(filePath);
            Load();
        }

        public TabFile(Tab tab, string filePath)
        {
            TabData = tab;
            FileInfo = new FileInfo(filePath);
            Save(filePath);
            FileInfo.Refresh();
        }

        public Tab TabData { get; private set; }

        public void Export(ExportFormat format, string filePath)
        {
            if (format == ExportFormat.Tabster)
            {
                File.Copy(FileInfo.FullName, filePath);
            }

            if (format == ExportFormat.Text)
            {
                File.WriteAllText(filePath, TabData.Contents);
            }
        }

        #region Static Methods

        public static TabFile Create(string artist, string song, TabType type, string contents, string filePath)
        {
            var tab = new Tab(artist, song, type, contents);
            var tabFile = new TabFile(tab, filePath);
            return tabFile;
        }

        public static TabFile Import(string artist, string song, TabType type, string filePath)
        {
            var contents = File.ReadAllText(filePath);
            return Create(artist, song, type, contents, filePath);
        }

        public static bool TryParse(string filePath, out TabFile tabFile)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    tabFile = null;
                    return false;
                }

                tabFile = new TabFile(filePath);
                return true;
            }

            catch
            {
                tabFile = null;
                return false;
            }
        }

        #endregion

        #region Implementation of ITabsterFile

        public void Load()
        {
            BeginFileRead(new Version(FILE_VERSION));

            var titleValue = ReadNodeValue("song", true) ?? ReadNodeValue("title");
            var artistValue = ReadNodeValue("artist");
            var typeValue = Tab.GetTabType(ReadNodeValue("type"));
            var contentsValue = ReadNodeValue("tab");
            var sourceValue = ReadNodeValue("source");
            var sourceType = Tab.GetTabSource(sourceValue);
            var remoteSourceValue = sourceType == TabSource.Download && Uri.IsWellFormedUriString(sourceValue, UriKind.Absolute) ? new Uri(sourceValue) : null;
            var audioValue = ReadNodeValue("audio");
            var lyricsValue = ReadNodeValue("lyrics");

            if (FileFormatOutdated)
            {
                //todo update format    
            }

            TabData = new Tab(artistValue, titleValue, typeValue, contentsValue);
        }

        public void Save()
        {
            Save(FileInfo.FullName);
        }

        public void Save(string filePath)
        {
            BeginFileWrite("tabster", FILE_VERSION);
            WriteNode("title", TabData.Title);
            WriteNode("artist", TabData.Artist);
            WriteNode("type", Tab.GetTabString(TabData.Type));
            WriteNode("tab", TabData.Contents);
            WriteNode("comment", TabData.Comment);
            WriteNode("lyrics", TabData.Lyrics);
            WriteNode("audio", TabData.Audio);
            FinishFileWrite();
            FileInfo.Refresh();

            base.Save();
        }

        #endregion
    }
}