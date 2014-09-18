#region

using System;
using System.Drawing;
using System.Windows.Forms;
using Tabster.Core.Printing;
using Tabster.Core.Types;

#endregion

namespace Tabster.Controls
{
    public partial class TablatureEditor : UserControl
    {
        #region Constructor

        public TablatureEditor()
        {
            InitializeComponent();
        }

        //disable resize flicker
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_COMPOSITED = 0x02000000;

                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_COMPOSITED;
                return cp;
            }
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

        private void autoscrollTimer_Tick(object sender, EventArgs e)
        {
            var visibleLines = txtContents.Size.Height/_lineSize.Height;
            var totalLines = txtContents.Lines.Length;
            var currentLine = txtContents.GetLineFromCharIndex(txtContents.SelectionStart) + 1;
            var nextLine = currentLine + 2;

            if ((int) visibleLines >= totalLines)
                return;

            ScrollToLine(nextLine);
        }

        private void ToggleAutoScroll(bool enabled)
        {
            if (enabled)
            {
                autoscrollTimer.Stop();
                autoscrollTimer.Start();
            }

            else
            {
                autoscrollTimer.Stop();
            }
        }

        #endregion

        #region Printing

        private bool _showPrintDialog = true;
        public TablaturePrintDocumentSettings PrintSettings { get; set; }

        public bool ShowPrintDialog
        {
            get { return _showPrintDialog; }
            set { _showPrintDialog = value; }
        }

        public void Print()
        {
            var documentName = string.Format("{0} - {1} ({2})", TabData.Artist, TabData.Title, TabData.Type);

            using (var printDocument = new TablaturePrintDocument(TabData, txtContents.Font) {DocumentName = documentName, Settings = PrintSettings})
            {
                if (ShowPrintDialog)
                {
                    using (var dialog = new PrintPreviewDialog {Document = printDocument})
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
        }

        #endregion

        private void txtContents_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                txtContents.SelectAll();
                e.Handled = true;
            }
        }
    }
}