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
    public class HsiDebtController : ApiController
    {
        [HttpPost]
        [Route("HsiDebt/Insert")]
        public async Task<string> Insert(HsiDebtModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (model.access_id != 1 && model.access_id != 4) return "无此权限";

            return await DBOper.HsiDebt.Insert(model.amount, model.money_debt, model.fee);
        }
        [HttpPut]
        [Route("HsiDebt/Update")]
        public async Task<string> Update(HsiDebtModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (model.access_id != 1 && model.access_id != 4) return "无此权限";

            return await DBOper.HsiDebt.Update(model.id, model.amount, model.money_debt, model.fee);
        }
        [HttpDelete]
        [Route("HsiDebt/Delete")]
        public async Task<string> Delete(HsiDebtModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (model.access_id != 1 && model.access_id != 4) return "无此权限";

            return await DBOper.HsiDebt.Delete(model.id);
        }
        [HttpGet]
        [Route("HsiDebt/GetOne")]
        public async Task<HsiDebtModel> GetOne(int id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return null;

            return await DBOper.HsiDebt.GetOne(id);
        }
        [HttpGet]
        [Route("HsiDebt/GetList")]
        public async Task<IEnumerable<HsiDebtModel>> GetList()
        {
            return await DBOper.HsiDebt.GetList();
        }
    }
}
