#region

using System;
using System.Net;
using Tabster.Data.Xml;

#endregion

namespace Tabster.Data.Processing
{
    public interface ITablatureWebImporter
    {
        /// <summary>
        ///     The name of the site where the page text is from.
        /// </summary>
        string SiteName { get; }

        /// <summary>
        ///     The hompage of the search engine.
        /// </summary>
        Uri Homepage { get; }

        /// <summary>
        ///     Determines whether a specified URL is parsable by the importer.
        /// </summary>
        /// <param name="url"> The URL to check. </param>
        /// <returns> True if the URL matches the supported pattern; otherwise, False. </returns>
        bool IsUrlParsable(Uri url);

        /// <summary>
        ///     Parses tab from text source.
        /// </summary>
        /// <param name="url"> Source text to parse. </param>
        /// <param name="proxy"> Proxy settings.</param>
        /// <returns> Parsed tab. </returns>
        TablatureDocument Parse(Uri url, WebProxy proxy);
    }
}