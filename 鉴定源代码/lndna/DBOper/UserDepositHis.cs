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
    public static class UserDepositHis
    {
        #region 常量
        /// <summary>
        /// 数据库表
        /// </summary>
        const string TABLE = "user_deposit_his";
        #endregion
        #region 一般操作
        /// <summary>
        /// 人工充值
        /// </summary>
        public static async Task<string> Deposit(ulong user_id, decimal money, string info, string number, int deposit_src_id, int sys_user_id)
        {
            if (sys_user_id != 4) return "权限不足";

            if (user_id <= 0 || money == 0 || string.IsNullOrWhiteSpace(number)) return "参数不全";
            if (info.Length > 200 || number.Length > 50) return "内容过长";
            if (string.IsNullOrWhiteSpace(info)) info = "充值";
            UserDepositHisModel udhm = await GetOne(0, number);
            if (udhm != null) return string.Empty;

            UserModel user = await User.GetOne(user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return "无法读取USER";
            decimal final_money = user.money + money;
            string money_flow_id = (money > 0 ? "1" : "2");

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("number", number);
                    dict.Add("deposit_src_id", deposit_src_id.ToString());
                    dict.Add("user_id", user_id.ToString());
                    dict.Add("money", money.ToString());
                    if (!string.IsNullOrWhiteSpace(info)) dict.Add("info", info);
                    dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("sys_user_id", sys_user_id.ToString());
                    if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                    if (await DBHelper.Insert(TABLE, dict, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    if (await UserMoneyChange.Insert(user_id, 1, money, final_money, info, number, Convert.ToUInt64(deposit_src_id),
                         user.cmp_id, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    dict = new Dictionary<string, string>();
                    if (money > 0) dict.Add("money", "数字相加+" + money);
                    else dict.Add("money", "数字相减-" + (-money));
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user_id.ToString());
                    if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    trans.Commit();
                }
            }

            await User.DeleteRedis(user.id);
            if (user.money_npay > 0)
            {
                await UserMoneyChange.MoneyNPay(await User.GetOne(user_id, string.Empty, string.Empty, string.Empty));
            }
            return string.Empty;
        }
        /// <summary>
        /// 支付接口充值
        /// </summary>
        public static string DepositSync(ulong user_id, decimal money, string info, string number, int deposit_src_id, int sys_user_id)
        {
            if (user_id <= 0 || money <= 0 || string.IsNullOrWhiteSpace(number)) return "参数不全";
            if (info.Length > 200 || number.Length > 50) return "内容过长";
            if (string.IsNullOrWhiteSpace(info)) info = "充值";
            UserDepositHisModel udhm = GetOneSync(0, number);
            if (udhm != null) return string.Empty;

            UserModel user = User.GetOneSync(user_id, string.Empty, string.Empty, string.Empty);
            if (user == null) return "无法读取USER";
            decimal final_money = user.money + money;
            string money_flow_id = (money > 0 ? "1" : "2");

            using (MySqlConnection dbConnection = DBHelper.GetOpenConSync())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("number", number);
                    dict.Add("deposit_src_id", deposit_src_id.ToString());
                    dict.Add("user_id", user_id.ToString());
                    dict.Add("money", money.ToString());
                    if (!string.IsNullOrWhiteSpace(info)) dict.Add("info", info);
                    dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("sys_user_id", sys_user_id.ToString());
                    if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                    if (DBHelper.InsertSync(TABLE, dict, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    if (UserMoneyChange.InsertSync(user_id, 1, money, final_money, info, number,
                        Convert.ToUInt64(deposit_src_id), user.cmp_id, dbConnection, trans) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    dict = new Dictionary<string, string>();
                    if (money > 0) dict.Add("money", "数字相加+" + money);
                    else dict.Add("money", "数字相减-" + (-money));
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user_id.ToString());
                    if ((DBHelper.UpdateSync(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "异常，请重试";
                    }

                    trans.Commit();
                }
            }

            User.DeleteRedisSync(user.id);
            if (user.money_npay > 0)
            {
                UserMoneyChange.MoneyNPaySync(User.GetOneSync(user_id, string.Empty, string.Empty, string.Empty));
            }
            return string.Empty;
        }
        public static async Task<IEnumerable<UserDepositHisModel>> GetList(string number, string deposit_src_id, string user_id,
         string create_times, string create_timee, string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(number)) fdict.Add("number", number);
            if (!string.IsNullOrWhiteSpace(deposit_src_id)) fdict.Add("deposit_src_id", deposit_src_id);
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            if (!string.IsNullOrWhiteSpace(create_times)) fdict.Add("create_time", ">=" + create_times);
            if (!string.IsNullOrWhiteSpace(create_timee)) fdict.Add("重复字段1重复字段create_time", "<=" + create_timee + " 23:59:59");
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            IEnumerable<UserDepositHisModel> list = await DBHelper.GetList<UserDepositHisModel, long>(TABLE, "*", "id desc", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));

            return await User.FillFkInfo(list);
        }
        public static async Task<long> GetCount(string number, string deposit_src_id, string user_id,
            string create_times, string create_timee)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(number)) fdict.Add("number", number);
            if (!string.IsNullOrWhiteSpace(deposit_src_id)) fdict.Add("deposit_src_id", deposit_src_id);
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            if (!string.IsNullOrWhiteSpace(create_times)) fdict.Add("create_time", ">=" + create_times);
            if (!string.IsNullOrWhiteSpace(create_timee)) fdict.Add("重复字段1重复字段create_time", "<=" + create_timee + " 23:59:59");
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<string> Exp(string number, string deposit_src_id, string user_id,
            string create_times, string create_timee)
        {
            IDictionary<string, string> cols = new Dictionary<string, string>();
            cols.Add("id", "id");
            cols.Add("客户ID", "user_id");
            cols.Add("昵称", "user_nick_name");
            cols.Add("电话", "user_mobile");
            cols.Add("充值额", "money");
            cols.Add("时间", "create_time");
            cols.Add("备注", "info");
            IEnumerable<UserDepositHisModel> list = await GetList(number, deposit_src_id,
                    user_id, create_times, create_timee, "0", "0");

            return Excel.MakeXmlForExcel(list, cols);
        }
        public static async Task<UserDepositHisModel> GetOne(long id, string number)
        {
            if (id > 0) return await DBHelper.GetOne<UserDepositHisModel>(id.ToString());
            else if (!string.IsNullOrWhiteSpace(number))
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("number", number);
                return await DBHelper.GetOne<UserDepositHisModel, Int64>(TABLE, fdict, "and");
            }
            else return null;
        }
        public static UserDepositHisModel GetOneSync(long id, string number)
        {
            if (id > 0) return DBHelper.GetOneSync<UserDepositHisModel>(id.ToString());
            else if (!string.IsNullOrWhiteSpace(number))
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("number", number);
                return DBHelper.GetOneSync<UserDepositHisModel, long>(TABLE, fdict, "and");
            }
            else return null;
        }
        #endregion
    }
}
