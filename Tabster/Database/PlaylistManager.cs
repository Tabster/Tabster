#region

using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Tabster.Data;

#endregion

namespace Tabster.Database
{
    internal class PlaylistManager
    {
        private readonly TabsterDatabaseHelper _databaseHelper;

        private readonly List<TablaturePlaylist> _playlists = new List<TablaturePlaylist>();

        public PlaylistManager(TabsterDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public int Count
        {
            get { return _playlists.Count; }
        }

        public void Load()
        {
            _playlists.Clear();

            using (var cmd = new SQLiteCommand(string.Format("SELECT * FROM {0}", TabsterDatabaseHelper.TablePlaylists), _databaseHelper.GetConnection()))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = long.Parse(reader["id"].ToString());
                        var name = reader["name"].ToString();
                        var created = reader["created"].ToString();
                        var playlist = new TablaturePlaylist(name) {Id = id, Created = TabsterDatabaseHelper.UnixTimestampToDateTime(int.Parse(created))};
                        _playlists.Add(playlist);
                    }
                }
            }
        }

        public List<TablaturePlaylist> GetPlaylists()
        {
            return new List<TablaturePlaylist>(_playlists);
        }

        public void Update(TablaturePlaylist playlist)
        {
            using (var cmd = new SQLiteCommand(_databaseHelper.GetConnection()))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(@"INSERT OR REPLACE INTO {0} (id, name, created) VALUES (@id, @name, @created)", TabsterDatabaseHelper.TablePlaylists);
                cmd.Parameters.Add(new SQLiteParameter("@id", playlist.Id));
                cmd.Parameters.Add(new SQLiteParameter("@name", playlist.Name));
                cmd.Parameters.Add(new SQLiteParameter("@created", TabsterDatabaseHelper.GetUnixTimestamp(playlist.Created.Value)));
                cmd.ExecuteNonQuery();
            }

            playlist.Id = _databaseHelper.GetConnection().LastInsertRowId;

            RemovePlaylistItems(playlist);

            foreach (var item in playlist)
            {
                using (var cmd = new SQLiteCommand(_databaseHelper.GetConnection()))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format(@"INSERT INTO {0} (filename, playlist_id) VALUES (@filename, @playlist_id)", TabsterDatabaseHelper.TablePlaylistItems);
                    cmd.Parameters.Add(new SQLiteParameter("@filename", item.FileInfo.FullName));
                    cmd.Parameters.Add(new SQLiteParameter("@playlist_id", playlist.Id));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Remove(TablaturePlaylist playlist)
        {
            using (var cmd = new SQLiteCommand(_databaseHelper.GetConnection()))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(@"DELETE FROM {0} WHERE id=@id", TabsterDatabaseHelper.TablePlaylists);
                cmd.Parameters.Add(new SQLiteParameter("@id", playlist.Id));
                cmd.ExecuteNonQuery();
            }

            RemovePlaylistItems(playlist);
        }

        private void RemovePlaylistItems(TablaturePlaylist playlist)
        {
            using (var cmd = new SQLiteCommand(_databaseHelper.GetConnection()))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(@"DELETE FROM {0} WHERE playlist_id=@playlist_id", TabsterDatabaseHelper.TablePlaylistItems);
                cmd.Parameters.Add(new SQLiteParameter("@playlist_id", playlist.Id));
                cmd.ExecuteNonQuery();
            }
        }
    }
}