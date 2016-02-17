#region

using System;
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

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public void Export(ITablatureFile file, string fileName, TablatureFileExportArguments args)
        {
            File.WriteAllText(fileName, file.Contents);
        }

        #endregion
    }
}