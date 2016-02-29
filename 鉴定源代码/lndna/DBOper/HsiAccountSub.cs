using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class HsiAccountSub
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        public const string TABLE = "hsi_account_sub";
        /// <summary>
        /// 关联外键的字段名
        /// </summary>
        const string FK_COLUMN = "hsi_account_sub_id";
        #endregion
        #region 一般操作
        public static async Task<string> Insert(int account_id, string number, string pwd)
        {
            if (account_id <= 0 || string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(pwd)) return "参数不全";
            if (await GetOne(0, number) != null) return "子帐号已存在";

            ulong newId = 0;
            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("account_id", account_id.ToString());
                    dict.Add("number", number);
                    dict.Add("pwd", pwd);
                    dict.Add("last_plan_date", DateTime.Today.ToString("yyyy-MM-dd"));
                    if (await DBHelper.Insert(TABLE, dict, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }
                    newId = await DBHelper.GetNewId(dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("sub_amount", "数字相加+1");
                    dict.Add("sub_free", "数字相加+1");
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", account_id.ToString());
                    if (await DBHelper.Update(HsiAccount.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    trans.Commit();
                }
            }
            await HsiAccount.DeleteRedis(account_id);
            InsFreeId(Convert.ToInt32(newId));
            return string.Empty;
        }
        public static async Task<string> Update(int id, string number, string pwd)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(pwd)) return "参数不全";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("number", number);
            dict.Add("pwd", pwd);
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            await DBHelper.Update(TABLE, dict, fdict, "and");

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<string> Delete(int id)
        {
            if (id <= 0) return "参数不全";
            HsiAccountSubModel model = await GetOne(id, string.Empty);
            if (model.user_id > 0)
            {
                if (model.last_plan_date > DateTime.Today.AddDays(-7)) return "不满足解绑条件";
            }

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if (model.user_id > 0)
                        fdict.Add("last_plan_date", "<=" + DateTime.Today.AddDays(-7).ToShortDateString());
                    await DBHelper.Delete(TABLE, fdict, "and");

                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("sub_amount", "数字相减-1");
                    if (model.user_id > 0)
                        dict.Add("sub_used", "数字相减-1");
                    else dict.Add("sub_free", "数字相减-1");
                    fdict = new Dictionary<string, string>();
                    fdict.Add("id", model.account_id.ToString());
                    if (await DBHelper.Update(HsiAccount.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    if (model.user_id > 0)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("hsi_account_sub_id", "0");
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", model.user_id.ToString());
                        fdict.Add("重复字段1重复字段hsi_account_sub_id", id.ToString());
                        if (await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    trans.Commit();
                }
            }
            await HsiAccount.DeleteRedis(model.account_id);
            await DeleteRedis(id);
            if (model.user_id > 0) await User.DeleteRedis(model.user_id);
            return string.Empty;
        }
        public static async Task<IEnumerable<HsiAccountSubModel>> GetList(string account_id, string number, string user_mobile, string page_size, string page_index)
        {
            string user_id = string.Empty;
            if (!string.IsNullOrWhiteSpace(user_mobile))
            {
                UserModel user = await User.GetOne(0, user_mobile, string.Empty, string.Empty);
                if (user != null) user_id = user.id.ToString();
            }

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(account_id)) fdict.Add("account_id", account_id);
            if (!string.IsNullOrWhiteSpace(number)) fdict.Add("number", number);
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            IEnumerable<HsiAccountSubModel> list = await DBHelper.GetList<HsiAccountSubModel, int>(TABLE, "*", "id", fdict, "and",
                        Convert.ToInt32(page_size), Convert.ToInt32(page_index));
            return await User.FillFkInfo(list);
        }
        public static async Task<long> GetCount(string account_id, string number, string user_mobile)
        {
            string user_id = string.Empty;
            if (!string.IsNullOrWhiteSpace(user_mobile))
            {
                UserModel user = await User.GetOne(0, user_mobile, string.Empty, string.Empty);
                if (user != null) user_id = user.id.ToString();
            }

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(account_id)) fdict.Add("account_id", account_id);
            if (!string.IsNullOrWhiteSpace(number)) fdict.Add("number", number);
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<HsiAccountSubModel> GetOne(int id, string number)
        {
            if (id > 0) return await DBHelper.GetOne<HsiAccountSubModel>(id.ToString());
            else
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("number", number);
                return await DBHelper.GetOne<HsiAccountSubModel, int>(TABLE, fdict, "and");
            }
        }
        public static async Task DeleteRedis(int id)
        {
            if (id <= 0) return;
            await Redis.Delete<HsiAccountSubModel>(id.ToString());
        }
        #endregion
        #region 获取空闲id
        static IList<int> freeList;
        static object freeLock = new object();
        public static void Init()
        {
            if (freeList == null)
            {
                freeList = new List<int>();
                IEnumerable<HsiAccountSubModel> list = DBHelper.GetFreeSync<HsiAccountSubModel>("select * from hsi_account_sub where user_id=0");
                foreach (HsiAccountSubModel model in list)
                {
                    freeList.Add(model.id);
                }
            }
        }
        public static int GetFreeId()
        {
            if (freeList.Count == 0) return 0;
            if (freeList.Count < 11)
            {
                Mobile.SendSmsSync(Mobile.ADMIN_MOBILE, "当前恒指系统子账户少于10个，请及时补充子账号信息");
            }
            lock (freeLock)
            {
                int id = freeList[0];
                freeList.Remove(id);
                return id;
            }
        }
        public static void InsFreeId(int id)
        {
            if (id <= 0) return;
            lock (freeLock)
            {
                freeList.Add(id);
            }
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
            return await ModelHelper.FillFkInfo<T, HsiAccountSubModel, int>(TABLE, FK_COLUMN, ul, list,
                new string[] { "number", "pwd" }, new string[] { "hsi_account_sub_number", "hsi_account_sub_pwd" });
        }
        #endregion
    }
}
