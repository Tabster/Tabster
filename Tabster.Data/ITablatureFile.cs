#region

using Tabster.Core.Types;

#endregion

namespace Tabster.Data
{
    public interface ITablatureFile : ITablatureAttributes, ITablatureSourceAttributes, ITablature, ITabsterFile
    {
        string Comment { get; set; }
    }
}