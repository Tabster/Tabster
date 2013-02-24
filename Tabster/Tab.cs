#region

using System;

#endregion

namespace Tabster
{
    public enum TabType
    {
        Guitar,
        Chord,
        Bass,
        Drum
    };

    public enum TabSource
    {
        Download = 0,
        FileImport = 1,
        UserCreated = 2,        
    }

    public class Tab
    {
        public static readonly string[] TabTypes;

        static Tab()
        {
            TabTypes = new[] { "Guitar Tab", "Guitar Chords", "Bass Tab", "Drum Tab" };
        }

        public Tab(string artist, string title, TabType type, string contents)
        {
            Title = title;
            Artist = artist;
            Type = type;
            Contents = contents;
        }

        /// <summary>
        ///   Gets or sets the title of the tab.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///   Gets or sets the artist of the tab.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Gets the contents of the tab
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        ///   Gets or sets the source of the tab.
        /// </summary>
        public TabSource Source { get; set; }

        /// <summary>
        /// Gets or sets the remote sorce of the tab.
        /// </summary>
        public Uri RemoteSource { get; set; }

        /// <summary>
        ///   Gets or sets the type of tab.
        /// </summary>
        public TabType Type { get; set; }

        public string Comment { get; set; }

        public string Audio { get; set; }

        public string Lyrics { get; set; }

        public static TabSource GetTabSource(string source)
        {
            switch (source)
            {
                case "UserCreated":
                    return TabSource.UserCreated;
                case "FileImport":
                    return TabSource.FileImport;
                default:
                    return TabSource.Download;
            }
        }

        public static TabType GetTabType(string type)
        {
            switch (type)
            {
                case "Bass Tab":
                    return TabType.Bass;
                case "Guitar Tab":
                    return TabType.Guitar;
                case "Guitar Chords":
                    return TabType.Chord;
                case "Drum Tab":
                    return TabType.Drum;
            }

            return TabType.Guitar;
        }
        public static string GetTabString(TabType type)
        {
            switch (type)
            {
                case TabType.Guitar:
                    return TabTypes[0];
                case TabType.Chord:
                    return TabTypes[1];
                case TabType.Bass:
                    return TabTypes[2];
                case TabType.Drum:
                    return TabTypes[3];
            }

            return TabTypes[0];
        }
    }
}