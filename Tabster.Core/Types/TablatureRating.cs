#region

using System;

#endregion

namespace Tabster.Core.Types
{
    /// <summary>
    ///   Tablature rating.
    /// </summary>
    public enum TablatureRating
    {
        None = 0,
        Stars1 = 1,
        Stars2 = 2,
        Stars3 = 3,
        Stars4 = 4,
        Stars5 = 5
    }

    public static class TablatureRatingUtilities
    {
        public static TablatureRating FromString(string str)
        {
            int i;
            if (int.TryParse(str, out i))
                return FromInt(i);

            try
            {
                return (TablatureRating) Enum.Parse(typeof (TablatureRating), str);
            }

            catch (ArgumentException)
            {
                return TablatureRating.None;
            }
        }

        public static TablatureRating FromInt(int i)
        {
            switch (i)
            {
                case 1:
                    return TablatureRating.Stars1;
                case 2:
                    return TablatureRating.Stars2;
                case 3:
                    return TablatureRating.Stars3;
                case 4:
                    return TablatureRating.Stars4;
                case 5:
                    return TablatureRating.Stars5;
                default:
                    return TablatureRating.None;
            }
        }
    }

    public static class TabRatingExtensions
    {
        public static int ToInt(this TablatureRating rating)
        {
            switch (rating)
            {
                case TablatureRating.None:
                    return 0;
                case TablatureRating.Stars1:
                    return 1;
                case TablatureRating.Stars2:
                    return 2;
                case TablatureRating.Stars3:
                    return 3;
                case TablatureRating.Stars4:
                    return 4;
                case TablatureRating.Stars5:
                    return 5;
                default:
                    throw new ArgumentOutOfRangeException("rating");
            }
        }

        public static int ToInt(this TablatureRating? rating)
        {
            return rating.HasValue ? ToInt(rating) : 0;
        }
    }
}