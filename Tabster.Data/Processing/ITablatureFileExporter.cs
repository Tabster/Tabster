using Tabster.Data.Xml;

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
        ///     Export a tablature document to the specified location.
        /// </summary>
        /// <param name="doc"> The tablature document to export. </param>
        /// <param name="fileName"> The path to export the document. </param>
        void Export(TablatureDocument doc, string fileName);
    }
}