#region

using System;
using System.Text;

#endregion

namespace Tabster
{
    public static class Common
    {
        public static string TruncateVersion(string version)
        {
            while (version.EndsWith("0") || version.EndsWith("."))
            {
                version = version.Remove(version.Length - 1, 1);
            }

            return version;
        }
    }
}