#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Tabster.Core.Types;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Data.Library
{
    public class TablatureFileLibrary<TTablatureFile> where TTablatureFile : class, ITablatureFile, new()
    {
        private readonly TabsterFileProcessor<TTablatureFile> _tablatureFileProcessor;

        private readonly List<TablatureLibraryItem<TTablatureFile>> _items = new List<TablatureLibraryItem<TTablatureFile>>();

        public TablatureFileLibrary(string tablatureDirectory,
            TabsterFileProcessor<TTablatureFile> tablatureFileProcessor)
        {
            TablatureDirectory = tablatureDirectory;
            _tablatureFileProcessor = tablatureFileProcessor;

            if (!Directory.Exists(TablatureDirectory))
                Directory.CreateDirectory(TablatureDirectory);
        }

        public string TablatureDirectory { get; private set; }

        public TabsterFileProcessor<TTablatureFile> GetTablatureFileProcessor()
        {
            return _tablatureFileProcessor;
        } 

        public virtual void LoadTablatureFiles()
        {
            foreach (var file in Directory.GetFiles(TablatureDirectory, string.Format("*{0}", Constants.TablatureFileExtension)))
            {
                var tablatureFile = _tablatureFileProcessor.Load(file);

                if (tablatureFile != null)
                {
                    Add(tablatureFile, new FileInfo(file));
                }
            }
        }

        public virtual TablatureLibraryItem<TTablatureFile> Add(AttributedTablature tablature)
        {
            if (tablature == null)
                throw new ArgumentNullException("tablature");

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

        public virtual void Add(TablatureLibraryItem<TTablatureFile> item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _items.Add(item);
        }

        public virtual TablatureLibraryItem<TTablatureFile> Add(TTablatureFile file, FileInfo fileInfo = null)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            if (fileInfo == null)
            {
                var path = GenerateUniqueFilename(TablatureDirectory, string.Format("{0}{1}", file.ToFriendlyString(), Constants.TablatureFileExtension));
                file.Save(path);
                fileInfo = new FileInfo(path);
            }

            var item = new TablatureLibraryItem<TTablatureFile>(file, fileInfo);
            _items.Add(item);
            return item;
        }

        public virtual bool Remove(TablatureLibraryItem<TTablatureFile> item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (File.Exists(item.FileInfo.FullName))
                File.Delete(item.FileInfo.FullName);

            return _items.Remove(item);
        }

        public virtual TablatureLibraryItem<TTablatureFile> FindTablatureItem(Predicate<TablatureLibraryItem<TTablatureFile>> match)
        {
            if (match == null)
                throw new ArgumentNullException("match");

            return _items.Find(match);
        }

        public List<TablatureLibraryItem<TTablatureFile>> FindAllTablatureItems(Predicate<TablatureLibraryItem<TTablatureFile>> match)
        {
            if (match == null)
                throw new ArgumentNullException("match");

            return _items.FindAll(match);
        }

        public virtual TablatureLibraryItem<TTablatureFile> FindTablatureItemByFile(TTablatureFile file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            return _items.Find(x => x.File.Equals(file));
        }

        public virtual TablatureLibraryItem<TTablatureFile> FindTablatureItemByPath(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            return _items.Find(x => x.FileInfo.FullName.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        public virtual List<TablatureLibraryItem<TTablatureFile>> GetTablatureItems()
        {
            return new List<TablatureLibraryItem<TTablatureFile>>(_items);
        }

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