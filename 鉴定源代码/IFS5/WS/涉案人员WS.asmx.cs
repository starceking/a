using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using LIB;
using DAL;
using System.Collections.Generic;
using System.Data.OracleClient;
using DNADAL;
using IFSOracleDAL;
using System.Data.SqlClient;

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
    public class 涉案人员WS : System.Web.Services.WebService
    {
        [WebMethod]
        public string Insert(string ID, string 案件ID, string 委托编号, string 库类型, string 姓名, string 样本类型,
            string 性别, string 人员类型, string 出生日期, string 民族, string 国籍, string 身份证, string 学历, string 身份, string 籍贯, string 现住址,
            string 包装情况, string 样本描述, string 备注)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("案件ID", 案件ID);
            dict.Add("委托编号", 委托编号);
            dict.Add("库类型", 库类型);
            dict.Add("姓名", 姓名);
            dict.Add("样本类型", 样本类型);
            dict.Add("性别", 性别);
            dict.Add("人员类型", 人员类型);
            dict.Add("出生日期", 出生日期);
            dict.Add("民族", 民族);
            dict.Add("国籍", 国籍);
            dict.Add("身份证", 身份证);
            dict.Add("学历", 学历);
            dict.Add("身份", 身份);
            dict.Add("籍贯", 籍贯);
            dict.Add("现住址", 现住址);
            dict.Add("包装情况", 包装情况);
            dict.Add("样本描述", 样本描述);
            dict.Add("备注", 备注);
            return DBHelperSQL.Insert("涉案人员", dict);
        }
        [WebMethod]
        public string InsertWithNo(string ID, string 案件ID, string 委托编号, string 库类型, string 姓名, string 样本类型,
            string 性别, string 人员类型, string 出生日期, string 民族, string 国籍, string 身份证, string 学历, string 身份, string 籍贯, string 现住址,
            string 包装情况, string 样本描述, string 备注,
            string preFix, string tableName, string len, string userName, string CASE_ORA_FLAG)//2012.1.4
        {
            string numStr = preFix + DataReader.GetNextSLN(preFix, tableName).PadLeft(Convert.ToInt32(len), '0');

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("案件ID", 案件ID);
            dict.Add("委托编号", 委托编号);
            dict.Add("库类型", 库类型);
            dict.Add("姓名", 姓名);
            dict.Add("样本类型", 样本类型);
            dict.Add("性别", 性别);
            dict.Add("人员类型", 人员类型);
            dict.Add("出生日期", 出生日期);
            dict.Add("民族", 民族);
            dict.Add("国籍", 国籍);
            dict.Add("身份证", 身份证);
            dict.Add("学历", 学历);
            dict.Add("身份", 身份);
            dict.Add("籍贯", 籍贯);
            dict.Add("现住址", 现住址);
            dict.Add("包装情况", 包装情况);
            dict.Add("样本描述", 样本描述);
            dict.Add("备注", 备注);
            dict.Add("样本编号", numStr);
            dict.Add("ORA_FLAG", CASE_ORA_FLAG);

            if (CASE_ORA_FLAG.Equals("1"))
            {
                string userOraId = DBHelperOracle.GetUserOraId(userName);
                DNADICTWS dictWs = new DNADICTWS();
                IDictionary<string, string> EVIDENCE_TYPE = dictWs.GetDnaDict("样本类型");
                IDictionary<string, string> GENDER = dictWs.GetDnaDict("性别");
                IDictionary<string, string> PERSONNEL_TYPE = dictWs.GetDnaDict("人员类型");
                IDictionary<string, string> NATIONALITY = dictWs.GetDnaDict("民族");
                IDictionary<string, string> COUNTRY = dictWs.GetDnaDict("国籍");
                IDictionary<string, string> EDUCATION_LEVEL = dictWs.GetDnaDict("学历");
                IDictionary<string, string> IDENTITY = dictWs.GetDnaDict("身份");
                IDictionary<string, string> RELATION = dictWs.GetDnaDict("亲属关系");
                IDictionary<string, string> RELATION_WITH_TARGET = dictWs.GetDnaDict("目标关系");
                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                DBHelperSQL.Insert("涉案人员", dict, dbConnection, trans);
                                CASE_PERSONNEL_SAMPLEOracleDAL.AddWithTransaction(ID, 案件ID, Helper.GetRelationWithCaseByScType(库类型),
                                   姓名,
                            EVIDENCE_TYPE[样本类型], GENDER[性别],
                            PERSONNEL_TYPE[人员类型], 出生日期, NATIONALITY[民族],
                            COUNTRY[国籍], 身份证, EDUCATION_LEVEL[学历],
                            IDENTITY[身份], 籍贯, 现住址,
                            包装情况, 样本描述, 备注,
                            numStr, Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, Helper.GetNoPreByScType(库类型)),
                            userName, userOraId, dbConnectionOra, transOra);

                                trans.Commit();
                                transOra.Commit();
                            }
                        }
                    }
                }
            }
            else
            {
                DBHelperSQL.Insert("涉案人员", dict);
            }
            return numStr;
        }
        [WebMethod]
        public string Update(string ID, string 姓名, string 样本类型,
            string 性别, string 人员类型, string 出生日期, string 民族, string 国籍, string 身份证, string 学历, string 身份, string 籍贯, string 现住址,
            string 包装情况, string 样本描述, string 备注, string 样本编号, string ORA_FLAG)//2012.1.4
        {
            if (!DataReader.ValidSlnDup(样本编号, ID))
            {
                return "样本编号重复！请检查。";
            }

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("姓名", 姓名);
            dict.Add("样本类型", 样本类型);
            dict.Add("性别", 性别);
            dict.Add("人员类型", 人员类型);
            dict.Add("出生日期", 出生日期);
            dict.Add("民族", 民族);
            dict.Add("国籍", 国籍);
            dict.Add("身份证", 身份证);
            dict.Add("学历", 学历);
            dict.Add("身份", 身份);
            dict.Add("籍贯", 籍贯);
            dict.Add("现住址", 现住址);
            dict.Add("包装情况", 包装情况);
            dict.Add("样本描述", 样本描述);
            dict.Add("备注", 备注);
            dict.Add("样本编号", 样本编号);

            if (ORA_FLAG.Equals("1"))
            {
                DNADICTWS dictWs = new DNADICTWS();
                IDictionary<string, string> EVIDENCE_TYPE = dictWs.GetDnaDict("样本类型");
                IDictionary<string, string> GENDER = dictWs.GetDnaDict("性别");
                IDictionary<string, string> PERSONNEL_TYPE = dictWs.GetDnaDict("人员类型");
                IDictionary<string, string> NATIONALITY = dictWs.GetDnaDict("民族");
                IDictionary<string, string> COUNTRY = dictWs.GetDnaDict("国籍");
                IDictionary<string, string> EDUCATION_LEVEL = dictWs.GetDnaDict("学历");
                IDictionary<string, string> IDENTITY = dictWs.GetDnaDict("身份");
                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                DBHelperSQL.Update("涉案人员", "ID='" + ID + "'", dict, dbConnection, trans);
                                CASE_PERSONNEL_SAMPLEOracleDAL.UpdateWithTransaction(ID, 姓名,
                                        EVIDENCE_TYPE[样本类型], GENDER[性别],
                                        PERSONNEL_TYPE[人员类型], 出生日期, NATIONALITY[民族],
                                        COUNTRY[国籍], 身份证, EDUCATION_LEVEL[学历],
                                        IDENTITY[身份], 籍贯, 现住址,
                                        包装情况, 样本描述, 备注,
                                        样本编号, "admin", dbConnectionOra, transOra);

                                trans.Commit();
                                transOra.Commit();
                            }
                        }
                    }
                }
                return "1";
            }
            else
            {
                return DBHelperSQL.Update("涉案人员", "ID='" + ID + "'", dict);
            }
        }
        [WebMethod]
        public string Delete(string ID, string ORA_FLAG)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("涉案人员", "ID，" + ID);
            if (ORA_FLAG.Equals("1"))
            {
                string sql = "delete from gdna.CASE_PERSONNEL_SAMPLE where id='" + ID + "';";
                sql += "delete from gdna.SAMPLE_EXAMINATION where SAMPLE_ID='" + ID + "';";
                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                DBHelperSQL.Delete(dict, dbConnection, trans);
                                DBHelperOracle.ExecuteSqlWithTransaction(sql, dbConnectionOra, transOra);
                                trans.Commit();
                                transOra.Commit();
                            }
                        }
                    }
                }
                return "1";
            }
            else
            {
                return DBHelperSQL.Delete(dict);
            }
        }
        [WebMethod]
        public string GetAll(string 案件ID, string 委托编号, string 库类型, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (库类型.Length > 0) filter += "库类型='" + 库类型 + "' and ";

            return DBHelperSQL.SelectRowCount("涉案人员", Helper.CutFilter(filter), "创建时间", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
    }
}
