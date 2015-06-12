#region

using Tabster.Core.Types;

#endregion

namespace Tabster.Data
{
    public interface ITablatureFile : ITablatureExtendedAttributes, ITablatureAttributes, ITablatureSourceAttributes, ITablature, ITabsterFile
    {
        
    }
}