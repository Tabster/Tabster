#region

using System;

#endregion

namespace Tabster.Data
{
    public interface ITabsterFileHeader
    {
        Version Version { get; }
    }
}