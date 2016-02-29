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
    public static class StockPlanTrade
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        const string TABLE = "stock_plan_trade";
        #endregion
        #region 一般操作
        /// <summary>
        /// 数据录入错误的更改
        /// </summary>
        /// <returns></returns>
        public static async Task<string> Update(ulong id, DateTime trade_time, decimal price, int amount,
            string trade_no, int sys_user_id)
        {
            if (id <= 0 || trade_time == null || price <= 0 || amount <= 0) return "参数不全";
            if (!string.IsNullOrWhiteSpace(trade_no) && trade_no.Length > 50) trade_no = trade_no.Substring(0, 50);
            StockPlanTradeModel trade = await GetOne(id);
            if (trade == null) return "无法读取TRADE";
            if (sys_user_id > 0 && trade.sys_user_id != sys_user_id) return "权限异常";
            StockPlanModel plan = await StockPlan.GetOne(trade.plan_id, 0);
            if (plan == null) return "无法读取PLAN";
            if (plan.plan_status_id == 1 || plan.plan_status_id == 2 || plan.plan_status_id == -1) return "该状态下不可更改";
            if (sys_user_id > 0 && plan.sys_user_id != sys_user_id) return "权限异常";
            SysUserModel suser = await SysUser.GetOne(plan.sys_user_id, string.Empty);
            if (suser == null) return "读取不到SUSER";
            decimal delta = trade.price * trade.amount - price * amount;

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("trade_time", trade_time.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("price", price.ToString());
                    dict.Add("amount", amount.ToString());
                    if (!string.IsNullOrWhiteSpace(trade_no)) dict.Add("trade_no", trade_no);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    if (trade.buy_or_sell == 1)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("stock_amount", amount.ToString());
                        dict.Add("buy_price", price.ToString());
                        if (plan.buy_price != price || plan.stock_amount != amount)
                        {
                            decimal stop_earn_point = plan.stop_earn_percent * plan.money_debt / amount / 100 + price;
                            decimal stop_loss_point = price - plan.stop_loss_money / amount;
                            dict.Add("stop_earn_point", stop_earn_point.ToString());
                            dict.Add("stop_loss_point", stop_loss_point.ToString());
                        }
                        dict.Add("buy_time", trade_time.ToString("yyyy-MM-dd HH:mm:ss"));
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.id.ToString());
                        if ((await DBHelper.Update(StockPlan.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "异常，请重试";
                        }
                    }

                    if (trade.price != price || trade.amount != amount)
                    {
                        await StockPlanLog.Insert(plan.id, "更改持仓", "交易ID：" + id +
                            "；数量：" + trade.amount + "->" + amount + "；价格：" + trade.price + "->" + price,
                            sys_user_id, dbConnection, trans);
                    }

                    if (trade.buy_or_sell == -1) delta = -delta;
                    dict = new Dictionary<string, string>();
                    if (delta > 0) dict.Add("stock_money", "数字相加+" + delta);
                    else if (delta < 0) dict.Add("stock_money", "数字相减-" + (-delta));
                    if (dict.Count > 0)
                    {
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.sys_user_id.ToString());
                        if ((await DBHelper.Update(SysUser.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                        if ((await SysUserStockMoneyChange.Insert(plan.sys_user_id, delta > 0 ? 1 : 2, delta,
                            suser.stock_money + delta, "更改持仓", TABLE, id,
                            plan.stock_no, plan.stock_name, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            if (trade.buy_or_sell == 1) await StockPlan.DeleteRedis(plan.id);
            else if (trade.price != price || trade.amount != amount) await StockPlan.ReCalc(plan.id, sys_user_id);
            if (delta != 0) await SysUser.DeleteRedis(suser.id);
            return string.Empty;
        }
        public static async Task<int> Insert(ulong plan_id, int buy_or_sell, DateTime trade_time, decimal price, int amount,
            string trade_no, int sys_user_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (plan_id <= 0 || trade_time == null || price <= 0 || amount <= 0) return -1;
            if (!string.IsNullOrWhiteSpace(trade_no) && trade_no.Length > 50) trade_no = trade_no.Substring(0, 50);

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("plan_id", plan_id.ToString());
            dict.Add("buy_or_sell", buy_or_sell.ToString());
            dict.Add("trade_time", trade_time.ToString("yyyy-MM-dd HH:mm:ss"));
            dict.Add("price", price.ToString());
            dict.Add("amount", amount.ToString());
            if (!string.IsNullOrWhiteSpace(trade_no)) dict.Add("trade_no", trade_no);
            if (sys_user_id > 0) dict.Add("sys_user_id", sys_user_id.ToString());
            return (await DBHelper.Insert(TABLE, dict, dbConnection, trans));
        }
        public static async Task<IEnumerable<StockPlanTradeModel>> GetList(string plan_id)
        {
            if (string.IsNullOrWhiteSpace(plan_id)) return null;
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("plan_id", plan_id);
            return await DBHelper.GetList<StockPlanTradeModel, long>(TABLE, "*", "id", fdict, "and");
        }
        public static IEnumerable<StockPlanTradeModel> GetListSync(string plan_id)
        {
            if (string.IsNullOrWhiteSpace(plan_id)) return null;
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("plan_id", plan_id);
            return DBHelper.GetListSync<StockPlanTradeModel, long>(TABLE, "*", "id", fdict, "and");
        }
        public static async Task<StockPlanTradeModel> GetOne(ulong id)
        {
            if (id <= 0) return null;
            return await DBHelper.GetOne<StockPlanTradeModel>(id.ToString());
        }
        public static StockPlanTradeModel GetOneSync(ulong id)
        {
            if (id <= 0) return null;
            return DBHelper.GetOneSync<StockPlanTradeModel>(id.ToString());
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<StockPlanTradeModel>(id.ToString());
        }
        public static void DeleteRedisSync(ulong id)
        {
            if (id <= 0) return;
            Redis.DeleteSync<StockPlanTradeModel>(id.ToString());
        }
        #endregion
        #region 操作回撤
        public static async Task<string> Delete(ulong id, int sys_user_id)
        {
            if (id <= 0 || sys_user_id <= 0) return "参数不全";
            StockPlanTradeModel spt = await GetOne(id);
            if (spt == null) return "读取不到MODEL";
            if (spt.sys_user_id != sys_user_id) return "权限异常";
            if (spt.buy_or_sell != -1) return "买入数据不可删除";
            ulong planId = spt.plan_id;
            StockPlanModel plan = await StockPlan.GetOne(planId, 0, false);
            if (plan == null) return "读取不到PLAN";
            if (plan.plan_status_id != 5 && plan.plan_status_id != 6) return "状态异常";

            decimal sell_price = 0;
            int amount = plan.stock_amount_already - spt.amount;
            if (amount > 0)
                sell_price = (plan.stock_amount_already * plan.sell_price - spt.amount * spt.price) / amount;
            if (amount < 0) return "数据异常";

            SysUserModel suser = await SysUser.GetOne(plan.sys_user_id, string.Empty);
            if (suser == null) return "读取不到SUSER";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if (await DBHelper.Delete(TABLE, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("stock_amount_already", "数字相减-" + spt.amount);
                    dict.Add("sell_price", sell_price.ToString());
                    if (amount == 0)
                    {
                        dict.Add("plan_status_id", "4");
                        dict.Add("profit", "0");
                        dict.Add("user_profit", "0");
                        dict.Add("user_money", "0");
                        dict.Add("sys_user_money", "0");
                    }
                    else dict.Add("plan_status_id", "5");
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", plan.id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", plan.plan_status_id.ToString());
                    if ((await DBHelper.Update(StockPlan.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    dict = new Dictionary<string, string>();
                    if (amount == 0) dict.Add("stock_plan_amount", "数字相减-1");
                    if (dict.Count > 0)
                    {
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.user_id.ToString());
                        if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    dict = new Dictionary<string, string>();
                    if (amount == 0) dict.Add("stock_plan_amount", "数字相减-1");
                    dict.Add("stock_money", "数字相减-" + (spt.amount * spt.price));
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", plan.sys_user_id.ToString());
                    if ((await DBHelper.Update(SysUser.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }
                    if ((await SysUserStockMoneyChange.Insert(plan.sys_user_id, 2, -spt.amount * spt.price,
                        suser.stock_money - spt.amount * spt.price, "卖出撤销", TABLE, id,
                        plan.stock_no, plan.stock_name, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            await StockPlan.DeleteRedis(plan.id);
            if (amount == 0) await User.DeleteRedis(plan.user_id);
            await SysUser.DeleteRedis(plan.sys_user_id);
            return string.Empty;
        }
        #endregion
    }
}
