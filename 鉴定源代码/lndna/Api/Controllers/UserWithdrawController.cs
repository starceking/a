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
    public class UserWithdrawController : ApiController
    {
        [HttpPost]
        [Route("UserWithdraw/Insert")]
        [Authorize]
        public async Task<JsonResultModel<string>> Insert(UserWithdrawModel model)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<string>() { msg = "获取用户失败" };

            string result = await DBOper.UserWithdraw.Insert(Convert.ToUInt64(userId), model.user_bank_id, model.money);
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("UserWithdraw/Finish")]
        public async Task<string> Finish(UserWithdrawModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.UserWithdraw.Finish(model.id, model.access_id, model.process_status_id, model.remark);
        }
        [HttpGet]
        [Route("UserWithdraw/GetList")]
        public async Task<IEnumerable<UserWithdrawModel>> GetList(string user_id, string sys_user_id, string process_status_id,
            string create_times, string create_timee, string finish_times, string finish_timee,
            string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token))
                return new List<UserWithdrawModel>();

            return await DBOper.UserWithdraw.GetList(user_id, sys_user_id, process_status_id, create_times,
                 create_timee, finish_times, finish_timee, page_size, page_index);
        }
        [HttpGet]
        [Route("UserWithdraw/GetCount")]
        public async Task<long> GetCount(string user_id, string sys_user_id, string process_status_id,
            string create_times, string create_timee, string finish_times, string finish_timee, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.UserWithdraw.GetCount(user_id, sys_user_id, process_status_id, create_times,
                 create_timee, finish_times, finish_timee);
        }
        [HttpGet]
        [Route("UserWithdraw/Exp")]
        public async Task<IHttpActionResult> Exp(string user_id, string sys_user_id, string process_status_id,
            string create_times, string create_timee, string finish_times, string finish_timee, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return Ok(string.Empty);

            return Ok(await DBOper.UserWithdraw.Exp(user_id, sys_user_id, process_status_id, create_times,
                 create_timee, finish_times, finish_timee));
        }
    }
}
