#region

using System;
using System.IO;

#endregion

namespace Tabster.Data
{
    public interface ITabsterDocument
    {
        Version FileVersion { get; }
        FileInfo FileInfo { get; }
        void Load(string filename);
        void Save();
        ITabsterDocument SaveAs(string fileName);
        void Update();
    }
}