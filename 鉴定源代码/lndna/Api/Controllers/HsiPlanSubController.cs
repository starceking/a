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
    public class HsiPlanSubController : ApiController
    {
        [HttpPost]
        [Route("HsiPlanSub/Insert")]
        [Authorize]
        public async Task<JsonResultModel<string>> Insert(HsiPlanSubModel model)
        {
            //点买
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) new JsonResultModel<string>() { msg = "获取用户失败" };
            string result = await DBOper.HsiPlanSub.Insert(Convert.ToUInt64(userId), model.stock_amount);
            return new JsonResultModel<string>(result.StartsWith("hasm:"), result, result.Replace("hasm:", string.Empty));
        }
        [HttpPut]
        [Route("HsiPlanSub/InsMoney")]
        public async Task<string> InsMoney(HsiPlanSubModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiPlanSub.InsMoney(model.id);
        }
        [HttpPut]
        [Route("HsiPlanSub/Cancel")]
        [Authorize]
        public async Task<JsonResultModel<string>> Cancel(HsiPlanSubModel model)
        {
            //点买
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) new JsonResultModel<string>() { msg = "获取用户失败" };
            string result = await DBOper.HsiPlanSub.Cancel(model.id, Convert.ToUInt64(userId));
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("HsiPlanSub/Abandon")]
        public async Task<string> Abandon(HsiPlanSubModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiPlanSub.Abandon(model.id, model.access_id);
        }
        [HttpPut]
        [Route("HsiPlanSub/InsMoneyOk")]
        public async Task<string> InsMoneyOk(HsiPlanSubModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiPlanSub.InsMoneyOk(model.id, model.access_id);
        }
        [HttpPut]
        [Route("HsiPlanSub/PreSell")]
        [Authorize]
        public async Task<JsonResultModel<string>> PreSell(HsiPlanSubModel model)
        {
            //点买
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) new JsonResultModel<string>() { msg = "获取用户失败" };
            string result = await DBOper.HsiPlanSub.PreSell(model.id, Convert.ToUInt64(userId));
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("HsiPlanSub/Sell")]
        public async Task<string> Sell(HsiPlanSubModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiPlanSub.Sell(model.id, model.profit, model.access_id);
        }
        [HttpPut]
        [Route("HsiPlanSub/UpdateProfit")]
        public async Task<string> UpdateProfit(HsiPlanSubModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiPlanSub.UpdateProfit(model.id, model.profit, model.access_id);
        }
        [HttpGet]
        [Route("HsiPlanSub/GetList")]
        public async Task<IEnumerable<HsiPlanSubModel>> GetList(string user_id, string hsi_account_id, string hsi_account_sub_number,
            string plan_status_id, string create_dates, string create_datee,
            string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<HsiPlanSubModel>();

            return await DBOper.HsiPlanSub.GetList(user_id, hsi_account_id, hsi_account_sub_number, plan_status_id,
                create_dates, create_datee, page_size, page_index);
        }
        [HttpGet]
        [Route("HsiPlanSub/GetCount")]
        public async Task<long> GetCount(string user_id, string hsi_account_id, string hsi_account_sub_number,
            string plan_status_id, string create_dates, string create_datee, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.HsiPlanSub.GetCount(user_id, hsi_account_id, hsi_account_sub_number, plan_status_id,
                create_dates, create_datee);
        }
        [HttpGet]
        [Route("HsiPlanSub/GetListUser")]
        [Authorize]
        public async Task<JsonResultModel<IEnumerable<HsiPlanSubModel>>> GetListUser(string hsi_account_id, string hsi_account_sub_id,
            string plan_status_id, string create_dates, string create_datee, string page_size, string page_index)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<IEnumerable<HsiPlanSubModel>>(false, "获取用户失败", null);
            IEnumerable<HsiPlanSubModel> result = await DBOper.HsiPlanSub.GetList(userId, hsi_account_id, hsi_account_sub_id, plan_status_id,
                create_dates, create_datee, page_size, page_index);
            return new JsonResultModel<IEnumerable<HsiPlanSubModel>>(result != null, result != null ? string.Empty : "获取列表失败", result);
        }
        [HttpGet]
        [Route("HsiPlanSub/GetCountUser")]
        [Authorize]
        public async Task<JsonResultModel<long>> GetCountUser(string hsi_account_id, string hsi_account_sub_id,
            string plan_status_id, string create_dates, string create_datee)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<long>();
            long result = await DBOper.HsiPlanSub.GetCount(userId, hsi_account_id, hsi_account_sub_id, plan_status_id,
                create_dates, create_datee);
            return new JsonResultModel<long>(result == 0, string.Empty, result);
        }
        [HttpGet]
        [Route("HsiPlanSub/GetOne")]
        public async Task<HsiPlanSubModel> GetOne(ulong id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.HsiPlanSub.GetOne(id, 0);
        }
        [HttpGet]
        [Route("HsiPlanSub/GetOneUser")]
        [Authorize]
        public async Task<JsonResultModel<HsiPlanSubModel>> GetOneUser(ulong id)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<HsiPlanSubModel>(false, "获取用户失败", null); ;
            HsiPlanSubModel result = await DBOper.HsiPlanSub.GetOne(id, Convert.ToUInt64(userId));
            return new JsonResultModel<HsiPlanSubModel>(result != null, result != null ? string.Empty : "获取信息失败", result);
        }
        [HttpGet]
        [Route("HsiPlanSub/GetTask")]
        public async Task<string> GetTask(int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.HsiPlanSub.GetTask();
        }
    }
}
