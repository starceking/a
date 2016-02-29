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
    public class StockPlanLogController : ApiController
    {
        [HttpGet]
        [Route("StockPlanLog/GetList")]
        public async Task<IEnumerable<StockPlanLogModel>> GetList(string plan_id,
             string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<StockPlanLogModel>();

            return await DBOper.StockPlanLog.GetList(plan_id, page_size, page_index);
        }
        [HttpGet]
        [Route("StockPlanLog/GetCount")]
        public async Task<long> GetCount(string plan_id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.StockPlanLog.GetCount(plan_id);
        }
    }
}
