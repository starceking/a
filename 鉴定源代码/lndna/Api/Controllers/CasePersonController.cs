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
    public class CasePersonController : ApiController
    {
        [HttpPost]
        [Route("CasePerson/Insert")]
        public async Task<string> Insert(CasePersonModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CasePerson.Insert(model.case_info_id, model.person_case_type, model.name, model.gender,
                model.nation, model.id_card_no, model.id_type, model.id_number, model.person_type, model.spec, model.birthday,
                model.country, model.alias, model.hjd_number, model.hjd_addr, model.xzz_number, model.xzz_addr,
                model.remark, model.age, model.missing_day, model.missing_addr, model.relative_id, model.relative_type,
                model.access_id);
        }
        [HttpPut]
        [Route("CasePerson/Update")]
        public async Task<string> Update(CasePersonModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CasePerson.Update(model.id, model.name, model.gender,
                model.nation, model.id_card_no, model.id_type, model.id_number, model.person_type, model.spec, model.birthday,
                model.country, model.alias, model.hjd_number, model.hjd_addr, model.xzz_number, model.xzz_addr,
                model.remark, model.age, model.missing_day, model.missing_addr, model.relative_type,
                model.access_id);
        }
        [HttpDelete]
        [Route("CasePerson/Delete")]
        public async Task<string> Delete(CasePersonModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CasePerson.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("CasePerson/GetOne")]
        public async Task<CasePersonModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.CasePerson.GetOne(id);
        }
        [HttpGet]
        [Route("CasePerson/GetList")]
        public async Task<IEnumerable<CasePersonModel>> GetList(string case_info_id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<CasePersonModel>();
            return await DBOper.CasePerson.GetList(case_info_id);
        }
    }
}
