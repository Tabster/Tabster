#region

using Tabster.Core.Data;
using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Parsing
{
    /// <summary>
    ///   String-based tab parser.
    /// </summary>
    public interface IStringTabParser : ITabParser
    {
        /// <summary>
        ///   Parses tab from text source.
        /// </summary>
        /// <param name="text"> Source text to parse. </param>
        /// <param name="type"> Explicitly defined type. </param>
        /// <returns> Parsed tab. </returns>
        TablatureDocument Parse(string text, TabType? type);
        //todo remove nullable
    }
}