#region

using System.Windows.Forms;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;

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

        public AttributedTablature Import(string fileName)
        {
            using (var rtb = new RichTextBox())
            {
                rtb.LoadFile(fileName);
                var doc = new AttributedTablature {Contents = rtb.Text};
                return doc;
            }
        }

        public AttributedTablature Import(string fileName, string artist, string title, TablatureType type)
        {
            var tab = Import(fileName);
            tab.Artist = artist;
            tab.Title = title;
            tab.Type = type;
            return tab;
        }

        #endregion
    }
}