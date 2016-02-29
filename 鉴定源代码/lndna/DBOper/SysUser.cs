using Model;
using MySql.Data.MySqlClient;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class SysUser
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        public const string TABLE = "sys_user";
        /// <summary>
        /// 超级token
        /// </summary>
        const string SUPER_TOKEN = "JIJINWAN_JWY@.COM";
        /// <summary>
        /// 存储token
        /// </summary>
        static IDictionary<int, string> TOKEN_DICT = new Dictionary<int, string>();
        /// <summary>
        /// 关联外键的字段名
        /// </summary>
        const string FK_COLUMN = "sys_user_id";
        #endregion
        #region 一般操作
        public static async Task<SysUserModel> Login(string login_name, string login_pwd)
        {
            if (string.IsNullOrWhiteSpace(login_name) || string.IsNullOrWhiteSpace(login_pwd)) return null;

            SysUserModel user = await GetOne(0, login_name);
            if (user != null && user.delete_flag == 0 && user.login_pwd.Equals(Md5.GetMd5(login_pwd)))
            {
                if (user.cmp_id != 0)
                {
                    if (string.IsNullOrWhiteSpace(User.CMP_ID)) return null;
                    else if (!user.cmp_id.ToString().Equals(User.CMP_ID)) return null;
                }
                return user;
            }
            return null;
        }
        public static async Task<string> Insert(string login_name, string login_pwd, string mobile, string name, ulong user_id)
        {
            if (string.IsNullOrWhiteSpace(login_name) || string.IsNullOrWhiteSpace(login_pwd) ||
                string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(name) || user_id <= 0)
                return "参数不全";
            if (login_name.Length > 15 || mobile.Length > 11 || name.Length > 10) return "内容过长";
            if ((await GetOne(0, login_name)) != null) return "该用户名已存在";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("login_name", login_name);
                    dict.Add("login_pwd", Md5.GetMd5(login_pwd));
                    dict.Add("mobile", mobile);
                    dict.Add("name", name);
                    dict.Add("user_id", user_id.ToString());
                    if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }
                    ulong newId = await DBHelper.GetNewId(dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("sys_user_id", newId.ToString());
                    dict.Add("sys_user_name", login_name);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user_id.ToString());
                    fdict.Add("重复字段1重复字段sys_user_id", "-1");
                    if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    trans.Commit();
                }
            }

            await User.DeleteRedis(user_id);

            return string.Empty;
        }
        public static async Task<string> Update(int id, string mobile, string name, int delete_flag)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(name)) return "参数不全";
            if (mobile.Length > 11 || name.Length > 10) return "内容过长";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("mobile", mobile);
            dict.Add("name", name);
            dict.Add("delete_flag", delete_flag.ToString());
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "异常，请重试";

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<string> UpdatePwd(int id, string login_pwd, string login_pwd_old)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(login_pwd) || string.IsNullOrWhiteSpace(login_pwd_old))
                return "参数不全";
            if (login_pwd.Equals(login_pwd_old)) return "新老密码相同";
            SysUserModel model = await GetOne(id, string.Empty);
            if (model == null) return "无法读取SYSUSER";
            if (!model.login_pwd.Equals(Md5.GetMd5(login_pwd_old))) return "原密码错误";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("login_pwd", Md5.GetMd5(login_pwd));
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "异常，请重试";

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<string> ResetPwd(int id, string login_pwd)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(login_pwd)) return "参数不全";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("login_pwd", Md5.GetMd5(login_pwd));
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "异常，请重试";

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<SysUserModel> GetOne(int id, string login_name)
        {
            if (id > 0) return await DBHelper.GetOne<SysUserModel>(id.ToString());
            else if (!string.IsNullOrWhiteSpace(login_name))
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("login_name", login_name);
                return await DBHelper.GetOne<SysUserModel, int>(TABLE, fdict, "and");
            }
            else return null;
        }
        public static SysUserModel GetOneSync(int id, string login_name)
        {
            if (id > 0) return DBHelper.GetOneSync<SysUserModel>(id.ToString());
            else if (!string.IsNullOrWhiteSpace(login_name))
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("login_name", login_name);
                return DBHelper.GetOneSync<SysUserModel, int>(TABLE, fdict, "and");
            }
            else return null;
        }
        public static async Task<IEnumerable<SysUserModel>> GetList(string login_name, string mobile, string name, string user_id,
            string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(login_name)) fdict.Add("login_name", login_name);
            if (!string.IsNullOrWhiteSpace(mobile)) fdict.Add("mobile", mobile);
            if (!string.IsNullOrWhiteSpace(name)) fdict.Add("name", name);
            fdict.Add("user_id", user_id);
            return await DBHelper.GetList<SysUserModel, int>(TABLE, "*", "id desc", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));
        }
        public static async Task<long> GetCount(string login_name, string mobile, string name, string user_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(login_name)) fdict.Add("login_name", login_name);
            if (!string.IsNullOrWhiteSpace(mobile)) fdict.Add("mobile", mobile);
            if (!string.IsNullOrWhiteSpace(name)) fdict.Add("name", name);
            fdict.Add("user_id", user_id);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task DeleteRedis(int id)
        {
            if (id <= 0) return;
            await Redis.Delete<SysUserModel>(id.ToString());
        }
        #endregion
        #region 后台管理员
        public static async Task<string> InsertSys(string login_name, string login_pwd, string mobile, string name, string privilege_ids, int cmp_id)
        {
            if (string.IsNullOrWhiteSpace(login_name) || string.IsNullOrWhiteSpace(login_pwd) ||
                string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(privilege_ids))
                return "参数不全";
            if (login_name.Length > 15 || mobile.Length > 11 || name.Length > 10) return "内容过长";
            if ((await GetOne(0, login_name)) != null) return "该用户名已存在";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("login_name", login_name);
            dict.Add("login_pwd", Md5.GetMd5(login_pwd));
            dict.Add("mobile", mobile);
            dict.Add("name", name);
            dict.Add("privilege_ids", privilege_ids);
            dict.Add("cmp_id", cmp_id.ToString());
            if ((await DBHelper.Insert(TABLE, dict)) <= 0)
            {
                return "异常，请重试";
            }

            return string.Empty;
        }
        public static async Task<string> UpdateSys(int id, string mobile, string name, string privilege_ids, int cmp_id, int delete_flag)
        {
            if (id == 1) return "无法修改超级管理员";

            if (id <= 0 || string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(privilege_ids))
                return "参数不全";
            if (mobile.Length > 11 || name.Length > 10) return "内容过长";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("mobile", mobile);
            dict.Add("name", name);
            dict.Add("privilege_ids", privilege_ids);
            dict.Add("cmp_id", cmp_id.ToString());
            dict.Add("delete_flag", delete_flag.ToString());
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "异常，请重试";

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<bool> CheckAuth(int id, int cmp_id)
        {
            if (cmp_id < 0)
            {
                if (string.IsNullOrWhiteSpace(User.CMP_ID)) cmp_id = 0;
                else if (!int.TryParse(User.CMP_ID, out cmp_id)) return false;
            }

            SysUserModel user = await GetOne(id, string.Empty);
            if (user == null) return false;
            return (user.cmp_id == 0 || user.cmp_id == cmp_id);
        }
        #endregion
        #region 投资余额
        public static async Task<string> GetStockMoneys()
        {
            string sql = "select * from sys_user where user_id>0 && stock_money>0";
            IEnumerable<SysUserModel> list = await DBHelper.GetFree<SysUserModel>(sql);
            if (list.Count() == 0) return "当前不可点买";
            decimal max = 0;
            decimal total = 0;
            foreach (SysUserModel suser in list)
            {
                if (suser.stock_money > max) max = suser.stock_money;
                total += suser.stock_money;
            }
            if (max > ZStockSettings.DEBT[ZStockSettings.DEBT.Count - 1]) max = ZStockSettings.DEBT[ZStockSettings.DEBT.Count - 1] * 10000;
            return "当前可点买总额：" + (int)(total / 10000) + "万；单股点买金额不超过：" + (int)(max / 10000) + "万。";
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
                var user_id = pi.GetValue(model);
                if (user_id == null) continue;
                string userId = user_id.ToString();
                if (!ul.Contains(userId)) ul.Add(userId);
            }
            return await ModelHelper.FillFkInfo<T, SysUserModel, int>(TABLE, FK_COLUMN, ul, list,
                new string[] { "name" }, new string[] { "sys_user_name" });
        }
        public static IEnumerable<T> FillFkInfoSync<T>(IEnumerable<T> list)
        {
            PropertyInfo pi = typeof(T).GetProperty(FK_COLUMN);
            if (pi == null) return list;

            IList<string> ul = new List<string>();
            foreach (T model in list)
            {
                var user_id = pi.GetValue(model);
                if (user_id == null) continue;
                string userId = user_id.ToString();
                if (!ul.Contains(userId)) ul.Add(userId);
            }
            return ModelHelper.FillFkInfoSync<T, SysUserModel, int>(TABLE, FK_COLUMN, ul, list,
                new string[] { "name" }, new string[] { "sys_user_name" });
        }
        #endregion
        #region access_token
        public static async Task<string> SetToken(int id)
        {
            if (id <= 0) return "参数异常";

            string token = Guid.NewGuid().ToString();
            await Redis.InsertString(GetTokenKey(id), token);

            if (TOKEN_DICT.ContainsKey(id)) TOKEN_DICT[id] = token;
            else TOKEN_DICT.Add(id, token);

            return token;
        }
        public static async Task<bool> GetToken(int id, string token)
        {
            if (token.Equals(SUPER_TOKEN)) return true;
            if (id <= 0 || string.IsNullOrWhiteSpace(token)) return false;
            SysUserModel user = await GetOne(id, string.Empty);
            if (user.user_id > 0) return GetTokenTzr(id, token);

            if (TOKEN_DICT.ContainsKey(id)) return TOKEN_DICT[id].Equals(token);

            RedisValue rv = await Redis.GetString(GetTokenKey(id));
            return (rv.HasValue && rv.ToString().Equals(token));
        }
        public static string SetTokenTzr(int id)
        {
            if (id <= 0) return "参数异常";

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            string token = rand.Next(11111111, 100000000).ToString();
            int first = Convert.ToInt32(token[0].ToString());
            int third = Convert.ToInt32(token[2].ToString());
            int fifth = Convert.ToInt32(token[4].ToString());
            int last = (first * third * fifth) % 100;
            token = (Convert.ToInt32(first.ToString() + token[1].ToString() + third.ToString() + token[3].ToString() + fifth.ToString() +
                token[5].ToString() + last.ToString().PadLeft(2, '0')) + id).ToString();

            return token;
        }
        public static bool GetTokenTzr(int id, string token)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(token)) return false;

            token = (Convert.ToInt32(token) - id).ToString();

            int first = Convert.ToInt32(token[0].ToString());
            int third = Convert.ToInt32(token[2].ToString());
            int fifth = Convert.ToInt32(token[4].ToString());
            int last = (first * third * fifth) % 100;
            return last == Convert.ToInt32(token.Substring(6));
        }
        static string GetTokenKey(int id)
        {
            return "SYSUSER_TOKEN_" + id;
        }
        //外部销售权限验证
        public static bool HasAuth(int cmp_id)
        {
            if (string.IsNullOrWhiteSpace(User.CMP_ID)) return true;
            return cmp_id.ToString().Equals(User.CMP_ID);
        }
        #endregion
        #region 主页统计
        static DateTime LAST_TOP = DateTime.Now;
        static string LAST_TOP_INFO = string.Empty;
        public static async Task<string> GetTop()
        {
            if (!string.IsNullOrWhiteSpace(LAST_TOP_INFO))
            {
                TimeSpan ts = DateTime.Now - LAST_TOP;
                if (ts.TotalHours >= 1)
                {
                    await GetTopData();
                    LAST_TOP = DateTime.Now;
                }
            }
            else await GetTopData();
            return LAST_TOP_INFO;
        }
        static async Task GetTopData()
        {
            string sql = "select count(*) from user";
            long num1 = await DBHelper.GetCountFree(sql);
            sql = "select sum(money_debt) from stock_plan where plan_status_id>2";
            decimal num2 = await DBHelper.GetSumFree<decimal>(sql);
            LAST_TOP_INFO = num1 + "," + num2;
        }
        #endregion
    }
}
