#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public RecentFile(TablatureFile tablatureFile, FileInfo fileInfo)
        {
            TablatureFile = tablatureFile;
            FileInfo = fileInfo;
        }

        public TablatureFile TablatureFile { get; private set; }
        public FileInfo FileInfo { get; private set; }
    }

    internal class RecentFilesManager
    {
        private readonly TabsterDatabaseHelper _databaseHelper;
        private readonly TabsterFileProcessor<TablatureFile> _fileProcessor;

        private readonly List<RecentFile> _items = new List<RecentFile>();
        private int _maxItems;

        public RecentFilesManager(TabsterDatabaseHelper databaseHelper, TabsterFileProcessor<TablatureFile> fileProcessor, int maxItems)
        {
            _databaseHelper = databaseHelper;
            _fileProcessor = fileProcessor;
            _maxItems = maxItems;
        }

        public int MaxItems
        {
            get { return _maxItems; }
            set
            {
                _maxItems = value;

                if (_items.Count > _maxItems)
                    _items.RemoveRange(_maxItems - 1, _items.Count - _maxItems);
            }
        }

        public ReadOnlyCollection<RecentFile> GetItems()
        {
            return _items.AsReadOnly();
        }

        public void Load()
        {
            _items.Clear();

            using (var cmd = new SQLiteCommand(string.Format("SELECT * FROM {0} ORDER BY id DESC", TabsterDatabaseHelper.TableRecentFiles), _databaseHelper.GetConnection()))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var filename = reader["filename"].ToString();

                        var file = _fileProcessor.Load(filename);

                        if (file != null)
                        {
                            var r = new RecentFile(file, new FileInfo(filename));
                            _items.Add(r);
                        }
                    }
                }
            }
        }

        public void Save()
        {
            ClearInternal();

            using (var trans = _databaseHelper.GetConnection().BeginTransaction())
            {
                foreach (var item in _items)
                {
                    using (var cmd = new SQLiteCommand(_databaseHelper.GetConnection()))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format(@"INSERT INTO {0} (filename) VALUES (@filename)", TabsterDatabaseHelper.TableRecentFiles);
                        cmd.Parameters.Add(new SQLiteParameter("@filename", item.FileInfo.FullName));
                        cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
            }
        }

        public void Add(RecentFile recentFile)
        {
            var filenameEntry = _items.Find(x => x.FileInfo.FullName.Equals(recentFile.FileInfo.FullName));
            if (filenameEntry != null)
                _items.Remove(filenameEntry);

            if (_items.Count == _maxItems)
                _items.Remove(_items.Last());

            _items.Insert(0, recentFile);
        }

        public void Clear()
        {
            ClearInternal();
            _items.Clear();
        }

        private void ClearInternal()
        {
            using (var cmd = new SQLiteCommand(string.Format("DELETE FROM {0}", TabsterDatabaseHelper.TableRecentFiles), _databaseHelper.GetConnection()))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}