#region

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Printing
{
    public class TablaturePrintDocument : PrintDocument
    {
        private readonly Font _font;
        private readonly ITablature _tablature;

        private int _pageCount;
        private bool _performingPageCount;
        private PrintAction _printAction;
        private string _printContents;
        private Rectangle _realPageBounds;
        private TablaturePrintDocumentSettings _settings;

        protected int TotalPages { get; private set; }

        #region Constructors

        public TablaturePrintDocument(ITablature tablature, Font font)
        {
            if (tablature == null)
                throw new ArgumentNullException("tablature");
            if (font == null)
                throw new ArgumentNullException("font");

            _tablature = tablature;
            _font = font;
            _settings = new TablaturePrintDocumentSettings {DisplayTitle = true, DisplayPageNumbers = true, DisplayPrintTime = true};
        }

        #endregion

        #region Properties

        public TablaturePrintDocumentSettings Settings
        {
            get { return _settings ?? (_settings = new TablaturePrintDocumentSettings()); }
            set { _settings = value; }
        }

        #endregion

        protected int CountTotalPages()
        {
            var _originalPrintContents = string.Copy(_printContents);

            //backup existing controller
            var existingController = PrintController;

            var pageCountController = new PageCountPrintController();
            PrintController = pageCountController;
            Print();

            PrintController = existingController;

            _printContents = _originalPrintContents;

            TotalPages = pageCountController.PageCount;
            return pageCountController.PageCount;
        }

        private void InternalPrintPage(PrintPageEventArgs e)
        {
            if (_realPageBounds == Rectangle.Empty)
                _realPageBounds = GetRealPageBounds(e, _printAction == PrintAction.PrintToPreview);

            _pageCount++;

            var printPageArgs = new TablaturePrintPageEventArgs(e.Graphics, e.MarginBounds, e.PageBounds, e.PageSettings) {CurrentPage = _pageCount, RealPageBounds = _realPageBounds};

            if (_printContents.Length > 0)
            {
                if (Settings.DisplayTitle)
                    OnDrawTitle(printPageArgs);

                if (Settings.DisplayPageNumbers)
                    OnDrawPageNumbers(printPageArgs);

                if (Settings.DisplayPrintTime)
                    OnDrawPrintTime(printPageArgs);

                DrawTabContents(printPageArgs);

                e.HasMorePages = printPageArgs.HasMorePages;
            }
        }

        #region Overrides

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            _printAction = e.PrintAction;
            _pageCount = 0;
            _realPageBounds = Rectangle.Empty;

            _printContents = string.Copy(_tablature.Contents);

            if (!_performingPageCount)
            {
                _performingPageCount = true;

                if (TotalPages == 0)
                    CountTotalPages();
            }

            base.OnBeginPrint(e);
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            if (_performingPageCount)
                _performingPageCount = false;

            _pageCount = 0;
            base.OnEndPrint(e);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            InternalPrintPage(e);
            base.OnPrintPage(e);
        }

        #endregion

        #region Drawing Methods

        private void OnDrawTitle(PrintPageEventArgs e)
        {
            e.Graphics.DrawString(Settings.Title, _font, new SolidBrush(_settings.PrintColor), _realPageBounds, new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            });
        }

        private void DrawTabContents(PrintPageEventArgs e)
        {
            var printBounds = e.MarginBounds;

            int charactersOnPage;
            int linesPerPage;

            e.Graphics.MeasureString(_printContents, _font, printBounds.Size, StringFormat.GenericTypographic, out charactersOnPage, out linesPerPage);
            e.Graphics.DrawString(_printContents, _font, new SolidBrush(_settings.PrintColor), printBounds, StringFormat.GenericTypographic);

            _printContents = _printContents.Substring(charactersOnPage);

            e.HasMorePages = _printContents.Length > 0;
        }

        protected virtual void OnDrawPageNumbers(TablaturePrintPageEventArgs e)
        {
            e.Graphics.DrawString(string.Format("Page {0} of {1}", e.CurrentPage, TotalPages), _font, new SolidBrush(_settings.PrintColor), _realPageBounds, new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Near
            });
        }

        protected virtual void OnDrawPrintTime(PrintPageEventArgs e)
        {
            e.Graphics.DrawString(DateTime.Now.ToString(), _font, new SolidBrush(_settings.PrintColor), _realPageBounds, new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Far
            });
        }

        #endregion

        #region Static Methods

        private static Rectangle GetRealPageBounds(PrintPageEventArgs e, bool preview)
        {
            // Return in units of 1/100 inch
            if (preview)
                return e.PageBounds;

            // Translate to units of 1/100 inch
            var vpb = e.Graphics.VisibleClipBounds;

            PointF[] bottomRight =
            {
                new PointF(vpb.Size.Width, vpb.Size.Height)
            };

            e.Graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, bottomRight);
            var dpiX = e.Graphics.DpiX;
            var dpiY = e.Graphics.DpiY;
            return new Rectangle(0, 0, (int) (bottomRight[0].X*100/dpiX), (int) (bottomRight[0].Y*100/dpiY));
        }

        #endregion
    }
}