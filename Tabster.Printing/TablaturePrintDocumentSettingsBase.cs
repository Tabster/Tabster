#region

using System.Drawing;

#endregion

namespace Tabster.Printing
{
    public abstract class TablaturePrintDocumentSettingsBase
    {
        protected TablaturePrintDocumentSettingsBase()
        {
            PrintColor = Color.Black;
        }

        public Color PrintColor { get; set; }
        public bool DisplayPageNumbers { get; set; }
        public bool DisplayTimetstamp { get; set; }
    }
}