#region

using System.Data;
using System.Data.SQLite;
using System.IO;
using Tabster.Data;
using Tabster.Data.Library;
using Tabster.Data.Processing;

#endregion

namespace Tabster.LocalUtilities
{
    internal class SqliteTabsterLibrary<TTablatureFile, TTablaturePlaylistFile> :
        TabsterFileSystemLibraryBase<TTablatureFile, TTablaturePlaylistFile> where TTablatureFile : class, ITablatureFile, new()
        where TTablaturePlaylistFile : class, ITablaturePlaylistFile, new()
    {
        private const string TableTablature = "library_tablature";
        private const string TablePlaylists = "library_playlists";

        private readonly SQLiteConnection _db;

        private readonly bool _scanNeeded;

        public SqliteTabsterLibrary(
            string databasePath,
            string tablatureDirectory,
            string playlistDirectory,
            TabsterFileProcessor<TTablatureFile> tablatureFileProcessor,
            TabsterFileProcessor<TTablaturePlaylistFile> tablaturePlaylistFileProcessor) :
                base(tablatureDirectory, playlistDirectory, tablatureFileProcessor, tablaturePlaylistFileProcessor)
        {
            _scanNeeded = !File.Exists(databasePath);

            _db = new SQLiteConnection(string.Format("Data Source={0};Version=3;Compress=True;", databasePath));
            _db.Open();

            CreateTables();
        }

        public void Load()
        {
            if (_scanNeeded)
            {
                LoadTablatureFiles();
                LoadPlaylistFiles();
            }

            else
            {
                //tablature
                using (var cmd = new SQLiteCommand(string.Format("SELECT * FROM {0}", TableTablature), _db))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = long.Parse(reader["id"].ToString());
                            var filename = reader["filename"].ToString();
                            var favorite = bool.Parse(reader["favorite"].ToString());
                            var views = int.Parse(reader["views"].ToString());

                            var file = TablatureFileProcessor.Load(filename);

                            if (file != null)
                            {
                                var fileInfo = new FileInfo(filename);
                                var item = new TablatureLibraryItem<TTablatureFile>(file, fileInfo)
                                {
                                    ID = id,
                                    Favorited = favorite,
                                    Views = views
                                };

                                AddTablatureItem(item);
                            }
                        }
                    }
                }

                //playlists
                using (var cmd = new SQLiteCommand(string.Format("SELECT * FROM {0}", TablePlaylists), _db))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = long.Parse(reader["id"].ToString());
                            var filename = reader["filename"].ToString();

                            var file = TablaturePlaylistFileProcessor.Load(filename);

                            if (file != null)
                            {
                                var fileInfo = new FileInfo(filename);
                                var item = new PlaylistLibraryItem<TTablaturePlaylistFile>(file, fileInfo)
                                {
                                    ID = id
                                };

                                AddPlaylistItem(item);
                            }
                        }
                    }
                }
            }
        }

        public void Save()
        {
            using (var trans = _db.BeginTransaction())
            {
                //tablature entries
                foreach (var item in GetTablatureItems())
                {
                    using (var cmd = new SQLiteCommand(_db))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format(@"INSERT OR REPLACE INTO {0} (id, filename, favorite, views) VALUES (@id, @filename, @favorite, @views)", TableTablature);
                        cmd.Parameters.Add(new SQLiteParameter("@id", item.ID));
                        cmd.Parameters.Add(new SQLiteParameter("@filename", item.FileInfo.FullName));
                        cmd.Parameters.Add(new SQLiteParameter("@favorite", item.Favorited));
                        cmd.Parameters.Add(new SQLiteParameter("@views", item.Views));
                        cmd.ExecuteNonQuery();

                        item.ID = _db.LastInsertRowId;
                    }
                }

                //playlist entries
                foreach (var item in GetPlaylistItems())
                {
                    using (var cmd = new SQLiteCommand(_db))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format(@"INSERT OR REPLACE INTO {0} (id, filename) VALUES (@id, @filename)", TablePlaylists);
                        cmd.Parameters.Add(new SQLiteParameter("@id", item.ID));
                        cmd.Parameters.Add(new SQLiteParameter("@filename", item.FileInfo.FullName));
                        cmd.ExecuteNonQuery();

                        item.ID = _db.LastInsertRowId;
                    }
                }

                trans.Commit();
            }
        }

        private void CreateTables()
        {
            using (var cmd = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS {0} (id INTEGER PRIMARY KEY, filename TEXT, favorite BOOLEAN, views INTEGER)", TableTablature), _db))
            {
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS {0} (id INTEGER, views TEXT)", TablePlaylists), _db))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}