using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Util;

namespace Api.Controllers
{
    public class StockPlanTradeController : ApiController
    {
        [HttpPut]
        [Route("StockPlanTrade/Update")]
        public async Task<string> Update(StockPlanTradeModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.StockPlanTrade.Update(model.id, model.trade_time, model.price, model.amount, model.trade_no, model.access_id);
        }
        [HttpGet]
        [Route("StockPlanTrade/GetList")]
        public async Task<IEnumerable<StockPlanTradeModel>> GetList(string plan_id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<StockPlanTradeModel>();

            return await DBOper.StockPlanTrade.GetList(plan_id);
        }
        [HttpGet]
        [Route("StockPlanTrade/GetListUser")]
        [Authorize]
        public async Task<JsonResultModel<IEnumerable<StockPlanTradeModel>>> GetListUser(string plan_id)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<IEnumerable<StockPlanTradeModel>>(false, "获取用户失败", null);

            IEnumerable<StockPlanTradeModel> result = await DBOper.StockPlanTrade.GetList(plan_id);
            return new JsonResultModel<IEnumerable<StockPlanTradeModel>>(result != null, result != null ? string.Empty : "获取列表失败", result);
        }
        [HttpGet]
        [Route("StockPlanTrade/GetOne")]
        public async Task<StockPlanTradeModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.StockPlanTrade.GetOne(id);
        }
        [HttpDelete]
        [Route("StockPlanTrade/Delete")]
        public async Task<string> Delete(StockPlanTradeModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.StockPlanTrade.Delete(model.id, model.access_id);
        }
    }
}
