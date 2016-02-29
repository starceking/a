using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class ZZStaUserDay
    {
        #region 常量
        static readonly string TABLE = "zzsta_user_day";
        #endregion
        #region 一般操作
        public static void Sta(DateTime day)
        {
            //每日1点运行，统计昨日数据
            if (GetOneSync(day) != null) return;
            int ip_amount = User.GetIpAmount();
            int cs_amount = User.GetIpCs();
            //reg_user
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("create_time", ">=" + day.ToShortDateString());
            fdict.Add("重复字段1重复字段create_time", "<" + day.AddDays(1).ToShortDateString());
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            long reg_user = DBHelper.GetCountSync(User.TABLE, fdict, "and");
            //reg_buy
            string sql = "select count(*) from user where create_time>='" + day.ToShortDateString() +
                  "' and create_time<'" + day.AddDays(1).ToShortDateString() + "' and (stock_plan_debt>0||hsi_plan_amount>0)";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            long reg_buy = DBHelper.GetCountFreeSync(sql);
            //dep_user
            sql = "select * from user_deposit_his where create_time>='" + day.ToShortDateString() +
               "' and create_time<'" + day.AddDays(1).ToShortDateString() + "'";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            IEnumerable<UserDepositHisModel> udhList = DBHelper.GetFreeSync<UserDepositHisModel>(sql);
            IList<ulong> userList = new List<ulong>();
            foreach (UserDepositHisModel udh in udhList)
            {
                if (!userList.Contains(udh.user_id)) userList.Add(udh.user_id);
            }
            int dep_user = userList.Count;
            //dep_buy、buy_user、buy_money、buy_my_pj、buy_amount、buy_amt_pj
            sql = "select * from stock_plan where start_time>='" + day.ToShortDateString() +
                "' and start_time<'" + day.AddDays(1).ToShortDateString() + "' and plan_status_id<>1 and plan_status_id<>-1";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            IEnumerable<StockPlanModel> spList = DBHelper.GetFreeSync<StockPlanModel>(sql);
            IList<ulong> userList2 = new List<ulong>();
            int dep_buy = 0;
            decimal buy_money = 0;
            foreach (StockPlanModel sp in spList)
            {
                if (!userList2.Contains(sp.user_id))
                {
                    userList2.Add(sp.user_id);
                    if (userList.Contains(sp.user_id)) dep_buy++;
                }
                buy_money += sp.money_debt;
            }
            int buy_user = userList2.Count;
            decimal buy_my_pj = buy_user > 0 ? buy_money / buy_user : 0;
            int buy_amount = spList.Count();
            decimal buy_amt_pj = buy_user > 0 ? buy_amount / buy_user : 0;
            //dep_buy、hsi_buy_user、hsi_buy_ss、hsi_buy_ss_pj、hsi_buy_amount、hsi_buy_amt_pj
            sql = "select * from hsi_plan_sub where create_date='" + day.ToShortDateString() + "' and plan_status_id<>1 and plan_status_id<>2 and plan_status_id<>-1";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            IEnumerable<HsiPlanSubModel> hsiList = DBHelper.GetFreeSync<HsiPlanSubModel>(sql);
            int hsi_buy_ss = 0;
            foreach (HsiPlanSubModel hps in hsiList)
            {
                if (!userList2.Contains(hps.user_id))
                {
                    userList2.Add(hps.user_id);
                    if (userList.Contains(hps.user_id)) dep_buy++;
                }
                hsi_buy_ss += hps.stock_amount;
            }
            int hsi_buy_user = userList2.Count - buy_user;
            decimal hsi_buy_ss_pj = hsi_buy_user > 0 ? hsi_buy_ss / hsi_buy_user : 0;
            int hsi_buy_amount = hsiList.Count();
            decimal hsi_buy_amt_pj = hsi_buy_user > 0 ? hsi_buy_amount / hsi_buy_user : 0;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("sta_day", day.ToString("yyyy-MM-dd"));
            dict.Add("ip_amount", ip_amount.ToString());
            dict.Add("cs_amount", cs_amount.ToString());
            dict.Add("reg_user", reg_user.ToString());
            dict.Add("reg_buy", reg_user > 0 ? (reg_buy * 100 / reg_user).ToString() : "0");
            dict.Add("dep_user", dep_user.ToString());
            dict.Add("dep_buy", dep_user > 0 ? (dep_buy * 100 / dep_user).ToString() : "0");
            dict.Add("buy_user", buy_user.ToString());
            dict.Add("buy_money", buy_money.ToString());
            dict.Add("buy_my_pj", buy_my_pj.ToString());
            dict.Add("buy_amount", buy_amount.ToString());
            dict.Add("buy_amt_pj", buy_amt_pj.ToString());
            dict.Add("hsi_buy_user", hsi_buy_user.ToString());
            dict.Add("hsi_buy_ss", hsi_buy_ss.ToString());
            dict.Add("hsi_buy_ss_pj", hsi_buy_ss_pj.ToString());
            dict.Add("hsi_buy_amount", hsi_buy_amount.ToString());
            dict.Add("hsi_buy_amt_pj", hsi_buy_amt_pj.ToString());
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) dict.Add("cmp_id", User.CMP_ID);
            DBHelper.InsertSync(TABLE, dict);

            User.ClearIp();
        }
        public static async Task<IEnumerable<ZZStaUserDayModel>> GetList(string sta_days, string sta_daye, string cmp_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(sta_days)) fdict.Add("sta_day", ">=" + sta_days);
            if (!string.IsNullOrWhiteSpace(sta_daye)) fdict.Add("重复字段1重复字段sta_day", "<=" + sta_daye);
            if (!string.IsNullOrWhiteSpace(cmp_id)) fdict.Add("cmp_id", cmp_id);
            else if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            return await DBHelper.GetList<ZZStaUserDayModel, int>(TABLE, "*", "id", fdict, "and");
        }
        public static ZZStaUserDayModel GetOneSync(DateTime day)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("sta_day", day.ToShortDateString());
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            else fdict.Add("cmp_id", "0");
            return DBHelper.GetOneSync<ZZStaUserDayModel, int>(TABLE, fdict, "and");
        }
        #endregion
    }
}
