#region

using System.Windows.Forms;

#endregion

namespace Tabster.WinForms
{
    public class BasicTablatureTextEditor : TablatureTextEditorBase
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
            };
        }
    }
}