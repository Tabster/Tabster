#region

using System.Windows.Forms;
using Tabster.Core.Data;

#endregion

namespace Tabster.Controls
{
    public class BasicTablatureTextEditor : TablatureTextEditorBase<TextBox>, ITablatureTextEditor
    {
        public BasicTablatureTextEditor(TextBox host) : base(host)
        {
        }

        public BasicTablatureTextEditor() : base(GenerateTextbox())
        {
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

        private static TextBox GenerateTextbox()
        {
            return new TextBox
                       {
                           BorderStyle = BorderStyle.None,
                           Dock = DockStyle.Fill,
                           Multiline = true,
                           ScrollBars = ScrollBars.Both,
                           Font = TablatureDisplayFont.GetFont()
                       };
        }

        public void LoadTab(TablatureDocument document)
        {
            TextBoxBase.Text = document.Contents;
        }
    }
}