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
    public class IdEpController : ApiController
    {
        [HttpPost]
        [Route("IdEp/Insert")]
        public async Task<string> Insert(IdEpModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdEp.Insert(model.number, model.name, model.access_id, model.shelf_type, model.id_day,
                model.machine, model.board, model.sheet, model.sample_ids);
        }
        [HttpPut]
        [Route("IdEp/Update")]
        public async Task<string> Update(IdEpModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdEp.Update(model.id, model.number, model.name, model.shelf_type, model.id_day,
                model.machine, model.board, model.sheet, model.access_id);
        }
        [HttpDelete]
        [Route("IdEp/Delete")]
        public async Task<string> Delete(IdEpModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdEp.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("IdEp/GetOne")]
        public async Task<IdEpModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.IdEp.GetOne(id);
        }
        [HttpGet]
        [Route("IdEp/GetList")]
        public async Task<IEnumerable<IdEpModel>> GetList(string number, string user_id, string id_days, string id_daye,
            string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<IdEpModel>();
            return await DBOper.IdEp.GetList(number, user_id, id_days, id_daye, page_size, page_index);
        }
        [HttpGet]
        [Route("IdEp/GetCount")]
        public async Task<long> GetCount(string number, string user_id, string id_days, string id_daye,
            int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return 0;
            return await DBOper.IdEp.GetCount(number, user_id, id_days, id_daye);
        }
    }
}
