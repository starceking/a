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
    public static class CasePerson
    {
        public const string TABLE = "case_person";

        public static async Task<string> Insert(ulong case_info_id, int person_case_type, string name, string gender, string nation,
            string id_card_no, string id_type, string id_number, string person_type, string spec, DateTime? birthday,
            string country, string alias, string hjd_number, string hjd_addr, string xzz_number, string xzz_addr,
            string remark, int age, DateTime? missing_day, string missing_addr, ulong relative_id, string relative_type,
            int user_id)
        {
            if (case_info_id <= 0 || person_case_type <= 0 || string.IsNullOrWhiteSpace(name))
                return "参数不全";
            string ck = await CaseInfo.CheckAuth(case_info_id, user_id);
            if (!string.IsNullOrWhiteSpace(ck)) return ck;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("case_info_id", case_info_id.ToString());
            dict.Add("person_case_type", person_case_type.ToString());
            dict.Add("name", name);
            if (!string.IsNullOrWhiteSpace(gender)) dict.Add("gender", gender);
            if (!string.IsNullOrWhiteSpace(nation)) dict.Add("nation", nation);
            if (!string.IsNullOrWhiteSpace(id_card_no)) dict.Add("id_card_no", id_card_no);
            if (!string.IsNullOrWhiteSpace(id_type)) dict.Add("id_type", id_type);
            if (!string.IsNullOrWhiteSpace(id_number)) dict.Add("id_number", id_number);
            if (!string.IsNullOrWhiteSpace(person_type)) dict.Add("person_type", person_type);
            if (!string.IsNullOrWhiteSpace(spec)) dict.Add("spec", spec);
            if (birthday != null) dict.Add("birthday", birthday.Value.ToShortDateString());
            if (!string.IsNullOrWhiteSpace(country)) dict.Add("country", country);
            if (!string.IsNullOrWhiteSpace(alias)) dict.Add("alias", alias);
            if (!string.IsNullOrWhiteSpace(hjd_number)) dict.Add("hjd_number", hjd_number);
            if (!string.IsNullOrWhiteSpace(hjd_addr)) dict.Add("hjd_addr", hjd_addr);
            if (!string.IsNullOrWhiteSpace(xzz_number)) dict.Add("xzz_number", xzz_number);
            if (!string.IsNullOrWhiteSpace(xzz_addr)) dict.Add("xzz_addr", xzz_addr);
            if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
            dict.Add("age", age.ToString());
            if (missing_day != null) dict.Add("missing_day", missing_day.Value.ToShortDateString());
            if (!string.IsNullOrWhiteSpace(missing_addr)) dict.Add("missing_addr", missing_addr);
            dict.Add("relative_id", relative_id.ToString());
            if (!string.IsNullOrWhiteSpace(relative_type)) dict.Add("relative_type", relative_type);
            if ((await DBHelper.Insert(TABLE, dict)) <= 0)
            {
                return "异常，请重试";
            }
            return string.Empty;
        }
        public static async Task<string> Update(ulong id, string name, string gender, string nation,
            string id_card_no, string id_type, string id_number, string person_type, string spec, DateTime? birthday,
            string country, string alias, string hjd_number, string hjd_addr, string xzz_number, string xzz_addr,
            string remark, int age, DateTime? missing_day, string missing_addr, int user_id)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(name)) return "参数不全";
            CasePersonModel cpm = await GetOne(id);
            if (cpm == null) return "读取不到CPM";
            string ck = await CaseInfo.CheckAuth(cpm.case_info_id, user_id);
            if (!string.IsNullOrWhiteSpace(ck)) return ck;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", name);
            if (!string.IsNullOrWhiteSpace(gender)) dict.Add("gender", gender);
            if (!string.IsNullOrWhiteSpace(nation)) dict.Add("nation", nation);
            if (!string.IsNullOrWhiteSpace(id_card_no)) dict.Add("id_card_no", id_card_no);
            if (!string.IsNullOrWhiteSpace(id_type)) dict.Add("id_type", id_type);
            if (!string.IsNullOrWhiteSpace(id_number)) dict.Add("id_number", id_number);
            if (!string.IsNullOrWhiteSpace(person_type)) dict.Add("person_type", person_type);
            if (!string.IsNullOrWhiteSpace(spec)) dict.Add("spec", spec);
            if (birthday != null) dict.Add("birthday", birthday.Value.ToShortDateString());
            if (!string.IsNullOrWhiteSpace(country)) dict.Add("country", country);
            if (!string.IsNullOrWhiteSpace(alias)) dict.Add("alias", alias);
            if (!string.IsNullOrWhiteSpace(hjd_number)) dict.Add("hjd_number", hjd_number);
            if (!string.IsNullOrWhiteSpace(hjd_addr)) dict.Add("hjd_addr", hjd_addr);
            if (!string.IsNullOrWhiteSpace(xzz_number)) dict.Add("xzz_number", xzz_number);
            if (!string.IsNullOrWhiteSpace(xzz_addr)) dict.Add("xzz_addr", xzz_addr);
            if (!string.IsNullOrWhiteSpace(remark)) dict.Add("remark", remark);
            dict.Add("age", age.ToString());
            if (missing_day != null) dict.Add("missing_day", missing_day.Value.ToShortDateString());
            if (!string.IsNullOrWhiteSpace(missing_addr)) dict.Add("missing_addr", missing_addr);
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
            CasePersonModel cem = await GetOne(id);
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
        public static async Task<IEnumerable<CasePersonModel>> GetList(string case_info_id, string page_size, string page_index)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(case_info_id)) fdict.Add("case_info_id", case_info_id);
            return await DBHelper.GetList<CasePersonModel, long>(TABLE, "*", "id", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));
        }
        public static async Task<long> GetCount(string case_info_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(case_info_id)) fdict.Add("case_info_id", case_info_id);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<CasePersonModel> GetOne(ulong id)
        {
            return await DBHelper.GetOne<CasePersonModel>(id.ToString());
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<CasePersonModel>(id.ToString());
        }
    }
}
