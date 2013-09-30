using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Tabster
{
    public static class Common
    {
        private static readonly Regex NewlineRegex = new Regex("(?<!\r)\n", RegexOptions.Compiled);

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

        public static string ConvertNewlines(string source)
        {
            return NewlineRegex.Replace(source, Environment.NewLine);
        }

        public static string StripHTML(string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            for (var i = 0; i < source.Length; i++)
            {
                var let = source[i];

                if (let == '<')
                {
                    inside = true;
                    continue;
                }

                if (let == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }

            return new string(array, 0, arrayIndex);
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