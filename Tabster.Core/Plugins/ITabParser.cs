#region

using System;

#endregion

namespace Tabster.Core.Plugins
{
    /// <summary>
    ///   Text-based tab parser.
    /// </summary>
    public interface ITabParser
    {
        /// <summary>
        ///   Parser name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   Parses tab from text source.
        /// </summary>
        /// <param name="sourceText"> Source text to parse. </param>
        /// <param name="type"> Explicitly defined type. </param>
        /// <returns> Parsed tab. </returns>
        Tab ParseTabFromSource(string sourceText, TabType? type);

        ///<summary>
        ///  Determines whether a specified URL matches a specific pattern used by the parser. Used for web-based parsing.
        ///</summary>
        ///<param name="url"> The URL to check. </param>
        ///<returns> True if the URL matches the supported pattern; otherwise, False. </returns>
        bool MatchesUrlPattern(Uri url);
    }

    public class TabParserException : Exception
    {
        public TabParserException()
        {
        }

        public TabParserException(string message)
            : base(message)
        {
        }
    }
}