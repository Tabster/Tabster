#region

using System;
using System.Collections.Generic;
using System.IO;
using HtmlFile.Properties;
using Tabster.Data;
using Tabster.Data.Processing;

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

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public void Export(ITablatureFile file, string fileName, TablatureFileExportArguments args)
        {
            var templates = new Dictionary<string, string>
            {
                {"{TAB_ARTIST}", file.Artist},
                {"{TAB_TITLE}", file.Title},
                {"{TAB_TYPE}", file.Type.ToFriendlyString()},
                {"{TAB_CONTENTS}", file.Contents}
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