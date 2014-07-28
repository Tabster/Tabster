#region

using System;

#endregion

namespace Tabster.Core.Types
{
    public interface ITablatureUserDefined : ITablature
    {
        string Comment { get; set; }
        DateTime Created { get; set; }
    }
}