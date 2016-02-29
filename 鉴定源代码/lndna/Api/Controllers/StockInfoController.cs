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
    public class StockInfoController : ApiController
    {
        [HttpGet]
        [Route("StockInfo/GetNowPriceMul")]
        public async Task<string> GetNowPriceMul(string nums)
        {
            return await DBOper.StockInfo.GetNowPriceMul(nums);
        }
        [HttpGet]
        [Route("StockInfo/GetOneByNum")]
        public async Task<StockInfoModel> GetOneByNum(string number)
        {
            return await DBOper.StockInfo.GetOneByNum(number);
        }
    }
}
