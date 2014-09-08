#region

using System;

#endregion

namespace Tabster.Core.Types
{
    /// <summary>
    ///   Tab rating.
    /// </summary>
    public enum TabRating
    {
        None = 0,
        Stars1 = 1,
        Stars2 = 2,
        Stars3 = 3,
        Stars4 = 4,
        Stars5 = 5
    }

    public static class TabRatingUtilities
    {
        public static TabRating FromString(string str)
        {
            int i;
            if (int.TryParse(str, out i))
                return FromInt(i);

            try
            {
                return (TabRating) Enum.Parse(typeof (TabRating), str);
            }

            catch (ArgumentException)
            {
                return TabRating.None;
            }
        }

        public static TabRating FromInt(int i)
        {
            switch (i)
            {
                case 1:
                    return TabRating.Stars1;
                case 2:
                    return TabRating.Stars2;
                case 3:
                    return TabRating.Stars3;
                case 4:
                    return TabRating.Stars4;
                case 5:
                    return TabRating.Stars5;
                default:
                    return TabRating.None;
            }
        }
    }

    public static class TabRatingExtensions
    {
        public static int ToInt(this TabRating rating)
        {
            switch (rating)
            {
                case TabRating.None:
                    return 0;
                case TabRating.Stars1:
                    return 1;
                case TabRating.Stars2:
                    return 2;
                case TabRating.Stars3:
                    return 3;
                case TabRating.Stars4:
                    return 4;
                case TabRating.Stars5:
                    return 5;
                default:
                    throw new ArgumentOutOfRangeException("rating");
            }
        }

        public static int ToInt(this TabRating? rating)
        {
            return rating.HasValue ? ToInt(rating) : 0;
        }
    }
}