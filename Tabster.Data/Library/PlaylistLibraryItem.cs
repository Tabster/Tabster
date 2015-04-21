#region

using System.IO;

#endregion

namespace Tabster.Data.Library
{
    public class PlaylistLibraryItem<TTablaturePlaylistFile> : LibraryItem where TTablaturePlaylistFile : ITablaturePlaylistFile
    {
        public PlaylistLibraryItem(TTablaturePlaylistFile file, FileInfo fileInfo)
            : base(file, fileInfo)
        {
            File = file;
        }

        public new TTablaturePlaylistFile File { get; private set; }
    }
}