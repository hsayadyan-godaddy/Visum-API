using System;

namespace Product.DataModels.Extensions
{
    public static class UnixTimeExt
    {
        public static DateTime FromUnix(this long value)
        {
            var tmp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return tmp.AddMilliseconds(value).ToLocalTime();
        }

        public static DateTime FromUnixTicks(long unixTime)
        {
            var tmp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return tmp.AddTicks(unixTime).ToLocalTime();
        }

        public static long ToUnix(this DateTime value)
        {
            return ToUnixTicks(value);
        }

        public static long ToUnixTicks(DateTime date)
        {
            var tmp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(date.ToUniversalTime() - tmp).TotalMilliseconds;
        }
    }
}
