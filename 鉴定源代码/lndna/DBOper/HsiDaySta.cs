using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class HsiDaySta
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        const string TABLE = "hsi_day_sta";
        #endregion
        #region 一般操作
        public static void Sta(DateTime day)
        {
            //每日1点运行，统计昨日数据
            if (GetOneSync(day) != null) return;

            int amount = 0;
            decimal money_debt = 0;
            decimal money_margin = 0;
            int oper_amount = 0;
            decimal profit = 0;
            decimal cmp_earn = 0;
            decimal cc_loss = 0;

            if (string.IsNullOrWhiteSpace(User.CMP_ID))
            {
                IEnumerable<HsiPlanModel> list = DBHelper.GetFreeSync<HsiPlanModel>("select * from hsi_plan where create_date='" + day.ToShortDateString() + "' and plan_status_id=7");
                if (list.Count() == 0) return;
                foreach (HsiPlanModel hpm in list)
                {
                    amount += hpm.stock_amount;
                    money_debt += hpm.money_debt;
                    oper_amount += hpm.oper_amount;
                    profit += hpm.profit;
                    cmp_earn += (hpm.cmp_earn + hpm.money_fee);
                    money_margin += hpm.money_margin;
                    if (hpm.profit + hpm.money_margin < 0) cc_loss += hpm.profit + hpm.money_margin;
                }
            }
            else
            {
                IEnumerable<HsiPlanSubModel> list = DBHelper.GetFreeSync<HsiPlanSubModel>("select * from hsi_plan_sub where create_date='" + day.ToShortDateString() + "' and plan_status_id=7 and cmp_id=" + User.CMP_ID);
                if (list.Count() == 0) return;
                foreach (HsiPlanSubModel hpm in list)
                {
                    amount += hpm.stock_amount;
                    money_debt += hpm.money_debt;
                    oper_amount += 1;
                    profit += hpm.profit;
                    cmp_earn += (0 + hpm.money_fee);
                    money_margin += hpm.money_margin;
                    if (hpm.profit + hpm.money_margin < 0) cc_loss += hpm.profit + hpm.money_margin;
                }
            }
            string sql = "select count(distinct user_id) from hsi_plan_sub where create_date='" + day.ToShortDateString() + "' and plan_status_id=7";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            long person = DBHelper.GetCountFreeSync(sql);

            if (person + amount + money_debt + oper_amount + profit + cmp_earn + money_margin + cc_loss > 0)
            {
                IDictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("sta_day", day.ToString("yyyy-MM-dd"));
                dict.Add("person", person.ToString());
                dict.Add("amount", amount.ToString());
                dict.Add("money_debt", money_debt.ToString());
                dict.Add("oper_amount", oper_amount.ToString());
                dict.Add("profit", profit.ToString());
                dict.Add("cmp_earn", cmp_earn.ToString());
                dict.Add("money_margin", money_margin.ToString());
                dict.Add("cc_loss", cc_loss.ToString());
                if (!string.IsNullOrWhiteSpace(User.CMP_ID)) dict.Add("cmp_id", User.CMP_ID);
                DBHelper.InsertSync(TABLE, dict);
            }
        }
        public static async Task<IEnumerable<HsiDayStaModel>> GetList(string sta_days, string sta_daye, string cmp_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(sta_days)) fdict.Add("sta_day", ">=" + sta_days);
            if (!string.IsNullOrWhiteSpace(sta_daye)) fdict.Add("重复字段1重复字段sta_day", "<=" + sta_daye);
            if (!string.IsNullOrWhiteSpace(cmp_id)) fdict.Add("cmp_id", cmp_id);
            else if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            return await DBHelper.GetList<HsiDayStaModel, int>(TABLE, "*", "id", fdict, "and");
        }
        public static HsiDayStaModel GetOneSync(DateTime sta_day)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("sta_day", sta_day.ToString("yyyy-MM-dd"));
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            else fdict.Add("cmp_id", "0");
            return DBHelper.GetOneSync<HsiDayStaModel, int>(TABLE, fdict, "and");
        }
        #endregion
    }
}
