#region

using System.IO;

#endregion

namespace Tabster.Data.Library
{
    public class LibraryItem<T> where T : ITabsterFile
    {
        internal LibraryItem()
        {
        }

        private LibraryItem(T file, FileInfo fileInfo)
        {
            File = file;
            FileInfo = fileInfo;
        }

        public int ID { get; set; }
        public FileInfo FileInfo { get; private set; }
        public T File { get; private set; }

        public static TLibraryItem FromFile<TLibraryItem, TTabsterFile>(TTabsterFile file, FileInfo fileInfo)
            where TLibraryItem : LibraryItem<TTabsterFile>, new()
            where TTabsterFile : class, ITabsterFile, new()
        {
            var item = new LibraryItem<TTabsterFile>(file, fileInfo);
            return (TLibraryItem) item;
        }
    }
}