#region

using System.IO;

#endregion

namespace Tabster.Data
{
    public class TablaturePlaylistItem
    {
        public TablaturePlaylistItem(ITablatureFile file, FileInfo fileInfo)
        {
            File = file;
            FileInfo = fileInfo;
        }

        public ITablatureFile File { get; private set; }
        public FileInfo FileInfo { get; private set; }
    }
}