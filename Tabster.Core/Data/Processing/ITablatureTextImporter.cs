#region

using Tabster.Core.Types;

#endregion

namespace Tabster.Core.Data.Processing
{
    /// <summary>
    ///   Text-based tab source.
    /// </summary>
    public interface ITablatureTextImporter
    {
        /// <summary>
        ///   Parses tab from text source.
        /// </summary>
        /// <param name="text"> Source text to parse. </param>
        /// <param name="type"> Explicitly defined type. </param>
        /// <returns> Parsed tab. </returns>
        TablatureDocument Parse(string text, TabType? type);
    }
}