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
    public class ZStockSettingsController : ApiController
    {
        [HttpPut]
        [Route("ZStockSettings/Update")]
        public async Task<string> Update(ZStockSettingsModel umc)
        {
            if (!await DBOper.SysUser.GetToken(umc.access_id, umc.access_token)) return "请重新登录";
            if (umc.access_id != 1 && umc.access_id != 4) return "无此权限";

            await DBOper.ZStockSettings.Update(umc.id, umc.value);
            return string.Empty;
        }
        [HttpGet]
        [Route("ZStockSettings/GetList")]
        public async Task<IEnumerable<ZStockSettingsModel>> GetList()
        {
            return await DBOper.ZStockSettings.GetList();
        }
        [HttpPut]
        [Route("ZStockSettings/SetRongduan")]
        public async Task<string> SetRongduan(ZStockSettingsModel umc)
        {
            if (!await DBOper.SysUser.GetToken(umc.access_id, umc.access_token)) return "请重新登录";

            await DBOper.ZStockSettings.SetRongduan(umc.id);
            return string.Empty;
        }
        [HttpGet]
        [Route("ZStockSettings/GetRongduan")]
        public async Task<int> GetRongduan()
        {
            return await DBOper.ZStockSettings.GetRongduan();
        }
        [HttpPut]
        [Route("ZStockSettings/SetIdxData")]
        public async Task<string> SetIdxData(ZStockSettingsModel umc)
        {
            if (!await DBOper.SysUser.GetToken(umc.access_id, umc.access_token)) return "请重新登录";
            if (umc.access_id != 1 && umc.access_id != 4) return "无此权限";

            await DBOper.ZStockSettings.SetIdxData(umc.value);
            return string.Empty;
        }
        [HttpGet]
        [Route("ZStockSettings/GetIdxData")]
        public async Task<string> GetIdxData()
        {
            return await DBOper.ZStockSettings.GetIdxData();
        }
    }
}
