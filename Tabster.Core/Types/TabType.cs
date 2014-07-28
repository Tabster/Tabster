#region

using System;

#endregion

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

    public static class TabTypeUtilities
    {
        internal static readonly string[] _typeStrings = new[] {"Guitar Tab", "Guitar Chords", "Bass Tab", "Drum Tab", "Ukulele Tab"};

        public static string ToFriendlyString(this TabType type)
        {
            return _typeStrings[(int) type];
        }

        public static string[] FriendlyStrings()
        {
            return _typeStrings;
        }

        public static TabType? FromFriendlyString(string str)
        {
            for (var i = 0; i < _typeStrings.Length; i++)
            {
                var typeString = _typeStrings[i];

                if (typeString.Equals(str, StringComparison.InvariantCultureIgnoreCase))
                {
                    var type = (TabType) i;
                    return type;
                }
            }

            return null;
        }
    }
}