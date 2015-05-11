#region

using System.Drawing;

#endregion

namespace Tabster.WinForms
{
    public static class TablatureDisplayFont
    {
        public static Font GetFont()
        {
            return new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }
    }
}