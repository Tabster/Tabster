#region

using System;

#endregion

namespace Tabster.Data.Processing
{
    /// <summary>
    ///     Allows exporting of a tablature document.
    /// </summary>
    public interface ITablatureFileExporter
    {
        /// <summary>
        ///     The file type to export to.
        /// </summary>
        FileType FileType { get; }

        /// <summary>
        ///     The exporter version.
        /// </summary>
        Version Version { get; }

        /// <summary>
        ///     Export a tablature document to the specified location.
        /// </summary>
        /// <param name="file"> The tablature document to export. </param>
        /// <param name="fileName"> The path to export the document. </param>
        void Export(ITablatureFile file, string fileName);
    }
}