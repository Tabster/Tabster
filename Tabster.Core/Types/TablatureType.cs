namespace Tabster.Core.Types
{
    public enum TablatureType
    {
        Guitar,
        Chords,
        Bass,
        Drum,
        Ukulele
    }

    public static class TabTypeExtensions
    {
        public static string ToFriendlyString(this TablatureType type)
        {
            switch (type)
            {
                case TablatureType.Guitar:
                    return "Guitar Tab";
                case TablatureType.Chords:
                    return "Guitar Chords";
                case TablatureType.Bass:
                    return "Bass Tab";
                case TablatureType.Drum:
                    return "Drum Tab";
                case TablatureType.Ukulele:
                    return "Ukulele Tab";
            }
            return null;
        }
    }
}