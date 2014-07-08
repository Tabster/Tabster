namespace Tabster.Core
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
        internal static readonly string[] _typeStrings = new[] {"Guitar Tab", "Guitar Chords", "Bass Tab", "Drum Tab", "Ukulele Tab"};

        public static string ToFriendlyString(this TabType type)
        {
            switch (type)
            {
                case TabType.Guitar:
                    return _typeStrings[0];
                case TabType.Chords:
                    return _typeStrings[1];
                case TabType.Bass:
                    return _typeStrings[2];
                case TabType.Drum:
                    return _typeStrings[3];
                case TabType.Ukulele:
                    return _typeStrings[4];
            }

            return null;
        }
    }
}