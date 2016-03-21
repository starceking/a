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
    /// 样本
    /// </summary>
    public static class CaseSample
    {
        public const string TABLE = "case_sample";

        public static async Task<string> Insert(ulong case_info_id, string number, string name, string sample_type, string description,
            string remark, string ref_table, ulong ref_id, int user_id)
        {
            if (case_info_id <= 0 || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(sample_type) ||
                string.IsNullOrWhiteSpace(ref_table) || ref_id <= 0)
                return "参数不全";
            string ck = await CaseInfo.CheckAuth(case_info_id, user_id);
            if (!string.IsNullOrWhiteSpace(ck)) return ck;
            CaseInfoModel cim = await CaseInfo.GetOne(case_info_id);
            if (cim == null) return "读取不到CIM";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("case_info_id", case_info_id.ToString());
            dict.Add("id_status_id", cim.case_status_id.ToString());
            if (cim.case_status_id > 2)
                dict.Add("accept_user_id", cim.accept_user_id.ToString());
            if (!string.IsNullOrWhiteSpace(number)) dict.Add("number", number);
            dict.Add("name", name);
            dict.Add("sample_type", sample_type);
            if (!string.IsNullOrWhiteSpace(description)) dict.Add("description", description);
            if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
            dict.Add("ref_table", ref_table);
            dict.Add("ref_id", ref_id.ToString());
            if ((await DBHelper.Insert(TABLE, dict)) <= 0)
            {
                return "异常，请重试";
            }
            return string.Empty;
        }
        public static async Task<string> Update(ulong id, string number, string name, string sample_type, string description,
            string remark, int user_id)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(sample_type))
                return "参数不全";
            CaseSampleModel csm = await GetOne(id);
            if (csm == null) return "读取不到CSM";
            string ck = await CaseInfo.CheckAuth(csm.case_info_id, user_id);
            if (!string.IsNullOrWhiteSpace(ck)) return ck;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(number)) dict.Add("number", number);
            dict.Add("name", name);
            dict.Add("sample_type", sample_type);
            if (!string.IsNullOrWhiteSpace(description)) dict.Add("description", description);
            if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
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
            CaseSampleModel csm = await GetOne(id);
            if (csm == null) return "读取不到CSM";
            string ck = await CaseInfo.CheckAuth(csm.case_info_id, user_id);
            if (!string.IsNullOrWhiteSpace(ck)) return ck;

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Delete(TABLE, fdict, "and")) <= 0)
            {
                return "异常，请重试";
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        /// <summary>
        /// 受理
        /// </summary>
        public static async Task<string> Accept(ulong id, int id_status_id, string accept_remark)
        {
            if (id <= 0) return "参数不全";
            CaseSampleModel csm = await GetOne(id);
            if (csm == null) return "读取不到CASE";
            if (csm.id_status_id != 2) return "状态异常";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("id_status_id", id_status_id.ToString());
            if (!string.IsNullOrWhiteSpace(accept_remark)) dict.Add("accept_remark", accept_remark);
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            fdict.Add("重复字段1重复字段id_status_id", "2");
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0)
            {
                return "异常，请重试";
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        /// <summary>
        /// 获取下一个样本编号
        /// </summary>
        static async Task<string> GetNumber(ulong case_info_id, string ref_table)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        public static async Task<IEnumerable<CaseSampleModel>> GetList(string case_info_id, string number, string sample_type,
            string id_status_id, string ref_table, string ref_id, string accept_user_id,
            string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(case_info_id)) fdict.Add("case_info_id", case_info_id);
            if (!string.IsNullOrWhiteSpace(number)) fdict.Add("number", number);
            if (!string.IsNullOrWhiteSpace(sample_type)) fdict.Add("sample_type", sample_type);
            if (!string.IsNullOrWhiteSpace(id_status_id)) fdict.Add("id_status_id", id_status_id);
            if (!string.IsNullOrWhiteSpace(ref_table)) fdict.Add("ref_table", ref_table);
            if (!string.IsNullOrWhiteSpace(ref_id)) fdict.Add("ref_id", ref_id);
            if (!string.IsNullOrWhiteSpace(accept_user_id)) fdict.Add("accept_user_id", accept_user_id);
            return await DBHelper.GetList<CaseSampleModel, long>(TABLE, "*", "id", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));
        }
        public static async Task<long> GetCount(string case_info_id, string number, string sample_type,
            string id_status_id, string ref_table, string ref_id, string accept_user_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(case_info_id)) fdict.Add("case_info_id", case_info_id);
            if (!string.IsNullOrWhiteSpace(number)) fdict.Add("number", number);
            if (!string.IsNullOrWhiteSpace(sample_type)) fdict.Add("sample_type", sample_type);
            if (!string.IsNullOrWhiteSpace(id_status_id)) fdict.Add("id_status_id", id_status_id);
            if (!string.IsNullOrWhiteSpace(ref_table)) fdict.Add("ref_table", ref_table);
            if (!string.IsNullOrWhiteSpace(ref_id)) fdict.Add("ref_id", ref_id);
            if (!string.IsNullOrWhiteSpace(accept_user_id)) fdict.Add("accept_user_id", accept_user_id);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<CaseSampleModel> GetOne(ulong id)
        {
            return await DBHelper.GetOne<CaseSampleModel>(id.ToString());
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<CaseInfoModel>(id.ToString());
        }
        public static async Task<int> Insert(ulong case_info_id, string number, string name, string sample_type, string description,
            string remark, string ref_table, ulong ref_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (case_info_id <= 0 || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(sample_type) ||
                string.IsNullOrWhiteSpace(ref_table) || ref_id <= 0)
                return -1;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("case_info_id", case_info_id.ToString());
            if (!string.IsNullOrWhiteSpace(number)) dict.Add("number", number);
            dict.Add("name", name);
            dict.Add("sample_type", sample_type);
            if (!string.IsNullOrWhiteSpace(description)) dict.Add("description", description);
            if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
            dict.Add("ref_table", ref_table);
            dict.Add("ref_id", ref_id.ToString());
            return (await DBHelper.Insert(TABLE, dict, dbConnection, trans));
        }
        public static async Task<int> Update(ulong id, string number, string name, string sample_type, string description,
            string remark, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(sample_type))
                return -1;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(number)) dict.Add("number", number);
            dict.Add("name", name);
            dict.Add("sample_type", sample_type);
            if (!string.IsNullOrWhiteSpace(description)) dict.Add("description", description);
            if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            return await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans);
        }
        public static async Task<int> Delete(ulong id, string ref_table, ulong ref_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (id <= 0) return -1;

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (id > 0) fdict.Add("id", id.ToString());
            if (!string.IsNullOrWhiteSpace(ref_table)) fdict.Add("ref_table", ref_table);
            if (ref_id > 0) fdict.Add("ref_id", ref_id.ToString());
            return await DBHelper.Delete(TABLE, fdict, "and", dbConnection, trans);
        }
    }
}
