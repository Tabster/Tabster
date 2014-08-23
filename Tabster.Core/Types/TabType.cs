namespace Tabster.Core.Types
{
    public enum TabType
    {
        Guitar,
        Chords,
        Bass,
        Drum,
        Ukulele
    }

    public static class TabTypeExtensions
    {
        public static string ToFriendlyString(this TabType type)
        {
            switch (type)
            {
                case TabType.Guitar:
                    return "Guitar Tab";
                case TabType.Chords:
                    return "Guitar Chords";
                case TabType.Bass:
                    return "Bass Tab";
                case TabType.Drum:
                    return "Drum Tab";
                case TabType.Ukulele:
                    return "Ukulele Tab";
            }
            return null;
        }
    }
}