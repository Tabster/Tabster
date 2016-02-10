#region

using System;

#endregion

namespace Tabster.Utilities
{
    internal static class TablatureUtilities
    {
        /// <summary>
        ///     Removes version convention from tablature titles.
        /// </summary>
        public static string RemoveVersionConventionFromTitle(string title)
        {
            var versionConventionIndex = title.IndexOf(" (ver ", StringComparison.OrdinalIgnoreCase);

            if (versionConventionIndex >= 0)
                title = title.Remove(versionConventionIndex);

            return title;
        }
    }
}