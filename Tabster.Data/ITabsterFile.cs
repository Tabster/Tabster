#region

using System;
using System.IO;

#endregion

namespace Tabster.Data
{
    public interface ITabsterFile
    {
        FileInfo FileInfo { get; }
        ITabsterFileHeader Load(string fileName);
        void Save(string fileName);
        ITabsterFileHeader GetHeader();
        TabsterFileAttributes FileAttributes { get; }
    }
}