using System;
using System.Text;

namespace Util
{
    public static class Log
    {
        static log4net.ILog log = log4net.LogManager.GetLogger(typeof(Log));

        public static void Fatal(string func, string info)
        {
            log.Fatal(MakeInfo(func, info));
        }
        public static void Error(string func, string info)
        {
            log.Error(MakeInfo(func, info));
        }
        public static void Warn(string func, string info)
        {
            log.Warn(MakeInfo(func, info));
        }
        public static void Info(string func, string info)
        {
            log.Info(MakeInfo(func, info));
        }
        public static void Debug(string func, string info)
        {
            log.Debug(MakeInfo(func, info));
        }
        static string MakeInfo(string func, string info)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            builder.Append("  ");
            builder.Append(func);
            builder.Append("：");
            builder.Append(info);
            return builder.ToString();
        }
    }
}
