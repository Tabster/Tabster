#region

using System.IO;

#endregion

namespace Tabster
{
    public static class Common
    {
        public static string TruncateVersion(string version, bool enforceDecimal = true)
        {
            while (version.EndsWith("0") || version.EndsWith("."))
            {
                version = version.Remove(version.Length - 1, 1);
            }

            if (enforceDecimal && !version.Contains("."))
            {
                version = string.Format("{0}.0", version);
            }

            return version;
        }

        public static bool IsFilePath(string path, bool checkExists)
        {
            try
            {
                Path.GetFullPath(path);
                return !checkExists || File.Exists(path);
            }

            catch
            {
                return false;
            }
        }
    }
}