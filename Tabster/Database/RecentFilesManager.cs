#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Tabster.Data.Binary;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Database
{
    internal class RecentFile
    {
        public RecentFile()
        {
        }

        public RecentFile(TablatureFile tablatureFile, FileInfo fileInfo, DateTime accessed)
        {
            TablatureFile = tablatureFile;
            FileInfo = fileInfo;
            Accessed = accessed;
        }

        public int? ID { get; set; }
        public TablatureFile TablatureFile { get; private set; }
        public FileInfo FileInfo { get; private set; }
        public DateTime? Accessed { get; private set; }
    }

    internal class RecentFilesManager
    {
        public const int MaxItems = 20;
        private readonly TabsterDatabaseHelper _databaseHelper;
        private readonly TabsterFileProcessor<TablatureFile> _fileProcessor;

        private readonly List<RecentFile> _items = new List<RecentFile>();
        private int _lastUsedId = 1;

        public RecentFilesManager(TabsterDatabaseHelper databaseHelper, TabsterFileProcessor<TablatureFile> fileProcessor)
        {
            _databaseHelper = databaseHelper;
            _fileProcessor = fileProcessor;
        }

        public List<RecentFile> GetItems()
        {
            var items = _items.Where(x => x.FileInfo != null);
            return items.OrderBy(o => o.Accessed).ToList();
        }

        public void Load()
        {
            _lastUsedId = 1;
            _items.Clear();

            using (var cmd = new SQLiteCommand(string.Format("SELECT * FROM {0} ORDER BY id ASC", TabsterDatabaseHelper.TableRecentFiles), _databaseHelper.GetConnection()))
            {
                var count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 0)
                {
                    CreateDummyEntries();
                    Load();
                    return;
                }

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = int.Parse(reader["id"].ToString());
                        var filename = reader["filename"].ToString();
                        var accessed = reader["accessed"].ToString();

                        if (!string.IsNullOrEmpty(filename))
                        {
                            var file = _fileProcessor.Load(filename);

                            if (file != null)
                            {
                                var r = new RecentFile(file, new FileInfo(filename), TabsterDatabaseHelper.UnixTimestampToDateTime(int.Parse(accessed))) {ID = id};
                                _items.Add(r);

                                if (id > _lastUsedId)
                                    _lastUsedId = id;
                            }
                        }

                        else
                        {
                            _items.Add(new RecentFile {ID = id});
                        }
                    }
                }
            }
        }

        public void Save()
        {
            using (var trans = _databaseHelper.GetConnection().BeginTransaction())
            {
                foreach (var item in _items)
                {
                    using (var cmd = new SQLiteCommand(_databaseHelper.GetConnection()))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format(@"UPDATE {0} SET filename=@filename, accessed=@accessed WHERE id=@id", TabsterDatabaseHelper.TableRecentFiles);
                        cmd.Parameters.Add(new SQLiteParameter("@id", item.ID));
                        cmd.Parameters.Add(new SQLiteParameter("@filename", item.FileInfo == null ? "" : item.FileInfo.FullName));
                        cmd.Parameters.Add(new SQLiteParameter("@accessed", item.Accessed == null ? 0 : TabsterDatabaseHelper.GetUnixTimestamp(item.Accessed.Value)));
                        cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
            }
        }

        public void Add(RecentFile recentFile)
        {
            var populatedItems = new List<RecentFile>(_items.Where(x => x.FileInfo != null).OrderByDescending(p => p.ID));
            if (populatedItems.Count == MaxItems)
            {
                var next = populatedItems.First();
                recentFile.ID = next.ID;
                _items.Remove(next);
            }
            else
            {
                _lastUsedId++;
                recentFile.ID = _lastUsedId;
            }

            _items.Add(recentFile);
        }

        public void Clear()
        {
            using (var cmd = new SQLiteCommand(string.Format("DELETE FROM {0}", TabsterDatabaseHelper.TableRecentFiles), _databaseHelper.GetConnection()))
            {
                cmd.ExecuteNonQuery();
            }

            _lastUsedId = 1;

            CreateDummyEntries();
            Load();
        }

        private void CreateDummyEntries()
        {
            //create dummy entries
            using (var trans = _databaseHelper.GetConnection().BeginTransaction())
            {
                for (var i = 0; i < MaxItems; i++)
                {
                    using (var cmd = new SQLiteCommand(string.Format("INSERT INTO {0} (filename, accessed) VALUES ('', 0)", TabsterDatabaseHelper.TableRecentFiles), _databaseHelper.GetConnection()))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
            }
        }
    }
}