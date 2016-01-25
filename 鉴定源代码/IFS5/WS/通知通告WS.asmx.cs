using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using DAL;
using System.Collections.Generic;
using LIB;

namespace WS
{
    /// <summary>
    /// ok
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class 通知通告WS : System.Web.Services.WebService
    {
        [WebMethod]
        public string Insert(string ID, string 标题, string 内容, string 是否重要, string 发布人, string 鉴定单位)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("标题", 标题);
            dict.Add("内容", 内容);
            dict.Add("是否重要", 是否重要);
            dict.Add("发布人", 发布人);
            dict.Add("单位信息ID", 鉴定单位);
            return DBHelperSQL.Insert("通知通告", dict);
        }
        [WebMethod]
        public string Update(string ID, string 标题, string 内容, string 是否重要)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("标题", 标题);
            dict.Add("内容", 内容);
            dict.Add("是否重要", 是否重要);
            return DBHelperSQL.Update("通知通告", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string Delete(string ID)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("通知通告", "ID，" + ID);
            return DBHelperSQL.Delete(dict);
        }
        [WebMethod]
        public string GetAll(string 鉴定单位, string 标题, string 是否重要, string s创建时间, string e创建时间, string pageSize, string pageIndex)
        {
            string filter = "单位信息ID='" + 鉴定单位 + "' and ";
            if (标题.Length > 0) filter += "标题 like '%" + 标题 + "%' and ";
            if (是否重要.Length > 0) filter += "是否重要='" + 是否重要 + "' and ";
            if (s创建时间.Length > 0) filter += "创建时间>='" + s创建时间 + "' and ";
            if (e创建时间.Length > 0) filter += "创建时间<'" + e创建时间 + "' and ";

            return DBHelperSQL.SelectRowCount("通知通告", Helper.CutFilter(filter), "创建时间 DESC", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string GetImportant(string 鉴定单位)
        {
            return DBHelperSQL.Select("通知通告", "是否重要='1' and 单位信息ID='" + 鉴定单位 + "'", "创建时间", "*").GetXml();
        }
    }
}
