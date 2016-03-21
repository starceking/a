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
    public class DictSettingsController : ApiController
    {
        [HttpPut]
        [Route("DictSettings/DictInit")]
        public async Task<string> DictInit(UserModel model)
        {
            if (!await DBOper.User.GetToken(model.access_id, model.access_token)) return "请重新登录";
            DBOper.DictSettings.DictInit(model.id);
            return string.Empty;
        }
    }
}
