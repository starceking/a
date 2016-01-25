using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using LIB;
using DAL;
using DNADAL;
using System.Collections.Generic;
using System.Configuration;

namespace WS
{
    /// <summary>
    /// Summary description for EXCASEWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class EXCASEWS : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetTaskRemind(string PERSON)
        {
            int jyrw = 0;
            int jjdq = 0;
            int wszz = 0;
            int shrw = 0;

            string filter = "(鉴定状态 ='检验中' or 鉴定状态 ='文书制作') and 一检人='" + PERSON + "'";
            DataSet ds = DBHelperSQL.Select("鉴定流程", filter, string.Empty, "计划完成,鉴定状态");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["鉴定状态"].ToString().Equals("文书制作")) wszz++;
                else
                {
                    jyrw++;
                    if (dr["计划完成"].ToString().Length > 0 && DateTime.Today < Convert.ToDateTime(dr["计划完成"]).AddDays(10)) jjdq++;
                }
            }

            filter = string.Format(@"鉴定状态 ='审核中' and 
((二检人='{0}' and 二检完成 is null) or 
(三检人='{0}' and 三检完成 is null and 二检完成 is not null) or 
(四检人='{0}' and 四检完成 is null and 三检完成 is not null) or 
(复核人='{0}' and 复核完成 is null and 二检完成 is not null and (三检人 is null or 三检完成 is not null) and (四检人 is null or 四检完成 is not null)) or 
(授权签字='{0}' and 签字完成 is null and 二检完成 is not null and (三检人 is null or 三检完成 is not null) and (四检人 is null or 四检完成 is not null) and (复核人 is null or 复核完成 is not null)) or 
(技管='{0}' and 技管完成 is null and 二检完成 is not null and (三检人 is null or 三检完成 is not null) and (四检人 is null or 四检完成 is not null) and (复核人 is null or 复核完成 is not null) and (授权签字 is null or 签字完成 is not null)) or 
(领导='{0}' and 审批完成 is null and 二检完成 is not null and (三检人 is null or 三检完成 is not null) and (四检人 is null or 四检完成 is not null) and (复核人 is null or 复核完成 is not null) and (授权签字 is null or 签字完成 is not null) and (技管 is null or 技管完成 is not null)))", PERSON);
            shrw = DBHelperSQL.Select("鉴定流程", filter, string.Empty, "委托编号").Tables[0].Rows.Count;

            return jyrw.ToString() + "，" + jjdq.ToString() + "，" + wszz.ToString() + "，" + shrw.ToString();
        }
        [WebMethod]
        public string GetExCaseList(string 打防管控, string 现场勘验, string 案件名称, string 登记日期s, string 登记日期e, string pageSize, string pageIndex)
        {
            DataSet ds = new DataSet("NewDataSet");
            DataTable dt = new DataTable("tableName"); ds.Tables.Add(dt);
            DataColumn dc = new DataColumn("案件名称", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("发案地点", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("发案时间", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("简要案情", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("ID", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("委托编号", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("打防管控", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("现场勘验", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("案件类型", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("案件性质", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("区划代码", typeof(string)); dt.Columns.Add(dc);
            DataRow dr = dt.NewRow(); dt.Rows.Add(dr);

            DataSet ds2 = new DataSet("NewDataSet");

            //用“案件名称”参数传入“关联系统”参数
            string 关联系统 = 案件名称;
            string JZ = string.Empty;
            string XK = string.Empty;
            switch (关联系统)
            {
                case "辽宁省厅":
                    JZ = ConfigurationManager.ConnectionStrings["LNJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["LNXKConnectionString"].ToString();
                    break;
                case "沈阳市":
                    JZ = ConfigurationManager.ConnectionStrings["SYJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["SYXKConnectionString"].ToString();
                    break;
                case "大连市":
                    JZ = ConfigurationManager.ConnectionStrings["DLJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["DLXKConnectionString"].ToString();
                    break;
                case "鞍山市":
                    JZ = ConfigurationManager.ConnectionStrings["ASJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["ASXKConnectionString"].ToString();
                    break;
                case "抚顺市":
                    JZ = ConfigurationManager.ConnectionStrings["FSJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["FSXKConnectionString"].ToString();
                    break;
                case "本溪市":
                    JZ = ConfigurationManager.ConnectionStrings["BXJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["BXXKConnectionString"].ToString();
                    break;
                case "丹东市":
                    JZ = ConfigurationManager.ConnectionStrings["DDJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["DDXKConnectionString"].ToString();
                    break;
                case "锦州市":
                    JZ = ConfigurationManager.ConnectionStrings["JZJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["JZXKConnectionString"].ToString();
                    break;
                case "营口市":
                    JZ = ConfigurationManager.ConnectionStrings["YKJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["YKXKConnectionString"].ToString();
                    break;
                case "阜新市":
                    JZ = ConfigurationManager.ConnectionStrings["FXJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["FXXKConnectionString"].ToString();
                    break;
                case "辽阳市":
                    JZ = ConfigurationManager.ConnectionStrings["LYJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["LYXKConnectionString"].ToString();
                    break;
                case "盘锦市":
                    JZ = ConfigurationManager.ConnectionStrings["PJJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["PJXKConnectionString"].ToString();
                    break;
                case "铁岭市":
                    JZ = ConfigurationManager.ConnectionStrings["TLJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["TLXKConnectionString"].ToString();
                    break;
                case "朝阳市":
                    JZ = ConfigurationManager.ConnectionStrings["CYJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["CYXKConnectionString"].ToString();
                    break;
                case "葫芦岛市":
                    JZ = ConfigurationManager.ConnectionStrings["HLDJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["HLDXKConnectionString"].ToString();
                    break;
                case "辽河":
                    JZ = ConfigurationManager.ConnectionStrings["LHJZConnectionString"].ToString();
                    XK = ConfigurationManager.ConnectionStrings["LHXKConnectionString"].ToString();
                    break;
            }

            if (打防管控.Length > 0)
            {//抚顺警综
                string sql = "select AJBH,FASJ,FADXZ,JYAQ,AJMC,FADSSXXQQ from T_AJ_JBXX where AJBH='" + 打防管控 + "'";
                ds2 = DBHelperOracle.Query(JZ, sql);

                foreach (DataRow dr2 in ds2.Tables[0].Rows)
                {
                    dr["打防管控"] = dr2["AJBH"].ToString();
                    dr["发案时间"] = dr2["FASJ"].ToString();
                    dr["发案地点"] = dr2["FADXZ"].ToString();
                    dr["简要案情"] = dr2["JYAQ"].ToString();
                    dr["案件名称"] = dr2["AJMC"].ToString();
                    dr["区划代码"] = dr2["FADSSXXQQ"].ToString();
                    dr = dt.NewRow(); dt.Rows.Add(dr);
                }
            }
            else if (现场勘验.Length > 0)
            {//抚顺现勘
                string sql = "select INVESTIGATION_NO,CASE_ID,INVESTIGATION_PLACE from SCENE_INVESTIGATION where INVESTIGATION_NO='" + 现场勘验 + "'";
                DataSet xkds = DBHelperOracle.Query(XK, sql);
                if (xkds.Tables[0].Rows.Count > 0)
                {
                    string INVESTIGATION_NO = xkds.Tables[0].Rows[0]["INVESTIGATION_NO"].ToString();//现勘编号
                    string CASE_ID = xkds.Tables[0].Rows[0]["CASE_ID"].ToString();//案件ID
                    string INVESTIGATION_PLACE = xkds.Tables[0].Rows[0]["INVESTIGATION_PLACE"].ToString();//勘查地点

                    sql = "select CASE_NO,CASE_TYPE,OCCURRENCE_DATE_FROM,SCENE_DETAIL,EXPOSURE_PROCESS,SCENE_REGIONALISM from SCENE_LAW_CASE where ID='" + CASE_ID + "'";
                    DataSet xkajds = DBHelperOracle.Query(XK, sql);

                    if (xkajds.Tables[0].Rows.Count > 0)
                    {
                        string CASE_NO = xkajds.Tables[0].Rows[0]["CASE_NO"].ToString();

                        if (CASE_NO.Length > 0)
                        {
                            sql = "select AJBH,FASJ,FADXZ,JYAQ,AJMC,FADSSXXQQ from T_AJ_JBXX where AJBH='" + CASE_NO + "'";
                            ds2 = DBHelperOracle.Query(JZ, sql);

                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                            {
                                dr["打防管控"] = dr2["AJBH"].ToString();
                                dr["现场勘验"] = INVESTIGATION_NO;
                                dr["发案时间"] = dr2["FASJ"].ToString();
                                dr["发案地点"] = dr2["FADXZ"].ToString();
                                dr["简要案情"] = dr2["JYAQ"].ToString();
                                dr["案件名称"] = dr2["AJMC"].ToString();
                                dr["区划代码"] = dr2["FADSSXXQQ"].ToString();
                                dr = dt.NewRow(); dt.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            foreach (DataRow dr2 in xkajds.Tables[0].Rows)
                            {
                                dr["打防管控"] = "";
                                dr["现场勘验"] = INVESTIGATION_NO;
                                dr["发案时间"] = dr2["OCCURRENCE_DATE_FROM"].ToString();
                                dr["发案地点"] = dr2["SCENE_DETAIL"].ToString();
                                dr["简要案情"] = dr2["EXPOSURE_PROCESS"].ToString();
                                dr["案件名称"] = "";
                                dr["区划代码"] = dr2["SCENE_REGIONALISM"].ToString();
                                dr = dt.NewRow(); dt.Rows.Add(dr);
                            }
                        }
                    }
                }

            }

            //大连警综平台
            //if (打防管控.Length == 0 && 登记日期s.Length == 0 && 登记日期e.Length == 0)
            //{
            //    return ds.GetXml();
            //}
            //else if (打防管控.Length == 0 && 登记日期s.Length > 0 && 登记日期e.Length > 0)
            //{
            //    ds2 = DBHelperOracle.Query2(ConfigurationManager.ConnectionStrings["JZPTConnectionString"].ToString(), 登记日期s, 登记日期e);
            //}
            //else
            //{
            //    ds2 = DBHelperOracle.Query1(ConfigurationManager.ConnectionStrings["JZPTConnectionString"].ToString(), 打防管控);
            //}

            return ds.GetXml();
        }
        [WebMethod]
        public string ExeSql(string sql)
        {
            return DBHelperSQL.ExecuteSql(sql);
        }
        [WebMethod]
        public string SendShortNote(string phone, string msg)
        {
            //http://10.119.1.87:5980/smsservice/SMSSvc.asmx
            //WebRsms.SMSSvc sms = new WebRsms.SMSSvc();
            //bool b = true;
            //sms.Url = "http://10.119.1.87:5980/smsservice/SMSSvc.asmx";
            //string strSource = " 330007";

            //return sms.SubmitA(phone, strSource, msg).Equals("success");
            return "1";
        }
        [WebMethod]
        public string GetPsb(string id, string bh, string mc)
        {
            string sql = string.Empty;
            DataSet ds = DBHelperOracle.Query(ConfigurationManager.ConnectionStrings["DfkConnectionString"].ToString(),
                "select * from dfk_gaxz.code_unit where name like '%" + mc + "%' order by code");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sql += "insert into 单位信息(ID,父单位,类型,编号,名称) values('" + Helper.GenerateID() +
                    "','" + id + "','1','" + bh + "','" + dr["name"] + "');";
            }
            return DBHelperSQL.ExecuteSql(sql);
        }
        [WebMethod]
        public string QueryGetReportCase(string 鉴定单位, string 案件名称)
        {
            return DBHelperSQL.Select("鉴定流程视图", "鉴定单位='" + 鉴定单位 + "' and 案件名称 like '%" + 案件名称 + "%' and 鉴定状态='待领取'",
                "发文确认 desc", "文书名称 ,案件编号,案件名称,委托单位名称").GetXml();
        }
        [WebMethod]
        public string GetSpTaskAmount(string 鉴定单位)
        {
            return DBHelperSQL.Query(@"
select 领导姓名,count(领导姓名) as amount from 鉴定流程视图
where 鉴定状态='审核中' and 鉴定单位='" + 鉴定单位 + @"' and 领导 is not null and 审批完成 is null and 
(技管 is null or 技管完成 is not null) and 
(授权签字 is null or 签字完成 is not null) and
 (复核人 is null or 复核完成 is not null) and
 (四检人 is null or 四检完成 is not null) and
 (三检人 is null or 三检完成 is not null) and
 (二检人 is null or 二检完成 is not null)
group by 领导姓名
").GetXml();
        }
        [WebMethod]
        public string GetYjrSp(string 一检人, string 受理序号)
        {
            string filter = "一检人='" + 一检人 + "' and 审批完成 is null and 鉴定状态='审核中' and ";
            if (受理序号.Length > 0) filter += "受理序号='" + 受理序号 + "' and ";
            return DBHelperSQL.Select("鉴定流程", Helper.CutFilter(filter), "受理时间 desc", "委托编号,文书名称,受理年份,受理序号,领导").GetXml();
        }
        #region 辅助的临时的
        [WebMethod]
        public string GetNextSLN(string preFix, string tableName)
        {
            return DataReader.GetNextSLN(preFix, tableName);
        }
        #endregion
    }
}
