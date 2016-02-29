using System;

namespace Util
{
    public static class Time
    {
        public static string GetTimeStamp()
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (DateTime.Now.Ticks - startTime.Ticks) / 10000;//除10000调整为13位
            return t.ToString();
        }
        public static long GetTimeStampLong(DateTime dt)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (dt.Ticks - startTime.Ticks) / 10000;//除10000调整为13位
            return t;
        }
        public static string GetTimeStamp(DateTime dt)
        {
            return GetTimeStampLong(dt).ToString();
        }
        public static DateTime GetTime(long lTime)
        {
            lTime = lTime * 10000;
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow); //得到转换后的时间
            return dtResult;
        }
        public static DateTime? GetTime(string stamp)
        {
            stamp = stamp + "0000";//说明下，时间格式为13位后面补加4个"0"，如果时间格式为10位则后面补加7个"0",
            long lTime = 0;
            if (long.TryParse(stamp, out lTime))
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                TimeSpan toNow = new TimeSpan(lTime);
                DateTime dtResult = dtStart.Add(toNow); //得到转换后的时间
                return dtResult;
            }
            return null;
        }
    }
}
