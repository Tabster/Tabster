#region

using System;
using System.IO;
using System.Text.RegularExpressions;
using Tabster.Core.Types;

#endregion

namespace Tabster.Data
{
    public class TablatureDocument : ITabsterDocument, ITablature, ITablatureAttributes, ITablatureSourceAttribute, ITablatureUserDefined, ITablatureRatedAttribute
    {
        #region Constants

        public const string FILE_EXTENSION = ".tabster";
        public static readonly Version FILE_VERSION = new Version("1.4.1");

        #endregion

        private readonly TabsterXmlDocument _doc = new TabsterXmlDocument("tabster");

        #region Constructors

        public TablatureDocument()
        {
        }

        public TablatureDocument(string artist, string title, TablatureType type)
            : this()
        {
            Artist = artist;
            Title = title;
            Type = type;
        }

        public TablatureDocument(string artist, string title, TablatureType type, string contents) : this(artist, title, type)
        {
            Contents = contents;
        }

        #endregion

        #region Implementation of ITablature

        #region ITablature Members

        public string Contents { get; set; }

        #endregion

        #region ITablatureAttributes Members

        public string Artist { get; set; }

        public string Title { get; set; }

        public TablatureType Type { get; set; }

        #endregion

        #endregion

        #region Implementation of ITabsterDocument

        public Version FileVersion
        {
            get { return _doc.Version; }
        }

        public FileInfo FileInfo { get; private set; }

        public void Load(string fileName)
        {
            FileInfo = new FileInfo(fileName);

            _doc.Load(fileName);

            Artist = _doc.TryReadNodeValue("artist", string.Empty);
            Title = _doc.TryReadNodeValues(new[] {"song", "title"}, string.Empty);

            var tabTypeValue = _doc.TryReadNodeValue("type");
            TablatureType? type = null;

            if (tabTypeValue != null && Enum.IsDefined(typeof (TablatureType), tabTypeValue))
            {
                type = (TablatureType) Enum.Parse(typeof (TablatureType), tabTypeValue);
            }

            else //legacy
            {
                var fromString = FromFriendlyString(tabTypeValue);

                if (fromString.HasValue)
                    type = fromString.Value;
            }

            if (!type.HasValue)
                throw new TabsterDocumentException("Invalid or missing tab type");

            Contents = _doc.TryReadNodeValue("tab", string.Empty);

            var createdValue = _doc.TryReadNodeValues(new[] {"date", "created"});

            DateTime createDatetime;

            Created = !string.IsNullOrEmpty(createdValue) && DateTime.TryParse(createdValue, out createDatetime)
                          ? createDatetime
                          : FileInfo.CreationTime;

            Comment = _doc.TryReadNodeValue("comment", string.Empty);

            SourceType = TablatureSourceType.UserCreated;

            var sourceValue = _doc.TryReadNodeValue("source", string.Empty);

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

            Method = _doc.TryReadNodeValue("method", string.Empty);

            var ratingValue = _doc.TryReadNodeValue("rating");

            if (!string.IsNullOrEmpty(ratingValue))
                Rating = TablatureRatingUtilities.FromString(ratingValue);
        }

        public void Save()
        {
            Save(FileInfo.FullName);
            FileInfo.Refresh();
        }

        public ITabsterDocument SaveAs(string fileName)
        {
            Save(fileName);
            var doc = new TablatureDocument();
            doc.Load(fileName);
            return doc;
        }

        public void Update()
        {
            //fix carriage returns without newlines and strip html
            if (FileVersion < new Version("1.4"))
            {
                var newlineRegex = new Regex("(?<!\r)\n", RegexOptions.Compiled);

                Contents = newlineRegex.Replace(Contents, Environment.NewLine);
                Contents = StripHTML(Contents);
            }

            if (FileVersion != FILE_VERSION)
                Save();
        }

        private void Save(string fileName)
        {
            _doc.Version = FILE_VERSION;

            _doc.WriteNode("title", Title);
            _doc.WriteNode("artist", Artist);
            _doc.WriteNode("type", Type.ToString());
            _doc.WriteNode("tab", Contents);

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

            _doc.WriteNode("source", sourceValue);
            _doc.WriteNode("method", Method);
            _doc.WriteNode("rating", Rating.ToInt().ToString());
            _doc.WriteNode("created", Created == DateTime.MinValue ? DateTime.Now.ToString() : Created.ToString());
            _doc.WriteNode("comment", Comment);

            _doc.Save(fileName);
        }

        #endregion

        #region Implementation of ITablatureSourced

        public TablatureSourceType SourceType { get; set; }

        public Uri Source { get; set; }

        public string Method { get; set; }

        #endregion

        #region Implementation of ITablatureUserDefined

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        #endregion

        #region Static Methods

        private static TablatureType? FromFriendlyString(string str)
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

        private static string StripHTML(string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            for (var i = 0; i < source.Length; i++)
            {
                var let = source[i];

                if (let == '<')
                {
                    inside = true;
                    continue;
                }

                if (let == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }

            return new string(array, 0, arrayIndex);
        }

        #endregion

        #region Implementation of ITablatureRated

        public TablatureRating Rating { get; set; }

        #endregion
    }
}