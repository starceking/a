using Model;
using MySql.Data.MySqlClient;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class User
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        public const string TABLE = "user";
        /// <summary>
        /// 超级token
        /// </summary>
        const string SUPER_TOKEN = "LNDNA@.COM";
        /// <summary>
        /// 存储token
        /// </summary>
        static IDictionary<int, string> TOKEN_DICT = new Dictionary<int, string>();
        #endregion
        #region 一般操作
        /// <summary>
        /// 
        /// </summary>
        public static async Task<UserModel> Login(string police_no, string login_pwd)
        {
            if (string.IsNullOrWhiteSpace(police_no) || string.IsNullOrWhiteSpace(login_pwd)) return null;

            UserModel user = await GetOne(0, police_no);
            //委托用户可以进任意网站
            if (user != null && user.login_pwd.Equals(Md5.GetMd5(login_pwd)) &&
              (DictSettings.DeptIsCg(user.dept_no) || DictSettings.DeptIsSub(DictSettings.DEPT_NO, user.dept_no)))
                return user;
            return null;
        }
        public static async Task<string> Insert(string dept_no, string police_no, string login_pwd,
            string name, string id_card_no, string auth_ids, int user_id)
        {
            if (string.IsNullOrWhiteSpace(dept_no) || string.IsNullOrWhiteSpace(police_no) ||
                string.IsNullOrWhiteSpace(login_pwd) || string.IsNullOrWhiteSpace(auth_ids) || user_id <= 0)
                return "参数不全";
            if (await GetOne(0, police_no) != null) return "该警号已存在";
            UserModel user = await GetOne(user_id, string.Empty);
            if (user == null) return "读取不到USER";
            if (!DictSettings.DeptIsSub(user.dept_no, dept_no)) return "权限异常";
            if (DictSettings.DeptIsCg(dept_no)) auth_ids = DictSettings.CgUserAuths;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("dept_no", dept_no);
            dict.Add("police_no", police_no);
            dict.Add("login_pwd", Md5.GetMd5(login_pwd));
            if (!string.IsNullOrWhiteSpace(name)) dict.Add("name", name);
            if (!string.IsNullOrWhiteSpace(id_card_no)) dict.Add("id_card_no", id_card_no);
            if (!string.IsNullOrWhiteSpace(auth_ids)) dict.Add("auth_ids", auth_ids);
            if ((await DBHelper.Insert(TABLE, dict)) <= 0)
            {
                return "异常，请重试";
            }

            return string.Empty;
        }
        public static async Task<string> Update(int id, string dept_no, string police_no,
            string name, string id_card_no, string auth_ids, int user_id)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(dept_no) || string.IsNullOrWhiteSpace(police_no) ||
                string.IsNullOrWhiteSpace(auth_ids) || user_id <= 0)
                return "参数不全";
            UserModel user = await GetOne(id, string.Empty);
            if (user == null) return "读取不到USER";
            if (!user.police_no.Equals(police_no))
            {
                if (await GetOne(0, police_no) != null) return "该警号已存在";
            }
            UserModel userm = await GetOne(user_id, string.Empty);
            if (userm == null) return "读取不到USERM";
            if (!DictSettings.DeptIsSub(userm.dept_no, user.dept_no)) return "权限异常";
            if (!DictSettings.DeptIsSub(userm.dept_no, dept_no)) return "权限异常";
            if (DictSettings.DeptIsCg(dept_no)) auth_ids = DictSettings.CgUserAuths;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("dept_no", dept_no);
            dict.Add("police_no", police_no);
            if (!string.IsNullOrWhiteSpace(name)) dict.Add("name", name);
            if (!string.IsNullOrWhiteSpace(id_card_no)) dict.Add("id_card_no", id_card_no);
            if (!string.IsNullOrWhiteSpace(auth_ids)) dict.Add("auth_ids", auth_ids);
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "异常，请重试";

            await DeleteRedis(id);
            if (!user.police_no.Equals(police_no))
            {
                fdict = new Dictionary<string, string>();
                fdict.Add("police_no", user.police_no);
                string key = DBHelper.MakeMultiIdKey(TABLE, fdict, "and");
                await Redis.DeleteKey(key);
            }
            return string.Empty;
        }
        public static async Task<string> UpdatePwd(int id, string login_pwd, string login_pwd_old)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(login_pwd) || string.IsNullOrWhiteSpace(login_pwd_old))
                return "参数不全";
            if (login_pwd.Equals(login_pwd_old)) return "新老密码相同";
            UserModel model = await GetOne(id, string.Empty);
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
        public static async Task<string> ResetPwd(int id, string login_pwd, int user_id)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(login_pwd)) return "参数不全";
            UserModel user = await GetOne(id, string.Empty);
            if (user == null) return "读取不到USER";
            UserModel userm = await GetOne(user_id, string.Empty);
            if (userm == null) return "读取不到USERM";
            if (!DictSettings.DeptIsSub(userm.dept_no, user.dept_no)) return "权限异常";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("login_pwd", Md5.GetMd5(login_pwd));
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "异常，请重试";

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<string> Delete(int id, int user_id)
        {
            if (id <= 0) return "参数不全";
            UserModel user = await GetOne(id, string.Empty);
            if (user == null) return "读取不到USER";
            UserModel userm = await GetOne(user_id, string.Empty);
            if (userm == null) return "读取不到USERM";
            if (!DictSettings.DeptIsSub(userm.dept_no, user.dept_no)) return "权限异常";

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Delete(TABLE, fdict, "and")) <= 0) return "异常，请重试";

            await DeleteRedis(id);
            fdict = new Dictionary<string, string>();
            fdict.Add("police_no", user.police_no);
            string key = DBHelper.MakeMultiIdKey(TABLE, fdict, "and");
            await Redis.DeleteKey(key);
            return string.Empty;
        }
        public static async Task<UserModel> GetOne(int id, string police_no)
        {
            if (id > 0) return await DBHelper.GetOne<UserModel>(id.ToString());
            else if (!string.IsNullOrWhiteSpace(police_no))
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("police_no", police_no);
                return await DBHelper.GetOne<UserModel, int>(TABLE, fdict, "and");
            }
            else return null;
        }
        public static async Task<IEnumerable<UserModel>> GetList(string dept_no, string police_no, string name,
            string page_size, string page_index)
        {
            if (!DictSettings.DeptIsSub(DictSettings.DEPT_NO, dept_no)) return new List<UserModel>();

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(dept_no)) fdict.Add("dept_no", dept_no);
            if (!string.IsNullOrWhiteSpace(police_no)) fdict.Add("police_no", police_no);
            if (!string.IsNullOrWhiteSpace(name)) fdict.Add("name", name);
            return await DBHelper.GetList<UserModel, int>(TABLE, "*", "id desc", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));
        }
        public static async Task<long> GetCount(string dept_no, string police_no, string name)
        {
            if (!DictSettings.DeptIsSub(DictSettings.DEPT_NO, dept_no)) return 0;

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(dept_no)) fdict.Add("dept_no", dept_no);
            if (!string.IsNullOrWhiteSpace(police_no)) fdict.Add("police_no", police_no);
            if (!string.IsNullOrWhiteSpace(name)) fdict.Add("name", name);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task DeleteRedis(int id)
        {
            if (id <= 0) return;
            await Redis.Delete<UserModel>(id.ToString());
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

            if (TOKEN_DICT.ContainsKey(id)) return TOKEN_DICT[id].Equals(token);

            RedisValue rv = await Redis.GetString(GetTokenKey(id));
            return (rv.HasValue && rv.ToString().Equals(token));
        }
        static string GetTokenKey(int id)
        {
            return "SYSUSER_TOKEN_" + id;
        }
        #endregion
    }
}
