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
    public class IdComparisonController : ApiController
    {
        [HttpPost]
        [Route("IdComparison/Insert")]
        public async Task<string> Insert(IdComparisonModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdComparison.Insert(model.number, model.name, model.amount, model.ref_table, model.ref_id, model.access_id);
        }
        [HttpPut]
        [Route("IdComparison/Update")]
        public async Task<string> Update(IdComparisonModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdComparison.Update(model.id, model.amount, model.access_id);
        }
        [HttpDelete]
        [Route("IdComparison/Delete")]
        public async Task<string> Delete(IdComparisonModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            return await DBOper.IdComparison.Delete(model.id, model.access_id);
        }
        [HttpGet]
        [Route("IdComparison/GetOne")]
        public async Task<IdComparisonModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.IdComparison.GetOne(id);
        }
        [HttpGet]
        [Route("IdComparison/GetList")]
        public async Task<IEnumerable<IdComparisonModel>> GetList(string ref_table, string ref_id, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<IdComparisonModel>();
            return await DBOper.IdComparison.GetList(ref_table, ref_id);
        }
    }
}
