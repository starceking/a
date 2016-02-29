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
    public static class StockPlanXrd
    {
        #region 常量
        const string TABLE = "stock_plan_xrd";
        #endregion
        #region 一般操作
        public static async Task<string> Insert(string stock_no, string stock_name, int amount, int sys_user_id)
        {
            if (string.IsNullOrWhiteSpace(stock_no) || string.IsNullOrWhiteSpace(stock_name) ||
                 amount <= 0 || sys_user_id <= 0)
                return "参数不全";

            IEnumerable<StockPlanModel> list = await DBHelper.GetFree<StockPlanModel>(
                string.Format("select * from stock_plan where stock_no='{0}' and (plan_status_id=3 or plan_status_id=4) and xrd_id=0", stock_no));

            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("stock_no", stock_no);
                    dict.Add("stock_name", stock_name);
                    dict.Add("amount", amount.ToString());
                    dict.Add("create_time", now);
                    dict.Add("finish_time", now);
                    dict.Add("cancel_time", now);
                    dict.Add("sys_user_id", sys_user_id.ToString());
                    if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }
                    ulong newId = await DBHelper.GetNewId(dbConnection, trans);

                    foreach (StockPlanModel plan in list)
                    {
                        int stock_amount = plan.stock_amount * (10 + amount) / 10;
                        decimal buy_price = plan.buy_price * 10 / (10 + amount);
                        decimal stop_earn_point = plan.stop_earn_percent * plan.money_debt / stock_amount / 100 + buy_price;
                        decimal stop_loss_point = buy_price - plan.stop_loss_money / stock_amount;

                        dict = new Dictionary<string, string>();
                        dict.Add("stock_amount", stock_amount.ToString());
                        dict.Add("stop_earn_point", stop_earn_point.ToString());
                        dict.Add("stop_loss_point", stop_loss_point.ToString());
                        dict.Add("buy_price", buy_price.ToString());
                        dict.Add("xrd_id", newId.ToString());
                        dict.Add("xrd_amount", plan.stock_amount.ToString());
                        dict.Add("xrd_price", plan.buy_price.ToString());
                        IDictionary<string, string> fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.id.ToString());
                        fdict.Add("plan_status_id", plan.plan_status_id.ToString());
                        fdict.Add("重复字段1重复字段xrd_id", "0");
                        if ((await DBHelper.Update(StockPlan.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "异常，请重试";
                        }
                    }

                    trans.Commit();
                }
            }
            
            foreach (StockPlanModel plan in list)
            {
                await StockPlan.DeleteRedis(plan.id);
            }

            return string.Empty;
        }
        public static async Task<string> Cancel(int id)
        {
            if (id <= 0) return "参数不全";
            StockPlanXrdModel xrd = await GetOne(id);
            if (xrd == null) return "读取不到MODEL";
            if (xrd.process_status_id != 3) return "状态异常";

            IEnumerable<StockPlanModel> list = await DBHelper.GetFree<StockPlanModel>(
               string.Format("select * from stock_plan where xrd_id={0}", id));

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("process_status_id", "-2");
                    dict.Add("cancel_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段process_status_id", "3");
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    foreach (StockPlanModel plan in list)
                    {
                        int stock_amount = plan.xrd_amount;
                        decimal buy_price = plan.xrd_price;
                        decimal stop_earn_point = plan.stop_earn_percent * plan.money_debt / stock_amount / 100 + buy_price;
                        decimal stop_loss_point = buy_price - plan.stop_loss_money / stock_amount;

                        dict = new Dictionary<string, string>();
                        dict.Add("stock_amount", stock_amount.ToString());
                        dict.Add("stop_earn_point", stop_earn_point.ToString());
                        dict.Add("stop_loss_point", stop_loss_point.ToString());
                        dict.Add("buy_price", buy_price.ToString());
                        dict.Add("xrd_id", "0");
                        dict.Add("xrd_amount", "0");
                        dict.Add("xrd_price", "0");
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", plan.id.ToString());
                        fdict.Add("plan_status_id", plan.plan_status_id.ToString());
                        fdict.Add("重复字段1重复字段xrd_id", id.ToString());
                        if ((await DBHelper.Update(StockPlan.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "异常，请重试";
                        }
                    }

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            foreach (StockPlanModel plan in list)
            {
                await StockPlan.DeleteRedis(plan.id);
            }

            return string.Empty;
        }
        public static async Task<StockPlanXrdModel> GetOne(int id)
        {
            if (id > 0) return await DBHelper.GetOne<StockPlanXrdModel>(id.ToString());
            else return null;
        }
        public static async Task<IEnumerable<StockPlanXrdModel>> GetList(string create_times, string create_timee,
            string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(create_times)) fdict.Add("create_time", ">=" + create_times);
            if (!string.IsNullOrWhiteSpace(create_timee)) fdict.Add("重复字段1重复字段create_time", "<=" + create_timee + " 23:59:59");
            return await DBHelper.GetList<StockPlanXrdModel, int>(TABLE, "*", "id desc", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));
        }
        public static async Task<long> GetCount(string create_times, string create_timee)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(create_times)) fdict.Add("create_time", ">=" + create_times);
            if (!string.IsNullOrWhiteSpace(create_timee)) fdict.Add("重复字段1重复字段create_time", "<=" + create_timee + " 23:59:59");
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task DeleteRedis(int id)
        {
            if (id <= 0) return;
            await Redis.Delete<StockPlanXrdModel>(id.ToString());
        }
        #endregion
    }
}
