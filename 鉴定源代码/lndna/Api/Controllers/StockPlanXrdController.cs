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
    public class StockPlanXrdController : ApiController
    {
        [HttpPost]
        [Route("StockPlanXrd/Insert")]
        public async Task<string> Insert(StockPlanXrdModel xrd)
        {
            if (!await DBOper.SysUser.GetToken(xrd.access_id, xrd.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(xrd.access_id, 0)) return "权限异常";

            return await DBOper.StockPlanXrd.Insert(xrd.stock_no, xrd.stock_name, xrd.amount, xrd.access_id);
        }
        [HttpPut]
        [Route("StockPlanXrd/Cancel")]
        public async Task<string> Cancel(StockPlanXrdModel xrd)
        {
            if (!await DBOper.SysUser.GetToken(xrd.access_id, xrd.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(xrd.access_id, 0)) return "权限异常";

            return await DBOper.StockPlanXrd.Cancel(xrd.id);
        }
        [HttpGet]
        [Route("StockPlanXrd/GetList")]
        public async Task<IEnumerable<StockPlanXrdModel>> GetList(string create_times, string create_timee,
             string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<StockPlanXrdModel>();

            return await DBOper.StockPlanXrd.GetList(create_times, create_timee, page_size, page_index);
        }
        [HttpGet]
        [Route("StockPlanXrd/GetCount")]
        public async Task<long> GetCount(string create_times, string create_timee, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.StockPlanXrd.GetCount(create_times, create_timee);
        }
    }
}
