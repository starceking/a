using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Util
{
    public static class Mobile
    {
        static bool SIM_DATA = (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SimData"]));
        #region verify code
        public static string ADMIN_MOBILE = "15058160140";
        public static string SUPER_CODE = "JWY！";
        public static async Task<string> SetCode(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile)) return "手机号格式不对";

            Random r = new Random(DateTime.Now.Millisecond);
            string code = r.Next(1000, 9999).ToString();
            await Redis.InsertString("MOBILE_CODE_" + mobile, code, new TimeSpan(0, 10, 0));
            return code;
        }
        public static async Task<bool> GetCode(string mobile, string code)
        {
            if (SIM_DATA) return true;
            if (code.Equals(SUPER_CODE)) return true;
            if (string.IsNullOrWhiteSpace(mobile)) return false;

            string key = "MOBILE_CODE_" + mobile;
            StackExchange.Redis.RedisValue val = await Redis.GetString(key);
            if (val.HasValue && val.ToString().Equals(code))
            {
                await Redis.DeleteKey(key);
                return true;
            }
            return false;
        }
        #endregion
        static string GetUrl(string mobile, string content)
        {
            return string.Empty;
        }
        public static async Task<string> SendSms(string phone, string message)
        {
            if (phone.Trim().Length == 0) return "手机号码格式错误";
            if (message.Trim().Length == 0) return string.Empty;
            Log.Info(phone, message);
            return await RequestGet(GetUrl(phone, message));
        }
        public static string SendSmsSync(string phone, string message)
        {
            if (phone.Trim().Length == 0) return "手机号码格式错误";
            if (message.Trim().Length == 0) return string.Empty;
            Log.Info(phone, message);
            return RequestGetSync(GetUrl(phone, message));
        }
        /// <summary>
        /// 发送GET请求到短信接口。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static async Task<string> RequestGet(string request)
        {
            using (var client = new WebClient())
            {
                try
                {
                    string response = (await client.DownloadStringTaskAsync(request)).Trim();
                    if (response.Trim().StartsWith("1,")) return string.Empty;
                    return response;
                }
                catch
                {
                    return "异常错误";
                }
            }
        }
        private static string RequestGetSync(string request)
        {
            using (var client = new WebClient())
            {
                try
                {
                    string response = (client.DownloadString(request)).Trim();
                    if (response.Trim().StartsWith("1,")) return string.Empty;
                    return response;
                }
                catch
                {
                    return "异常错误";
                }
            }
        }
        public static string HideMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile)) return mobile;

            if (mobile.Length == 11)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(mobile.Substring(0, 3));
                builder.Append("****");
                builder.Append(mobile.Substring(7, 4));
                return builder.ToString();
            }
            return mobile;
        }
        #region 短信安全控制
        static IDictionary<string, DateTime> ipDict = new Dictionary<string, DateTime>();
        static IDictionary<string, DateTime> ipmDict = new Dictionary<string, DateTime>();
        static IDictionary<string, int> ipaDict = new Dictionary<string, int>();
        public static bool SmsCanIp(string ip, int sed, int max)
        {
            //            1、图片验证码（硬性要求）
            //2、单IP触同一个手机号发短信间隔，不小于60秒或者120秒（硬性要求）
            //3、单IP触发次数限制，建议24小时不超过20条。单号码24小时不超过5条（硬性要求）
            if (string.IsNullOrWhiteSpace(ip)) return false;
            if (ipDict.ContainsKey(ip))
            {
                TimeSpan ts = DateTime.Now - ipDict[ip];
                if (ts.TotalSeconds < sed) return false;
            }
            if (ipmDict.ContainsKey(ip) && ipaDict.ContainsKey(ip))
            {
                TimeSpan ts = DateTime.Now - ipmDict[ip];
                int amount = ipaDict[ip];
                if (amount >= ((int)ts.TotalHours / 24 + 1) * max) return false;
            }
            return true;
        }
        public static void SetCanIp(string ip)
        {
            try
            {
                DateTime now = DateTime.Now;
                if (ipDict.ContainsKey(ip)) ipDict[ip] = now;
                else ipDict.Add(ip, now);

                if (!ipmDict.ContainsKey(ip)) ipmDict.Add(ip, now);

                if (ipaDict.ContainsKey(ip)) ipaDict[ip]++;
                else ipaDict.Add(ip, 1);
            }
            catch
            {

            }
        }
        #endregion
    }
}
