#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    /* TextBoxExtended
     * 
     * Features:
     * 
     * * Placeholder text
     * * Delayed TextChanged event 
     * * Ctrl+A support
    */

    internal class TextBoxExtended : TextBox
    {
        //windows constants

        private const int WM_SETFOCUS = 7;
        private const int WM_KILLFOCUS = 8;
        private const int WM_PAINT = 15;

        public TextBoxExtended()
        {
            _delayTimer.Tick += _delayTimer_Tick;
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Behavior")]
        [Description("Automatically select all text when focused.")]
        public bool SelectOnFocus { get; set; }

        #region Delay

        private readonly Timer _delayTimer = new Timer();
        private bool _delayTimerElapsed;
        private bool _keysPressed;

        private int _textChangedDelay;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Behavior")]
        [Description("The amount of time, in milliseconds to wait after user input has stopped to trigger the TextChanged event.")]
        public int TextChangedDelay
        {
            get { return _textChangedDelay; }
            set
            {
                _textChangedDelay = value;

                if (value > 0)
                    _delayTimer.Interval = _textChangedDelay;
            }
        }

        private void _delayTimer_Tick(object sender, EventArgs e)
        {
            _delayTimer.Stop();
            _delayTimerElapsed = true;
            base.OnTextChanged(e);
        }

        #endregion

        #region Placeholder

        private Color _placeholderForecolor = SystemColors.GrayText;
        private bool _placeholderNeedsDrawn = true;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        [Description("The text to display while the control is not populated or focused.")]
        public string PlaceholderText { get; set; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        [Description("The Forecolor of the placeholder text.")]
        public Color PlaceholderForecolor
        {
            get { return _placeholderForecolor; }
            set { _placeholderForecolor = value; }
        }

        #endregion

        public new void Clear()
        {
            base.Clear();
        }

        protected override void OnTextAlignChanged(EventArgs e)
        {
            //re-draw the control when the text alignment changes
            base.OnTextAlignChanged(e);
            Invalidate();
        }

        protected override void OnEnter(EventArgs e)
        {
            if (SelectOnFocus && Text.Length > 0)
                SelectAll();

            base.OnEnter(e);
        }

        protected virtual void DrawPlaceholder()
        {
            using (var g = CreateGraphics())
            {
                DrawPlaceholder(g);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_placeholderNeedsDrawn && Text.Length == 0)
                DrawPlaceholder(e.Graphics);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ShortcutsEnabled)
            {
                if (keyData == (Keys.Control | Keys.A))
                {
                    SelectAll();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected virtual void DrawPlaceholder(Graphics g)
        {
            var flags = TextFormatFlags.NoPadding | TextFormatFlags.Top | TextFormatFlags.EndEllipsis;

            var rect = ClientRectangle;

            switch (TextAlign)
            {
                case HorizontalAlignment.Center:
                    flags = flags | TextFormatFlags.HorizontalCenter;
                    rect.Offset(0, 1);
                    break;

                case HorizontalAlignment.Left:
                    flags = flags | TextFormatFlags.Left;
                    rect.Offset(1, 1);
                    break;

                case HorizontalAlignment.Right:
                    flags = flags | TextFormatFlags.Right;
                    rect.Offset(0, 1);
                    break;
            }

            TextRenderer.DrawText(g, PlaceholderText, Font, rect, PlaceholderForecolor, BackColor, flags);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SETFOCUS:
                    _placeholderNeedsDrawn = false;
                    break;

                case WM_KILLFOCUS:
                    _placeholderNeedsDrawn = true;
                    break;
            }

            base.WndProc(ref m);

            if (m.Msg == WM_PAINT && _placeholderNeedsDrawn && Text.Length == 0 && !GetStyle(ControlStyles.UserPaint))
                DrawPlaceholder();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (_delayTimer.Interval > 0)
            {
                _delayTimer.Stop();
                _delayTimer.Start();
            }

            _keysPressed = true;

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (_delayTimer.Interval > 0 && (_keysPressed || !_delayTimerElapsed))
                return;

            if (PlaceholderText != null && Text.Length == 0)
            {
                DrawPlaceholder();
            }

            base.OnTextChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (_delayTimer != null)
            {
                _delayTimer.Stop();
                _delayTimer.Tick -= _delayTimer_Tick;
            }

            base.Dispose(disposing);
        }
    }
}