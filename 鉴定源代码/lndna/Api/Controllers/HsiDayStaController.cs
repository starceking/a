﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    public class HsiDayStaController : ApiController
    {
        [HttpGet]
        [Route("HsiDaySta/GetList")]
        public async Task<IEnumerable<HsiDayStaModel>> GetList(string sta_days, string sta_daye, string cmp_id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<HsiDayStaModel>();
            if (!string.IsNullOrWhiteSpace(cmp_id))
                if (!await DBOper.SysUser.CheckAuth(access_id, 0)) return new List<HsiDayStaModel>();

            return await DBOper.HsiDaySta.GetList(sta_days, sta_daye, cmp_id);
        }
    }
}
