#region

using Tabster.Core.Types;

#endregion

namespace Tabster.Utilities.Extensions
{
    public static class ITablatureExtensions
    {
        public static string ToFriendlyString(this ITablatureAttributes tab)
        {
            return string.Format("{0} - {1} ({2})", tab.Artist, tab.Title, tab.Type.ToFriendlyString());
        }
    }
}