#region

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;
using Version = System.Version;

#endregion

namespace PdfFile
{
    public class PdfFileExporter : ITablatureFileExporter
    {
        public PdfFileExporter()
        {
            FileType = new FileType("PDF File", ".pdf");
        }

        #region Implementation of ITablatureFileExporter

        public FileType FileType { get; private set; }

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public void Export(ITablatureFile file, string fileName, TablatureFileExportArguments args)
        {
            var plugin = new PdfFilePlugin();

            var font = new Font(Font.FontFamily.COURIER, 9F);

            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                var pdfdoc = new Document();

                using (var writer = PdfWriter.GetInstance(pdfdoc, fs))
                {
                    pdfdoc.AddCreator(string.Format("{0} {1}", plugin.DisplayName, plugin.Version));
                    pdfdoc.AddTitle(file.ToFriendlyString());

                    pdfdoc.Open();

                    pdfdoc.Add(new Paragraph(file.Contents, font));
                    pdfdoc.Close();
                }
            }
        }

        #endregion
    }
}