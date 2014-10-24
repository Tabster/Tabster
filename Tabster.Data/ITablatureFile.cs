#region

using System;
using System.Collections.Generic;
using Tabster.Core.Types;

#endregion

namespace Tabster.Data
{
    public interface ITablatureFile : ITablatureAttributes
    {
        Version Version { get; }
        Dictionary<string, string> Attributes { get; }

        void Load(string fileName);
        void Save(string fileName);
    }
}