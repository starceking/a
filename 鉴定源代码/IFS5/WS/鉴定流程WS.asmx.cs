using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using DAL;
using LIB;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using DNADAL;
using System.IO;
using System.Data.OracleClient;
using IFSOracleDAL;

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
    public class 鉴定流程WS : System.Web.Services.WebService
    {
        #region 操作
        /// <summary>
        /// 如果有基于该案件的补送，则不能取消。
        /// 所有检材的编号要重置为null
        /// </summary>
        [WebMethod]
        public string Cancel(string CASE_ID, string CONSIGNID, string MPID, string 委托编号, string ORA_FLAG)//2012.1.4
        {
            if (委托编号.StartsWith("D"))
            {
                if (DBHelperSQL.QueryRowCount("select count(id) from 案件信息 where SRCID='" + CASE_ID + "' and ora_flag='1'") > 1)
                {
                    return "存在基于该案件的补送，不能取消！";
                }
            }

            DataSet ds = null;
            if (委托编号.StartsWith("D") || 委托编号.StartsWith("R"))
            {
                ds = DBHelperSQL.Query("select 亲属一ID,亲属二ID,ID from 亲属定义 where 委托编号='" + 委托编号 + "'");
            }

            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("鉴定状态", "新的委托");
                    dict.Add("受理年份", string.Empty);
                    dict.Add("受理序号", string.Empty);
                    dict.Add("案件序号", string.Empty);
                    dict.Add("受理人员", string.Empty);
                    dict.Add("受理时间", string.Empty);
                    dict.Add("计划完成", string.Empty);
                    dict.Add("认证认可", string.Empty);
                    dict.Add("受理意见", string.Empty);
                    dict.Add("一检人", string.Empty);
                    DBHelperSQL.Update("鉴定流程", "委托编号='" + 委托编号 + "'", dict, dbConnection, trans);

                    if (委托编号.StartsWith("C"))
                    {
                        string sql = "update 鉴定材料 set 材料编号='受理后自动生成' where 委托编号='" + 委托编号 + "';";
                        sql += "update 案件信息 set 案件编号='受理后自动生成' where 委托编号='" + 委托编号 + "';";
                        DBHelperSQL.ExecuteSql(sql, dbConnection, trans);
                    }
                    else if (委托编号.StartsWith("D"))
                    {
                        string sql = "update 案件信息 set 案件编号='受理后自动生成',ora_flag='0' where 委托编号='" + 委托编号 + "';";
                        sql += "update 涉案人员 set 样本编号='受理后自动生成',ora_flag='0' where 委托编号='" + 委托编号 + "';";
                        sql += "update 无名尸体 set 样本编号='受理后自动生成',ora_flag='0' where 委托编号='" + 委托编号 + "';";
                        sql += "update 样本信息 set 样本编号='受理后自动生成',ora_flag='0' where 委托编号='" + 委托编号 + "';";
                        sql += "update 亲属定义 set ora_flag='0' where 委托编号='" + 委托编号 + "';";
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            sql += "update 亲属信息 set 样本编号='受理后自动生成' where ID='" + dr[0].ToString() + "';";
                            if (dr[1].ToString().Length > 0)
                                sql += "update 亲属信息 set 样本编号='受理后自动生成' where ID='" + dr[1].ToString() + "';";
                        }
                        DBHelperSQL.ExecuteSql(sql, dbConnection, trans);
                        if (ORA_FLAG.Equals("1"))
                        {
                            案件信息WS.DeleteDNAOra(CASE_ID, CONSIGNID, 委托编号, dbConnection, trans);
                        }
                    }
                    else if (委托编号.StartsWith("R"))
                    {
                        string sql = string.Empty;
                        DataRow dr = ds.Tables[0].Rows[0];
                        sql += "update 亲属定义 set ora_flag='0' where 委托编号='" + 委托编号 + "';";
                        sql += "update 亲属信息 set 样本编号='受理后自动生成' where ID='" + dr[0].ToString() + "';";
                        if (dr[1].ToString().Length > 0)
                            sql += "update 亲属信息 set 样本编号='受理后自动生成' where ID='" + dr[1].ToString() + "';";
                        DBHelperSQL.ExecuteSql(sql, dbConnection, trans);
                        if (ORA_FLAG.Equals("1"))
                        {
                            亲属定义WS.DeleteRelativeOra(dr[2].ToString(), CONSIGNID, dr[0].ToString(), dr[1].ToString());
                        }
                    }
                    else if (委托编号.StartsWith("L"))
                    {
                        DBHelperSQL.ExecuteSql("update 失踪人员 set 样本编号='受理后自动生成',ora_flag='0' where ID='" + MPID + "'", dbConnection, trans);
                        if (ORA_FLAG.Equals("1"))
                        {
                            失踪人员WS.DeleteMPOra(MPID, CONSIGNID);
                        }
                    }
                    trans.Commit();
                }
            }
            return "1";
        }
        private void UpdateAcceptFlow(string 委托编号, string 受理年份, string 受理序号, string 案件序号, string 受理人员, string 受理时间,
            string 计划完成, string 认证认可, string 受理意见, string 一检人, string 鉴定状态, string 鉴定方法, SqlConnection dbConnection, SqlTransaction trans)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("受理年份", 受理年份);
            dict.Add("受理人员", 受理人员);
            dict.Add("受理时间", 受理时间);
            dict.Add("计划完成", 计划完成);
            dict.Add("认证认可", 认证认可);
            dict.Add("受理意见", 受理意见);
            dict.Add("一检人", 一检人);
            dict.Add("受理序号", 受理序号);
            dict.Add("案件序号", 案件序号);
            dict.Add("鉴定方法", 鉴定方法);
            if (鉴定状态.Equals("新的委托"))
            {
                dict.Add("鉴定状态", "检验中");
            }
            else if (鉴定状态.Equals("不予受理"))
            {
                dict.Add("鉴定状态", "不予受理");
                dict["受理序号"] = string.Empty;
                dict["案件序号"] = string.Empty;
            }
            DBHelperSQL.Update("鉴定流程", "委托编号='" + 委托编号 + "'", dict, dbConnection, trans);
        }
        [WebMethod]
        public string UpdateAccept(string ID, string 委托编号, string 受理年份, string 受理序号, string 案件序号, string 受理人员, string 受理时间,
            string 计划完成, string 认证认可, string 受理意见, string 一检人, string 鉴定状态, string SLN, string 文书名称, string 鉴定单位, string 鉴定方法)
        {
            if (!DataReader.ValidAccNo(鉴定单位, 委托编号, 文书名称, 受理年份, 受理序号))
            {
                return "受理序号重复！请检查。";
            }

            DataSet ds = DBHelperSQL.Select("鉴定材料", "委托编号='" + 委托编号 + "'", "是否样本,创建时间", "ID");

            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    UpdateAcceptFlow(委托编号, 受理年份, 受理序号, 案件序号, 受理人员, 受理时间, 计划完成, 认证认可, 受理意见, 一检人, 鉴定状态, 鉴定方法, dbConnection, trans);

                    if (鉴定状态.Equals("新的委托"))
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("案件编号", 受理年份 + "-" + 受理序号);
                        DBHelperSQL.Update("案件信息", "ID='" + ID + "'", dict, dbConnection, trans);

                        int next = 1;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            dict = new Dictionary<string, string>();
                            dict.Add("材料编号", Helper.GetNumStr(SLN, 受理年份, 受理序号, next.ToString()));
                            DBHelperSQL.Update("鉴定材料", "ID='" + dr["ID"] + "'", dict, dbConnection, trans);
                            next++;
                        }
                    }

                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string UpdateAcceptMp(string ID, string 委托编号, string 受理年份, string 受理序号, string 案件序号, string 受理人员, string 受理时间,
            string 计划完成, string 认证认可, string 受理意见, string 一检人, string 鉴定状态, string 样本编号, string 文书名称, string 鉴定单位, string 鉴定方法)
        {
            if (!DataReader.ValidAccNo(鉴定单位, 委托编号, 文书名称, 受理年份, 受理序号))
            {
                return "受理序号重复！请检查。";
            }
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        UpdateAcceptFlow(委托编号, 受理年份, 受理序号, 案件序号, 受理人员, 受理时间, 计划完成, 认证认可, 受理意见, 一检人, 鉴定状态, 鉴定方法, dbConnection, trans);

                        if (鉴定状态.Equals("新的委托"))
                        {
                            IDictionary<string, string> dict = new Dictionary<string, string>();
                            dict.Add("样本编号", 样本编号);
                            DBHelperSQL.Update("失踪人员", "ID='" + ID + "'", dict, dbConnection, trans);


                        }
                        trans.Commit();
                    }
                }
                return "1";
            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("违反唯一约束条件"))
                {
                    Helper.NoForDnaNext = Helper.NoForDnaNext + 250;
                    return "异常失败，请尝试重新受理。";
                }
                return ex.Message;
            }
        }
        [WebMethod]
        public string UpdateAcceptMpr(string ID, string 亲属一ID, string 亲属二ID, string 委托编号, string 受理年份, string 受理序号, string 案件序号, string 受理人员, string 受理时间,
            string 计划完成, string 认证认可, string 受理意见, string 一检人, string 鉴定状态, string 样本编号1, string 样本编号2, string 文书名称, string 鉴定单位, string 鉴定方法)
        {
            if (!DataReader.ValidAccNo(鉴定单位, 委托编号, 文书名称, 受理年份, 受理序号))
            {
                return "受理序号重复！请检查。";
            }
            try
            {

                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        UpdateAcceptFlow(委托编号, 受理年份, 受理序号, 案件序号, 受理人员, 受理时间, 计划完成, 认证认可, 受理意见, 一检人, 鉴定状态, 鉴定方法, dbConnection, trans);

                        if (鉴定状态.Equals("新的委托"))
                        {
                            string sql = "update 亲属信息 set 样本编号='" + 样本编号1 + "' where id='" + 亲属一ID + "';";
                            if (亲属二ID.Length > 0)
                            {
                                sql += "update 亲属信息 set 样本编号='" + 样本编号2 + "' where id='" + 亲属二ID + "';";
                            }
                            DBHelperSQL.ExecuteSql(sql);


                        }

                        trans.Commit();
                    }
                }
                return "1";
            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("违反唯一约束条件"))
                {
                    Helper.NoForDnaNext = Helper.NoForDnaNext + 250;
                    return "异常失败，请尝试重新受理。";
                }
                return ex.Message;
            }
        }
        [WebMethod]
        public string UpdateAcceptDna(string ID, string SRCID, string 委托编号, string 受理年份, string 受理序号, string 案件序号, string 受理人员, string 受理时间,
             string 计划完成, string 认证认可, string 受理意见, string 一检人, string 鉴定状态, string cln, string WholeNo,
             string SE_PRE, string CPS_PRE, string CR_PRE, string UD_PRE,
             string SE_START, string CPS_START, string CR_START, string UD_START,
             string SE_LEN, string CPS_LEN, string CR_LEN, string UD_LEN, string 文书名称, string 鉴定单位, string 鉴定方法)
        {
            if (!DataReader.ValidAccNo(鉴定单位, 委托编号, 文书名称, 受理年份, 受理序号))
            {
                return "受理序号重复！请检查。";
            }

            DataSet seDs = DBHelperSQL.Select("样本信息", "委托编号='" + 委托编号 + "'", "创建时间", "ID");
            DataSet cpsDs = DBHelperSQL.Select("涉案人员", "委托编号='" + 委托编号 + "'", "创建时间", "ID,库类型,创建时间");
            DataSet crDs = DBHelperSQL.Select("案件亲属视图", "委托编号='" + 委托编号 + "'", "创建时间", "ID,亲属一ID,亲属二ID,库类型,创建时间");
            DataSet udDs = DBHelperSQL.Select("无名尸体", "委托编号='" + 委托编号 + "'", "创建时间", "ID");
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        UpdateAcceptFlow(委托编号, 受理年份, 受理序号, 案件序号, 受理人员, 受理时间, 计划完成, 认证认可, 受理意见, 一检人, 鉴定状态, 鉴定方法, dbConnection, trans);

                        if (鉴定状态.Equals("新的委托"))
                        {
                            string sql = "update 案件信息 set 案件编号='" + cln + "' where id='" + ID + "';";

                            int len = Convert.ToInt32(SE_LEN);
                            int start = Convert.ToInt32(SE_START);

                            if (WholeNo.Equals("1"))
                            {

                                //受害人
                                DataRow[] drs = cpsDs.Tables[0].Select("库类型='受害人'", "创建时间");
                                foreach (DataRow dr in drs)
                                {
                                    sql += "update 涉案人员 set 样本编号='" + SE_PRE + start.ToString().PadLeft(len, '0') +
                                        "' where id='" + dr[0].ToString() + "';";
                                    start++;
                                }
                                //物证检材
                                foreach (DataRow dr in seDs.Tables[0].Rows)
                                {
                                    sql += "update 样本信息 set 样本编号='" + SE_PRE + start.ToString().PadLeft(len, '0') +
                                        "' where id='" + dr[0].ToString() + "';";
                                    start++;
                                }
                                //嫌疑人
                                drs = cpsDs.Tables[0].Select("库类型='嫌疑人'", "创建时间");
                                foreach (DataRow dr in drs)
                                {
                                    sql += "update 涉案人员 set 样本编号='" + SE_PRE + start.ToString().PadLeft(len, '0') +
                                        "' where id='" + dr[0].ToString() + "';";
                                    start++;
                                }

                                //无名尸
                                foreach (DataRow dr in udDs.Tables[0].Rows)
                                {
                                    sql += "update 无名尸体 set 样本编号='" + SE_PRE + start.ToString().PadLeft(len, '0') +
                                    "' where id='" + dr[0].ToString() + "';";
                                    start++;
                                }
                                //受害人亲属
                                drs = crDs.Tables[0].Select("库类型='受害人亲属'", "创建时间");
                                foreach (DataRow dr in drs)
                                {
                                    sql += "update 亲属信息 set 样本编号='" + SE_PRE + start.ToString().PadLeft(len, '0') +
                                        "' where id='" + dr["亲属一ID"].ToString() + "';";
                                    start++;
                                    if (dr["亲属二ID"].ToString().Length > 0)
                                    {
                                        sql += "update 亲属信息 set 样本编号='" + SE_PRE + start.ToString().PadLeft(len, '0') +
                                            "' where id='" + dr["亲属二ID"].ToString() + "';";
                                        start++;
                                    }
                                }
                                //嫌疑人亲属
                                drs = crDs.Tables[0].Select("库类型='嫌疑人亲属'", "创建时间");
                                foreach (DataRow dr in drs)
                                {
                                    sql += "update 亲属信息 set 样本编号='" + SE_PRE + start.ToString().PadLeft(len, '0') +
                                        "' where id='" + dr["亲属一ID"].ToString() + "';";
                                    start++;
                                    if (dr["亲属二ID"].ToString().Length > 0)
                                    {
                                        sql += "update 亲属信息 set 样本编号='" + SE_PRE + start.ToString().PadLeft(len, '0') +
                                            "' where id='" + dr["亲属二ID"].ToString() + "';";
                                        start++;
                                    }
                                }
                                //其他人员
                                drs = cpsDs.Tables[0].Select("库类型='其他人员'", "创建时间");
                                foreach (DataRow dr in drs)
                                {
                                    sql += "update 涉案人员 set 样本编号='" + SE_PRE + start.ToString().PadLeft(len, '0') +
                                        "' where id='" + dr[0].ToString() + "';";
                                    start++;
                                }

                            }
                            else
                            {
                                //物证检材
                                len = Convert.ToInt32(SE_LEN);
                                start = Convert.ToInt32(SE_START);
                                foreach (DataRow dr in seDs.Tables[0].Rows)
                                {
                                    sql += "update 样本信息 set 样本编号='" + SE_PRE + start.ToString().PadLeft(len, '0') +
                                    "' where id='" + dr[0].ToString() + "';";
                                    start++;
                                }

                                //辽宁修改编号 （yyyy-FW-cccc-YBnnn）（yyyy-FW-cccc-JCnnn）
                                //受害人
                                len = Convert.ToInt32(CPS_LEN);
                                start = Convert.ToInt32(CPS_START);
                                DataRow[] drs = cpsDs.Tables[0].Select("库类型='受害人'", "创建时间");
                                foreach (DataRow dr in drs)
                                {
                                    sql += "update 涉案人员 set 样本编号='" + CPS_PRE + start.ToString().PadLeft(len, '0') +
                                    "' where id='" + dr[0].ToString() + "';";
                                    start++;
                                }
                                //嫌疑人
                                drs = cpsDs.Tables[0].Select("库类型='嫌疑人'", "创建时间");
                                foreach (DataRow dr in drs)
                                {
                                    sql += "update 涉案人员 set 样本编号='" + CPS_PRE + start.ToString().PadLeft(len, '0') +
                                    "' where id='" + dr[0].ToString() + "';";
                                    start++;
                                }
                                //受害人亲属
                                drs = crDs.Tables[0].Select("库类型='受害人亲属'", "创建时间");
                                foreach (DataRow dr in drs)
                                {
                                    sql += "update 亲属信息 set 样本编号='" + CPS_PRE + start.ToString().PadLeft(len, '0') +
                                        "' where id='" + dr["亲属一ID"].ToString() + "';";
                                    start++;
                                    if (dr["亲属二ID"].ToString().Length > 0)
                                    {
                                        sql += "update 亲属信息 set 样本编号='" + CPS_PRE + start.ToString().PadLeft(len, '0') +
                                            "' where id='" + dr["亲属二ID"].ToString() + "';";
                                        start++;
                                    }
                                }
                                //嫌疑人亲属
                                drs = crDs.Tables[0].Select("库类型='嫌疑人亲属'", "创建时间");
                                foreach (DataRow dr in drs)
                                {
                                    sql += "update 亲属信息 set 样本编号='" + CPS_PRE + start.ToString().PadLeft(len, '0') +
                                        "' where id='" + dr["亲属一ID"].ToString() + "';";
                                    start++;
                                    if (dr["亲属二ID"].ToString().Length > 0)
                                    {
                                        sql += "update 亲属信息 set 样本编号='" + CPS_PRE + start.ToString().PadLeft(len, '0') +
                                            "' where id='" + dr["亲属二ID"].ToString() + "';";
                                        start++;
                                    }
                                }
                                //无名尸
                                foreach (DataRow dr in udDs.Tables[0].Rows)
                                {
                                    sql += "update 无名尸体 set 样本编号='" + CPS_PRE + start.ToString().PadLeft(len, '0') +
                                    "' where id='" + dr[0].ToString() + "';";
                                    start++;
                                }
                                //其他人员
                                drs = cpsDs.Tables[0].Select("库类型='其他人员'", "创建时间");
                                foreach (DataRow dr in drs)
                                {
                                    sql += "update 涉案人员 set 样本编号='" + CPS_PRE + start.ToString().PadLeft(len, '0') +
                                    "' where id='" + dr[0].ToString() + "';";
                                    start++;
                                }


                            }
                            //提交
                            if (sql.Length > 0)
                            {
                                DBHelperSQL.ExecuteSql(sql, dbConnection, trans);
                            }

                        }

                        trans.Commit();
                    }
                }
                return "1";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("违反唯一约束条件"))
                {
                    Helper.NoForDnaNext = Helper.NoForDnaNext + 250;
                    return "异常失败，请尝试重新受理。";
                }
                return ex.Message;
            }
        }
        [WebMethod]
        public string UpdateConclusion(string 委托编号, string 鉴定结论, string 结论概述, string ID)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("鉴定结论", 鉴定结论);
            dict.Add("结论概述", 结论概述);
            return DBHelperSQL.Update("鉴定流程", "委托编号='" + 委托编号 + "'", dict);

        }
        [WebMethod]
        public string UpdateTesterFinish(string 鉴定单位, string 委托编号, string 一检留言, string 二检人, string 三检人, string 四检人,
            string 复核人, string 授权签字, string 技管, string 领导, string 是否提交, string 存档原因, string 发文年份, string 发文序号)
        {
            //   if (!CheckConsignDoc(鉴定单位, 委托编号))
            //   {
            //       return "委托书未打印，请先将委托书补充回来";
            //    }
            if (!CheckAcceptDoc(鉴定单位, 委托编号))
            {
                return "受理确认书未打印，请先将受理确认书补充回来";
            }
            IDictionary<string, string> dict = new Dictionary<string, string>();
            if (是否提交.Equals("0"))
            {
                dict.Add("二检人", 二检人);
                dict.Add("三检人", 三检人);
                dict.Add("四检人", 四检人);
                dict.Add("复核人", 复核人);
                dict.Add("授权签字", 授权签字);
                dict.Add("技管", 技管);
                dict.Add("领导", 领导);
            }
            else if (是否提交.Equals("1"))
            {
                dict.Add("一检留言", 一检留言);
                dict.Add("二检人", 二检人);
                dict.Add("三检人", 三检人);
                dict.Add("四检人", 四检人);
                dict.Add("复核人", 复核人);
                dict.Add("授权签字", 授权签字);
                dict.Add("技管", 技管);
                dict.Add("领导", 领导);
                dict.Add("鉴定状态", "审核中");
                dict.Add("一检完成", DateTime.Now.ToString());
                dict.Add("鉴定记事", DateTime.Now.ToString() + "  一检人将本案件提交进入审核。\r\n\n" + GetIdRecord(委托编号));
            }
            else if (是否提交.Equals("2"))
            {
                dict.Add("鉴定状态", "已存档");
                dict.Add("发文年份", 发文年份);
                dict.Add("发文序号", 发文序号);
                dict.Add("一检完成", DateTime.Now.ToString());
                dict.Add("鉴定记事", DateTime.Now.ToString() + "  一检人将本案件存档。原因为：\r\n" + 存档原因 + "\r\n\n" + GetIdRecord(委托编号));
            }
            return DBHelperSQL.Update("鉴定流程", "委托编号='" + 委托编号 + "'", dict);
        }
        [WebMethod]
        public string UpdateAudit(string 委托编号, string 操作人时间字段, string 操作人, string 操作原因, string 鉴定状态)
        {
            string sf = string.Empty;
            switch (操作人时间字段)
            {
                case "二检完成": sf = "二检人"; break;
                case "三检完成": sf = "三检人"; break;
                case "四检完成": sf = "四检人"; break;
                case "复核完成": sf = "复核人"; break;
                case "签字完成": sf = "授权签字人"; break;
                case "技管完成": sf = "科室负责人"; break;
                case "审批完成": sf = "领导"; break;
            }

            IDictionary<string, string> dict = new Dictionary<string, string>();
            if (鉴定状态.Equals("通过"))
            {
                dict.Add(操作人时间字段, DateTime.Now.ToString());
                if (操作人时间字段.Equals("审批完成"))
                {
                    dict.Add("鉴定状态", "文书制作");
                    dict.Add("鉴定记事", DateTime.Now.ToString() + "  " + sf + 操作人 + "审核通过。意见为：\r\n" + 操作原因 + "\r\n\n" + GetIdRecord(委托编号));
                }
                else if (操作人时间字段.Equals("发文确认"))
                {
                    string[] docInfo = 操作原因.Split('-');
                    dict.Add("鉴定状态", "待领取");
                    dict.Add("发文年份", docInfo[0]);
                    dict.Add("发文序号", docInfo[1]);
                    dict.Add("鉴定记事", DateTime.Now.ToString() + "  文书制作完成，进入待领取状态。\r\n\n" + GetIdRecord(委托编号));
                }
                else
                {
                    dict.Add("鉴定记事", DateTime.Now.ToString() + "  " + sf + 操作人 + "审核通过。意见为：\r\n" + 操作原因 + "\r\n\n" + GetIdRecord(委托编号));
                }
            }
            else if (鉴定状态.Equals("存档"))
            {
                dict.Add(操作人时间字段, DateTime.Now.ToString());
                dict.Add("鉴定记事", DateTime.Now.ToString() + "  " + sf + 操作人 + "将本案件存档。原因为：\r\n" + 操作原因 + "\r\n\n" + GetIdRecord(委托编号));
                dict.Add("鉴定状态", "已存档");
            }
            else if (鉴定状态.Equals("退回") || 鉴定状态.Equals("激活"))
            {
                dict.Add("一检完成", string.Empty);
                dict.Add("二检完成", string.Empty);
                dict.Add("三检完成", string.Empty);
                dict.Add("四检完成", string.Empty);
                dict.Add("复核完成", string.Empty);
                dict.Add("签字完成", string.Empty);
                dict.Add("技管完成", string.Empty);
                dict.Add("审批完成", string.Empty);
                dict.Add("鉴定记事", DateTime.Now.ToString() + "  " + sf + 操作人 + "将本案件" + 鉴定状态 + "。原因为：\r\n" + 操作原因 + "\r\n\n" + GetIdRecord(委托编号));
                dict.Add("鉴定状态", "检验中");
            }
            else if (鉴定状态.Equals("修改发文号"))
            {
                string[] docInfo = 操作原因.Split('-');
                dict.Add("发文年份", docInfo[0]);
                dict.Add("发文序号", docInfo[1]);
            }
            return DBHelperSQL.Update("鉴定流程", "委托编号='" + 委托编号 + "'", dict);
        }
        [WebMethod]
        public string UpdateRepGet(string 委托编号, string 文书领取, string 领取人一, string 领一电话,
            string 领取人二, string 领二电话, string 鉴定状态)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("文书领取", 文书领取);
            dict.Add("领取人一", 领取人一);
            dict.Add("领一电话", 领一电话);
            dict.Add("领取人二", 领取人二);
            dict.Add("领二电话", 领二电话);
            if (鉴定状态.Equals("待领取"))
            {
                dict.Add("鉴定状态", "已发文");
                dict.Add("领取完成", DateTime.Now.ToString());
                dict.Add("鉴定记事", DateTime.Now.ToString() + "  报告被" + 领取人一 + "(" + 领一电话 + ")领取。\r\n\n" + GetIdRecord(委托编号));
            }
            return DBHelperSQL.Update("鉴定流程", "委托编号='" + 委托编号 + "'", dict);
        }
        private string GetIdRecord(string 委托编号)
        {
            DataSet ds = DBHelperSQL.Select("鉴定流程", "委托编号='" + 委托编号 + "'", "委托编号", "鉴定记事");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            return string.Empty;
        }
        #endregion
        #region 任务
        [WebMethod]
        public string GetCaseAcceptTask(string 鉴定单位, string 案件名称, string 鉴定专业, string 鉴定类别, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位 = '" + 鉴定单位 + "' and 鉴定状态 ='新的委托' and ";
            if (案件名称.Length > 0) filter += "案件名称 like '%" + 案件名称 + "%' and ";
            if (鉴定专业.Length > 0) filter += "鉴定专业 = '" + 鉴定专业 + "' and ";
            if (鉴定类别.Length > 0) filter += "鉴定类别 = '" + 鉴定类别 + "' and ";

            return DBHelperSQL.SelectRowCount("鉴定流程视图", Helper.CutFilter(filter), "委托时间 DESC", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string GetAcceptByBarCode(string 鉴定单位, string 委托编号)
        {
            string filter = "鉴定单位 = '" + 鉴定单位 + "' and 鉴定状态 ='新的委托' and 委托编号 ='" + 委托编号 + "'";
            return DBHelperSQL.Select("鉴定流程视图", filter, "委托编号", "*").GetXml();
        }
        [WebMethod]
        public string GetTestTask(string 鉴定单位, string 一检人, string 案件名称, string 案件序号, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位 = '" + 鉴定单位 + "' and 鉴定状态 ='检验中' and 一检人='" + 一检人 + "' and ";
            if (案件名称.Length > 0) filter += "案件名称 like '%" + 案件名称 + "%' and ";
            if (案件序号.Length > 0) filter += "案件序号 = '" + 案件序号 + "' and ";

            return DBHelperSQL.SelectRowCount("鉴定流程视图", Helper.CutFilter(filter), "鉴定专业,受理年份 DESC,受理序号 desc,鉴定类别", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string GetAuditTask(string 鉴定单位, string 审核人, string pageSize, string pageIndex)
        {
            string filter = string.Format(@"鉴定单位 = '{0}' and 鉴定状态 ='审核中' and 
((二检人='{1}' and 二检完成 is null) or 
(三检人='{1}' and 三检完成 is null and 二检完成 is not null) or 
(四检人='{1}' and 四检完成 is null and 三检完成 is not null) or 
(复核人='{1}' and 复核完成 is null and 二检完成 is not null and (三检人 is null or 三检完成 is not null) and (四检人 is null or 四检完成 is not null)) or 
(授权签字='{1}' and 签字完成 is null and 二检完成 is not null and (三检人 is null or 三检完成 is not null) and (四检人 is null or 四检完成 is not null) and (复核人 is null or 复核完成 is not null)) or 
(技管='{1}' and 技管完成 is null and 二检完成 is not null and (三检人 is null or 三检完成 is not null) and (四检人 is null or 四检完成 is not null) and (复核人 is null or 复核完成 is not null) and (授权签字 is null or 签字完成 is not null)) or 
(领导='{1}' and 审批完成 is null and 二检完成 is not null and (三检人 is null or 三检完成 is not null) and (四检人 is null or 四检完成 is not null) and (复核人 is null or 复核完成 is not null) and (授权签字 is null or 签字完成 is not null) and (技管 is null or 技管完成 is not null))) and ", 鉴定单位, 审核人);

            return DBHelperSQL.SelectRowCount("鉴定流程视图", Helper.CutFilter(filter), "鉴定专业,受理年份 DESC,受理序号 desc,鉴定类别", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string GetDocMakeTask(string 鉴定单位, string 一检人, string 案件名称, string 案件序号, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位 = '" + 鉴定单位 + "' and 鉴定状态 ='文书制作' and 一检人='" + 一检人 + "' and 审批完成 is not null and 发文确认 is null and ";
            if (案件名称.Length > 0) filter += "案件名称 like '%" + 案件名称 + "%' and ";
            if (案件序号.Length > 0) filter += "案件序号 = '" + 案件序号 + "' and ";

            return DBHelperSQL.SelectRowCount("鉴定流程视图", Helper.CutFilter(filter), "鉴定专业,鉴定类别,受理年份 DESC,案件序号 desc", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string GetReportTask(string 鉴定单位, string 委托编号, string 案件名称, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位 = '" + 鉴定单位 + "' and 鉴定状态 ='待领取' and ";
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";
            if (案件名称.Length > 0) filter += "案件名称 like '%" + 案件名称 + "%' and ";

            return DBHelperSQL.SelectRowCount("鉴定流程视图", Helper.CutFilter(filter), "鉴定专业,受理年份 DESC,受理序号 desc,鉴定类别", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        #endregion
        #region 获取下一个编号
        [WebMethod]
        public string GetNextConNo(string 委托单位简称, string 委托年份, string 委托编号, string 鉴定单位)
        {
            string filter = "简称='" + 委托单位简称 + "' and 委托年份='" + 委托年份 + "' and 鉴定单位='" + 鉴定单位 + "' and 单位信息.ID=鉴定流程.委托单位 ";
            DataSet dataSet = DBHelperSQL.Select("鉴定流程,单位信息", filter, "委托序号 desc", "委托序号,委托编号");
            if (dataSet.Tables[0].Rows.Count == 0) return "1";
            else if (dataSet.Tables[0].Rows[0]["委托序号"].ToString().Length > 0)
            {
                if (dataSet.Tables[0].Rows[0]["委托编号"].ToString().Equals(委托编号))
                {
                    return dataSet.Tables[0].Rows[0]["委托序号"].ToString();
                }
                else
                {
                    return (Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                }
            }
            return "1";
        }
        [WebMethod]
        public string GetNextAccNo(string 鉴定单位, string 受理年份, string 文书名称, string 委托编号)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and 受理年份='" + 受理年份 + "' and 文书名称='" + 文书名称 + "'";
            DataSet dataSet = DBHelperSQL.Select("鉴定流程", filter, "受理序号 desc", "受理序号,委托编号");
            int slxh = 1;
            int ajxh = 1;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                if (dataSet.Tables[0].Rows[0]["受理序号"].ToString().Length > 0)
                {
                    if (!dataSet.Tables[0].Rows[0]["委托编号"].ToString().Equals(委托编号))
                    {
                        slxh = Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString()) + 1;
                    }
                }
            }
            dataSet = DBHelperSQL.Select("鉴定流程", filter, "案件序号 desc", "案件序号,委托编号");
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                if (dataSet.Tables[0].Rows[0]["案件序号"].ToString().Length > 0)
                {
                    if (!dataSet.Tables[0].Rows[0]["委托编号"].ToString().Equals(委托编号))
                    {
                        ajxh = Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString()) + 1;
                    }
                }
            }
            return slxh.ToString() + "，" + ajxh.ToString();
        }
        [WebMethod]
        public string GetNextDocNo(string 鉴定单位, string 发文年份, string 文书名称, string 委托编号)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and 发文年份='" + 发文年份 + "' and 文书名称='" + 文书名称 + "'";
            DataSet dataSet = DBHelperSQL.Select("鉴定流程", filter, "发文序号 desc", "发文序号,委托编号");
            if (dataSet.Tables[0].Rows.Count == 0) return "1";
            else if (dataSet.Tables[0].Rows[0]["发文序号"].ToString().Length > 0)
            {
                if (dataSet.Tables[0].Rows[0]["委托编号"].ToString().Equals(委托编号))
                {
                    return dataSet.Tables[0].Rows[0]["发文序号"].ToString();
                }
                else
                {
                    return (Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                }
            }
            return "1";
        }
        #endregion
        #region 其他
        [WebMethod]
        public string GetNextSLN(string preFix, string tableName)
        {
            return DataReader.GetNextSLN(preFix, tableName);
        }
        [WebMethod]
        public string GetCaseNextSLN(string preFixSe, string preFixCps, string preFixCr, string preFixUd)
        {
            return DataReader.GetNextSLN(preFixSe, "样本信息") + "-" +
                DataReader.GetNextSLN(preFixCps, "涉案人员") + "-" +
                DataReader.GetNextSLN(preFixCr, "亲属信息") + "-" +
                DataReader.GetNextSLN(preFixUd, "无名尸体");
        }
        #endregion
        /// <summary>
        /// 鉴定档案查询
        /// </summary>
        [WebMethod]
        public string QueryAllCase(string 打防管控, string 现场勘验, string 案件名称, string 案件类型, string 案件编号,
            string 发案地点, string s发案时间, string e发案时间, string 案件性质,
            string 鉴定单位, string 鉴定状态, string 委托单位, string 委托单位名称, string 委托年份, string 委托序号, string s委托时间, string e委托时间, string 鉴定专业,
            string 鉴定类别, string 鉴定项目, string 受理年份, string 受理序号, string 发文年份, string 发文序号, string 受理人员, string s受理时间,
            string e受理时间, string 认证认可, string 鉴定结论, string 一检人, string 二检人, string 三检人, string 四检人, string 复核人,
            string 授权签字, string 技管, string 领导, string 文书领取, string s领取完成, string e领取完成, string 送检人, string 被鉴定人, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (打防管控.Length > 0) filter += "打防管控='" + 打防管控 + "' and ";
            if (现场勘验.Length > 0) filter += "现场勘验='" + 现场勘验 + "' and ";
            if (案件名称.Length > 0) filter += "案件名称 like '%" + 案件名称 + "%' and ";
            if (案件类型.Length > 0) filter += "案件类型='" + 案件类型 + "' and ";
            if (案件编号.Length > 0) filter += "案件编号 like '%" + 案件编号 + "%' and ";
            if (发案地点.Length > 0) filter += "发案地点 like '%" + 发案地点 + "%' and ";
            if (s发案时间.Length > 0) filter += "发案时间>='" + s发案时间 + "' and ";
            if (e发案时间.Length > 0) filter += "发案时间<='" + e发案时间 + "' and ";
            if (案件性质.Length > 0) filter += "案件性质='" + 案件性质 + "' and ";

            if (鉴定单位.Length > 0) filter += "鉴定单位='" + 鉴定单位 + "' and (鉴定状态<>'新的委托') and ";
            if (鉴定状态.Length > 0) filter += "鉴定状态='" + 鉴定状态 + "' and ";
            if (委托单位.Length > 0) filter += "委托单位='" + 委托单位 + "' and ";
            if (委托单位名称.Length > 0) filter += "委托单位名称 like '%" + 委托单位名称 + "%' and ";
            if (委托年份.Length > 0) filter += "委托年份='" + 委托年份 + "' and ";
            if (委托序号.Length > 0) filter += "委托序号='" + 委托序号 + "' and ";
            if (s委托时间.Length > 0) filter += "委托时间>='" + s委托时间 + "' and ";
            if (e委托时间.Length > 0) filter += "委托时间<='" + e委托时间 + "' and ";

            if (鉴定专业.Length > 0) filter += "鉴定专业='" + 鉴定专业 + "' and ";
            if (鉴定类别.Length > 0) filter += "鉴定类别='" + 鉴定类别 + "' and ";
            if (鉴定项目.Length > 0) filter += "鉴定项目 like '%" + 鉴定项目 + "%' and ";
            if (受理年份.Length > 0) filter += "受理年份='" + 受理年份 + "' and ";
            if (受理序号.Length > 0) filter += "受理序号='" + 受理序号 + "' and ";
            if (发文年份.Length > 0) filter += "发文年份='" + 发文年份 + "' and ";
            if (发文序号.Length > 0) filter += "发文序号='" + 发文序号 + "' and ";
            if (受理人员.Length > 0) filter += "受理人员='" + 受理人员 + "' and ";
            if (s受理时间.Length > 0) filter += "受理时间>='" + s受理时间 + "' and ";
            if (e受理时间.Length > 0) filter += "受理时间<='" + e受理时间 + "' and ";
            if (认证认可.Length > 0) filter += "认证认可='" + 认证认可 + "' and ";
            if (鉴定结论.Length > 0) filter += "鉴定结论='" + 鉴定结论 + "' and ";

            if (一检人.Length > 0) filter += "一检人='" + 一检人 + "' and ";
            if (二检人.Length > 0) filter += "二检人='" + 二检人 + "' and ";
            if (三检人.Length > 0) filter += "三检人='" + 三检人 + "' and ";
            if (四检人.Length > 0) filter += "四检人='" + 四检人 + "' and ";
            if (复核人.Length > 0) filter += "复核人='" + 复核人 + "' and ";
            if (授权签字.Length > 0) filter += "授权签字='" + 授权签字 + "' and ";
            if (技管.Length > 0) filter += "技管='" + 技管 + "' and ";
            if (领导.Length > 0) filter += "领导='" + 领导 + "' and ";
            if (文书领取.Length > 0) filter += "文书领取='" + 文书领取 + "' and ";
            if (s领取完成.Length > 0) filter += "领取完成>='" + s领取完成 + "' and ";
            if (e领取完成.Length > 0) filter += "领取完成<='" + e领取完成 + "' and ";
            if (送检人.Length > 0) filter += "(一送姓名='" + 送检人 + "' or 二送姓名='" + 送检人 + "') and ";
            if (被鉴定人.Length > 0) filter += "被鉴定人 like '%" + 被鉴定人 + "%' and ";
            //if (被鉴定人姓名.Length > 0) filter += "被鉴定人姓名 like '%" + 被鉴定人姓名 + "%' and ";

            return DBHelperSQL.SelectRowCount("鉴定流程视图", Helper.CutFilter(filter), "受理年份 DESC,受理序号 desc,鉴定专业,鉴定类别,受理时间 desc", "*",
                 Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string PrintTz(string 打防管控, string 现场勘验, string 案件名称, string 案件类型, string 案件编号,
            string 发案地点, string s发案时间, string e发案时间, string 案件性质,
            string 鉴定单位, string 鉴定状态, string 委托单位, string 委托单位名称, string 委托年份, string 委托序号, string s委托时间, string e委托时间, string 鉴定专业,
            string 鉴定类别, string 鉴定项目, string 受理年份, string 受理序号, string 发文年份, string 发文序号, string 受理人员, string s受理时间,
            string e受理时间, string 认证认可, string 鉴定结论, string 一检人, string 二检人, string 三检人, string 四检人, string 复核人,
            string 授权签字, string 技管, string 领导, string 文书领取, string s领取完成, string e领取完成, string 送检人, string 被鉴定人)
        {
            string filter = string.Empty;
            if (打防管控.Length > 0) filter += "打防管控='" + 打防管控 + "' and ";
            if (现场勘验.Length > 0) filter += "现场勘验='" + 现场勘验 + "' and ";
            if (案件名称.Length > 0) filter += "案件名称 like '%" + 案件名称 + "%' and ";
            if (案件类型.Length > 0) filter += "案件类型='" + 案件类型 + "' and ";
            if (案件编号.Length > 0) filter += "案件编号 like '%" + 案件编号 + "%' and ";
            if (发案地点.Length > 0) filter += "发案地点 like '%" + 发案地点 + "%' and ";
            if (s发案时间.Length > 0) filter += "发案时间>='" + s发案时间 + "' and ";
            if (e发案时间.Length > 0) filter += "发案时间<='" + e发案时间 + "' and ";
            if (案件性质.Length > 0) filter += "案件性质='" + 案件性质 + "' and ";

            if (鉴定单位.Length > 0) filter += "鉴定单位='" + 鉴定单位 + "' and (鉴定状态<>'新的委托') and ";
            if (鉴定状态.Length > 0) filter += "鉴定状态='" + 鉴定状态 + "' and ";
            if (委托单位.Length > 0) filter += "委托单位='" + 委托单位 + "' and ";
            if (委托单位名称.Length > 0) filter += "委托单位名称 like '%" + 委托单位名称 + "%' and ";
            if (委托年份.Length > 0) filter += "委托年份='" + 委托年份 + "' and ";
            if (委托序号.Length > 0) filter += "委托序号='" + 委托序号 + "' and ";
            if (s委托时间.Length > 0) filter += "委托时间>='" + s委托时间 + "' and ";
            if (e委托时间.Length > 0) filter += "委托时间<='" + e委托时间 + "' and ";

            if (鉴定专业.Length > 0) filter += "鉴定专业='" + 鉴定专业 + "' and ";
            if (鉴定类别.Length > 0) filter += "鉴定类别='" + 鉴定类别 + "' and ";
            if (鉴定项目.Length > 0) filter += "鉴定项目 like '%" + 鉴定项目 + "%' and ";
            if (受理年份.Length > 0) filter += "受理年份='" + 受理年份 + "' and ";
            if (受理序号.Length > 0) filter += "受理序号='" + 受理序号 + "' and ";
            if (发文年份.Length > 0) filter += "发文年份='" + 发文年份 + "' and ";
            if (发文序号.Length > 0) filter += "发文序号='" + 发文序号 + "' and ";
            if (受理人员.Length > 0) filter += "受理人员='" + 受理人员 + "' and ";
            if (s受理时间.Length > 0) filter += "受理时间>='" + s受理时间 + "' and ";
            if (e受理时间.Length > 0) filter += "受理时间<='" + e受理时间 + "' and ";
            if (认证认可.Length > 0) filter += "认证认可='" + 认证认可 + "' and ";
            if (鉴定结论.Length > 0) filter += "鉴定结论='" + 鉴定结论 + "' and ";

            if (一检人.Length > 0) filter += "一检人='" + 一检人 + "' and ";
            if (二检人.Length > 0) filter += "二检人='" + 二检人 + "' and ";
            if (三检人.Length > 0) filter += "三检人='" + 三检人 + "' and ";
            if (四检人.Length > 0) filter += "四检人='" + 四检人 + "' and ";
            if (复核人.Length > 0) filter += "复核人='" + 复核人 + "' and ";
            if (授权签字.Length > 0) filter += "授权签字='" + 授权签字 + "' and ";
            if (技管.Length > 0) filter += "技管='" + 技管 + "' and ";
            if (领导.Length > 0) filter += "领导='" + 领导 + "' and ";
            if (文书领取.Length > 0) filter += "文书领取='" + 文书领取 + "' and ";
            if (s领取完成.Length > 0) filter += "领取完成>='" + s领取完成 + "' and ";
            if (e领取完成.Length > 0) filter += "领取完成<='" + e领取完成 + "' and ";
            if (送检人.Length > 0) filter += "(一送姓名='" + 送检人 + "' or 二送姓名='" + 送检人 + "') and ";
            if (被鉴定人.Length > 0) filter += "被鉴定人 like '%" + 被鉴定人 + "%' and ";
            //if (被鉴定人姓名.Length > 0) filter += "被鉴定人姓名 like '%" + 被鉴定人姓名 + "%' and ";

            DataSet ds = DBHelperSQL.Select("鉴定流程视图", Helper.CutFilter(filter), "受理年份 DESC,受理序号 desc,鉴定专业,鉴定类别,受理时间 desc", "*");
            string fileName = Helper.GenerateID();//.doc
            //excel处理，将ds里面的数据填充到“鉴定台账.xls”中
            //生成的excel存放到web的 Tmp/fileName里面
            //
            return ExcelOper.PrintTz(ds.Tables[0], fileName);
        }
        [WebMethod]
        public string PrintFWJL(string 打防管控, string 现场勘验, string 案件名称, string 案件类型, string 案件编号, string 发案地点, string s发案时间, string e发案时间, string 案件性质,
            string 鉴定单位, string 鉴定状态, string 委托单位, string 委托单位名称, string 委托年份, string 委托序号, string s委托时间, string e委托时间, string 鉴定专业,
            string 鉴定类别, string 鉴定项目, string 受理年份, string 受理序号, string 发文年份, string 发文序号, string 受理人员, string s受理时间,
            string e受理时间, string 认证认可, string 鉴定结论, string 一检人, string 二检人, string 三检人, string 四检人, string 复核人,
            string 授权签字, string 技管, string 领导, string 文书领取, string s领取完成, string e领取完成, string 送检人, string filename)
        {
            string filter = string.Empty;
            if (打防管控.Length > 0) filter += "打防管控='" + 打防管控 + "' and ";
            if (现场勘验.Length > 0) filter += "现场勘验='" + 现场勘验 + "' and ";
            if (案件名称.Length > 0) filter += "案件名称 like '%" + 案件名称 + "%' and ";
            if (案件类型.Length > 0) filter += "案件类型='" + 案件类型 + "' and ";
            if (案件编号.Length > 0) filter += "案件编号 like '%" + 案件编号 + "%' and ";
            if (发案地点.Length > 0) filter += "发案地点 like '%" + 发案地点 + "%' and ";
            if (s发案时间.Length > 0) filter += "发案时间>='" + s发案时间 + "' and ";
            if (e发案时间.Length > 0) filter += "发案时间<='" + e发案时间 + "' and ";
            if (案件性质.Length > 0) filter += "案件性质='" + 案件性质 + "' and ";

            if (鉴定单位.Length > 0) filter += "鉴定单位='" + 鉴定单位 + "' and (鉴定状态<>'新的委托') and ";
            if (鉴定状态.Length > 0) filter += "鉴定状态='" + 鉴定状态 + "' and ";
            if (委托单位.Length > 0) filter += "委托单位='" + 委托单位 + "' and ";
            if (委托单位名称.Length > 0) filter += "委托单位名称 like '%" + 委托单位名称 + "%' and ";
            if (委托年份.Length > 0) filter += "委托年份='" + 委托年份 + "' and ";
            if (委托序号.Length > 0) filter += "委托序号='" + 委托序号 + "' and ";
            if (s委托时间.Length > 0) filter += "委托时间>='" + s委托时间 + "' and ";
            if (e委托时间.Length > 0) filter += "委托时间<='" + e委托时间 + "' and ";

            if (鉴定专业.Length > 0) filter += "鉴定专业='" + 鉴定专业 + "' and ";
            if (鉴定类别.Length > 0) filter += "鉴定类别='" + 鉴定类别 + "' and ";
            if (鉴定项目.Length > 0) filter += "鉴定项目 like '%" + 鉴定项目 + "%' and ";
            if (受理年份.Length > 0) filter += "受理年份='" + 受理年份 + "' and ";
            if (受理序号.Length > 0) filter += "受理序号='" + 受理序号 + "' and ";
            if (发文年份.Length > 0) filter += "发文年份='" + 发文年份 + "' and ";
            if (发文序号.Length > 0) filter += "发文序号='" + 发文序号 + "' and ";
            if (受理人员.Length > 0) filter += "受理人员='" + 受理人员 + "' and ";
            if (s受理时间.Length > 0) filter += "受理时间>='" + s受理时间 + "' and ";
            if (e受理时间.Length > 0) filter += "受理时间<='" + e受理时间 + "' and ";
            if (认证认可.Length > 0) filter += "认证认可='" + 认证认可 + "' and ";
            if (鉴定结论.Length > 0) filter += "鉴定结论='" + 鉴定结论 + "' and ";

            if (一检人.Length > 0) filter += "一检人='" + 一检人 + "' and ";
            if (二检人.Length > 0) filter += "二检人='" + 二检人 + "' and ";
            if (三检人.Length > 0) filter += "三检人='" + 三检人 + "' and ";
            if (四检人.Length > 0) filter += "四检人='" + 四检人 + "' and ";
            if (复核人.Length > 0) filter += "复核人='" + 复核人 + "' and ";
            if (授权签字.Length > 0) filter += "授权签字='" + 授权签字 + "' and ";
            if (技管.Length > 0) filter += "技管='" + 技管 + "' and ";
            if (领导.Length > 0) filter += "领导='" + 领导 + "' and ";
            if (文书领取.Length > 0) filter += "文书领取='" + 文书领取 + "' and ";
            if (s领取完成.Length > 0) filter += "领取完成>='" + s领取完成 + "' and ";
            if (e领取完成.Length > 0) filter += "领取完成<='" + e领取完成 + "' and ";
            if (送检人.Length > 0) filter += "(一送姓名='" + 送检人 + "' or 二送姓名='" + 送检人 + "') and ";
            //if (被鉴定人姓名.Length > 0) filter += "被鉴定人姓名 like '%" + 被鉴定人姓名 + "%' and ";

            DataSet ds = DBHelperSQL.Select("鉴定流程视图", Helper.CutFilter(filter), "受理年份 DESC,受理序号 desc,鉴定专业,鉴定类别,受理时间 desc", "*");
            WordWS ws = new WordWS();
            return ws.FillFWJL(ds, filename);
        }
        public bool CheckConsignDoc(string 鉴定单位, string 委托编号)
        {
            bool bl = false;
            string tmpPath = ConfigurationManager.AppSettings["WebPath"] + 鉴定单位 + "\\鉴定档案\\" + 委托编号 + "\\文书记录\\";
            if (!Directory.Exists(tmpPath))
            {
                bl = false;
            }
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(tmpPath);
                FileInfo[] files = dirInfo.GetFiles("*.doc", SearchOption.TopDirectoryOnly);

                foreach (FileInfo file in files)
                {
                    if (file.Name.Contains("委托书"))
                    {
                        bl = true;
                        break;
                    }
                }
            }
            return bl;
        }
        public bool CheckAcceptDoc(string 鉴定单位, string 委托编号)
        {
            bool bl = false;
            string tmpPath = ConfigurationManager.AppSettings["WebPath"] + 鉴定单位 + "\\鉴定档案\\" + 委托编号 + "\\文书记录\\";
            if (!Directory.Exists(tmpPath))
            {
                bl = false;
            }
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(tmpPath);
                FileInfo[] files = dirInfo.GetFiles("*.doc", SearchOption.TopDirectoryOnly);

                foreach (FileInfo file in files)
                {
                    if (file.Name.Replace(" ", "").Contains("确认书"))
                    {
                        bl = true;
                        break;
                    }
                }
            }
            return bl;
        }
    }
}
