#region

using System;
using System.IO;
using Tabster.Core.Types;

#endregion

namespace Tabster.Data
{
    public interface ITablatureFile : ITablatureAttributes
    {
        FileInfo FileInfo { get; }
        Version Version { get; }
        void Load(string fileName);
        void Save(string fileName);
    }
}