using Microsoft.Owin;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.Client;
using Util;

namespace Api.Controllers
{
    public class UserController : ApiController
    {
        #region 一般操作
        [HttpGet]
        [Route("User/LoginNm")]
        public async Task<JsonResultModel<UserModel>> LoginNm(string nick_name, string login_pwd)
        {
            UserModel user = await DBOper.User.GetOne(0, string.Empty, nick_name, string.Empty, false, false);
            if (user == null) user = await DBOper.User.GetOne(0, nick_name, string.Empty, string.Empty, false, false);
            if (user != null)
            {
                if (user.login_pwd.Equals(Md5.GetMd5(login_pwd)))
                {
                    TokenResponse rt = await WebApi.GetToken(user.id.ToString(), "id");
                    if (rt != null)
                    {
                        user.access_token = rt.AccessToken;
                        user.refresh_token = rt.RefreshToken;
                    }
                    user.login_pwd = string.Empty;
                    return new JsonResultModel<UserModel>(true, string.Empty, user);
                }
                else return new JsonResultModel<UserModel>(false, "密码错误", new UserModel { id = ulong.MinValue, nick_name = "密码错误" });
            }
            else return new JsonResultModel<UserModel>(false, "用户不存在", new UserModel { id = ulong.MinValue, nick_name = "用户不存在" });
        }
        [HttpGet]
        [Route("User/RegisterNm")]
        public async Task<JsonResultModel<UserModel>> RegisterNm(string nick_name, string login_pwd, string mobile, string code, ulong ref_id)
        {
            UserModel user = await DBOper.User.RegisterNm(nick_name, login_pwd, mobile, code, ref_id);
            if (user.id > 0)
            {
                TokenResponse rt = await WebApi.GetToken(user.id.ToString(), "id");
                if (rt != null)
                {
                    user.access_token = rt.AccessToken;
                    user.refresh_token = rt.RefreshToken;
                }
            }
            return new JsonResultModel<UserModel>(true, string.Empty, user);
        }
        [HttpPut]
        [Route("User/UpdateMobile")]
        [Authorize]
        public async Task<JsonResultModel<string>> UpdateMobile(UserModel user)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<string>() { msg = "获取用户失败" };

            string result = await DBOper.User.UpdateMobile(Convert.ToUInt64(userId), user.mobile, user.nick_name, user.name);
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("User/UpdateNickName")]
        [Authorize]
        public async Task<JsonResultModel<string>> UpdateNickName(UserModel user)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<string>() { msg = "获取用户失败" };
            string result = await DBOper.User.UpdateNickName(Convert.ToUInt64(userId), user.nick_name);
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("User/UpdatePwd")]
        [Authorize]
        public async Task<JsonResultModel<string>> UpdatePwd(UserModel user)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<string>() { msg = "获取用户失败" };
            string result = await DBOper.User.UpdatePwd(Convert.ToUInt64(userId), user.login_pwd, user.nick_name);
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("User/UpdatePwdMobile")]
        public async Task<JsonResultModel<string>> UpdatePwdMobile(UserModel user)
        {
            string result = await DBOper.User.UpdatePwdMobile(user.mobile, user.nick_name, user.login_pwd);
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("User/ResetPwd")]
        public async Task<IHttpActionResult> ResetPwd(UserModel user)
        {
            if (!await DBOper.SysUser.GetToken(user.access_id, user.access_token)) return Ok("请重新登录");
            if (!await DBOper.SysUser.CheckAuth(user.access_id, -1)) return Ok("权限异常");

            return Ok(await DBOper.User.ResetPwd(user.id, user.login_pwd));
        }
        [HttpPut]
        [Route("User/UpdateIdInfo")]
        [Authorize]
        public async Task<JsonResultModel<string>> UpdateIdInfo(UserModel user)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) new JsonResultModel<string>() { msg = "获取用户失败" };
            string result = await DBOper.User.UpdateIdInfo(Convert.ToUInt64(userId), user.name, user.id_card_no);
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("User/UpdateRef")]
        public async Task<string> UpdateRef(UserModel user)
        {
            if (!await DBOper.SysUser.GetToken(user.access_id, user.access_token)) return "请重新登录";
            if (user.access_id != 1 && user.access_id != 4 && user.access_id != 9 &&
                user.access_id != 19)
                return "无此权限";

            return await DBOper.User.UpdateRef(user.id, user.profit_from_ref, user.ref_id, user.delete_flag);
        }
        [HttpPut]
        [Route("User/UpdateBatchRef")]
        public async Task<string> UpdateBatchRef(UserModel user)
        {
            if (!await DBOper.SysUser.GetToken(user.access_id, user.access_token)) return "请重新登录";
            if (user.access_id != 1 && user.access_id != 4 && user.access_id != 9 &&
                user.access_id != 19)
                return "无此权限";

            return await DBOper.User.UpdateBatchRef(user.id, user.name);
        }
        [HttpGet]
        [Route("User/GetList")]
        public async Task<IEnumerable<UserModel>> GetList(string id, string mobile, string nick_name, string name,
            string id_card_no, string create_times, string create_timee,
            string ref_id, string moneys, string moneye, string cmp_id, string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<UserModel>();

            return await DBOper.User.GetList(id, mobile, nick_name, name, id_card_no, create_times, create_timee, ref_id,
                moneys, moneye, cmp_id, page_size, page_index);
        }
        [HttpGet]
        [Route("User/GetCount")]
        public async Task<long> GetCount(string id, string mobile, string nick_name, string name,
            string id_card_no, string create_times, string create_timee,
            string ref_id, string moneys, string moneye, string cmp_id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.User.GetCount(id, mobile, nick_name, name, id_card_no, create_times, create_timee, ref_id,
                moneys, moneye, cmp_id);
        }
        [HttpGet]
        [Route("User/Exp")]
        public async Task<IHttpActionResult> Exp(string id, string mobile, string nick_name, string name,
            string id_card_no, string create_times, string create_timee,
            string ref_id, string moneys, string moneye, string cmp_id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return Ok(string.Empty);

            return Ok(await DBOper.User.Exp(id, mobile, nick_name, name, id_card_no, create_times, create_timee,
                ref_id, moneys, moneye, cmp_id));
        }
        [HttpGet]
        [Route("User/GetSelf")]
        [Authorize]
        public async Task<JsonResultModel<UserModel>> GetSelf()
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<UserModel>() { msg = "获取用户失败" };
            UserModel result = await DBOper.User.GetOne(Convert.ToUInt64(userId), string.Empty, string.Empty, string.Empty, false, false);
            if (result != null) result.login_pwd = string.Empty;
            return new JsonResultModel<UserModel>(result != null, string.Empty, result);
        }
        [HttpGet]
        [Route("User/GetOne")]
        public async Task<UserModel> GetOne(string id, string mobile, string nick_name, string id_card_no, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.User.GetOne(string.IsNullOrWhiteSpace(id) ? 0 : Convert.ToUInt64(id), mobile, nick_name, id_card_no, true);
        }
        [HttpPost]
        [Route("User/InsIp")]
        public async Task InsIp()
        {
            string ip = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            await DBOper.User.InsertIp(ip);
        }
        [HttpGet]
        [Route("User/GetTop")]
        public async Task<IEnumerable<UserModel>> GetTop()
        {
            return await DBOper.User.GetTop();
        }
        #endregion
    }
}
