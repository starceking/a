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
    public class IdAmpController : ApiController
    {
        [HttpPost]
        [Route("IdAmp/Insert")]
        public async Task<string> Insert(IdAmpModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdAmp.Insert(model.number, model.name, model.access_id, model.shelf_type, model.id_day,
                model.method, model.machine, model.kit, model.amount, model.batch, model.circle, model.board, model.sample_ids);
        }
        [HttpPut]
        [Route("IdAmp/Update")]
        public async Task<string> Update(IdAmpModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdAmp.Update(model.id, model.number, model.name, model.shelf_type, model.id_day,
                model.method, model.machine, model.kit, model.amount, model.batch, model.circle, model.board, model.access_id);
        }
        [HttpDelete]
        [Route("IdAmp/Delete")]
        public async Task<string> Delete(IdAmpModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdAmp.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("IdAmp/GetOne")]
        public async Task<IdAmpModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.IdAmp.GetOne(id);
        }
        [HttpGet]
        [Route("IdAmp/GetList")]
        public async Task<IEnumerable<IdAmpModel>> GetList(string number, string user_id, string id_days, string id_daye,
            string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<IdAmpModel>();
            return await DBOper.IdAmp.GetList(number, user_id, id_days, id_daye, page_size, page_index);
        }
        [HttpGet]
        [Route("IdAmp/GetCount")]
        public async Task<long> GetCount(string number, string user_id, string id_days, string id_daye,
            int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return 0;
            return await DBOper.IdAmp.GetCount(number, user_id, id_days, id_daye);
        }
    }
}
