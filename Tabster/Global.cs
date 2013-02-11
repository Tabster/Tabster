#region

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#endregion

namespace Tabster
{
    public enum InvalidLibraryItemReason
    {
        Missing,
        Corrupt,
    }

    public class TabsterWebClient : WebClient
    {
        public TabsterWebClient(IWebProxy proxy = null)
        {
            Proxy = proxy;
            Encoding = Encoding.UTF8;
            Headers.Add("user-agent", string.Format("Tabster {0}", Application.ProductVersion));
        }
    }

    public class Constants
    {
        public const string LibraryFormatVersion = "1.0";
        public const string RegistryTablist = "Software\\Classes\\.tablist";
        public const string RegistryTabster = "Software\\Classes\\.tabster";

        public static readonly string[] TabTypes = {"Guitar Tab", "Guitar Chords", "Bass Tab", "Drum Tab"};
    }

    public static class Global
    {
        public static readonly LibraryManager libraryManager = new LibraryManager();

        //directories/files
        public static string UserDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tabster");
        public static string WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tabster");
        public static string LibraryDirectory = Path.Combine(UserDirectory, "Library");
        public static string PlaylistDirectory = Path.Combine(UserDirectory, "Playlists");
        public static string TempDirectory = Path.Combine(WorkingDirectory, "Temp");
        public static string CachePath = Path.Combine(WorkingDirectory, "library.dat");

        public static bool IsValidFileName(string filename)
        {
            var containsABadCharacter = new Regex(string.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars()))));
            return !containsABadCharacter.IsMatch(filename);
        }

        public static string GenerateUniqueFilename(string directory, string filename)
        {
            var sanitized = RemoveInvalidFilePathCharacters(filename, "");
            //sanitized = Path.GetFileNameWithoutExtension(sanitized);

            var fileName = Path.GetFileNameWithoutExtension(sanitized);
            var fileExt = Path.GetExtension(sanitized);

            var firstTry = Path.Combine(directory, String.Format("{0}{1}", fileName, fileExt));
            if (!File.Exists(firstTry))
                return firstTry;

            for (var i = 1;; ++i)
            {
                var appendedPath = Path.Combine(directory, String.Format("{0} ({1}){2}", fileName, i, fileExt));

                if (!File.Exists(appendedPath))
                    return appendedPath;
            }
        }

        public static string RemoveInvalidFilePathCharacters(string filename, string replaceChar)
        {
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(filename, replaceChar);
        }

        public static string GetTabString(TabType type)
        {
            switch (type)
            {
                case TabType.Guitar:
                    return Constants.TabTypes[0];
                case TabType.Chord:
                    return Constants.TabTypes[1];
                case TabType.Bass:
                    return Constants.TabTypes[2];
                case TabType.Drum:
                    return Constants.TabTypes[3];
            }

            return Constants.TabTypes[0];
        }

        public static TabType GetTabType(string str)
        {
            if (str.Equals("Guitar Tab", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Guitar;
            if (str.Equals("Guitar Chords", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Chord;
            if (str.Equals("Bass Tab", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Bass;
            if (str.Equals("Drum Tab", StringComparison.InvariantCultureIgnoreCase))
                return TabType.Guitar;

            return TabType.Guitar;
        }
    }
}