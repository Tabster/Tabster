#region

using System.Reflection;
using System.Windows.Forms;
using Tabster.Core.Data.Processing;

#endregion

namespace Tabster.Utilities
{
    internal static class Common
    {
        private static string _copyrightString;

        public static string GetTablatureDocumentMethodString(ITablatureWebpageImporter importer = null)
        {
            var str = string.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);

            if (importer != null && !string.IsNullOrEmpty(importer.SiteName))
            {
                str += string.Format(" / {0}", importer.SiteName);
            }

            return str;
        }

        public static string GetCopyrightString(string prefix = "Copyright", bool appendCompanyName = true)
        {
            if (_copyrightString != null)
                return _copyrightString;

            var str = "";

            var assembly = Assembly.GetExecutingAssembly();

            var copyrightAttributes = assembly.GetCustomAttributes(typeof (AssemblyCopyrightAttribute), true);
            if (copyrightAttributes.Length > 0)
            {
                var copyrightString = ((AssemblyCopyrightAttribute) copyrightAttributes[0]).Copyright;

                if (!string.IsNullOrEmpty(copyrightString))
                    str = copyrightString;
            }

            if (appendCompanyName)
            {
                string companyString = null;

                var companyAttributes = assembly.GetCustomAttributes(typeof (AssemblyCompanyAttribute), true);
                if (companyAttributes.Length > 0)
                    companyString = ((AssemblyCompanyAttribute) companyAttributes[0]).Company;

                if (!string.IsNullOrEmpty(companyString) && !str.EndsWith(companyString))
                {
                    str += string.Format(" {0}", companyString);
                }
            }

            if (!str.StartsWith(prefix))
                str = str.Insert(0, string.Format("{0} ", prefix));

            _copyrightString = str;

            return str;
        }
    }
}