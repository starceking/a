using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace Util
{
    public static class WebApi
    {
        static readonly string OAuthUrl = ConfigurationManager.AppSettings["OAuthUrl"];
        static readonly string OAuthCid = ConfigurationManager.AppSettings["OAuthCid"];
        static readonly string OAuthCkey = ConfigurationManager.AppSettings["OAuthCkey"];

        #region api
        public static async Task<T> Get<T>(string url, IDictionary<string, string> content, string access_token) where T : class
        {
            StringBuilder builder = new StringBuilder();
            foreach (string key in content.Keys)
            {
                builder.Append("&");
                builder.Append(key);
                builder.Append("=");
                builder.Append(content[key]);
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(url);
            if (builder.Length > 0)
            {
                builder2.Append("?");
                builder2.Append(builder.ToString().Substring(1));
            }

            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

            using (var http = new HttpClient(handler))
            {
                if (!string.IsNullOrWhiteSpace(access_token)) http.SetBearerToken(access_token);

                var response = await http.GetAsync(builder2.ToString());
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                return (Json.Parse<T>(result));
            }
        }
        public static async Task<string> Post(string url, IDictionary<string, string> content, string access_token)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (var http = new HttpClient(handler))
            {
                if (!string.IsNullOrWhiteSpace(access_token)) http.SetBearerToken(access_token);

                var response = await http.PostAsync(url, new FormUrlEncodedContent(content));
                response.EnsureSuccessStatusCode();
                return (await response.Content.ReadAsStringAsync());
            }
        }
        public static async Task<string> Put(string url, IDictionary<string, string> content, string access_token)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (var http = new HttpClient(handler))
            {
                if (!string.IsNullOrWhiteSpace(access_token)) http.SetBearerToken(access_token);

                var response = await http.PutAsync(url, new FormUrlEncodedContent(content));
                response.EnsureSuccessStatusCode();
                return (await response.Content.ReadAsStringAsync());
            }
        }
        public static async Task<string> Delete(string url, string access_token)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

            using (var http = new HttpClient(handler))
            {
                if (!string.IsNullOrWhiteSpace(access_token)) http.SetBearerToken(access_token);

                var response = await http.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                return (await response.Content.ReadAsStringAsync());
            }
        }
        #endregion
        #region sync
        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retstring = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retstring;
        }
        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr.Length > 0 ? "?" : postDataStr) + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retstring = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retstring;
        }
        #endregion
        #region user
        public static async Task<TokenResponse> GetToken(string name, string pwd)
        {
            var client = new OAuth2Client(new Uri(OAuthUrl), OAuthCid, OAuthCkey);
            TokenResponse tr = await client.RequestResourceOwnerPasswordAsync(name, pwd, "api1 offline_access");
            //if (tr != null)
            //{
            //    await SaveToken(tr.AccessToken, tr.RefreshToken, tr.ExpiresIn);
            //}
            return tr;
        }
        public static TokenResponse GetTokenSync(string name, string pwd)
        {
            var client = new OAuth2Client(new Uri(OAuthUrl), OAuthCid, OAuthCkey);
            TokenResponse tr = client.RequestResourceOwnerPasswordAsync(name, pwd, "api1 offline_access").Result;
            //if (tr != null)
            //{
            //    await SaveToken(tr.AccessToken, tr.RefreshToken, tr.ExpiresIn);
            //}
            return tr;
        }
        public static async Task<TokenResponse> RefToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var client = new OAuth2Client(new Uri(OAuthUrl), OAuthCid, OAuthCkey);
            TokenResponse tr = await client.RequestRefreshTokenAsync(token);
            //if (tr != null)
            //{
            //    await SaveToken(tr.AccessToken, tr.RefreshToken, tr.ExpiresIn);
            //}
            return tr;
        }
        public static string GetUserId(System.Security.Principal.IPrincipal user)
        {
            ClaimsPrincipal caller = user as ClaimsPrincipal;
            if (caller == null) return string.Empty;
            var subjectClaim = caller.FindFirst("sub");
            if (subjectClaim != null) return subjectClaim.Value;
            return string.Empty;
        }
        //public static async Task<string> ValidToken(string access_token)
        //{
        //    if (string.IsNullOrWhiteSpace(access_token)) return string.Empty;

        //    string key = "WebApi_SaveToken_access_token_" + access_token;
        //    SaveTokenModel stm = await Redis.Get<SaveTokenModel>(access_token);
        //    if (stm == null) return string.Empty;
        //    if (stm.expiresIn > DateTime.Now) return access_token;
        //    TokenResponse tr = await RefToken(stm.refresh_token);
        //    if (tr != null)
        //    {
        //        await SaveToken(tr.AccessToken, tr.RefreshToken, tr.ExpiresIn);
        //    }
        //    return tr.AccessToken;
        //}
        //public static string ValidTokenSync(string access_token)
        //{
        //    if (string.IsNullOrWhiteSpace(access_token)) return string.Empty;

        //    string key = "WebApi_SaveToken_access_token_" + access_token;
        //    SaveTokenModel stm = Redis.GetSync<SaveTokenModel>(access_token);
        //    if (stm == null) return string.Empty;
        //    if (stm.expiresIn > DateTime.Now) return access_token;
        //    TokenResponse tr = RefTokenSync(stm.refresh_token);
        //    if (tr != null)
        //    {
        //        SaveTokenSync(tr.AccessToken, tr.RefreshToken, tr.ExpiresIn);
        //    }
        //    return tr.AccessToken;
        //}
        //static TokenResponse RefTokenSync(string token)
        //{
        //    if (string.IsNullOrWhiteSpace(token)) return null;

        //    var client = new OAuth2Client(new Uri(OAuthUrl), OAuthCid, OAuthCkey);
        //    TokenResponse tr = client.RequestRefreshTokenAsync(token).Result;
        //    if (tr != null)
        //    {
        //        SaveTokenSync(tr.AccessToken, tr.RefreshToken, tr.ExpiresIn);
        //    }
        //    return tr;
        //}
        //static async Task SaveToken(string access_token, string refresh_token, long expiresIn)
        //{
        //    if (string.IsNullOrWhiteSpace(access_token) || string.IsNullOrWhiteSpace(refresh_token) || expiresIn <= 0) return;

        //    string key = "WebApi_SaveToken_access_token_" + access_token;
        //    SaveTokenModel stm = new SaveTokenModel
        //    {
        //        refresh_token = refresh_token,
        //        expiresIn = DateTime.Now.AddSeconds(expiresIn)
        //    };
        //    await Redis.Insert<SaveTokenModel>(access_token, stm);
        //}
        //static void SaveTokenSync(string access_token, string refresh_token, long expiresIn)
        //{
        //    if (string.IsNullOrWhiteSpace(access_token) || string.IsNullOrWhiteSpace(refresh_token) || expiresIn <= 0) return;

        //    string key = "WebApi_SaveToken_access_token_" + access_token;
        //    SaveTokenModel stm = new SaveTokenModel
        //    {
        //        refresh_token = refresh_token,
        //        expiresIn = DateTime.Now.AddSeconds(expiresIn)
        //    };
        //    Redis.InsertSync<SaveTokenModel>(access_token, stm);
        //}
        #endregion
    }
    //class SaveTokenModel
    //{
    //    public string refresh_token { get; set; }
    //    public DateTime expiresIn { get; set; }
    //}
}
