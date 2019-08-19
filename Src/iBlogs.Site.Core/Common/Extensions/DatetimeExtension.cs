using System;

namespace iBlogs.Site.Core.Common.Extensions
{
    public static class DatetimeExtension
    {
        public static int ToUnixTimestamp(this DateTime dateTime)
        {
            return (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        /// <summary>
        /// 已重载.计算两个日期的时间间隔,返回的是时间间隔的日期差的绝对值.
        /// </summary>
        /// <param name="dateTime1">第一个日期和时间</param>
        /// <param name="dateTime2">第二个日期和时间</param>
        /// <returns></returns>
        public static string DateDiffTo(this DateTime dateTime1, DateTime dateTime2)
        {
            var ts1 = new TimeSpan(dateTime1.Ticks);
            var ts2 = new TimeSpan(dateTime2.Ticks);
            var ts = ts1.Subtract(ts2).Duration();
            var dateDiff = ts.Days.ToString() + "天"
                                                 + ts.Hours.ToString() + "小时"
                                                 + ts.Minutes.ToString() + "分钟"
                                                 + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }
        /// <summary>
        /// 已重载.计算一个时间与当前本地日期和时间的时间间隔,返回的是时间间隔的日期差的绝对值.
        /// </summary>
        /// <param name="dateTime1">一个日期和时间</param>
        /// <returns></returns>
        public static string DateDiffToNow(this DateTime dateTime1)
        {
            return dateTime1.DateDiffTo(DateTime.Now);
        }
    }
}