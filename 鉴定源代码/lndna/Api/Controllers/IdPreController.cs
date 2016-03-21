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
    public class IdPreController : ApiController
    {
        [HttpPost]
        [Route("IdPre/Insert")]
        public async Task<string> Insert(IdPreModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdPre.Insert(model.number, model.name, model.access_id, model.shelf_type, model.id_day,
                model.sample_ids);
        }
        [HttpPut]
        [Route("IdPre/Update")]
        public async Task<string> Update(IdPreModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdPre.Update(model.id, model.number, model.name, model.shelf_type,
                model.id_day, model.access_id);
        }
        [HttpDelete]
        [Route("IdPre/Delete")]
        public async Task<string> Delete(IdPreModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdPre.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("IdPre/GetOne")]
        public async Task<IdPreModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.IdPre.GetOne(id);
        }
        [HttpGet]
        [Route("IdPre/GetList")]
        public async Task<IEnumerable<IdPreModel>> GetList(string number, string user_id, string id_days, string id_daye,
            string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<IdPreModel>();
            return await DBOper.IdPre.GetList(number, user_id, id_days, id_daye, page_size, page_index);
        }
        [HttpGet]
        [Route("IdPre/GetCount")]
        public async Task<long> GetCount(string number, string user_id, string id_days, string id_daye,
            int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return 0;
            return await DBOper.IdPre.GetCount(number, user_id, id_days, id_daye);
        }
    }
}
