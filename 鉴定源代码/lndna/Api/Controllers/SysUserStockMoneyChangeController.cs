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
    public class SysUserStockMoneyChangeController : ApiController
    {
        [HttpPut]
        [Route("SysUserStockMoneyChange/Update")]
        public async Task<IHttpActionResult> Update(SysUserStockMoneyChangeModel umc)
        {
            if (!await DBOper.SysUser.GetToken(umc.access_id, umc.access_token)) return Ok("请重新登录");
            if (umc.access_id != 1 && umc.access_id != 4 && umc.access_id != 9) return Ok("权限不足");

            return Ok(await DBOper.SysUserStockMoneyChange.Update(umc.user_id, umc.money, umc.info, umc.ref_table, umc.ref_id, umc.access_id));
        }
        [HttpGet]
        [Route("SysUserStockMoneyChange/GetListUser")]
        [Authorize]
        public async Task<JsonResultModel<IEnumerable<SysUserStockMoneyChangeModel>>> GetListUser(string money_flow_id,
             string ref_table, string ref_id, string create_times, string create_timee,
             string page_size, string page_index)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<IEnumerable<SysUserStockMoneyChangeModel>>() { msg = "获取用户失败" };

            IEnumerable<SysUserStockMoneyChangeModel> result = await DBOper.SysUserStockMoneyChange.GetList(userId, money_flow_id, ref_table, ref_id,
               create_times, create_timee, page_size, page_index);
            return new JsonResultModel<IEnumerable<SysUserStockMoneyChangeModel>>(result != null, string.Empty, result);
        }
        [HttpGet]
        [Route("SysUserStockMoneyChange/GetCountUser")]
        [Authorize]
        public async Task<JsonResultModel<long>> GetCountUser(string money_flow_id,
            string ref_table, string ref_id, string create_times, string create_timee)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<long>() { msg = "获取用户失败" };
            long result = await DBOper.SysUserStockMoneyChange.GetCount(userId, money_flow_id, ref_table, ref_id,
               create_times, create_timee);
            return new JsonResultModel<long>(result >= 0, string.Empty, result);
        }
        [HttpGet]
        [Route("SysUserStockMoneyChange/GetList")]
        public async Task<IEnumerable<SysUserStockMoneyChangeModel>> GetList(string user_id, string money_flow_id,
            string ref_table, string ref_id, string create_times, string create_timee,
            string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token))
                return new List<SysUserStockMoneyChangeModel>();

            return await DBOper.SysUserStockMoneyChange.GetList(user_id, money_flow_id, ref_table, ref_id,
                 create_times, create_timee, page_size, page_index);
        }
        [HttpGet]
        [Route("SysUserStockMoneyChange/GetCount")]
        public async Task<long> GetCount(string user_id, string money_flow_id, string ref_table,
            string ref_id, string create_times, string create_timee, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.SysUserStockMoneyChange.GetCount(user_id, money_flow_id, ref_table, ref_id,
               create_times, create_timee);
        }
        [HttpGet]
        [Route("SysUserStockMoneyChange/Exp")]
        public async Task<IHttpActionResult> Exp(string user_id, string money_flow_id, string ref_table,
            string ref_id, string create_times, string create_timee, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return Ok(string.Empty);

            return Ok(await DBOper.SysUserStockMoneyChange.Exp(user_id, money_flow_id, ref_table, ref_id,
               create_times, create_timee));
        }
    }
}
