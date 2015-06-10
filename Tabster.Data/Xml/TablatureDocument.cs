#region

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Tabster.Core.Types;

#endregion

namespace Tabster.Data.Xml
{
    [Obsolete]
    public class TablatureDocument : ITablatureFile
    {
        #region Constants

        public const string FileExtension = ".tabster";
        public static readonly Version FileVersion = new Version("1.4");
        private static readonly Encoding DefaultEncoding = Encoding.GetEncoding("ISO-8859-1");

        #endregion

        private const string RootNode = "tabster";

        #region Implementation of ITabsterDocument

        public void Load(string fileName)
        {
            var fi = new FileInfo(fileName);

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.GetElementByTagName(RootNode);

            FileHeader = new TabsterFileHeader(new Version(rootNode.GetAttributeValue("version")), CompressionMode.None);

            //required properties

            Artist = xmlDoc.GetNodeValue("artist");
            if (string.IsNullOrEmpty(Artist))
                throw new TabsterFileException("Missing artist property");

            Title = xmlDoc.GetNodeValues(new[] {"song", "title"});
            if (string.IsNullOrEmpty(Title))
                throw new TabsterFileException("Missing title property");

            Type = FromFriendlyString(xmlDoc.GetNodeValue("type"));
            if (Type == null)
                throw new TabsterFileException("Invalid or missing tablature type");

            //allow contents to be empty, just not null
            Contents = xmlDoc.GetNodeValue("tab");
            if (Contents == null)
                throw new TabsterFileException("Missing tablature contents");

            //"optional" properties

            var createdValue = xmlDoc.GetNodeValues(new[] {"date", "created"});

            DateTime createDatetime;

            var created = !string.IsNullOrEmpty(createdValue) && DateTime.TryParse(createdValue, out createDatetime)
                ? createDatetime
                : fi.CreationTime;

            FileAttributes = new TabsterFileAttributes(created, Encoding.GetEncoding(xmlDoc.GetXmlDeclaration().Encoding));

            Comment = xmlDoc.GetNodeValue("comment", string.Empty);

            SourceType = TablatureSourceType.UserCreated;

            var sourceValue = xmlDoc.GetNodeValue("source", string.Empty);

            if (!string.IsNullOrEmpty(sourceValue))
            {
                //legacy
                if (sourceValue == "UserCreated")
                    SourceType = TablatureSourceType.UserCreated;
                else if (sourceValue == "FileImport")
                    SourceType = TablatureSourceType.FileImport;

                else if (Uri.IsWellFormedUriString(sourceValue, UriKind.Absolute))
                {
                    Source = new Uri(sourceValue);
                    SourceType = Source.IsFile ? TablatureSourceType.FileImport : TablatureSourceType.Download;
                }
            }

            //fix carriage returns without newlines and strip html
            if (FileHeader.Version < new Version("1.4"))
            {
                var newlineRegex = new Regex("(?<!\r)\n", RegexOptions.Compiled);

                Contents = newlineRegex.Replace(Contents, Environment.NewLine);
                Contents = StripHtml(Contents);

                Save(fileName);
            }
        }

        public void Save(string fileName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", DefaultEncoding.EncodingName, null));
            var rootNode = xmlDoc.CreateElement(RootNode);
            xmlDoc.AppendChild(rootNode);

            xmlDoc.SetAttributeValue(rootNode, "version", FileVersion.ToString());

            xmlDoc.WriteNode("title", Title);
            xmlDoc.WriteNode("artist", Artist);
            xmlDoc.WriteNode("type", ToFriendlyString(Type));
            xmlDoc.WriteNode("tab", Contents);

            var sourceValue = "UserCreated";

            if (Source != null)
            {
                sourceValue = Source.ToString();
            }

            else
            {
                //legacy
                if (SourceType == TablatureSourceType.FileImport)
                    sourceValue = "FileImport";
                if (SourceType == TablatureSourceType.UserCreated)
                    sourceValue = "UserCreated";
            }

            xmlDoc.WriteNode("source", sourceValue);
            xmlDoc.WriteNode("created", FileAttributes.Created == DateTime.MinValue ? DateTime.Now.ToString() : FileAttributes.Created.ToString());
            xmlDoc.WriteNode("comment", Comment);

            xmlDoc.Save(fileName);

            FileHeader = new TabsterFileHeader(FileVersion, CompressionMode.None);
            FileAttributes = new TabsterFileAttributes(DateTime.UtcNow, FileAttributes != null ? FileAttributes.Encoding : DefaultEncoding);
        }

        public TabsterFileAttributes FileAttributes { get; private set; }
        public TabsterFileHeader FileHeader { get; private set; }

        #endregion

        #region Static Methods

        private static TablatureType FromFriendlyString(string str)
        {
            switch (str)
            {
                case "Guitar Tab":
                    return TablatureType.Guitar;
                case "Guitar Chords":
                    return TablatureType.Chords;
                case "Bass Tab":
                    return TablatureType.Bass;
                case "Drum Tab":
                    return TablatureType.Drum;
                case "Ukulele Tab":
                    return TablatureType.Ukulele;
            }

            return null;
        }

        private static string ToFriendlyString(TablatureType type)
        {
            if (type == TablatureType.Guitar)
                return "Guitar Tab";
            if (type == TablatureType.Guitar)
                return "Guitar Chords";
            if (type == TablatureType.Chords)
                return "Bass Tab";
            if (type == TablatureType.Bass)
                return "Drum Tab";
            if (type == TablatureType.Drum)
                return "Ukulele Tab";

            return null;
        }

        private static string StripHtml(string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var c in source)
            {
                if (c == '<')
                {
                    inside = true;
                    continue;
                }

                if (c == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside)
                {
                    array[arrayIndex] = c;
                    arrayIndex++;
                }
            }

            return new string(array, 0, arrayIndex);
        }

        #endregion

        #region Implementation of ITablatureFile

        public string Comment { get; set; }

        #endregion

        #region Implementation of ITablatureAttributes

        public string Artist { get; set; }
        public string Title { get; set; }
        public TablatureType Type { get; set; }

        #endregion

        #region Implementation of ITablatureSourceAttribute

        public TablatureSourceType SourceType { get; set; }
        public Uri Source { get; set; }

        #endregion

        #region Implementation of ITablature

        public string Contents { get; set; }

        #endregion
    }
}