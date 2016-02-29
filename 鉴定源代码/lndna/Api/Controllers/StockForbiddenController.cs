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
    public class StockForbiddenController : ApiController
    {
        [HttpPost]
        [Route("StockForbidden/Insert")]
        public async Task<string> Insert(StockForbiddenModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.StockForbidden.Insert(model.number, model.name, model.reason_id);
        }
        [HttpDelete]
        [Route("StockForbidden/Delete")]
        public async Task<string> Delete(StockForbiddenModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.StockForbidden.Delete(model.id, model.number);
        }
        [HttpGet]
        [Route("StockForbidden/GetList")]
        public async Task<IEnumerable<StockForbiddenModel>> GetList()
        {
            return await DBOper.StockForbidden.GetList();
        }
    }
}
