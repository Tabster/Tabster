#region

using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace PdfFile
{
    public class PdfFileExporter : ITablatureFileExporter
    {
        private readonly Font _font;

        public PdfFileExporter()
        {
            FileType = new FileType("PDF File", ".pdf");

            //register system fonts directory before loading fonts
            var fontsDirectory = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "Fonts");
            FontFactory.RegisterDirectory(fontsDirectory);

            _font = FontFactory.GetFont("Courier New", 9F, BaseColor.BLACK);
        }

        #region Implementation of ITablatureFileExporter

        public FileType FileType { get; private set; }

        public void Export(TablatureDocument doc, string fileName)
        {
            var plugin = new PdfFilePlugin();

            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                var pdfdoc = new Document();

                using (var writer = PdfWriter.GetInstance(pdfdoc, fs))
                {
                    pdfdoc.AddCreator(string.Format("{0} {1}", plugin.DisplayName, plugin.Version));
                    pdfdoc.AddTitle(doc.ToFriendlyString());

                    pdfdoc.Open();

                    pdfdoc.Add(new Paragraph(doc.Contents, _font));
                    pdfdoc.Close();
                }
            }
        }

        #endregion
    }
}