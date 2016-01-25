using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using LIB;
using DAL;

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
    public class 案件人员WS : System.Web.Services.WebService
    {
        [WebMethod]
        public string Insert(string ID, string 委托编号, string 姓名, string 性别, string 身份证, string 电话,
            string 出生日期, string 年龄, string 职业, string 学历, string 籍贯, string 工作地点, string 现住址)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("委托编号", 委托编号);
            dict.Add("姓名", 姓名);
            dict.Add("性别", 性别);
            dict.Add("身份证", 身份证);
            dict.Add("电话", 电话);
            dict.Add("出生日期", 出生日期);
            dict.Add("年龄", 年龄);
            dict.Add("职业", 职业);
            dict.Add("学历", 学历);
            dict.Add("籍贯", 籍贯);
            dict.Add("工作地点", 工作地点);
            dict.Add("现住址", 现住址);
            string fuck = DBHelperSQL.Insert("案件人员", dict);
            string sql = "";
            if (fuck == "1")
            {
                DataSet ds = DBHelperSQL.Query("select * from 案件人员 where 委托编号='" + 委托编号 + "'");
                string name_text = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    name_text = name_text + ds.Tables[0].Rows[i]["姓名"].ToString() + "，";
                }
                sql = DBHelperSQL.ExecuteSql("update 案件信息 set 被鉴定人='" + name_text.Substring(0, name_text.Length - 1) + "' where 委托编号='" + 委托编号 + "'");
            }
            return sql;
        }
        [WebMethod]
        public string Update(string ID, string 姓名, string 性别, string 身份证, string 电话,
            string 出生日期, string 年龄, string 职业, string 学历, string 籍贯, string 工作地点, string 现住址)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("姓名", 姓名);
            dict.Add("性别", 性别);
            dict.Add("身份证", 身份证);
            dict.Add("电话", 电话);
            dict.Add("出生日期", 出生日期);
            dict.Add("年龄", 年龄);
            dict.Add("职业", 职业);
            dict.Add("学历", 学历);
            dict.Add("籍贯", 籍贯);
            dict.Add("工作地点", 工作地点);
            dict.Add("现住址", 现住址);
            string fuck = DBHelperSQL.Update("案件人员", "ID='" + ID + "'", dict);
            string sql = "";
            if (fuck == "1")
            {
                DataSet ds = DBHelperSQL.Query("select * from 案件人员 where 委托编号=(select 委托编号 from 案件人员 where ID='" + ID + "')");
                string name_text = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    name_text = name_text + ds.Tables[0].Rows[i]["姓名"].ToString() + "，";
                }
                sql = DBHelperSQL.ExecuteSql("update 案件信息 set 被鉴定人='" + name_text.Substring(0, name_text.Length - 1) + "' where 委托编号='" + ds.Tables[0].Rows[0]["委托编号"].ToString() + "'");
            }
            return sql;
        }
        [WebMethod]
        public string Delete(string ID)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("案件人员", "ID，" + ID);
            DataSet ds = DBHelperSQL.Query("select * from 案件人员 where 委托编号=(select 委托编号 from 案件人员 where ID='" + ID + "')");
            string fuck = DBHelperSQL.Delete(dict);
            string sql = "";
            if (fuck == "1")
            {
                string name_text = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    name_text = name_text + ds.Tables[0].Rows[i]["姓名"].ToString() + "，";
                }
                sql = DBHelperSQL.ExecuteSql("update 案件信息 set 被鉴定人='" + name_text.Substring(0, name_text.Length - 1) + "' where 委托编号='" + ds.Tables[0].Rows[0]["委托编号"].ToString() + "'");
            }
            return sql;
        }
        [WebMethod]
        public string GetAll(string 委托编号)
        {
            return DBHelperSQL.Select("案件人员", "委托编号='" + 委托编号 + "'", "创建时间", "*").GetXml();
        }
        [WebMethod]
        public string GetALLAJRY()
        {
            string sql = "";
            DataSet ds = DBHelperSQL.Query("select 委托编号 from 案件信息");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataSet ds_ajry = DBHelperSQL.Query("select * from 案件人员 where 委托编号='" + ds.Tables[0].Rows[i]["委托编号"].ToString() + "'");
                if (ds_ajry.Tables[0].Rows.Count > 0)
                {
                    string name_text = string.Empty;
                    for (int j = 0; j < ds_ajry.Tables[0].Rows.Count; j++)
                    {
                        name_text = name_text + ds_ajry.Tables[0].Rows[j]["姓名"].ToString() + "，";
                    }
                    sql = DBHelperSQL.ExecuteSql("update 案件信息 set 被鉴定人='" + name_text.Substring(0, name_text.Length - 1) + "' where 委托编号='" + ds.Tables[0].Rows[i]["委托编号"].ToString() + "'");

                }
            }
            return sql;
        }
    }
}
