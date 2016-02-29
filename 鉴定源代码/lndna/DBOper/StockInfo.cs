using Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Util;
using System.Text;

namespace DBOper
{
    public static class StockInfo
    {
        #region 常量
        public static bool SIM_DATA = (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SimData"]));
        static IList<DateTime> fesDay = new List<DateTime>();
        #endregion
        #region 操盘时间
        /// <summary>
        /// 开盘时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetRealStartTime()
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 9, 30, 0);
        }
        /// <summary>
        /// 中午收盘时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetRealMidEndTime()
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 11, 29, 59);
        }
        /// <summary>
        /// 下午开盘时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetRealMidStartTime()
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 13, 0, 0);
        }
        /// <summary>
        /// 收盘时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetRealEndTime()
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 14, 59, 59);
        }
        /// <summary>
        /// 开盘时间
        /// </summary>
        /// <returns></returns>
        static DateTime GetStartTime()
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                ZStockSettings.TRADE_TIME1, ZStockSettings.TRADE_TIME2, 0);
        }
        /// <summary>
        /// 中午收盘时间
        /// </summary>
        /// <returns></returns>
        static DateTime GetMidEndTime()
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                ZStockSettings.TRADE_TIME3, ZStockSettings.TRADE_TIME4, 0);
        }
        /// <summary>
        /// 下午开盘时间
        /// </summary>
        /// <returns></returns>
        static DateTime GetMidStartTime()
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                ZStockSettings.TRADE_TIME5, ZStockSettings.TRADE_TIME6, 0);
        }
        /// <summary>
        /// 收盘时间
        /// </summary>
        /// <returns></returns>
        static DateTime GetEndTime()
        {
            if (SIM_DATA) return DateTime.Today.AddDays(1);
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                ZStockSettings.TRADE_TIME7, ZStockSettings.TRADE_TIME8, 0);
        }
        /// <summary>
        /// 当日是否可以交易，每年重新设置一次fesDay变量
        /// </summary>
        /// <returns></returns>
        public static bool InOrderDate(DateTime date)
        {
            if (SIM_DATA) return true;
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return false;

            if (fesDay.Count == 0)
            {
                fesDay.Add(new DateTime(2016, 4, 4));
                fesDay.Add(new DateTime(2016, 5, 2));
                fesDay.Add(new DateTime(2016, 6, 9));
                fesDay.Add(new DateTime(2016, 6, 10));
                fesDay.Add(new DateTime(2016, 9, 15));
                fesDay.Add(new DateTime(2016, 9, 16));
                fesDay.Add(new DateTime(2016, 10, 3));
                fesDay.Add(new DateTime(2016, 10, 4));
                fesDay.Add(new DateTime(2016, 10, 5));
                fesDay.Add(new DateTime(2016, 10, 6));
                fesDay.Add(new DateTime(2016, 10, 7));
            }
            foreach (DateTime day in fesDay)
            {
                if (date.Equals(day)) return false;
            }
            return true;
        }
        /// <summary>
        /// 是否处于交易时间内
        /// </summary>
        /// <returns></returns>
        public static bool InBuyTime()
        {
            if (SIM_DATA) return true;
            if (!InOrderDate(DateTime.Today)) return false;
            if (!ZStockSettings.AllowTrade()) return false;

            return ((DateTime.Now >= GetStartTime() && DateTime.Now <= GetMidEndTime()) ||
                (DateTime.Now >= GetMidStartTime() && DateTime.Now <= GetEndTime()));
        }
        public static DateTime GetNextOrderDate(DateTime day)
        {
            do
            {
                day = day.AddDays(1);
                if (InOrderDate(day)) return day;
            } while (true);
        }
        #endregion
        #region 价格数值
        public static async Task<decimal> GetNowPrice(string number)
        {
            if (SIM_DATA)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                return rand.Next(10, 21);
            }

            StockInfoModel sdm = await GetOneByNum(number);
            if (sdm != null) return sdm.now;
            return 0;
        }
        public static decimal GetNowPriceSync(string number)
        {
            if (SIM_DATA)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                return rand.Next(10, 21);
            }

            StockInfoModel sdm = GetOneByNumSync(number);
            if (sdm != null) return sdm.now;
            return 0;
        }
        public static async Task<decimal> GetLastPrice(string number)
        {
            if (SIM_DATA)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                return rand.Next(10, 21);
            }

            StockInfoModel sdm = await GetOneByNum(number);
            if (sdm != null) return sdm.last;
            return 0;
        }
        public static decimal GetLastPriceSync(string number)
        {
            if (SIM_DATA)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                return rand.Next(10, 21);
            }

            StockInfoModel sdm = GetOneByNumSync(number);
            if (sdm != null) return sdm.last;
            return 0;
        }
        /// <summary>
        /// 批量获取当前价格
        /// </summary>
        public static async Task<string> GetNowPriceMul(string nums)
        {
            if (string.IsNullOrWhiteSpace(nums)) return string.Empty;
            StringBuilder builder = new StringBuilder();
            foreach (string number in nums.Split(','))
            {
                decimal d = await GetNowPrice(number);
                if (builder.Length > 0)
                    builder.Append(";");
                builder.Append(number);
                if (d > 0)
                {
                    builder.Append(",");
                    builder.Append(d);
                }
                else builder.Append(",停牌");
            }
            return builder.ToString();
        }
        /// <summary>
        /// 是否跌停
        /// </summary>
        public static bool IsLimitDown(string number)
        {
            if (SIM_DATA)
            {
                return false;
            }

            StockInfoModel sdm = GetOneByNumSync(number);
            if (sdm == null) return false;
            if (sdm.now <= 0) return false;
            if (sdm.last * 9 / 10 >= sdm.now) return true;
            return false;
        }
        public static async Task<StockInfoModel> GetOneByNum(string number)
        {
            if (SIM_DATA)
            {
                return new StockInfoModel { now = await GetNowPrice(number) };
            }

            if (string.IsNullOrWhiteSpace(number)) return null;
            string key = GetKey(number);
            RedisValue rv = await Redis.GetString(key, false);
            if (!rv.HasValue) return null;
            return Json.ScriptDeserialize<StockInfoModel>(rv.ToString());
        }
        public static StockInfoModel GetOneByNumSync(string number)
        {
            if (SIM_DATA)
            {
                return new StockInfoModel { now = GetNowPriceSync(number) };
            }

            if (string.IsNullOrWhiteSpace(number)) return null;
            string key = GetKey(number);
            RedisValue rv = Redis.GetStringSync(key, false);
            if (!rv.HasValue) return null;
            return Json.ScriptDeserialize<StockInfoModel>(rv.ToString());
        }
        static readonly string SH = "SH";
        static readonly string SZ = "SZ";
        static readonly string SHC = "60";
        static readonly string SZC = "00";
        static readonly string SZC2 = "30";
        static string GetKey(string number)
        {
            number = number.ToUpper();
            if (number.StartsWith(SHC)) number = SH + number;
            else if (number.StartsWith(SZC) || number.StartsWith(SZC2)) number = SZ + number;
            return "wsreal_SplitData_GetKey_" + number;
        }
        #endregion
    }
}
