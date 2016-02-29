using Model;
using MySql.Data.MySqlClient;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
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
        /// 关联外键的字段名
        /// </summary>
        const string FK_COLUMN = "user_id";
        //外部销售团队
        public static readonly string CMP_ID = ConfigurationManager.AppSettings["CmpId"];
        #endregion
        #region 一般操作
        public static async Task<UserModel> Register(string mobile, string code, string login_pwd, ulong ref_id)
        {
            if (string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(login_pwd))
                return new UserModel { id = ulong.MinValue, nick_name = "参数不全" };
            if (mobile.Length != 11 || code.Length != 4) return new UserModel { id = ulong.MinValue, nick_name = "参数异常" };
            if (!await Mobile.GetCode(mobile, code)) return new UserModel { id = ulong.MinValue, nick_name = "验证码错误" };
            UserModel user = await GetOne(0, mobile, string.Empty, string.Empty);
            if (user != null) return user;

            ulong newId = 0;
            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    if (await ZSqlUnique.Insert("user_mobile", "mobile", mobile, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return new UserModel { id = ulong.MinValue, nick_name = "异常，请重试" };
                    }

                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("login_pwd", Md5.GetMd5(login_pwd));
                    dict.Add("mobile", mobile);
                    dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (ref_id > 0) dict.Add("ref_id", ref_id.ToString());
                    if (!string.IsNullOrWhiteSpace(CMP_ID)) dict.Add("cmp_id", CMP_ID);
                    if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return new UserModel { id = ulong.MinValue, nick_name = "异常，请重试" };
                    }

                    newId = await DBHelper.GetNewId(dbConnection, trans);

                    trans.Commit();
                }
            }

            return new UserModel { id = newId, mobile = mobile, money = 0 };
        }
        public static async Task<UserModel> RegisterNm(string nick_name, string login_pwd, string mobile, string code, ulong ref_id)
        {
            if (string.IsNullOrWhiteSpace(nick_name) || string.IsNullOrWhiteSpace(login_pwd) || string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(code))
                return new UserModel { id = ulong.MinValue, nick_name = "参数不全" };
            nick_name = nick_name.Trim();
            if (!NickName.CanIns(nick_name)) return new UserModel { id = ulong.MinValue, nick_name = "用户名包含非法字符" };
            if (nick_name.Length > 15) return new UserModel { id = ulong.MinValue, nick_name = "内容过长" };
            if (await GetOne(0, string.Empty, nick_name, string.Empty) != null)
                return new UserModel { id = ulong.MinValue, nick_name = "该昵称已存在" };
            if (mobile.Length != 11 || code.Length != 4) return new UserModel { id = ulong.MinValue, nick_name = "参数异常" };
            if (!await Mobile.GetCode(mobile, code)) return new UserModel { id = ulong.MinValue, nick_name = "验证码错误" };
            UserModel user = await GetOne(0, mobile, string.Empty, string.Empty);
            if (user != null) return new UserModel { id = ulong.MinValue, nick_name = "该手机号已存在" };

            ulong newId = 0;
            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    if (await ZSqlUnique.Insert("user_nick_name", "nick_name", nick_name, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return new UserModel { id = ulong.MinValue, nick_name = "异常，请重试" };
                    }
                    if (await ZSqlUnique.Insert("user_mobile", "mobile", mobile, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return new UserModel { id = ulong.MinValue, nick_name = "异常，请重试" };
                    }

                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("login_pwd", Md5.GetMd5(login_pwd));
                    dict.Add("nick_name", nick_name);
                    dict.Add("mobile", mobile);
                    dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (ref_id > 0) dict.Add("ref_id", ref_id.ToString());
                    if (!string.IsNullOrWhiteSpace(CMP_ID)) dict.Add("cmp_id", CMP_ID);
                    if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return new UserModel { id = ulong.MinValue, nick_name = "异常，请重试" };
                    }

                    newId = await DBHelper.GetNewId(dbConnection, trans);

                    trans.Commit();
                }
            }

            return new UserModel { id = newId, nick_name = nick_name, mobile = mobile, money = 0 };
        }
        public static async Task<string> UpdateMobile(ulong id, string mobile, string code, string code_old)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(code))
                return "参数不全";
            if (mobile.Length != 11 || code.Length != 4) return "参数异常";
            if (!await Mobile.GetCode(mobile, code)) return "验证码错误";
            UserModel user = await GetOne(id, string.Empty, string.Empty, string.Empty, false, false);
            if (user == null) return "读取不到USER";
            if (!string.IsNullOrWhiteSpace(user.mobile))
            {
                if (user.mobile.Equals(mobile)) return string.Empty;
                if (string.IsNullOrWhiteSpace(code_old)) return "参数不全";
                if (!await Mobile.GetCode(user.mobile, code_old)) return "验证码错误";
            }
            if (await GetOne(0, mobile, string.Empty, string.Empty, false, false) != null) return "该手机号已存在";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    if (await ZSqlUnique.Insert("user_mobile", "mobile", mobile, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }
                    if (!string.IsNullOrWhiteSpace(user.mobile))
                    {
                        if (await ZSqlUnique.Delete("user_mobile", "mobile", user.mobile, dbConnection, trans) <= 0)
                        {
                            trans.Rollback();
                            return "异常，请重试";
                        }
                    }

                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("mobile", mobile);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0) return "异常，请重试";

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            if (!string.IsNullOrWhiteSpace(user.mobile))
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("mobile", user.mobile);
                string key = DBHelper.MakeMultiIdKey(TABLE, fdict, "and");
                await Redis.DeleteKey(key);
            }
            return string.Empty;
        }
        public static async Task<string> UpdateNickName(ulong id, string nick_name)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(nick_name))
                return "参数不全";
            nick_name = nick_name.Trim();
            if (nick_name.Length > 15) return "内容过长";
            UserModel user = await GetOne(id, string.Empty, string.Empty, string.Empty, false, false);
            if (user == null) return "读取不到USER";
            if (!string.IsNullOrWhiteSpace(user.nick_name))
            {
                if (user.nick_name.Equals(nick_name)) return string.Empty;
            }
            if (await GetOne(0, string.Empty, nick_name, string.Empty, false, false) != null) return "该昵称已存在";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    if (await ZSqlUnique.Insert("user_nick_name", "nick_name", nick_name, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }
                    if (!string.IsNullOrWhiteSpace(user.nick_name))
                    {
                        if (await ZSqlUnique.Delete("user_nick_name", "nick_name", user.nick_name, dbConnection, trans) <= 0)
                        {
                            trans.Rollback();
                            return "异常，请重试";
                        }
                    }

                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("nick_name", nick_name);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            if (!string.IsNullOrWhiteSpace(user.nick_name))
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("nick_name", user.nick_name);
                string key = DBHelper.MakeMultiIdKey(TABLE, fdict, "and");
                await Redis.DeleteKey(key);
            }
            return string.Empty;
        }
        public static async Task<string> UpdateIdInfo(ulong id, string name, string id_card_no)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(id_card_no)) return "参数不全";
            if (name.Length > 50 || id_card_no.Length > 50) return "参数异常";
            UserModel user = await GetOne(id, string.Empty, string.Empty, string.Empty, false, false);
            if (user == null) return "读取不到USER";
            UserModel user2 = await GetOne(0, string.Empty, string.Empty, id_card_no, false, false);
            if (user2 != null && user2.id != id) return "该身份证已存在";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    if (string.IsNullOrWhiteSpace(user.id_card_no))
                    {
                        if (await ZSqlUnique.Insert("user_id_card_no", "id_card_no", id_card_no, dbConnection, trans) <= 0)
                        {
                            trans.Rollback();
                            return "异常，请重试";
                        }
                    }
                    else if (!user.id_card_no.Equals(id_card_no))
                    {
                        if (await ZSqlUnique.Insert("user_id_card_no", "id_card_no", id_card_no, dbConnection, trans) <= 0)
                        {
                            trans.Rollback();
                            return "异常，请重试";
                        }
                        if (await ZSqlUnique.Delete("user_id_card_no", "id_card_no", user.id_card_no, dbConnection, trans) <= 0)
                        {
                            trans.Rollback();
                            return "异常，请重试";
                        }
                    }

                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("name", name);
                    dict.Add("id_card_no", id_card_no);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0) return "异常，请重试";

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            if (!string.IsNullOrWhiteSpace(user.id_card_no) && !user.id_card_no.Equals(id_card_no))
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("id_card_no", user.id_card_no);
                string key = DBHelper.MakeMultiIdKey(TABLE, fdict, "and");
                await Redis.DeleteKey(key);
            }
            return string.Empty;
        }
        public static async Task<string> UpdatePwd(ulong id, string login_pwd, string login_pwd_old)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(login_pwd) || string.IsNullOrWhiteSpace(login_pwd_old)) return "参数不全";
            if (login_pwd.Equals(login_pwd_old)) return "新老密码相同";
            UserModel model = await GetOne(id, string.Empty, string.Empty, string.Empty, false, false);
            if (model == null) return "无法读取USER";
            if (!model.login_pwd.Equals(Md5.GetMd5(login_pwd_old))) return "原密码错误";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("login_pwd", Md5.GetMd5(login_pwd));
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "异常，请重试";

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<string> UpdatePwdMobile(string mobile, string code, string login_pwd)
        {
            if (string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(login_pwd))
                return "参数不全";
            UserModel user = await GetOne(0, mobile, string.Empty, string.Empty, false, false);
            if (user == null) return "无法读取USER";
            if (!(await Mobile.GetCode(user.mobile, code))) return "验证码错误";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("login_pwd", Md5.GetMd5(login_pwd));
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", user.id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "异常，请重试";

            await DeleteRedis(user.id);
            return string.Empty;
        }
        public static async Task<string> ResetPwd(ulong id, string login_pwd)
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
        public static async Task<string> UpdateRef(ulong id, decimal profit_from_ref, ulong ref_id, int delete_flag)
        {
            if (id <= 0) return "参数不全";
            if (id == ref_id) return "不可邀请自己";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("profit_from_ref", profit_from_ref.ToString());
            dict.Add("ref_id", ref_id.ToString());
            dict.Add("delete_flag", delete_flag.ToString());
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if (!string.IsNullOrWhiteSpace(CMP_ID)) fdict.Add("cmp_id", CMP_ID);
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "异常，请重试";

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<IEnumerable<UserModel>> GetList(string id, string mobile, string nick_name, string name,
            string id_card_no, string create_times, string create_timee, string ref_id,
            string moneys, string moneye, string cmp_id, string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(id)) fdict.Add("id", id);
            if (!string.IsNullOrWhiteSpace(mobile)) fdict.Add("mobile", mobile + "%");
            if (!string.IsNullOrWhiteSpace(nick_name)) fdict.Add("nick_name", nick_name + "%");
            if (!string.IsNullOrWhiteSpace(name)) fdict.Add("name", name + "%");
            if (!string.IsNullOrWhiteSpace(id_card_no)) fdict.Add("id_card_no", id_card_no + "%");
            if (!string.IsNullOrWhiteSpace(create_times)) fdict.Add("create_time", ">=" + create_times);
            if (!string.IsNullOrWhiteSpace(create_timee)) fdict.Add("重复字段1重复字段create_time", "<=" + create_timee + " 23:59:59");
            if (!string.IsNullOrWhiteSpace(ref_id)) fdict.Add("ref_id", ref_id);
            if (!string.IsNullOrWhiteSpace(CMP_ID)) fdict.Add("cmp_id", CMP_ID);
            else if (!string.IsNullOrWhiteSpace(cmp_id)) fdict.Add("cmp_id", cmp_id);
            if (!string.IsNullOrWhiteSpace(moneys)) fdict.Add("money", ">=" + moneys);
            if (!string.IsNullOrWhiteSpace(moneye)) fdict.Add("重复字段1重复字段money", "<=" + moneye);
            IEnumerable<UserModel> list = await DBHelper.GetList<UserModel, long>(TABLE, "*", "id desc", fdict, "and",
                      Convert.ToInt32(page_size), Convert.ToInt32(page_index));
            foreach (UserModel user in list)
            {
                if (user.ref_id > 0)
                {
                    UserModel ruser = await DBHelper.GetOne<UserModel>(user.ref_id.ToString());
                    if (ruser != null)
                    {
                        if (!string.IsNullOrWhiteSpace(ruser.name)) user.ref_info = "(" + ruser.name + ")";
                        else if (!string.IsNullOrWhiteSpace(ruser.nick_name)) user.ref_info = "(" + ruser.nick_name + ")";
                        else if (!string.IsNullOrWhiteSpace(ruser.mobile)) user.ref_info = "(" + ruser.mobile + ")";
                    }
                }
            }
            return list;
        }
        public static async Task<long> GetCount(string id, string mobile, string nick_name, string name,
            string id_card_no, string create_times, string create_timee, string ref_id, string moneys, string moneye, string cmp_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(id)) fdict.Add("id", id);
            if (!string.IsNullOrWhiteSpace(mobile)) fdict.Add("mobile", mobile + "%");
            if (!string.IsNullOrWhiteSpace(nick_name)) fdict.Add("nick_name", nick_name + "%");
            if (!string.IsNullOrWhiteSpace(name)) fdict.Add("name", name + "%");
            if (!string.IsNullOrWhiteSpace(id_card_no)) fdict.Add("id_card_no", id_card_no + "%");
            if (!string.IsNullOrWhiteSpace(create_times)) fdict.Add("create_time", ">=" + create_times);
            if (!string.IsNullOrWhiteSpace(create_timee)) fdict.Add("重复字段1重复字段create_time", "<=" + create_timee + " 23:59:59");
            if (!string.IsNullOrWhiteSpace(ref_id)) fdict.Add("ref_id", ref_id);
            if (!string.IsNullOrWhiteSpace(CMP_ID)) fdict.Add("cmp_id", CMP_ID);
            else if (!string.IsNullOrWhiteSpace(cmp_id)) fdict.Add("cmp_id", cmp_id);
            if (!string.IsNullOrWhiteSpace(moneys)) fdict.Add("money", ">=" + moneys);
            if (!string.IsNullOrWhiteSpace(moneye)) fdict.Add("重复字段1重复字段money", "<=" + moneye);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<string> Exp(string id, string mobile, string nick_name, string name,
            string id_card_no, string create_times, string create_timee, string ref_id, string moneys, string moneye, string cmp_id)
        {
            IDictionary<string, string> cols = new Dictionary<string, string>();
            cols.Add("用户ID", "id");
            cols.Add("电话", "mobile");
            cols.Add("昵称", "nick_name");
            cols.Add("姓名", "name");
            cols.Add("身份证", "id_card_no");
            cols.Add("余额", "money");
            cols.Add("欠费", "money_npay");
            cols.Add("佣金", "profit_from_ref");
            cols.Add("邀请人", "ref_info");
            cols.Add("点买额", "stock_plan_debt");
            cols.Add("点买数", "stock_plan_amount");
            cols.Add("状态", "delete_flag");
            IEnumerable<UserModel> list = await GetList(id, mobile, nick_name, name, id_card_no, create_times, create_timee,
                ref_id, moneys, moneye, cmp_id, "0", "0");
            return Excel.MakeXmlForExcel(list, cols);
        }
        public static async Task<UserModel> GetOne(ulong id, string mobile, string nick_name, string id_card_no, bool ref_info = false, bool cmp = true)
        {
            UserModel user = null;
            if (id > 0) user = await DBHelper.GetOne<UserModel>(id.ToString());
            else
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(mobile)) fdict.Add("mobile", mobile);
                else if (!string.IsNullOrWhiteSpace(nick_name)) fdict.Add("nick_name", nick_name);
                else if (!string.IsNullOrWhiteSpace(id_card_no)) fdict.Add("id_card_no", id_card_no);
                else user = null;
                user = await DBHelper.GetOne<UserModel, long>(TABLE, fdict, "and");
            }
            if (user != null && user.ref_id > 0 && ref_info)
            {
                UserModel ruser = await DBHelper.GetOne<UserModel>(user.ref_id.ToString());
                if (ruser != null)
                {
                    if (!string.IsNullOrWhiteSpace(ruser.name)) user.ref_info = "(" + ruser.name + ")";
                    else if (!string.IsNullOrWhiteSpace(ruser.nick_name)) user.ref_info = "(" + ruser.nick_name + ")";
                    else if (!string.IsNullOrWhiteSpace(ruser.mobile)) user.ref_info = "(" + ruser.mobile + ")";
                }
            }
            if (user != null)
            {
                user.stock_plan_debt_delay = user.stock_plan_debt * ZStockSettings.DEFER_FEE / 10000;
                //欠费补交
                decimal um = await UserMoneyChange.MoneyNPay(user);
                decimal delta = user.money - um;
                user.money = um;
                user.money_npay -= delta;
                //
                if (cmp && (!string.IsNullOrWhiteSpace(CMP_ID)))
                {
                    if (!user.cmp_id.ToString().Equals(CMP_ID)) return null;
                }
            }
            return user;
        }
        public static UserModel GetOneSync(ulong id, string mobile, string nick_name, string id_card_no)
        {
            UserModel user = null;
            if (id > 0) user = DBHelper.GetOneSync<UserModel>(id.ToString());
            else
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(mobile)) fdict.Add("mobile", mobile);
                else if (!string.IsNullOrWhiteSpace(nick_name)) fdict.Add("nick_name", nick_name);
                else if (!string.IsNullOrWhiteSpace(id_card_no)) fdict.Add("id_card_no", id_card_no);
                else return null;
                user = DBHelper.GetOneSync<UserModel, long>(TABLE, fdict, "and");
            }
            if (user != null)
            {
                //欠费补交
                decimal um = UserMoneyChange.MoneyNPaySync(user);
                decimal delta = user.money - um;
                user.money = um;
                user.money_npay -= delta;
                //
            }
            return user;
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<UserModel>(id.ToString());
        }
        public static void DeleteRedisSync(ulong id)
        {
            if (id <= 0) return;
            Redis.DeleteSync<UserModel>(id.ToString());
        }
        public static string GetUserInfo(UserModel user)
        {
            if (user == null) return string.Empty;

            string userInfo = string.Empty;
            if (!string.IsNullOrWhiteSpace(user.nick_name)) userInfo = user.nick_name;
            else if (!string.IsNullOrWhiteSpace(user.mobile)) userInfo = Mobile.HideMobile(user.mobile);
            return userInfo;
        }
        public static async Task<string> UpdateBatchRef(ulong id, string refIds)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(refIds)) return "参数异常";
            bool cmp = (!string.IsNullOrWhiteSpace(CMP_ID));

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            string[] ids = refIds.Split(',');
            foreach (string uid in ids)
            {
                if (!string.IsNullOrWhiteSpace(uid))
                {
                    ulong uidi = 0;
                    if (ulong.TryParse(uid, out uidi))
                    {
                        if (cmp)
                        {
                            UserModel user = await GetOne(uidi, string.Empty, string.Empty, string.Empty);
                            if (user == null) return "权限异常";
                        }
                        fdict.Add("重复字段" + uid + "重复字段id", uid);
                    }
                    else return "参数异常";
                }
            }
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ref_id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "or")) <= 0) return "异常，请重试";

            foreach (string uid in ids)
            {
                ulong uidi = 0;
                if (ulong.TryParse(uid, out uidi))
                    await DeleteRedis(uidi);
            }

            return string.Empty;
        }
        #endregion
        #region 关联表信息读取
        public static async Task<IEnumerable<T>> FillFkInfo<T>(IEnumerable<T> list, bool cmp = true)
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
            return await ModelHelper.FillFkInfo<T, UserModel, long>(TABLE, FK_COLUMN, ul, list,
                new string[] { "nick_name", "mobile", "name" }, new string[] { "user_nick_name", "user_mobile", "user_name" }, cmp);
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
            return ModelHelper.FillFkInfoSync<T, UserModel, long>(TABLE, FK_COLUMN, ul, list,
                new string[] { "nick_name", "mobile", "name" }, new string[] { "user_nick_name", "user_mobile", "user_name" });
        }
        #endregion
        #region 记录ip
        const string USER_IP = "User_ip";
        const string USER_IP_CS = "User_ip_cs";
        public static async Task InsertIp(string ip)
        {
            bool ncmp = string.IsNullOrWhiteSpace(CMP_ID);
            string USER_IP_key = ncmp ? USER_IP : (USER_IP + CMP_ID);
            string USER_IP_CS_key = ncmp ? USER_IP_CS : (USER_IP_CS + CMP_ID);

            await Redis.InsertSet(USER_IP_key, ip);
            await Redis.InsertNumber(USER_IP_CS_key, 1);
        }
        public static void ClearIp()
        {
            bool ncmp = string.IsNullOrWhiteSpace(CMP_ID);
            string USER_IP_key = ncmp ? USER_IP : (USER_IP + CMP_ID);
            string USER_IP_CS_key = ncmp ? USER_IP_CS : (USER_IP_CS + CMP_ID);

            Redis.DeleteKeySync(USER_IP_key);
            Redis.DeleteKeySync(USER_IP_CS_key);
        }
        public static int GetIpAmount()
        {
            bool ncmp = string.IsNullOrWhiteSpace(CMP_ID);
            string USER_IP_key = ncmp ? USER_IP : (USER_IP + CMP_ID);

            return Redis.GetSetSync(USER_IP_key).Length;
        }
        public static int GetIpCs()
        {
            bool ncmp = string.IsNullOrWhiteSpace(CMP_ID);
            string USER_IP_CS_key = ncmp ? USER_IP_CS : (USER_IP_CS + CMP_ID);

            RedisValue rv = Redis.GetStringSync(USER_IP_CS_key);
            if (rv.HasValue)
            {
                int cs = 0;
                if (int.TryParse(rv.ToString(), out cs))
                {
                    return cs;
                }
            }
            return 0;
        }
        #endregion
        #region 主页统计
        static DateTime LAST_TOP = DateTime.Now;
        static IEnumerable<UserModel> LAST_TOP_LIST = null;
        public static async Task<IEnumerable<UserModel>> GetTop()
        {
            if (LAST_TOP_LIST != null)
            {
                TimeSpan ts = DateTime.Now - LAST_TOP;
                if (ts.TotalHours >= 1)
                {
                    await GetTopData();
                    LAST_TOP = DateTime.Now;
                }
            }
            else await GetTopData();
            return LAST_TOP_LIST;
        }
        static async Task GetTopData()
        {
            string sql = "select * from user where stock_plan_earn+hsi_plan_profit>0 order by stock_plan_earn+hsi_plan_profit desc limit 0,7";
            LAST_TOP_LIST = await DBHelper.GetFree<UserModel>(sql);
            foreach (UserModel user in LAST_TOP_LIST)
            {
                if (!string.IsNullOrWhiteSpace(user.nick_name))
                {
                    if (user.nick_name.Length == 11)
                        user.nick_name = (user.nick_name.Substring(0, 4) + "****" + user.nick_name.Substring(8));
                }
                if (!string.IsNullOrWhiteSpace(user.mobile))
                {
                    if (user.mobile.Length == 11)
                        user.mobile = (user.mobile.Substring(0, 4) + "****" + user.mobile.Substring(8));
                }
            }
        }
        #endregion
    }
}
