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
    public class IdAmpSampleController : ApiController
    {
        [HttpPost]
        [Route("IdAmpSample/Insert")]
        public async Task<string> Insert(IdAmpSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdAmpSample.InsertOne(model.id_amp_id, model.case_sample_id, model.position,
                model.amount, model.access_id);
        }
        [HttpPut]
        [Route("IdAmpSample/Update")]
        public async Task<string> Update(IdAmpSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdAmpSample.Update(model.id, model.position, model.amount, model.access_id);
        }
        [HttpDelete]
        [Route("IdAmpSample/Delete")]
        public async Task<string> Delete(IdAmpSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdAmpSample.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("IdAmpSample/GetOne")]
        public async Task<IdAmpSampleModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.IdAmpSample.GetOne(id);
        }
        [HttpGet]
        [Route("IdAmpSample/GetList")]
        public async Task<IEnumerable<IdAmpSampleModel>> GetList(string id_amp_id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<IdAmpSampleModel>();
            return await DBOper.IdAmpSample.GetList(id_amp_id);
        }
    }
}
