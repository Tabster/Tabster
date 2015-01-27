#region

using System;
using Tabster.Core.Types;

#endregion

namespace Songsterr
{
    internal static class Common
    {
        internal static TablatureType GetTabTypeFromURL(Uri url)
        {
            const string TEXT_IDENTIFIER = "-tab-";

            var urlString = url.ToString();

            if (urlString.IndexOf(string.Format("{0}g-", TEXT_IDENTIFIER), StringComparison.InvariantCultureIgnoreCase) != -1)
                return TablatureType.Guitar;
            if (urlString.IndexOf(string.Format("{0}b-", TEXT_IDENTIFIER), StringComparison.InvariantCultureIgnoreCase) != -1)
                return TablatureType.Bass;
            if (urlString.IndexOf(string.Format("{0}d-", TEXT_IDENTIFIER), StringComparison.InvariantCultureIgnoreCase) != -1)
                return TablatureType.Drum;

            return null;
        }
    }
}