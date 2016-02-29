using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class ZSqlUnique
    {
        #region 一般操作
        public static async Task<int> Insert(string table, string column, string value,
            MySqlConnection dbConnection, MySqlTransaction trans)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add(column, value);
            return await DBHelper.Insert(table, dict, dbConnection, trans);
        }
        public static int InsertSync(string table, string column, string value,
            MySqlConnection dbConnection, MySqlTransaction trans)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add(column, value);
            return DBHelper.InsertSync(table, dict, dbConnection, trans);
        }
        public static async Task<int> Delete(string table, string column, string value,
            MySqlConnection dbConnection, MySqlTransaction trans)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add(column, value);
            return await DBHelper.Delete(table, fdict, "and", dbConnection, trans);
        }
        #endregion
    }
}
