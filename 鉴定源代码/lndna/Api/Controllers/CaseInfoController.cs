using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    public class CaseInfoController : ApiController
    {
        [HttpPost]
        [Route("CaseInfo/Insert")]
        public async Task<string> Insert(CaseInfoModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseInfo.Insert(model.type_id, model.cg_name, model.cg_mobile,
                model.cg_fax, model.cg_day, model.cg_addr, model.cg_postcode, model.cg_man1, model.cg_mobile1,
                model.cg_cre_type1, model.cg_cre_number1, model.cg_duty1, model.cg_man2, model.cg_mobile2, model.cg_cre_type2,
                model.cg_cre_number2, model.cg_duty2, model.lab_no, model.id_request, model.cg_summary, model.id_src_info, model.id_reason,
                model.cg_remark, model.case_name, model.case_type, model.case_property, model.case_day,
                model.case_addr_number, model.case_addr, model.case_level, model.case_summary, model.ref_sys_number1,
                model.ref_sys_number2, model.ref_sys_number3, model.access_id);
        }
        [HttpPut]
        [Route("CaseInfo/Update")]
        public async Task<string> Update(CaseInfoModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseInfo.Update(model.id, model.cg_mobile,
                model.cg_fax, model.cg_day, model.cg_addr, model.cg_postcode, model.cg_man1, model.cg_mobile1,
                model.cg_cre_type1, model.cg_cre_number1, model.cg_duty1, model.cg_man2, model.cg_mobile2, model.cg_cre_type2,
                model.cg_cre_number2, model.cg_duty2, model.lab_no, model.id_request, model.cg_summary, model.id_src_info, model.id_reason,
                model.cg_remark, model.case_name, model.case_type, model.case_property, model.case_day,
                model.case_addr_number, model.case_addr, model.case_level, model.case_summary, model.ref_sys_number1,
                model.ref_sys_number2, model.ref_sys_number3, model.access_id);
        }
        [HttpPut]
        [Route("CaseInfo/PreAccept")]
        public async Task<string> PreAccept(CaseInfoModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseInfo.PreAccept(model.id, model.access_id);
        }
        [HttpPut]
        [Route("CaseInfo/Accept")]
        public async Task<string> Accept(CaseInfoModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseInfo.Accept(model.id, model.case_status_id, model.accept_remark, model.access_id);
        }
        [HttpDelete]
        [Route("CaseInfo/Delete")]
        public async Task<string> Delete(CaseInfoModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseInfo.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("CaseInfo/GetOne")]
        public async Task<CaseInfoModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.CaseInfo.GetOne(id);
        }
        [HttpGet]
        [Route("CaseInfo/GetList")]
        public async Task<IEnumerable<CaseInfoModel>> GetList(string type_id, string cg_number,
            string cg_days, string cg_daye, string case_name, string case_days, string case_daye,
            string consign_number, string accept_number,
            string case_status_id, string lab_no, string accept_user_id,
            string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<CaseInfoModel>();
            return await DBOper.CaseInfo.GetList(type_id, cg_number, cg_days, cg_daye,
                case_name, case_days, case_daye, consign_number, accept_number,
                case_status_id, lab_no, accept_user_id, page_size, page_index, access_id);
        }
        [HttpGet]
        [Route("CaseInfo/GetCount")]
        public async Task<long> GetCount(string type_id, string cg_number,
            string cg_days, string cg_daye, string case_name, string case_days, string case_daye,
            string consign_number, string accept_number,
            string case_status_id, string lab_no, string accept_user_id,
            int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return 0;
            return await DBOper.CaseInfo.GetCount(type_id, cg_number, cg_days, cg_daye,
                case_name, case_days, case_daye, consign_number, accept_number,
                case_status_id, lab_no, accept_user_id, access_id);
        }
    }
}
