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
    public class CaseEvidenceController : ApiController
    {
        [HttpPost]
        [Route("CaseEvidence/Insert")]
        public async Task<string> Insert(CaseEvidenceModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseEvidence.Insert(model.case_info_id, model.name, model.evi_type, model.description,
                model.remark, model.access_id);
        }
        [HttpPut]
        [Route("CaseEvidence/Update")]
        public async Task<string> Update(CaseEvidenceModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseEvidence.Update(model.id, model.name, model.evi_type, model.description,
                model.remark, model.access_id);
        }
        [HttpDelete]
        [Route("CaseEvidence/Delete")]
        public async Task<string> Delete(CaseEvidenceModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseEvidence.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("CaseEvidence/GetOne")]
        public async Task<CaseEvidenceModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.CaseEvidence.GetOne(id);
        }
        [HttpGet]
        [Route("CaseEvidence/GetList")]
        public async Task<IEnumerable<CaseEvidenceModel>> GetList(string case_info_id,int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<CaseEvidenceModel>();
            return await DBOper.CaseEvidence.GetList(case_info_id);
        }
    }
}
