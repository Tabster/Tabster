#region

using System;
using System.IO;
using System.Xml;

#endregion

namespace Tabster.Core
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

        public TabFile(string filePath, bool autoLoad)
        {
            FileInfo = new FileInfo(filePath);

            if (autoLoad)
                Load();
        }

        public Tab TabData { get; private set; }

        public void Export(ExportFormat format, string filePath)
        {
            switch (format)
            {
                case ExportFormat.Tabster:
                    File.Copy(FileInfo.FullName, filePath);
                    break;
                case ExportFormat.Text:
                    File.WriteAllText(filePath, TabData.Contents);
                    break;
            }
        }

        #region Static Methods

        public static TabFile Create(Tab tab, string directory)
        {
            var filePath = GenerateUniqueFilename(directory, string.Format("{0} - {1} ({2}){3}", tab.Artist, tab.Title, tab.Type.ToFriendlyString(), FILE_EXTENSION));
            var tabFile = new TabFile(filePath, false) {TabData = tab, FileInfo = new FileInfo(filePath)};
            tabFile.Save();
            return tabFile;
        }

        public static bool TryParse(string filePath, out TabFile tabFile)
        {
            var success = false;
            TabFile tf = null;

            try
            {
                tf = new TabFile(filePath, true);
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

            tabFile = tf;
            return success;
        }

        #endregion

        #region Implementation of ITabsterFile

        public void Load()
        {
            Load(true);
        }

        public void Save()
        {
            Save(FileInfo.FullName);
        }

        public void Load(bool convertCarriageReturns)
        {
            BeginFileRead(new Version(FILE_VERSION));

            var titleValue = ReadNodeValue("song", true) ?? ReadNodeValue("title");
            var artistValue = ReadNodeValue("artist");
            var typeValue = Tab.GetTabType(ReadNodeValue("type"));
            var contentsValue = ReadNodeValue("tab");
            var createdValue = ReadNodeValue("date", true) ?? ReadNodeValue("created", true);
            var sourceValue = ReadNodeValue("source", true);

            var sourceType = TabSource.UserCreated;

            Uri sourceURI = null;

            if (!string.IsNullOrEmpty(sourceValue))
            {
                //legacy
                if (sourceValue == "UserCreated")
                    sourceType = TabSource.UserCreated;

                else if (sourceValue == "FileImport")
                    sourceType = TabSource.FileImport;

                else if (Uri.IsWellFormedUriString(sourceValue, UriKind.Absolute))
                {
                    sourceURI = new Uri(sourceValue);
                    sourceType = sourceURI.IsFile ? TabSource.FileImport : TabSource.Download;
                }
            }

            //var remoteSourceValue = sourceType == TabSource.Download && Uri.IsWellFormedUriString(sourceValue, UriKind.Absolute) ? new Uri(sourceValue) : null;
            var audioValue = ReadNodeValue("audio");
            var lyricsValue = ReadNodeValue("lyrics");

            //fix carriage returns without newlines
            if (convertCarriageReturns)
                contentsValue = Common.ConvertNewlines(contentsValue);

            contentsValue = Common.StripHTML(contentsValue);

            TabData = new Tab(artistValue, titleValue, typeValue, contentsValue)
                          {
                              SourceType = sourceType,
                              Source = sourceURI,
                              Lyrics = lyricsValue,
                              Audio = audioValue,
                              Created = createdValue != null ? DateTime.Parse(createdValue) : FileInfo.CreationTime
                          };

            if (FileFormatOutdated)
            {
                Save();
                Load();
            }
        }

        public new void Save(string filePath)
        {
            BeginFileWrite("tabster", FILE_VERSION);
            WriteNode("title", TabData.Title);
            WriteNode("artist", TabData.Artist);
            WriteNode("type", TabData.Type.ToFriendlyString());
            WriteNode("tab", TabData.Contents);

            var sourceValue = "UserCreated";

            if (TabData.Source != null)
                sourceValue = TabData.Source.ToString();
            else if (TabData.SourceType == TabSource.FileImport)
                sourceValue = "FileImport";
            else if (TabData.SourceType == TabSource.UserCreated)
                sourceValue = "UserCreated";

            WriteNode("source", sourceValue);
            WriteNode("created", TabData.Created.ToString());
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