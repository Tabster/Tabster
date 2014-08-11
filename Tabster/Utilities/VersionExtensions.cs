#region

using System;

#endregion

namespace Tabster.Utilities
{
    public static class VersionExtensions
    {
        public static string ToShortString(this Version version, bool enforceDecimal = true)
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
    }
}