#region

using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace Tabster.Utilities
{
    internal class TablatureFontManager
    {
        private static readonly PrivateFontCollection PrivateFontCollection;

        static TablatureFontManager()
        {
            PrivateFontCollection = new PrivateFontCollection();
            PrivateFontCollection.AddFontFile(Path.Combine(Application.StartupPath, "Resources/Fonts/SourceCodePro-Regular.ttf"));
        }

        public static Font GetFont(float size = 9F, FontStyle fontStyle = FontStyle.Regular)
        {
            return new Font(PrivateFontCollection.Families.First(), size, fontStyle);
        }
    }
}