#region

using System.Windows.Forms;
using Tabster.Core.Data.Processing;

#endregion

namespace Tabster.LocalUtilities
{
    internal static class Common
    {
        public static string GetTablatureDocumentMethodString(ITablatureWebpageImporter importer = null)
        {
            var str = string.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);

            if (importer != null && !string.IsNullOrEmpty(importer.SiteName))
            {
                str += string.Format(" / {0}", importer.SiteName);
            }

            return str;
        }
    }
}