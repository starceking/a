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
    public class HsiAccountSubSubController : ApiController
    {
        [HttpPost]
        [Route("HsiAccountSub/Insert")]
        public async Task<string> Insert(HsiAccountSubModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiAccountSub.Insert(model.account_id, model.number, model.pwd);
        }
        [HttpPut]
        [Route("HsiAccountSub/Update")]
        public async Task<string> Update(HsiAccountSubModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiAccountSub.Update(model.id, model.number, model.pwd);
        }
        [HttpDelete]
        [Route("HsiAccountSub/Delete")]
        public async Task<string> Delete(HsiAccountSubModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiAccountSub.Delete(model.id);
        }
        [HttpGet]
        [Route("HsiAccountSub/GetOne")]
        public async Task<HsiAccountSubModel> GetOne(int id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.HsiAccountSub.GetOne(id, string.Empty);
        }
        [HttpGet]
        [Route("HsiAccountSub/GetList")]
        public async Task<IEnumerable<HsiAccountSubModel>> GetList(string account_id, string number, string user_mobile,
           string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<HsiAccountSubModel>();

            return await DBOper.HsiAccountSub.GetList(account_id, number, user_mobile, page_size, page_index);
        }
        [HttpGet]
        [Route("HsiAccountSub/GetCount")]
        public async Task<long> GetCount(string account_id, string number, string user_mobile, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.HsiAccountSub.GetCount(account_id, number, user_mobile);
        }
    }
}
