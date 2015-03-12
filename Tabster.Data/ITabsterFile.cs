#region

using System.IO;

#endregion

namespace Tabster.Data
{
    public interface ITabsterFile
    {
        FileInfo FileInfo { get; }
        void Load(string fileName);
        void Save(string fileName);
        TabsterFileAttributes FileAttributes { get; }
        ITabsterFileHeader FileHeader { get; }
    }
}