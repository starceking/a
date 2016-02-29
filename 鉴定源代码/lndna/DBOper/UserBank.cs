using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class UserBank
    {
        #region 常量
        static readonly string TABLE = "user_bank";
        #endregion
        #region 一般操作
        public static async Task<string> Insert(ulong user_id, int bank_id, int province_id, int city_id, int district_id,
            string branch_name, string card_no, int default_flag)
        {
            if (user_id <= 0 || bank_id <= 0 || province_id <= 0 || city_id <= 0 ||
                string.IsNullOrWhiteSpace(branch_name) || string.IsNullOrWhiteSpace(card_no))
                return "参数不全";
            if (branch_name.Length > 100 || card_no.Length > 50) return "内容过长";
            if (district_id <= 0) district_id = -1;

            UserModel user = await User.GetOne(user_id, string.Empty, string.Empty, string.Empty, false);
            if (user == null) return "读取不到USER";

            if (await DBHelper.GetCountFree("select count(*) from user_bank where user_id=" + user_id) > 0)
                return "仅限添加一张银行卡";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("user_id", user_id.ToString());
            dict.Add("bank_id", bank_id.ToString());
            dict.Add("province_id", province_id.ToString());
            dict.Add("city_id", city_id.ToString());
            dict.Add("district_id", district_id.ToString());
            dict.Add("branch_name", branch_name);
            dict.Add("card_no", card_no);
            dict.Add("default_flag", default_flag.ToString());
            if (user.cmp_id > 0) dict.Add("cmp_id", user.cmp_id.ToString());
            if ((await DBHelper.Insert(TABLE, dict)) <= 0) return "SQL执行错误";

            return string.Empty;
        }
        public static async Task<string> Update(ulong id, int bank_id, int province_id, int city_id, int district_id,
            string branch_name, string card_no, int default_flag, ulong user_id)
        {
            if (id <= 0 || user_id <= 0 || bank_id <= 0 || province_id <= 0 || city_id <= 0 ||
                string.IsNullOrWhiteSpace(branch_name) || string.IsNullOrWhiteSpace(card_no))
                return "参数不全";
            if (branch_name.Length > 100 || card_no.Length > 50) return "内容过长";
            if (district_id <= 0) district_id = -1;
            UserBankModel ubm = await GetOne(id, user_id);
            if (ubm == null) return "读取不到BANK";
            if (ubm.process_status_id == 3) return "已审核通过的卡无法修改";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("bank_id", bank_id.ToString());
            dict.Add("province_id", province_id.ToString());
            dict.Add("city_id", city_id.ToString());
            dict.Add("district_id", district_id.ToString());
            dict.Add("branch_name", branch_name);
            dict.Add("card_no", card_no);
            dict.Add("default_flag", default_flag.ToString());
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "SQL执行错误";

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<string> Delete(ulong id, ulong user_id)
        {
            if (id <= 0) return "参数不全";
            UserBankModel ubm = await GetOne(id, user_id);
            if (ubm == null) return "读取不到BANK";
            if (ubm.process_status_id == 3) return "已审核通过的卡无法删除";

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Delete(TABLE, fdict, "and")) <= 0) return "SQL执行错误";

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<UserBankModel> GetOne(ulong id, ulong user_id)
        {
            if (id <= 0) return null;

            UserBankModel ubm = await DBHelper.GetOne<UserBankModel>(id.ToString());
            if (user_id > 0)
            {
                if (ubm.user_id != user_id) return null;
            }
            return ubm;
        }
        public static UserBankModel GetOneSync(ulong id)
        {
            if (id <= 0) return null;

            return DBHelper.GetOneSync<UserBankModel>(id.ToString());
        }
        public static async Task<IEnumerable<UserBankModel>> GetList(ulong user_id)
        {
            if (user_id <= 0) return new List<UserBankModel>();

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("user_id", user_id.ToString());
            return await DBHelper.GetList<UserBankModel, Int64>(TABLE, "*", "id", fdict, "and");
        }
        static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<UserBankModel>(id.ToString());
        }
        #endregion
        #region 管理员
        public static async Task<string> Audit(ulong id, int process_status_id)
        {
            if (id <= 0) return "参数不全";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("process_status_id", process_status_id.ToString());
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0) return "SQL执行错误";

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<IEnumerable<UserBankModel>> GetListAudit(string user_id, string process_status_id, string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            if (!string.IsNullOrWhiteSpace(process_status_id)) fdict.Add("process_status_id", process_status_id);
            if (string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", "0");
            else fdict.Add("cmp_id", User.CMP_ID);
            IEnumerable<UserBankModel> list = await DBHelper.GetList<UserBankModel, long>(TABLE, "*", "id desc", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));
            return await User.FillFkInfo(list);
        }
        public static async Task<long> GetCountAudit(string user_id, string process_status_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(user_id)) fdict.Add("user_id", user_id);
            if (!string.IsNullOrWhiteSpace(process_status_id)) fdict.Add("process_status_id", process_status_id);
            if (string.IsNullOrWhiteSpace(User.CMP_ID)) fdict.Add("cmp_id", "0");
            else fdict.Add("cmp_id", User.CMP_ID);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        #endregion
    }
}
