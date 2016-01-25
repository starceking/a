using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using LIB;

namespace DAL
{
    public class DBHelperSQL
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string strConnection = ConfigurationManager.ConnectionStrings["IFSConnectionString"].ToString();
        #region 一般操作
        public static string ExecuteSql(string strSql, SqlParameter[] dd)
        {
            if (strSql.Length > 0)
            {
                using (SqlConnection dbConnection = new SqlConnection(strConnection))
                {
                    dbConnection.Open();
                    using (SqlCommand objCommand = new SqlCommand(strSql, dbConnection))
                    {
                        using (SqlTransaction trans = dbConnection.BeginTransaction())
                        {
                            objCommand.Transaction = trans;
                            objCommand.Parameters.AddRange(dd);
                            objCommand.ExecuteNonQuery();
                            trans.Commit();
                        }
                    }
                }
            }
            return "1";
        }
        public static void ExecuteSql(string strSql, SqlParameter[] dd, SqlConnection dbConnection, SqlTransaction trans)
        {
            if (strSql.Length > 0)
            {
                using (SqlCommand objCommand = new SqlCommand(strSql, dbConnection))
                {
                    objCommand.Transaction = trans;
                    objCommand.Parameters.AddRange(dd);
                    objCommand.ExecuteNonQuery();
                }
            }
        }
        public static string ExecuteSql(string strSql, SqlConnection dbConnection, SqlTransaction trans)
        {
            if (strSql.Length > 0)
            {
                using (SqlCommand objCommand = new SqlCommand(strSql, dbConnection))
                {
                    objCommand.Transaction = trans;
                    objCommand.ExecuteNonQuery();
                }
            }
            return "1";
        }
        public static string ExecuteSql(string strSql)
        {
            if (strSql.Length > 0)
            {
                using (SqlConnection dbConnection = new SqlConnection(strConnection))
                {
                    dbConnection.Open();
                    using (SqlCommand objCommand = new SqlCommand(strSql, dbConnection))
                    {
                        using (SqlTransaction trans = dbConnection.BeginTransaction())
                        {
                            objCommand.Transaction = trans;
                            objCommand.ExecuteNonQuery();
                            trans.Commit();
                        }
                    }
                }
            }
            return "1";
        }
        public static DataSet Query(string strSql)
        {
            DataSet retds = new DataSet();
            using (SqlConnection dbConnection = new SqlConnection(strConnection))
            {
                dbConnection.Open();
                using (SqlDataAdapter objSDA = new SqlDataAdapter(strSql, dbConnection))
                {
                    objSDA.Fill(retds, "tableName");
                }
            }
            return retds;
        }
        public static DataSet Query(string strSql, SqlConnection dbConnection, SqlTransaction trans)
        {
            DataSet retds = new DataSet();
            using (SqlDataAdapter objSDA = new SqlDataAdapter())
            {
                objSDA.SelectCommand = new SqlCommand(strSql, dbConnection, trans);
                objSDA.Fill(retds, "tableName");
            }
            return retds;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static DataSet Query(string strSql, SqlParameter[] ss)
        {
            DataSet retds = new DataSet();
            using (SqlConnection dbConnection = new SqlConnection(strConnection))
            {
                dbConnection.Open();
                using (SqlDataAdapter objSDA = new SqlDataAdapter())
                {
                    objSDA.SelectCommand = new SqlCommand(strSql, dbConnection);
                    objSDA.SelectCommand.Parameters.AddRange(ss);
                    objSDA.Fill(retds, "tableName");
                }
            }
            return retds;
        }
        public static int QueryRowCount(string strSql)
        {
            int nums = 0;
            using (SqlConnection dbConnection = new SqlConnection(strConnection))
            {
                dbConnection.Open();
                using (SqlCommand objCommand = new SqlCommand(strSql, dbConnection))
                {
                    nums = Convert.ToInt32(objCommand.ExecuteScalar());
                }
            }
            return nums;
        }
        #endregion
        #region 模版
        public static string Insert(string table, IDictionary<string, string> dict)
        {
            if (dict.Count > 0)
            {
                int counter = 1;
                IList<SqlParameter> paraList = new List<SqlParameter>();
                string cols = string.Empty;
                string vals = string.Empty;

                foreach (string key in dict.Keys)
                {
                    cols += key;
                    cols += (counter == dict.Count ? ")" : ",");

                    vals += "@" + key;
                    vals += (counter == dict.Count ? ")" : ",");

                    paraList.Add(new SqlParameter(key, Helper.GetDBValue(dict[key])));

                    counter++;
                }

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into " + table + "(");
                strSql.Append(cols);
                strSql.Append(" values (");
                strSql.Append(vals);

                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    parameters[i] = paraList[i];
                }
                return ExecuteSql(strSql.ToString(), parameters);
            }
            return "1";
        }
        public static string Update(string table, string filter, IDictionary<string, string> dict)
        {
            if (dict.Count > 0)
            {
                int counter = 1;
                IList<SqlParameter> paraList = new List<SqlParameter>();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update " + table + " set ");

                foreach (string key in dict.Keys)
                {
                    strSql.Append(key + "=@" + key);
                    strSql.Append(counter == dict.Count ? string.Empty : ",");

                    paraList.Add(new SqlParameter(key, Helper.GetDBValue(dict[key])));

                    counter++;
                }

                strSql.Append(" where " + filter);

                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    parameters[i] = paraList[i];
                }
                return ExecuteSql(strSql.ToString(), parameters);
            }
            return "1";
        }
        public static void Insert(string table, IDictionary<string, string> dict, SqlConnection dbConnection, SqlTransaction trans)
        {
            if (dict.Count > 0)
            {
                int counter = 1;
                IList<SqlParameter> paraList = new List<SqlParameter>();
                string cols = string.Empty;
                string vals = string.Empty;

                foreach (string key in dict.Keys)
                {
                    cols += key;
                    cols += (counter == dict.Count ? ")" : ",");

                    vals += "@" + key;
                    vals += (counter == dict.Count ? ")" : ",");

                    paraList.Add(new SqlParameter(key, Helper.GetDBValue(dict[key])));

                    counter++;
                }

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into " + table + "(");
                strSql.Append(cols);
                strSql.Append(" values (");
                strSql.Append(vals);

                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    parameters[i] = paraList[i];
                }
                ExecuteSql(strSql.ToString(), parameters, dbConnection, trans);
            }
        }
        public static void Update(string table, string filter, IDictionary<string, string> dict, SqlConnection dbConnection, SqlTransaction trans)
        {
            if (dict.Count > 0)
            {
                int counter = 1;
                IList<SqlParameter> paraList = new List<SqlParameter>();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update " + table + " set ");

                foreach (string key in dict.Keys)
                {
                    strSql.Append(key + "=@" + key);
                    strSql.Append(counter == dict.Count ? string.Empty : ",");

                    paraList.Add(new SqlParameter(key, Helper.GetDBValue(dict[key])));

                    counter++;
                }

                strSql.Append(" where " + filter);

                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    parameters[i] = paraList[i];
                }
                ExecuteSql(strSql.ToString(), parameters, dbConnection, trans);
            }
        }
        public static string Delete(IDictionary<string, string> dict)
        {
            if (dict.Count > 0)
            {
                string sql = string.Empty;
                foreach (string table in dict.Keys)
                {
                    string[] colVal = dict[table].Split('，');
                    if (colVal[1].Contains("￥"))
                    {
                        string[] colVal2 = colVal[1].Split('￥');
                        foreach (string cv2 in colVal2)
                        {
                            sql += "delete from " + table + " where " + colVal[0] + "='" + cv2 + "';";
                        }
                    }
                    else
                    {
                        sql += "delete from " + table + " where " + colVal[0] + "='" + colVal[1] + "';";
                    }
                }
                return ExecuteSql(sql);
            }
            return "1";
        }
        public static string Delete(IDictionary<string, string> dict, SqlConnection dbConnection, SqlTransaction trans)
        {
            if (dict.Count > 0)
            {
                string sql = string.Empty;
                foreach (string table in dict.Keys)
                {
                    string[] colVal = dict[table].Split('，');
                    if (colVal[1].Contains("￥"))
                    {
                        string[] colVal2 = colVal[1].Split('￥');
                        foreach (string cv2 in colVal2)
                        {
                            sql += "delete from " + table + " where " + colVal[0] + "='" + cv2 + "';";
                        }
                    }
                    else
                    {
                        sql += "delete from " + table + " where " + colVal[0] + "='" + colVal[1] + "';";
                    }
                }
                return ExecuteSql(sql, dbConnection, trans);
            }
            return "1";
        }
        public static DataSet Select(string getter)
        {
            string sql = "select " + getter ;
            return Query(sql);
        }
        public static DataSet Select(string table, string filter, string order, string getter)
        {
            string sql = "select " + getter + " from " + table;
            if (filter.Trim().Length > 0)
            {
                sql += " where (" + filter + ")";
            }
            if (order.Trim().Length > 0)
            {
                sql += " order by " + order;
            }
            return Query(sql);
        }
        public static DataSet Select(string table, string filter, string group, string order, string getter)
        {
            string sql = "select " + getter + " from " + table;
            if (filter.Trim().Length > 0)
            {
                sql += " where (" + filter + ")";
            }
            if (group.Trim().Length > 0)
            {
                sql += " group by " + group;
            }
            if (order.Trim().Length > 0)
            {
                sql += " order by " + order;
            }
            return Query(sql);
        }
        public static DataSet Select(string table, string filter, string order, string getter, int pageSize, int pageIndex)
        {
            int first = pageSize * (pageIndex - 1) + 1;
            int second = pageSize * pageIndex;

            string sql = "select top " + pageSize + " " + getter + " from (select row_number() over(order by " + order +
                ") as ROWID," + getter + " from " + table + ") as tb where ROWID between " + first + " and " + second;
            if (filter.Trim().Length > 0)
            {
                sql = "select top " + pageSize + " " + getter + " from (select row_number() over(order by " + order +
                    ") as ROWID," + getter + " from " + table + " where (" + filter + ")) as tb where ROWID between " + first + " and " + second;
            }
            return Query(sql);
        }
        public static DataSet SelectRowCount(string table, string filter, string order, string getter, int pageSize, int pageIndex)
        {
            int first = pageSize * (pageIndex - 1) + 1;
            int second = pageSize * pageIndex;

            string sql = "select top " + pageSize + " " + getter + " from (select row_number() over(order by " + order +
                ") as ROWID," + getter + " from " + table + ") as tb where ROWID between " + first + " and " + second;
            if (filter.Trim().Length > 0)
            {
                sql = "select top " + pageSize + " " + getter + " from (select row_number() over(order by " + order +
                    ") as ROWID," + getter + " from " + table + " where (" + filter + ")) as tb where ROWID between " + first + " and " + second;
            }
            DataSet ds = Query(sql);

            int count = SelectRowCount(table, filter, getter);
            DataColumn dc = new DataColumn("RowCount", typeof(string));
            dc.DefaultValue = count;
            ds.Tables[0].Columns.Add(dc);
            return ds;
        }
        public static int SelectRowCount(string table, string filter, string getter)
        {
            string sql = "select count(" + getter + ") from " + table;
            if (filter.Trim().Length > 0)
            {
                sql += " where (" + filter + ")";
            }
            return QueryRowCount(sql);
        }
        #endregion
    }
}
