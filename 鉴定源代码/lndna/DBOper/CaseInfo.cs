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
    /// 案件
    /// </summary>
    public static class CaseInfo
    {
        public const string TABLE = "case_info";

        /// <summary>
        /// 发起新的委托
        /// </summary>
        public static async Task<string> Insert(int type_id, string cg_name, string cg_mobile,
            string cg_fax, DateTime cg_day, string cg_addr, string cg_postcode, string cg_man1, string cg_mobile1, string cg_cre_type1,
            string cg_cre_number1, string cg_duty1, string cg_man2, string cg_mobile2, string cg_cre_type2, string cg_cre_number2, string cg_duty2, string lab_no,
            string id_request, string cg_summary, string id_src_info, string id_reason, string cg_remark, string case_name, string case_type,
            string case_property, DateTime case_day, string case_addr_number, string case_addr, string case_level, string case_summary,
            string ref_sys_number1, string ref_sys_number2, string ref_sys_number3, int user_id)
        {
            if (type_id <= 0 || cg_day == null || string.IsNullOrWhiteSpace(cg_name) ||
                string.IsNullOrWhiteSpace(cg_man1) || string.IsNullOrWhiteSpace(cg_mobile1) || string.IsNullOrWhiteSpace(cg_cre_type1) || string.IsNullOrWhiteSpace(cg_cre_number1) ||
                string.IsNullOrWhiteSpace(cg_man1) || string.IsNullOrWhiteSpace(cg_mobile2) || string.IsNullOrWhiteSpace(cg_cre_type2) || string.IsNullOrWhiteSpace(cg_cre_number2) ||
                string.IsNullOrWhiteSpace(case_name) || case_day == null || string.IsNullOrWhiteSpace(case_addr_number) ||
                string.IsNullOrWhiteSpace(case_addr))
                return "参数不全";
            UserModel user = await User.GetOne(user_id, string.Empty);
            if (user == null) return "读取不到USER";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("type_id", type_id.ToString());
            dict.Add("cg_number", user.dept_no);
            dict.Add("cg_name", cg_name);
            if (!string.IsNullOrWhiteSpace(cg_mobile)) dict.Add("cg_mobile", cg_mobile);
            if (!string.IsNullOrWhiteSpace(cg_fax)) dict.Add("cg_fax", cg_fax);
            dict.Add("cg_day", cg_day.ToString());
            if (!string.IsNullOrWhiteSpace(cg_addr)) dict.Add("cg_addr", cg_addr);
            if (!string.IsNullOrWhiteSpace(cg_postcode)) dict.Add("cg_postcode", cg_postcode);
            dict.Add("cg_man1", cg_man1);
            dict.Add("cg_mobile1", cg_mobile1);
            dict.Add("cg_cre_type1", cg_cre_type1);
            dict.Add("cg_cre_number1", cg_cre_number1);
            if (!string.IsNullOrWhiteSpace(cg_duty1)) dict.Add("cg_duty1", cg_duty1);
            dict.Add("cg_man2", cg_man2);
            dict.Add("cg_mobile2", cg_mobile2);
            dict.Add("cg_cre_type2", cg_cre_type2);
            dict.Add("cg_cre_number2", cg_cre_number2);
            if (!string.IsNullOrWhiteSpace(cg_duty2)) dict.Add("cg_duty2", cg_duty2);
            dict.Add("lab_no", lab_no);
            if (!string.IsNullOrWhiteSpace(id_request)) dict.Add("id_request", id_request);
            if (!string.IsNullOrWhiteSpace(cg_summary)) dict.Add("cg_summary", cg_summary);
            dict.Add("consign_number", await GetConsignNumber());
            if (!string.IsNullOrWhiteSpace(id_src_info)) dict.Add("id_src_info", id_src_info);
            if (!string.IsNullOrWhiteSpace(id_reason)) dict.Add("id_reason", id_reason);
            if (!string.IsNullOrWhiteSpace(cg_remark)) dict.Add("cg_remark", cg_remark);
            dict.Add("case_name", case_name);
            if (!string.IsNullOrWhiteSpace(case_type)) dict.Add("case_type", case_type);
            if (!string.IsNullOrWhiteSpace(case_property)) dict.Add("case_property", case_property);
            dict.Add("case_day", case_day.ToString());
            dict.Add("case_addr_number", case_addr_number);
            dict.Add("case_addr", case_addr);
            if (!string.IsNullOrWhiteSpace(case_level)) dict.Add("case_level", case_level);
            if (!string.IsNullOrWhiteSpace(case_summary)) dict.Add("case_summary", case_summary);
            if (!string.IsNullOrWhiteSpace(ref_sys_number1)) dict.Add("ref_sys_number1", ref_sys_number1);
            if (!string.IsNullOrWhiteSpace(ref_sys_number2)) dict.Add("ref_sys_number2", ref_sys_number2);
            if (!string.IsNullOrWhiteSpace(ref_sys_number3)) dict.Add("ref_sys_number3", ref_sys_number3);
            if ((await DBHelper.Insert(TABLE, dict)) <= 0)
            {
                return "异常，请重试";
            }

            return string.Empty;
        }
        /// <summary>
        /// 获取下一个委托编号
        /// </summary>
        static async Task<string> GetConsignNumber()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        /// <summary>
        /// 修改基本信息
        /// </summary>
        public static async Task<string> Update(ulong id, string cg_mobile,
            string cg_fax, DateTime cg_day, string cg_addr, string cg_postcode, string cg_man1, string cg_mobile1, string cg_cre_type1,
            string cg_cre_number1, string cg_duty1, string cg_man2, string cg_mobile2, string cg_cre_type2, string cg_cre_number2, string cg_duty2, string lab_no,
            string id_request, string cg_summary, string id_src_info, string id_reason, string cg_remark, string case_name, string case_type,
            string case_property, DateTime case_day, string case_addr_number, string case_addr, string case_level, string case_summary,
            string ref_sys_number1, string ref_sys_number2, string ref_sys_number3, int user_id)
        {
            if (id <= 0 || cg_day == null ||
                string.IsNullOrWhiteSpace(cg_man1) || string.IsNullOrWhiteSpace(cg_mobile1) || string.IsNullOrWhiteSpace(cg_cre_type1) || string.IsNullOrWhiteSpace(cg_cre_number1) ||
                string.IsNullOrWhiteSpace(cg_man1) || string.IsNullOrWhiteSpace(cg_mobile2) || string.IsNullOrWhiteSpace(cg_cre_type2) || string.IsNullOrWhiteSpace(cg_cre_number2) ||
                string.IsNullOrWhiteSpace(lab_no) || string.IsNullOrWhiteSpace(case_name) || case_day == null || string.IsNullOrWhiteSpace(case_addr_number) ||
                string.IsNullOrWhiteSpace(case_addr))
                return "参数不全";
            string ck = await CheckAuth(id, user_id);
            if (!string.IsNullOrWhiteSpace(ck)) return ck;
            UserModel user = await User.GetOne(user_id, string.Empty);
            if (user == null) return "读取不到USER";
            if (user.dept_no.Length == 6 && !user.dept_no.Equals(lab_no)) return "权限异常";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(cg_mobile)) dict.Add("cg_mobile", cg_mobile);
            if (!string.IsNullOrWhiteSpace(cg_fax)) dict.Add("cg_fax", cg_fax);
            if (cg_day != null) dict.Add("cg_day", cg_day.ToString());
            if (!string.IsNullOrWhiteSpace(cg_addr)) dict.Add("cg_addr", cg_addr);
            if (!string.IsNullOrWhiteSpace(cg_postcode)) dict.Add("cg_postcode", cg_postcode);
            dict.Add("cg_man1", cg_man1);
            dict.Add("cg_mobile1", cg_mobile1);
            dict.Add("cg_cre_type1", cg_cre_type1);
            dict.Add("cg_cre_number1", cg_cre_number1);
            if (!string.IsNullOrWhiteSpace(cg_duty1)) dict.Add("cg_duty1", cg_duty1);
            dict.Add("cg_man2", cg_man2);
            dict.Add("cg_mobile2", cg_mobile2);
            dict.Add("cg_cre_type2", cg_cre_type2);
            dict.Add("cg_cre_number2", cg_cre_number2);
            if (!string.IsNullOrWhiteSpace(cg_duty2)) dict.Add("cg_duty2", cg_duty2);
            dict.Add("lab_no", lab_no);
            if (!string.IsNullOrWhiteSpace(id_request)) dict.Add("id_request", id_request);
            if (!string.IsNullOrWhiteSpace(cg_summary)) dict.Add("cg_summary", cg_summary);
            if (!string.IsNullOrWhiteSpace(id_src_info)) dict.Add("id_src_info", id_src_info);
            if (!string.IsNullOrWhiteSpace(id_reason)) dict.Add("id_reason", id_reason);
            if (!string.IsNullOrWhiteSpace(cg_remark)) dict.Add("cg_remark", cg_remark);
            dict.Add("case_name", case_name);
            if (!string.IsNullOrWhiteSpace(case_type)) dict.Add("case_type", case_type);
            if (!string.IsNullOrWhiteSpace(case_property)) dict.Add("case_property", case_property);
            dict.Add("case_day", case_day.ToString());
            dict.Add("case_addr_number", case_addr_number);
            dict.Add("case_addr", case_addr);
            if (!string.IsNullOrWhiteSpace(case_level)) dict.Add("case_level", case_level);
            if (!string.IsNullOrWhiteSpace(case_summary)) dict.Add("case_summary", case_summary);
            if (!string.IsNullOrWhiteSpace(ref_sys_number1)) dict.Add("ref_sys_number1", ref_sys_number1);
            if (!string.IsNullOrWhiteSpace(ref_sys_number2)) dict.Add("ref_sys_number2", ref_sys_number2);
            if (!string.IsNullOrWhiteSpace(ref_sys_number3)) dict.Add("ref_sys_number3", ref_sys_number3);
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0)
            {
                return "异常，请重试";
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        /// <summary>
        /// 提交待受理
        /// </summary>
        public static async Task<string> PreAccept(ulong id, int user_id)
        {
            if (id <= 0) return "参数不全";
            CaseInfoModel cim = await GetOne(id);
            if (cim == null) return "读取不到CASE";
            if (cim.case_status_id != 1 && cim.case_status_id != -1) return "状态异常";
            string ck = await CheckAuth(id, user_id);
            if (!string.IsNullOrWhiteSpace(ck)) return ck;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("case_status_id", "2");
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            fdict.Add("重复字段1重复字段case_status_id", cim.case_status_id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0)
            {
                return "异常，请重试";
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        /// <summary>
        /// 受理
        /// </summary>
        public static async Task<string> Accept(ulong id, int case_status_id, string accept_remark, int user_id)
        {
            if (id <= 0) return "参数不全";
            CaseInfoModel cim = await GetOne(id);
            if (cim == null) return "读取不到CASE";
            if (cim.case_status_id != 2) return "状态异常";
            UserModel user = await User.GetOne(user_id, string.Empty);
            if (user == null) return "读取不到USER";
            if (!user.dept_no.Equals(cim.lab_no)) return "权限异常";

            string accept_number = await GetAcceptNumber();

            IEnumerable<CaseSampleModel> list = await CaseSample.GetList(id.ToString(), string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, string.Empty, "0", "0");

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("case_status_id", case_status_id.ToString());
                    dict.Add("accept_number", accept_number);
                    if (!string.IsNullOrWhiteSpace(accept_remark)) dict.Add("accept_remark", accept_remark);
                    if (case_status_id == 3) dict.Add("accept_user_id", user_id.ToString());
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    fdict.Add("重复字段1重复字段case_status_id", "2");
                    if ((await DBHelper.Update(TABLE, dict, fdict, "and", dbConnection, trans)) <= 0)
                    {
                        return "异常，请重试";
                    }

                    if (case_status_id == 3 && list.Count() > 0)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("id_status_id", "3");
                        dict.Add("accept_user_id", user_id.ToString());
                        fdict = new Dictionary<string, string>();
                        fdict.Add("case_info_id", id.ToString());
                        if ((await DBHelper.Update(CaseSample.TABLE, dict, fdict, "and", dbConnection, trans)) != list.Count())
                        {
                            return "异常，请重试";
                        }
                    }

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            foreach (CaseSampleModel csm in list)
                await CaseSample.DeleteRedis(csm.id);
            return string.Empty;
        }
        /// <summary>
        /// 获取下一个受理编号
        /// </summary>
        static async Task<string> GetAcceptNumber()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        public static async Task<string> Delete(ulong id, int user_id)
        {
            if (id <= 0) return "参数不全";
            CaseInfoModel cim = await GetOne(id);
            if (cim == null) return "读取不到CIM";
            UserModel user = await User.GetOne(user_id, string.Empty);
            if (user == null) return "读取不到USERM";
            if (cim.case_status_id > 1) return "已受理的案件不可删除";

            using (MySqlConnection dbConnection = await DBHelper.GetOpenCon())
            {
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> fdict = new Dictionary<string, string>();
                    fdict.Add("id", id.ToString());
                    if ((await DBHelper.Delete(TABLE, fdict, "and", dbConnection, trans)) <= 0) return "异常，请重试";

                    fdict = new Dictionary<string, string>();
                    fdict.Add("case_info_id", id.ToString());
                    await DBHelper.Delete(CaseSample.TABLE, fdict, "and", dbConnection, trans);

                    trans.Commit();
                }
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<IEnumerable<CaseInfoModel>> GetList(string type_id, string cg_number,
            string cg_days, string cg_daye, string case_name, string case_days, string case_daye,
            string consign_number, string accept_number,
            string case_status_id, string lab_no, string accept_user_id,
            string page_size, string page_index, int user_id)
        {
            if (user_id <= 0) return new List<CaseInfoModel>();
            UserModel user = await User.GetOne(user_id, string.Empty);
            if (user == null) return new List<CaseInfoModel>();
            if (DictSettings.DeptIsCg(user.dept_no))
            {
                if (string.IsNullOrWhiteSpace(cg_number)) cg_number = user.dept_no;
                cg_number = DictSettings.DebtGetSubs(cg_number);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(lab_no)) lab_no = user.dept_no;
                lab_no = DictSettings.DebtGetSubs(lab_no);
            }

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(type_id)) fdict.Add("type_id", type_id);
            if (!string.IsNullOrWhiteSpace(cg_number)) fdict.Add("cg_number", cg_number + "%");
            if (!string.IsNullOrWhiteSpace(cg_days)) fdict.Add("cg_day", ">=" + cg_days);
            if (!string.IsNullOrWhiteSpace(cg_daye)) fdict.Add("重复字段1重复字段cg_day", "<=" + cg_daye);
            if (!string.IsNullOrWhiteSpace(case_days)) fdict.Add("case_day", ">=" + case_days);
            if (!string.IsNullOrWhiteSpace(case_daye)) fdict.Add("重复字段1重复字段case_day", "<=" + case_daye);
            if (!string.IsNullOrWhiteSpace(consign_number)) fdict.Add("consign_number", consign_number);
            if (!string.IsNullOrWhiteSpace(accept_number)) fdict.Add("accept_number", accept_number);
            if (!string.IsNullOrWhiteSpace(case_status_id)) fdict.Add("case_status_id", case_status_id);
            if (!string.IsNullOrWhiteSpace(lab_no)) fdict.Add("lab_no", lab_no + "%");
            if (!string.IsNullOrWhiteSpace(accept_user_id)) fdict.Add("accept_user_id", accept_user_id);
            return await DBHelper.GetList<CaseInfoModel, long>(TABLE, "*", "id desc", fdict, "and",
                Convert.ToInt32(page_size), Convert.ToInt32(page_index));
        }
        public static async Task<long> GetCount(string type_id, string cg_number,
            string cg_days, string cg_daye, string case_name, string case_days, string case_daye,
            string consign_number, string accept_number,
            string case_status_id, string lab_no, string accept_user_id,
            int user_id)
        {
            if (user_id <= 0) return 0;
            UserModel user = await User.GetOne(user_id, string.Empty);
            if (user == null) return 0;
            if (DictSettings.DeptIsCg(user.dept_no))
            {
                if (string.IsNullOrWhiteSpace(cg_number)) cg_number = user.dept_no;
                cg_number = DictSettings.DebtGetSubs(cg_number);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(lab_no)) lab_no = user.dept_no;
                lab_no = DictSettings.DebtGetSubs(lab_no);
            }

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(type_id)) fdict.Add("type_id", type_id);
            if (!string.IsNullOrWhiteSpace(cg_number)) fdict.Add("cg_number", cg_number + "%");
            if (!string.IsNullOrWhiteSpace(cg_days)) fdict.Add("cg_day", ">=" + cg_days);
            if (!string.IsNullOrWhiteSpace(cg_daye)) fdict.Add("重复字段1重复字段cg_day", "<=" + cg_daye);
            if (!string.IsNullOrWhiteSpace(case_days)) fdict.Add("case_day", ">=" + case_days);
            if (!string.IsNullOrWhiteSpace(case_daye)) fdict.Add("重复字段1重复字段case_day", "<=" + case_daye);
            if (!string.IsNullOrWhiteSpace(consign_number)) fdict.Add("consign_number", consign_number);
            if (!string.IsNullOrWhiteSpace(accept_number)) fdict.Add("accept_number", accept_number);
            if (!string.IsNullOrWhiteSpace(case_status_id)) fdict.Add("case_status_id", case_status_id);
            if (!string.IsNullOrWhiteSpace(lab_no)) fdict.Add("lab_no", lab_no + "%");
            if (!string.IsNullOrWhiteSpace(accept_user_id)) fdict.Add("accept_user_id", accept_user_id);
            return await DBHelper.GetCount(TABLE, fdict, "and");
        }
        public static async Task<CaseInfoModel> GetOne(ulong id)
        {
            return await DBHelper.GetOne<CaseInfoModel>(id.ToString());
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<CaseInfoModel>(id.ToString());
        }
        /// <summary>
        /// 该用户是否拥有该案件的修改权限
        /// </summary>
        public static async Task<string> CheckAuth(ulong id, int user_id)
        {
            UserModel user = await User.GetOne(user_id, string.Empty);
            if (user == null) return "读取不到USER";
            CaseInfoModel cim = await GetOne(id);
            if (cim == null) return "读取不到CASE";
            if (cim.case_status_id >= 3)
            {
                //已受理的案件，只有一检人可以修改
                if (cim.accept_user_id != user_id) return "已受理的案件无法修改";
            }
            //未受理的案件，只有委托单位可以修改
            else if (!user.dept_no.Equals(cim.cg_number)) return "权限异常";

            return string.Empty;
        }
    }
}
