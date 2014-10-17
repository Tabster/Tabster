#region

using HtmlAgilityPack;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace HtmlFile
{
    public class HtmlFileImporter : ITablatureFileImporter
    {
        public HtmlFileImporter()
        {
            FileType = new FileType("HTML File", new[] {".html", ".htm"});
        }

        #region Implementation of ITablatureFileImporter

        public FileType FileType { get; private set; }

        public TablatureDocument Import(string fileName)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(fileName);

            string tabContents;

            var pre = htmlDoc.DocumentNode.SelectSingleNode("//pre");

            if (pre != null)
            {
                tabContents = pre.InnerText;
            }

            else
            {
                var body = htmlDoc.DocumentNode.SelectSingleNode("//body");
                tabContents = body != null ? body.InnerText : htmlDoc.DocumentNode.InnerText;
            }

            var doc = new TablatureDocument {Contents = tabContents};
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