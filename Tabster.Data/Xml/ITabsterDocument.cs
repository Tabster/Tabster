#region

using System;

#endregion

namespace Tabster.Data.Xml
{
    [Obsolete]
    public interface ITabsterDocument
    {
        Version FileVersion { get; }
        void Load(string filename);
        void Save();
        void Save(string fileName);
        void Update();
    }
}