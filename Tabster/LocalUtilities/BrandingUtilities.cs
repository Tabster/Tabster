#region

using System;
using System.Reflection;

#endregion

namespace Tabster.LocalUtilities
{
    internal static class BrandingUtilities
    {
        public static string ToShortVersionString(this Version version, bool enforceDecimal = true)
        {
            var versionStr = version.ToString();

            while (versionStr.EndsWith("0") || versionStr.EndsWith("."))
            {
                versionStr = versionStr.Remove(versionStr.Length - 1, 1);
            }

            if (enforceDecimal && !versionStr.Contains("."))
            {
                versionStr = string.Format("{0}.0", versionStr);
            }

            return versionStr;
        }

        public static string GetCopyrightString(Assembly assembly, string prefix = "Copyright", bool appendCompanyName = true)
        {
            var str = "";

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

            return str;
        }
    }
}