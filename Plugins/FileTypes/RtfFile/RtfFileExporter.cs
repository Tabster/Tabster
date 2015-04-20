#region

using System.Drawing;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace RtfFile
{
    internal class RtfFileExporter : ITablatureFileExporter
    {
        private Font _font;
        private RichTextBox _rtb;

        #region Implementation of ITablatureFileExporter

        public FileType FileType { get; private set; }

        public void Export(ITablatureFile file, string fileName)
        {
            if (_font == null)
                _font = new Font("Courier New", 9F);

            if (_rtb == null)
                _rtb = new RichTextBox {Font = new Font("Courier New", 9F), Text = file.Contents};

            _rtb.SaveFile(fileName);
            _rtb.SaveFile(fileName); //have to call method twice otherwise empty file is created
        }

        #endregion

        public RtfFileExporter()
        {
            FileType = new FileType("Rich Text Format File", ".rtf");
        }

        ~RtfFileExporter()
        {
            if (_font != null)
                _font.Dispose();

            if (_rtb != null && !_rtb.IsDisposed)
                _rtb.Dispose();
        }
    }
}