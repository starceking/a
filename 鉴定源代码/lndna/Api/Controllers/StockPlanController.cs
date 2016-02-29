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
    public class StockPlanController : ApiController
    {
        [HttpPost]
        [Route("StockPlan/Insert")]
        [Authorize]
        public async Task<JsonResultModel<string>> Insert(StockPlanModel model)
        {
            //点买
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) new JsonResultModel<string>() { msg = "获取用户失败" };
            string result = await DBOper.StockPlan.Insert(Convert.ToUInt64(userId), model.stock_no, model.stock_name, model.money_debt,
                model.stock_amount, model.stop_earn_percent, model.money_margin, 0);
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("StockPlan/SetSysUser")]
        public async Task<string> SetSysUser(StockPlanModel model)
        {
            //管理员接单plan_status_id=2、流单plan_status_id=-1
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.StockPlan.SetSysUser(model.id, model.access_id, model.plan_status_id, model.remark, true);
        }
        [HttpPost]
        [Route("StockPlan/Buy")]
        public async Task<string> Buy(StockPlanTradeModel model)
        {
            //管理员买入
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.StockPlan.Buy(model.plan_id, model.trade_time, model.price, model.amount, model.trade_no, model.access_id,
                string.Empty);
        }
        [HttpPut]
        [Route("StockPlan/PreSell")]
        [Authorize]
        public async Task<JsonResultModel<string>> PreSell(StockPlanModel model)
        {
            //申请点卖
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) new JsonResultModel<string>() { msg = "获取用户失败" };
            string result = await DBOper.StockPlan.PreSell(model.id, model.remark, Convert.ToUInt64(userId), false);
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("StockPlan/CancelPreSell")]
        public async Task<string> CancelPreSell(StockPlanModel model)
        {
            //管理员撤销平仓
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.StockPlan.CancelPreSell(model.id, model.remark, 0, model.access_id, true);
        }
        [HttpPost]
        [Route("StockPlan/Sell")]
        public async Task<string> Sell(StockPlanTradeModel model)
        {
            //管理员卖出
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.StockPlan.Sell(model.plan_id, model.trade_time, model.price, model.amount, model.trade_no, model.access_id,
                string.Empty);
        }
        [HttpPut]
        [Route("StockPlan/Calc")]
        public async Task<string> Calc(StockPlanModel model)
        {
            //结算审核通过
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.StockPlan.Calc(model.id, model.access_id);
        }
        [HttpGet]
        [Route("StockPlan/GetListUser")]
        [Authorize]
        public async Task<JsonResultModel<IEnumerable<StockPlanModel>>> GetListUser(string plan_status_id, string profit,
            string start_dates, string start_datee, string end_dates, string end_datee,
            string stock_no, string stock_name, string money_debts, string money_debte, string page_size, string page_index)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<IEnumerable<StockPlanModel>>(false, "获取用户失败", null);
            IEnumerable<StockPlanModel> result = await DBOper.StockPlan.GetList(string.Empty, userId, plan_status_id, profit, start_dates,
                start_datee, end_dates, end_datee, stock_no, stock_name, money_debts, money_debte, string.Empty, "id desc", page_size, page_index);
            return new JsonResultModel<IEnumerable<StockPlanModel>>(result != null, result != null ? string.Empty : "获取列表失败", result);
        }
        [HttpGet]
        [Route("StockPlan/GetCountUser")]
        [Authorize]
        public async Task<JsonResultModel<long>> GetCountUser(string plan_status_id, string profit,
            string start_dates, string start_datee, string end_dates, string end_datee,
            string stock_no, string stock_name, string money_debts, string money_debte)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<long>();
            long result = await DBOper.StockPlan.GetCount(string.Empty, userId, plan_status_id, profit, start_dates,
                start_datee, end_dates, end_datee, stock_no, stock_name, money_debts, money_debte, string.Empty);
            return new JsonResultModel<long>(result == 0, string.Empty, result);
        }
        [HttpGet]
        [Route("StockPlan/GetOneUser")]
        [Authorize]
        public async Task<JsonResultModel<StockPlanModel>> GetOneUser(ulong id)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<StockPlanModel>(false, "获取用户失败", null); ;
            StockPlanModel result = await DBOper.StockPlan.GetOne(id, Convert.ToUInt64(userId), true);
            return new JsonResultModel<StockPlanModel>(result != null, result != null ? string.Empty : "获取信息失败", result);
        }
        [HttpGet]
        [Route("StockPlan/GetList")]
        public async Task<IEnumerable<StockPlanModel>> GetList(string id, string user_id, string plan_status_id, string profit,
            string start_dates, string start_datee, string end_dates, string end_datee,
            string stock_no, string stock_name, string money_debts, string money_debte, string order, string page_size, string page_index,
            int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<StockPlanModel>();

            return await DBOper.StockPlan.GetList(id, user_id, plan_status_id, profit, start_dates,
                    start_datee, end_dates, end_datee, stock_no, stock_name, money_debts, money_debte,
                    access_id.ToString(), order, page_size, page_index);
        }
        [HttpGet]
        [Route("StockPlan/GetCount")]
        public async Task<long> GetCount(string id, string user_id, string plan_status_id, string profit,
            string start_dates, string start_datee, string end_dates, string end_datee,
            string stock_no, string stock_name, string money_debts, string money_debte,
            int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.StockPlan.GetCount(id, user_id, plan_status_id, profit, start_dates, start_datee, end_dates, end_datee,
                stock_no, stock_name, money_debts, money_debte, access_id.ToString());
        }
        [HttpGet]
        [Route("StockPlan/Exp")]
        public async Task<IHttpActionResult> Exp(string id, string user_id, string plan_status_id, string profit,
            string start_dates, string start_datee, string end_dates, string end_datee,
            string stock_no, string stock_name, string money_debts, string money_debte, string order, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return Ok(string.Empty);

            return Ok(await DBOper.StockPlan.Exp(id, user_id, plan_status_id, profit, start_dates,
                    start_datee, end_dates, end_datee, stock_no, stock_name, money_debts, money_debte,
                    access_id.ToString(), order));
        }
        [HttpGet]
        [Route("StockPlan/GetOne")]
        public async Task<StockPlanModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.StockPlan.GetOne(id, 0, true);
        }
        [HttpPut]
        [Route("StockPlan/ReCalc")]
        public async Task<string> ReCalc(StockPlanModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "登录超时";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            await DBOper.StockPlan.ReCalc(model.id, model.access_id);
            return string.Empty;
        }
        [HttpGet]
        [Route("StockPlan/GetTask")]
        public async Task<string> GetTask(int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.StockPlan.GetTask(access_id);
        }
        [HttpGet]
        [Route("StockPlan/GetTodayAmount")]
        [Authorize]
        public async Task<JsonResultModel<int>> GetTodayAmount()
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<int>() { msg = "获取用户失败" };
            int result = await DBOper.StockPlan.GetTodayAmount(Convert.ToUInt64(userId));
            return new JsonResultModel<int>(true, string.Empty, result);
        }
        [HttpGet]
        [Route("StockPlan/GetTop")]
        public async Task<IEnumerable<StockPlanModel>> GetTop()
        {
            return await DBOper.StockPlan.GetTop();
        }
        [HttpGet]
        [Route("StockPlan/GetJsData")]
        [Authorize]
        public async Task<IEnumerable<DBOper.StockPlan.StockPlanJsModel>> GetJsData()
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return null;
            return await DBOper.StockPlan.GetJsData(Convert.ToUInt64(userId));
        }
        [HttpGet]
        [Route("StockPlan/ExpToday")]
        public async Task<IHttpActionResult> ExpToday(string start_dates, string start_datee, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return Ok(string.Empty);

            return Ok(await DBOper.StockPlan.ExpToday(access_id.ToString(), start_dates, start_datee));
        }
        [HttpPut]
        [Route("StockPlan/UpdatePoUser")]
        public async Task<string> UpdatePoUser(StockPlanModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.StockPlan.UpdatePoUser(model.id, model.po_user);
        }
    }
}
