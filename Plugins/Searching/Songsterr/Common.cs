#region

using System;
using Tabster.Core.Types;

#endregion

namespace Songsterr
{
    internal static class Common
    {
        internal static TablatureType GetTabTypeFromUrl(Uri url)
        {
            const string textIdentifier = "-tab-";

            var urlString = url.ToString();

            if (urlString.IndexOf(string.Format("{0}g-", textIdentifier), StringComparison.InvariantCultureIgnoreCase) != -1)
                return TablatureType.Guitar;
            if (urlString.IndexOf(string.Format("{0}b-", textIdentifier), StringComparison.InvariantCultureIgnoreCase) != -1)
                return TablatureType.Bass;
            if (urlString.IndexOf(string.Format("{0}d-", textIdentifier), StringComparison.InvariantCultureIgnoreCase) != -1)
                return TablatureType.Drum;

            return null;
        }
    }
}