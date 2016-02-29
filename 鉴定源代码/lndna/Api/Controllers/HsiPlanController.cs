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
    public class HsiPlanController : ApiController
    {
        [HttpPut]
        [Route("HsiPlan/PreSettle")]
        public async Task<string> PreSettle(HsiPlanModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiPlan.PreSettle(model.id, model.oper_amount, model.access_id);
        }
        [HttpPut]
        [Route("HsiPlan/Settle")]
        public async Task<string> Settle(HsiPlanModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiPlan.Settle(model.id, model.access_id);
        }
        [HttpPut]
        [Route("HsiPlan/UpdateOperAmount")]
        public async Task<string> UpdateOperAmount(HsiPlanModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiPlan.UpdateOperAmount(model.id, model.oper_amount);
        }
        [HttpGet]
        [Route("HsiPlan/GetList")]
        public async Task<IEnumerable<HsiPlanModel>> GetList(string hsi_account_id, string plan_status_id,
            string create_dates, string create_datee, string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<HsiPlanModel>();

            return await DBOper.HsiPlan.GetList(hsi_account_id, plan_status_id, create_dates, create_datee, page_size, page_index);
        }
        [HttpGet]
        [Route("HsiPlan/GetCount")]
        public async Task<long> GetCount(string hsi_account_id, string plan_status_id,
            string create_dates, string create_datee, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.HsiPlan.GetCount(hsi_account_id, plan_status_id, create_dates, create_datee);
        }
        [HttpGet]
        [Route("HsiPlan/GetOne")]
        public async Task<HsiPlanModel> GetOne(ulong id, int hsi_account_id, DateTime? create_date, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.HsiPlan.GetOne(id, hsi_account_id, create_date, true);
        }
    }
}
