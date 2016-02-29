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
    public static class HsiPlanSub
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        public const string TABLE = "hsi_plan_sub";
        #endregion
        #region 一般操作
        /// <summary>
        /// 申请
        /// </summary>
        public static async Task<string> Insert(ulong user_id, int amount)
        {
            if (user_id <= 0 || amount <= 0 || amount > 20) return "参数不全";
            decimal money_debt = 0;
            decimal money_margin = HsiDebt.MARGIN * amount;
            decimal money_fee = 0;
            HsiDebtModel hdm = HsiDebt.GetDebt(amount);
            if (hdm == null)
            {
                hdm = HsiDebt.GetDebt(1);
                if (hdm == null) return "参数异常";
                money_debt = hdm.money_debt * amount;
                money_fee = hdm.fee * amount;
            }
            else
            {
                money_debt = hdm.money_debt;
                money_fee = hdm.fee;
            }
            UserModel user = await User.GetOne(user_id, string.Empty, string.Empty, string.Empty, false);
            if (user == null) return "读取不到USER";
            if (user.money < money_margin + money_fee) return "账户余额不足";

            int hsi_account_id = 0;
            int hsi_account_sub_id = 0;
            if (user.hsi_account_sub_id <= 0)
                hsi_account_sub_id = HsiAccountSub.GetFreeId();
            else
                hsi_account_sub_id = user.hsi_account_sub_id;
            if (hsi_account_sub_id <= 0) return "点买名额已抢完";
            HsiAccountSubModel hasm = await HsiAccountSub.GetOne(hsi_account_sub_id, string.Empty);
            if (hasm == null) return "读取不到HASM";
            hsi_account_id = hasm.account_id;

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("user_id", user_id.ToString());
                    dict.Add("hsi_account_id", hsi_account_id.ToString());
                    dict.Add("hsi_account_sub_id", hsi_account_sub_id.ToString());
                    dict.Add("stock_amount", amount.ToString());
                    dict.Add("money_debt", money_debt.ToString());
                    dict.Add("money_margin", money_margin.ToString());
                    dict.Add("money_fee", money_fee.ToString());
                    dict.Add("create_date", DateTime.Today.ToString("yyyy-MM-dd"));
                    dict.Add("start_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("end_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("sell_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                    if (await DBHelper.Insert(TABLE, dict, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }
                    ulong newId = await DBHelper.GetNewId(dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("money", "数字相减-" + (money_margin + money_fee));
                    if (user.hsi_account_sub_id <= 0) dict.Add("hsi_account_sub_id", hsi_account_sub_id.ToString());
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user_id.ToString());
                    fdict.Add("重复字段1重复字段money", ">=" + (money_margin + money_fee));
                    if (user.hsi_account_sub_id <= 0) fdict.Add("重复字段1重复字段hsi_account_sub_id", "<=0");
                    if (await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    //if ((await UserMoneyChange.Insert(user_id, 2, -(money_margin + money_fee), user.money - (money_margin + money_fee),
                    //    "申请恒指点买", TABLE, newId, dbConnection, trans)) <= 0)
                    //{
                    //    trans.Rollback();
                    //    return "SQL执行错误";
                    //}
                    //2部分组成
                    if (money_fee > 0)
                    {
                        if ((await UserMoneyChange.Insert(user_id, 2, -money_fee, user.money - money_fee,
                            "申请恒指点买-管理费", TABLE, newId, user.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }
                    if ((await UserMoneyChange.Insert(user_id, 2, -money_margin, user.money - (money_margin + money_fee),
                            "申请恒指点买-保证金", TABLE, newId, user.cmp_id, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    if (user.hsi_account_sub_id <= 0) dict.Add("user_id", user_id.ToString());
                    dict.Add("last_plan_date", DateTime.Today.ToString("yyyy-MM-dd"));
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", hsi_account_sub_id.ToString());
                    if (user.hsi_account_sub_id <= 0) fdict.Add("重复字段1重复字段user_id", "0");
                    if (await DBHelper.Update(HsiAccountSub.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    if (user.hsi_account_sub_id <= 0)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("sub_used", "数字相加+1");
                        dict.Add("sub_free", "数字相减-1");
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", hsi_account_id.ToString());
                        if (await DBHelper.Update(HsiAccount.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    trans.Commit();
                }
            }
            await User.DeleteRedis(user_id);
            await HsiAccountSub.DeleteRedis(hsi_account_sub_id);
            if (user.hsi_account_sub_id <= 0) await HsiAccount.DeleteRedis(hsi_account_id);
            return "hasm:" + hasm.number + "," + hasm.pwd;
        }
        /// <summary>
        /// 入金前置
        /// </summary>
        public static async Task<string> InsMoney(ulong id)
        {
            if (id <= 0) return "参数异常";
            HsiPlanSubModel hpsm = await GetOne(id, 0);
            if (hpsm == null) return "读取不到MODEL";
            if (hpsm.plan_status_id != 1)
                return "该方案处于" + DictHsiPlanStatus.GetName(hpsm.plan_status_id) + "状态";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("plan_status_id", "2");
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            fdict.Add("重复字段1重复字段plan_status_id", "1");
            if (await DBHelper.Update(TABLE, dict, fdict, "and") <= 0)
            {
                return "SQL执行错误";
            }
            await DeleteRedis(id);
            return string.Empty;
        }
        /// <summary>
        /// 入金前撤单
        /// </summary>
        public static async Task<string> Cancel(ulong id, ulong user_id)
        {
            if (id <= 0) return "参数异常";
            HsiPlanSubModel hpsm = await GetOne(id, user_id);
            if (hpsm == null) return "读取不到MODEL";
            if (hpsm.plan_status_id != 1)
                return "该方案处于" + DictHsiPlanStatus.GetName(hpsm.plan_status_id) + "状态";
            if (user_id > 0 && hpsm.user_id != user_id) return "权限不足";
            if (!StockInfo.SIM_DATA)
            {
                if (DateTime.Now.AddMinutes(-30) < hpsm.start_time) return "发起半小时后才能撤单";
            }
            UserModel user = await User.GetOne(hpsm.user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return "读取不到USER";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("plan_status_id", "-1");
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", "1");
                    if (!StockInfo.SIM_DATA)
                        fdict.Add("重复字段1重复字段start_time", "<=" + DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss"));
                    if (await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("money", "数字相加+" + (hpsm.money_margin + hpsm.money_fee));
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", hpsm.user_id.ToString());
                    if (await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    //if ((await UserMoneyChange.Insert(hpsm.user_id, 1, hpsm.money_margin + hpsm.money_fee,
                    //    user.money + hpsm.money_margin + hpsm.money_fee,
                    //    "撤销恒指点买", TABLE, id, dbConnection, trans)) <= 0)
                    //{
                    //    trans.Rollback();
                    //    return "SQL执行错误";
                    //}
                    //2部分组成
                    if (hpsm.money_fee > 0)
                    {
                        if ((await UserMoneyChange.Insert(hpsm.user_id, 1, hpsm.money_fee,
                            user.money + hpsm.money_fee,
                            "撤销恒指点买-退还管理费", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }
                    if ((await UserMoneyChange.Insert(hpsm.user_id, 1, hpsm.money_margin,
                        user.money + hpsm.money_margin + hpsm.money_fee,
                        "撤销恒指点买-退还保证金", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    trans.Commit();
                }
            }
            await User.DeleteRedis(hpsm.user_id);
            await DeleteRedis(id);
            return string.Empty;
        }
        /// <summary>
        /// 入金后流单
        /// </summary>
        public static async Task<string> Abandon(ulong id, int sys_user_id)
        {
            if (id <= 0) return "参数异常";
            HsiPlanSubModel hpsm = await GetOne(id, 0);
            if (hpsm == null) return "读取不到MODEL";
            if (hpsm.plan_status_id > 2)
                return "该方案处于" + DictHsiPlanStatus.GetName(hpsm.plan_status_id) + "状态";
            UserModel user = await User.GetOne(hpsm.user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return "读取不到USER";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("plan_status_id", "-1");
                    dict.Add("sys_user_id", sys_user_id.ToString());
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", hpsm.plan_status_id.ToString());
                    if (await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("money", "数字相加+" + (hpsm.money_margin + hpsm.money_fee));
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", hpsm.user_id.ToString());
                    if (await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    //if ((await UserMoneyChange.Insert(hpsm.user_id, 1, hpsm.money_margin + hpsm.money_fee,
                    //    user.money + hpsm.money_margin + hpsm.money_fee,
                    //    "恒指点买失败", TABLE, id, dbConnection, trans)) <= 0)
                    //{
                    //    trans.Rollback();
                    //    return "SQL执行错误";
                    //}
                    //2部分组成
                    if (hpsm.money_fee > 0)
                    {
                        if ((await UserMoneyChange.Insert(hpsm.user_id, 1, hpsm.money_fee,
                            user.money + hpsm.money_fee,
                            "恒指点买失败-退还管理费", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }
                    if ((await UserMoneyChange.Insert(hpsm.user_id, 1, hpsm.money_margin,
                        user.money + hpsm.money_margin + hpsm.money_fee,
                        "恒指点买失败-退还保证金", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    trans.Commit();
                }
            }
            await User.DeleteRedis(hpsm.user_id);
            await DeleteRedis(id);
            return string.Empty;
        }
        /// <summary>
        /// 入金完毕
        /// </summary>
        public static async Task<string> InsMoneyOk(ulong id, int sys_user_id)
        {
            if (id <= 0) return "参数异常";
            HsiPlanSubModel hpsm = await GetOne(id, 0);
            if (hpsm == null) return "读取不到MODEL";
            if (hpsm.plan_status_id != 2)
                return "该方案处于" + DictHsiPlanStatus.GetName(hpsm.plan_status_id) + "状态";
            HsiPlanModel hpm = await HsiPlan.GetOne(0, hpsm.hsi_account_id, DateTime.Today);
            if (hpsm == null) return "读取不到HPM";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("plan_status_id", "3");
                    dict.Add("sys_user_id", sys_user_id.ToString());
                    dict.Add("create_date", DateTime.Today.ToString("yyyy-MM-dd"));
                    dict.Add("start_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", "2");
                    if (await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("stock_amount", "数字相加+" + hpsm.stock_amount);
                    dict.Add("money_debt", "数字相加+" + hpsm.money_debt);
                    dict.Add("money_margin", "数字相加+" + hpsm.money_margin);
                    dict.Add("money_fee", "数字相加+" + hpsm.money_fee);
                    dict.Add("sub_amount3", "数字相加+1");
                    dict.Add("plan_status_id", "3");
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", hpm.id.ToString());
                    if (await DBHelper.Update(HsiPlan.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    trans.Commit();
                }
            }
            await HsiPlan.DeleteRedis(hpm.id);
            await DeleteRedis(id);
            return string.Empty;
        }
        /// <summary>
        /// 申请平仓
        /// </summary>
        public static async Task<string> PreSell(ulong id, ulong user_id)
        {
            if (id <= 0) return "参数异常";
            HsiPlanSubModel hpsm = await GetOne(id, user_id);
            if (hpsm == null) return "读取不到MODEL";
            if (hpsm.plan_status_id != 3)
                return "该方案处于" + DictHsiPlanStatus.GetName(hpsm.plan_status_id) + "状态";
            if (user_id > 0 && hpsm.user_id != user_id) return "权限不足";
            HsiPlanModel hpm = await HsiPlan.GetOne(0, hpsm.hsi_account_id, hpsm.create_date);
            if (hpsm == null) return "读取不到HPM";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("plan_status_id", "4");
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", "3");
                    if (await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("sub_amount3", "数字相减-1");
                    dict.Add("sub_amount4", "数字相加+1");
                    if (hpm.sub_amount3 == 1) dict.Add("plan_status_id", "4");
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", hpm.id.ToString());
                    if (hpm.sub_amount3 == 1) fdict.Add("重复字段1重复字段sub_amount3", "1");
                    if (await DBHelper.Update(HsiPlan.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    trans.Commit();
                }
            }
            await HsiPlan.DeleteRedis(hpm.id);
            await DeleteRedis(id);
            return string.Empty;
        }
        /// <summary>
        /// 录入盈利
        /// </summary>
        public static async Task<string> Sell(ulong id, decimal profit, int sys_user_id)
        {
            if (id <= 0) return "参数异常";
            HsiPlanSubModel hpsm = await GetOne(id, 0);
            if (hpsm == null) return "读取不到MODEL";
            if (hpsm.plan_status_id != 3 && hpsm.plan_status_id != 4)
                return "该方案处于" + DictHsiPlanStatus.GetName(hpsm.plan_status_id) + "状态";
            HsiPlanModel hpm = await HsiPlan.GetOne(0, hpsm.hsi_account_id, hpsm.create_date);
            if (hpsm == null) return "读取不到HPM";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("plan_status_id", "5");
                    dict.Add("sys_user_id", sys_user_id.ToString());
                    dict.Add("profit", profit.ToString());
                    dict.Add("sell_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", hpsm.plan_status_id.ToString());
                    if (await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    if (hpsm.plan_status_id == 3) dict.Add("sub_amount3", "数字相减-1");
                    else if (hpsm.plan_status_id == 4) dict.Add("sub_amount4", "数字相减-1");
                    dict.Add("sub_amount5", "数字相加+1");
                    if (hpm.sub_amount3 + hpm.sub_amount4 == 1) dict.Add("plan_status_id", "5");
                    if (profit > 0) dict.Add("profit", "数字相加+" + profit);
                    else if (profit < 0)
                    {
                        dict.Add("profit", "数字相减-" + (-profit));
                        decimal cmp_earn = profit + hpsm.money_margin;
                        if (cmp_earn < 0) dict.Add("cmp_earn", "数字相减-" + (-cmp_earn));
                    }
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", hpm.id.ToString());
                    if (hpm.sub_amount3 + hpm.sub_amount4 == 1)
                    {
                        fdict.Add("重复字段1重复字段sub_amount3", hpm.sub_amount3.ToString());
                        fdict.Add("重复字段1重复字段sub_amount4", hpm.sub_amount4.ToString());
                    }
                    if (await DBHelper.Update(HsiPlan.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    trans.Commit();
                }
            }
            await HsiPlan.DeleteRedis(hpm.id);
            await DeleteRedis(id);
            return string.Empty;
        }
        /// <summary>
        /// 清算
        /// </summary>
        public static async Task<string> Settle(ulong id, int sys_user_id)
        {
            if (id <= 0) return "参数异常";
            HsiPlanSubModel hpsm = await GetOne(id, 0);
            return await Settle(hpsm, sys_user_id);
        }
        public static async Task<string> Settle(HsiPlanSubModel hpsm, int sys_user_id)
        {
            if (hpsm == null) return "读取不到MODEL";
            if (hpsm.plan_status_id != 6)
                return "该方案处于" + DictHsiPlanStatus.GetName(hpsm.plan_status_id) + "状态";
            UserModel user = await User.GetOne(hpsm.user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return "读取不到USER";
            HsiPlanModel hpm = await HsiPlan.GetOne(0, hpsm.hsi_account_id, hpsm.create_date);
            if (hpsm == null) return "读取不到HPM";
            decimal money = hpsm.profit + hpsm.money_margin;
            if (money < 0) money = 0;

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("plan_status_id", "7");
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", hpsm.id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", "6");
                    if (await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("sub_amount6", "数字相减-1");
                    dict.Add("sub_amount7", "数字相加+1");
                    if (hpm.sub_amount6 == 1)
                    {
                        dict.Add("plan_status_id", "7");
                        dict.Add("end_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        dict.Add("sys_user_id", sys_user_id.ToString());
                    }
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", hpm.id.ToString());
                    if (hpm.sub_amount6 == 1) fdict.Add("重复字段1重复字段sub_amount6", "1");
                    if (await DBHelper.Update(HsiPlan.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }


                    dict = new Dictionary<string, string>();
                    if (money > 0) dict.Add("money", "数字相加+" + money);
                    dict.Add("hsi_plan_amount", "数字相加+1");
                    if (hpsm.profit > 0) dict.Add("hsi_plan_profit", "数字相加+" + hpsm.profit);
                    else if (hpsm.profit < 0) dict.Add("hsi_plan_profit", "数字相减-" + (-hpsm.profit));
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", hpsm.user_id.ToString());
                    if (await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    if (money > 0)
                    {
                        //if ((await UserMoneyChange.Insert(hpsm.user_id, 1, money,
                        //    user.money + money,
                        //    "恒指点买清算", TABLE, hpsm.id, dbConnection, trans)) <= 0)
                        //{
                        //    trans.Rollback();
                        //    return "SQL执行错误";
                        //}
                        //拆分,2部分组成
                        if (hpsm.profit > 0)
                        {
                            if ((await UserMoneyChange.Insert(hpsm.user_id, 1, hpsm.profit,
                                user.money + hpsm.profit,
                                "恒指点买清算-盈利", TABLE, hpsm.id, user.cmp_id, dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                            if ((await UserMoneyChange.Insert(hpsm.user_id, 1, hpsm.money_margin,
                                user.money + money,
                                "恒指点买清算-退还保证金", TABLE, hpsm.id, user.cmp_id, dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                        }
                        else
                        {
                            if ((await UserMoneyChange.Insert(hpsm.user_id, 1, money,
                                user.money + money, "恒指点买清算-退还保证金", TABLE, hpsm.id,
                                 user.cmp_id, dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                        }
                    }

                    trans.Commit();
                }
            }
            await HsiPlan.DeleteRedis(hpm.id);
            await User.DeleteRedis(hpsm.user_id);
            await DeleteRedis(hpsm.id);
            if (user.money_npay > 0)
            {
                await UserMoneyChange.MoneyNPay(await User.GetOne(hpsm.user_id, string.Empty, string.Empty, string.Empty));
            }
            return string.Empty;
        }
        /// <summary>
        /// 修改盈亏
        /// </summary>
        public static async Task<string> UpdateProfit(ulong id, decimal profit, int sys_user_id)
        {
            if (id <= 0) return "参数异常";
            HsiPlanSubModel hpsm = await GetOne(id, 0);
            if (hpsm == null) return "读取不到MODEL";
            if (hpsm.plan_status_id < 5)
                return "该方案处于" + DictHsiPlanStatus.GetName(hpsm.plan_status_id) + "状态";
            UserModel user = await User.GetOne(hpsm.user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return "读取不到USER";
            HsiPlanModel hpm = await HsiPlan.GetOne(0, hpsm.hsi_account_id, hpsm.create_date);
            if (hpsm == null) return "读取不到HPM";
            decimal delta = profit - hpsm.profit;

            decimal cmp_earn_delta = 0;
            decimal cmp_earn_old = hpsm.profit + hpsm.money_margin;
            decimal cmp_earn = profit + hpsm.money_margin;
            if (cmp_earn_old > 0) cmp_earn_old = 0;
            if (cmp_earn > 0) cmp_earn = 0;
            if (cmp_earn < 0 || cmp_earn_old < 0)
            {
                cmp_earn_delta = cmp_earn - cmp_earn_old;
            }

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("profit", profit.ToString());
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if (await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    if (delta > 0) dict.Add("profit", "数字相加+" + delta);
                    else if (delta < 0) dict.Add("profit", "数字相减-" + (-delta));
                    if (cmp_earn_delta > 0) dict.Add("cmp_earn", "数字相加+" + cmp_earn_delta);
                    else if (cmp_earn_delta < 0) dict.Add("cmp_earn", "数字相减-" + (-cmp_earn_delta));
                    if (dict.Count > 0)
                    {
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", hpm.id.ToString());
                        if (await DBHelper.Update(HsiPlan.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    if (hpsm.plan_status_id == 7)
                    {
                        dict = new Dictionary<string, string>();
                        if (delta > 0)
                        {
                            dict.Add("money", "数字相加+" + delta);
                            dict.Add("hsi_plan_profit", "数字相加+" + delta);
                        }
                        else if (delta < 0)
                        {
                            dict.Add("money", "数字相减-" + (-delta));
                            dict.Add("hsi_plan_profit", "数字相减-" + (-delta));
                        }
                        if (dict.Count > 0)
                        {
                            fdict = new Dictionary<string, string>();
                            fdict.Add("id", hpsm.user_id.ToString());
                            if (await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                        }

                        if (delta > 0)
                        {
                            //1部分组成
                            if ((await UserMoneyChange.Insert(hpsm.user_id, 1, delta, user.money + delta,
                                "恒指点买重算收益", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                        }
                        else if (delta < 0)
                        {
                            //1部分组成
                            if ((await UserMoneyChange.Insert(hpsm.user_id, 2, delta, user.money + delta,
                                "恒指点买重算收益", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                        }
                    }

                    trans.Commit();
                }
            }
            if (hpsm.plan_status_id == 7 && delta != 0) await User.DeleteRedis(hpsm.user_id);
            if (delta != 0) await HsiPlan.DeleteRedis(hpm.id);
            await DeleteRedis(id);
            if (user.money_npay > 0)
            {
                await UserMoneyChange.MoneyNPay(await User.GetOne(hpsm.user_id, string.Empty, string.Empty, string.Empty));
            }
            return string.Empty;
        }
        public static async Task<IEnumerable<HsiPlanSubModel>> GetList(string user_id, string hsi_account_id, string hsi_account_sub_number,
            string plan_status_id, string create_dates, string create_datee, string page_size, string page_index)
        {
            if (!string.IsNullOrWhiteSpace(hsi_account_sub_number))
            {
                HsiAccountSubModel hasm = await HsiAccountSub.GetOne(0, hsi_account_sub_number);
                if (hasm != null)
                    hsi_account_sub_number = hasm.id.ToString();
            }

            IEnumerable<HsiPlanSubModel> list = null;
            if (!string.IsNullOrWhiteSpace(plan_status_id) && plan_status_id.Contains(","))
            {
                string filter = string.Empty;
                foreach (string pid in plan_status_id.Split(','))
                {
                    filter += " or plan_status_id=" + pid;
                }
                filter = " and (" + filter.Substring(4) + ")";
                if (!string.IsNullOrWhiteSpace(user_id)) filter += " and user_id=" + user_id;
                if (!string.IsNullOrWhiteSpace(hsi_account_id)) filter += " and hsi_account_id=" + hsi_account_id;
                if (!string.IsNullOrWhiteSpace(hsi_account_sub_number)) filter += " and hsi_account_sub_number='" + hsi_account_sub_number + "'";
                if (!string.IsNullOrWhiteSpace(create_dates)) filter += " and create_date>='" + create_dates + "'";
                if (!string.IsNullOrWhiteSpace(create_datee)) filter += " and create_date<='" + create_datee + "'";
                list = await DBHelper.GetFreeList<HsiPlanSubModel, long>("select id from hsi_plan_sub where " + filter.Substring(5) + " order by id desc",
                    TABLE, "*", "id desc", Convert.ToInt32(page_size), Convert.ToInt32(page_index));
            }
            else
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
                if (!string.IsNullOrWhiteSpace(hsi_account_id)) fdict.Add("hsi_account_id", hsi_account_id);
                if (!string.IsNullOrWhiteSpace(hsi_account_sub_number)) fdict.Add("hsi_account_sub_id", hsi_account_sub_number);
                if (!string.IsNullOrWhiteSpace(plan_status_id)) fdict.Add("plan_status_id", plan_status_id);
                if (!string.IsNullOrWhiteSpace(create_dates)) fdict.Add("create_date", ">=" + create_dates);
                if (!string.IsNullOrWhiteSpace(create_datee)) fdict.Add("重复字段1重复字段create_date", "<=" + create_datee);
                list = await DBHelper.GetList<HsiPlanSubModel, long>(TABLE, "*", "id desc", fdict, "and",
                            Convert.ToInt32(page_size), Convert.ToInt32(page_index));
            }
            return await HsiAccount.FillFkInfo(await HsiAccountSub.FillFkInfo(await User.FillFkInfo(list)));
        }
        public static async Task<long> GetCount(string user_id, string hsi_account_id, string hsi_account_sub_number,
            string plan_status_id, string create_dates, string create_datee)
        {
            if (!string.IsNullOrWhiteSpace(hsi_account_sub_number))
            {
                HsiAccountSubModel hasm = await HsiAccountSub.GetOne(0, hsi_account_sub_number);
                if (hasm != null)
                    hsi_account_sub_number = hasm.id.ToString();
            }

            if (!string.IsNullOrWhiteSpace(plan_status_id) && plan_status_id.Contains(","))
            {
                string filter = string.Empty;
                foreach (string pid in plan_status_id.Split(','))
                {
                    filter += " or plan_status_id=" + pid;
                }
                filter = " and (" + filter.Substring(4) + ")";
                if (!string.IsNullOrWhiteSpace(user_id)) filter += " and user_id=" + user_id;
                if (!string.IsNullOrWhiteSpace(hsi_account_id)) filter += " and hsi_account_id=" + hsi_account_id;
                if (!string.IsNullOrWhiteSpace(hsi_account_sub_number)) filter += " and hsi_account_sub_number='" + hsi_account_sub_number + "'";
                if (!string.IsNullOrWhiteSpace(create_dates)) filter += " and create_date>='" + create_dates + "'";
                if (!string.IsNullOrWhiteSpace(create_datee)) filter += " and create_date<='" + create_datee + "'";
                return await DBHelper.GetCountFree("select count(id) from hsi_plan_sub where " + filter.Substring(5));
            }
            else
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
                if (!string.IsNullOrWhiteSpace(hsi_account_id)) fdict.Add("hsi_account_id", hsi_account_id);
                if (!string.IsNullOrWhiteSpace(hsi_account_sub_number)) fdict.Add("hsi_account_sub_id", hsi_account_sub_number);
                if (!string.IsNullOrWhiteSpace(plan_status_id)) fdict.Add("plan_status_id", plan_status_id);
                if (!string.IsNullOrWhiteSpace(create_dates)) fdict.Add("create_date", ">=" + create_dates);
                if (!string.IsNullOrWhiteSpace(create_datee)) fdict.Add("重复字段1重复字段create_date", "<=" + create_datee);
                return await DBHelper.GetCount(TABLE, fdict, "and");
            }
        }
        public static async Task<HsiPlanSubModel> GetOne(ulong id, ulong user_id)
        {
            if (id <= 0) return null;
            HsiPlanSubModel hpsm = await DBHelper.GetOne<HsiPlanSubModel>(id.ToString());
            if (user_id > 0 && hpsm.user_id != user_id) return null;
            if (hpsm != null)
            {
                HsiAccountSubModel hasm = await HsiAccountSub.GetOne(hpsm.hsi_account_sub_id, string.Empty);
                if (hasm != null)
                {
                    hpsm.hsi_account_sub_number = hasm.number;
                    hpsm.hsi_account_sub_pwd = hasm.pwd;
                }
            }
            return hpsm;
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<HsiPlanSubModel>(id.ToString());
        }
        #endregion
        #region 获取任务
        public static async Task<string> GetTask()
        {
            string task = string.Empty;
            long count = await DBHelper.GetCountFree("select count(*) from hsi_plan_sub where plan_status_id=1");
            task = (count + ",");
            count = await DBHelper.GetCountFree("select count(*) from hsi_plan_sub where plan_status_id=4");
            task += count;
            return task;
        }
        #endregion
    }
}
