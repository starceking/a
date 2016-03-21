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
    public class IdPreSampleController : ApiController
    {
        [HttpPost]
        [Route("IdPreSample/Insert")]
        public async Task<string> Insert(IdPreSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdPreSample.InsertOne(model.id_pre_id, model.case_sample_id, model.id_method,
                model.id_result, model.access_id);
        }
        [HttpPut]
        [Route("IdPreSample/Update")]
        public async Task<string> Update(IdPreSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdPreSample.Update(model.id, model.id_method, model.id_result, model.access_id);
        }
        [HttpDelete]
        [Route("IdPreSample/Delete")]
        public async Task<string> Delete(IdPreSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdPreSample.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("IdPreSample/GetOne")]
        public async Task<IdPreSampleModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.IdPreSample.GetOne(id);
        }
        [HttpGet]
        [Route("IdPreSample/GetList")]
        public async Task<IEnumerable<IdPreSampleModel>> GetList(string id_amp_id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<IdPreSampleModel>();
            return await DBOper.IdPreSample.GetList(id_amp_id);
        }
    }
}
