#region

using System;

#endregion

namespace Tabster.Data
{
    public static class Constants
    {
        public const string TablatureFileExtension = ".tabster";
        public const string TablaturePlaylistFileExtension = ".tablist";

        public static readonly Version TablatureFileVersion;
        public static readonly Version TablaturePlaylistFileVersion;

        static Constants()
        {
            TablatureFileVersion = new Version("1.0");
            TablaturePlaylistFileVersion = new Version("1.0");
        }
    }
}