#region

using System.Data;
using System.Data.SQLite;
using System.IO;
using Tabster.Core.Types;
using Tabster.Data.Binary;
using Tabster.Data.Library;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Database
{
    internal class LibraryManager : TablatureFileLibrary<TablatureFile>
    {
        private readonly TabsterDatabaseHelper _databaseHelper;
        private readonly TabsterFileProcessor<TablatureFile> _fileProcessor;

        public LibraryManager(TabsterDatabaseHelper databaseHelper, TabsterFileProcessor<TablatureFile> fileProcessor,
            string tablatureDirectory) : base(tablatureDirectory, fileProcessor)
        {
            _databaseHelper = databaseHelper;
            _fileProcessor = fileProcessor;
        }

        public int Count
        {
            get { return GetTablatureItems().Count; }
        }

        public void Load(bool fileSystem)
        {
            if (fileSystem)
            {
                LoadTablatureFiles();
            }

            else
            {
                using (var cmd = new SQLiteCommand(string.Format("SELECT * FROM {0}", TabsterDatabaseHelper.TableLibraryItems), _databaseHelper.GetConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = long.Parse(reader["id"].ToString());
                            var filename = reader["filename"].ToString();
                            var favorite = bool.Parse(reader["favorite"].ToString());
                            var views = int.Parse(reader["views"].ToString());
                            var rating = TablatureRatingUtilities.FromString(reader["rating"].ToString());

                            var file = _fileProcessor.Load(filename);

                            if (file != null)
                            {
                                var fileInfo = new FileInfo(filename);
                                var item = new TablatureLibraryItem<TablatureFile>(file, fileInfo)
                                {
                                    Id = id,
                                    Favorited = favorite,
                                    Views = views,
                                    Rating = rating
                                };

                                base.Add(item);
                            }
                        }
                    }
                }
            }
        }

        public void Save()
        {
            using (var trans = _databaseHelper.GetConnection().BeginTransaction())
            {
                foreach (var item in GetTablatureItems())
                {
                    Update(item);
                }

                trans.Commit();
            }
        }

        public void Update(TablatureLibraryItem<TablatureFile> libraryItem)
        {
            using (var cmd = new SQLiteCommand(_databaseHelper.GetConnection()))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(@"INSERT OR REPLACE INTO {0} (id, filename, favorite, views, rating) VALUES (@id, @filename, @favorite, @views, @rating)", TabsterDatabaseHelper.TableLibraryItems);
                cmd.Parameters.Add(new SQLiteParameter("@id", libraryItem.Id));
                cmd.Parameters.Add(new SQLiteParameter("@filename", libraryItem.FileInfo.FullName));
                cmd.Parameters.Add(new SQLiteParameter("@favorite", libraryItem.Favorited));
                cmd.Parameters.Add(new SQLiteParameter("@views", libraryItem.Views));
                cmd.Parameters.Add(new SQLiteParameter("@rating", TablatureRatingUtilities.ToInt(libraryItem.Rating)));
                cmd.ExecuteNonQuery();

                libraryItem.Id = _databaseHelper.GetConnection().LastInsertRowId;
            }
        }

        public override bool Remove(TablatureLibraryItem<TablatureFile> libraryItem)
        {
            using (var cmd = new SQLiteCommand(_databaseHelper.GetConnection()))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(@"DELETE FROM {0} WHERE id=@id", TabsterDatabaseHelper.TableLibraryItems);
                cmd.Parameters.Add(new SQLiteParameter("@id", libraryItem.Id));
                cmd.ExecuteNonQuery();
            }

            return base.Remove(libraryItem);
        }
    }
}