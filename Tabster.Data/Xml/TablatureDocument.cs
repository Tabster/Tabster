#region

using System;
using System.IO;
using System.Text.RegularExpressions;
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

        #endregion

        private const string RootNode = "tabster";

        #region Implementation of ITabsterDocument

        public void Load(string fileName)
        {
            var fi = new FileInfo(fileName);

            var doc = new TabsterXmlDocument(RootNode);
            doc.Load(fileName);

            FileHeader = new TabsterXmlFileHeader(doc.Version);

            //required properties

            Artist = doc.TryReadNodeValue("artist");
            if (string.IsNullOrEmpty(Artist))
                throw new TabsterFileException("Missing artist property");

            Title = doc.TryReadNodeValues(new[] {"song", "title"});
            if (string.IsNullOrEmpty(Title))
                throw new TabsterFileException("Missing title property");

            Type = FromFriendlyString(doc.TryReadNodeValue("type"));
            if (Type == null)
                throw new TabsterFileException("Invalid or missing tablature type");

            //allow contents to be empty, just not null
            Contents = doc.TryReadNodeValue("tab");
            if (Contents == null)
                throw new TabsterFileException("Missing tablature contents");

            //"optional" properties

            var createdValue = doc.TryReadNodeValues(new[] {"date", "created"});

            DateTime createDatetime;

            var created = !string.IsNullOrEmpty(createdValue) && DateTime.TryParse(createdValue, out createDatetime)
                ? createDatetime
                : fi.CreationTime;

            FileAttributes = new TabsterFileAttributes(created);

            Comment = doc.TryReadNodeValue("comment", string.Empty);

            SourceType = TablatureSourceType.UserCreated;

            var sourceValue = doc.TryReadNodeValue("source", string.Empty);

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
            var doc = new TabsterXmlDocument(RootNode) {Version = FileVersion};

            doc.WriteNode("title", Title);
            doc.WriteNode("artist", Artist);
            doc.WriteNode("type", ToFriendlyString(Type));
            doc.WriteNode("tab", Contents);

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

            doc.WriteNode("source", sourceValue);
            doc.WriteNode("created", FileAttributes.Created == DateTime.MinValue ? DateTime.Now.ToString() : FileAttributes.Created.ToString());
            doc.WriteNode("comment", Comment);

            doc.Save(fileName);
        }

        public TabsterFileAttributes FileAttributes { get; private set; }
        public ITabsterFileHeader FileHeader { get; private set; }

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