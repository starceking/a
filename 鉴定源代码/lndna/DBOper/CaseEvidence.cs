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
    /// 物证
    /// </summary>
    public static class CaseEvidence
    {
        public const string TABLE = "case_evidence";

        public static async Task<string> Insert(ulong case_info_id, string name, string evi_type, string description,
            string remark, int user_id)
        {
            if (case_info_id <= 0 || string.IsNullOrWhiteSpace(name)) return "参数不全";
            string ck = await CaseInfo.CheckAuth(case_info_id, user_id);
            if (!string.IsNullOrWhiteSpace(ck)) return ck;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("case_info_id", case_info_id.ToString());
            dict.Add("name", name);
            if (!string.IsNullOrWhiteSpace(evi_type)) dict.Add("evi_type", evi_type);
            if (!string.IsNullOrWhiteSpace(description)) dict.Add("description", description);
            if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
            if ((await DBHelper.Insert(TABLE, dict)) <= 0)
            {
                return "异常，请重试";
            }
            return string.Empty;
        }
        public static async Task<string> Update(ulong id, string name, string evi_type, string description,
            string remark, int user_id)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(name)) return "参数不全";
            CaseEvidenceModel cem = await GetOne(id);
            if (cem == null) return "读取不到CEM";
            string ck = await CaseInfo.CheckAuth(cem.case_info_id, user_id);
            if (!string.IsNullOrWhiteSpace(ck)) return ck;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", name);
            if (!string.IsNullOrWhiteSpace(evi_type)) dict.Add("evi_type", evi_type);
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
            CaseEvidenceModel cem = await GetOne(id);
            if (cem == null) return "读取不到CEM";
            string ck = await CaseInfo.CheckAuth(cem.case_info_id, user_id);
            if (!string.IsNullOrWhiteSpace(ck)) return ck;

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if ((await DBHelper.Delete(TABLE, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        return "异常，请重试";
                    }

                    await CaseSample.Delete(0, TABLE, id, dbConnection, trans);

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<IEnumerable<CaseEvidenceModel>> GetList(string case_info_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("case_info_id", case_info_id);
            return await DBHelper.GetList<CaseEvidenceModel, long>(TABLE, "*", "id", fdict, "and");
        }
        public static async Task<CaseEvidenceModel> GetOne(ulong id)
        {
            if (id <= 0) return null;
            return await DBHelper.GetOne<CaseEvidenceModel>(id.ToString());
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<CaseEvidenceModel>(id.ToString());
        }
    }
}
