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
    public class NewsController : ApiController
    {
        [HttpGet]
        [Route("News/GetTopNews")]
        public async Task<JsonResultModel<IEnumerable<NewsModel>>> GetTopNews()
        {
            IEnumerable<NewsModel> list = await DBOper.News.GetTopNews();
            return new JsonResultModel<IEnumerable<NewsModel>>()
            {
                success = list != null,
                msg = list != null ? "" : "无法获取到news",
                data = list
            };
        }
    }
}
