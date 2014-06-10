#region

using System;

#endregion

namespace Tabster.Core
{
    public enum TabType
    {
        Guitar,
        Chord,
        Bass,
        Drum,
        Ukulele
    }

    public enum TabSource
    {
        Download,
        FileImport,
        UserCreated,
    }

    public enum Difficulty
    {
        Unknown,
        Novice,
        Intermediate,
        Advanced
    }

    public enum Tuning
    {
        Unknown,
        Standard,
        HalfStepDown,
        BTuning,
        CTuning,
        DTuning,
        DropA,
        DropASharp,
        DropB,
        DropC,
        DropCSharp,
        DropD,
        OpenC,
        OpenD,
        OpenE,
        OpenG
    }

    public class Tab
    {
        public static readonly string[] TabTypes;

        static Tab()
        {
            TabTypes = new[] {"Guitar Tab", "Guitar Chords", "Bass Tab", "Drum Tab", "Ukulele Tab"};
        }

        public Tab(string artist, string title, TabType type, string contents)
        {
            Comment = "";
            Lyrics = "";
            Audio = "";
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
        ///   Gets the contents of the tab
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        ///   Gets or sets the source of the tab.
        /// </summary>
        public TabSource SourceType { get; set; }

        /// <summary>
        ///   Gets or sets the remote sorce of the tab.
        /// </summary>
        public Uri Source { get; set; }

        /// <summary>
        ///   Gets or sets the type of tab.
        /// </summary>
        public TabType Type { get; set; }

        public Difficulty Difficulty { get; set; }

        public DateTime Created { get; set; }

        public string Comment { get; set; }

        public string Audio { get; set; }

        public string Lyrics { get; set; }

        public string GetName()
        {
            return string.Format("{0} - {1} ({2})", Artist, Title, GetTabString(Type));
        }

        #region Overrides

        public override string ToString()
        {
            return GetName();
        }

        #endregion

        #region Static Methods

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
                case "Ukulele Tab":
                    return TabType.Ukulele;
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
                case TabType.Ukulele:
                    return TabTypes[4];
            }

            return TabTypes[0];
        }

        #endregion
    }
}