namespace TD1990.Libs.TDLyutil.Common
{
    using System;

    public static class UnixTimeConverter
    {
        public static DateTime FromUnixTimestamp(this int unixtime)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(unixtime);
        }

        public static int ToUnixTimestamp(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return  (int)diff.TotalSeconds;
        }
    }
}
