using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class HsiAccount
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        public const string TABLE = "hsi_account";
        /// <summary>
        /// 关联外键的字段名
        /// </summary>
        const string FK_COLUMN = "hsi_account_id";
        /// <summary>
        /// 
        /// </summary>
        static string JSON = string.Empty;
        #endregion
        #region 一般操作
        public static async Task<string> Insert(int src_id, int calc_id, string number)
        {
            if (src_id <= 0 || string.IsNullOrWhiteSpace(number)) return "参数不全";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("src_id", src_id.ToString());
            dict.Add("calc_id", calc_id.ToString());
            dict.Add("number", number);
            await DBHelper.Insert(TABLE, dict);

            JSON = string.Empty;
            return string.Empty;
        }
        public static async Task<string> Update(int id, int src_id, int calc_id, string number)
        {
            if (id <= 0 || src_id <= 0 || string.IsNullOrWhiteSpace(number)) return "参数不全";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("src_id", src_id.ToString());
            dict.Add("calc_id", calc_id.ToString());
            dict.Add("number", number);
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            await DBHelper.Update(TABLE, dict, fdict, "and");

            await DeleteRedis(id);
            JSON = string.Empty;
            return string.Empty;
        }
        public static async Task<string> Delete(int id)
        {
            if (id <= 0) return "参数不全";
            HsiAccountModel ham = await GetOne(id);
            if (ham == null) return "读取不到MODEL";
            if (ham.sub_amount > 0) return "有子帐号关联，不可删除";

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            await DBHelper.Delete(TABLE, fdict, "and");

            await DeleteRedis(id);
            JSON = string.Empty;
            return string.Empty;
        }
        public static async Task<IEnumerable<HsiAccountModel>> GetList(string src_id, string number, string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(src_id)) fdict.Add("src_id", src_id);
            if (!string.IsNullOrWhiteSpace(number)) fdict.Add("number", number);
            return await DBHelper.GetList<HsiAccountModel, int>(TABLE, "*", "id", fdict, "and",
                        Convert.ToInt32(page_size), Convert.ToInt32(page_index));
        }
        public static async Task<long> GetCount(string src_id, string number)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(src_id)) fdict.Add("src_id", src_id);
            if (!string.IsNullOrWhiteSpace(number)) fdict.Add("number", number);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<HsiAccountModel> GetOne(int id)
        {
            if (id <= 0) return null;
            return await DBHelper.GetOne<HsiAccountModel>(id.ToString());
        }
        public static async Task DeleteRedis(int id)
        {
            if (id <= 0) return;
            await Redis.Delete<HsiAccountModel>(id.ToString());
        }
        public static async Task<string> GetJson()
        {
            if (!string.IsNullOrWhiteSpace(JSON)) return JSON;

            IEnumerable<HsiAccountModel> list = await GetList("", "", "0", "0");
            string json = string.Empty;
            foreach (HsiAccountModel model in list)
            {
                json += ",{ 'id':'" + model.id + "','name':'" + model.number + "'}";
            }
            if (json.Length > 0) json = json.Substring(1);
            json = "{ 'Head':[ " + json + " ]}";

            JSON = json;
            return json;
        }
        #endregion
        #region 关联表信息读取
        public static async Task<IEnumerable<T>> FillFkInfo<T>(IEnumerable<T> list)
        {
            PropertyInfo pi = typeof(T).GetProperty(FK_COLUMN);
            if (pi == null) return list;

            IList<string> ul = new List<string>();
            foreach (T model in list)
            {
                var id = pi.GetValue(model);
                if (id == null) continue;
                string idstr = id.ToString();
                if (!ul.Contains(idstr)) ul.Add(idstr);
            }
            return await ModelHelper.FillFkInfo<T, HsiAccountModel, int>(TABLE, FK_COLUMN, ul, list,
                new string[] { "number" }, new string[] { "hsi_account_number" });
        }
        #endregion
    }
}
