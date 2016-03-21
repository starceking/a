using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    /// <summary>
    /// 对照样本
    /// </summary>
    public static class IdComparison
    {
        public const string TABLE = "id_comparison";

        public static async Task<string> Insert(string number, string name, int amount, string ref_table,
            ulong ref_id, int user_id)
        {
            if (string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(name) || amount <= 0 ||
                string.IsNullOrWhiteSpace(ref_table) || ref_id <= 0)
                return "参数不全";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("number", number);
            dict.Add("name", name);
            dict.Add("amount", amount.ToString());
            dict.Add("ref_table", ref_table);
            dict.Add("ref_id", ref_id.ToString());
            dict.Add("user_id", user_id.ToString());
            if ((await DBHelper.Insert(TABLE, dict)) <= 0)
            {
                return "异常，请重试";
            }
            return string.Empty;
        }
        public static async Task<string> Update(ulong id, int amount, int user_id)
        {
            if (amount <= 0) return "参数不全";
            IdComparisonModel icm = await GetOne(id);
            if (icm == null) return "读取不到MODEL";
            if (icm.user_id != user_id) return "权限不足";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("amount", amount.ToString());
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            fdict.Add("user_id", user_id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0)
            {
                return "异常，请重试";
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<string> Delete(ulong id, int user_id)
        {
            if (id <= 0) return "参数不全";
            IdComparisonModel icm = await GetOne(id);
            if (icm == null) return "读取不到MODEL";
            if (icm.user_id != user_id) return "权限不足";

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            fdict.Add("user_id", user_id.ToString());
            if ((await DBHelper.Delete(TABLE, fdict, "and")) <= 0)
            {
                return "异常，请重试";
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<IEnumerable<IdComparisonModel>> GetList(string ref_table, string ref_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(ref_table)) fdict.Add("ref_table", ref_table);
            if (!string.IsNullOrWhiteSpace(ref_id)) fdict.Add("ref_id", ref_id);
            return await DBHelper.GetList<IdComparisonModel, long>(TABLE, "*", "id", fdict, "and");
        }
        public static async Task<IdComparisonModel> GetOne(ulong id)
        {
            return await DBHelper.GetOne<IdComparisonModel>(id.ToString());
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<IdComparisonModel>(id.ToString());
        }
    }
}
