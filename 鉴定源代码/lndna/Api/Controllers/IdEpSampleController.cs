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
    public class IdEpSampleController : ApiController
    {
        [HttpPost]
        [Route("IdEpSample/Insert")]
        public async Task<string> Insert(IdEpSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdEpSample.InsertOne(model.id_ep_id, model.case_sample_id, model.position,
                model.amount, model.access_id);
        }
        [HttpPut]
        [Route("IdEpSample/Update")]
        public async Task<string> Update(IdEpSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdEpSample.Update(model.id, model.position, model.amount, model.access_id);
        }
        [HttpDelete]
        [Route("IdEpSample/Delete")]
        public async Task<string> Delete(IdEpSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdEpSample.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("IdEpSample/GetOne")]
        public async Task<IdEpSampleModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.IdEpSample.GetOne(id);
        }
        [HttpGet]
        [Route("IdEpSample/GetList")]
        public async Task<IEnumerable<IdEpSampleModel>> GetList(string id_amp_id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<IdEpSampleModel>();
            return await DBOper.IdEpSample.GetList(id_amp_id);
        }
    }
}
