#region

using System.Windows.Forms;
using Tabster.Core.Parsing;
using Tabster.Core.Plugins;

#endregion

namespace Tabster.Utilities
{
    internal static class Common
    {
        public static string GetTablatureDocumentMethodString(ITabParser parser = null)
        {
            var str = string.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);

            if (parser != null && !string.IsNullOrEmpty(parser.Name))
            {
                str += string.Format(" / {0} v{1}", parser.Name, parser.Version);
            }

            return str;
        }
    }
}