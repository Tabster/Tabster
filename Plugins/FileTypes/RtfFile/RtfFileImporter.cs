#region

using System.Windows.Forms;
using Tabster.Core.Data;
using Tabster.Core.Data.Processing;
using Tabster.Core.Types;

#endregion

namespace RtfFile
{
    public class RtfFileImporter : ITablatureFileImporter
    {
        public RtfFileImporter()
        {
            FileType = new FileType("Rich Text Format File", ".rtf");
        }

        #region Implementation of ITablatureDocumentImporter

        public FileType FileType { get; private set; }

        public TablatureDocument Import(string fileName)
        {
            using (var rtb = new RichTextBox())
            {
                rtb.LoadFile(fileName);
                var doc = new TablatureDocument {Contents = rtb.Text};
                return doc;
            }
        }

        public TablatureDocument Import(string fileName, string artist, string title, TabType type)
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