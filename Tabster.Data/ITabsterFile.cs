namespace Tabster.Data
{
    public interface ITabsterFile
    {
        TabsterFileAttributes FileAttributes { get; }
        TabsterFileHeader FileHeader { get; }
        void Load(string fileName);
        void Save(string fileName);
    }
}