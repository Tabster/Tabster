#region

using Tabster.Core.Types;

#endregion

namespace Tabster.Utilities
{
    public static class ITablatureExtensions
    {
        public static string ToFriendlyString(this ITablature tab)
        {
            return string.Format("{0} - {1} ({2})", tab.Artist, tab.Title, tab.Type.ToFriendlyString());
        }
    }
}