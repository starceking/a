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
    public static class StockPlanLog
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        const string TABLE = "stock_plan_log";
        #endregion
        #region 一般操作
        public static async Task<int> Insert(ulong plan_id, string log_type, string info, int sys_user_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (plan_id <= 0 || string.IsNullOrWhiteSpace(log_type) || string.IsNullOrWhiteSpace(info)) return -1;
            if (log_type.Length > 10) log_type = log_type.Substring(0, 10);
            if (info.Length > 200) info = info.Substring(0, 200);

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("plan_id", plan_id.ToString());
            dict.Add("log_type", log_type);
            dict.Add("info", info);
            if (sys_user_id > 0) dict.Add("sys_user_id", sys_user_id.ToString());
            dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return (await DBHelper.Insert(TABLE, dict, dbConnection, trans));
        }
        public static int InsertSync(ulong plan_id, string log_type, string info, int sys_user_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (plan_id <= 0 || string.IsNullOrWhiteSpace(log_type) || string.IsNullOrWhiteSpace(info)) return -1;
            if (log_type.Length > 10) log_type = log_type.Substring(0, 10);
            if (info.Length > 200) info = info.Substring(0, 200);

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("plan_id", plan_id.ToString());
            dict.Add("log_type", log_type);
            dict.Add("info", info);
            if (sys_user_id > 0) dict.Add("sys_user_id", sys_user_id.ToString());
            dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return (DBHelper.InsertSync(TABLE, dict, dbConnection, trans));
        }
        public static async Task<IEnumerable<StockPlanLogModel>> GetList(string plan_id, string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(plan_id)) fdict.Add("plan_id", plan_id);
            return await DBHelper.GetList<StockPlanLogModel, long>(TABLE, "*", "id desc", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));
        }
        public static async Task<long> GetCount(string plan_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(plan_id)) fdict.Add("plan_id", plan_id);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        #endregion
    }
}
