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

        /// <summary>
        ///     The current page number being printed.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        ///     Real page bounds based on printable area of the page.
        /// </summary>
        public Rectangle RealPageBounds { get; set; }
    }
}