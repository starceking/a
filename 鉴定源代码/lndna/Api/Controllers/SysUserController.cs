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
    public class SysUserController : ApiController
    {
        [HttpGet]
        [Route("SysUser/Login")]
        public async Task<SysUserModel> Login(string login_name, string login_pwd)
        {
            SysUserModel user = await DBOper.SysUser.Login(login_name, login_pwd);
            if (user != null)
            {
                string token = string.Empty;
                if (user.user_id > 0)
                    token = DBOper.SysUser.SetTokenTzr(user.id);
                else
                    token = await DBOper.SysUser.SetToken(user.id);
                user.access_id = user.id;
                user.access_token = token;
            }
            return user;
        }
        [HttpPost]
        [Route("SysUser/Insert")]
        public async Task<string> Insert(SysUserModel user)
        {
            if (!await DBOper.SysUser.GetToken(user.access_id, user.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(user.access_id, 0)) return "权限异常";

            return await DBOper.SysUser.Insert(user.login_name, user.login_pwd, user.mobile, user.name, user.user_id);
        }
        [HttpPut]
        [Route("SysUser/Update")]
        public async Task<string> Update(SysUserModel user)
        {
            if (!await DBOper.SysUser.GetToken(user.access_id, user.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(user.access_id, 0)) return "权限异常";

            return await DBOper.SysUser.Update(user.id, user.mobile, user.name, user.delete_flag);
        }
        [HttpPut]
        [Route("SysUser/UpdatePwd")]
        public async Task<string> UpdatePwd(SysUserModel user)
        {
            if (!await DBOper.SysUser.GetToken(user.access_id, user.access_token)) return "请重新登录";

            return await DBOper.SysUser.UpdatePwd(user.id, user.login_pwd, user.name);
        }
        [HttpPut]
        [Route("SysUser/ResetPwd")]
        public async Task<string> ResetPwd(SysUserModel user)
        {
            if (!await DBOper.SysUser.GetToken(user.access_id, user.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(user.access_id, 0)) return "权限异常";

            return await DBOper.SysUser.ResetPwd(user.id, user.login_pwd);
        }
        [HttpGet]
        [Route("SysUser/GetOne")]
        public async Task<SysUserModel> GetOne(int id, string login_name, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.SysUser.GetOne(id, login_name);
        }
        [HttpGet]
        [Route("SysUser/GetList")]
        public async Task<IEnumerable<SysUserModel>> GetList(string login_name, string mobile, string name, string user_id,
             string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<SysUserModel>();

            return await DBOper.SysUser.GetList(login_name, mobile, name, user_id, page_size, page_index);
        }
        [HttpGet]
        [Route("SysUser/GetCount")]
        public async Task<long> GetCount(string login_name, string mobile, string name, string user_id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.SysUser.GetCount(login_name, mobile, name, user_id);
        }
        [HttpPost]
        [Route("SysUser/InsertSys")]
        public async Task<string> InsertSys(SysUserModel user)
        {
            if (!await DBOper.SysUser.GetToken(user.access_id, user.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(user.access_id, 0)) return "权限异常";

            return await DBOper.SysUser.InsertSys(user.login_name, user.login_pwd, user.mobile, user.name, user.privilege_ids, user.cmp_id);
        }
        [HttpPut]
        [Route("SysUser/UpdateSys")]
        public async Task<string> UpdateSys(SysUserModel user)
        {
            if (!await DBOper.SysUser.GetToken(user.access_id, user.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(user.access_id, 0)) return "权限异常";

            return await DBOper.SysUser.UpdateSys(user.id, user.mobile, user.name, user.privilege_ids, user.cmp_id, user.delete_flag);
        }
        [HttpGet]
        [Route("SysUser/GetStockMoneys")]
        public async Task<JsonResultModel<string>> GetStockMoneys()
        {
            string result = await DBOper.SysUser.GetStockMoneys();
            return new JsonResultModel<string>(true, string.Empty, result);
        }
        [HttpGet]
        [Route("SysUser/GetTop")]
        public async Task<string> GetTop()
        {
            return await DBOper.SysUser.GetTop();
        }
    }
}
