#region

using System;
using System.Data.SQLite;

#endregion

namespace Tabster.Database
{
    internal class TabsterDatabaseHelper
    {
        public const string TableLibraryItems = "library_items";
        public const string TablePlaylists = "playlists";
        public const string TablePlaylistItems = "playlist_items";
        public const string TableRecentFiles = "recent_files";

        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private readonly SQLiteConnection _db;

        public TabsterDatabaseHelper(string databasePath)
        {
            _db = new SQLiteConnection(string.Format("Data Source={0};Version=3;Compress=True;", databasePath));
            _db.Open();

            CreateTables();
        }

        private void CreateTables()
        {
            using (var cmd = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS {0} (id INTEGER PRIMARY KEY, filename TEXT, favorite BOOLEAN, views INTEGER, rating INTEGER)", TableLibraryItems), _db))
            {
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS {0} (id INTEGER PRIMARY KEY, name TEXT, created INTEGER)", TablePlaylists), _db))
            {
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS {0} (id INTEGER PRIMARY KEY, filename TEXT, playlist_id INTEGER)", TablePlaylistItems), _db))
            {
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SQLiteCommand(string.Format("CREATE TABLE IF NOT EXISTS {0} (id INTEGER PRIMARY KEY, filename TEXT, accessed INTEGER)", TableRecentFiles), _db))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public SQLiteConnection GetConnection()
        {
            return _db;
        }

        public static int GetUnixTimestamp(DateTime dateTime)
        {
            return (int) (dateTime.Subtract(Epoch)).TotalSeconds;
        }

        public static DateTime UnixTimestampToDateTime(int timestamp)
        {
            return Epoch.AddSeconds(timestamp);
        }
    }
}