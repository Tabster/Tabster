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

        static Constants()
        {
            TablatureFileVersion = new Version("1.0");
        }
    }
}