#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Tabster.Data.Utilities;

#endregion

namespace Tabster.Data.Library
{
    public class TablatureLibraryIndexFile
    {
        public const string FILE_HEADER = "TABLIB";
        protected const int HEADER_SIZE = 16;

        private const bool COMPRESSED = false;
        private static readonly Version FORMAT_VERSION = new Version("1.0");
        private static readonly Encoding FILE_ENCODING = new UTF8Encoding();

        private readonly TablatureFileLibrary _library;

        private readonly List<TablatureLibraryItem> _libraryItems = new List<TablatureLibraryItem>();
        private readonly List<TablaturePlaylistDocument> _playlistFiles = new List<TablaturePlaylistDocument>();

        public TablatureLibraryIndexFile(TablatureFileLibrary library)
        {
            _library = library;
        }

        public ReadOnlyCollection<TablatureLibraryItem> LibraryItems
        {
            get { return _libraryItems.AsReadOnly(); }
        }

        public ReadOnlyCollection<TablaturePlaylistDocument> Playlists
        {
            get { return _playlistFiles.AsReadOnly(); }
        }

        public void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(fs))
                    {
                        ReadHeader(reader);
                        ReadTablatureEntries(reader);
                        ReadPlaylistEntries(reader);
                    }
                }
            }
        }

        private static void WriteFilePath<T>(BinaryWriter writer, T item, FileInfo fileInfo, Dictionary<T, int> directoryTable, string extension)
        {
            writer.Write(directoryTable[item]);

            var usesDefinedExtension = fileInfo.Extension.Equals(extension, StringComparison.OrdinalIgnoreCase);

            var filenameToEncode = usesDefinedExtension ? Path.GetFileNameWithoutExtension(fileInfo.Name) : fileInfo.Name;

            writer.Write(filenameToEncode, FILE_ENCODING);
        }

        private static string ReadFilePath(BinaryReader reader, IList<string> directoryTable, int directoryIndex, string extension)
        {
            var directory = directoryTable[directoryIndex];
            var filename = reader.ReadString(FILE_ENCODING);

            var ext = Path.GetExtension(filename);

            var needsExtension = ext == null || !ext.Equals(extension, StringComparison.OrdinalIgnoreCase);

            var path = Path.Combine(directory, filename);

            if (needsExtension)
                path += extension;

            return path;
        }

        protected virtual Version ReadHeader(BinaryReader reader)
        {
            reader.BaseStream.Position = 0;

            var header = new string(reader.ReadChars(FILE_HEADER.Length));
            var version = reader.ReadString();

            if (header != FILE_HEADER)
                throw new IOException("Invalid or missing header.");

            return new Version(version);
        }

        protected virtual void ReadTablatureEntries(BinaryReader reader)
        {
            _libraryItems.Clear();

            reader.BaseStream.Position = HEADER_SIZE;

            if (reader.PeekChar() == -1)
                return;

            var directoryTable = new List<string>();
            var directoryCount = reader.ReadInt32();

            while (directoryTable.Count < directoryCount)
            {
                var directory = reader.ReadString(FILE_ENCODING);
                directoryTable.Add(directory);
            }

            var fileCount = reader.ReadInt32();

            for (var i = 0; i < fileCount; i++)
            {
                var directoryIndex = reader.ReadInt32();

                var path = ReadFilePath(reader, directoryTable, directoryIndex, TablatureDocument.FILE_EXTENSION);

                if (File.Exists(path))
                {
                    var added = DateTimeUtilities.UnixTimestampToDateTime(reader.ReadInt32());
                    var favorited = reader.ReadBoolean();
                    var views = reader.ReadInt32();

                    var lastViewedValue = reader.ReadInt32();

                    DateTime? lastViewed = null;

                    if (lastViewedValue > 0)
                        lastViewed = DateTimeUtilities.UnixTimestampToDateTime(lastViewedValue);

                    var doc = new TablatureDocument();
                    doc.Load(path);

                    _libraryItems.Add(new TablatureLibraryItem(doc) {Added = added, Favorited = favorited, Views = views, LastViewed = lastViewed});
                }
            }
        }

        protected virtual void ReadPlaylistEntries(BinaryReader reader)
        {
            _playlistFiles.Clear();

            if (reader.PeekChar() == -1)
                return;

            var directoryTable = new List<string>();
            var directoryCount = reader.ReadInt32();

            while (directoryTable.Count < directoryCount)
            {
                var directory = reader.ReadString();
                directoryTable.Add(directory);
            }

            var fileCount = reader.ReadInt32();

            for (var i = 0; i < fileCount; i++)
            {
                var directoryIndex = reader.ReadInt32();

                var path = ReadFilePath(reader, directoryTable, directoryIndex, TablaturePlaylistDocument.FILE_EXTENSION);

                if (File.Exists(path))
                {
                    var playlist = new TablaturePlaylistDocument();
                    playlist.Load(path);

                    _playlistFiles.Add(playlist);
                }
            }
        }

        protected virtual void WriteHeader(BinaryWriter writer, Version formatVersion)
        {
            writer.BaseStream.Position = 0;
            writer.Write(FILE_HEADER.ToCharArray());
            writer.Write(formatVersion.ToString());
            writer.Write(COMPRESSED);
        }

        protected virtual void WriteTablatureEntries(BinaryWriter writer)
        {
            writer.BaseStream.Position = HEADER_SIZE;

            var knownDirectories = new List<string>();
            var directoryLookupTable = new Dictionary<TablatureLibraryItem, int>();

            var existingEntries = _library.FindAll(x => File.Exists(x.FileInfo.FullName));

            foreach (var entry in existingEntries)
            {
                var directory = Path.GetDirectoryName(entry.FileInfo.FullName);

                if (!knownDirectories.Contains(directory))
                {
                    knownDirectories.Add(directory);
                }

                var index = knownDirectories.IndexOf(directory);

                directoryLookupTable.Add(entry, index);
            }

            writer.Write(knownDirectories.Count);

            foreach (var directory in knownDirectories)
            {
                writer.Write(directory, FILE_ENCODING);
            }

            writer.Write(existingEntries.Count);

            foreach (var entry in existingEntries)
            {
                WriteFilePath(writer, entry, entry.FileInfo, directoryLookupTable, TablatureDocument.FILE_EXTENSION);
                writer.Write(DateTimeUtilities.GetUnixTimestamp(entry.Added));
                writer.Write(entry.Favorited);
                writer.Write(entry.Views);
                writer.Write(entry.LastViewed.HasValue ? DateTimeUtilities.GetUnixTimestamp(entry.LastViewed.Value) : 0);
            }
        }

        protected virtual void WritePlaylistEntries(BinaryWriter writer)
        {
            var knownDirectories = new List<string>();
            var directoryLookupTable = new Dictionary<TablaturePlaylistDocument, int>();

            var existingEntries = new List<TablaturePlaylistDocument>(_library.Playlists).FindAll(x => File.Exists(x.FileInfo.FullName));

            foreach (var entry in existingEntries)
            {
                var directory = Path.GetDirectoryName(entry.FileInfo.FullName);

                if (!knownDirectories.Contains(directory))
                {
                    knownDirectories.Add(directory);
                }

                var index = knownDirectories.IndexOf(directory);

                directoryLookupTable.Add(entry, index);
            }

            writer.Write(knownDirectories.Count);

            foreach (var directory in knownDirectories)
            {
                writer.Write(directory);
            }

            writer.Write(existingEntries.Count);

            foreach (var entry in existingEntries)
            {
                WriteFilePath(writer, entry, entry.FileInfo, directoryLookupTable, TablaturePlaylistDocument.FILE_EXTENSION);
            }
        }

        public void Save(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    WriteHeader(writer, FORMAT_VERSION);
                    WriteTablatureEntries(writer);
                    WritePlaylistEntries(writer);
                }
            }
        }
    }
}