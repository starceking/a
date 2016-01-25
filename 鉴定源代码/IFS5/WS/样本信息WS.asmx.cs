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
    public class 样本信息WS : System.Web.Services.WebService
    {
        [WebMethod]
        public string Insert(string ID, string 案件ID, string 委托编号, string 名称, string 样本类型, string 数量, string 承载物, string 样本包装)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("案件ID", 案件ID);
            dict.Add("委托编号", 委托编号);
            dict.Add("名称", 名称);
            dict.Add("样本类型", 样本类型);
            dict.Add("数量", 数量);
            dict.Add("承载物", 承载物);
            dict.Add("样本包装", 样本包装);
            return DBHelperSQL.Insert("样本信息", dict);
        }
        [WebMethod]
        public string InsertWithNo(string ID, string 案件ID, string 委托编号, string 名称, string 样本类型, string 数量, string 承载物, string 样本包装,
            string preFix, string tableName, string len, string userName, string CASE_ORA_FLAG)//2012.1.4
        {
            string numStr = preFix + DataReader.GetNextSLN(preFix, tableName).PadLeft(Convert.ToInt32(len), '0');
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("案件ID", 案件ID);
            dict.Add("委托编号", 委托编号);
            dict.Add("名称", 名称);
            dict.Add("样本类型", 样本类型);
            dict.Add("数量", 数量);
            dict.Add("承载物", 承载物);
            dict.Add("样本包装", 样本包装);
            dict.Add("样本编号", numStr);
            dict.Add("ORA_FLAG", CASE_ORA_FLAG);
            //大连DNA修改【物证样本名称前加案件编号】
            DataSet ajds = DBHelperSQL.Query("select * from 案件信息 where 委托编号='" + 委托编号 + "';");
            string ajbh = ajds.Tables[0].Rows[0]["案件编号"].ToString();

            if (CASE_ORA_FLAG.Equals("1"))
            {
                string userOraId = DBHelperOracle.GetUserOraId(userName);
                DNADICTWS dictWs = new DNADICTWS();
                IDictionary<string, string> EVIDENCE_TYPE = dictWs.GetDnaDict("样本类型");
                IDictionary<string, string> EVIDENCE_CARRIER_TYPE = dictWs.GetDnaDict("承载物");
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
                                DBHelperSQL.Insert("样本信息", dict, dbConnection, trans);
                                SCENE_EVIDENCEOracleDAL.AddWithTransaction(ID, 案件ID, Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "W"),
                                                (ajbh + 名称), EVIDENCE_TYPE[样本类型], 数量, EVIDENCE_CARRIER_TYPE[承载物],
                                          numStr, 样本包装, userName, userOraId, dbConnectionOra, transOra);
                                trans.Commit();
                                transOra.Commit();
                            }
                        }
                    }
                }
            }
            else
                DBHelperSQL.Insert("样本信息", dict);
            return numStr;
        }
        [WebMethod]
        public string Update(string ID, string 名称, string 样本类型, string 数量, string 承载物, string 样本编号, string 样本包装, string ORA_FLAG)//2012.1.4
        {
            if (!DataReader.ValidSlnDup(样本编号, ID))
            {
                return "样本编号重复！请检查。";
            }

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("名称", 名称);
            dict.Add("样本类型", 样本类型);
            dict.Add("数量", 数量);
            dict.Add("承载物", 承载物);
            dict.Add("样本编号", 样本编号);
            dict.Add("样本包装", 样本包装);
            //大连DNA修改【物证样本名称前加案件编号】
            DataSet ybds = DBHelperSQL.Query("select * from 样本信息 where ID='" + ID + "';");
            DataSet ajds = DBHelperSQL.Query("select * from 案件信息 where 委托编号='" + ybds.Tables[0].Rows[0]["委托编号"].ToString() + "';");
            string ajbh = ajds.Tables[0].Rows[0]["案件编号"].ToString();

            if (ORA_FLAG.Equals("1"))
            {
                DNADICTWS dictWs = new DNADICTWS();
                IDictionary<string, string> EVIDENCE_TYPE = dictWs.GetDnaDict("样本类型");
                IDictionary<string, string> EVIDENCE_CARRIER_TYPE = dictWs.GetDnaDict("承载物");
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
                                DBHelperSQL.Update("样本信息", "ID='" + ID + "'", dict, dbConnection, trans);
                                SCENE_EVIDENCEOracleDAL.UpdateWithTransaction(ID, (ajbh + 名称),
                                              EVIDENCE_TYPE[样本类型], 数量, EVIDENCE_CARRIER_TYPE[承载物],
                                              样本编号, 样本包装, "admin", dbConnectionOra, transOra);
                                trans.Commit();
                                transOra.Commit();
                            }
                        }
                    }
                }
                return "1";
            }
            else
                return DBHelperSQL.Update("样本信息", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string Delete(string ID, string ORA_FLAG)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("样本信息", "ID，" + ID);
            if (ORA_FLAG.Equals("0"))
            {
                return DBHelperSQL.Delete(dict);
            }
            else
            {
                string sql = "delete from gdna.SCENE_EVIDENCE where id='" + ID + "';";
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
        public string GetAll(string 案件ID, string 委托编号, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";

            return DBHelperSQL.SelectRowCount("样本信息", Helper.CutFilter(filter), "样本编号,样本类型,创建时间", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
    }
}
