using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class WeChat
    {
        public static string appId = ConfigurationManager.AppSettings["key_wx"];
        public static string appSecret = ConfigurationManager.AppSettings["secret_wx"];

        public static async Task<WeUnionId> GetUserInfo(string code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("https://api.weixin.qq.com/sns/oauth2/access_token?appid=");
            builder.Append(appId);
            builder.Append("&secret=");
            builder.Append(appSecret);
            builder.Append("&code=");
            builder.Append(code);
            builder.Append("&grant_type=authorization_code");
            string json = await Get(builder.ToString());
            WeOpenId wo = Json.Parse<WeOpenId>(json);
            if (wo == null || string.IsNullOrWhiteSpace(wo.openid) || string.IsNullOrWhiteSpace(wo.access_token))
                return null;

            builder = new StringBuilder();
            builder.Append("https://api.weixin.qq.com/sns/userinfo?access_token=");
            builder.Append(wo.access_token);
            builder.Append("&openid=");
            builder.Append(wo.openid);
            json = await Get(builder.ToString());
            WeUnionId wu = Json.Parse<WeUnionId>(json);
            return wu;
        }
        public static async Task<string> Get(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            HttpWebResponse myResponse = (HttpWebResponse)(await myRequest.GetResponseAsync());
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = await reader.ReadToEndAsync();
            reader.Close();
            return content;
        }
    }
    class WeOpenId
    {
        public string openid { get; set; }
        public string access_token { get; set; }
    }
    public class WeUnionId
    {
        public string unionid { get; set; }
        public string nickname { get; set; }
    }
}
