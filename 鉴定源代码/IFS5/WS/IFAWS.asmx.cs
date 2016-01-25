using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Data.OracleClient;
using DAL;
using System.Data.SqlClient;

namespace WS
{
    /// <summary>
    /// Summary description for IFAWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class IFAWS : System.Web.Services.WebService
    {
        #region 获取Str
        [WebMethod]
        public string ReadStr(string 案件ID, string 委托编号, string ip)
        {
            if (ip.Trim().Length == 0)
                ip = this.Context.Request.UserHostAddress;
            //删除老的数据
            DBHelperSQL.ExecuteSql("delete from 样本基因 where 委托编号='" + 委托编号 + "'");
            //初始化基础数据
            IDictionary<string, string> idDict = new Dictionary<string, string>();
            IDictionary<string, string> nameDict = new Dictionary<string, string>();
            IDictionary<string, string> scDict = new Dictionary<string, string>();
            string slns = string.Empty;

            DataSet sampleDs = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", string.Empty, "ID,名称,样本编号,库类型");
            foreach (DataRow dr in sampleDs.Tables[0].Rows)
            {
                idDict.Add(dr["样本编号"].ToString(), dr["ID"].ToString());
                nameDict.Add(dr["样本编号"].ToString(), dr["名称"].ToString());
                scDict.Add(dr["样本编号"].ToString(), dr["库类型"].ToString());
                slns += "d.sample_name = '" + dr["样本编号"].ToString() + "' or ";
            }
            if (slns.Length == 0) return "本案件没有样本";
            else slns = slns.Substring(0, slns.Length - 4);
            //获取str数据
            DataSet strDs = GetStrs(slns, ip);
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    foreach (DataRow dr in strDs.Tables[0].Rows)
                    {
                        IDictionary<string, string> dict = new Dictionary<string, string>();
                        dict.Add("案件ID", 案件ID);
                        dict.Add("委托编号", 委托编号);
                        dict.Add("SAMPLE_ID", idDict[dr["sample_lab_no"].ToString()]);
                        dict.Add("样本编号", dr["sample_lab_no"].ToString());
                        dict.Add("样本名称", nameDict[dr["sample_lab_no"].ToString()]);
                        dict.Add("库类型", scDict[dr["sample_lab_no"].ToString()]);

                        foreach (DataColumn dc in strDs.Tables[0].Columns)
                        {
                            if (dc.ColumnName.Equals("PENTA D"))
                            {
                                dict.Add("PENTA_D", dr[dc.ColumnName].ToString());
                            }
                            else if (dc.ColumnName.Equals("PENTA E"))
                            {
                                dict.Add("PENTA_E", dr[dc.ColumnName].ToString());
                            }
                            else if (!dc.ColumnName.Equals("sample_lab_no"))
                            {
                                dict.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                            }
                        }

                        DBHelperSQL.Insert("样本基因", dict, dbConnection, trans);
                    }

                    trans.Commit();
                }
            }
            return "1";
        }
        public DataSet GetStrs(string slns, string ip)
        {
            DataSet dataSet = new DataSet();
            DataTable dt = GenTable(); dataSet.Tables.Add(dt);

            DataSet ds = GetDataSet(@"select c.Sample_ID,d.sample_name,a.Allele_Name || '-' || b.Marker_Name as Allele from 
IFA.Allele a,IFA.Allele_Call b,IFA.Analysis c,IFA.Sample d where 
a.Allele_Call_ID = b.Allele_Call_ID and b.Analysis_ID=c.Analysis_ID and d.Sample_ID=c.sample_id and
(" + slns + ") and a.State=1 order by c.Sample_ID asc", "dd", ip);
            IList<string> silist = new List<string>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (silist.Contains(dr["Sample_ID"].ToString()))
                {
                    string[] allele = dr["allele"].ToString().Split('-');
                    if (dt.Columns.Contains(allele[1]))
                    {
                        if (dt.Rows[dt.Rows.Count - 1][allele[1]].ToString().Length == 0)
                        {
                            dt.Rows[dt.Rows.Count - 1][allele[1]] = allele[0];
                        }
                        else
                        {
                            dt.Rows[dt.Rows.Count - 1][allele[1]] = InsertStrVal(dt.Rows[dt.Rows.Count - 1][allele[1]].ToString(), allele[0]);
                        }
                    }
                }
                else
                {
                    silist.Add(dr["Sample_ID"].ToString());
                    DataRow drow = dt.NewRow();
                    drow["sample_lab_no"] = dr["sample_name"];
                    string[] allele = dr["allele"].ToString().Split('-');
                    if (dt.Columns.Contains(allele[1]))
                    {
                        drow[allele[1]] = allele[0];
                    }
                    dt.Rows.Add(drow);
                }
            }
            return dataSet;
        }
        DataTable GenTable()
        {
            DataTable dt = new DataTable("tableName");
            DataColumn dc = new DataColumn("sample_lab_no", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("AMEL", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D8S1179", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D21S11", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D18S51", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("VWA", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D3S1358", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("FGA", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TH01", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D5S818", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D13S317", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D7S820", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("CSF1PO", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D16S539", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("TPOX", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D2S1338", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D19S433", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("PENTA D", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("PENTA E", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D6S1043", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("F13A01", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("FESFPS", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D1S80", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("B_DYS456", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("B_DYS389I", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("B_DYS390", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("B_DYS389II", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("G_DYS458", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("G_DYS19", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("G_DYS385", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Y_DYS393", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Y_DYS391", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Y_DYS439", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Y_DYS635", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Y_DYS392", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("R_Y_GATA_H4", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("R_DYS437", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("R_DYS438", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("R_DYS448", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("D12S391", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D1S1656", typeof(string)); dt.Columns.Add(dc);

            dc = new DataColumn("D2S441", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D22S1045", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("SE33", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("D10S1248", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Y_indel", typeof(string)); dt.Columns.Add(dc);
            return dt;
        }
        string InsertStrVal(string src, string tar)
        {
            if (tar.Equals("OL"))
            {
                return src + "/OL";
            }
            else if (tar.Equals("X"))
            {
                return "X/" + src;
            }
            else if (tar.Equals("Y"))
            {
                return src + "/Y";
            }
            string[] srcs = src.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            IList<string> ret = new List<string>();
            foreach (string s in srcs)
            {
                ret.Add(s);
            }
            ret.Add(tar);
            for (int i = 0; i < ret.Count - 1; i++)
            {
                if (ret[i].Equals("OL"))
                {
                    for (int max = ret.Count - 1; max > 0; max--)
                    {
                        if (!ret[max].Equals("OL"))
                        {
                            ret[i] = ret[max];
                            ret[max] = "OL";
                            i--;
                            break;
                        }
                    }
                    continue;
                }
                float number = 0;
                if (!float.TryParse(ret[i], out number))
                {
                    continue;
                }
                for (int j = i + 1; j < ret.Count; j++)
                {
                    float number2 = 0;
                    if (float.TryParse(ret[j], out number2))
                    {
                        if (number2 < number)
                        {
                            ret[j] = number.ToString();
                            ret[i] = number2.ToString();
                            i--;
                            break;
                        }
                    }
                }
            }
            string realsrc = string.Empty;
            for (int i = 0; i < ret.Count; i++)
            {
                realsrc += ret[i] + "/";
            }
            return realsrc.Substring(0, realsrc.Length - 1);
        }
        #endregion
        #region "基本"
        OracleConnection Open(string ip)
        {
            OracleConnection cnn;
            string strConnection = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + ip +
                   ")(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=IFA)));uid=system;pwd=manager;";
            cnn = new OracleConnection(strConnection);
            cnn.Open();
            return cnn;
        }
        void Close(OracleConnection cnn)
        {
            cnn.Close();
        }
        DataSet GetDataSet(string CmdString, string TableName, string ip)
        {
            try
            {
                OracleConnection cnn = Open(ip);
                OracleDataAdapter myDa = new OracleDataAdapter();
                myDa.SelectCommand = new OracleCommand(CmdString, cnn);
                DataSet myDs = new DataSet();
                myDa.Fill(myDs, TableName);
                Close(cnn);
                return myDs;
            }
            catch
            {
                return new DataSet();
            }
        }
        #endregion
    }
}
