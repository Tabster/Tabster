#region

using System;
using Tabster.Core.Data;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Parsing
{
    /// <summary>
    ///   Web-based tab parser.
    /// </summary>
    public interface IWebTabParser : IStringTabParser
    {
        /// <summary>
        ///   Parses tab from URL.
        /// </summary>
        /// <param name="url"> Source url to parse. </param>
        /// <param name="type"> Explicitly defined type. </param>
        /// <returns> Parsed tab. </returns>
        TablatureDocument Parse(Uri url, TabType? type);
        //todo remove nullable

        ///<summary>
        ///  Determines whether a specified URL matches a specific pattern used by the parser. Used for web-based parsing.
        ///</summary>
        ///<param name="url"> The URL to check. </param>
        ///<returns> True if the URL matches the supported pattern; otherwise, False. </returns>
        bool MatchesUrlPattern(Uri url);
    }
}