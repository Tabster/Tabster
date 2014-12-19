#region

using System;
using System.IO;

#endregion

namespace Tabster.Data
{
    public interface ITabsterDocument
    {
        Version FileVersion { get; }
        void Load(string filename);
        void Save();
        void Save(string fileName);
        void Update();
    }
}