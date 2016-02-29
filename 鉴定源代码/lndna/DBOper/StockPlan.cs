using Model;
using MySql.Data.MySqlClient;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class StockPlan
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        public const string TABLE = "stock_plan";
        #endregion
        #region 一般操作
        /// <summary>
        /// 申请点买
        /// </summary>
        /// <returns></returns>
        public static async Task<string> Insert(ulong user_id, string stock_no, string stock_name, decimal money_debt, int stock_amount,
            int stop_earn_percent, decimal money_margin, decimal buy_price)
        {
            if (user_id <= 0 || string.IsNullOrWhiteSpace(stock_no) || string.IsNullOrWhiteSpace(stock_name) || money_debt <= 0 ||
               stop_earn_percent <= 0 || money_margin <= 0)
                return "参数不全";
            if (await ZStockSettings.GetRongduan() > 0) return "熔断状态，不可点买";
            if (stock_no.Length > 6)
                stock_no = stock_no.Substring(stock_no.Length - 6);
            if (!ZStockSettings.DEBT.Contains(money_debt)) return "参数异常";
            money_debt = money_debt * 10000;
            if (!ZStockSettings.STOP_EARN.Contains(stop_earn_percent)) return "参数异常";
            if (!StockInfo.SIM_DATA)
            {
                if (!StockInfo.InBuyTime()) return "非交易时间不可点买";
            }
            if (!await StockForbidden.CanBuy(stock_no)) return "该股票为高风险股  已列入点买黑名单 无投资人接单";
            if (!await ZStockSettings.PriceIn(stock_no))
                return "该股票价格异常或涨跌幅过" + ZStockSettings.PRICE_IN + "%，无法发起点买";
            int times = Convert.ToInt32(money_debt / money_margin);
            if (!ZStockSettings.STOP_LOSS.ContainsKey(times)) return "不支持该杠杆";
            decimal stop_loss_money = Math.Round(ZStockSettings.STOP_LOSS[times] * money_margin, 2);
            if (buy_price == 0)
            {
                buy_price = await StockInfo.GetLastPrice(stock_no);
                buy_price *= Convert.ToDecimal(1.1);
            }

            DateTime start_date = DateTime.Today;
            DateTime end_date = StockInfo.GetNextOrderDate(DateTime.Today);
            decimal money_fee = ZStockSettings.GetFee(money_debt);
            decimal money = money_fee + money_margin;

            UserModel user = await User.GetOne(user_id, string.Empty, string.Empty, string.Empty, false);
            if (user == null) return "读取不到USER";
            if (user.money < money) return "账户余额不足";

            int todayAmount = await SetTodayAmount(user_id, 1);
            if (!StockInfo.SIM_DATA)
            {
                if (todayAmount > ZStockSettings.DAY_UPPER) return "超过今日点买上限：" + ZStockSettings.DAY_UPPER;
            }

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("money", "数字相减-" + money);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user_id.ToString());
                    fdict.Add("重复字段1重复字段money", ">=" + money);
                    if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("user_id", user_id.ToString());
                    dict.Add("stock_no", stock_no);
                    dict.Add("stock_name", stock_name);
                    dict.Add("money_debt", money_debt.ToString());
                    dict.Add("stock_amount", stock_amount.ToString());
                    dict.Add("start_date", start_date.ToString("yyyy-MM-dd"));
                    dict.Add("end_date", end_date.ToString("yyyy-MM-dd"));
                    dict.Add("money_fee", money_fee.ToString());
                    dict.Add("stop_earn_percent", stop_earn_percent.ToString());
                    dict.Add("money_margin", money_margin.ToString());
                    dict.Add("stop_loss_money", stop_loss_money.ToString());
                    dict.Add("buy_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("sell_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("start_amount", stock_amount.ToString());
                    dict.Add("start_price", buy_price.ToString());
                    dict.Add("start_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                    if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }
                    ulong newId = await DBHelper.GetNewId(dbConnection, trans);

                    //if ((await UserMoneyChange.Insert(user_id, 2, -money, user.money - money, "申请点买", TABLE, newId, dbConnection, trans)) <= 0)
                    //{
                    //    trans.Rollback();
                    //    return "SQL执行错误";
                    //}
                    //money由2部分组成
                    if (money_fee > 0)
                    {
                        if ((await UserMoneyChange.Insert(user_id, 2, -money_fee, user.money - money_fee, "申请点买-管理费",
                            TABLE, newId, user.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }
                    if ((await UserMoneyChange.Insert(user_id, 2, -money_margin, user.money - money, "申请点买-保证金",
                        TABLE, newId, user.cmp_id, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    await StockPlanLog.Insert(newId, "申请点买", string.Empty, -1, dbConnection, trans);

                    trans.Commit();
                }
            }

            await User.DeleteRedis(user.id);
            return string.Empty;
        }
        /// <summary>
        /// 接单或流单、强制平仓
        /// </summary>
        /// <returns></returns>
        public static async Task<string> SetSysUser(ulong id, int sys_user_id, int plan_status_id, string remark, bool force)
        {
            if (plan_status_id == 4 && sys_user_id > 0) return await PreSell(id, remark, 0, force);

            if (id <= 0 || sys_user_id <= 0) return "参数不足";
            if (!string.IsNullOrWhiteSpace(remark) && remark.Length > 100) remark = remark.Substring(0, 100);
            SysUserModel suser = await SysUser.GetOne(sys_user_id, string.Empty);
            if (suser == null) return "读取不到SUSER";
            if (suser.user_id <= 0) return "你不是投资人";
            UserModel ssuser = await User.GetOne(suser.user_id, string.Empty, string.Empty, string.Empty);
            if (ssuser == null) return "无法读取SSUSER";
            StockPlanModel plan = await GetOne(id, 0);
            if (plan == null) return "无法读取PLAN";
            if (!StockInfo.SIM_DATA && suser.user_id == plan.user_id) return "无法接自己的单";
            UserModel user = null;
            if (plan.plan_status_id != 1)
            {
                //接单后在未买入的时候可以选择流单
                if (!(plan.plan_status_id == 2 && plan.stock_amount_already == 0 && plan.sys_user_id == sys_user_id && plan_status_id == -1))
                    return "该方案处于" + DictPlanStatus.GetName(plan.plan_status_id) + "状态";
            }
            if (plan_status_id == -1)
            {
                user = await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty);
                if (user == null) return "读取不到USER";
            }
            if (plan_status_id == 2)
            {
                if (!StockInfo.SIM_DATA)
                {
                    if (!StockInfo.InBuyTime()) return "非交易时间不可接单";
                }
                if (suser.stock_money < plan.money_debt) return "投资余额不足";
                if (ssuser.money < plan.money_debt * plan.stop_earn_percent / 100)
                    return "账户余额不足，止盈冻结需要：" + (plan.money_debt * plan.stop_earn_percent / 100);
            }

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("sys_user_id", sys_user_id.ToString());
                    dict.Add("plan_status_id", plan_status_id.ToString());
                    if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
                    if (plan_status_id == -1) dict.Add("end_date", plan.start_date.ToShortDateString());
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if (plan.plan_status_id == 1) fdict.Add("重复字段1重复字段plan_status_id", "1");
                    else
                    {
                        fdict.Add("重复字段1重复字段plan_status_id", "2");
                        fdict.Add("stock_amount_already", "0");
                        fdict.Add("重复字段1重复字段sys_user_id", sys_user_id.ToString());
                    }
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    if (plan_status_id == -1)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("money", "数字相加+" + (plan.money_fee + plan.money_margin));
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.user_id.ToString());
                        if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                        //if ((await UserMoneyChange.Insert(plan.user_id, 1, plan.money_fee + plan.money_margin,
                        //    user.money + plan.money_fee + plan.money_margin, "点买流单", TABLE, id, dbConnection, trans)) <= 0)
                        //{
                        //    trans.Rollback();
                        //    return "SQL执行错误";
                        //}
                        //2部分组成
                        if (plan.money_fee > 0)
                        {
                            if ((await UserMoneyChange.Insert(plan.user_id, 1, plan.money_fee,
                                user.money + plan.money_fee, "点买流单-退还管理费", TABLE, id,
                                 user.cmp_id, dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                        }
                        if ((await UserMoneyChange.Insert(plan.user_id, 1, plan.money_margin,
                            user.money + plan.money_fee + plan.money_margin, "点买流单-退还保证金", TABLE, id,
                             user.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }

                        if (plan.plan_status_id == 2)
                        {
                            dict = new Dictionary<string, string>();
                            dict.Add("money", "数字相加+" + (plan.money_debt * plan.stop_earn_percent / 100));
                            fdict = new Dictionary<string, string>();
                            fdict.Add("id", ssuser.id.ToString());
                            if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                            //1部分组成
                            if ((await UserMoneyChange.Insert(ssuser.id, 1, plan.money_debt * plan.stop_earn_percent / 100,
                                ssuser.money + plan.money_debt * plan.stop_earn_percent / 100, "[投资人]流单解冻", TABLE, id,
                                 ssuser.cmp_id, dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                        }
                    }

                    if (plan_status_id == 2)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("money", "数字相减-" + (plan.money_debt * plan.stop_earn_percent / 100));
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", ssuser.id.ToString());
                        fdict.Add("重复字段1重复字段money", ">=" + (plan.money_debt * plan.stop_earn_percent / 100));
                        if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                        //1部分组成
                        if ((await UserMoneyChange.Insert(ssuser.id, 2, -plan.money_debt * plan.stop_earn_percent / 100,
                            ssuser.money - plan.money_debt * plan.stop_earn_percent / 100, "[投资人]接单冻结", TABLE, id,
                             ssuser.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    await StockPlanLog.Insert(id, "状态变更", DictPlanStatus.GetName(plan.plan_status_id) + "->" + DictPlanStatus.GetName(plan_status_id),
                        sys_user_id, dbConnection, trans);

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            await User.DeleteRedis(ssuser.id);
            await SysUser.DeleteRedis(plan.sys_user_id);
            if (plan_status_id == -1)
            {
                await User.DeleteRedis(plan.user_id);
                await SetTodayAmount(plan.user_id, -1);
            }
            return string.Empty;
        }
        /// <summary>
        /// 超时流单
        /// </summary>
        public static string SetSysUserSync(ulong id, int sys_user_id, int plan_status_id, string remark)
        {
            if (id <= 0 || sys_user_id <= 0) return "参数不足";
            if (!string.IsNullOrWhiteSpace(remark) && remark.Length > 100) remark = remark.Substring(0, 100);
            //SysUserModel suser = SysUser.GetOneSync(sys_user_id, string.Empty);
            //if (suser == null) return "读取不到SUSER";
            //if (suser.is_investor == 0)
            //{
            //    if (!(sys_user_id == 1 && plan_status_id == -1)) return "你不是投资人";
            //}
            StockPlanModel plan = GetOneSync(id);
            if (plan == null) return "无法读取PLAN";
            UserModel user = null;
            if (plan.plan_status_id != 1)
            {
                //接单后在未买入的时候可以选择流单
                if (!(plan.plan_status_id == 2 && plan.stock_amount_already == 0 && plan.sys_user_id == sys_user_id && plan_status_id == -1))
                    return "状态异常";
            }
            if (plan_status_id == -1)
            {
                user = User.GetOneSync(plan.user_id, string.Empty, string.Empty, string.Empty);
                if (user == null) return "读取不到USER";
            }

            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("sys_user_id", sys_user_id.ToString());
                    dict.Add("plan_status_id", plan_status_id.ToString());
                    if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
                    if (plan_status_id == -1) dict.Add("end_date", plan.start_date.ToShortDateString());
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if (plan.plan_status_id == 1) fdict.Add("重复字段1重复字段plan_status_id", "1");
                    else
                    {
                        fdict.Add("重复字段1重复字段plan_status_id", "2");
                        fdict.Add("stock_amount_already", "0");
                        fdict.Add("重复字段1重复字段sys_user_id", sys_user_id.ToString());
                    }
                    if ((DBHelper.UpdateSync(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    if (plan_status_id == -1)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("money", "数字相加+" + (plan.money_fee + plan.money_margin));
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.user_id.ToString());
                        if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }

                        //if ((UserMoneyChange.InsertSync(plan.user_id, 1, plan.money_fee + plan.money_margin,
                        //    user.money + plan.money_fee + plan.money_margin, "点买流单", TABLE, id, dbConnection, trans)) <= 0)
                        //{
                        //    trans.Rollback();
                        //    return "SQL执行错误";
                        //}
                        //2部分组成
                        if (plan.money_fee > 0)
                        {
                            if ((UserMoneyChange.InsertSync(plan.user_id, 1, plan.money_fee,
                                user.money + plan.money_fee, "点买流单-退还管理费", TABLE, id,
                                 user.cmp_id, dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                        }
                        if ((UserMoneyChange.InsertSync(plan.user_id, 1, plan.money_margin,
                            user.money + plan.money_fee + plan.money_margin, "点买流单-退还保证金", TABLE, id,
                             user.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    StockPlanLog.InsertSync(id, "状态变更", DictPlanStatus.GetName(plan.plan_status_id) + "->" + DictPlanStatus.GetName(plan_status_id),
                       sys_user_id, dbConnection, trans);

                    trans.Commit();
                }
            }

            DeleteRedisSync(id);
            if (plan_status_id == -1)
            {
                User.DeleteRedisSync(plan.user_id);
                SetTodayAmountSync(plan.user_id, -1);
            }
            return string.Empty;
        }
        /// <summary>
        /// 买入，只会买入一次
        /// </summary>
        /// <returns></returns>
        public static async Task<string> Buy(ulong id, DateTime trade_time, decimal price, int amount,
            string trade_no, int sys_user_id, string remark)
        {
            if (id <= 0 || trade_time == null || price <= 0 || amount <= 0) return "参数不全";
            if (!string.IsNullOrWhiteSpace(trade_no) && trade_no.Length > 50) trade_no = trade_no.Substring(0, 50);
            if (!string.IsNullOrWhiteSpace(remark) && remark.Length > 100) remark = remark.Substring(0, 100);
            StockPlanModel plan = await GetOne(id, 0, true);
            if (plan == null) return "无法读取PLAN";
            if (sys_user_id > 0 && plan.sys_user_id != sys_user_id) return "权限异常";
            if (plan.plan_status_id != 2)
                return "该方案处于" + DictPlanStatus.GetName(plan.plan_status_id) + "状态";
            decimal stop_earn_point = plan.stop_earn_percent * plan.money_debt / amount / 100 + price;
            decimal stop_loss_point = price - plan.stop_loss_money / amount;
            //计算邀请佣金
            UserModel user = await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return "无法读取USER";
            decimal profit_from_ref = 0;
            if (user.ref_id > 0)
            {
                user = await User.GetOne(user.ref_id, string.Empty, string.Empty, string.Empty);
                if (user != null)
                {
                    if (user.delete_flag == 1) user = null;
                    else
                    {
                        profit_from_ref = user.profit_from_ref;
                        if (profit_from_ref <= 0) profit_from_ref = ZStockSettings.PROFIT_FROM_REF;
                        profit_from_ref = plan.money_debt / 10000 * profit_from_ref;
                        profit_from_ref *= 2;
                    }
                }
            }
            else user = null;
            SysUserModel suser = await SysUser.GetOne(plan.sys_user_id, string.Empty);
            if (suser == null) return "读取不到SUSER";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("stock_amount", amount.ToString());
                    //dict.Add("stock_amount_already", amount.ToString());
                    dict.Add("stop_earn_point", stop_earn_point.ToString());
                    dict.Add("stop_loss_point", stop_loss_point.ToString());
                    dict.Add("plan_status_id", "3");
                    if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
                    dict.Add("buy_price", price.ToString());
                    dict.Add("buy_time", trade_time.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (user != null && profit_from_ref > 0) dict.Add("defer_ref_fee", "数字相加+" + profit_from_ref);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", "2");
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    if (await StockPlanTrade.Insert(id, 1, trade_time, price, amount, trade_no, sys_user_id, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("stock_money", "数字相减-" + (amount * price));
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", plan.sys_user_id.ToString());
                    if ((await DBHelper.Update(SysUser.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }
                    if ((await SysUserStockMoneyChange.Insert(plan.sys_user_id, 2, -amount * price,
                        suser.stock_money - amount * price, "买入股票", TABLE, id,
                        plan.stock_no, plan.stock_name, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    if (user != null && profit_from_ref > 0)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("money", "数字相加+" + profit_from_ref);
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", user.id.ToString());
                        if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                        //1部分组成
                        if ((await UserMoneyChange.Insert(user.id, 1, profit_from_ref, user.money + profit_from_ref, "邀请收益",
                            TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            await SysUser.DeleteRedis(suser.id);
            if (user != null && profit_from_ref > 0) await User.DeleteRedis(user.id);

            await InsTop(plan);
            return string.Empty;
        }
        /// <summary>
        /// 客户申请平仓
        /// </summary>
        /// <returns></returns>
        public static async Task<string> PreSell(ulong id, string remark, ulong user_id, bool force)
        {
            if (id <= 0) return "参数不全";
            if (!StockInfo.SIM_DATA && DateTime.Now.Hour * 60 + DateTime.Now.Minute >= 955) return "收盘前不可再点卖";
            if (await ZStockSettings.GetRongduan() > 0) return "熔断状态，不可点卖";
            if (!string.IsNullOrWhiteSpace(remark) && remark.Length > 100) remark = remark.Substring(0, 100);
            StockPlanModel plan = await GetOne(id, 0);
            if (plan == null) return "无法读取PLAN";
            if (plan.plan_status_id != 3)
                return "该方案处于" + DictPlanStatus.GetName(plan.plan_status_id) + "状态";
            if (user_id > 0 && plan.user_id != user_id) return "权限异常";
            if (!force)
            {
                if (plan.end_date != DateTime.Today) return "今日不可执行该指令";
                if (!StockInfo.SIM_DATA)
                {
                    //判断是否停牌或跌停
                    if (StockInfo.IsLimitDown(plan.stock_no)) return "停牌或跌停股票不可平仓";
                }
            }
            //按跌停价卖出
            decimal sell_price = await StockInfo.GetLastPrice(plan.stock_no);
            sell_price *= Convert.ToDecimal(0.9);

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    //dict.Add("stock_amount", plan.stock_amount_already.ToString());
                    //dict.Add("stock_amount_already", "0");
                    dict.Add("plan_status_id", "4");
                    dict.Add("end_price", sell_price.ToString());
                    dict.Add("sell_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
                    else if (force) dict.Add("remark", "管理员强制平仓");
                    else dict.Add("remark", "客户申请平仓");
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", "3");
                    if (!force) fdict.Add("重复字段1重复字段end_date", "<=" + DateTime.Today.ToShortDateString());
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    if (DateTime.Today > plan.start_date)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("stock_plan_debt", "数字相减-" + plan.money_debt);
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.user_id.ToString());
                        if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    string type = user_id > 0 ? "申请点卖" : "强制平仓";
                    await StockPlanLog.Insert(id, type, string.IsNullOrWhiteSpace(remark) ? type : remark, -1, dbConnection, trans);

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            await User.DeleteRedis(plan.user_id);
            return string.Empty;
        }
        /// <summary>
        /// 系统自动平仓
        /// </summary>
        /// <returns></returns>
        public static string PreSellSync(ulong id, string remark, ulong user_id, bool down)
        {
            if (id <= 0) return "参数不全";
            if (!string.IsNullOrWhiteSpace(remark) && remark.Length > 100) remark = remark.Substring(0, 100);
            StockPlanModel plan = GetOneSync(id);
            if (plan == null) return "无法读取PLAN";
            if (plan.plan_status_id != 3)
                return "该方案处于" + DictPlanStatus.GetName(plan.plan_status_id) + "状态";
            if (user_id > 0 && plan.user_id != user_id) return "权限异常";
            if (!StockInfo.SIM_DATA)
            {
                if (plan.end_date > DateTime.Today) return "今日不可执行点卖指令";
            }
            if (down && StockInfo.IsLimitDown(plan.stock_no)) return "停牌或跌停股票不可平仓";
            //按跌停价卖出
            decimal sell_price = StockInfo.GetLastPriceSync(plan.stock_no);
            sell_price *= Convert.ToDecimal(0.9);

            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    //dict.Add("stock_amount", plan.stock_amount_already.ToString());
                    //dict.Add("stock_amount_already", "0");
                    dict.Add("plan_status_id", "4");
                    dict.Add("end_price", sell_price.ToString());
                    dict.Add("sell_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", "3");
                    fdict.Add("重复字段1重复字段end_date", "<=" + DateTime.Today.ToShortDateString());
                    if ((DBHelper.UpdateSync(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    if (DateTime.Today > plan.start_date)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("stock_plan_debt", "数字相减-" + plan.money_debt);
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.user_id.ToString());
                        if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    StockPlanLog.InsertSync(id, "申请点卖", (string.IsNullOrWhiteSpace(remark) ? string.Empty : remark), -1, dbConnection, trans);

                    trans.Commit();
                }
            }

            DeleteRedisSync(id);
            User.DeleteRedisSync(plan.user_id);
            return string.Empty;
        }
        public static async Task<string> CancelPreSell(ulong id, string remark, ulong user_id, int sys_user_id, bool force)
        {
            if (id <= 0) return "参数不全";
            if (!string.IsNullOrWhiteSpace(remark) && remark.Length > 100) remark = remark.Substring(0, 100);
            StockPlanModel plan = await GetOne(id, 0);
            if (plan == null) return "无法读取PLAN";
            if (plan.plan_status_id != 4 || plan.stock_amount_already > 0)
                return "该方案处于" + DictPlanStatus.GetName(plan.plan_status_id) + "状态";
            if (user_id > 0 && plan.user_id != user_id) return "权限异常";
            if (!StockInfo.SIM_DATA && !force)
            {
                if (plan.end_date != DateTime.Today) return "今日不可执行该指令";
            }

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    //dict.Add("stock_amount_already", plan.stock_amount.ToString());
                    dict.Add("plan_status_id", "3");
                    if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
                    else dict.Add("remark", "撤销平仓");
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", "4");
                    fdict.Add("重复字段1重复字段stock_amount_already", "0");
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    if (DateTime.Today > plan.start_date)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("stock_plan_debt", "数字相加+" + plan.money_debt);
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.user_id.ToString());
                        if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    await StockPlanLog.Insert(id, "撤销平仓", (string.IsNullOrWhiteSpace(remark) ? "撤销平仓" : remark),
                        sys_user_id, dbConnection, trans);

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            await User.DeleteRedis(plan.user_id);
            return string.Empty;
        }
        /// <summary>
        /// 卖出，可能多次
        /// </summary>
        /// <returns></returns>
        public static async Task<string> Sell(ulong id, DateTime trade_time, decimal price, int amount,
            string trade_no, int sys_user_id, string remark)
        {
            if (id <= 0 || trade_time == null || price <= 0 || amount <= 0) return "参数不全";
            if (!string.IsNullOrWhiteSpace(trade_no) && trade_no.Length > 50) trade_no = trade_no.Substring(0, 50);
            if (!string.IsNullOrWhiteSpace(remark) && remark.Length > 100) remark = remark.Substring(0, 100);
            StockPlanModel plan = await GetOne(id, 0);
            if (plan == null) return "无法读取PLAN";
            if (sys_user_id > 0 && plan.sys_user_id != sys_user_id) return "权限异常";
            if (plan.plan_status_id != 4 && plan.plan_status_id != 5)
                return "该方案处于" + DictPlanStatus.GetName(plan.plan_status_id) + "状态";
            decimal sell_price = Math.Round((price * amount + plan.sell_price * plan.stock_amount_already) / (amount + plan.stock_amount_already), 4);
            decimal profit = 0;
            decimal user_profit = 0;
            decimal user_money = 0;
            decimal sys_user_money = 0;
            UserModel user = null;
            //UserModel ruser = null;
            //decimal profit_from_ref = 0;
            if (amount + plan.stock_amount_already >= plan.stock_amount)
            {
                profit = Math.Round((sell_price - plan.buy_price) * (amount + plan.stock_amount_already), 2);
                if (profit > 0)
                {
                    user_profit = Math.Round(profit * ZStockSettings.USER_PROFIT / 100, 2);
                    user_money = plan.money_margin + user_profit;
                }
                else
                {
                    user_money = plan.money_margin + profit;
                }
                if (user_money < 0) user_money = 0;
                sys_user_money = plan.money_margin + plan.money_debt * plan.stop_earn_percent / 100 - user_money;

                user = await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty);
                if (user == null) return "读取不到USER";
                //卖出成功的，计算邀请佣金
                //if (user.ref_id > 0)
                //{
                //    ruser = await User.GetOne(user.ref_id, string.Empty, string.Empty, string.Empty);
                //    if (ruser != null)
                //    {
                //        if (ruser.delete_flag == 1) ruser = null;
                //        else
                //        {
                //            profit_from_ref = user.profit_from_ref;
                //            if (profit_from_ref <= 0) profit_from_ref = ZStockSettings.PROFIT_FROM_REF;
                //            profit_from_ref = plan.money_debt / 10000 * profit_from_ref;
                //        }
                //    }
                //}
            }
            SysUserModel suser = await SysUser.GetOne(plan.sys_user_id, string.Empty);
            if (suser == null) return "读取不到SUSER";
            UserModel ssuser = await User.GetOne(suser.user_id, string.Empty, string.Empty, string.Empty);
            if (ssuser == null) return "无法读取SSUSER";

            int buy_pj = 0;
            int sell_pj = 0;
            int buy_onem = 0;
            int sell_onem = 0;
            if (plan.stock_amount_already == 0)
            {
                TimeSpan ts = plan.buy_time - plan.start_time;
                int bsed = Convert.ToInt32(ts.TotalSeconds);
                if (bsed >= 60) buy_onem = 1;
                ts = DateTime.Now - plan.sell_time;
                int ssed = Convert.ToInt32(ts.TotalSeconds);
                if (ssed >= 60) sell_onem = 1;
                if (suser.buy_pj == 0) buy_pj = bsed;
                else
                {
                    buy_pj = (buy_pj + suser.buy_pj * suser.stock_plan_amount) / (suser.stock_plan_amount + 1);
                }
                if (suser.sell_pj == 0) sell_pj = ssed;
                else
                {
                    sell_pj = (sell_pj + suser.sell_pj * suser.stock_plan_amount) / (suser.stock_plan_amount + 1);
                }
            }

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("stock_amount_already", "数字相加+" + amount);
                    if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
                    dict.Add("sell_price", sell_price.ToString());
                    dict.Add("sell_time", trade_time.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (amount + plan.stock_amount_already >= plan.stock_amount)
                    {
                        if (ZStockSettings.AUTO_TRADE == 0) dict.Add("plan_status_id", "6");
                        else
                        {
                            dict.Add("plan_status_id", "6");
                            //dict.Add("plan_status_id", "7");
                        }
                        dict.Add("profit", profit.ToString());
                        dict.Add("user_profit", user_profit.ToString());
                        dict.Add("user_money", user_money.ToString());
                        dict.Add("sys_user_money", sys_user_money.ToString());
                    }
                    else dict.Add("plan_status_id", "5");
                    //if (user != null && profit_from_ref > 0) dict.Add("defer_ref_fee", "数字相加+" + profit_from_ref);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", plan.plan_status_id.ToString());
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    if (await StockPlanTrade.Insert(id, -1, trade_time, price, amount, trade_no, sys_user_id, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    dict = new Dictionary<string, string>();
                    if (amount + plan.stock_amount_already >= plan.stock_amount) dict.Add("stock_plan_amount", "数字相加+1");
                    if (ZStockSettings.AUTO_TRADE == 1)
                    {
                        //if (user_money > 0) dict.Add("money", "数字相加+" + user_money);
                    }
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

                    if (ZStockSettings.AUTO_TRADE == 1)
                    {
                        //if (user_money > 0)
                        //{
                        //    if ((await UserMoneyChange.Insert(plan.user_id, 1, user_money, user.money + user_money,
                        //        "点买结算", TABLE, id, dbConnection, trans)) <= 0)
                        //    {
                        //        trans.Rollback();
                        //        return "SQL执行错误";
                        //    }
                        //}
                    }

                    dict = new Dictionary<string, string>();
                    if (amount + plan.stock_amount_already >= plan.stock_amount) dict.Add("stock_plan_amount", "数字相加+1");
                    dict.Add("stock_money", "数字相加+" + (amount * price));
                    if (buy_pj > 0) dict.Add("buy_pj", buy_pj.ToString());
                    if (sell_pj > 0) dict.Add("sell_pj", sell_pj.ToString());
                    if (buy_onem > 0) dict.Add("buy_onem", "数字相加+1");
                    if (sell_onem > 0) dict.Add("sell_onem", "数字相加+1");
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", plan.sys_user_id.ToString());
                    if ((await DBHelper.Update(SysUser.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }
                    if ((await SysUserStockMoneyChange.Insert(plan.sys_user_id, 1, amount * price,
                        suser.stock_money + amount * price, "卖出股票", TABLE, id,
                        plan.stock_no, plan.stock_name, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }
                    if (ZStockSettings.AUTO_TRADE == 1)
                    {
                        //if (sys_user_money != 0)
                        //{
                        //    dict = new Dictionary<string, string>();
                        //    if (sys_user_money > 0) dict.Add("money", "数字相加+" + sys_user_money);
                        //    else if (sys_user_money < 0) dict.Add("money", "数字相减-" + (-sys_user_money));
                        //    fdict = new Dictionary<string, string>();
                        //    fdict.Add("id", ssuser.id.ToString());
                        //    if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        //    {
                        //        trans.Rollback();
                        //        return "SQL执行错误";
                        //    }

                        //    if ((await UserMoneyChange.Insert(ssuser.id, sys_user_money > 0 ? 1 : 2, sys_user_money,
                        //        ssuser.money + sys_user_money, "[投资人]结算余额", TABLE, id, dbConnection, trans)) <= 0)
                        //    {
                        //        trans.Rollback();
                        //        return "SQL执行错误";
                        //    }
                        //}
                    }

                    //if (ruser != null && profit_from_ref > 0)
                    //{
                    //    dict = new Dictionary<string, string>();
                    //    dict.Add("money", "数字相加+" + profit_from_ref);
                    //    dict.Add("profit_from_ref", "数字相加+" + profit_from_ref);
                    //    fdict = new Dictionary<string, string>();
                    //    fdict.Add("id", ruser.id.ToString());
                    //    if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    //    {
                    //        trans.Rollback();
                    //        return "SQL执行错误";
                    //    }
                    //    //1部分组成
                    //    if ((await UserMoneyChange.Insert(ruser.id, 1, profit_from_ref, ruser.money + profit_from_ref, "邀请收益", TABLE, id, dbConnection, trans)) <= 0)
                    //    {
                    //        trans.Rollback();
                    //        return "SQL执行错误";
                    //    }
                    //}

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            await User.DeleteRedis(plan.user_id);
            if (ZStockSettings.AUTO_TRADE == 1)
            {
                //if (sys_user_money != 0) await User.DeleteRedis(ssuser.id);
            }
            //if (ruser != null && profit_from_ref > 0) await User.DeleteRedis(ruser.id);
            await SysUser.DeleteRedis(plan.sys_user_id);
            return string.Empty;
        }
        /// <summary>
        /// 人工结算
        /// </summary>
        /// <returns></returns>
        public static async Task<string> Calc(ulong id, int sys_user_id)
        {
            if (id <= 0) return "参数不全";
            StockPlanModel plan = await GetOne(id, 0);
            if (plan == null) return "无法读取PLAN";
            if (plan.plan_status_id == 7)
            {
                await ReCalc(id, sys_user_id);
                return string.Empty;
            }
            else if (plan.plan_status_id == 6)
            {
                UserModel user = await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty);
                if (user == null) return "读取不到USER";
                SysUserModel suser = await SysUser.GetOne(plan.sys_user_id, string.Empty);
                if (suser == null) return "读取不到SUSER";
                UserModel ssuser = await User.GetOne(suser.user_id, string.Empty, string.Empty, string.Empty);
                if (ssuser == null) return "无法读取SSUSER";

                //判断是否欠费超过保证金
                bool npay = false;
                if (plan.money_npay > plan.money_margin && plan.user_profit > 0)
                {
                    plan.sys_user_money += plan.user_profit;
                    plan.user_money -= plan.user_profit;
                    plan.user_profit = 0;
                    npay = true;
                }
                //decimal npaym = 0;
                //if (plan.money_npay > 0)
                //{
                //    if (plan.user_money > plan.money_npay) npaym = plan.money_npay;
                //    else npaym = plan.user_money;
                //}

                using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
                {
                    using (MySqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        IDictionary<string, string> dict = new Dictionary<string, string>();
                        if (npay)
                        {
                            dict.Add("user_profit", "0");
                            dict.Add("user_money", "数字相减-" + plan.user_profit);
                            dict.Add("sys_user_money", "数字相加+" + plan.user_profit);
                        }
                        dict.Add("plan_status_id", "7");
                        //if (npaym > 0) dict.Add("money_npay", "数字相减-" + npaym);
                        IDictionary<string, string> fdict = new Dictionary<string, string>();
                        fdict.Add("id", id.ToString());
                        fdict.Add("重复字段1重复字段plan_status_id", plan.plan_status_id.ToString());
                        if (npay)
                        {
                            fdict.Add("重复字段1重复字段money_npay", ">" + plan.money_margin);
                            fdict.Add("重复字段1重复字段user_profit", ">0");
                        }
                        if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "异常，请重试";
                        }

                        if (plan.user_money > 0)
                        {
                            dict = new Dictionary<string, string>();
                            dict.Add("money", "数字相加+" + plan.user_money);
                            if (plan.user_profit > 0) dict.Add("stock_plan_earn", "数字相加+" + plan.user_profit);
                            else if (plan.user_profit < 0) dict.Add("stock_plan_loss", "数字相加+" + (-plan.user_profit));
                            fdict = new Dictionary<string, string>();
                            fdict.Add("id", plan.user_id.ToString());
                            if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                            //if ((await UserMoneyChange.Insert(plan.user_id, 1, plan.user_money, user.money + plan.user_money,
                            //    "点买结算", TABLE, id, dbConnection, trans)) <= 0)
                            //{
                            //    trans.Rollback();
                            //    return "SQL执行错误";
                            //}
                            //拆分，2部分组成
                            if (plan.user_profit > 0)
                            {
                                if ((await UserMoneyChange.Insert(plan.user_id, 1, plan.user_profit, user.money + plan.user_profit,
                                  "点买结算-盈利", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                                {
                                    trans.Rollback();
                                    return "SQL执行错误";
                                }
                                if ((await UserMoneyChange.Insert(plan.user_id, 1, plan.money_margin, user.money + plan.user_money,
                                  "点买结算-退还保证金", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                                {
                                    trans.Rollback();
                                    return "SQL执行错误";
                                }
                            }
                            else
                            {
                                if ((await UserMoneyChange.Insert(plan.user_id, 1, plan.user_money, user.money + plan.user_money,
                                    "点买结算-退还保证金", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                                {
                                    trans.Rollback();
                                    return "SQL执行错误";
                                }
                            }
                        }

                        //if (npaym > 0)
                        //{
                        //dict = new Dictionary<string, string>();
                        //dict.Add("money_npay", "数字相减-" + npaym);
                        //fdict = new Dictionary<string, string>();
                        //fdict.Add("id", plan.user_id.ToString());
                        //fdict.Add("重复字段1重复字段money_npay", ">=" + npaym);
                        //if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        //{
                        //    trans.Rollback();
                        //    return "SQL执行错误";
                        //}

                        //if ((await UserMoneyChange.Insert(plan.user_id, 2, -npaym, user.money + plan.user_money - npaym,
                        //       "点买结算-补交递延费", TABLE, id, dbConnection, trans)) <= 0)
                        //{
                        //    trans.Rollback();
                        //    return "SQL执行错误";
                        //}
                        //}

                        if (plan.sys_user_money != 0)
                        {
                            dict = new Dictionary<string, string>();
                            if (plan.sys_user_money > 0) dict.Add("money", "数字相加+" + plan.sys_user_money);
                            else if (plan.sys_user_money < 0) dict.Add("money", "数字相减-" + (-plan.sys_user_money));
                            fdict = new Dictionary<string, string>();
                            fdict.Add("id", ssuser.id.ToString());
                            if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }

                            //if ((await UserMoneyChange.Insert(ssuser.id, plan.sys_user_money > 0 ? 1 : 2, plan.sys_user_money,
                            //    ssuser.money + plan.sys_user_money, "[投资人]结算余额", TABLE, id, dbConnection, trans)) <= 0)
                            //{
                            //    trans.Rollback();
                            //    return "SQL执行错误";
                            //}
                            //2部分组成
                            if ((await UserMoneyChange.Insert(ssuser.id, 1, plan.money_debt * plan.stop_earn_percent / 100,
                                ssuser.money + plan.money_debt * plan.stop_earn_percent / 100, "[投资人]结算余额-退还保证金",
                                TABLE, id, ssuser.cmp_id, dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return "SQL执行错误";
                            }
                            if (plan.money_margin - plan.user_money != 0)
                            {
                                if ((await UserMoneyChange.Insert(ssuser.id, (plan.money_margin - plan.user_money) > 0 ? 1 : 2, (plan.money_margin - plan.user_money),
                                    ssuser.money + plan.sys_user_money, "[投资人]结算余额-盈亏", TABLE, id,
                                     ssuser.cmp_id, dbConnection, trans)) <= 0)
                                {
                                    trans.Rollback();
                                    return "SQL执行错误";
                                }
                            }
                        }

                        trans.Commit();
                    }
                }

                await DeleteRedis(id);
                await User.DeleteRedis(plan.user_id);
                if (plan.sys_user_money != 0) await User.DeleteRedis(ssuser.id);
                await SysUser.DeleteRedis(plan.sys_user_id);
                await UserMoneyChange.StockPlanFinish(id);
                if (user.money_npay > plan.money_npay)
                {
                    await UserMoneyChange.MoneyNPay(await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty));
                }
                return string.Empty;
            }
            return "状态异常";
        }
        /// <summary>
        /// 重新结算
        /// </summary>
        /// <returns></returns>
        public static async Task ReCalc(ulong id, int sys_user_id)
        {
            if (id <= 0) return;
            StockPlanModel plan = await GetOne(id, 0);
            if (plan == null) return;
            if (plan.plan_status_id < 5) return;
            if (plan.plan_status_id == 7 && plan.profit > 0 && plan.user_profit == 0)
                return;//因欠费超过保证金，盈利不会返还的，无法自动计算
            IEnumerable<StockPlanTradeModel> trades = await StockPlanTrade.GetList(id.ToString());
            decimal price = 0;
            decimal amount = 0;
            DateTime trade_time = DateTime.MinValue;
            foreach (StockPlanTradeModel trade in trades)
            {
                if (trade.buy_or_sell == 1) continue;

                price += trade.price * trade.amount;
                amount += trade.amount;
                if (trade.trade_time > trade_time) trade_time = trade.trade_time;
            }

            decimal sell_price = Math.Round(price / amount, 4);
            decimal profit = 0;
            decimal user_profit = 0;
            decimal user_money = 0;
            decimal sys_user_money = 0;
            decimal user_money_delta = 0;
            decimal sys_user_money_delta = 0;
            decimal profit_delta = 0;
            UserModel user = null;
            if (amount >= plan.stock_amount)
            {
                profit = Math.Round((sell_price - plan.buy_price) * amount, 2);
                if (profit > 0)
                {
                    user_profit = Math.Round(profit * ZStockSettings.USER_PROFIT / 100, 2);
                    user_money = plan.money_margin + user_profit;
                }
                else
                {
                    user_money = plan.money_margin + profit;
                }
                if (user_money < 0) user_money = 0;
                sys_user_money = plan.money_margin + plan.money_debt * plan.stop_earn_percent / 100 - user_money;

                user = await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty);
                if (user == null) return;
            }
            user_money_delta = user_money - plan.user_money;
            if (user_money != 0 && user == null)
            {
                user = await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty);
                if (user == null) return;
            }
            SysUserModel suser = await SysUser.GetOne(plan.sys_user_id, string.Empty);
            if (suser == null) return;
            UserModel ssuser = await User.GetOne(suser.user_id, string.Empty, string.Empty, string.Empty);
            if (ssuser == null) return;
            sys_user_money_delta = sys_user_money - plan.sys_user_money;
            profit_delta = profit - plan.profit;

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("stock_amount_already", amount.ToString());
                    dict.Add("sell_price", sell_price.ToString());
                    dict.Add("sell_time", trade_time.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (amount >= plan.stock_amount)
                    {
                        if (plan.plan_status_id != 7) dict.Add("plan_status_id", "6");
                        dict.Add("profit", profit.ToString());
                        dict.Add("user_profit", user_profit.ToString());
                        dict.Add("user_money", user_money.ToString());
                        dict.Add("sys_user_money", sys_user_money.ToString());
                    }
                    else dict.Add("plan_status_id", "5");
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", plan.plan_status_id.ToString());
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return;
                    }

                    dict = new Dictionary<string, string>();
                    if (user_money_delta > 0)
                    {
                        if (plan.plan_status_id == 7) dict.Add("money", "数字相加+" + user_money_delta);
                    }
                    else if (user_money_delta < 0)
                    {
                        if (plan.plan_status_id == 7) dict.Add("money", "数字相减-" + (-user_money_delta));
                    }
                    if (profit_delta > 0) dict.Add("stock_plan_earn", "数字相加+" + profit_delta);
                    else if (profit_delta < 0) dict.Add("stock_plan_loss", "数字相加+" + (-profit_delta));
                    if (dict.Count > 0)
                    {
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.user_id.ToString());
                        if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return;
                        }
                    }
                    if (user_money_delta != 0 && plan.plan_status_id == 7)
                    {
                        //1部分组成
                        if ((await UserMoneyChange.Insert(plan.user_id, user_money_delta > 0 ? 1 : 2, user_money_delta, user.money + user_money_delta,
                            "重算收益", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return;
                        }
                    }

                    if (sys_user_money_delta != 0)
                    {
                        if (plan.plan_status_id == 7)
                        {
                            dict = new Dictionary<string, string>();
                            if (sys_user_money_delta > 0) dict.Add("money", "数字相加+" + sys_user_money_delta);
                            else if (sys_user_money_delta < 0) dict.Add("money", "数字相减-" + (-sys_user_money_delta));
                            fdict = new Dictionary<string, string>();
                            fdict.Add("id", ssuser.id.ToString());
                            if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return;
                            }
                            //1部分组成
                            if ((await UserMoneyChange.Insert(ssuser.id, sys_user_money_delta > 0 ? 1 : 2, sys_user_money_delta,
                                ssuser.money + sys_user_money_delta, "[投资人]重算收益", TABLE, id,
                                 ssuser.cmp_id, dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return;
                            }
                        }

                        dict = new Dictionary<string, string>();
                        if (dict.Count > 0)
                        {
                            fdict = new Dictionary<string, string>();
                            fdict.Add("id", suser.id.ToString());
                            if ((await DBHelper.Update(SysUser.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                            {
                                trans.Rollback();
                                return;
                            }
                        }
                    }

                    if (plan.plan_status_id >= 6)
                    {
                        if (plan.profit != profit || plan.user_profit != user_profit)
                            await StockPlanLog.Insert(id, "重算收益", "总盈亏：" + plan.profit + "->" + profit + "；客户盈亏：" + plan.user_profit + "->" + user_profit, sys_user_id, dbConnection, trans);
                    }

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            await User.DeleteRedis(plan.user_id);
            if (sys_user_money_delta != 0)
            {
                await User.DeleteRedis(ssuser.id);
                await SysUser.DeleteRedis(suser.id);
            }
            if (user.money_npay > 0)
            {
                await UserMoneyChange.MoneyNPay(await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty));
            }
        }
        public static async Task<string> UpdatePoUser(ulong id, string po_user)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(po_user)) return "参数不足";
            StockPlanModel plan = await GetOne(id, 0, false);
            if (plan == null) return "读取不到MODEL";
            if (!string.IsNullOrWhiteSpace(plan.po_user))
            {
                if (plan.po_user.Equals(po_user)) return string.Empty;
                return "已被" + plan.po_user + "挂单中";
            }

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("po_user", po_user);
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0)
            {
                return "异常，请重试";
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<IEnumerable<StockPlanModel>> GetList(string id, string user_id, string plan_status_id, string profit,
            string start_dates, string start_datee, string end_dates, string end_datee,
            string stock_no, string stock_name, string money_debts, string money_debte, string sys_user_id, string order,
            string page_size, string page_index)
        {
            if (!string.IsNullOrWhiteSpace(sys_user_id))
            {
                SysUserModel suser = await SysUser.GetOne(Convert.ToInt32(sys_user_id), string.Empty);
                if (suser == null) return new List<StockPlanModel>();
                if (suser.user_id <= 0) sys_user_id = string.Empty;
                else if (!string.IsNullOrWhiteSpace(plan_status_id) && plan_status_id.Equals("1"))
                    sys_user_id = string.Empty;
            }
            if (StockInfo.SIM_DATA)
            {
                if (!string.IsNullOrWhiteSpace(end_datee))
                    end_datee = Convert.ToDateTime(end_datee).AddDays(7).ToShortDateString();
            }

            IEnumerable<StockPlanModel> list = null;
            if (!string.IsNullOrWhiteSpace(plan_status_id) && plan_status_id.Contains(","))
            {
                string filter = string.Empty;
                foreach (string pid in plan_status_id.Split(','))
                {
                    filter += " or plan_status_id=" + pid;
                }
                filter = " and (" + filter.Substring(4) + ")";
                if (!string.IsNullOrWhiteSpace(id)) filter += " and id=" + id;
                if (!string.IsNullOrWhiteSpace(user_id)) filter += " and user_id=" + user_id;
                if (!string.IsNullOrWhiteSpace(profit)) filter += " and profit " + profit;
                if (!string.IsNullOrWhiteSpace(start_dates)) filter += " and start_date>='" + start_dates + "'";
                if (!string.IsNullOrWhiteSpace(start_datee)) filter += " and start_date<='" + start_datee + "'";
                if (!string.IsNullOrWhiteSpace(end_dates)) filter += " and end_date>='" + end_dates + "'";
                if (!string.IsNullOrWhiteSpace(end_datee)) filter += " and end_date<='" + end_datee + "'";
                if (!string.IsNullOrWhiteSpace(stock_no)) filter += " and stock_no like '" + stock_no + "%'";
                if (!string.IsNullOrWhiteSpace(stock_name)) filter += " and stock_name like '" + stock_name + "%'";
                if (!string.IsNullOrWhiteSpace(money_debts)) filter += " and money_debt>='" + money_debts + "'";
                if (!string.IsNullOrWhiteSpace(money_debte)) filter += " and money_debt<='" + money_debte + "'";
                if (!string.IsNullOrWhiteSpace(sys_user_id)) filter += " and sys_user_id=" + sys_user_id;
                if (!string.IsNullOrWhiteSpace(User.CMP_ID)) filter += " and cmp_id=" + User.CMP_ID;
                list = await DBHelper.GetFreeList<StockPlanModel, long>("select id from stock_plan where " + filter.Substring(5) +
                   (string.IsNullOrWhiteSpace(order) ? string.Empty : (" order by " + order)), TABLE, "*",
                    order, Convert.ToInt32(page_size), Convert.ToInt32(page_index));
            }
            else
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(id)) fdict.Add("id", id);
                if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
                if (!string.IsNullOrWhiteSpace(plan_status_id)) fdict.Add("plan_status_id", plan_status_id);
                if (!string.IsNullOrWhiteSpace(profit)) fdict.Add("profit", profit);
                if (!string.IsNullOrWhiteSpace(start_dates)) fdict.Add("start_date", ">=" + start_dates);
                if (!string.IsNullOrWhiteSpace(start_datee)) fdict.Add("重复字段1重复字段start_date", "<=" + start_datee);
                if (!string.IsNullOrWhiteSpace(end_dates)) fdict.Add("end_date", ">=" + end_dates);
                if (!string.IsNullOrWhiteSpace(end_datee)) fdict.Add("重复字段1重复字段end_date", "<=" + end_datee);
                if (!string.IsNullOrWhiteSpace(stock_no)) fdict.Add("stock_no", stock_no + "%");
                if (!string.IsNullOrWhiteSpace(stock_name)) fdict.Add("stock_name", stock_name + "%");
                if (!string.IsNullOrWhiteSpace(money_debts)) fdict.Add("money_debt", ">=" + money_debts);
                if (!string.IsNullOrWhiteSpace(money_debte)) fdict.Add("重复字段1重复字段money_debt", "<=" + money_debte);
                if (!string.IsNullOrWhiteSpace(sys_user_id)) fdict.Add("sys_user_id", sys_user_id);
                if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
                list = await DBHelper.GetList<StockPlanModel, long>(TABLE, "*", order, fdict, "and",
                        Convert.ToInt32(page_size), Convert.ToInt32(page_index));
            }
            return await SysUser.FillFkInfo(await User.FillFkInfo(list));
        }
        public static async Task<long> GetCount(string id, string user_id, string plan_status_id, string profit,
            string start_dates, string start_datee, string end_dates, string end_datee,
            string stock_no, string stock_name, string money_debts, string money_debte, string sys_user_id)
        {
            if (!string.IsNullOrWhiteSpace(sys_user_id))
            {
                SysUserModel suser = await SysUser.GetOne(Convert.ToInt32(sys_user_id), string.Empty);
                if (suser == null) return 0;
                if (suser.user_id <= 0) sys_user_id = string.Empty;
                else if (!string.IsNullOrWhiteSpace(plan_status_id) && plan_status_id.Equals("1"))
                    sys_user_id = string.Empty;
            }
            if (StockInfo.SIM_DATA)
            {
                if (!string.IsNullOrWhiteSpace(end_datee))
                    end_datee = Convert.ToDateTime(end_datee).AddDays(7).ToShortDateString();
            }

            if (!string.IsNullOrWhiteSpace(plan_status_id) && plan_status_id.Contains(","))
            {
                string filter = string.Empty;
                foreach (string pid in plan_status_id.Split(','))
                {
                    filter += " or plan_status_id=" + pid;
                }
                filter = " and (" + filter.Substring(4) + ")";
                if (!string.IsNullOrWhiteSpace(id)) filter += " and id=" + id;
                if (!string.IsNullOrWhiteSpace(user_id)) filter += " and user_id=" + user_id;
                if (!string.IsNullOrWhiteSpace(profit)) filter += " and profit " + profit;
                if (!string.IsNullOrWhiteSpace(start_dates)) filter += " and start_date>='" + start_dates + "'";
                if (!string.IsNullOrWhiteSpace(start_datee)) filter += " and start_date<='" + start_datee + "'";
                if (!string.IsNullOrWhiteSpace(end_dates)) filter += " and end_date>='" + end_dates + "'";
                if (!string.IsNullOrWhiteSpace(end_datee)) filter += " and end_date<='" + end_datee + "'";
                if (!string.IsNullOrWhiteSpace(stock_no)) filter += " and stock_no like '" + stock_no + "%'";
                if (!string.IsNullOrWhiteSpace(stock_name)) filter += " and stock_name like '" + stock_name + "%'";
                if (!string.IsNullOrWhiteSpace(money_debts)) filter += " and money_debt>='" + money_debts + "'";
                if (!string.IsNullOrWhiteSpace(money_debte)) filter += " and money_debt<='" + money_debte + "'";
                if (!string.IsNullOrWhiteSpace(sys_user_id)) filter += " and sys_user_id=" + sys_user_id;
                if (!string.IsNullOrWhiteSpace(User.CMP_ID)) filter += " and cmp_id=" + User.CMP_ID;
                return await DBHelper.GetCountFree("select count(id) from stock_plan where " + filter.Substring(5));
            }
            else
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(id)) fdict.Add("id", id);
                if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
                if (!string.IsNullOrWhiteSpace(plan_status_id)) fdict.Add("plan_status_id", plan_status_id);
                if (!string.IsNullOrWhiteSpace(profit)) fdict.Add("profit", profit);
                if (!string.IsNullOrWhiteSpace(start_dates)) fdict.Add("start_date", ">=" + start_dates);
                if (!string.IsNullOrWhiteSpace(start_datee)) fdict.Add("重复字段1重复字段start_date", "<=" + start_datee);
                if (!string.IsNullOrWhiteSpace(end_dates)) fdict.Add("end_date", ">=" + end_dates);
                if (!string.IsNullOrWhiteSpace(end_datee)) fdict.Add("重复字段1重复字段end_date", "<=" + end_datee);
                if (!string.IsNullOrWhiteSpace(stock_no)) fdict.Add("stock_no", stock_no + "%");
                if (!string.IsNullOrWhiteSpace(stock_name)) fdict.Add("stock_name", stock_name + "%");
                if (!string.IsNullOrWhiteSpace(money_debts)) fdict.Add("money_debt", ">=" + money_debts);
                if (!string.IsNullOrWhiteSpace(money_debte)) fdict.Add("重复字段1重复字段money_debt", "<=" + money_debte);
                if (!string.IsNullOrWhiteSpace(sys_user_id)) fdict.Add("sys_user_id", sys_user_id);
                if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
                return await DBHelper.GetCount(TABLE, fdict, "and");
            }
        }
        public static async Task<string> Exp(string id, string user_id, string plan_status_id, string profit,
            string start_dates, string start_datee, string end_dates, string end_datee,
            string stock_no, string stock_name, string money_debts, string money_debte, string sys_user_id, string order)
        {
            IDictionary<string, string> cols = new Dictionary<string, string>();
            cols.Add("ID", "id");
            cols.Add("用户ID", "user_id");
            cols.Add("昵称", "user_nick_name");
            cols.Add("姓名", "user_name");
            cols.Add("电话", "user_mobile");
            cols.Add("投资人", "sys_user_name");
            cols.Add("股票代码", "stock_no");
            cols.Add("股票名称", "stock_name");
            cols.Add("点买额", "money_debt");
            cols.Add("股数", "stock_amount");
            cols.Add("开始日期", "start_date");
            cols.Add("截止日期", "end_date");
            cols.Add("欠费", "money_npay");
            cols.Add("保证金", "money_margin");
            cols.Add("止损额", "stop_loss_money");
            cols.Add("盈亏", "profit");
            cols.Add("用户盈亏", "user_profit");
            cols.Add("状态", "plan_status_id");
            IEnumerable<StockPlanModel> list = await GetList(id, user_id, plan_status_id, profit, start_dates, start_datee,
                end_dates, end_datee, stock_no, stock_name, money_debts, money_debte, sys_user_id, order, "0", "0");
            return Excel.MakeXmlForExcel(list, cols);
        }
        public static async Task<StockPlanModel> GetOne(ulong id, ulong user_id, bool userInfo = false)
        {
            if (id <= 0) return null;
            StockPlanModel plan = await DBHelper.GetOne<StockPlanModel>(id.ToString());
            if (plan != null)
            {
                if (!string.IsNullOrWhiteSpace(User.CMP_ID))
                {
                    if (!plan.cmp_id.ToString().Equals(User.CMP_ID)) return null;
                }
            }

            if (plan != null && user_id > 0 && plan.user_id != user_id) return null;
            if (plan != null && userInfo)
            {
                UserModel user = await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty);
                if (user != null)
                {
                    if (!string.IsNullOrWhiteSpace(user.mobile)) plan.user_mobile = user.mobile;
                    if (!string.IsNullOrWhiteSpace(user.nick_name)) plan.user_nick_name = user.nick_name;
                    if (!string.IsNullOrWhiteSpace(user.name)) plan.user_name = user.name;
                }
                SysUserModel suser = await SysUser.GetOne(plan.sys_user_id, string.Empty);
                if (suser != null)
                {
                    plan.sys_user_name = suser.name;
                }
            }
            return plan;
        }
        public static StockPlanModel GetOneSync(ulong id)
        {
            if (id <= 0) return null;
            return DBHelper.GetOneSync<StockPlanModel>(id.ToString());
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<StockPlanModel>(id.ToString());
        }
        public static void DeleteRedisSync(ulong id)
        {
            if (id <= 0) return;
            Redis.DeleteSync<StockPlanModel>(id.ToString());
        }
        #endregion
        #region 递延
        //public static async Task<string> Delay(ulong id, ulong user_id)
        //{
        //    if (id <= 0) return "参数不全";
        //    StockPlanModel plan = await GetOne(id, 0);
        //    if (plan == null) return "无法读取PLAN";
        //    if (plan.plan_status_id != 3) return "状态异常";
        //    if (user_id > 0 && plan.user_id != user_id) return "权限异常";
        //    if (!StockInfo.SIM_DATA)
        //    {
        //        if (plan.end_date != DateTime.Today) return "今日不可执行递延指令";
        //    }
        //    DateTime end_date = StockInfo.GetNextOrderDate(DateTime.Today);
        //    if (plan.end_date >= end_date) return "请勿重复递延";
        //    if (!StockInfo.SIM_DATA)
        //    {
        //        if (!await StockForbidden.CanBuy(plan.stock_no)) return "该股票不可递延";
        //        if (plan.defer_times > ZStockSettings.DEFER_DAYS) return "超过最大递延限制";
        //        if (!await ZStockSettings.CanDelay(plan.stock_no, plan.money_margin, plan.buy_price)) return "低于递延阈值";
        //    }
        //    decimal money = plan.money_debt / 10000 * ZStockSettings.DEFER_FEE;
        //    UserModel user = await User.GetOne(plan.user_id, string.Empty, string.Empty, string.Empty);
        //    if (user == null) return "读取不到USER";
        //    if (user.money < money) return "账户余额不足：" + money;
        //    //延期成功的，计算邀请佣金
        //    UserModel ruser = null;
        //    decimal profit_from_ref = 0;
        //    if (user.ref_id > 0)
        //    {
        //        ruser = await User.GetOne(user.ref_id, string.Empty, string.Empty, string.Empty);
        //        if (ruser != null)
        //        {
        //            if (ruser.delete_flag == 1) ruser = null;
        //            else
        //            {
        //                profit_from_ref = user.profit_from_ref;
        //                if (profit_from_ref <= 0) profit_from_ref = ZStockSettings.PROFIT_FROM_REF;
        //                profit_from_ref = plan.money_debt / 10000 * profit_from_ref;
        //            }
        //        }
        //    }

        //    using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
        //    {
        //        using (MySqlTransaction trans = dbConnection.BeginTransaction())
        //        {
        //            IDictionary<string, string> dict = new Dictionary<string, string>();
        //            dict.Add("end_date", end_date.ToString("yyyy-MM-dd"));
        //            dict.Add("defer_times", "数字相加+1");
        //            dict.Add("defer_fee", "数字相加+" + money);
        //            if (ruser != null && profit_from_ref > 0) dict.Add("defer_ref_fee", "数字相加+" + profit_from_ref);
        //            IDictionary<string, string> fdict = new Dictionary<string, string>();
        //            fdict.Add("id", id.ToString());
        //            fdict.Add("重复字段1重复字段plan_status_id", "3");
        //            fdict.Add("重复字段1重复字段end_date", plan.end_date.ToString("yyyy-MM-dd"));
        //            if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
        //            {
        //                trans.Rollback();
        //                return "异常，请重试";
        //            }

        //            dict = new Dictionary<string, string>();
        //            dict.Add("money", "数字相减-" + money);
        //            fdict = new Dictionary<string, string>();
        //            fdict.Add("id", plan.user_id.ToString());
        //            fdict.Add("重复字段1重复字段money", ">=" + money);
        //            if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
        //            {
        //                trans.Rollback();
        //                return "SQL执行错误";
        //            }

        //            if ((await UserMoneyChange.Insert(plan.user_id, 2, -money,
        //                user.money - money, "递延", TABLE, id, dbConnection, trans)) <= 0)
        //            {
        //                trans.Rollback();
        //                return "SQL执行错误";
        //            }

        //            if (ruser != null && profit_from_ref > 0)
        //            {
        //                dict = new Dictionary<string, string>();
        //                dict.Add("money", "数字相加+" + profit_from_ref);
        //                fdict = new Dictionary<string, string>();
        //                fdict.Add("id", ruser.id.ToString());
        //                if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
        //                {
        //                    trans.Rollback();
        //                    return "SQL执行错误";
        //                }

        //                if ((await UserMoneyChange.Insert(ruser.id, 1, profit_from_ref, ruser.money + profit_from_ref, "邀请收益", TABLE, id, dbConnection, trans)) <= 0)
        //                {
        //                    trans.Rollback();
        //                    return "SQL执行错误";
        //                }
        //            }

        //            trans.Commit();
        //        }
        //    }

        //    await DeleteRedis(id);
        //    await User.DeleteRedis(plan.user_id);
        //    if (ruser != null && profit_from_ref > 0) await User.DeleteRedis(ruser.id);
        //    return string.Empty;
        //}
        //public static string DelaySync(ulong id, ulong user_id)
        //{
        //    if (id <= 0) return "参数不全";
        //    StockPlanModel plan = GetOneSync(id);
        //    if (plan == null) return "无法读取PLAN";
        //    if (plan.plan_status_id != 3) return "状态异常";
        //    if (user_id > 0 && plan.user_id != user_id) return "权限异常";
        //    if (!StockInfo.SIM_DATA)
        //    {
        //        if (plan.end_date != DateTime.Today) return "今日不可执行递延指令";
        //    }
        //    DateTime end_date = StockInfo.GetNextOrderDate(DateTime.Today);
        //    if (plan.end_date >= end_date) return "请勿重复递延";
        //    //优先判断是否停牌股票
        //    bool isStop = StockInfo.IsLimitDown(plan.stock_no);
        //    if (!isStop)
        //    {
        //        if (!StockInfo.SIM_DATA)
        //        {
        //            if (!StockForbidden.CanBuySync(plan.stock_no)) return "该股票不可递延";
        //            if (plan.defer_times > ZStockSettings.DEFER_DAYS) return "超过最大递延限制";
        //            if (!ZStockSettings.CanDelaySync(plan.stock_no, plan.money_margin, plan.buy_price)) return "低于递延阈值";
        //        }
        //    }
        //    decimal money = plan.money_debt / 10000 * ZStockSettings.DEFER_FEE;
        //    UserModel user = User.GetOneSync(plan.user_id, string.Empty, string.Empty, string.Empty);
        //    if (user == null) return "读取不到USER";
        //    if (user.money < money)
        //    {
        //        if (isStop)
        //        {
        //            //强制递延
        //            decimal npay = money - user.money;
        //            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
        //            {
        //                using (MySqlTransaction trans = dbConnection.BeginTransaction())
        //                {
        //                    IDictionary<string, string> dict = new Dictionary<string, string>();
        //                    dict.Add("end_date", end_date.ToString("yyyy-MM-dd"));
        //                    dict.Add("defer_times", "数字相加+1");
        //                    dict.Add("defer_fee", "数字相加+" + user.money);
        //                    dict.Add("money_npay", "数字相加+" + npay);
        //                    IDictionary<string, string> fdict = new Dictionary<string, string>();
        //                    fdict.Add("id", id.ToString());
        //                    fdict.Add("重复字段1重复字段plan_status_id", "3");
        //                    fdict.Add("重复字段1重复字段end_date", plan.end_date.ToString("yyyy-MM-dd"));
        //                    if ((DBHelper.UpdateSync(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
        //                    {
        //                        trans.Rollback();
        //                        return "异常，请重试";
        //                    }

        //                    dict = new Dictionary<string, string>();
        //                    dict.Add("money", "数字相减-" + user.money);
        //                    dict.Add("money_npay", "数字相加+" + npay);
        //                    fdict = new Dictionary<string, string>();
        //                    fdict.Add("id", plan.user_id.ToString());
        //                    fdict.Add("重复字段1重复字段money", ">=" + user.money);
        //                    if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
        //                    {
        //                        trans.Rollback();
        //                        return "SQL执行错误";
        //                    }

        //                    if ((UserMoneyChange.InsertSync(plan.user_id, 2, -user.money,
        //                        0, "因停牌递延，欠费" + npay, TABLE, id, dbConnection, trans)) <= 0)
        //                    {
        //                        trans.Rollback();
        //                        return "SQL执行错误";
        //                    }

        //                    trans.Commit();
        //                }
        //            }

        //            DeleteRedisSync(id);
        //            User.DeleteRedisSync(plan.user_id);
        //            return string.Empty;
        //        }
        //        else
        //            return "账户余额不足：" + money;
        //    }
        //    //延期成功的，计算邀请佣金
        //    UserModel ruser = null;
        //    decimal profit_from_ref = 0;
        //    if (user.ref_id > 0)
        //    {
        //        ruser = User.GetOneSync(user.ref_id, string.Empty, string.Empty, string.Empty);
        //        if (ruser != null)
        //        {
        //            if (ruser.delete_flag == 1) ruser = null;
        //            else
        //            {
        //                profit_from_ref = user.profit_from_ref;
        //                if (profit_from_ref <= 0) profit_from_ref = ZStockSettings.PROFIT_FROM_REF;
        //                profit_from_ref = plan.money_debt / 10000 * profit_from_ref;
        //            }
        //        }
        //    }

        //    using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
        //    {
        //        using (MySqlTransaction trans = dbConnection.BeginTransaction())
        //        {
        //            IDictionary<string, string> dict = new Dictionary<string, string>();
        //            dict.Add("end_date", end_date.ToString("yyyy-MM-dd"));
        //            dict.Add("defer_times", "数字相加+1");
        //            dict.Add("defer_fee", "数字相加+" + money);
        //            if (ruser != null && profit_from_ref > 0) dict.Add("defer_ref_fee", "数字相加+" + profit_from_ref);
        //            IDictionary<string, string> fdict = new Dictionary<string, string>();
        //            fdict.Add("id", id.ToString());
        //            fdict.Add("重复字段1重复字段plan_status_id", "3");
        //            fdict.Add("重复字段1重复字段end_date", plan.end_date.ToString("yyyy-MM-dd"));
        //            if ((DBHelper.UpdateSync(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
        //            {
        //                trans.Rollback();
        //                return "异常，请重试";
        //            }

        //            dict = new Dictionary<string, string>();
        //            dict.Add("money", "数字相减-" + money);
        //            fdict = new Dictionary<string, string>();
        //            fdict.Add("id", plan.user_id.ToString());
        //            fdict.Add("重复字段1重复字段money", ">=" + money);
        //            if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
        //            {
        //                trans.Rollback();
        //                return "SQL执行错误";
        //            }

        //            if ((UserMoneyChange.InsertSync(plan.user_id, 2, -money,
        //                user.money - money, "递延", TABLE, id, dbConnection, trans)) <= 0)
        //            {
        //                trans.Rollback();
        //                return "SQL执行错误";
        //            }

        //            if (ruser != null && profit_from_ref > 0)
        //            {
        //                dict = new Dictionary<string, string>();
        //                dict.Add("money", "数字相加+" + profit_from_ref);
        //                fdict = new Dictionary<string, string>();
        //                fdict.Add("id", ruser.id.ToString());
        //                if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
        //                {
        //                    trans.Rollback();
        //                    return "SQL执行错误";
        //                }

        //                if ((UserMoneyChange.InsertSync(ruser.id, 1, profit_from_ref, ruser.money + profit_from_ref, "邀请收益", TABLE, id, dbConnection, trans)) <= 0)
        //                {
        //                    trans.Rollback();
        //                    return "SQL执行错误";
        //                }
        //            }

        //            trans.Commit();
        //        }
        //    }

        //    DeleteRedisSync(id);
        //    User.DeleteRedisSync(plan.user_id);
        //    if (ruser != null && profit_from_ref > 0) User.DeleteRedisSync(ruser.id);
        //    return string.Empty;
        //}
        /// <summary>
        /// 强制递延
        /// </summary>
        /// <returns></returns>
        public static string DelaySync(ulong id)
        {
            if (id <= 0) return "参数不全";
            StockPlanModel plan = GetOneSync(id);
            if (plan == null) return "无法读取PLAN";
            if (plan.plan_status_id != 3 && plan.plan_status_id != 4 && plan.plan_status_id != 5) return "状态异常";
            if (!StockInfo.SIM_DATA)
            {
                if (plan.end_date != DateTime.Today) return "今日不可执行递延指令";
            }
            DateTime end_date = StockInfo.GetNextOrderDate(DateTime.Today);
            if (plan.end_date >= end_date) return "请勿重复递延";
            decimal money = plan.money_debt / 10000 * ZStockSettings.DEFER_FEE;
            UserModel user = User.GetOneSync(plan.user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return "读取不到USER";
            if (user.money < money)
            {
                decimal npay = money - user.money;
                using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
                {
                    using (MySqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        IDictionary<string, string> dict = new Dictionary<string, string>();
                        dict.Add("end_date", end_date.ToString("yyyy-MM-dd"));
                        dict.Add("defer_times", "数字相加+1");
                        dict.Add("defer_fee", "数字相加+" + user.money);
                        dict.Add("money_npay", "数字相加+" + npay);
                        if (plan.plan_status_id == 4)
                        {
                            dict.Add("plan_status_id", "3");
                            dict.Add("remark", "撤销平仓");
                        }
                        IDictionary<string, string> fdict = new Dictionary<string, string>();
                        fdict.Add("id", id.ToString());
                        fdict.Add("重复字段1重复字段plan_status_id", plan.plan_status_id.ToString());
                        fdict.Add("重复字段1重复字段end_date", plan.end_date.ToString("yyyy-MM-dd"));
                        if ((DBHelper.UpdateSync(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "异常，请重试";
                        }

                        dict = new Dictionary<string, string>();
                        dict.Add("money", "数字相减-" + user.money);
                        dict.Add("money_npay", "数字相加+" + npay);
                        if (plan.plan_status_id == 4)
                            dict.Add("stock_plan_debt", "数字相加+" + plan.money_debt);
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.user_id.ToString());
                        fdict.Add("重复字段1重复字段money", ">=" + user.money);
                        if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                        //1部分组成
                        if ((UserMoneyChange.InsertSync(plan.user_id, 2, -user.money,
                            0, "递延，仍欠费" + npay, TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }

                        trans.Commit();
                    }
                }

                DeleteRedisSync(id);
                User.DeleteRedisSync(plan.user_id);
                return string.Empty;
            }
            //延期成功的，计算邀请佣金
            UserModel ruser = null;
            decimal profit_from_ref = 0;
            if (user.ref_id > 0)
            {
                ruser = User.GetOneSync(user.ref_id, string.Empty, string.Empty, string.Empty);
                if (ruser != null)
                {
                    if (ruser.delete_flag == 1) ruser = null;
                    else
                    {
                        profit_from_ref = ruser.profit_from_ref;
                        if (profit_from_ref <= 0) profit_from_ref = ZStockSettings.PROFIT_FROM_REF;
                        profit_from_ref = plan.money_debt / 10000 * profit_from_ref;
                    }
                }
            }

            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("end_date", end_date.ToString("yyyy-MM-dd"));
                    dict.Add("defer_times", "数字相加+1");
                    dict.Add("defer_fee", "数字相加+" + money);
                    if (ruser != null && profit_from_ref > 0) dict.Add("defer_ref_fee", "数字相加+" + profit_from_ref);
                    if (plan.plan_status_id == 4)
                    {
                        dict.Add("plan_status_id", "3");
                        dict.Add("remark", "撤销平仓");
                    }
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段plan_status_id", plan.plan_status_id.ToString());
                    fdict.Add("重复字段1重复字段end_date", plan.end_date.ToString("yyyy-MM-dd"));
                    if ((DBHelper.UpdateSync(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("money", "数字相减-" + money);
                    if (plan.plan_status_id == 4)
                        dict.Add("stock_plan_debt", "数字相加+" + plan.money_debt);
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", plan.user_id.ToString());
                    fdict.Add("重复字段1重复字段money", ">=" + money);
                    if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }
                    //1部分组成
                    if ((UserMoneyChange.InsertSync(plan.user_id, 2, -money,
                        user.money - money, "递延", TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    if (ruser != null && profit_from_ref > 0)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("money", "数字相加+" + profit_from_ref);
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", ruser.id.ToString());
                        if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                        //1部分组成
                        if ((UserMoneyChange.InsertSync(ruser.id, 1, profit_from_ref, ruser.money + profit_from_ref, "邀请收益",
                            TABLE, id, ruser.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    trans.Commit();
                }
            }

            DeleteRedisSync(id);
            User.DeleteRedisSync(plan.user_id);
            if (ruser != null && profit_from_ref > 0) User.DeleteRedisSync(ruser.id);
            return string.Empty;
        }
        #endregion
        #region 定时止损、止盈、递延、费用不足自动平仓
        public static void AutoStop()
        {
            if (!StockInfo.InOrderDate(DateTime.Today)) return;
            ZStockSettings.Init();

            DateTime dt1 = StockInfo.GetRealMidEndTime();
            DateTime dt2 = StockInfo.GetRealMidStartTime();
            DateTime dt3 = StockInfo.GetRealEndTime();
            string today = DateTime.Today.ToShortDateString();

            do
            {
                if (DateTime.Now > dt3) return;
                if (DateTime.Now > dt1 && DateTime.Now < dt2)
                {
                    Thread.Sleep(5000);
                    continue;
                }
                if (ZStockSettings.GetRongduanSync() > 0)
                {
                    Thread.Sleep(20000);
                    continue;
                }
                //止损、止盈
                IEnumerable<StockPlanModel> list = DBHelper.GetFreeSync<StockPlanModel>("select * from stock_plan where start_date<'" + today + "' and plan_status_id=3");
                foreach (StockPlanModel plan in list)
                {
                    decimal now = StockInfo.GetNowPriceSync(plan.stock_no);
                    if (now == 0) continue;
                    string remark = string.Empty;
                    if (now >= plan.stop_earn_point) remark = "止盈（触发价位：" + now + "）";
                    if (now <= plan.stop_loss_point) remark = "止损（触发价位：" + now + "）";
                    if (!string.IsNullOrWhiteSpace(remark))
                    {
                        string result = PreSellSync(plan.id, remark, 0, true);
                        if (string.IsNullOrWhiteSpace(result))
                        {
                            UserModel user = User.GetOneSync(plan.user_id, string.Empty, string.Empty, string.Empty);
                            if (user != null)
                                Mobile.SendSmsSync(user.mobile, "您的方案：" + plan.id + "，由于“" + remark + "”，即将平仓");
                        }
                        else
                            Util.Log.Info("err", plan.id + "->" + remark + "->" + result);
                    }
                }
                Thread.Sleep(2000);

            } while (true);
        }
        public static void AutoDelayRemind()
        {
            //延期费用不足提醒，14：00
            if (!StockInfo.InOrderDate(DateTime.Today)) return;
            if (ZStockSettings.GetRongduanSync() > 0) return;
            ZStockSettings.Init();

            IEnumerable<UserModel> list = DBHelper.GetFreeSync<UserModel>("select * from user where stock_plan_debt>0 && stock_plan_debt/10000*" +
                ZStockSettings.DEFER_FEE + ">money");
            foreach (UserModel user in list)
            {
                string msg = "您当前持仓中的方案如需递延到下一个交易日需要" +
                    (user.stock_plan_debt / 10000 * ZStockSettings.DEFER_FEE) +
                    "递延费用，当前余额不足，请及时充值，如不想递延，请在14：50分之前自行卖出，否则14：50分投资人有权直接卖出";
                Mobile.SendSmsSync(user.mobile, msg);
            }
        }
        public static void AutoPreSell()
        {
            if (!StockInfo.InOrderDate(DateTime.Today)) return;
            if (ZStockSettings.GetRongduanSync() > 0) return;
            ZStockSettings.Init();

            string rmk = string.Empty;
            do
            {
                if (DateTime.Now.Hour != 14 || DateTime.Now.Minute > 50) return;

                //操盘中的方案判断是否满足递延条件
                IEnumerable<StockPlanModel> list2 = DBHelper.GetFreeSync<StockPlanModel>("select * from stock_plan where end_date<='" + DateTime.Today.ToShortDateString() + "' and plan_status_id=3");
                foreach (StockPlanModel plan in list2)
                {
                    //优先判断是否停牌股票
                    rmk = string.Empty;
                    bool isStop = StockInfo.IsLimitDown(plan.stock_no);
                    if (!isStop)
                    {
                        if (!StockInfo.SIM_DATA)
                        {
                            if (!StockForbidden.CanBuySync(plan.stock_no)) rmk = "该股票不可递延";
                            else if (plan.defer_times > ZStockSettings.DEFER_DAYS) rmk = "超过最大递延限制";
                            else if (!ZStockSettings.CanDelaySync(plan.stock_no, plan.money_margin, plan.buy_price, plan.money_debt, plan.stock_amount)) rmk = "低于递延阈值";
                        }
                    }
                    if (rmk.Length == 0 && plan.end_date < DateTime.Today) rmk = "过期方案自动平仓";
                    if (rmk.Length > 0)
                    {
                        string result = PreSellSync(plan.id, rmk, 0, true);
                        if (string.IsNullOrWhiteSpace(result))
                        {
                            UserModel user = User.GetOneSync(plan.user_id, string.Empty, string.Empty, string.Empty);
                            if (user != null)
                                Mobile.SendSmsSync(user.mobile, "您的方案：" + plan.id + "，由于“" + rmk + "”，即将平仓");
                        }
                        else
                            Util.Log.Info("err", plan.id + "->" + rmk + "->" + result);
                    }
                }
                rmk = "递延费不足";
                IEnumerable<UserModel> list = DBHelper.GetFreeSync<UserModel>("select * from user where stock_plan_debt>0 && stock_plan_debt/10000*" +
                    ZStockSettings.DEFER_FEE + ">money");
                foreach (UserModel user in list)
                {
                    IEnumerable<StockPlanModel> spList = DBHelper.GetFreeSync<StockPlanModel>("select * from stock_plan where user_id=" + user.id + " and end_date='" + DateTime.Today.ToShortDateString() + "' and plan_status_id=3");

                    Dictionary<ulong, decimal> pdict = new Dictionary<ulong, decimal>();
                    Dictionary<ulong, decimal> tdict = new Dictionary<ulong, decimal>();
                    foreach (StockPlanModel spm in spList)
                    {
                        decimal np = StockInfo.GetNowPriceSync(spm.stock_no);
                        if (np > 0)
                        {
                            np = spm.stock_amount * (np - spm.buy_price);
                        }
                        pdict.Add(spm.id, np);

                        tdict.Add(spm.id, spm.money_debt / 10000 * ZStockSettings.DEFER_FEE);
                    }
                    Dictionary<ulong, decimal> pdictDesc
                        = (from d in pdict orderby d.Value descending select d).ToDictionary(k => k.Key, v => v.Value);

                    decimal total = 0;
                    foreach (ulong id in pdictDesc.Keys)
                    {
                        total += tdict[id];
                        if (total > user.money)
                        {
                            string result = PreSellSync(id, rmk, 0, true);
                            if (string.IsNullOrWhiteSpace(result))
                                Mobile.SendSmsSync(user.mobile, "您的方案：" + id + "，由于“" + rmk + "”，即将平仓");
                            else
                                Util.Log.Info("err", id + "->" + rmk + "->" + result);
                        }
                    }
                }

                Thread.Sleep(120000);
            }
            while (true);
        }
        public static void AutoDelay()
        {
            //延期，15:01
            if (!StockInfo.InOrderDate(DateTime.Today)) return;
            ZStockSettings.Init();

            IEnumerable<StockPlanModel> list = DBHelper.GetFreeSync<StockPlanModel>("select * from stock_plan where end_date='" + DateTime.Today.ToShortDateString() + "' and (plan_status_id=3 or plan_status_id=4 or plan_status_id=5)");
            foreach (StockPlanModel plan in list)
            {
                string msg = DelaySync(plan.id);
                if (!string.IsNullOrWhiteSpace(msg)) Util.Log.Info("err", plan.id + "->" + msg);
            }
        }
        #endregion
        #region 获取任务
        public static async Task<string> GetTask(int sys_user_id)
        {
            string task = string.Empty;
            long count = await DBHelper.GetCountFree("select count(*) from stock_plan where plan_status_id=1");
            task = (count + ",");
            count = await DBHelper.GetCountFree("select count(*) from stock_plan where plan_status_id=4 and sys_user_id=" + sys_user_id);
            task += (count + ",");
            count = await DBHelper.GetCountFree("select count(*) from stock_plan where plan_status_id=5 and sys_user_id=" + sys_user_id);
            task += count;
            return task;
        }
        #endregion
        #region 长时间无人接单
        public static void Cancel()
        {
            if (!StockInfo.InOrderDate(DateTime.Today)) return;

            ZStockSettings.Init();

            DateTime dt3 = StockInfo.GetRealEndTime();
            do
            {
                if (DateTime.Now > dt3) return;

                IEnumerable<StockPlanModel> list = DBHelper.GetFreeSync<StockPlanModel>("select * from stock_plan where plan_status_id=1 and buy_time<='" + DateTime.Now.AddSeconds(-ZStockSettings.BUY_WAIT_SECOND) + "'");
                foreach (StockPlanModel plan in list)
                {
                    SetSysUserSync(plan.id, 1, -1, string.Empty);
                }
                Thread.Sleep(5000);
            } while (true);
        }
        #endregion
        #region 设置借款额
        public static void SetDebt()
        {
            //每日早上0点设置
            IEnumerable<SetDebtModel> list = DBHelper.GetFreeSync<SetDebtModel>("select user_id,sum(money_debt) as money_debt from stock_plan where start_date='" + DateTime.Today.AddDays(-1).ToShortDateString() + "' and plan_status_id=3 group by user_id");
            foreach (SetDebtModel plan in list)
            {
                IDictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("stock_plan_debt", "数字相加+" + plan.money_debt);
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("id", plan.user_id.ToString());
                DBHelper.UpdateSync(User.TABLE, dict, fdict, "and");

                User.DeleteRedisSync(plan.user_id);
            }
        }
        class SetDebtModel
        {
            public ulong user_id { get; set; }
            public decimal money_debt { get; set; }
        }
        #endregion
        #region 辅助
        public static async Task<int> SetTodayAmount(ulong user_id, int add)
        {
            string key = "StockPlan_SetTodayAmount_" + user_id + "_" + DateTime.Today.ToString("yyyy-MM-dd");
            await Redis.InsertNumber(key, add);
            return await GetTodayAmount(user_id);
        }
        public static int SetTodayAmountSync(ulong user_id, int add)
        {
            string key = "StockPlan_SetTodayAmount_" + user_id + "_" + DateTime.Today.ToString("yyyy-MM-dd");
            Redis.InsertNumberSync(key, add);
            return GetTodayAmountSync(user_id);
        }
        public static async Task<int> GetTodayAmount(ulong user_id)
        {
            string key = "StockPlan_SetTodayAmount_" + user_id + "_" + DateTime.Today.ToString("yyyy-MM-dd");
            RedisValue rv = await Redis.GetString(key);
            int amount = 0;
            if (rv.HasValue)
            {
                int.TryParse(rv, out amount);
            }
            return amount;
        }
        public static int GetTodayAmountSync(ulong user_id)
        {
            string key = "StockPlan_SetTodayAmount_" + user_id + "_" + DateTime.Today.ToString("yyyy-MM-dd");
            RedisValue rv = Redis.GetStringSync(key);
            int amount = 0;
            if (rv.HasValue)
            {
                int.TryParse(rv, out amount);
            }
            return amount;
        }
        public static async Task<string> ExpToday(string sys_user_id, string start_dates, string start_datee)
        {
            IDictionary<string, string> cols = new Dictionary<string, string>();
            cols.Add("用户ID", "user_id");
            cols.Add("股票代码", "stock_no");
            cols.Add("五矿账户", string.Empty);
            cols.Add("差额1", string.Empty);
            cols.Add("股票名称", "stock_name");
            cols.Add("点买额度", "money_debt");
            cols.Add("持仓数量", "stock_amount");
            cols.Add("成交数量", string.Empty);
            cols.Add("差额2", string.Empty);
            cols.Add("买入价格", "buy_price");
            cols.Add("成交均价", string.Empty);
            cols.Add("差额3", string.Empty);
            cols.Add("买入时间", "buy_time");
            cols.Add("卖出价格", "sell_price");
            cols.Add("卖出价", string.Empty);
            cols.Add("差额4", string.Empty);
            cols.Add("卖出时间", "sell_time");

            string sql = string.Format(@"SELECT * FROM
        (select * from stock_plan where sys_user_id={0} and start_date>='{1}' and start_date<='{2}' and 
(plan_status_id=3 or plan_status_id=4) order by buy_time desc) AS p1
    UNION
    SELECT * FROM
        (select * from stock_plan where sys_user_id={0} and end_date>='{1}' and end_date<='{2}' and 
plan_status_id>4 order by sell_time desc) AS p2",
                sys_user_id, start_dates, start_datee);
            IEnumerable<StockPlanModel> list = await DBHelper.GetFree<StockPlanModel>(sql);
            return Excel.MakeXmlForExcel(list, cols);
        }
        #endregion
        #region 主页统计
        static IList<StockPlanModel> LAST_LIST = null;
        public static async Task<IEnumerable<StockPlanModel>> GetTop()
        {
            if (LAST_LIST == null)
            {
                string sql = "select * from stock_plan where plan_status_id>2 order by id desc limit 0,7";
                LAST_LIST = (await DBHelper.GetFree<StockPlanModel>(sql)).ToList();
                LAST_LIST = (await User.FillFkInfo(LAST_LIST, false)).ToList();
                foreach (StockPlanModel spm in LAST_LIST)
                {
                    if (!string.IsNullOrWhiteSpace(spm.user_nick_name))
                    {
                        if (spm.user_nick_name.Length == 11)
                            spm.user_nick_name = (spm.user_nick_name.Substring(0, 4) + "****" + spm.user_nick_name.Substring(8));
                    }
                    if (!string.IsNullOrWhiteSpace(spm.user_mobile))
                    {
                        if (spm.user_mobile.Length == 11)
                            spm.user_mobile = (spm.user_mobile.Substring(0, 4) + "****" + spm.user_mobile.Substring(8));
                    }
                }
            }
            return LAST_LIST;
        }
        static async Task InsTop(StockPlanModel spm)
        {
            if (!string.IsNullOrWhiteSpace(spm.user_nick_name))
            {
                if (spm.user_nick_name.Length == 11)
                    spm.user_nick_name = (spm.user_nick_name.Substring(0, 4) + "****" + spm.user_nick_name.Substring(8));
            }
            if (!string.IsNullOrWhiteSpace(spm.user_mobile))
            {
                if (spm.user_mobile.Length == 11)
                    spm.user_mobile = (spm.user_mobile.Substring(0, 4) + "****" + spm.user_mobile.Substring(8));
            }

            await GetTop();
            while (LAST_LIST.Count() > 6)
                LAST_LIST.RemoveAt(6);
            LAST_LIST.Insert(0, spm);
        }
        #endregion
        #region 结算区统计
        public static async Task<IEnumerable<StockPlanJsModel>> GetJsData(ulong user_id)
        {
            string sql = string.Format("select plan_status_id,count(*) as amount from stock_plan where user_id={0} and (plan_status_id=4 or plan_status_id=5 or plan_status_id=6 or (plan_status_id=-1 and start_date='{1}')) group by plan_status_id", user_id, DateTime.Today.ToShortDateString());
            return await DBHelper.GetFree<StockPlanJsModel>(sql);
        }
        public class StockPlanJsModel
        {
            public int plan_status_id { get; set; }
            public int amount { get; set; }
        }
        #endregion
    }
}
