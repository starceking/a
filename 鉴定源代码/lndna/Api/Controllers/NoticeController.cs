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
    public class NoticeController : ApiController
    {
        [HttpDelete]
        [Route("Notice/Delete")]
        public async Task<string> Delete(NoticeModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            if (!await DBOper.SysUser.CheckAuth(model.access_id, -1)) return "权限异常";

            return await DBOper.Notice.Delete(model.id);
        }
        [HttpGet]
        [Route("Notice/GetOne")]
        public async Task<NoticeModel> GetOne(int id)
        {
            return await DBOper.Notice.GetOne(id);
        }
        [HttpGet]
        [Route("Notice/GetList")]
        public async Task<IEnumerable<NoticeModel>> GetList(string page_size, string page_index)
        {
            return await DBOper.Notice.GetList(page_size, page_index);
        }
        [HttpGet]
        [Route("Notice/GetCount")]
        public async Task<long> GetCount()
        {
            return await DBOper.Notice.GetCount();
        }
    }
}
