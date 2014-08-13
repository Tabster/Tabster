#region

using System;
using System.IO;
using System.Text.RegularExpressions;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.FileTypes
{
    public class TablatureDocument : ITabsterDocument, ITablatureSourced, ITablatureUserDefined
    {
        #region Constants

        public const string FILE_EXTENSION = ".tabster";
        public static readonly Version FILE_VERSION = new Version("1.4.1");

        #endregion

        private readonly TabsterXmlDocument _doc = new TabsterXmlDocument("tabster");

        public TablatureDocument()
        {
        }

        public TablatureDocument(string artist, string title, TabType type)
            : this()
        {
            Artist = artist;
            Title = title;
            Type = type;
            
        }

        public TablatureDocument(string artist, string title, TabType type, string contents) : this(artist, title, type)
        {
            Contents = contents;
        }

        #region Implementation of ITablature

        public string Artist { get; set; }

        public string Title { get; set; }

        public string Contents { get; set; }

        public TabType Type { get; set; }

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

            Title = _doc.TryReadNodeValue("song") ?? _doc.TryReadNodeValue("title", string.Empty);
            Artist = _doc.TryReadNodeValue("artist", string.Empty);
            Type = TabTypeUtilities.FromFriendlyString(_doc.TryReadNodeValue("type", string.Empty)).Value;
            Contents = _doc.TryReadNodeValue("tab", string.Empty);

            Created = DateTime.Parse(_doc.TryReadNodeValue("date") ?? _doc.TryReadNodeValue("created") ?? FileInfo.CreationTime.ToString());

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
                Rating = (TabRating) int.Parse(ratingValue);

            Update();
        }

        public void Save()
        {
            Save(FileInfo.FullName);
        }

        public void Save(string fileName)
        {
            _doc.Version = FILE_VERSION;

            _doc.WriteNode("title", Title);
            _doc.WriteNode("artist", Artist);
            _doc.WriteNode("type", Type.ToFriendlyString());
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
            _doc.WriteNode("rating", Rating.HasValue ? ((int) Rating).ToString() : string.Empty);
            _doc.WriteNode("created", Created == DateTime.MinValue ? DateTime.Now.ToString() : Created.ToString());
            _doc.WriteNode("comment", Comment);

            _doc.Save(fileName);

            FileInfo = new FileInfo(fileName);
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

        #endregion

        #region Implementation of ITablatureSourced

        public TablatureSourceType SourceType { get; set; }

        public Uri Source { get; set; }

        public string Method { get; set; }

        public TabRating? Rating { get; set; }

        #endregion

        #region Implementation of ITablatureUserDefined

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        #endregion

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

        #region Implementation of IEquatable<ITabsterDocument>

        public bool Equals(ITabsterDocument other)
        {
            return Equals((object) other);
        }

        public override bool Equals(object other)
        {
            var doc = other as TablatureDocument;
            return doc != null && FileInfo.FullName.Equals(doc.FileInfo.FullName, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}