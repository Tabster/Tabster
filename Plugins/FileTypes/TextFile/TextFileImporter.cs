#region

using System.IO;
using Tabster.Core.Data;
using Tabster.Core.Data.Processing;
using Tabster.Core.Types;

#endregion

namespace TextFile
{
    public class TextFileImporter : ITablatureFileImporter
    {
        public TextFileImporter()
        {
            FileType = new FileType("Text File", ".txt");
        }

        #region Implementation of ITablatureDocumentImporter

        public FileType FileType { get; private set; }

        public TablatureDocument Import(string fileName)
        {
            var contents = File.ReadAllText(fileName);
            var doc = new TablatureDocument {Contents = contents};
            return doc;
        }

        public TablatureDocument Import(string fileName, string artist, string title, TablatureType type)
        {
            var doc = Import(fileName);
            doc.Artist = artist;
            doc.Title = title;
            doc.Type = type;
            return doc;
        }

        #endregion
    }
}