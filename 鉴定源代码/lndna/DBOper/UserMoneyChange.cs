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
    public static class UserMoneyChange
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        const string TABLE = "user_money_change";
        #endregion
        #region 一般操作
        /// <summary>
        /// 管理员人工调整实盘资金
        /// </summary>
        public static async Task<string> Update(ulong user_id, decimal money, string info, string ref_table, ulong ref_id, int sys_user_id)
        {
            if (user_id <= 0 || money == 0) return "参数不全";
            if (info.Length > 200 || ref_table.Length > 50) return "内容过长";

            UserModel user = await User.GetOne(user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return "无法读取USER";
            decimal final_money = user.money + money;
            string money_flow_id = (money > 0 ? "1" : "2");

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("user_id", user_id.ToString());
                    dict.Add("money_flow_id", money_flow_id);
                    dict.Add("money", money.ToString());
                    dict.Add("final_money", final_money.ToString());
                    if (!string.IsNullOrWhiteSpace(info)) dict.Add("info", info);
                    if (!string.IsNullOrWhiteSpace(ref_table)) dict.Add("ref_table", ref_table);
                    if (ref_id > 0) dict.Add("ref_id", ref_id.ToString());
                    dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("sys_user_id", sys_user_id.ToString());
                    if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                    if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    dict = new Dictionary<string, string>();
                    if (money > 0) dict.Add("money", "数字相加+" + money);
                    else dict.Add("money", "数字相减-" + (-money));
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user_id.ToString());
                    if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    trans.Commit();
                }
            }

            await User.DeleteRedis(user.id);
            if (user.money_npay > 0)
            {
                await MoneyNPay(await User.GetOne(user_id, string.Empty, string.Empty, string.Empty));
            }
            return string.Empty;
        }
        public static async Task<decimal> MoneyNPay(UserModel user)
        {
            if (user == null) return 0;
            if (user.money_npay <= 0 || user.money <= 0) return user.money;
            decimal npay = 0;
            if (user.money >= user.money_npay) npay = user.money_npay;
            else npay = user.money;

            IEnumerable<StockPlanModel> list = await DBHelper.GetFree<StockPlanModel>("select * from stock_plan where user_id='" + user.id + "' and money_npay>0");
            IDictionary<ulong, decimal> mDict = new Dictionary<ulong, decimal>();
            decimal total = 0;
            foreach (StockPlanModel plan in list)
            {
                if (plan.money_npay <= 0) continue;
                decimal left = npay - total;
                if (left <= 0) break;
                if (left > plan.money_npay) left = plan.money_npay;
                total += left;
                mDict.Add(plan.id, left);
            }

            decimal money = user.money;

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("money", "数字相减-" + npay);
                    dict.Add("money_npay", "数字相减-" + npay);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user.id.ToString());
                    fdict.Add("重复字段1重复字段money", ">=" + npay);
                    fdict.Add("重复字段1重复字段money_npay", ">=" + npay);
                    if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return user.money;
                    }

                    foreach (ulong id in mDict.Keys)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("money_npay", "数字相减-" + mDict[id]);
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", id.ToString());
                        fdict.Add("重复字段1重复字段money_npay", ">=" + mDict[id]);
                        if ((await DBHelper.Update(StockPlan.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return user.money;
                        }

                        money -= mDict[id];

                        dict = new Dictionary<string, string>();
                        dict.Add("user_id", user.id.ToString());
                        dict.Add("money_flow_id", "2");
                        dict.Add("money", "-" + mDict[id]);
                        dict.Add("final_money", money.ToString());
                        dict.Add("info", "欠费补交");
                        dict.Add("ref_table", StockPlan.TABLE);
                        dict.Add("ref_id", id.ToString());
                        dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                        if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return user.money;
                        }
                    }

                    if (npay - total > 0)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("user_id", user.id.ToString());
                        dict.Add("money_flow_id", "2");
                        dict.Add("money", "-" + (npay - total));
                        dict.Add("final_money", money.ToString());
                        dict.Add("info", "欠费补交");
                        dict.Add("ref_table", User.TABLE);
                        dict.Add("ref_id", user.id.ToString());
                        dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                        if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return user.money;
                        }
                    }

                    trans.Commit();
                }
            }

            await User.DeleteRedis(user.id);
            return user.money - npay;
        }
        public static decimal MoneyNPaySync(UserModel user)
        {
            if (user == null) return 0;
            if (user.money_npay <= 0 || user.money <= 0) return user.money;
            decimal npay = 0;
            if (user.money >= user.money_npay) npay = user.money_npay;
            else npay = user.money;

            IEnumerable<StockPlanModel> list = DBHelper.GetFreeSync<StockPlanModel>("select * from stock_plan where user_id='" + user.id + "' and money_npay>0");
            IDictionary<ulong, decimal> mDict = new Dictionary<ulong, decimal>();
            decimal total = 0;
            foreach (StockPlanModel plan in list)
            {
                if (plan.money_npay <= 0) continue;
                decimal left = npay - total;
                if (left <= 0) break;
                if (left > plan.money_npay) left = plan.money_npay;
                total += left;
                mDict.Add(plan.id, left);
            }

            decimal money = user.money;

            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("money", "数字相减-" + npay);
                    dict.Add("money_npay", "数字相减-" + npay);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user.id.ToString());
                    fdict.Add("重复字段1重复字段money", ">=" + npay);
                    fdict.Add("重复字段1重复字段money_npay", ">=" + npay);
                    if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return user.money;
                    }

                    foreach (ulong id in mDict.Keys)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("money_npay", "数字相减-" + mDict[id]);
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", id.ToString());
                        fdict.Add("重复字段1重复字段money_npay", ">=" + mDict[id]);
                        if ((DBHelper.UpdateSync(StockPlan.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return user.money;
                        }

                        money -= mDict[id];

                        dict = new Dictionary<string, string>();
                        dict.Add("user_id", user.id.ToString());
                        dict.Add("money_flow_id", "2");
                        dict.Add("money", "-" + mDict[id]);
                        dict.Add("final_money", money.ToString());
                        dict.Add("info", "欠费补交");
                        dict.Add("ref_table", StockPlan.TABLE);
                        dict.Add("ref_id", id.ToString());
                        dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                        if ((DBHelper.InsertSync(TABLE, dict, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return user.money;
                        }
                    }

                    if (npay - total > 0)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("user_id", user.id.ToString());
                        dict.Add("money_flow_id", "2");
                        dict.Add("money", "-" + (npay - total));
                        dict.Add("final_money", money.ToString());
                        dict.Add("info", "欠费补交");
                        dict.Add("ref_table", User.TABLE);
                        dict.Add("ref_id", user.id.ToString());
                        dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                        if ((DBHelper.InsertSync(TABLE, dict, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return user.money;
                        }
                    }

                    trans.Commit();
                }
            }

            User.DeleteRedisSync(user.id);
            return user.money - npay;
        }
        public static async Task<int> Insert(ulong user_id, int money_flow_id, decimal money, decimal final_money, string info,
            string ref_table, ulong ref_id, int cmp_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (user_id <= 0 || money_flow_id <= 0 || money == 0) return -1;
            if (info.Length > 200 || ref_table.Length > 50) return -1;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("user_id", user_id.ToString());
            dict.Add("money_flow_id", money_flow_id.ToString());
            dict.Add("money", money.ToString());
            dict.Add("final_money", final_money.ToString());
            if (!string.IsNullOrWhiteSpace(info)) dict.Add("info", info);
            if (!string.IsNullOrWhiteSpace(ref_table)) dict.Add("ref_table", ref_table);
            if (ref_id > 0) dict.Add("ref_id", ref_id.ToString());
            dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            dict.Add("cmp_id", cmp_id.ToString());
            return (await DBHelper.Insert(TABLE, dict, dbConnection, trans));
        }
        public static int InsertSync(ulong user_id, int money_flow_id, decimal money, decimal final_money, string info,
            string ref_table, ulong ref_id, int cmp_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (user_id <= 0 || money_flow_id <= 0 || money == 0) return -1;
            if (info.Length > 200 || ref_table.Length > 50) return -1;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("user_id", user_id.ToString());
            dict.Add("money_flow_id", money_flow_id.ToString());
            dict.Add("money", money.ToString());
            dict.Add("final_money", final_money.ToString());
            if (!string.IsNullOrWhiteSpace(info)) dict.Add("info", info);
            if (!string.IsNullOrWhiteSpace(ref_table)) dict.Add("ref_table", ref_table);
            if (ref_id > 0) dict.Add("ref_id", ref_id.ToString());
            dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            dict.Add("cmp_id", cmp_id.ToString());
            return (DBHelper.InsertSync(TABLE, dict, dbConnection, trans));
        }
        public static async Task<IEnumerable<UserMoneyChangeModel>> GetList(string user_id, string money_flow_id,
            string ref_table, string ref_id, string create_times, string create_timee,
            string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            if (!string.IsNullOrWhiteSpace(money_flow_id)) fdict.Add("money_flow_id", money_flow_id);
            if (!string.IsNullOrWhiteSpace(ref_table)) fdict.Add("ref_table", ref_table);
            if (!string.IsNullOrWhiteSpace(ref_id)) fdict.Add("ref_id", ref_id);
            if (!string.IsNullOrWhiteSpace(create_times)) fdict.Add("create_time", ">=" + create_times);
            if (!string.IsNullOrWhiteSpace(create_timee)) fdict.Add("重复字段1重复字段create_time", "<=" + create_timee + " 23:59:59");
            IEnumerable<UserMoneyChangeModel> list = await DBHelper.GetList<UserMoneyChangeModel, long>(TABLE, "*", "id desc", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));
            return await User.FillFkInfo(list);
        }
        public static async Task<long> GetCount(string user_id, string money_flow_id, string ref_table, string ref_id,
            string create_times, string create_timee)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            if (!string.IsNullOrWhiteSpace(money_flow_id)) fdict.Add("money_flow_id", money_flow_id);
            if (!string.IsNullOrWhiteSpace(ref_table)) fdict.Add("ref_table", ref_table);
            if (!string.IsNullOrWhiteSpace(ref_id)) fdict.Add("ref_id", ref_id);
            if (!string.IsNullOrWhiteSpace(create_times)) fdict.Add("create_time", ">=" + create_times);
            if (!string.IsNullOrWhiteSpace(create_timee)) fdict.Add("重复字段1重复字段create_time", "<=" + create_timee + " 23:59:59");
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<string> Exp(string user_id, string money_flow_id, string ref_table, string ref_id,
              string create_times, string create_timee)
        {
            IDictionary<string, string> cols = new Dictionary<string, string>();
            cols.Add("id", "id");
            cols.Add("客户ID", "user_id");
            cols.Add("昵称", "user_nick_name");
            cols.Add("电话", "user_mobile");
            cols.Add("流向", "money_flow_id");
            cols.Add("变动实盘资金", "money");
            cols.Add("最终实盘资金", "final_money");
            cols.Add("时间", "create_time");
            cols.Add("备注", "info");
            IEnumerable<UserMoneyChangeModel> list = await GetList(user_id, money_flow_id,
                    ref_table, ref_id, create_times, create_timee, "0", "0");

            return Excel.MakeXmlForExcel(list, cols);
        }
        #endregion
        #region 处理已结算的方案，清除欠费
        public static async Task StockPlanFinish(ulong id)
        {
            if (id <= 0) return;
            StockPlanModel plan = await StockPlan.GetOne(id, 0);
            if (plan == null) return;
            if (plan.plan_status_id != 7) return;
            if (plan.money_npay <= 0) return;

            UserModel user = await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return;
            if (user.money_npay <= 0) return;

            plan = await StockPlan.GetOne(id, 0);
            if (plan == null) return;
            if (plan.plan_status_id != 7) return;
            if (plan.money_npay <= 0) return;

            decimal npay = 0;
            if (user.money > plan.money_npay) npay = plan.money_npay;
            else npay = user.money;

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    if (npay > 0) dict.Add("money", "数字相减-" + npay);
                    dict.Add("money_npay", "数字相减-" + plan.money_npay);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user.id.ToString());
                    if (npay > 0) fdict.Add("重复字段1重复字段money", ">=" + npay);
                    fdict.Add("重复字段1重复字段money_npay", ">=" + plan.money_npay);
                    if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return;
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("money_npay", "0");
                    if (string.IsNullOrWhiteSpace(plan.remark)) dict.Add("remark", "免除递延费" + plan.money_npay + "元");
                    else dict.Add("remark", plan.remark + "[免除递延费" + plan.money_npay + "元]");
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段money_npay", plan.money_npay.ToString());
                    if ((await DBHelper.Update(StockPlan.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return;
                    }

                    if (npay > 0)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("user_id", user.id.ToString());
                        dict.Add("money_flow_id", "2");
                        dict.Add("money", "-" + npay);
                        dict.Add("final_money", (user.money - npay).ToString());
                        dict.Add("info", "欠费补交");
                        dict.Add("ref_table", StockPlan.TABLE);
                        dict.Add("ref_id", id.ToString());
                        dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                        if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return;
                        }
                    }

                    trans.Commit();
                }
            }

            await User.DeleteRedis(user.id);
            await StockPlan.DeleteRedis(id);
        }
        public static void StockPlanFinishSync(ulong id)
        {
            if (id <= 0) return;
            StockPlanModel plan = StockPlan.GetOneSync(id);
            if (plan == null) return;
            if (plan.plan_status_id != 7) return;
            if (plan.money_npay <= 0) return;

            UserModel user = User.GetOneSync(plan.user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return;
            if (user.money_npay <= 0) return;

            plan = StockPlan.GetOneSync(id);
            if (plan == null) return;
            if (plan.plan_status_id != 7) return;
            if (plan.money_npay <= 0) return;

            decimal npay = 0;
            if (user.money > plan.money_npay) npay = plan.money_npay;
            else npay = user.money;

            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    if (npay > 0) dict.Add("money", "数字相减-" + npay);
                    dict.Add("money_npay", "数字相减-" + plan.money_npay);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user.id.ToString());
                    if (npay > 0) fdict.Add("重复字段1重复字段money", ">=" + npay);
                    fdict.Add("重复字段1重复字段money_npay", ">=" + plan.money_npay);
                    if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return;
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("money_npay", "0");
                    if (string.IsNullOrWhiteSpace(plan.remark)) dict.Add("remark", "免除递延费" + plan.money_npay + "元");
                    else dict.Add("remark", plan.remark + "[免除递延费" + plan.money_npay + "元]");
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段money_npay", plan.money_npay.ToString());
                    if ((DBHelper.UpdateSync(StockPlan.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return;
                    }

                    if (npay > 0)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("user_id", user.id.ToString());
                        dict.Add("money_flow_id", "2");
                        dict.Add("money", "-" + npay);
                        dict.Add("final_money", (user.money - npay).ToString());
                        dict.Add("info", "欠费补交");
                        dict.Add("ref_table", StockPlan.TABLE);
                        dict.Add("ref_id", id.ToString());
                        dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                        if ((DBHelper.InsertSync(TABLE, dict, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return;
                        }
                    }

                    trans.Commit();
                }
            }

            User.DeleteRedisSync(user.id);
            StockPlan.DeleteRedisSync(id);
        }
        #endregion
    }
}
