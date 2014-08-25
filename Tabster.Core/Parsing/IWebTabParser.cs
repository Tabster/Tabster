#region

using System;

#endregion

namespace Tabster.Core.Parsing
{
    /// <summary>
    ///   Web-based tab parser.
    /// </summary>
    public interface IWebTabParser : IStringTabParser
    {
        ///<summary>
        ///  Determines whether a specified URL matches a specific pattern used by the parser. Used for web-based parsing.
        ///</summary>
        ///<param name="url"> The URL to check. </param>
        ///<returns> True if the URL matches the supported pattern; otherwise, False. </returns>
        bool MatchesUrlPattern(Uri url);
    }
}