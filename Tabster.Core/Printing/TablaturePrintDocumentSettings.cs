#region

using System.Drawing;

#endregion

namespace Tabster.Core.Printing
{
    public sealed class TablaturePrintDocumentSettings
    {
        public TablaturePrintDocumentSettings()
        {
            PrintColor = Color.Black;
        }

        public string Title { get; set; }

        public bool DisplayTitle { get; set; }
        public bool DisplayPageNumbers { get; set; }
        public bool DisplayPrintTime { get; set; }

        public Color PrintColor { get; set; }
    }
}