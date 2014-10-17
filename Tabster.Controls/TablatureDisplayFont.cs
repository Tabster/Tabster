#region

using System.Drawing;

#endregion

namespace Tabster.Controls
{
    public static class TablatureDisplayFont
    {
        public static Font GetFont()
        {
            return new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }
    }
}