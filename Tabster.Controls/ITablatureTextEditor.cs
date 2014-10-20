#region

using Tabster.Core.Types;

#endregion

namespace Tabster.Controls
{
    public interface ITablatureTextEditor
    {
        bool ReadOnly { get; set; }
        bool Modified { get; set; }
        void LoadTablature(ITablature tablature);
    }
}