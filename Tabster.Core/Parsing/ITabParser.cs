#region

using System;

#endregion

namespace Tabster.Core.Parsing
{
    public interface ITabParser
    {
        /// <summary>
        ///   Parser name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   Parser Version
        /// </summary>
        Version Version { get; }
    }
}