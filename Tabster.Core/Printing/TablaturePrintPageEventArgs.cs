#region

using System.Drawing;
using System.Drawing.Printing;

#endregion

namespace Tabster.Core.Printing
{
    public class TablaturePrintPageEventArgs : PrintPageEventArgs
    {
        public TablaturePrintPageEventArgs(Graphics graphics, Rectangle marginBounds, Rectangle pageBounds, PageSettings pageSettings) :
            base(graphics, marginBounds, pageBounds, pageSettings)
        {
        }

        public int CurrentPage { get; set; }
    }
}