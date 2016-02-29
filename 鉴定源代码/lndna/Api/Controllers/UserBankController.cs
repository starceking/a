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
    public class UserBankController : ApiController
    {
        [HttpPost]
        [Route("UserBank/Insert")]
        [Authorize]
        public async Task<JsonResultModel<string>> Insert(UserBankModel model)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<string>() { msg = "获取用户失败" };

            string result = await DBOper.UserBank.Insert(Convert.ToUInt64(userId), model.bank_id, model.province_id, model.city_id,
                model.district_id, model.branch_name, model.card_no, model.default_flag);
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpPut]
        [Route("UserBank/Update")]
        [Authorize]
        public async Task<JsonResultModel<string>> Update(UserBankModel model)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<string>() { msg = "获取用户失败" };

            string result = await DBOper.UserBank.Update(model.id, model.bank_id, model.province_id, model.city_id,
                model.district_id, model.branch_name, model.card_no, model.default_flag, Convert.ToUInt64(userId));
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpDelete]
        [Route("UserBank/Delete")]
        [Authorize]
        public async Task<JsonResultModel<string>> Delete(UserBankModel model)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<string>() { msg = "获取用户失败" };

            string result = await DBOper.UserBank.Delete(model.id, Convert.ToUInt64(userId));
            return new JsonResultModel<string>(string.IsNullOrWhiteSpace(result), result, result);
        }
        [HttpGet]
        [Route("UserBank/GetOne")]
        [Authorize]
        public async Task<JsonResultModel<UserBankModel>> GetOne(ulong id)
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<UserBankModel>() { msg = "获取用户失败" };
            UserBankModel result = await DBOper.UserBank.GetOne(id, Convert.ToUInt64(userId));
            return new JsonResultModel<UserBankModel>(result != null, string.Empty, result);
        }
        [HttpGet]
        [Route("UserBank/GetList")]
        [Authorize]
        public async Task<JsonResultModel<IEnumerable<UserBankModel>>> GetList()
        {
            string userId = WebApi.GetUserId(User);
            if (userId.Length == 0) return new JsonResultModel<IEnumerable<UserBankModel>>(false, "获取用户失败", null);

            IEnumerable<UserBankModel> result = await DBOper.UserBank.GetList(Convert.ToUInt64(userId));
            return new JsonResultModel<IEnumerable<UserBankModel>>(result != null, result != null ? string.Empty : "获取列表失败", result);
        }
        [HttpGet]
        [Route("UserBank/GetList")]
        public async Task<IEnumerable<UserBankModel>> GetList(string user_id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<UserBankModel>();

            return await DBOper.UserBank.GetList(Convert.ToUInt64(user_id));
        }
        #region 管理员
        [HttpPut]
        [Route("UserBank/Audit")]
        public async Task<string> Audit(UserBankModel bank)
        {
            if (!await DBOper.SysUser.GetToken(bank.access_id, bank.access_token)) return "请重新登录";

            return await DBOper.UserBank.Audit(bank.id, bank.process_status_id);
        }
        [HttpGet]
        [Route("UserBank/GetListAudit")]
        public async Task<IEnumerable<UserBankModel>> GetListAudit(string user_id, string process_status_id,
             string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<UserBankModel>();

            return await DBOper.UserBank.GetListAudit(user_id, process_status_id, page_size, page_index);
        }
        [HttpGet]
        [Route("UserBank/GetCountAudit")]
        public async Task<long> GetCountAudit(string user_id, string process_status_id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return 0;

            return await DBOper.UserBank.GetCountAudit(user_id, process_status_id);
        }
        #endregion
    }
}
