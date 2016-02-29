using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class StockPlanDay
    {
        #region 常量
        static readonly string TABLE = "stock_plan_day";
        #endregion
        #region 一般操作
        public static void Sta(DateTime day)
        {
            //每日1点运行，统计昨日数据
            if (GetOneSync(day) != null) return;

            int person = 0;
            int cs = 0;
            decimal money_debt = 0;
            decimal money_margin = 0;
            decimal profit_earn = 0;
            decimal profit_loss = 0;
            decimal start_fee = 0;
            decimal delay_fee = 0;
            decimal delay_ref_fee = 0;
            decimal cc_loss = 0;
            decimal tp_loss = 0;

            string sql = "select * from stock_plan where start_date='" + day.ToShortDateString() +
                "' and plan_status_id<>1 and plan_status_id<>-1";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            IEnumerable<StockPlanModel> spList = DBHelper.GetFreeSync<StockPlanModel>(sql);
            IList<ulong> uList = new List<ulong>();
            foreach (StockPlanModel sp in spList)
            {
                //money_debt += sp.money_debt;
                money_margin += sp.money_margin;
                start_fee += sp.money_fee;
                if (!uList.Contains(sp.user_id))
                    uList.Add(sp.user_id);
            }
            person = uList.Count();
            cs = spList.Count();
            sql = "select * from stock_plan where end_date='" + day.ToShortDateString() + "' and plan_status_id=7";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            spList = DBHelper.GetFreeSync<StockPlanModel>(sql);
            foreach (StockPlanModel sp in spList)
            {
                if (sp.profit > 0) profit_earn += sp.profit;
                else if (sp.profit < 0) profit_loss += sp.profit;
                if (sp.profit + sp.money_margin < 0) cc_loss += sp.profit + sp.money_margin;
            }
            sql = "select sum(money) from user_money_change where create_time>='" + day.ToShortDateString() +
               "' and create_time<'" + day.AddDays(1).ToShortDateString() + "' and info='递延'";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            delay_fee = DBHelper.GetSumFreeSync<decimal>(sql);
            sql = "select sum(money) from user_money_change where create_time>='" + day.ToShortDateString() +
               "' and create_time<'" + day.AddDays(1).ToShortDateString() + "' and info='邀请收益'";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            delay_ref_fee = DBHelper.GetSumFreeSync<decimal>(sql);

            sql = "select sum(money_debt) from stock_plan where plan_status_id=3";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            money_debt = DBHelper.GetSumFreeSync<decimal>(sql);

            if (person + cs + money_debt + money_margin + profit_earn + profit_loss + start_fee + delay_fee + delay_ref_fee + cc_loss + tp_loss > 0)
            {
                IDictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("sta_day", day.ToString("yyyy-MM-dd"));
                dict.Add("person", person.ToString());
                dict.Add("cs", cs.ToString());
                dict.Add("money_debt", money_debt.ToString());
                dict.Add("money_margin", money_margin.ToString());
                dict.Add("profit_earn", profit_earn.ToString());
                dict.Add("profit_loss", profit_loss.ToString());
                dict.Add("start_fee", start_fee.ToString());
                dict.Add("delay_fee", delay_fee.ToString());
                dict.Add("delay_ref_fee", delay_ref_fee.ToString());
                dict.Add("cc_loss", cc_loss.ToString());
                dict.Add("tp_loss", tp_loss.ToString());
                if (!string.IsNullOrWhiteSpace(User.CMP_ID)) dict.Add("cmp_id", User.CMP_ID);
                DBHelper.InsertSync(TABLE, dict);
            }
        }
        public static async Task<IEnumerable<StockPlanDayModel>> GetList(string sta_days, string sta_daye, string cmp_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(sta_days)) fdict.Add("sta_day", ">=" + sta_days);
            if (!string.IsNullOrWhiteSpace(sta_daye)) fdict.Add("重复字段1重复字段sta_day", "<=" + sta_daye);
            if (!string.IsNullOrWhiteSpace(cmp_id)) fdict.Add("cmp_id", cmp_id);
            else if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            return await DBHelper.GetList<StockPlanDayModel, int>(TABLE, "*", "id", fdict, "and");
        }
        public static StockPlanDayModel GetOneSync(DateTime day)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("sta_day", day.ToShortDateString());
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            else fdict.Add("cmp_id", "0");
            return DBHelper.GetOneSync<StockPlanDayModel, int>(TABLE, fdict, "and");
        }
        #endregion
    }
}
