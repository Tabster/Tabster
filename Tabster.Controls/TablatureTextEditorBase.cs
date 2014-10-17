#region

using System;
using System.Drawing;
using System.Windows.Forms;
using Tabster.Core.Printing;

#endregion

namespace Tabster.Controls
{
    public class TablatureTextEditorBase<TTextBoxBase> : Control where TTextBoxBase : TextBoxBase
    {
        public TablatureTextEditorBase(TTextBoxBase textBoxBase)
        {
            TextBoxBase = textBoxBase;
            TextBoxBase.Font = TablatureDisplayFont.GetFont();
            TextBoxBase.KeyDown += TextBoxBase_KeyDown;
            TextBoxBase.ModifiedChanged += TextBoxBase_ModifiedChanged;

            Controls.Add(textBoxBase);
        }

        protected TTextBoxBase TextBoxBase { get; private set; }

        private void TextBoxBase_ModifiedChanged(object sender, EventArgs e)
        {
            if (ContentsModified != null)
            {
                ContentsModified(this, EventArgs.Empty);
            }
        }

        private void TextBoxBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                TextBoxBase.SelectAll();
                e.Handled = true;
            }
        }

        public void ScrollToPosition(int position)
        {
            TextBoxBase.SelectionStart = position;
            TextBoxBase.ScrollToCaret();
        }

        public void ScrollToLine(int finish)
        {
            var position = 0;

            for (var i = 0; i < finish && i < TextBoxBase.Lines.Length; i++)
            {
                position += TextBoxBase.Lines[i].Length;
                position += Environment.NewLine.Length;
            }

            ScrollToPosition(position);
        }

        #region Properties

        public new Color ForeColor
        {
            get { return TextBoxBase.ForeColor; }
            set { TextBoxBase.ForeColor = value; }
        }

        public new Color BackColor
        {
            get { return TextBoxBase.BackColor; }
            set { TextBoxBase.BackColor = value; }
        }

        public new string Text
        {
            get { return TextBoxBase.Text; }
            set { TextBoxBase.Text = value; }
        }

        public bool ReadOnly
        {
            get { return TextBoxBase.ReadOnly; }
            set
            {
                TextBoxBase.ReadOnly = value;
                TextBoxBase.BackColor = SystemColors.Window;
            }
        }

        public bool Modified
        {
            get { return TextBoxBase.Modified; }
            set { TextBoxBase.Modified = value; }
        }



        #endregion

        #region Events

        public event EventHandler ContentsModified;

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
            //todo print
            /*
            var documentName = string.Format("Tablature Document {0}", DateTime.Now.ToString());

            using (var printDocument = new TablaturePrintDocument(Tablature, TextBoxBase.Font) {DocumentName = documentName, Settings = PrintSettings})
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
            }*/
        }

        #endregion

        #region AutoScroll

        private bool _autoScroll;

        //todo rename
        public bool AutoScroll
        {
            get { return _autoScroll; }
            set
            {
                _autoScroll = value;

                if (!value)
                    ScrollToPosition(0);
            }
        }

        #endregion
    }
}