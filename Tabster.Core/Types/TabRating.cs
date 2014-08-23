using System;

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

    public static class TabRatingExtensions
    {
        public static int ToInt(this TabRating rating)
        {
            switch(rating)
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
    }
}