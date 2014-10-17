#region

using System;

#endregion

namespace Tabster.Data.Processing
{
    /// <summary>
    ///   Web-based tab source.
    /// </summary>
    public interface ITablatureWebpageImporter : ITablatureTextImporter
    {
        /// <summary>
        ///   The name of the site (if applicable) where the page text is from.
        /// </summary>
        string SiteName { get; }

        ///<summary>
        ///  Determines whether a specified URL matches a specific pattern used by source processor.
        ///</summary>
        ///<param name="url"> The URL to check. </param>
        ///<returns> True if the URL matches the supported pattern; otherwise, False. </returns>
        bool MatchesUrlPattern(Uri url);
    }
}