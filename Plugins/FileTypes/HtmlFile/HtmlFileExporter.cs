#region

using System.Collections.Generic;
using System.IO;
using HtmlFile.Properties;
using Tabster.Data;
using Tabster.Data.Processing;
using Tabster.Data.Xml;

#endregion

namespace HtmlFile
{
    public class HtmlFileExporter : ITablatureFileExporter
    {
        public HtmlFileExporter()
        {
            FileType = new FileType("HTML File", new[] {".html", ".htm"});
        }

        #region Implementation of ITablatureFileExporter

        public FileType FileType { get; private set; }

        public void Export(TablatureDocument doc, string fileName)
        {
            var templates = new Dictionary<string, string>
            {
                {"{TAB_ARTIST}", doc.Artist},
                {"{TAB_TITLE}", doc.Title},
                {"{TAB_TYPE}", doc.Type.ToFriendlyString()},
                {"{TAB_CONTENTS}", doc.Contents}
            };

            var html = Resources.Html_Template;

            foreach (var templatePair in templates)
            {
                html = html.Replace(templatePair.Key, templatePair.Value);
            }

            File.WriteAllText(fileName, html);
        }

        #endregion
    }
}