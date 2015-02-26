#region

using System;

#endregion

namespace Tabster.Data
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