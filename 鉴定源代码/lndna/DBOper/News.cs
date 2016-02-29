using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class News
    {
        #region 常量
        /// <summary>
        /// 当日新闻redis的key
        /// </summary>
        const string TODAY_NEWS_KEY = "News_GetTopNews";
        static readonly string SavePath = ConfigurationManager.AppSettings["NewsPath"];
        static string APITOKEN = "0e6533725c486e7ab4f5e6d4d05ded8551d98bedd6fb3df887565785d92f4eeb";
        #endregion
        #region webapi
        /// <summary>
        /// 获取首页新闻
        /// </summary>
        public static async Task<IEnumerable<NewsModel>> GetTopNews()
        {
            bool ncmp = string.IsNullOrWhiteSpace(User.CMP_ID);
            string TODAY_NEWS_KEY_key = ncmp ? TODAY_NEWS_KEY : (TODAY_NEWS_KEY + User.CMP_ID);

            string newsStr = await Redis.GetString(TODAY_NEWS_KEY_key);
            if (string.IsNullOrWhiteSpace(newsStr)) return new List<NewsModel>();
            return Json.ScriptDeserialize<List<NewsModel>>(newsStr);
        }
        #endregion
        #region sync
        /// <summary>
        /// 将今日新闻读入redis
        /// </summary>
        /// <param name="json"></param>
        public static void InsertListSync(List<NewsModel> list)
        {
            bool ncmp = string.IsNullOrWhiteSpace(User.CMP_ID);
            string TODAY_NEWS_KEY_key = ncmp ? TODAY_NEWS_KEY : (TODAY_NEWS_KEY + User.CMP_ID);

            if (list == null || list.Count == 0) return;
            string json = Json.ScriptSerialize(list);
            Log.Info(TODAY_NEWS_KEY_key, json);
            Redis.InsertStringSync(TODAY_NEWS_KEY_key, json);
        }
        #endregion
        #region 定时任务
        public static void GetNews()
        {
            //每日9:21
            string url = "https://api.wmcloud.com:443/data/v1/";
            url += "api/subject/getNewsContentByTime.json";
            string postDataStr = "field=";
            postDataStr += "&beginTime=06:00";
            postDataStr += "&endTime=08:25";
            postDataStr += "&newsPublishDate=" + DateTime.Now.ToString("yyyyMMdd");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;";
            request.Headers.Add("Authorization", "Bearer " + APITOKEN);
            try
            {
                HttpWebResponse response = (HttpWebResponse)(request.GetResponse());
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                myStreamReader.Dispose();
                myResponseStream.Dispose();
                NewsJsonModel model = Json.ScriptDeserialize<NewsJsonModel>(retString);
                if (model.data != null)
                {
                    List<NewsModel> newsList = (from p in model.data
                                                where
                                                (p.newsTitle.IndexOf("股票") >= 0 ||
                                                p.newsTitle.IndexOf("期指") >= 0 ||
                                                p.newsTitle.IndexOf("期货") >= 0 ||
                                                p.newsTitle.IndexOf("沪深") >= 0 ||
                                                p.newsTitle.IndexOf("股市") >= 0 ||
                                                p.newsTitle.IndexOf("证券") >= 0 ||
                                                p.newsTitle.IndexOf("券商") >= 0) && p.newsPublishSite.Length == 3
                                                select p).Take(5).ToList();

                    Build(newsList);
                    InsertListSync(newsList);

                    Console.WriteLine(model.retMsg + "-> get records " + newsList == null ? 0 : newsList.Count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Build(List<NewsModel> list)
        {
            if (!Directory.Exists(SavePath)) return;
            DirectoryInfo di = new DirectoryInfo(SavePath);
            //查询文件并排序
            FileInfo[] files = di.GetFiles("*.cshtml", System.IO.SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].FullName.Contains("Detail"))
                    continue;
                File.Delete(files[i].FullName);
            }

            string path = SavePath + "/Detail.cshtml";
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string html = sr.ReadToEnd();
            foreach (NewsModel item in list)
            {
                StringBuilder bodyBuilder = new StringBuilder();
                string[] lines = item.newsBody.Split('\n');
                foreach (string line in lines)
                {
                    bodyBuilder.Append("<p>");
                    bodyBuilder.Append(line);
                    bodyBuilder.AppendLine("</p>");
                }

                string temp = html;
                temp = temp.Replace("$[title]", item.newsTitle);
                temp = temp.Replace("$[source]", item.newsPublishTime + "&nbsp;来源：" + item.newsPublishSite);
                temp = temp.Replace("$[body]", bodyBuilder.ToString());
                FileStream fs = new FileStream(SavePath + "/" + item.newsID.ToString() + ".cshtml", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(temp);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
        }
        #endregion
    }
    class NewsJsonModel
    {
        public string retCode { get; set; }
        public string retMsg { get; set; }
        public List<NewsModel> data { get; set; }
    }
}
