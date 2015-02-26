namespace Tabster.Data
{
    public interface ITabsterFile
    {
        void Load(string fileName);
        void Save(string fileName);
    }
}