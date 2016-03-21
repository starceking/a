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
    public class IdExtractSampleController : ApiController
    {
        [HttpPost]
        [Route("IdExtractSample/Insert")]
        public async Task<string> Insert(IdExtractSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdExtractSample.InsertOne(model.id_extract_id, model.case_sample_id, model.id_method,
                model.access_id);
        }
        [HttpPut]
        [Route("IdExtractSample/Update")]
        public async Task<string> Update(IdExtractSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdExtractSample.Update(model.id, model.id_method, model.access_id);
        }
        [HttpDelete]
        [Route("IdExtractSample/Delete")]
        public async Task<string> Delete(IdExtractSampleModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdExtractSample.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("IdExtractSample/GetOne")]
        public async Task<IdExtractSampleModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.IdExtractSample.GetOne(id);
        }
        [HttpGet]
        [Route("IdExtractSample/GetList")]
        public async Task<IEnumerable<IdExtractSampleModel>> GetList(string id_extract_id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<IdExtractSampleModel>();
            return await DBOper.IdExtractSample.GetList(id_extract_id);
        }
    }
}
