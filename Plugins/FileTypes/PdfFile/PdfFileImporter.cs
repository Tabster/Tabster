#region

using System;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace PdfFile
{
    public class PdfFileImporter : ITablatureFileImporter
    {
        public PdfFileImporter()
        {
            FileType = new FileType("PDF File", ".pdf");
        }

        #region Implementation of ITablatureDocumentImporter

        public FileType FileType { get; private set; }

        public AttributedTablature Import(string fileName)
        {
            var contentsBuilder = new StringBuilder();

            if (File.Exists(fileName))
            {
                var pdfReader = new PdfReader(fileName);

                for (var page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    var currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    contentsBuilder.Append(currentText);
                    contentsBuilder.Append(Environment.NewLine);
                }
                pdfReader.Close();
            }

            var doc = new AttributedTablature {Contents = contentsBuilder.ToString()};
            return doc;
        }

        public AttributedTablature Import(string fileName, string artist, string title, TablatureType type)
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