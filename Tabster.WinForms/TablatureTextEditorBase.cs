#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Tabster.Core.Types;
using Tabster.Printing;

#endregion

namespace Tabster.WinForms
{
    public abstract class TablatureTextEditorBase : Control
    {
        protected TablatureTextEditorBase(TextBoxBase textBoxBase)
        {
            TextBoxBase = textBoxBase;
            TextBoxBase.KeyDown += TextBoxBase_KeyDown;
            TextBoxBase.ModifiedChanged += TextBoxBase_ModifiedChanged;

            Controls.Add(textBoxBase);
        }

        protected TextBoxBase TextBoxBase { get; private set; }

        public new Font Font
        {
            get { return TextBoxBase.Font; }
            set { TextBoxBase.Font = value; }
        }

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

        public virtual void LoadTablature(ITablature tablature)
        {
            TextBoxBase.Text = tablature.Contents;

            if (TablatureLoaded != null)
            {
                TablatureLoaded(this, EventArgs.Empty);
            }
        }

        public void Clear()
        {
            TextBoxBase.Clear();
        }

        public new void Focus()
        {
            Focus(true);
        }

        public void Focus(bool selectAll)
        {
            TextBoxBase.Focus();

            if (!selectAll)
                TextBoxBase.Select(0, 0);
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

        public float FontSize
        {
            get { return TextBoxBase.Font.Size; }
            set
            {
                if (value <= 0)
                    return;

                TextBoxBase.Font = new Font(TextBoxBase.Font.FontFamily, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasScrollableContents
        {
            get
            {
                var size = TextRenderer.MeasureText(TextBoxBase.Text, TextBoxBase.Font);
                return size.Width >= TextBoxBase.Width || size.Height >= TextBoxBase.Height;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified
        {
            get { return TextBoxBase.Modified; }
            set { TextBoxBase.Modified = value; }
        }

        #endregion

        #region Events

        public event EventHandler ContentsModified;
        public event EventHandler TablatureLoaded;

        #endregion

        #region Printing

        public void PrintPreview(TablaturePrintDocumentSettings settings = null)
        {
            var documentName = string.Format("Tablature File {0}", DateTime.Now);

            using (
                var printDocument = new TablaturePrintDocument(new AttributedTablature {Contents = Text},
                    TextBoxBase.Font)
                {
                    DocumentName = documentName,
                    Settings = settings ?? new TablaturePrintDocumentSettings()
                })
            {
                using (var dialog = new PrintPreviewDialog {Document = printDocument})
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        printDocument.Print();
                    }
                }
            }
        }

        public void Print(TablaturePrintDocumentSettings settings = null)
        {
            var documentName = string.Format("Tablature File {0}", DateTime.Now);

            using (
                var printDocument = new TablaturePrintDocument(new AttributedTablature {Contents = Text},
                    TextBoxBase.Font)
                {
                    DocumentName = documentName,
                    Settings = settings ?? new TablaturePrintDocumentSettings()
                })
            {
                printDocument.Print();
            }
        }

        #endregion
    }
}