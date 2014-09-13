#region

using Tabster.Core.Data;
using Tabster.Core.Types;
using Tabster.Utilities.IO;

#endregion

namespace Tabster.Utilities.Extensions
{
    public static class TabsterDocumentExtensions
    {
        public static string GenerateUniqueFilename(this ITablaturePlaylist playlist, string directory)
        {
            return UniqueFilenameGenerator.GenerateUniqueFilename(directory, playlist.Name + TablaturePlaylistDocument.FILE_EXTENSION);
        }

        public static string GenerateUniqueFilename(this ITablature tab, string directory)
        {
            return UniqueFilenameGenerator.GenerateUniqueFilename(directory, tab.ToFriendlyString() + TablatureDocument.FILE_EXTENSION);
        }
    }
}