#region

using System;
using System.IO;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;

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

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public AttributedTablature Import(string fileName)
        {
            var contents = File.ReadAllText(fileName);
            var tab = new AttributedTablature { Contents = contents };
            return tab;
        }

        public AttributedTablature Import(string fileName, string artist, string title, TablatureType type)
        {
            var tab = Import(fileName);
            tab.Artist = artist;
            tab.Title = title;
            tab.Type = type;
            return tab;
        }

        #endregion
    }
}