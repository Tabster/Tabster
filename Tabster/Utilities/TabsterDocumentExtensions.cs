#region

using System;
using System.IO;
using System.Text.RegularExpressions;
using Tabster.Core.FileTypes;
using Tabster.Core.Types;

#endregion

namespace Tabster.Utilities
{
    public static class TabsterDocumentExtensions
    {
        public static string GenerateUniqueFilename(this ITablaturePlaylist playlist, string directory)
        {
            return GenerateUniqueFilename(directory, playlist.Name + TablaturePlaylistDocument.FILE_EXTENSION);
        }

        public static string GenerateUniqueFilename(this ITablature tab, string directory)
        {
            var friendlyName = tab.ToFriendlyString();
            return GenerateUniqueFilename(directory, friendlyName + TablatureDocument.FILE_EXTENSION);
        }

        private static string GenerateUniqueFilename(string directory, string filename)
        {
            //remove invalid file path characters
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var sanitized = new Regex(String.Format("[{0}]", Regex.Escape(regexSearch))).Replace(filename, "");

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
    }
}