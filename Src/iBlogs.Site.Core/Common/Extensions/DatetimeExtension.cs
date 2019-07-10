using System;

namespace iBlogs.Site.Core.Common.Extensions
{
    public static class DatetimeExtension
    {
        public static int ToUnixTimestamp(this DateTime dateTime)
        {
            return (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}