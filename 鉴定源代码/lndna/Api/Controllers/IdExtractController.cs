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
    public class IdExtractController : ApiController
    {
        [HttpPost]
        [Route("IdExtract/Insert")]
        public async Task<string> Insert(IdExtractModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdExtract.Insert(model.number, model.name, model.access_id, model.shelf_type, model.id_day,
                model.machine, model.remark, model.sample_ids);
        }
        [HttpPut]
        [Route("IdExtract/Update")]
        public async Task<string> Update(IdExtractModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdExtract.Update(model.id, model.number, model.name, model.shelf_type, model.id_day,
                model.machine, model.remark, model.access_id);
        }
        [HttpDelete]
        [Route("IdExtract/Delete")]
        public async Task<string> Delete(IdExtractModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdExtract.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("IdExtract/GetOne")]
        public async Task<IdExtractModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.IdExtract.GetOne(id);
        }
        [HttpGet]
        [Route("IdExtract/GetList")]
        public async Task<IEnumerable<IdExtractModel>> GetList(string number, string user_id, string id_days, string id_daye,
            string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<IdExtractModel>();
            return await DBOper.IdExtract.GetList(number, user_id, id_days, id_daye, page_size, page_index);
        }
        [HttpGet]
        [Route("IdExtract/GetCount")]
        public async Task<long> GetCount(string number, string user_id, string id_days, string id_daye,
            int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return 0;
            return await DBOper.IdExtract.GetCount(number, user_id, id_days, id_daye);
        }
    }
}
