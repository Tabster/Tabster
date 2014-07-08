#region

using System;
using System.Text;

#endregion

namespace Tabster.Core
{
    public enum TabSource
    {
        Download,
        FileImport,
        UserCreated
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

    public interface ITablature
    {
        string Artist { get; set; }
        string Title { get; set; }
        string Contents { get; set; }
        TabType Type { get; set; }
        TabSource SourceType { get; set; }
        Uri Source { get; set; }
    }

    public static class ITablatureExtensions
    {
        public static string ToFriendlyString(this ITablature tab)
        {
            return string.Format("{0} - {1} ({2})", tab.Artist, tab.Title, tab.Type.ToFriendlyString());
        }
    }

    public class Tab
    {
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

        public string ToFriendlyName()
        {
            return string.Format("{0} - {1} ({2})", Artist, Title, Type.ToFriendlyString());
        }

        #region Static Methods

        public static TabType GetTabType(string type)
        {
            switch (type)
            {
                case "Bass Tab":
                    return TabType.Bass;
                case "Guitar Tab":
                    return TabType.Guitar;
                case "Guitar Chords":
                    return TabType.Chords;
                case "Drum Tab":
                    return TabType.Drum;
                case "Ukulele Tab":
                    return TabType.Ukulele;
            }

            return TabType.Guitar;
        }

        #endregion
    }
}