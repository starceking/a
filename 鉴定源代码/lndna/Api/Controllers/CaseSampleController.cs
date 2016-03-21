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
    public class CaseSampleController : ApiController
    {
        [HttpPost]
        [Route("CaseSample/Insert")]
        public async Task<string> Insert(CaseSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseSample.Insert(model.case_info_id, model.number, model.name, model.sample_type,
                model.description, model.remark, model.ref_table, model.ref_id, model.access_id);
        }
        [HttpPut]
        [Route("CaseSample/Update")]
        public async Task<string> Update(CaseSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseSample.Update(model.id, model.number, model.name, model.sample_type,
                model.description, model.remark, model.access_id);
        }
        [HttpDelete]
        [Route("CaseSample/Delete")]
        public async Task<string> Delete(CaseSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseSample.Delete(model.id, model.access_id);
        }
        [HttpPut]
        [Route("CaseSample/Accept")]
        public async Task<string> Accept(CaseSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.CaseSample.Accept(model.id, model.id_status_id, model.accept_remark);
        }
        [HttpGet]
        [Route("CaseSample/GetOne")]
        public async Task<CaseSampleModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.CaseSample.GetOne(id);
        }
        [HttpGet]
        [Route("CaseSample/GetList")]
        public async Task<IEnumerable<CaseSampleModel>> GetList(string case_info_id, string number, string sample_type,
            string id_status_id, string ref_table, string ref_id, string accept_user_id,
            string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<CaseSampleModel>();
            return await DBOper.CaseSample.GetList(case_info_id, number, sample_type,
                id_status_id, ref_table, ref_id, accept_user_id,
                page_size, page_index);
        }
        [HttpGet]
        [Route("CaseSample/GetCount")]
        public async Task<long> GetCount(string case_info_id, string number, string sample_type,
            string id_status_id, string ref_table, string ref_id, string accept_user_id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return 0;
            return await DBOper.CaseSample.GetCount(case_info_id, number, sample_type,
                id_status_id, ref_table, ref_id, accept_user_id);
        }
    }
}
