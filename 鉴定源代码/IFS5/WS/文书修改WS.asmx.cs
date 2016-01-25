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
    /// Summary description for 文书修改WS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class 文书修改WS : System.Web.Services.WebService
    {
        [WebMethod]
        public string Insert(string ID, string 委托编号, string 修改人, string 修改时间, string 审批人, string 修改位置, string 原内容, string 修改后内容,
             string 新的编号, string 审批时间)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("委托编号", 委托编号);
            dict.Add("修改人", 修改人);
            dict.Add("修改时间", 修改时间);
            dict.Add("审批人", 审批人);
            dict.Add("修改位置", 修改位置);
            dict.Add("原内容", 原内容);
            dict.Add("修改后内容", 修改后内容);
            dict.Add("新的编号", 新的编号);
            dict.Add("审批时间", 审批时间);
            return DBHelperSQL.Insert("文书修改", dict);
        }
        [WebMethod]
        public string Update(string ID, string 修改时间, string 审批人, string 修改位置, string 原内容, string 修改后内容,
             string 新的编号, string 审批时间)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("修改时间", 修改时间);
            dict.Add("审批人", 审批人);
            dict.Add("审批时间", 审批时间);
            dict.Add("修改位置", 修改位置);
            dict.Add("原内容", 原内容);
            dict.Add("修改后内容", 修改后内容);
            dict.Add("新的编号", 新的编号);
            return DBHelperSQL.Update("文书修改", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string Delete(string ID)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("文书修改", "ID，" + ID);
            return DBHelperSQL.Delete(dict);
        }
        [WebMethod]
        public string GetAll(string 委托编号)
        {
            return DBHelperSQL.Select("文书修改", "委托编号='" + 委托编号 + "'", "修改时间 DESC", "*").GetXml();
        }
    }
}
