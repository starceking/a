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
    /// <summary>
    /// 预实验
    /// </summary>
    public static class IdPre
    {
        public const string TABLE = "id_pre";

        public static async Task<string> Insert(string number, string name, int user_id, string shelf_type,
            DateTime id_day, string sampleIds)
        {
            if (user_id <= 0 || string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(shelf_type) || id_day == null || string.IsNullOrWhiteSpace(sampleIds))
                return "参数不全";
            UserModel user = await User.GetOne(user_id, string.Empty);
            if (user == null) return "读取不到USER";
            if (!user.dept_no.Equals(DictSettings.LAB_NO)) return "权限异常";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("number", number);
                    dict.Add("name", name);
                    dict.Add("user_id", user_id.ToString());
                    dict.Add("shelf_type", shelf_type);
                    dict.Add("id_day", id_day.ToShortDateString());
                    if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                    {
                        return "异常，请重试";
                    }
                    ulong newId = await DBHelper.GetNewId(dbConnection, trans);

                    foreach (string sid in sampleIds.Split(','))
                    {
                        ulong sample_id = 0;
                        if (ulong.TryParse(sid, out sample_id))
                            await IdPreSample.Insert(newId, sample_id, user_id, dbConnection, trans);
                    }

                    trans.Commit();
                }
            }

            return string.Empty;
        }
        public static async Task<string> Update(ulong id, string number, string name, string shelf_type,
            DateTime id_day, int oper_id)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(shelf_type) || id_day == null)
                return "参数不全";
            IdPreModel ipm = await GetOne(id);
            if (ipm == null) return "读取不到MODEL";
            if (ipm.user_id != oper_id) return "权限异常";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("number", number);
            dict.Add("name", name);
            dict.Add("shelf_type", shelf_type);
            dict.Add("id_day", id_day.ToShortDateString());
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            fdict.Add("user_id", oper_id.ToString());
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
            IdPreModel ipm = await GetOne(id);
            if (ipm == null) return "读取不到MODEL";
            if (ipm.user_id != user_id) return "权限异常";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("user_id", user_id.ToString());
                    if ((await DBHelper.Delete(TABLE, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        return "异常，请重试";
                    }

                    await IdPreSample.Delete(0, id, dbConnection, trans);

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<IEnumerable<IdPreModel>> GetList(string number, string user_id, string id_days, string id_daye,
            string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(number)) fdict.Add("number", number);
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            if (!string.IsNullOrWhiteSpace(id_days)) fdict.Add("id_day", ">=" + id_days);
            if (!string.IsNullOrWhiteSpace(id_daye)) fdict.Add("重复字段1重复字段id_day", "<=" + id_daye);
            return await DBHelper.GetList<IdPreModel, long>(TABLE, "*", "id", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));
        }
        public static async Task<long> GetCount(string number, string user_id, string id_days, string id_daye)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(number)) fdict.Add("number", number);
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            if (!string.IsNullOrWhiteSpace(id_days)) fdict.Add("id_day", ">=" + id_days);
            if (!string.IsNullOrWhiteSpace(id_daye)) fdict.Add("重复字段1重复字段id_day", "<=" + id_daye);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<IdPreModel> GetOne(ulong id)
        {
            return await DBHelper.GetOne<IdPreModel>(id.ToString());
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<IdPreModel>(id.ToString());
        }
    }
}
