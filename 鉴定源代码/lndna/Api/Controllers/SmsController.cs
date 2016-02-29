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
    public class SmsController : ApiController
    {
        //[HttpGet]
        //[Route("Sms/Send")]
        //public async Task<JsonResultModel<string>> Send(string mobile, string type)
        //{
        //    if (string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(type)) return new JsonResultModel<string>() { msg = "参数不全" };
        //    string code = string.Empty;
        //    string msg = string.Empty;

        //    string ip = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
        //    if (!Util.Mobile.SmsCanIp(ip, 50, 20)) return new JsonResultModel<string>() { msg = "该ip调用接口太频繁" };
        //    if (mobile.Length != 11) return new JsonResultModel<string>() { msg = "手机号格式错误" };
        //    if (!Util.Mobile.SmsCanIp(mobile, 50, 5)) return new JsonResultModel<string>() { msg = "该号码调用接口太频繁" };

        //    UserModel user = await DBOper.User.GetOne(0, mobile, string.Empty, string.Empty);
        //    if (type.Equals("Register"))
        //    {
        //        if (user != null) return new JsonResultModel<string>() { msg = "该手机号已注册" };
        //        code = await Util.Mobile.SetCode(mobile);
        //        msg = ("您正在使用手机号注册，验证码：" + code);
        //    }
        //    else if (type.Equals("ForgetPwd"))
        //    {
        //        if (user == null) return new JsonResultModel<string>() { msg = "该手机号未注册" };
        //        code = await Util.Mobile.SetCode(mobile);
        //        msg = ("您正在使用密码找回功能，验证码：" + code);
        //    }
        //    else if (type.Equals("UpdateMobile"))
        //    {
        //        if (user != null) return new JsonResultModel<string>() { msg = "该手机号已注册" };
        //        code = await Util.Mobile.SetCode(mobile);
        //        msg = ("您正在绑定手机号，验证码为：" + code);
        //    }
        //    string result = await Util.Mobile.SendSms(mobile, msg);

        //    Util.Mobile.SetCanIp(ip);
        //    Util.Mobile.SetCanIp(mobile);

        //    return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        //}
        [HttpGet]
        [Route("Sms/SendAuth")]
        [Authorize]
        public async Task<string> SendAuth(string mobile, string type)
        {
            if (string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(type)) return "参数不全";
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return "参数不全";

            string ip = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            if (!Util.Mobile.SmsCanIp(ip, 50, 20)) return "该ip调用接口太频繁";
            if (mobile.Length != 11) return "手机号格式错误";
            if (!Util.Mobile.SmsCanIp(mobile, 50, 5)) return "该号码调用接口太频繁";

            string code = await Util.Mobile.SetCode(mobile);
            string msg = string.Empty;
            if (type.Equals("UpdateMobile")) msg = ("您正在绑定手机号，验证码为：" + code);
            else if (type.Equals("UpdateSPM")) msg = ("您正在修改提现密码，验证码：" + code + "，请输入验证码完成修改。");
            string result = await Util.Mobile.SendSms(mobile, msg);

            Util.Mobile.SetCanIp(ip);
            Util.Mobile.SetCanIp(mobile);

            return result;
        }
        [HttpGet]
        [Route("Sms/ValidCode")]
        public async Task<string> ValidCode(string mobile, string code)
        {
            if (string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(code)) return "参数不全";

            if (await Mobile.GetCode(mobile, code)) return string.Empty;
            return "验证码错误";
        }
    }
}
