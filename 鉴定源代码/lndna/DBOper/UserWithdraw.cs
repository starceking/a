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
    public static class UserWithdraw
    {
        #region 常量
        static readonly string TABLE = "user_withdraw";
        #endregion
        #region 一般操作
        public static async Task<string> Insert(ulong user_id, ulong user_bank_id, decimal money)
        {
            if (user_id <= 0 || user_bank_id <= 0 || money <= 0) return "参数不全";

            UserModel user = await User.GetOne(user_id, string.Empty, string.Empty, string.Empty, false);
            if (user == null) return "无法读取USER";
            if (string.IsNullOrWhiteSpace(user.id_card_no) || string.IsNullOrWhiteSpace(user.name))
                return "请先到账户安全里面进行实名认证";
            decimal final_money = user.money - money;
            if (final_money < 0) return "账户余额不足";
            if (user.money <= 10 && money < user.money) return "余额少于10元必须全部提取";
            UserBankModel ubm = await UserBank.GetOne(user_bank_id, 0);
            if (ubm == null) return "读取不到UBM";
            if (ubm.process_status_id != 3) return "该卡尚未通过审核";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("user_id", user_id.ToString());
                    dict.Add("user_bank_id", user_bank_id.ToString());
                    dict.Add("money", money.ToString());
                    dict.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dict.Add("finish_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
                    if ((await DBHelper.Insert(TABLE, dict, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }
                    ulong newId = await DBHelper.GetNewId(dbConnection, trans);

                    if ((await UserMoneyChange.Insert(user_id, 2, -money, final_money, "提款申请", TABLE, newId,
                         user.cmp_id, dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("money", "数字相减-" + money);
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", user_id.ToString());
                    fdict.Add("重复字段1重复字段money", ">=" + money);
                    if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        trans.Rollback();
                        return "SQL执行错误";
                    }

                    trans.Commit();
                }
            }

            await User.DeleteRedis(user.id);
            return string.Empty;
        }
        public static async Task<string> Finish(ulong id, int sys_user_id, int process_status_id, string remark)
        {
            if (id <= 0 || sys_user_id <= 0) return "参数不全";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) return "没有权限";

            UserWithdrawModel model = await GetOne(id);
            if (model == null) return "无法读取WITHDRAW";
            if (string.IsNullOrWhiteSpace(model.remark)) return "请在自动转账失败后执行该操作";
            if (model.process_status_id <= 0 || model.process_status_id > 2) return "状态异常";
            string process_status_ido = string.Empty;
            if (process_status_id == 2 || process_status_id == -1)
            {
                if (model.process_status_id != 1) return "状态不对";
                process_status_ido = "1";
            }
            else if (process_status_id == 3 || process_status_id == -2)
            {
                if (model.process_status_id != 2) return "状态不对";
                process_status_ido = "2";
            }

            UserModel user = null;
            if (process_status_id == -1 || process_status_id == -2)
            {
                user = await User.GetOne(model.user_id, string.Empty, string.Empty, string.Empty);
                if (user == null) return "无法读取USER";
            }

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("sys_user_id", sys_user_id.ToString());
                    dict.Add("process_status_id", process_status_id.ToString());
                    if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
                    if (process_status_id != 2) dict.Add("finish_time", DateTime.Now.ToString());
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段process_status_id", process_status_ido);
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0) return "SQL执行错误";

                    if (process_status_id == -1 || process_status_id == -2)
                    {
                        if ((await UserMoneyChange.Insert(model.user_id, 1, model.money, user.money + model.money, "提款失败",
                            TABLE, id, user.cmp_id, dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }

                        dict = new Dictionary<string, string>();
                        dict.Add("money", "数字相加+" + model.money);
                        fdict = new Dictionary<string, string>();
                        fdict.Add("id", model.user_id.ToString());
                        if ((await DBHelper.Update(User.TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                        {
                            trans.Rollback();
                            return "SQL执行错误";
                        }
                    }

                    trans.Commit();
                }
            }

            if (process_status_id == -1 || process_status_id == -2)
                await User.DeleteRedis(model.user_id);
            await DeleteRedis(id);
            return string.Empty;
        }
        static async Task<UserWithdrawModel> GetOne(ulong id)
        {
            if (id <= 0) return null;
            return await DBHelper.GetOne<UserWithdrawModel>(id.ToString());
        }
        public static UserWithdrawModel GetOneSync(ulong id)
        {
            if (id <= 0) return null;
            UserWithdrawModel uwm = DBHelper.GetOneSync<UserWithdrawModel>(id.ToString());

            if (uwm != null)
            {
                UserModel user = User.GetOneSync(uwm.user_id, string.Empty, string.Empty, string.Empty);
                if (user != null)
                {
                    if (!string.IsNullOrWhiteSpace(user.name))
                        uwm.user_name = user.name;
                    if (!string.IsNullOrWhiteSpace(user.mobile))
                        uwm.user_mobile = user.mobile;
                }
                UserBankModel ubm = UserBank.GetOneSync(uwm.user_bank_id);
                if (ubm != null)
                {
                    uwm.branch_name = ubm.branch_name;
                    uwm.card_no = ubm.card_no;

                    uwm.bank_name = DictBank.GetName(ubm.bank_id);
                    uwm.province_name = DictDistrict.GetName(ubm.province_id);
                    uwm.city_name = DictDistrict.GetName(ubm.city_id).Replace(uwm.province_name, string.Empty);
                }
            }

            return uwm;
        }
        public static async Task<IEnumerable<UserWithdrawModel>> GetList(string user_id, string sys_user_id, string process_status_id,
            string create_times, string create_timee, string finish_times, string finish_timee, string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            if (!string.IsNullOrWhiteSpace(sys_user_id)) fdict.Add("sys_user_id", sys_user_id);
            if (!string.IsNullOrWhiteSpace(process_status_id)) fdict.Add("process_status_id", process_status_id);
            if (!string.IsNullOrWhiteSpace(create_times)) fdict.Add("create_time", ">=" + create_times);
            if (!string.IsNullOrWhiteSpace(create_timee)) fdict.Add("重复字段1重复字段create_time", "<=" + create_timee + " 23:59:59");
            if (!string.IsNullOrWhiteSpace(finish_times)) fdict.Add("finish_time", ">=" + finish_times);
            if (!string.IsNullOrWhiteSpace(finish_timee)) fdict.Add("重复字段1重复字段finish_time", "<=" + finish_timee + " 23:59:59");
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            IEnumerable<UserWithdrawModel> list = DBHelper.GetListSync<UserWithdrawModel, long>(TABLE, "*", "id desc", fdict, "and",
                 Convert.ToInt32(page_size), Convert.ToInt32(page_index));
            list = await User.FillFkInfo(list);

            foreach (UserWithdrawModel uwm in list)
            {
                UserBankModel ubm = await UserBank.GetOne(uwm.user_bank_id, 0);
                if (ubm != null)
                {
                    uwm.branch_name = ubm.branch_name;
                    uwm.card_no = ubm.card_no;

                    uwm.bank_name = DictBank.GetName(ubm.bank_id);
                    uwm.province_name = DictDistrict.GetName(ubm.province_id);
                    uwm.city_name = DictDistrict.GetName(ubm.city_id).Replace(uwm.province_name, string.Empty);
                }
            }

            return list;
        }
        public static async Task<long> GetCount(string user_id, string sys_user_id, string process_status_id,
            string create_times, string create_timee, string finish_times, string finish_timee)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            if (!string.IsNullOrWhiteSpace(sys_user_id)) fdict.Add("sys_user_id", sys_user_id);
            if (!string.IsNullOrWhiteSpace(process_status_id)) fdict.Add("process_status_id", process_status_id);
            if (!string.IsNullOrWhiteSpace(create_times)) fdict.Add("create_time", ">=" + create_times);
            if (!string.IsNullOrWhiteSpace(create_timee)) fdict.Add("重复字段1重复字段create_time", "<=" + create_timee + " 23:59:59");
            if (!string.IsNullOrWhiteSpace(finish_times)) fdict.Add("finish_time", ">=" + finish_times);
            if (!string.IsNullOrWhiteSpace(finish_timee)) fdict.Add("重复字段1重复字段finish_time", "<=" + finish_timee + " 23:59:59");
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", User.CMP_ID);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<string> Exp(string user_id, string sys_user_id, string process_status_id,
            string create_times, string create_timee, string finish_times, string finish_timee)
        {
            IDictionary<string, string> cols = new Dictionary<string, string>();
            cols.Add("*收款方姓名", "user_name");
            cols.Add("*收款方银行账号", "card_no");
            cols.Add("*开户行所在省", "province_name");
            cols.Add("*开户行所在市", "city_name");
            cols.Add("*开户行名称", "branch_name");
            cols.Add("*收款方银行名称", "bank_name");
            cols.Add("*金额", "money");
            cols.Add("申请时间", "create_time");
            cols.Add("状态(1表示未处理)", "process_status_id");
            cols.Add("用户id", "user_id");
            IEnumerable<UserWithdrawModel> list = await GetList(user_id, sys_user_id, process_status_id, create_times,
                    create_timee, finish_times, finish_timee, "0", "0");

            return Excel.MakeXmlForExcel(list, cols);
        }
        static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<UserWithdrawModel>(id.ToString());
        }
        static void DeleteRedisSync(ulong id)
        {
            if (id <= 0) return;
            Redis.DeleteSync<UserWithdrawModel>(id.ToString());
        }
        #endregion
        #region 自动转账
        public static void AutoTrans(ulong id, string code, string msg, string mobile, decimal money)
        {
            string remark = (msg + "[" + code + "]");
            if (remark.Length > 200) remark = remark.Substring(0, 200);
            IDictionary<string, string> dict = new Dictionary<string, string>();
            if (code.Equals("0000"))
            {
                dict.Add("process_status_id", "3");
                dict.Add("finish_time", DateTime.Now.ToString());
            }
            if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
            if (dict.Count > 0)
            {
                IDictionary<string, string> fdict = new Dictionary<string, string>();
                fdict.Add("id", id.ToString());
                DBHelper.UpdateSync(TABLE, dict, fdict, "and");
            }
            DeleteRedisSync(id);

            if (code.Equals("0000"))
            {
                if (!string.IsNullOrWhiteSpace(mobile))
                    Mobile.SendSmsSync(mobile, "你的提款[" + money + "]元已交易成功，请及时查收。");
            }
        }
        #endregion
    }
}
