using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;

namespace DNADAL
{
    public class DBHelperOracle
    {
        public static string strConnectionString = ConfigurationManager.ConnectionStrings["OraConnectionString"].ToString();

        #region 执行sql语句
        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dd"></param>
        /// <returns></returns>
        public static void ExecuteSql(string strSql, OracleParameter[] dd)
        {
            if (strSql.Length == 0)
            {
                return;
            }
            if (strSql.Contains(";")) strSql = "begin " + strSql + " end;";
            using (OracleConnection dbConnection = new OracleConnection(strConnectionString))
            {
                dbConnection.Open();
                using (OracleCommand objCommand = new OracleCommand(strSql, dbConnection))
                {
                    using (OracleTransaction trans = dbConnection.BeginTransaction())
                    {
                        objCommand.Transaction = trans;
                        objCommand.Parameters.AddRange(dd);
                        objCommand.ExecuteNonQuery();
                        trans.Commit();
                    }
                }
            }
        }

        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static void ExecuteSql(string strSql)
        {
            if (strSql.Length == 0)
            {
                return;
            }
            if (strSql.Contains(";")) strSql = "begin " + strSql + " end;";
            using (OracleConnection dbConnection = new OracleConnection(strConnectionString))
            {
                dbConnection.Open();
                using (OracleCommand objCommand = new OracleCommand(strSql, dbConnection))
                {
                    using (OracleTransaction trans = dbConnection.BeginTransaction())
                    {
                        objCommand.Transaction = trans;
                        objCommand.ExecuteNonQuery();
                        trans.Commit();
                    }
                }
            }
        }
        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dd"></param>
        /// <param name="dbConnection"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static void ExecuteSqlWithTransaction(string strSql, OracleParameter[] dd, OracleConnection dbConnection, OracleTransaction trans)
        {
            if (strSql.Length == 0)
            {
                return;
            }
            if (strSql.Contains(";")) strSql = "begin " + strSql + " end;";
            using (OracleCommand objCommand = new OracleCommand(strSql, dbConnection))
            {
                objCommand.Parameters.AddRange(dd);
                objCommand.Transaction = trans;
                objCommand.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbConnection"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static void ExecuteSqlWithTransaction(string strSql, OracleConnection dbConnection, OracleTransaction trans)
        {
            if (strSql.Length == 0)
            {
                return;
            }
            if (strSql.Contains(";")) strSql = "begin " + strSql + " end;";
            using (OracleCommand objCommand = new OracleCommand(strSql, dbConnection))
            {
                objCommand.Transaction = trans;
                objCommand.ExecuteNonQuery();
            }
        }
        #endregion
        #region  查询  返回dataset
        public static bool TryCon(string conStr)
        {
            OracleConnection dbConnection = new OracleConnection(conStr);
            try
            {
                dbConnection.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataSet Query(string strSql)
        {
            DataSet retds = new DataSet();
            using (OracleConnection dbConnection = new OracleConnection(strConnectionString))
            {
                dbConnection.Open();
                using (OracleDataAdapter objSDA = new OracleDataAdapter(strSql, dbConnection))
                {
                    objSDA.Fill(retds, "tableName");
                }
            }
            return retds;
        }
        public static DataSet Query(string con, string strSql)
        {
            DataSet retds = new DataSet();
            using (OracleConnection dbConnection = new OracleConnection(con))
            {
                dbConnection.Open();
                using (OracleDataAdapter objSDA = new OracleDataAdapter(strSql, dbConnection))
                {
                    objSDA.Fill(retds, "tableName");
                }
            }
            return retds;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static DataSet Query(string strSql, OracleParameter[] ss)
        {
            DataSet retds = new DataSet();
            using (OracleConnection dbConnection = new OracleConnection(strConnectionString))
            {
                dbConnection.Open();
                using (OracleDataAdapter objSDA = new OracleDataAdapter(strSql, dbConnection))
                {
                    objSDA.SelectCommand.Parameters.AddRange(ss);
                    objSDA.Fill(retds, "tableName");
                }
            }
            return retds;
        }
        #endregion
        #region 查询 返回记录数
        /// <summary>
        /// 查询返回的记录数
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int QueryCount(string strSql)
        {
            int nums = 0;
            using (OracleConnection dbConnection = new OracleConnection(strConnectionString))
            {
                dbConnection.Open();
                using (OracleCommand objCommand = new OracleCommand(strSql, dbConnection))
                {
                    nums = Convert.ToInt32(objCommand.ExecuteScalar());
                }
            }
            return nums;
        }
        #endregion
        #region 警综平台存储过程接口
        public static DataSet Query1(string con, string ajbh)
        {
            OracleParameter p1 = new OracleParameter("ajbh2", OracleType.VarChar);
            OracleParameter p2 = new OracleParameter("set1", OracleType.Cursor);
            p1.Direction = ParameterDirection.Input;
            p2.Direction = ParameterDirection.Output;
            p1.Value = ajbh;
            OracleParameter[] ss = new OracleParameter[]
            {
                p1,p2
            };

            DataSet retds = new DataSet();
            using (OracleConnection dbConnection = new OracleConnection(con))
            {
                dbConnection.Open();
                using (OracleDataAdapter objSDA = new OracleDataAdapter("jwzh_rd10.p_resultbyajbh", dbConnection))
                {
                    objSDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    objSDA.SelectCommand.Parameters.AddRange(ss);
                    objSDA.Fill(retds, "tableName");
                }
            }
            return retds;
        }
        public static DataSet Query2(string con, string sj1, string sj2)
        {
            OracleParameter p1 = new OracleParameter("sj1", OracleType.DateTime);
            OracleParameter p2 = new OracleParameter("sj2", OracleType.DateTime);
            OracleParameter p3 = new OracleParameter("set2", OracleType.Cursor);
            p1.Direction = ParameterDirection.Input;
            p2.Direction = ParameterDirection.Input;
            p3.Direction = ParameterDirection.Output;
            p1.Value = sj1;
            p2.Value = sj2;
            OracleParameter[] ss = new OracleParameter[]
            {
                p1,p2,p3
            };

            DataSet retds = new DataSet();
            using (OracleConnection dbConnection = new OracleConnection(con))
            {
                dbConnection.Open();
                using (OracleDataAdapter objSDA = new OracleDataAdapter("jwzh_rd10.p_resultbytime", dbConnection))
                {
                    objSDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    objSDA.SelectCommand.Parameters.AddRange(ss);
                    objSDA.Fill(retds, "tableName");
                }
            }
            return retds;
        }
        #endregion
        #region 辅助
        public static string LAB_ID = ConfigurationManager.AppSettings["LAB_ID"];
        public static string INIT_SERVER_NO = ConfigurationManager.AppSettings["INIT_SERVER_NO"];
        public static string RECEPTION_REGIONALISM = ConfigurationManager.AppSettings["RECEPTION_REGIONALISM"];
        public static string RECEPTION_ORG_NAME = ConfigurationManager.AppSettings["RECEPTION_ORG_NAME"];
        public static string RECEPTION_TEL = ConfigurationManager.AppSettings["RECEPTION_TEL"];

        public static string GetUserOraId(string name)
        {
            DataSet ds = Query("select id from gdna.SYS_USER where true_name='" + name + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                return "F7B035ECED5759CA82570E9FEFB05C75";
            }
            return ds.Tables[0].Rows[0][0].ToString();
        }
        #endregion
    }
}
