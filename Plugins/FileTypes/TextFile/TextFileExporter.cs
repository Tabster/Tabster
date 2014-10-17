#region

using System.IO;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace TextFile
{
    internal class TextFileExporter : ITablatureFileExporter
    {
        public TextFileExporter()
        {
            FileType = new FileType("Text File", ".txt");
        }

        #region Implementation of ITablatureFileExporter

        public FileType FileType { get; private set; }

        public void Export(TablatureDocument doc, string fileName)
        {
            File.WriteAllText(fileName, doc.Contents);
        }

        #endregion
    }
}