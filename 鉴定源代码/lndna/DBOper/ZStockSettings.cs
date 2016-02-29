using Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class ZStockSettings
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        public const string TABLE = "zstock_settings";
        public const bool AUTO_BUYSELL = false;
        public static IList<decimal> DEBT = new List<decimal>() { };//前台显示点买金额共分8档，单位为万元
        public static IList<int> STOP_EARN = new List<int>() { };//止盈 = 点买金额 X 止盈比率
        public static int DEFER_DAYS = 0;//最长可展期时间
        public static int BUY_WAIT_SECOND = 0;//60s内无人接单，系统自动流单
        public static int USER_PROFIT = 0;//盈利后，点买人获得盈利的比利
        public static int DAY_UPPER = 0;//同一ID单日最多发起点买次数
        public static decimal DEFER_FEE = 0;//每万元递延的费用
        public static decimal PROFIT_FROM_REF = 0;//每万元邀请的收益
        public static decimal PRICE_IN = 0;//股价格波动风险
        public static decimal CAN_DELAY = 0;//递延阈值
        public static decimal FEE = 0;//交易综合费
        public static int AUTO_TRADE = 0;//是否开启自动交易
        public static int TRADE_TIME1 = 0;//允许交易的时段
        public static int TRADE_TIME2 = 0;
        public static int TRADE_TIME3 = 0;
        public static int TRADE_TIME4 = 0;
        public static int TRADE_TIME5 = 0;
        public static int TRADE_TIME6 = 0;
        public static int TRADE_TIME7 = 0;
        public static int TRADE_TIME8 = 0;
        public static IDictionary<int, decimal> STOP_LOSS = new Dictionary<int, decimal>();//止损杠杆
        #endregion
        #region 一般操作
        public static async Task Update(int id, string value)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(value)) return;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("value", value);
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            await DBHelper.Update(TABLE, dict, fdict, "and");

            GiveVal(id, value);

            await DeleteRedis(id);
        }
        public static void Init()
        {
            IEnumerable<ZStockSettingsModel> list = GetListSync();
            foreach (ZStockSettingsModel model in list)
            {
                GiveVal(model.id, model.value);
            }
        }
        static void GiveVal(int id, string value)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(value)) return;

            if (id == 1)
            {
                DEBT.Clear();
                foreach (string str in value.Split(','))
                {
                    DEBT.Add(Convert.ToDecimal(str));
                }
            }
            else if (id == 2)
            {
                STOP_EARN.Clear();
                foreach (string str in value.Split(','))
                {
                    STOP_EARN.Add(Convert.ToInt32(str));
                }
            }
            else if (id == 3) DEFER_DAYS = Convert.ToInt32(value);
            else if (id == 4) BUY_WAIT_SECOND = Convert.ToInt32(value);
            else if (id == 5) USER_PROFIT = Convert.ToInt32(value);
            else if (id == 6) DAY_UPPER = Convert.ToInt32(value);
            else if (id == 7) DEFER_FEE = Convert.ToDecimal(value);
            else if (id == 8) PROFIT_FROM_REF = Convert.ToDecimal(value);
            else if (id == 9) PRICE_IN = Convert.ToDecimal(value);
            else if (id == 10) CAN_DELAY = Convert.ToDecimal(value);
            else if (id == 11)
            {
                STOP_LOSS.Clear();
                foreach (string str in value.Split(';'))
                {
                    string[] str1 = str.Split(',');
                    STOP_LOSS.Add(Convert.ToInt32(str1[0]), Convert.ToDecimal(str1[1]));
                }
            }
            else if (id == 12) FEE = Convert.ToDecimal(value);
            else if (id == 13) AUTO_TRADE = Convert.ToInt32(value);
            else if (id == 14)
            {
                if (value.Equals("00:00,24:00"))
                {
                    TRADE_TIME1 = 0;
                    TRADE_TIME2 = 0;
                    TRADE_TIME3 = 0;
                    TRADE_TIME4 = 0;
                    TRADE_TIME5 = 0;
                    TRADE_TIME6 = 0;
                    TRADE_TIME7 = 0;
                    TRADE_TIME8 = 0;
                }
                else
                {
                    string[] arr = value.Split('|');
                    string[] arr1 = arr[0].Split(',');
                    string[] arr11 = arr1[0].Split(':');
                    string[] arr12 = arr1[1].Split(':');
                    string[] arr2 = arr[1].Split(',');
                    string[] arr21 = arr2[0].Split(':');
                    string[] arr22 = arr2[1].Split(':');
                    TRADE_TIME1 = Convert.ToInt32(arr11[0]);
                    TRADE_TIME2 = Convert.ToInt32(arr11[1]);
                    TRADE_TIME3 = Convert.ToInt32(arr12[0]);
                    TRADE_TIME4 = Convert.ToInt32(arr12[1]);
                    TRADE_TIME5 = Convert.ToInt32(arr21[0]);
                    TRADE_TIME6 = Convert.ToInt32(arr21[1]);
                    TRADE_TIME7 = Convert.ToInt32(arr22[0]);
                    TRADE_TIME8 = Convert.ToInt32(arr22[1]);
                }
            }
        }
        public static async Task<ZStockSettingsModel> GetOne(int id)
        {
            return await DBHelper.GetOne<ZStockSettingsModel>(id.ToString());
        }
        public static async Task<IEnumerable<ZStockSettingsModel>> GetList()
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            return await DBHelper.GetList<ZStockSettingsModel, int>(TABLE, "*", "id", fdict, "and");
        }
        public static IEnumerable<ZStockSettingsModel> GetListSync()
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            return DBHelper.GetListSync<ZStockSettingsModel, int>(TABLE, "*", "id", fdict, "and");
        }
        public static async Task DeleteRedis(int id)
        {
            if (id <= 0) return;
            await Redis.Delete<ZStockSettingsModel>(id.ToString());
        }
        #endregion
        #region 熔断
        static string RONG_DUAN = "ZStockSettings_RONG_DUAN";
        public static async Task SetRongduan(int rd)
        {
            await Redis.InsertString(RONG_DUAN, rd.ToString());
        }
        public static async Task<int> GetRongduan()
        {
            RedisValue rv = await Redis.GetString(RONG_DUAN);
            if (rv.HasValue)
            {
                int i = 0;
                if (int.TryParse(rv, out i)) return i;
            }
            return 0;
        }
        public static int GetRongduanSync()
        {
            RedisValue rv = Redis.GetStringSync(RONG_DUAN);
            if (rv.HasValue)
            {
                int i = 0;
                if (int.TryParse(rv, out i)) return i;
            }
            return 0;
        }
        public static void DelRongduan()
        {
            Redis.DeleteKeySync(RONG_DUAN);
        }
        #endregion
        #region 首页统计数据
        static string IDX_STA_DATA = "ZStockSettings_IDX_STA_DATA";
        public static async Task SetIdxData(string data)
        {
            await Redis.InsertString(IDX_STA_DATA, data);
        }
        public static async Task<string> GetIdxData()
        {
            return await Redis.GetString(IDX_STA_DATA);
        }
        #endregion
        #region 辅助
        public static bool AllowTrade()
        {
            return !(TRADE_TIME1 == 0 && TRADE_TIME2 == 0 && TRADE_TIME3 == 0 && TRADE_TIME4 == 0 && TRADE_TIME5 == 0 &&
                TRADE_TIME6 == 0 && TRADE_TIME7 == 0 && TRADE_TIME8 == 0);
        }
        public static decimal GetFee(decimal money_debt)
        {
            return money_debt / 10000 * FEE;//每万元发起的费用
        }
        public static async Task<bool> PriceIn(string number)
        {
            if (StockInfo.SIM_DATA) return true;

            //股票日内涨跌幅过8%时无法发起点买
            StockInfoModel sdm = await StockInfo.GetOneByNum(number);
            if (sdm == null) return false;
            decimal now = sdm.now;
            if (now == 0) return false;
            decimal last = sdm.last;
            if (last == 0) return false;

            decimal delta = (now - last) * 100 / last;
            if (Math.Abs(delta) >= PRICE_IN) return false;

            return true;
        }
        public static bool PriceInSync(string number)
        {
            if (StockInfo.SIM_DATA) return true;

            //股票日内涨跌幅过8%时无法发起点买
            StockInfoModel sdm = StockInfo.GetOneByNumSync(number);
            if (sdm == null) return false;
            decimal now = sdm.now;
            if (now == 0) return false;
            decimal last = sdm.last;

            decimal delta = (now - last) * 100 / last;
            if (Math.Abs(delta) >= PRICE_IN) return false;

            return true;
        }
        public static bool CanDelaySync(string number, decimal margin, decimal buy_price, decimal money_debt, int stock_amount)
        {
            //（浮动盈亏+履约保证金）/点买金额>递延阈值
            decimal now = StockInfo.GetNowPriceSync(number);
            if (now == 0) return false;
            return (now - buy_price) * stock_amount > (money_debt * CAN_DELAY - margin);
        }
        #endregion
    }
}
