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
    public class UserDepositHisController : ApiController
    {
        [HttpPost]
        [Route("UserDepositHis/Deposit")]
        public async Task<IHttpActionResult> Deposit(UserDepositHisModel udh)
        {
            if (!await DBOper.SysUser.GetToken(udh.access_id, udh.access_token)) return Ok("请重新登录");
            if (!await DBOper.SysUser.CheckAuth(udh.access_id, 0)) return Ok("权限异常");

            return Ok(await DBOper.UserDepositHis.Deposit(udh.user_id, udh.money,
                udh.info, udh.number, udh.deposit_src_id, udh.access_id));
        }
        [HttpGet]
        [Route("UserDepositHis/GetList")]
        public async Task<IEnumerable<UserDepositHisModel>> GetList(string number, string deposit_src_id, string user_id,
            string create_times, string create_timee,
            string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token))
                return new List<UserDepositHisModel>();

            return await DBOper.UserDepositHis.GetList(number, deposit_src_id, user_id, create_times,
                 create_timee, page_size, page_index);
        }
        [HttpGet]
        [Route("UserDepositHis/GetCount")]
        public async Task<long> GetCount(string number, string deposit_src_id, string user_id,
            string create_times, string create_timee, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.UserDepositHis.GetCount(number, deposit_src_id, user_id, create_times,
                 create_timee);
        }
        [HttpGet]
        [Route("UserDepositHis/Exp")]
        public async Task<IHttpActionResult> Exp(string number, string deposit_src_id, string user_id,
            string create_times, string create_timee, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return Ok(string.Empty);

            return Ok(await DBOper.UserDepositHis.Exp(number, deposit_src_id, user_id, create_times,
                 create_timee));
        }
    }
}
