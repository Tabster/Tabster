#region

using System;

#endregion

namespace Tabster.Data.Utilities
{
    internal static class DateTimeUtilities
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static int GetUnixTimestamp()
        {
            return (int) (DateTime.UtcNow.Subtract(Epoch)).TotalSeconds;
        }

        public static int GetUnixTimestamp(DateTime dateTime)
        {
            return (int) (dateTime.Subtract(Epoch)).TotalSeconds;
        }

        public static DateTime UnixTimestampToDateTime(int timestamp)
        {
            return Epoch.AddSeconds(timestamp);
        }
    }
}