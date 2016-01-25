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
using System.Data.SqlClient;
using System.Data.OracleClient;
using DNADAL;
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
    public class 失踪人员WS : System.Web.Services.WebService
    {
        [WebMethod]
        public string Insert(string ID, string 委托编号, string 姓名, string 样本类型,
            string 性别, string 人员类型, string 出生日期, string 民族, string 国籍, string 身份证, string 学历, string 身份, string 籍贯, string 现住址,
            string 包装情况, string 样本描述, string 备注, string 案件名称,
            string 鉴定单位, string 委托表号, string 委托单位, string 送检人一,
            string 一送姓名, string 一送警号, string 一送电话, string 二送姓名, string 二送警号, string 二送电话,
            string 委托年份, string 委托序号, string 委托时间, string 鉴定要求, string 文书名称)
        {
            委托编号 = Helper.GenerateConNo(委托编号);

            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("委托编号", 委托编号);
                    dict.Add("鉴定单位", 鉴定单位);
                    dict.Add("委托表号", Helper.GenerateID());
                    dict.Add("委托单位", 委托单位);
                    dict.Add("送检人一", 送检人一);
                    dict.Add("一送姓名", 一送姓名);
                    dict.Add("一送警号", 一送警号);
                    dict.Add("一送电话", 一送电话);
                    dict.Add("二送姓名", 二送姓名);
                    dict.Add("二送警号", 二送警号);
                    dict.Add("二送电话", 二送电话);
                    dict.Add("委托年份", 委托年份);
                    dict.Add("委托序号", 委托序号);
                    dict.Add("委托时间", 委托时间);
                    dict.Add("鉴定专业", "DNA");
                    dict.Add("鉴定类别", "失踪人员");
                    dict.Add("鉴定要求", 鉴定要求);
                    dict.Add("文书名称", 文书名称);
                    DBHelperSQL.Insert("鉴定流程", dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("ID", ID);
                    dict.Add("委托编号", 委托编号);
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
                    dict.Add("案件名称", 案件名称);
                    DBHelperSQL.Insert("失踪人员", dict, dbConnection, trans);

                    trans.Commit();
                }
            }
            return 委托编号;
        }
        [WebMethod]
        public string Update(string ID, string 委托编号, string 姓名, string 样本类型,
            string 性别, string 人员类型, string 出生日期, string 民族, string 国籍, string 身份证, string 学历, string 身份, string 籍贯, string 现住址,
            string 包装情况, string 样本描述, string 备注, string 案件名称, string 样本编号,
            string 一送姓名, string 一送警号, string 一送电话, string 二送姓名, string 二送警号, string 二送电话,
            string 委托年份, string 委托序号, string 委托时间, string 鉴定要求, string ORA_FLAG)//2012.1.4
        {
            if (!DataReader.ValidSlnDup(样本编号, ID))
            {
                return "样本编号重复！请检查。";
            }

            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
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
                    dict.Add("案件名称", 案件名称);
                    dict.Add("样本编号", 样本编号);
                    DBHelperSQL.Update("失踪人员", "ID='" + ID + "'", dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("一送姓名", 一送姓名);
                    dict.Add("一送警号", 一送警号);
                    dict.Add("一送电话", 一送电话);
                    dict.Add("二送姓名", 二送姓名);
                    dict.Add("二送警号", 二送警号);
                    dict.Add("二送电话", 二送电话);
                    dict.Add("委托年份", 委托年份);
                    dict.Add("委托序号", 委托序号);
                    dict.Add("委托时间", 委托时间);
                    dict.Add("鉴定要求", 鉴定要求);
                    DBHelperSQL.Update("鉴定流程", "委托编号='" + 委托编号 + "'", dict, dbConnection, trans);

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
                        using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                        {
                            dbConnectionOra.Open();
                            using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                            {
                                MISSING_PERSONOracleDAL.UpdateWithTransaction(ID, 姓名,
                                    EVIDENCE_TYPE[样本类型], GENDER[性别],
                                    PERSONNEL_TYPE[人员类型], 出生日期, NATIONALITY[民族],
                                    COUNTRY[国籍], 身份证, EDUCATION_LEVEL[学历],
                                    IDENTITY[身份], 籍贯, 现住址,
                                    包装情况, 样本描述, 备注,
                                    样本编号, "admin", dbConnectionOra, transOra);
                                transOra.Commit();
                            }
                        }
                    }

                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string Delete(string ID, string CONSIGNID, string 委托编号, string ORA_FLAG)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("失踪人员", "ID，" + ID);
            dict.Add("鉴定流程", "委托编号，" + 委托编号);

            if (ORA_FLAG.Equals("1"))
            {
                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        DBHelperSQL.Delete(dict, dbConnection, trans);
                        DeleteMPOra(ID, CONSIGNID);
                        trans.Commit();
                    }
                }
                return "1";
            }
            else
                return DBHelperSQL.Delete(dict);
        }
        public static void DeleteMPOra(string ID, string CONSIGNID)
        {
            string sql = "delete from gdna.MISSING_PERSON where id='" + ID + "';";
            sql += "delete from gdna.SAMPLE_EXAMINATION where SAMPLE_ID='" + ID + "';";
            sql += "delete from gdna.CONSIGNMENT where id='" + CONSIGNID + "';";
            DBHelperOracle.ExecuteSql(sql);
        }
        [WebMethod]
        public string GetOneMp(string ID)
        {
            return DBHelperSQL.Select("失踪人员视图", "ID='" + ID + "'", "ID", "*").GetXml();
        }
        [WebMethod]
        public string ImportToOraMp(string ID, string userName)
        {
            DNADICTWS dictWs = new DNADICTWS();
            IDictionary<string, string> EVIDENCE_TYPE = dictWs.GetDnaDict("样本类型");
            IDictionary<string, string> GENDER = dictWs.GetDnaDict("性别");
            IDictionary<string, string> PERSONNEL_TYPE = dictWs.GetDnaDict("人员类型");
            IDictionary<string, string> NATIONALITY = dictWs.GetDnaDict("民族");
            IDictionary<string, string> COUNTRY = dictWs.GetDnaDict("国籍");
            IDictionary<string, string> EDUCATION_LEVEL = dictWs.GetDnaDict("学历");
            IDictionary<string, string> IDENTITY = dictWs.GetDnaDict("身份");

            if (DBHelperOracle.Query("select id from gdna.MISSING_PERSON where id='" + ID + "'").Tables[0].Rows.Count == 0)
            {
                using (OracleConnection dbConnectionOra = new OracleConnection(DBHelperOracle.strConnectionString))
                {
                    dbConnectionOra.Open();
                    using (OracleTransaction transOra = dbConnectionOra.BeginTransaction())
                    {
                        DataSet mpds = DBHelperSQL.Select("失踪人员", "ID='" + ID + "'", string.Empty, "*");
                        DataRow mpdr = mpds.Tables[0].Rows[0];
                        DataSet jdlcds = DBHelperSQL.Select("失踪人员视图", "ID='" + ID + "'", string.Empty, "*");
                        DataRow jdlcdr = jdlcds.Tables[0].Rows[0];
                        string oraFlag = mpdr["ORA_FLAG"].ToString();
                        string userOraId = DBHelperOracle.GetUserOraId(jdlcdr["一检姓名"].ToString());

                        CONSIGNMENTOracleDAL.AddWithTransaction(jdlcdr["委托表号"].ToString(), jdlcdr["委托单位编号"].ToString(), string.Empty,
                                       jdlcdr["委托单位名称"].ToString(), jdlcdr["委托单位电话"].ToString(), jdlcdr["受理时间"].ToString(), jdlcdr["一送姓名"].ToString(),
                                       jdlcdr["一送警号"].ToString(), jdlcdr["委托单位地址"].ToString(), userName, dbConnectionOra, transOra);
                        MISSING_PERSONOracleDAL.AddWithTransaction(mpdr["ID"].ToString(), jdlcdr["委托表号"].ToString(), mpdr["姓名"].ToString(),
                            EVIDENCE_TYPE[mpdr["样本类型"].ToString()], GENDER[mpdr["性别"].ToString()],
                            PERSONNEL_TYPE[mpdr["人员类型"].ToString()], mpdr["出生日期"].ToString(), NATIONALITY[mpdr["民族"].ToString()],
                            COUNTRY[mpdr["国籍"].ToString()], mpdr["身份证"].ToString(), EDUCATION_LEVEL[mpdr["学历"].ToString()],
                            IDENTITY[mpdr["身份"].ToString()], mpdr["籍贯"].ToString(), mpdr["现住址"].ToString(),
                            mpdr["包装情况"].ToString(), mpdr["样本描述"].ToString(), mpdr["备注"].ToString(),
                            mpdr["样本编号"].ToString(), Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "L"), jdlcdr["一检姓名"].ToString(), userOraId, dbConnectionOra, transOra);
                        transOra.Commit();
                    }
                }
            }

            return "1";
        }
    }
}
