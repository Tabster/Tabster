#region

using System.Drawing;
using Novacode;
using Tabster.Data;
using Tabster.Data.Processing;
using Tabster.Data.Xml;

#endregion

namespace WordDoc
{
    public class WordDocExporter : ITablatureFileExporter
    {
        public WordDocExporter()
        {
            FileType = new FileType("Microsoft Office Open XML Format Document", ".docx");
        }

        #region Implementation of ITablatureFileExporter

        public FileType FileType { get; private set; }

        public void Export(TablatureDocument doc, string fileName)
        {
            using (var document = DocX.Create(fileName))
            {
                var p = document.InsertParagraph();
                p.Append(doc.Contents).Font(new FontFamily("Courier New")).FontSize(9);
                document.Save();
            }
        }

        #endregion
    }
}