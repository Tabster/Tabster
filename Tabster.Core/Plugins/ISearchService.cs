#region

using System;

#endregion

namespace Tabster.Core.Plugins
{
    /// <summary>
    ///   Tab service which enables searching.
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        ///   Service name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   Associated parser.
        /// </summary>
        ITabParser Parser { get; }

        /// <summary>
        ///   Service flags.
        /// </summary>
        SearchServiceFlags Flags { get; }

        /// <summary>
        ///   Queries service and returns results based on search parameters.
        /// </summary>
        /// <param name="artist"> The tab artist. </param>
        /// <param name="title"> The tab title. </param>
        /// <param name="type"> The tab type. </param>
        /// <returns> Array of results. </returns>
        Tab[] Search(string artist, string title, TabType? type);

        ///<summary>
        ///  Determines whether a specific TabType is supported by the service.
        ///</summary>
        ///<param name="type"> The type to check. </param>
        ///<returns> True if the type is supported by the service; otherwise, False. </returns>
        bool SupportsTabType(TabType type);
    }

    /// <summary>
    ///   Search service flags.
    /// </summary>
    [Flags]
    public enum SearchServiceFlags
    {
        None = 1,
        RequiresArtistParameter = 2,
        RequiresTitleParameter = 4,
        RequiresTypeParamter = 8,
    }
}