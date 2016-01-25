using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using LIB;
using DAL;

namespace WS
{
    /// <summary>
    /// 行政区划WS 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class 行政区划WS : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetAll(string 区划代码, string 区域名称, string 类型)
        {
            string filter = string.Empty;
            if (区划代码.Length > 0)
            {
                filter += "区划代码 like '" + 区划代码 + "%' and ";
                filter += "区划代码<>'" + 区划代码 + "0000' and ";
                filter += "区划代码<>'" + 区划代码 + "00' and ";
            }

            if (类型.Equals("省"))
            {
                filter += "区划代码 like '%0000' and ";
            }
            else if (类型.Equals("市"))
            {
                filter += "区划代码 like '%00' and ";
            }
            else if (类型.Equals("直辖市"))
            {
                类型 = "市";
            }

            DataSet ds = DBHelperSQL.Select("行政区划", Helper.CutFilter(filter), "ID", "*");
            DataColumn dc = new DataColumn("类型", typeof(string));
            dc.DefaultValue = 类型;
            ds.Tables[0].Columns.Add(dc);
            return ds.GetXml();
        }
    }
}
