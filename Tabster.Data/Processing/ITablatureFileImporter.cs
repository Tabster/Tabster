#region

using System;
using Tabster.Core.Types;

#endregion

namespace Tabster.Data.Processing
{
    /// <summary>
    ///     Allows importing of a tablature document from various file formats.
    /// </summary>
    public interface ITablatureFileImporter
    {
        /// <summary>
        ///     The file type to import from.
        /// </summary>
        FileType FileType { get; }

        /// <summary>
        ///     The exporter version.
        /// </summary>
        Version Version { get; }

        /// <summary>
        ///     Imports a file from a designated path.
        /// </summary>
        /// <param name="fileName"> The path of the file to import. </param>
        AttributedTablature Import(string fileName);

        /// <summary>
        ///     Imports a file from a designated path.
        /// </summary>
        /// <param name="fileName"> The path of the file to import. </param>
        /// <param name="artist"> Explicitly-defined artist. </param>
        /// <param name="title"> Explicitly-defined title. </param>
        /// <param name="type"> Explicitly-defined type. </param>
        AttributedTablature Import(string fileName, string artist, string title, TablatureType type);
    }
}