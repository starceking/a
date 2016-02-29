using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class HsiPlan
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        public const string TABLE = "hsi_plan";
        #endregion
        #region 一般操作
        /// <summary>
        /// 统一结算
        /// </summary>
        public static async Task<string> PreSettle(ulong id, int oper_amount, int sys_user_id)
        {
            if (id <= 0) return "参数异常";
            if (oper_amount <= 0) return "请录入操盘次数";
            HsiPlanModel hpm = await GetOne(id, 0, null);
            if (hpm == null) return "读取不到MODEL";
            if (hpm.plan_status_id != 5)
                return "母方案处于" + DictHsiPlanStatus.GetName(hpm.plan_status_id) + "状态";
            if (hpm.sub_amount3 + hpm.sub_amount4 > 0) return "尚有未平仓的订单";

            IEnumerable<HsiPlanSubModel> list = await DBHelper.GetFree<HsiPlanSubModel>(
                "select * from hsi_plan_sub where hsi_account_id=" + hpm.hsi_account_id + " and create_date='" + hpm.create_date.ToShortDateString() +
                "' and plan_status_id=5");
            if (hpm.sub_amount5 != list.Count()) return "母方案数据异常";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("plan_status_id", "6");
                    dict.Add("sub_amount5", "0");
                    dict.Add("sub_amount6", hpm.sub_amount5.ToString());
                    dict.Add("oper_amount", oper_amount.ToString());
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", "5");
                    fdict.Add("重复字段1重复字段sub_amount5", hpm.sub_amount5.ToString());
                    if (await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("plan_status_id", "6");
                    fdict = new Dictionary<string, string>();
                    fdict.Add("hsi_account_id", hpm.hsi_account_id.ToString());
                    fdict.Add("create_date", hpm.create_date.ToShortDateString());
                    fdict.Add("重复字段1重复字段plan_status_id", "5");
                    int rows = await DBHelper.Update(HsiPlanSub.TABLE, dict, fdict, "and", dbConnection, trans);
                    if (rows != list.Count())
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    trans.Commit();
                }
            }

            foreach (HsiPlanSubModel hpsm in list)
            {
                await HsiPlanSub.DeleteRedis(hpsm.id);
            }
            await DeleteRedis(hpm.id);

            return string.Empty;
        }
        /// <summary>
        /// 统一清算
        /// </summary>
        public static async Task<string> Settle(ulong id, int sys_user_id)
        {
            if (id <= 0) return "参数异常";
            HsiPlanModel hpm = await GetOne(id, 0, null);
            if (hpm == null) return "读取不到MODEL";
            if (hpm.plan_status_id < 6)
                return "母方案处于" + DictHsiPlanStatus.GetName(hpm.plan_status_id) + "状态";
            if (hpm.sub_amount3 + hpm.sub_amount4 + hpm.sub_amount5 > 0) return "尚有未清算的订单";

            IEnumerable<HsiPlanSubModel> list = await HsiPlanSub.GetList(string.Empty, hpm.hsi_account_id.ToString(), string.Empty, "6",
                hpm.create_date.ToShortDateString(), hpm.create_date.ToShortDateString(), "0", "0");
            foreach (HsiPlanSubModel hpsm in list)
            {
                await HsiPlanSub.Settle(hpsm, sys_user_id);
            }

            return string.Empty;
        }
        public static async Task<string> UpdateOperAmount(ulong id, int oper_amount)
        {
            if (id <= 0 || oper_amount <= 0) return "参数不全";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("oper_amount", oper_amount.ToString());
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if (await DBHelper.Update(TABLE, dict, fdict, "and") <= 0)
            {
                return "SQL执行错误";
            }
            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<IEnumerable<HsiPlanModel>> GetList(string hsi_account_id, string plan_status_id,
            string create_dates, string create_datee, string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(hsi_account_id)) fdict.Add("hsi_account_id", hsi_account_id);
            if (!string.IsNullOrWhiteSpace(plan_status_id)) fdict.Add("plan_status_id", plan_status_id);
            if (!string.IsNullOrWhiteSpace(create_dates)) fdict.Add("create_date", ">=" + create_dates);
            if (!string.IsNullOrWhiteSpace(create_datee)) fdict.Add("重复字段1重复字段create_date", "<=" + create_datee);
            IEnumerable<HsiPlanModel> list = await DBHelper.GetList<HsiPlanModel, long>(TABLE, "*", "id desc", fdict, "and",
                        Convert.ToInt32(page_size), Convert.ToInt32(page_index));
            return await HsiAccount.FillFkInfo(list);
        }
        public static async Task<long> GetCount(string hsi_account_id, string plan_status_id,
            string create_dates, string create_datee)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(hsi_account_id)) fdict.Add("hsi_account_id", hsi_account_id);
            if (!string.IsNullOrWhiteSpace(plan_status_id)) fdict.Add("plan_status_id", plan_status_id);
            if (!string.IsNullOrWhiteSpace(create_dates)) fdict.Add("create_date", ">=" + create_dates);
            if (!string.IsNullOrWhiteSpace(create_datee)) fdict.Add("重复字段1重复字段create_date", "<=" + create_datee);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<HsiPlanModel> GetOne(ulong id, int hsi_account_id, DateTime? create_date, bool readA = false)
        {
            HsiPlanModel hpm = null;
            if (id > 0)
                hpm = await DBHelper.GetOne<HsiPlanModel>(id.ToString());
            else
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("hsi_account_id", hsi_account_id.ToString());
                fdict.Add("create_date", create_date.Value.ToString("yyyy-MM-dd"));
                hpm = await DBHelper.GetOne<HsiPlanModel, long>(TABLE, fdict, "and");
                if (hpm == null)
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("hsi_account_id", hsi_account_id.ToString());
                    dict.Add("create_date", create_date.Value.ToString("yyyy-MM-dd"));
                    dict.Add("end_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    await DBHelper.Insert(TABLE, dict);

                    fdict = new Dictionary<string, string>();
                    fdict.Add("hsi_account_id", hsi_account_id.ToString());
                    fdict.Add("create_date", create_date.Value.ToString("yyyy-MM-dd"));
                    string key = DBHelper.MakeMultiIdKey(TABLE, fdict, "and");
                    await Redis.DeleteKey(key);

                    return await GetOne(0, hsi_account_id, create_date);
                }
            }
            if (hpm != null && readA)
            {
                HsiAccountModel ham = await HsiAccount.GetOne(hpm.hsi_account_id);
                if (ham != null) hpm.hsi_account_number = ham.number;
            }
            return hpm;
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<HsiPlanModel>(id.ToString());
        }
        #endregion
    }
}
