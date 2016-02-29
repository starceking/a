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
    public static class SysUserStockMoneyChange
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        const string TABLE = "sys_user_stock_money_change";
        #endregion
        #region 一般操作
        public static async Task<string> Update(int user_id, decimal money, string info, string ref_table, ulong ref_id, int sys_user_id)
        {
            if (user_id <= 0 || money == 0) return "参数不全";
            if (info.Length > 200 || ref_table.Length > 50) return "内容过长";

            SysUserModel user = await SysUser.GetOne(user_id, string.Empty);
            if (user == null) return "无法读取USER";
            decimal final_money = user.stock_money + money;
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
                    if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    dict = new Dictionary<string, string>();
                    if (money > 0) dict.Add("stock_money", "数字相加+" + money);
                    else dict.Add("stock_money", "数字相减-" + (-money));
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user_id.ToString());
                    if ((await DBHelper.Update(SysUser.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    trans.Commit();
                }
            }

            await SysUser.DeleteRedis(user.id);
            return string.Empty;
        }
        public static async Task<int> Insert(int user_id, int money_flow_id, decimal money, decimal final_money, string info,
            string ref_table, ulong ref_id, string stock_no, string stock_name, MySqlConnection dbConnection, MySqlTransaction trans)
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
            if (!string.IsNullOrWhiteSpace(stock_no)) dict.Add("stock_no", stock_no);
            if (!string.IsNullOrWhiteSpace(stock_name)) dict.Add("stock_name", stock_name);
            return (await DBHelper.Insert(TABLE, dict, dbConnection, trans));
        }
        public static int InsertSync(int user_id, int money_flow_id, decimal money, decimal final_money, string info,
            string ref_table, ulong ref_id, string stock_no, string stock_name, MySqlConnection dbConnection, MySqlTransaction trans)
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
            if (!string.IsNullOrWhiteSpace(stock_no)) dict.Add("stock_no", stock_no);
            if (!string.IsNullOrWhiteSpace(stock_name)) dict.Add("stock_name", stock_name);
            return (DBHelper.InsertSync(TABLE, dict, dbConnection, trans));
        }
        public static async Task<IEnumerable<SysUserStockMoneyChangeModel>> GetList(string user_id, string money_flow_id,
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
            return await DBHelper.GetList<SysUserStockMoneyChangeModel, long>(TABLE, "*", "id desc", fdict, "and",
                    Convert.ToInt32(page_size), Convert.ToInt32(page_index));
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
            cols.Add("投资人ID", "user_id");
            cols.Add("流向", "money_flow_id");
            cols.Add("变动实盘资金", "money");
            cols.Add("最终实盘资金", "final_money");
            cols.Add("时间", "create_time");
            cols.Add("备注", "info");
            IEnumerable<SysUserStockMoneyChangeModel> list = await GetList(user_id, money_flow_id,
                    ref_table, ref_id, create_times, create_timee, "0", "0");

            return Excel.MakeXmlForExcel(list, cols);
        }
        #endregion
    }
}
