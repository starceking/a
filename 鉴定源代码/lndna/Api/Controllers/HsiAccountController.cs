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
    public class HsiAccountController : ApiController
    {
        [HttpPost]
        [Route("HsiAccount/Insert")]
        public async Task<string> Insert(HsiAccountModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiAccount.Insert(model.src_id, model.calc_id, model.number);
        }
        [HttpPut]
        [Route("HsiAccount/Update")]
        public async Task<string> Update(HsiAccountModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiAccount.Update(model.id, model.src_id, model.calc_id, model.number);
        }
        [HttpDelete]
        [Route("HsiAccount/Delete")]
        public async Task<string> Delete(HsiAccountModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, 0)) return "权限异常";

            return await DBOper.HsiAccount.Delete(model.id);
        }
        [HttpGet]
        [Route("HsiAccount/GetOne")]
        public async Task<HsiAccountModel> GetOne(int id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.HsiAccount.GetOne(id);
        }
        [HttpGet]
        [Route("HsiAccount/GetList")]
        public async Task<IEnumerable<HsiAccountModel>> GetList(string src_id, string number,
           string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<HsiAccountModel>();

            return await DBOper.HsiAccount.GetList(src_id, number, page_size, page_index);
        }
        [HttpGet]
        [Route("HsiAccount/GetCount")]
        public async Task<long> GetCount(string src_id, string number, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.HsiAccount.GetCount(src_id, number);
        }
        [HttpGet]
        [Route("HsiAccount/GetJson")]
        public async Task<string> GetJson(int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return string.Empty;

            return await DBOper.HsiAccount.GetJson();
        }
    }
}
