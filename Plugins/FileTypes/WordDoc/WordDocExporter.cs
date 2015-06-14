#region

using System;
using System.Drawing;
using Novacode;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace WordDoc
{
    public class WordDocExporter : ITablatureFileExporter
    {
        public WordDocExporter()
        {
            FileType = new FileType("Microsoft Office Open XML Format File", ".docx");
        }

        #region Implementation of ITablatureFileExporter

        public FileType FileType { get; private set; }

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public void Export(ITablatureFile file, string fileName)
        {
            using (var document = DocX.Create(fileName))
            {
                var p = document.InsertParagraph();
                p.Append(file.Contents).Font(new FontFamily("Courier New")).FontSize(9);
                document.Save();
            }
        }

        #endregion
    }
}