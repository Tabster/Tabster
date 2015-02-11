#region

using System.Drawing;
using System.Drawing.Printing;

#endregion

namespace Tabster.Core.Printing
{
    internal class PageCountPrintController : PreviewPrintController
    {
        public int PageCount { get; private set; }

        public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
        {
            base.OnStartPrint(document, e);
            PageCount = 0;
        }

        public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
        {
            ++PageCount;
            return base.OnStartPage(document, e);
        }
    }
}