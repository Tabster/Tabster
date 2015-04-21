#region

using System.IO;
using System.Text.RegularExpressions;
using Tabster.Core.Types;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Data.Library
{
    public class TabsterFileSystemLibraryBase<TTablatureFile, TTablaturePlaylistFile> : TabsterLibraryBase<TTablatureFile, TTablaturePlaylistFile>
        where TTablatureFile : class, ITablatureFile, new()
        where TTablaturePlaylistFile : class, ITablaturePlaylistFile, new()
    {
        public readonly TabsterFileProcessor<TTablatureFile> TablatureFileProcessor;
        public readonly TabsterFileProcessor<TTablaturePlaylistFile> TablaturePlaylistFileProcessor;

        public TabsterFileSystemLibraryBase(string tablatureDirectory, string playlistDirectory,
            TabsterFileProcessor<TTablatureFile> tablatureFileProcessor, TabsterFileProcessor<TTablaturePlaylistFile> tablaturePlaylistFileProcessor)
        {
            TablatureDirectory = tablatureDirectory;
            PlaylistDirectory = playlistDirectory;
            TablatureFileProcessor = tablatureFileProcessor;
            TablaturePlaylistFileProcessor = tablaturePlaylistFileProcessor;

            if (!Directory.Exists(TablatureDirectory))
                Directory.CreateDirectory(TablatureDirectory);

            if (!Directory.Exists(PlaylistDirectory))
                Directory.CreateDirectory(PlaylistDirectory);
        }

        public string TablatureDirectory { get; private set; }
        public string PlaylistDirectory { get; private set; }

        #region Tablature Methods

        public virtual void LoadTablatureFiles()
        {
            foreach (var file in Directory.GetFiles(TablatureDirectory, string.Format("*{0}", Constants.TablatureFileExtension)))
            {
                var tablatureFile = TablatureFileProcessor.Load(file);

                if (tablatureFile != null)
                {
                    Add(tablatureFile, new FileInfo(file));
                }
            }
        }

        public virtual TablatureLibraryItem<TTablatureFile> Add(AttributedTablature tablature)
        {
            var file = new TTablatureFile
            {
                Artist = tablature.Artist,
                Title = tablature.Title,
                Type = tablature.Type,
                Contents = tablature.Contents,
                Source = tablature.Source,
            };

            return Add(file);
        }

        public virtual TablatureLibraryItem<TTablatureFile> Add(TTablatureFile file, FileInfo fileInfo = null)
        {
            if (fileInfo == null)
            {
                var path = GenerateUniqueFilename(TablatureDirectory, string.Format("{0}{1}", file.ToFriendlyString(), Constants.TablatureFileExtension));
                file.Save(path);
                fileInfo = new FileInfo(path);
            }

            var item = new TablatureLibraryItem<TTablatureFile>(file, fileInfo);
            base.AddTablatureItem(item);
            return item;
        }

        #endregion

        #region Playlist Methods

        public virtual void LoadPlaylistFiles()
        {
            foreach (var file in Directory.GetFiles(PlaylistDirectory, string.Format("*{0}", Constants.TablaturePlaylistFileExtension)))
            {
                var playlistFile = TablaturePlaylistFileProcessor.Load(file);

                if (playlistFile != null)
                {
                    Add(playlistFile, new FileInfo(file));
                }
            }
        }

        public virtual PlaylistLibraryItem<TTablaturePlaylistFile> Add(TTablaturePlaylistFile file, FileInfo fileInfo = null)
        {
            if (fileInfo == null)
            {
                var path = GenerateUniqueFilename(PlaylistDirectory, string.Format("{0}{1}", file.Name, Constants.TablaturePlaylistFileExtension));
                file.Save(path);
                fileInfo = new FileInfo(path);
            }

            var item = new PlaylistLibraryItem<TTablaturePlaylistFile>(file, fileInfo);
            base.AddPlaylistItem(item);
            return item;
        }

        public virtual void RemovePlaylist(TTablaturePlaylistFile file)
        {
            var item = FindPlaylistItemsByFile(file);

            if (item != null)
            {
                RemovePlaylistItem(item);
            }
        }

        #endregion

        private static string GenerateUniqueFilename(string directory, string filename)
        {
            //remove invalid file path characters
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var sanitized = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch))).Replace(filename, "");

            var fileName = Path.GetFileNameWithoutExtension(sanitized);
            var fileExt = Path.GetExtension(sanitized);

            var firstTry = Path.Combine(directory, string.Format("{0}{1}", fileName, fileExt));
            if (!File.Exists(firstTry))
                return firstTry;

            for (var i = 1;; ++i)
            {
                var appendedPath = Path.Combine(directory, string.Format("{0} ({1}){2}", fileName, i, fileExt));

                if (!File.Exists(appendedPath))
                    return appendedPath;
            }
        }
    }
}