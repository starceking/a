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
    public class 无名尸体WS : System.Web.Services.WebService
    {
        [WebMethod]
        public string Insert(string ID, string 案件ID, string 委托编号, string 姓名, string 样本类型,
            string 性别, string 包装情况, string 样本描述, string 尸体特征, string 大致年龄, string 备注)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("案件ID", 案件ID);
            dict.Add("委托编号", 委托编号);
            dict.Add("姓名", 姓名);
            dict.Add("样本类型", 样本类型);
            dict.Add("性别", 性别);
            dict.Add("包装情况", 包装情况);
            dict.Add("样本描述", 样本描述);
            dict.Add("备注", 备注);
            dict.Add("尸体特征", 尸体特征);
            dict.Add("大致年龄", 大致年龄);
            return DBHelperSQL.Insert("无名尸体", dict);
        }
        [WebMethod]
        public string InsertWithNo(string ID, string 案件ID, string 委托编号, string 姓名, string 样本类型,
            string 性别, string 包装情况, string 样本描述, string 尸体特征, string 大致年龄, string 备注,
            string preFix, string tableName, string len, string CONSIGNID, string userName, string CASE_ORA_FLAG)//2012.1.4
        {
            string numStr = preFix + DataReader.GetNextSLN(preFix, tableName).PadLeft(Convert.ToInt32(len), '0');
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("案件ID", 案件ID);
            dict.Add("委托编号", 委托编号);
            dict.Add("姓名", 姓名);
            dict.Add("样本类型", 样本类型);
            dict.Add("性别", 性别);
            dict.Add("包装情况", 包装情况);
            dict.Add("样本描述", 样本描述);
            dict.Add("备注", 备注);
            dict.Add("尸体特征", 尸体特征);
            dict.Add("大致年龄", 大致年龄);
            dict.Add("样本编号", numStr);
            dict.Add("ORA_FLAG", CASE_ORA_FLAG);

            if (CASE_ORA_FLAG.Equals("1"))
            {
                string userOraId = DBHelperOracle.GetUserOraId(userName);
                DNADICTWS dictWs = new DNADICTWS();
                IDictionary<string, string> EVIDENCE_TYPE = dictWs.GetDnaDict("样本类型");
                IDictionary<string, string> GENDER = dictWs.GetDnaDict("性别");
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
                                DBHelperSQL.Insert("无名尸体", dict, dbConnection, trans);
                                UNKNOWN_DECEASEDOracleDAL.AddWithTransaction(ID, 案件ID, CONSIGNID, 姓名,
                                         EVIDENCE_TYPE[样本类型], GENDER[性别],
                                         包装情况, 样本描述, 尸体特征, 大致年龄, 备注,
                                         numStr, Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "U"), userName, userOraId,
                                         dbConnectionOra, transOra);
                                trans.Commit();
                                transOra.Commit();
                            }
                        }
                    }
                }
            }
            else
                DBHelperSQL.Insert("无名尸体", dict);
            return numStr;
        }
        [WebMethod]
        public string Update(string ID, string 姓名, string 样本类型,
            string 性别, string 包装情况, string 样本描述, string 尸体特征, string 大致年龄, string 备注, string 样本编号, string ORA_FLAG)//2012.1.4
        {
            if (!DataReader.ValidSlnDup(样本编号, ID))
            {
                return "样本编号重复！请检查。";
            }

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("姓名", 姓名);
            dict.Add("样本类型", 样本类型);
            dict.Add("性别", 性别);
            dict.Add("包装情况", 包装情况);
            dict.Add("样本描述", 样本描述);
            dict.Add("备注", 备注);
            dict.Add("尸体特征", 尸体特征);
            dict.Add("大致年龄", 大致年龄);
            dict.Add("样本编号", 样本编号);

            if (ORA_FLAG.Equals("1"))
            {
                DNADICTWS dictWs = new DNADICTWS();
                IDictionary<string, string> EVIDENCE_TYPE = dictWs.GetDnaDict("样本类型");
                IDictionary<string, string> GENDER = dictWs.GetDnaDict("性别");
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
                                DBHelperSQL.Update("无名尸体", "ID='" + ID + "'", dict, dbConnection, trans);
                                UNKNOWN_DECEASEDOracleDAL.UpdateWithTransaction(ID, 姓名,
                                            EVIDENCE_TYPE[样本类型], GENDER[性别],
                                            包装情况, 样本描述, 尸体特征, 大致年龄, 备注,
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
                return DBHelperSQL.Update("无名尸体", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string Delete(string ID, string ORA_FLAG)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("无名尸体", "ID，" + ID);
            if (ORA_FLAG.Equals("0"))
            {
                return DBHelperSQL.Delete(dict);
            }
            else
            {
                string sql = "delete from gdna.UNKNOWN_DECEASED where id='" + ID + "';";
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
        }
        [WebMethod]
        public string GetAll(string 案件ID, string 委托编号)
        {
            string filter = string.Empty;
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";

            return DBHelperSQL.Select("无名尸体", Helper.CutFilter(filter), "创建时间", "*").GetXml();
        }
    }
}
