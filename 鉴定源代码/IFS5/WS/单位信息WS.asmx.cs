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
    public class 单位信息WS : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetAll(string 父单位, string 类型, string PSBtype)
        {
            string filter = string.Empty;
            if (父单位.Length > 0)
            {
                if (父单位.ToLower().Trim().Equals("null"))
                {
                    filter += "父单位 is null and ";
                }
                else if (父单位.ToLower().Trim().Equals("000000000000000000000000000000"))
                {
                    filter += "id like '%" + 父单位 + "' and ";
                }
                else
                {
                    filter += "父单位='" + 父单位 + "' and ";
                }
            }
            if (类型.Length > 0) filter += "类型='" + 类型 + "' and ";

            DataSet ds = DBHelperSQL.Select("单位信息", Helper.CutFilter(filter), "ID", "*");
            DataColumn dc = new DataColumn("PSBtype", typeof(string));
            dc.DefaultValue = PSBtype;
            ds.Tables[0].Columns.Add(dc);
            return ds.GetXml();
        }

        [WebMethod]
        public string Insert(string ID, string 父单位, string 编号, string 名称, string 地址, string 邮编, string 简称, string 电话)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("父单位", 父单位);
            dict.Add("类型", "1");
            dict.Add("编号", 编号);
            dict.Add("名称", 名称);
            dict.Add("地址", 地址);
            dict.Add("邮编", 邮编);
            dict.Add("简称", 简称);
            dict.Add("电话", 电话);
            return DBHelperSQL.Insert("单位信息", dict);
        }
        [WebMethod]
        public string Update(string ID, string 编号, string 名称, string 地址, string 邮编, string 简称, string 电话)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("编号", 编号);
            dict.Add("名称", 名称);
            dict.Add("地址", 地址);
            dict.Add("邮编", 邮编);
            dict.Add("简称", 简称);
            dict.Add("电话", 电话);
            return DBHelperSQL.Update("单位信息", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string Delete(string ID)
        {
            int count = DBHelperSQL.QueryRowCount("select count(委托编号) as xxx from 鉴定流程 where 委托单位='" + ID + "'");
            if (count == 0)
            {
                IDictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("单位信息", "ID，" + ID);
                return DBHelperSQL.Delete(dict);
            }
            return "该单位与某案件相关联，不能删除。";
        }
    }
}
