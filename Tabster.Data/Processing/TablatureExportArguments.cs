#region

using System.Drawing;

#endregion

namespace Tabster.Data.Processing
{
    public class TablatureFileExportArguments
    {
        public TablatureFileExportArguments(Font font)
        {
            Font = font;
        }

        public Font Font { get; private set; }
        // todo custom font colors
    }
}