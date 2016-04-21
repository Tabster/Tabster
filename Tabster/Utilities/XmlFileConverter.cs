#region

using System;
using System.IO;
using log4net.Repository.Hierarchy;
using Tabster.Data;
using Tabster.Data.Processing;
using Tabster.Data.Xml;
using Tabster.Database;

#endregion

namespace Tabster.Utilities
{
    internal static class XmlFileConverter
    {
        /// <summary>
        ///     Convert Xml-based files to binary.
        /// </summary>
        public static void ConvertXmlFiles(PlaylistManager playlistManager, LibraryManager libraryManager)
        {
            var playlistsDirectory = Path.Combine(TabsterEnvironment.GetEnvironmentDirectoryPath(TabsterEnvironmentDirectory.UserData), "Playlists");

            // playlists are no longer stored as files, but are now stored in database
            if (Directory.Exists(playlistsDirectory))
            {
#pragma warning disable 612
                var playlistProcessor = new TabsterFileProcessor<TablaturePlaylistDocument>(TablaturePlaylistDocument.FileVersion);
#pragma warning restore 612

                foreach (var file in Directory.GetFiles(playlistsDirectory, string.Format("*{0}", Constants.TablaturePlaylistFileExtension), SearchOption.AllDirectories))
                {
                    var playlistFile = playlistProcessor.Load(file);

                    if (playlistFile != null)
                    {
                        var playlist = new TablaturePlaylist(playlistFile.Name) {Created = playlistFile.FileAttributes.Created};

                        foreach (var item in playlistFile)
                        {
                            playlist.Add(item);
                        }

                        playlistManager.Update(playlist);

                        try
                        {
                            File.Delete(file);
                        }

                        catch(Exception ex)
                        {
                            Logging.GetLogger().Error(string.Format("Error occured during playlist conversion: {0}", file), ex);
                        }
                    }
                }
            }

            if (Directory.Exists(libraryManager.TablatureDirectory))
            {
                foreach (var file in Directory.GetFiles(libraryManager.TablatureDirectory, string.Format("*{0}", Constants.TablatureFileExtension), SearchOption.AllDirectories))
                {
                    var tablatureFile = TabsterXmlFileConverter.ConvertTablatureDocument(file);

                    if (tablatureFile != null)
                        tablatureFile.Save(file);
                }
            }
        }
    }
}