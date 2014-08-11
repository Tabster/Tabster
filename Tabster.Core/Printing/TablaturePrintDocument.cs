#region

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using Tabster.Core.FileTypes;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Printing
{
    public class TablaturePrintDocument : PrintDocument
    {
        private readonly Font _font;
        private readonly ITablature _tab;

        private Rectangle _pageBounds;

        private int _pageCount;
        private bool _performingPageCount;
        private string _printContents;
        private TablaturePrintDocumentSettings _settings;
        private int _totalPages;

        public TablaturePrintDocument(TablatureDocument doc, Font font)
        {
            _tab = doc;
            _font = font;
        }

        public TablaturePrintDocument(ITablature tab, Font font)
        {
            _tab = tab;
            _font = font;
        }

        #region Properties

        public TablaturePrintDocumentSettings Settings
        {
            get { return _settings ?? (_settings = new TablaturePrintDocumentSettings()); }
            set { _settings = value; }
        }

        #endregion

        private int GetPageCount()
        {
            var _originalPrintContents = string.Copy(_printContents);

            var existingController = PrintController;
            var controller = new PageCountPrintController();
            PrintController = controller;
            Print();
            PrintController = existingController;

            _printContents = _originalPrintContents;

            return controller.PageCount;
        }

        private void InternalPrintPage(PrintPageEventArgs e)
        {
            if (_pageBounds == Rectangle.Empty)
                _pageBounds = GetRealPageBounds(e, false);

            if (_printContents.Length > 0)
            {
                _pageCount++;

                if (Settings.DisplayHeaderFirstPage && _pageCount == 1)
                    DrawHeader(e);

                else if (Settings.DisplayHeaderAllPages)
                    DrawHeader(e);

                if (Settings.DisplayPageNumbers)
                    DrawPageNumbers(e.Graphics, _pageCount);

                DrawTabContents(e);
            }
        }

        #region Overrides

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            _pageCount = 0;

            //todo detect tab structure and split pages accordingly
            _printContents = string.Copy(_tab.Contents);

            if (!_performingPageCount)
            {
                _performingPageCount = true;
                _totalPages = GetPageCount();
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

        private void DrawTabContents(PrintPageEventArgs e)
        {
            var printBounds = e.MarginBounds;

            int charactersOnPage;
            int linesPerPage;

            e.Graphics.MeasureString(_printContents, _font, printBounds.Size, StringFormat.GenericTypographic, out charactersOnPage, out linesPerPage);
            e.Graphics.DrawString(_printContents, _font, Brushes.Black, printBounds, StringFormat.GenericTypographic);

            _printContents = _printContents.Substring(charactersOnPage);

            e.HasMorePages = _printContents.Length > 0;
        }

        private void DrawHeader(PrintPageEventArgs e)
        {
            var tableBasePosition = new Point(_pageBounds.X, _pageBounds.Y);
            var tablePosition = tableBasePosition;

            var verticalSeparatorPosition = new Point(tablePosition.X + 60, tablePosition.Y);

            const int propertyValuePadding = 10;
            const int horizontalLineWidth = 550;
            var horizontalLinePadding = (int) e.Graphics.MeasureString("TEST", _font).Height + 3;

            var tableLineColor = Pens.LightGray;
            var tablePropertyColor = Brushes.Gray;
            var tableValueColor = Brushes.Black;

            //artist
            e.Graphics.DrawString("Artist", _font, tablePropertyColor, tablePosition.X, tablePosition.Y);
            e.Graphics.DrawString(_tab.Artist, _font, tableValueColor, verticalSeparatorPosition.X + propertyValuePadding, tablePosition.Y);
            e.Graphics.DrawLine(tableLineColor, tablePosition.X, tablePosition.Y + horizontalLinePadding, horizontalLineWidth, tablePosition.Y + horizontalLinePadding);

            tablePosition.Y += 20;

            //title
            e.Graphics.DrawString("Title", _font, tablePropertyColor, tablePosition.X, tablePosition.Y);
            e.Graphics.DrawString(_tab.Title, _font, tableValueColor, verticalSeparatorPosition.X + propertyValuePadding, tablePosition.Y);
            e.Graphics.DrawLine(tableLineColor, tablePosition.X, tablePosition.Y + horizontalLinePadding, horizontalLineWidth, tablePosition.Y + horizontalLinePadding);

            tablePosition.Y += 20;

            //type
            e.Graphics.DrawString("Type", _font, tablePropertyColor, tablePosition.X, tablePosition.Y);
            e.Graphics.DrawString(_tab.Type.ToFriendlyString(), _font, tableValueColor, verticalSeparatorPosition.X + propertyValuePadding, tablePosition.Y);
            e.Graphics.DrawLine(tableLineColor, tablePosition.X, tablePosition.Y + horizontalLinePadding, horizontalLineWidth, tablePosition.Y + horizontalLinePadding);

            tablePosition.Y += 20;

            if (Settings.DisplayPrintTime)
            {
                e.Graphics.DrawString("Date", _font, tablePropertyColor, tablePosition.X, tablePosition.Y);
                e.Graphics.DrawString(DateTime.Now.ToString(), _font, tableValueColor, verticalSeparatorPosition.X + propertyValuePadding, tablePosition.Y);
                e.Graphics.DrawLine(tableLineColor, tablePosition.X, tablePosition.Y + horizontalLinePadding, horizontalLineWidth, tablePosition.Y + horizontalLinePadding);

                tablePosition.Y += 20;
            }

            //vertical separator
            e.Graphics.DrawLine(tableLineColor, verticalSeparatorPosition.X, tableBasePosition.Y, verticalSeparatorPosition.X, tablePosition.Y - 1);
        }

        private void DrawPageNumbers(Graphics g, int currentPage)
        {
            g.DrawString(string.Format("Page {0} of {1}", currentPage, _totalPages), _font, Brushes.Black, _pageBounds, new StringFormat
                                                                                                                            {
                                                                                                                                Alignment = StringAlignment.Far,
                                                                                                                                LineAlignment = StringAlignment.Near
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

            PointF[] bottomRight = {
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