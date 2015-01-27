#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    public class EllipsizedTabPage : TabPage
    {
        private const string EllipseSuffix = "...";
        private string _originalText;

        public new string Text
        {
            get { return base.Text; }
            set
            {
                _originalText = value;
                base.Text = value;
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            EllipsizeText();
            base.OnParentChanged(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            EllipsizeText();
            base.OnTextChanged(e);
        }

        private void EllipsizeText()
        {
            var tabControl = Parent as TabControl;

            if (tabControl == null)
                return;

            var headerSize = tabControl.ItemSize;
            var tabPageText = _originalText.TrimEnd();

            using (var g = CreateGraphics())
            {
                var needsResized = g.MeasureString(tabPageText, Font).Width >= headerSize.Width;

                if (needsResized)
                {
                    while ((g.MeasureString(tabPageText, Font)).Width >= headerSize.Width)
                    {
                        var alreadyEllipsized = tabPageText.EndsWith(EllipseSuffix);
                        var amountToTrim = alreadyEllipsized ? (EllipseSuffix.Length*2) + 1 : EllipseSuffix.Length + 1;

                        tabPageText = tabPageText.Substring(0, tabPageText.Length - amountToTrim).TrimEnd();
                        tabPageText += EllipseSuffix;
                    }
                }
            }

            base.Text = tabPageText;
        }
    }
}