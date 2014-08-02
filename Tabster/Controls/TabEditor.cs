#region

using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Tabster.Core.Types;
using Tabster.Utilities;

#endregion

namespace Tabster.Controls
{
    public partial class TabEditor : UserControl
    {
        #region Constructor

        public TabEditor()
        {
            InitializeComponent();

            _scrollTimer.Interval = 1000;
            _scrollTimer.Tick += _scrollTimer_Tick;
        }

        #endregion

        #region Properties

        private bool _autoScroll;

        public new bool AutoScroll
        {
            get { return _autoScroll; }
            set
            {
                _autoScroll = value;
                ToggleAutoScroll(value);
            }
        }

        public new Color ForeColor
        {
            get { return txtContents.ForeColor; }
            set { txtContents.ForeColor = value; }
        }

        public new Color BackColor
        {
            get { return txtContents.BackColor; }
            set { txtContents.BackColor = value; }
        }

        public bool ReadOnly
        {
            get { return txtContents.ReadOnly; }
            set
            {
                txtContents.ReadOnly = value;
                txtContents.ShortcutsEnabled = !value;
                txtContents.BackColor = SystemColors.Window;
            }
        }

        public ITablature TabData { get; private set; }

        public bool HasBeenModified { get; private set; }

        #endregion

        #region Events

        private SizeF _lineSize;
        private string _oldContents = "";
        private string _originalContents = "";

        public event EventHandler TabLoaded;
        public event EventHandler TabModified;

        private void txtContents_TextChanged(object sender, EventArgs e)
        {
            ModificationCheck();
        }

        #endregion

        #region Public Methods

        public void ModificationCheck()
        {
            if (txtContents.Text.Length != _oldContents.Length && txtContents.Text != _oldContents && txtContents.Text != _originalContents)
            {
                HasBeenModified = true;

                _oldContents = txtContents.Text;
            }

            else
            {
                HasBeenModified = false;
            }

            if (TabModified != null)
                TabModified(this, EventArgs.Empty);
        }

        public void ScrollToPosition(int position)
        {
            txtContents.SelectionStart = position;
            txtContents.ScrollToCaret();
        }

        public void ScrollToLine(int finish)
        {
            var position = 0;

            for (var i = 0; i < finish && i < txtContents.Lines.Length; i++)
            {
                position += txtContents.Lines[i].Length;
                position += Environment.NewLine.Length;
            }

            ScrollToPosition(position);
        }

        public string GetText()
        {
            return txtContents.Text;
        }

        public void SetText(string text)
        {
            txtContents.Text = text;
        }

        public void LoadTab(ITablature t)
        {
            TabData = t;

            if (TabData != null)
            {
                _originalContents = TabData.Contents;
                txtContents.Text = TabData.Contents;

                //get line size
                if (txtContents.Lines.Length > 0)
                {
                    var bmp = new Bitmap(1, 1);
                    using (var graphics = Graphics.FromImage(bmp))
                    {
                        _lineSize = graphics.MeasureString(txtContents.Lines[0], txtContents.Font);
                    }
                }
            }

            if (TabLoaded != null)
                TabLoaded(this, EventArgs.Empty);
        }

        #endregion

        #region AutoScroll

        private readonly Timer _scrollTimer = new Timer();

        private void _scrollTimer_Tick(object sender, EventArgs e)
        {
            var visibleLines = txtContents.Size.Height/_lineSize.Height;
            var totalLines = txtContents.Lines.Length;
            var currentLine = txtContents.GetLineFromCharIndex(txtContents.SelectionStart) + 1;
            var nextLine = currentLine + 2;

            if ((int) visibleLines == totalLines)
                return;

            ScrollToLine(nextLine);
        }

        private void ToggleAutoScroll(bool enabled)
        {
            if (enabled)
            {
                _scrollTimer.Stop();
                _scrollTimer.Start();
            }

            else
            {
                _scrollTimer.Stop();
            }
        }

        #endregion

        #region Printing

        private string stringToPrint;

        private void printPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage;
            int linesPerPage;

            e.Graphics.MeasureString(stringToPrint, txtContents.Font, e.MarginBounds.Size, StringFormat.GenericTypographic, out charactersOnPage, out linesPerPage);
            e.Graphics.DrawString(stringToPrint, txtContents.Font, Brushes.Black, e.MarginBounds, StringFormat.GenericTypographic);
            stringToPrint = stringToPrint.Substring(charactersOnPage);
            e.HasMorePages = (stringToPrint.Length > 0);
        }

        public void Print(bool showDialog = true)
        {
            stringToPrint = txtContents.Text;

            var printDocument = new PrintDocument {DocumentName = TabData.ToFriendlyString()};
            printDocument.PrintPage += printPage;

            if (showDialog)
            {
                using (var dialog = new PrintDialog {Document = printDocument})
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        printDocument.Print();
                    }
                }
            }

            else
            {
                printDocument.Print();
            }
        }

        #endregion
    }
}