using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using LIB;
using System.IO;
using DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.OracleClient;
using DNADAL;
using System.Text.RegularExpressions;

namespace WS
{
    /// <summary>
    /// Summary description for CODIESWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class CODIESWS : System.Web.Services.WebService
    {
        #region 其他
        [WebMethod]
        public string GetAll(string 鉴定单位, string today)
        {
            string dir = ConfigurationManager.AppSettings["WebPath"] + 鉴定单位 + "\\Codies\\";
            Helper.CheckDir(dir);

            DataSet ds = new DataSet("NewDataSet");
            DataTable dt = new DataTable("tableName"); ds.Tables.Add(dt);
            DataColumn dc = new DataColumn("FileName", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Url", typeof(string)); dt.Columns.Add(dc);

            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            FileInfo[] files = dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                if (today.Equals("1"))
                {
                    if (!file.Name.StartsWith(DateTime.Now.ToShortDateString()))
                        continue;
                }

                DataRow dr = dt.NewRow();
                dr["FileName"] = file.Name;
                dr["Url"] = ConfigurationManager.AppSettings["ServerAddr"] + 鉴定单位 + "/Codies/" + file.Name;
                dt.Rows.Add(dr);
            }

            return ds.GetXml();
        }
        [WebMethod]
        public string GetAllTmpStr(string 案件ID, string 委托编号, string 样本编号, string 样本名称, string 库类型, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (样本编号.Length > 0) filter += "样本编号='" + 样本编号 + "' and ";
            if (样本名称.Length > 0) filter += "样本名称 like '%" + 样本名称 + "%' and ";
            if (库类型.Length > 0) filter += "库类型='" + 库类型 + "' and ";

            return DBHelperSQL.SelectRowCount("样本基因", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string deleteTmpStr(string ID)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("样本基因", "ID，" + ID);
            return DBHelperSQL.Delete(dict);
        }
        [WebMethod]
        public string deleteAllTmpStr(string ID)
        {
            string[] ids = ID.Split(new char[] { '，' }, StringSplitOptions.RemoveEmptyEntries);
            string filter = string.Empty;
            foreach (string id in ids)
            {
                filter += "ID='" + id + "' or ";
            }
            string sql = "delete from 样本基因 where " + filter.Substring(0, filter.Length - 4);
            return DBHelperSQL.ExecuteSql(sql);
        }
        [WebMethod]
        public string GetAllStr(string 案件ID, string 委托编号, string 样本编号, string 样本名称, string 库类型, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (样本编号.Length > 0) filter += "样本编号='" + 样本编号 + "' and ";
            if (样本名称.Length > 0) filter += "名称 like '%" + 样本名称 + "%' and ";
            if (库类型.Length > 0) filter += "库类型='" + 库类型 + "' and ";

            return DBHelperSQL.SelectRowCount("样本视图", Helper.CutFilter(filter), "比中序号 ,样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string UpdateStrFromTmp(string ID, string 库类型)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            string tableName = Helper.GetTableByScType(库类型);

            IList<string> exList = new List<string>();
            exList.Add("ID"); exList.Add("案件ID"); exList.Add("委托编号");
            exList.Add("SAMPLE_ID"); exList.Add("样本编号"); exList.Add("样本名称"); exList.Add("库类型"); exList.Add("创建时间");

            DataSet ds = DBHelperSQL.Select("样本基因", "id='" + ID + "'", string.Empty, "*");
            DataSet ds2 = DBHelperSQL.Select(tableName, "id='" + ds.Tables[0].Rows[0]["SAMPLE_ID"].ToString() + "'", string.Empty, "*");

            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (exList.Contains(dc.ColumnName)) continue;
                if (ds2.Tables[0].Rows[0][dc.ColumnName].ToString().Length > 0) continue;
                dict.Add(dc.ColumnName, ds.Tables[0].Rows[0][dc.ColumnName].ToString());
            }
            if (ds.Tables[0].Rows[0]["B_DYS456"].ToString().Length == 0) dict.Add("STR_FLAG", "1");
            else dict.Add("YSTR_FLAG", "1");
            return DBHelperSQL.Update(tableName, "id='" + ds.Tables[0].Rows[0]["SAMPLE_ID"].ToString() + "'", dict);
        }
        [WebMethod]
        public string UpdateAllStrFromTmp(string ID, string 库类型)
        {
            string[] ids = ID.Split(new char[] { '，' }, StringSplitOptions.RemoveEmptyEntries);
            string[] 库类型s = 库类型.Split(new char[] { '，' }, StringSplitOptions.RemoveEmptyEntries);
            string strSql = string.Empty;

            for (int i = 0; i < ids.Length; i++)
            {
                IDictionary<string, string> dict = new Dictionary<string, string>();
                string tableName = Helper.GetTableByScType(库类型s[i]);

                IList<string> exList = new List<string>();
                exList.Add("ID"); exList.Add("案件ID"); exList.Add("委托编号");
                exList.Add("SAMPLE_ID"); exList.Add("样本编号"); exList.Add("样本名称"); exList.Add("库类型"); exList.Add("创建时间");

                DataSet ds = DBHelperSQL.Select("样本基因", "id='" + ids[i] + "'", string.Empty, "*");
                DataSet ds2 = DBHelperSQL.Select(tableName, "id='" + ds.Tables[0].Rows[0]["SAMPLE_ID"].ToString() + "'", string.Empty, "*");

                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    if (exList.Contains(dc.ColumnName)) continue;
                    if (ds2.Tables[0].Rows[0][dc.ColumnName].ToString().Length > 0) continue;
                    dict.Add(dc.ColumnName, ds.Tables[0].Rows[0][dc.ColumnName].ToString());
                }
                if (ds.Tables[0].Rows[0]["B_DYS456"].ToString().Length == 0) dict.Add("STR_FLAG", "1");
                else dict.Add("YSTR_FLAG", "1");

                string filter = "id='" + ds.Tables[0].Rows[0]["SAMPLE_ID"].ToString() + "'";

                if (dict.Count > 0)
                {
                    strSql += "update " + tableName + " set ";

                    foreach (string key in dict.Keys)
                    {
                        strSql += key + "='" + Helper.GetDBValue(dict[key]) + "',";
                    }
                    strSql = strSql.Substring(0, strSql.Length - 1);
                    strSql += " where " + filter + ";";
                }
            }
            return DBHelperSQL.ExecuteSql(strSql);

        }
        [WebMethod]
        public string UpdateStr(string ID, string 库类型, string AMEL, string D8S1179, string D21S11, string D18S51, string vWA,
            string D3S1358, string FGA, string TH01, string D5S818, string D13S317, string D7S820, string CSF1PO, string D16S539, string TPOX, string D2S1338,
            string D19S433, string Penta_D, string Penta_E, string D6S1043, string F13A01, string FESFPS, string D1S80, string D12S391, string D1S1656,
            string D2S441, string D22S1045, string SE33, string D10S1248, string Y_indel, string B_DYS456, string B_DYS389I,
            string B_DYS390, string B_DYS389II, string G_DYS458, string G_DYS19, string G_DYS385, string Y_DYS393, string Y_DYS391, string Y_DYS439, string Y_DYS635, string Y_DYS392,
            string R_Y_GATA_H4, string R_DYS437, string R_DYS438, string R_DYS448, string IMP_FLAG)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("AMEL", AMEL);
            dict.Add("D8S1179", D8S1179);
            dict.Add("D21S11", D21S11);
            dict.Add("D18S51", D18S51);
            dict.Add("vWA", vWA);
            dict.Add("D3S1358", D3S1358);
            dict.Add("FGA", FGA);
            dict.Add("TH01", TH01);
            dict.Add("D5S818", D5S818);
            dict.Add("D13S317", D13S317);
            dict.Add("D7S820", D7S820);
            dict.Add("CSF1PO", CSF1PO);
            dict.Add("D16S539", D16S539);
            dict.Add("TPOX", TPOX);
            dict.Add("D2S1338", D2S1338);
            dict.Add("D19S433", D19S433);
            dict.Add("Penta_D", Penta_D);
            dict.Add("Penta_E", Penta_E);
            dict.Add("D6S1043", D6S1043);
            dict.Add("F13A01", F13A01);
            dict.Add("FESFPS", FESFPS);
            dict.Add("D1S80", D1S80);
            dict.Add("D12S391", D12S391);
            dict.Add("D1S1656", D1S1656);

            dict.Add("D2S441", D2S441);
            dict.Add("D22S1045", D22S1045);
            dict.Add("SE33", SE33);
            dict.Add("D10S1248", D10S1248);
            dict.Add("Y_indel", Y_indel);

            dict.Add("B_DYS456", B_DYS456);
            dict.Add("B_DYS389I", B_DYS389I);
            dict.Add("B_DYS390", B_DYS390);
            dict.Add("B_DYS389II", B_DYS389II);
            dict.Add("G_DYS458", G_DYS458);
            dict.Add("G_DYS19", G_DYS19);
            dict.Add("G_DYS385", G_DYS385);
            dict.Add("Y_DYS393", Y_DYS393);
            dict.Add("Y_DYS391", Y_DYS391);
            dict.Add("Y_DYS439", Y_DYS439);
            dict.Add("Y_DYS635", Y_DYS635);
            dict.Add("Y_DYS392", Y_DYS392);
            dict.Add("R_Y_GATA_H4", R_Y_GATA_H4);
            dict.Add("R_DYS437", R_DYS437);
            dict.Add("R_DYS438", R_DYS438);
            dict.Add("R_DYS448", R_DYS448);
            if (IMP_FLAG.Equals("已导入") || IMP_FLAG.Equals("1"))
            {
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
                                DBHelperSQL.Update(Helper.GetTableByScType(库类型), "id='" + ID + "'", dict, dbConnection, trans);

                                string AMELOGENIN_1 = string.Empty; string AMELOGENIN_2 = string.Empty; string D8S1179_1 = string.Empty; string D8S1179_2 = string.Empty;
                                string D21S11_1 = string.Empty; string D21S11_2 = string.Empty; string D18S51_1 = string.Empty; string D18S51_2 = string.Empty;
                                string VWA_1 = string.Empty; string VWA_2 = string.Empty; string D3S1358_1 = string.Empty; string D3S1358_2 = string.Empty;
                                string FGA_1 = string.Empty; string FGA_2 = string.Empty; string D5S818_1 = string.Empty; string D5S818_2 = string.Empty;
                                string D13S317_1 = string.Empty; string D13S317_2 = string.Empty; string D7S820_1 = string.Empty; string D7S820_2 = string.Empty;
                                string CSF1PO_1 = string.Empty; string CSF1PO_2 = string.Empty; string TH01_1 = string.Empty; string TH01_2 = string.Empty;
                                string D16S539_1 = string.Empty; string D16S539_2 = string.Empty; string D2S1338_1 = string.Empty; string D2S1338_2 = string.Empty;
                                string D19S433_1 = string.Empty; string D19S433_2 = string.Empty; string TPOX_1 = string.Empty; string TPOX_2 = string.Empty;
                                string D6S1043_1 = string.Empty; string D6S1043_2 = string.Empty; string PENTA_E_1 = string.Empty; string PENTA_E_2 = string.Empty;
                                string PENTA_D_1 = string.Empty; string PENTA_D_2 = string.Empty; string F13A01_1 = string.Empty; string F13A01_2 = string.Empty;
                                string FESFPS_1 = string.Empty; string FESFPS_2 = string.Empty; string D1S80_1 = string.Empty; string D1S80_2 = string.Empty;
                                string LOCUS5_1 = string.Empty; string LOCUS5_2 = string.Empty; string LOCUS4_1 = string.Empty; string LOCUS4_2 = string.Empty;

                                Anal(AMEL, ref AMELOGENIN_1, ref AMELOGENIN_2);
                                Anal(D8S1179, ref D8S1179_1, ref D8S1179_2);
                                Anal(D21S11, ref D21S11_1, ref D21S11_2);
                                Anal(D18S51, ref D18S51_1, ref D18S51_2);
                                Anal(vWA, ref VWA_1, ref VWA_2);
                                Anal(D3S1358, ref D3S1358_1, ref D3S1358_2);
                                Anal(FGA, ref FGA_1, ref FGA_2);
                                Anal(TH01, ref TH01_1, ref TH01_2);
                                Anal(D5S818, ref D5S818_1, ref D5S818_2);
                                Anal(D13S317, ref D13S317_1, ref D13S317_2);
                                Anal(D7S820, ref D7S820_1, ref D7S820_2);
                                Anal(CSF1PO, ref CSF1PO_1, ref CSF1PO_2);
                                Anal(D16S539, ref D16S539_1, ref D16S539_2);
                                Anal(TPOX, ref TPOX_1, ref TPOX_2);
                                Anal(D2S1338, ref D2S1338_1, ref D2S1338_2);
                                Anal(D19S433, ref D19S433_1, ref D19S433_2);
                                Anal(Penta_D, ref PENTA_D_1, ref PENTA_D_2);
                                Anal(Penta_E, ref PENTA_E_1, ref PENTA_E_2);
                                Anal(D6S1043, ref D6S1043_1, ref D6S1043_2);
                                Anal(F13A01, ref F13A01_1, ref F13A01_2);
                                Anal(FESFPS, ref FESFPS_1, ref FESFPS_2);
                                Anal(D1S80, ref D1S80_1, ref D1S80_2);
                                Anal(D12S391, ref LOCUS5_1, ref LOCUS5_2);
                                Anal(D1S1656, ref LOCUS4_1, ref LOCUS4_2);
                                //DNA 数据库 未更新 D2S441, D22S1045,SE33,D10S1248,Y_indel,
                                //Anal(D2S441, ref LOCUS4_1, ref LOCUS4_2);
                                //Anal(D22S1045, ref LOCUS4_1, ref LOCUS4_2);
                                //Anal(SE33, ref LOCUS4_1, ref LOCUS4_2);
                                //Anal(D10S1248, ref LOCUS4_1, ref LOCUS4_2);
                                //Anal(Y_indel, ref LOCUS4_1, ref LOCUS4_2);

                                string sql = UpdateDNA_STR(ID, AMELOGENIN_1,
                                    AMELOGENIN_2, D8S1179_1, D8S1179_2, D21S11_1, D21S11_2,
                                    D18S51_1, D18S51_2, VWA_1, VWA_2, D3S1358_1, D3S1358_2,
                                    FGA_1, FGA_2, D5S818_1, D5S818_2, D13S317_1, D13S317_2,
                                    D7S820_1, D7S820_2, CSF1PO_1, CSF1PO_2, TH01_1, TH01_2,
                                    D16S539_1, D16S539_2, D2S1338_1, D2S1338_2, D19S433_1, D19S433_2,
                                    TPOX_1, TPOX_2, D6S1043_1, D6S1043_2, PENTA_E_1, PENTA_E_2,
                                    PENTA_D_1, PENTA_D_2, F13A01_1, F13A01_2,
                                    FESFPS_1, FESFPS_2, D1S80_1, D1S80_2, LOCUS5_1, LOCUS5_2, LOCUS4_1, LOCUS4_2);

                                DBHelperOracle.ExecuteSqlWithTransaction(sql, dbConnectionOra, transOra);

                                trans.Commit();
                                transOra.Commit();

                                return "1";
                            }
                        }
                    }
                }
            }
            else
            {
                if ((D8S1179.Trim().Length == 0) && (B_DYS456.Trim().Length == 0))
                {
                    dict.Add("YSTR_FLAG", "0");
                    dict.Add("STR_FLAG", "0");
                }
                else
                {
                    string xycol = B_DYS456.Trim().Length > 0 ? "YSTR_FLAG" : "STR_FLAG";
                    dict.Add(xycol, "1");
                }
                return DBHelperSQL.Update(Helper.GetTableByScType(库类型), "id='" + ID + "'", dict);
            }
        }
        string UpdateDNA_STR(string Sample_ID,
           string AMELOGENIN_1, string AMELOGENIN_2, string D8S1179_1, string D8S1179_2, string D21S11_1, string D21S11_2,
           string D18S51_1, string D18S51_2, string VWA_1, string VWA_2, string D3S1358_1, string D3S1358_2,
           string FGA_1, string FGA_2, string D5S818_1, string D5S818_2, string D13S317_1, string D13S317_2,
           string D7S820_1, string D7S820_2, string CSF1PO_1, string CSF1PO_2, string TH01_1, string TH01_2,
           string D16S539_1, string D16S539_2, string D2S1338_1, string D2S1338_2, string D19S433_1, string D19S433_2,
           string TPOX_1, string TPOX_2,
           string D6S1043_1, string D6S1043_2, string PENTA_E_1, string PENTA_E_2,
           string PENTA_D_1, string PENTA_D_2, string F13A01_1, string F13A01_2,
           string FESFPS_1, string FESFPS_2, string D1S80_1, string D1S80_2, string LOCUS5_1, string LOCUS5_2, string LOCUS4_1, string LOCUS4_2)
        {
            Sample_ID = GetString(Sample_ID);
            AMELOGENIN_1 = GetString(AMELOGENIN_1);
            AMELOGENIN_2 = GetString(AMELOGENIN_2);
            D8S1179_1 = GetString(D8S1179_1);
            D8S1179_2 = GetString(D8S1179_2);
            D21S11_1 = GetString(D21S11_1);
            D21S11_2 = GetString(D21S11_2);
            D18S51_1 = GetString(D18S51_1);
            D18S51_2 = GetString(D18S51_2);
            VWA_1 = GetString(VWA_1);
            VWA_2 = GetString(VWA_2);
            D3S1358_1 = GetString(D3S1358_1);
            D3S1358_2 = GetString(D3S1358_2);
            FGA_1 = GetString(FGA_1);
            FGA_2 = GetString(FGA_2);
            D5S818_1 = GetString(D5S818_1);
            D5S818_2 = GetString(D5S818_2);
            D13S317_1 = GetString(D13S317_1);
            D13S317_2 = GetString(D13S317_2);
            D7S820_1 = GetString(D7S820_1);
            D7S820_2 = GetString(D7S820_2);
            CSF1PO_1 = GetString(CSF1PO_1);
            CSF1PO_2 = GetString(CSF1PO_2);
            TH01_1 = GetString(TH01_1);
            TH01_2 = GetString(TH01_2);
            D16S539_1 = GetString(D16S539_1);
            D16S539_2 = GetString(D16S539_2);
            D2S1338_1 = GetString(D2S1338_1);
            D2S1338_2 = GetString(D2S1338_2);
            D19S433_1 = GetString(D19S433_1);
            D19S433_2 = GetString(D19S433_2);
            TPOX_1 = GetString(TPOX_1);
            TPOX_2 = GetString(TPOX_2);

            D6S1043_1 = GetString(D6S1043_1);
            D6S1043_2 = GetString(D6S1043_2);
            PENTA_E_1 = GetString(PENTA_E_1);
            PENTA_E_2 = GetString(PENTA_E_2);
            PENTA_D_1 = GetString(PENTA_D_1);
            PENTA_D_2 = GetString(PENTA_D_2);
            F13A01_1 = GetString(F13A01_1);
            F13A01_2 = GetString(F13A01_2);
            FESFPS_1 = GetString(FESFPS_1);
            FESFPS_2 = GetString(FESFPS_2);
            D1S80_1 = GetString(D1S80_1);
            D1S80_2 = GetString(D1S80_2);

            LOCUS5_1 = GetString(LOCUS5_1);
            LOCUS5_2 = GetString(LOCUS5_2);
            LOCUS4_1 = GetString(LOCUS4_1);
            LOCUS4_2 = GetString(LOCUS4_2);

            string values = string.Empty;
            if (!AMELOGENIN_1.Equals("NULL"))
            {
                values += string.Format("AMELOGENIN_1={0}, AMELOGENIN_2={1},", AMELOGENIN_1, AMELOGENIN_2);
            }
            if (!D8S1179_1.Equals("NULL"))
            {
                values += string.Format("D8S1179_1={0}, D8S1179_2={1},", D8S1179_1, D8S1179_2);
            }
            if (!D21S11_1.Equals("NULL"))
            {
                values += string.Format("D21S11_1={0}, D21S11_2={1},", D21S11_1, D21S11_2);
            }
            if (!D18S51_1.Equals("NULL"))
            {
                values += string.Format("D18S51_1={0}, D18S51_2={1},", D18S51_1, D18S51_2);
            }
            if (!VWA_1.Equals("NULL"))
            {
                values += string.Format("VWA_1={0}, VWA_2={1},", VWA_1, VWA_2);
            }
            if (!D3S1358_1.Equals("NULL"))
            {
                values += string.Format("D3S1358_1={0}, D3S1358_2={1},", D3S1358_1, D3S1358_2);
            }
            if (!FGA_1.Equals("NULL"))
            {
                values += string.Format("FGA_1={0}, FGA_2={1},", FGA_1, FGA_2);
            }
            if (!D5S818_1.Equals("NULL"))
            {
                values += string.Format("D5S818_1={0}, D5S818_2={1},", D5S818_1, D5S818_2);
            }
            if (!D13S317_1.Equals("NULL"))
            {
                values += string.Format("D13S317_1={0}, D13S317_2={1},", D13S317_1, D13S317_2);
            }
            if (!D7S820_1.Equals("NULL"))
            {
                values += string.Format("D7S820_1={0}, D7S820_2={1},", D7S820_1, D7S820_2);
            }
            if (!CSF1PO_1.Equals("NULL"))
            {
                values += string.Format("CSF1PO_1={0}, CSF1PO_2={1},", CSF1PO_1, CSF1PO_2);
            }
            if (!TH01_1.Equals("NULL"))
            {
                values += string.Format("TH01_1={0}, TH01_2={1},", TH01_1, TH01_2);
            }
            if (!D16S539_1.Equals("NULL"))
            {
                values += string.Format("D16S539_1={0}, D16S539_2={1},", D16S539_1, D16S539_2);
            }
            if (!D2S1338_1.Equals("NULL"))
            {
                values += string.Format("D2S1338_1={0}, D2S1338_2={1},", D2S1338_1, D2S1338_2);
            }
            if (!D19S433_1.Equals("NULL"))
            {
                values += string.Format("D19S433_1={0}, D19S433_2={1},", D19S433_1, D19S433_2);
            }
            if (!TPOX_1.Equals("NULL"))
            {
                values += string.Format("TPOX_1={0}, TPOX_2={1},", TPOX_1, TPOX_2);
            }
            if (!D6S1043_1.Equals("NULL"))
            {
                values += string.Format("D6S1043_1={0}, D6S1043_2={1},", D6S1043_1, D6S1043_2);
            }
            if (!PENTA_E_1.Equals("NULL"))
            {
                values += string.Format("PENTA_E_1={0}, PENTA_E_2={1},", PENTA_E_1, PENTA_E_2);
            }
            if (!PENTA_D_1.Equals("NULL"))
            {
                values += string.Format("PENTA_D_1={0}, PENTA_D_2={1},", PENTA_D_1, PENTA_D_2);
            }
            if (!F13A01_1.Equals("NULL"))
            {
                values += string.Format("F13A01_1={0}, F13A01_2={1},", F13A01_1, F13A01_2);
            }
            if (!FESFPS_1.Equals("NULL"))
            {
                values += string.Format("FESFPS_1={0}, FESFPS_2={1},", FESFPS_1, FESFPS_2);
            }
            if (!D1S80_1.Equals("NULL"))
            {
                values += string.Format("D1S80_1={0}, D1S80_2={1},", D1S80_1, D1S80_2);
            }
            if (!LOCUS5_1.Equals("NULL"))
            {
                values += string.Format("LOCUS5_1={0}, LOCUS5_2={1},", LOCUS5_1, LOCUS5_2);
            }
            if (!LOCUS4_1.Equals("NULL"))
            {
                values += string.Format("LOCUS4_1={0}, LOCUS4_2={1},", LOCUS4_1, LOCUS4_2);
            }
            if (values.Length == 0)
            {
                return string.Empty;
            }
            return string.Format(@"update gdna.DNA_LAB_STR set {0} where Sample_ID=" + Sample_ID + ";", values.Substring(0, values.Length - 1));
        }

        [WebMethod]
        public string PrintStrTable(string 鉴定单位, string 案件ID, string 委托编号, string 库类型, string 试剂盒)
        {
            string fileName = Helper.GenerateID() + ".xls";
            //string filter = "(STR_FLAG='1' or YSTR_FLAG='1') AND ";
            string filter = string.Empty;

            if (鉴定单位.Length > 0) filter += "鉴定单位='" + 鉴定单位 + "' and ";
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";
            if (库类型.Length > 0) filter += "库类型='" + 库类型 + "' and ";
            DataSet ds = DBHelperSQL.Select("样本视图", Helper.CutFilter(filter), "比中序号 ,样本编号", "*");

            DataSet ds2 = DBHelperSQL.Select("鉴定流程视图", "委托编号='" + 委托编号 + "'", "", "案件编号");
            string result = string.Empty;
            switch (试剂盒)
            {
                case "Identfiler": result = ExcelOper.PrintStrID(ds.Tables[0], ds2.Tables[0], fileName); break;
                case "PowerPlex16": result = ExcelOper.PrintStrPP16(ds.Tables[0], ds2.Tables[0], fileName); break;
                case "DNATyper15": result = ExcelOper.PrintStrTYPER(ds.Tables[0], ds2.Tables[0], fileName); break;
                case "Y-filer": result = ExcelOper.PrintStrY(ds.Tables[0], ds2.Tables[0], fileName); break;
                default: result = "0"; break;
            }
            return result;
        }
        [WebMethod]
        public string SameCaseBzAna(string 案件ID)
        {
            DataSet ds = null;
            string sql = string.Empty;
            ds = DBHelperSQL.Query(string.Format(@"select * from 样本视图 where 案件ID='{0}';", 案件ID));
            //同案比中分析
            int bzxh = 1;
            DataTable dt = ds.Tables[0];
            DataColumn dc = new DataColumn("比中序号2", typeof(int)); dt.Columns.Add(dc);
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                if (dt.Rows[i]["比中序号2"].ToString().Length == 0)
                {
                    dt.Rows[i]["比中序号2"] = bzxh;
                }
                else
                {
                    continue;
                }
                for (int j = i + 1; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["比中序号2"].ToString().Length > 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (RC(dt.Rows[i], dt.Rows[j], false) > 0)
                        {
                            dt.Rows[j]["比中序号2"] = bzxh;
                        }
                    }
                }
                bzxh++;
            }
            foreach (DataRow dr in dt.Rows)
            {
                sql += "update " + Helper.GetTableByScType(dr["库类型"].ToString()) + " set 比中序号='" + dr["比中序号2"].ToString() + "' where id='" + dr["id"].ToString() + "';";
            }
            return DBHelperSQL.ExecuteSql(sql);
        }
        private static int RC(DataRow src, DataRow tar, bool ystr)
        {
            int number = 0;
            if (ystr)
            {
                if ((src["B_DYS456"].ToString().Trim().Length > 0) &&
                    (tar["B_DYS456"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["B_DYS456"].ToString().Trim().Equals(tar["B_DYS456"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["B_DYS389I"].ToString().Trim().Length > 0) &&
                   (tar["B_DYS389I"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["B_DYS389I"].ToString().Trim().Equals(tar["B_DYS389I"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["B_DYS390"].ToString().Trim().Length > 0) &&
                  (tar["B_DYS390"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["B_DYS390"].ToString().Trim().Equals(tar["B_DYS390"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["B_DYS389II"].ToString().Trim().Length > 0) &&
                  (tar["B_DYS389II"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["B_DYS389II"].ToString().Trim().Equals(tar["B_DYS389II"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["G_DYS458"].ToString().Trim().Length > 0) &&
                    (tar["G_DYS458"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["G_DYS458"].ToString().Trim().Equals(tar["G_DYS458"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["G_DYS19"].ToString().Trim().Length > 0) &&
                   (tar["G_DYS19"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["G_DYS19"].ToString().Trim().Equals(tar["G_DYS19"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["G_DYS385"].ToString().Trim().Length > 0) &&
                  (tar["G_DYS385"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["G_DYS385"].ToString().Trim().Equals(tar["G_DYS385"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["Y_DYS393"].ToString().Trim().Length > 0) &&
                  (tar["Y_DYS393"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["Y_DYS393"].ToString().Trim().Equals(tar["Y_DYS393"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["Y_DYS391"].ToString().Trim().Length > 0) &&
                    (tar["Y_DYS391"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["Y_DYS391"].ToString().Trim().Equals(tar["Y_DYS391"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["Y_DYS439"].ToString().Trim().Length > 0) &&
                   (tar["Y_DYS439"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["Y_DYS439"].ToString().Trim().Equals(tar["Y_DYS439"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["Y_DYS635"].ToString().Trim().Length > 0) &&
                  (tar["Y_DYS635"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["Y_DYS635"].ToString().Trim().Equals(tar["Y_DYS635"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["Y_DYS392"].ToString().Trim().Length > 0) &&
                  (tar["Y_DYS392"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["Y_DYS392"].ToString().Trim().Equals(tar["Y_DYS392"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["R_Y_GATA_H4"].ToString().Trim().Length > 0) &&
                    (tar["R_Y_GATA_H4"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["R_Y_GATA_H4"].ToString().Trim().Equals(tar["R_Y_GATA_H4"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["R_DYS437"].ToString().Trim().Length > 0) &&
                   (tar["R_DYS437"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["R_DYS437"].ToString().Trim().Equals(tar["R_DYS437"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["R_DYS438"].ToString().Trim().Length > 0) &&
                  (tar["R_DYS438"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["R_DYS438"].ToString().Trim().Equals(tar["R_DYS438"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["R_DYS448"].ToString().Trim().Length > 0) &&
                  (tar["R_DYS448"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["R_DYS448"].ToString().Trim().Equals(tar["R_DYS448"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["Y_indel"].ToString().Trim().Length > 0) &&
                 (tar["Y_indel"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["Y_indel"].ToString().Trim().Equals(tar["Y_indel"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
            }
            else
            {
                if ((src["AMEL"].ToString().Trim().Length > 0) &&
                    (tar["AMEL"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["AMEL"].ToString().Trim().Equals(tar["AMEL"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D8S1179"].ToString().Trim().Length > 0) &&
                   (tar["D8S1179"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D8S1179"].ToString().Trim().Equals(tar["D8S1179"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D21S11"].ToString().Trim().Length > 0) &&
                  (tar["D21S11"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D21S11"].ToString().Trim().Equals(tar["D21S11"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D18S51"].ToString().Trim().Length > 0) &&
                  (tar["D18S51"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D18S51"].ToString().Trim().Equals(tar["D18S51"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["VWA"].ToString().Trim().Length > 0) &&
                  (tar["VWA"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["VWA"].ToString().Trim().Equals(tar["VWA"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D3S1358"].ToString().Trim().Length > 0) &&
                  (tar["D3S1358"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D3S1358"].ToString().Trim().Equals(tar["D3S1358"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["FGA"].ToString().Trim().Length > 0) &&
                  (tar["FGA"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["FGA"].ToString().Trim().Equals(tar["FGA"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["TH01"].ToString().Trim().Length > 0) &&
                  (tar["TH01"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["TH01"].ToString().Trim().Equals(tar["TH01"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D5S818"].ToString().Trim().Length > 0) &&
                  (tar["D5S818"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D5S818"].ToString().Trim().Equals(tar["D5S818"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D13S317"].ToString().Trim().Length > 0) &&
                  (tar["D13S317"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D13S317"].ToString().Trim().Equals(tar["D13S317"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D7S820"].ToString().Trim().Length > 0) &&
                  (tar["D7S820"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D7S820"].ToString().Trim().Equals(tar["D7S820"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["CSF1PO"].ToString().Trim().Length > 0) &&
                  (tar["CSF1PO"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["CSF1PO"].ToString().Trim().Equals(tar["CSF1PO"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D16S539"].ToString().Trim().Length > 0) &&
                  (tar["D16S539"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D16S539"].ToString().Trim().Equals(tar["D16S539"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["TPOX"].ToString().Trim().Length > 0) &&
                  (tar["TPOX"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["TPOX"].ToString().Trim().Equals(tar["TPOX"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D2S1338"].ToString().Trim().Length > 0) &&
                  (tar["D2S1338"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D2S1338"].ToString().Trim().Equals(tar["D2S1338"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D19S433"].ToString().Trim().Length > 0) &&
                  (tar["D19S433"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D19S433"].ToString().Trim().Equals(tar["D19S433"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["PENTA_D"].ToString().Trim().Length > 0) &&
                  (tar["PENTA_D"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["PENTA_D"].ToString().Trim().Equals(tar["PENTA_D"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["PENTA_E"].ToString().Trim().Length > 0) &&
                  (tar["PENTA_E"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["PENTA_E"].ToString().Trim().Equals(tar["PENTA_E"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D6S1043"].ToString().Trim().Length > 0) &&
                  (tar["D6S1043"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D6S1043"].ToString().Trim().Equals(tar["D6S1043"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["F13A01"].ToString().Trim().Length > 0) &&
                  (tar["F13A01"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["F13A01"].ToString().Trim().Equals(tar["F13A01"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["FESFPS"].ToString().Trim().Length > 0) &&
                 (tar["FESFPS"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["FESFPS"].ToString().Trim().Equals(tar["FESFPS"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D1S80"].ToString().Trim().Length > 0) &&
                 (tar["D1S80"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D1S80"].ToString().Trim().Equals(tar["D1S80"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D12S391"].ToString().Trim().Length > 0) &&
                  (tar["D12S391"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D12S391"].ToString().Trim().Equals(tar["D12S391"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D1S1656"].ToString().Trim().Length > 0) &&
                  (tar["D1S1656"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D1S1656"].ToString().Trim().Equals(tar["D1S1656"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D2S441"].ToString().Trim().Length > 0) &&
                 (tar["D2S441"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D2S441"].ToString().Trim().Equals(tar["D2S441"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D22S1045"].ToString().Trim().Length > 0) &&
                 (tar["D22S1045"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D22S1045"].ToString().Trim().Equals(tar["D22S1045"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["SE33"].ToString().Trim().Length > 0) &&
                 (tar["SE33"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["SE33"].ToString().Trim().Equals(tar["SE33"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
                if ((src["D10S1248"].ToString().Trim().Length > 0) &&
                 (tar["D10S1248"].ToString().Trim().Length > 0))
                {
                    number++;
                    if (!src["D10S1248"].ToString().Trim().Equals(tar["D10S1248"].ToString().Trim()))
                    {
                        return 0;
                    }
                }
            }
            return number;
        }
        #endregion
        #region 导入str
        [WebMethod]
        public string Import(string idstr, string user, string caseId)
        {
            string[] ids = idstr.Split(new char[] { '，' }, StringSplitOptions.RemoveEmptyEntries);

            DNADICTWS dictWs = new DNADICTWS();
            IDictionary<string, string> RELATION = dictWs.GetDnaDict("亲属关系");
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
                            foreach (string s in ids)
                            {
                                DataSet ds = DBHelperSQL.Query("select * from 样本视图 where id='" + s + "';", dbConnection, trans);
                                DataRow dr = ds.Tables[0].Rows[0];
                                if (!dr["imp_flag"].ToString().Equals("1"))
                                {
                                    string category = Helper.GetScByScType(dr["库类型"].ToString());
                                    string geneId = Helper.GenerateID();
                                    string anotherGeneId = Helper.GenerateID();
                                    string AMELOGENIN_1 = string.Empty; string AMELOGENIN_2 = string.Empty; string D8S1179_1 = string.Empty; string D8S1179_2 = string.Empty;
                                    string D21S11_1 = string.Empty; string D21S11_2 = string.Empty; string D18S51_1 = string.Empty; string D18S51_2 = string.Empty;
                                    string VWA_1 = string.Empty; string VWA_2 = string.Empty; string D3S1358_1 = string.Empty; string D3S1358_2 = string.Empty;
                                    string FGA_1 = string.Empty; string FGA_2 = string.Empty; string D5S818_1 = string.Empty; string D5S818_2 = string.Empty;
                                    string D13S317_1 = string.Empty; string D13S317_2 = string.Empty; string D7S820_1 = string.Empty; string D7S820_2 = string.Empty;
                                    string CSF1PO_1 = string.Empty; string CSF1PO_2 = string.Empty; string TH01_1 = string.Empty; string TH01_2 = string.Empty;
                                    string D16S539_1 = string.Empty; string D16S539_2 = string.Empty; string D2S1338_1 = string.Empty; string D2S1338_2 = string.Empty;
                                    string D19S433_1 = string.Empty; string D19S433_2 = string.Empty; string TPOX_1 = string.Empty; string TPOX_2 = string.Empty;
                                    string D6S1043_1 = string.Empty; string D6S1043_2 = string.Empty; string PENTA_E_1 = string.Empty; string PENTA_E_2 = string.Empty;
                                    string PENTA_D_1 = string.Empty; string PENTA_D_2 = string.Empty; string F13A01_1 = string.Empty; string F13A01_2 = string.Empty;
                                    string FESFPS_1 = string.Empty; string FESFPS_2 = string.Empty; string D1S80_1 = string.Empty; string D1S80_2 = string.Empty;
                                    string LOCUS5_1 = string.Empty; string LOCUS5_2 = string.Empty; string LOCUS4_1 = string.Empty; string LOCUS4_2 = string.Empty;
                                    int ac = 0, every = 0;
                                    every = Anal(dr["amel"].ToString(), ref AMELOGENIN_1, ref AMELOGENIN_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D8S1179"].ToString(), ref D8S1179_1, ref D8S1179_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D21S11"].ToString(), ref D21S11_1, ref D21S11_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D18S51"].ToString(), ref D18S51_1, ref D18S51_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["vWA"].ToString(), ref VWA_1, ref VWA_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D3S1358"].ToString(), ref D3S1358_1, ref D3S1358_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["FGA"].ToString(), ref FGA_1, ref FGA_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["TH01"].ToString(), ref TH01_1, ref TH01_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D5S818"].ToString(), ref D5S818_1, ref D5S818_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D13S317"].ToString(), ref D13S317_1, ref D13S317_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D7S820"].ToString(), ref D7S820_1, ref D7S820_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["CSF1PO"].ToString(), ref CSF1PO_1, ref CSF1PO_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D16S539"].ToString(), ref D16S539_1, ref D16S539_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["TPOX"].ToString(), ref TPOX_1, ref TPOX_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D2S1338"].ToString(), ref D2S1338_1, ref D2S1338_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D19S433"].ToString(), ref D19S433_1, ref D19S433_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["Penta_D"].ToString(), ref PENTA_D_1, ref PENTA_D_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["Penta_E"].ToString(), ref PENTA_E_1, ref PENTA_E_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D6S1043"].ToString(), ref D6S1043_1, ref D6S1043_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["F13A01"].ToString(), ref F13A01_1, ref F13A01_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["FESFPS"].ToString(), ref FESFPS_1, ref FESFPS_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D1S80"].ToString(), ref D1S80_1, ref D1S80_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D12S391"].ToString(), ref LOCUS5_1, ref LOCUS5_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    every = Anal(dr["D1S1656"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                    if (every == 2) { continue; } else { ac += every; }
                                    //DNA 数据库 未更新 D2S441, D22S1045,SE33,D10S1248,Y_indel,
                                    //every = Anal(dr["D2S441"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                    //if (every == 2) { continue; } else { ac += every; }
                                    //every = Anal(dr["D22S1045"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                    //if (every == 2) { continue; } else { ac += every; }
                                    //every = Anal(dr["SE33"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                    //if (every == 2) { continue; } else { ac += every; }
                                    //every = Anal(dr["D10S1248"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                    //if (every == 2) { continue; } else { ac += every; }
                                    //every = Anal(dr["Y_indel"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                    //if (every == 2) { continue; } else { ac += every; }
                                    InsertDNA_LAB_STR(geneId, dr["ID"].ToString(), category, ac.ToString(),
                                        AMELOGENIN_1, AMELOGENIN_2, D8S1179_1, D8S1179_2, D21S11_1, D21S11_2,
                                        D18S51_1, D18S51_2, VWA_1, VWA_2, D3S1358_1, D3S1358_2,
                                        FGA_1, FGA_2, D5S818_1, D5S818_2, D13S317_1, D13S317_2,
                                        D7S820_1, D7S820_2, CSF1PO_1, CSF1PO_2, TH01_1, TH01_2,
                                        D16S539_1, D16S539_2, D2S1338_1, D2S1338_2, D19S433_1, D19S433_2,
                                        TPOX_1, TPOX_2, D6S1043_1, D6S1043_2, PENTA_E_1, PENTA_E_2,
                                        PENTA_D_1, PENTA_D_2, F13A01_1, F13A01_2,
                                        FESFPS_1, FESFPS_2, D1S80_1, D1S80_2, LOCUS5_1, LOCUS5_2, LOCUS4_1, LOCUS4_2, user, dbConnectionOra, transOra);
                                    DBHelperSQL.ExecuteSql("update " + Helper.GetTableByScType(dr["库类型"].ToString()) + " set imp_flag='1' where id='" +
                                        s + "';", dbConnection, trans);

                                    if (category.Equals("1") || category.Equals("2") || category.Equals("3") || category.Equals("5") || category.Equals("6"))
                                    {
                                        InsertAQ_Normal(geneId, category, user, dbConnectionOra, transOra);
                                    }
                                    else if (category.Equals("16") || category.Equals("17"))
                                    {
                                        DataSet ds2 = DBHelperSQL.Query("select * from 亲属定义 where 亲属一ID='" + dr["ID"].ToString() +
                                            "' or 亲属二ID='" + dr["ID"].ToString() + "';", dbConnection, trans);
                                        DataRow dr2 = ds2.Tables[0].Rows[0];

                                        string geneId2 = string.Empty;
                                        string aGeneId2 = string.Empty;

                                        string anotherId = string.Empty;
                                        if (dr2["亲属一ID"].ToString().Equals(dr["ID"].ToString()))
                                        {
                                            anotherId = dr2["亲属二ID"].ToString();
                                            geneId2 = dr2["亲属一ID"].ToString();
                                            aGeneId2 = anotherId;
                                        }
                                        else
                                        {
                                            anotherId = dr2["亲属一ID"].ToString();
                                            geneId2 = dr2["亲属二ID"].ToString();
                                            aGeneId2 = anotherId;
                                        }
                                        if (anotherId.Length > 0)
                                        {
                                            DataSet ds3 = DBHelperSQL.Query("select * from 亲属信息 where ID='" + anotherId + "';", dbConnection, trans);
                                            DataRow dr3 = ds3.Tables[0].Rows[0];
                                            AMELOGENIN_1 = string.Empty; AMELOGENIN_2 = string.Empty; D8S1179_1 = string.Empty; D8S1179_2 = string.Empty;
                                            D21S11_1 = string.Empty; D21S11_2 = string.Empty; D18S51_1 = string.Empty; D18S51_2 = string.Empty;
                                            VWA_1 = string.Empty; VWA_2 = string.Empty; D3S1358_1 = string.Empty; D3S1358_2 = string.Empty;
                                            FGA_1 = string.Empty; FGA_2 = string.Empty; D5S818_1 = string.Empty; D5S818_2 = string.Empty;
                                            D13S317_1 = string.Empty; D13S317_2 = string.Empty; D7S820_1 = string.Empty; D7S820_2 = string.Empty;
                                            CSF1PO_1 = string.Empty; CSF1PO_2 = string.Empty; TH01_1 = string.Empty; TH01_2 = string.Empty;
                                            D16S539_1 = string.Empty; D16S539_2 = string.Empty; D2S1338_1 = string.Empty; D2S1338_2 = string.Empty;
                                            D19S433_1 = string.Empty; D19S433_2 = string.Empty; TPOX_1 = string.Empty; TPOX_2 = string.Empty;
                                            D6S1043_1 = string.Empty; D6S1043_2 = string.Empty; PENTA_E_1 = string.Empty; PENTA_E_2 = string.Empty;
                                            PENTA_D_1 = string.Empty; PENTA_D_2 = string.Empty; F13A01_1 = string.Empty; F13A01_2 = string.Empty;
                                            FESFPS_1 = string.Empty; FESFPS_2 = string.Empty; D1S80_1 = string.Empty; D1S80_2 = string.Empty;
                                            LOCUS5_1 = string.Empty; LOCUS5_2 = string.Empty; LOCUS4_1 = string.Empty; LOCUS4_2 = string.Empty;
                                            ac = 0; every = 0;
                                            every = Anal(dr3["amel"].ToString(), ref AMELOGENIN_1, ref AMELOGENIN_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D8S1179"].ToString(), ref D8S1179_1, ref D8S1179_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D21S11"].ToString(), ref D21S11_1, ref D21S11_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D18S51"].ToString(), ref D18S51_1, ref D18S51_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["vWA"].ToString(), ref VWA_1, ref VWA_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D3S1358"].ToString(), ref D3S1358_1, ref D3S1358_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["FGA"].ToString(), ref FGA_1, ref FGA_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["TH01"].ToString(), ref TH01_1, ref TH01_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D5S818"].ToString(), ref D5S818_1, ref D5S818_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D13S317"].ToString(), ref D13S317_1, ref D13S317_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D7S820"].ToString(), ref D7S820_1, ref D7S820_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["CSF1PO"].ToString(), ref CSF1PO_1, ref CSF1PO_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D16S539"].ToString(), ref D16S539_1, ref D16S539_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["TPOX"].ToString(), ref TPOX_1, ref TPOX_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D2S1338"].ToString(), ref D2S1338_1, ref D2S1338_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D19S433"].ToString(), ref D19S433_1, ref D19S433_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["Penta_D"].ToString(), ref PENTA_D_1, ref PENTA_D_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["Penta_E"].ToString(), ref PENTA_E_1, ref PENTA_E_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D6S1043"].ToString(), ref D6S1043_1, ref D6S1043_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["F13A01"].ToString(), ref F13A01_1, ref F13A01_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["FESFPS"].ToString(), ref FESFPS_1, ref FESFPS_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D1S80"].ToString(), ref D1S80_1, ref D1S80_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D12S391"].ToString(), ref LOCUS5_1, ref LOCUS5_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            every = Anal(dr3["D1S1656"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                            if (every == 2) { continue; } else { ac += every; }
                                            //DNA 数据库 未更新 D2S441, D22S1045,SE33,D10S1248,Y_indel,
                                            //every = Anal(dr3["D2S441"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                            //if (every == 2) { continue; } else { ac += every; }
                                            //every = Anal(dr3["D22S1045"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                            //if (every == 2) { continue; } else { ac += every; }
                                            //every = Anal(dr3["SE33"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                            //if (every == 2) { continue; } else { ac += every; }
                                            //every = Anal(dr3["D10S1248"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                            //if (every == 2) { continue; } else { ac += every; }
                                            //every = Anal(dr3["Y_indel"].ToString(), ref LOCUS4_1, ref LOCUS4_2);
                                            //if (every == 2) { continue; } else { ac += every; }
                                            InsertDNA_LAB_STR(anotherGeneId, anotherId, category, ac.ToString(),
                                                AMELOGENIN_1, AMELOGENIN_2, D8S1179_1, D8S1179_2, D21S11_1, D21S11_2,
                                                D18S51_1, D18S51_2, VWA_1, VWA_2, D3S1358_1, D3S1358_2,
                                                FGA_1, FGA_2, D5S818_1, D5S818_2, D13S317_1, D13S317_2,
                                                D7S820_1, D7S820_2, CSF1PO_1, CSF1PO_2, TH01_1, TH01_2,
                                                D16S539_1, D16S539_2, D2S1338_1, D2S1338_2, D19S433_1, D19S433_2,
                                                TPOX_1, TPOX_2, D6S1043_1, D6S1043_2, PENTA_E_1, PENTA_E_2,
                                                PENTA_D_1, PENTA_D_2, F13A01_1, F13A01_2,
                                                FESFPS_1, FESFPS_2, D1S80_1, D1S80_2, LOCUS5_1, LOCUS5_2, LOCUS4_1, LOCUS4_2, user, dbConnectionOra, transOra);
                                            DBHelperSQL.ExecuteSql("update 亲属信息 set imp_flag='1' where id='" + anotherId + "';", dbConnection, trans);
                                        }
                                        InsertAQ_CaseR(geneId2, aGeneId2, category, user, RELATION[dr2["亲属关系"].ToString()], caseId, dbConnectionOra, transOra);
                                    }
                                }
                            }

                            transOra.Commit();
                            trans.Commit();
                        }
                    }
                }
            }
            return "1";
        }
        void InsertDNA_LAB_STR(string ID, string Sample_ID, string SAMPLE_CATEGORY,
           string ALLELE_COUNT,
           string AMELOGENIN_1, string AMELOGENIN_2, string D8S1179_1, string D8S1179_2, string D21S11_1, string D21S11_2,
           string D18S51_1, string D18S51_2, string VWA_1, string VWA_2, string D3S1358_1, string D3S1358_2,
           string FGA_1, string FGA_2, string D5S818_1, string D5S818_2, string D13S317_1, string D13S317_2,
           string D7S820_1, string D7S820_2, string CSF1PO_1, string CSF1PO_2, string TH01_1, string TH01_2,
           string D16S539_1, string D16S539_2, string D2S1338_1, string D2S1338_2, string D19S433_1, string D19S433_2,
           string TPOX_1, string TPOX_2,
           string D6S1043_1, string D6S1043_2, string PENTA_E_1, string PENTA_E_2,
           string PENTA_D_1, string PENTA_D_2, string F13A01_1, string F13A01_2,
           string FESFPS_1, string FESFPS_2, string D1S80_1, string D1S80_2, string LOCUS5_1, string LOCUS5_2, string LOCUS4_1, string LOCUS4_2,
           string username, OracleConnection dbConnection, OracleTransaction trans)
        {
            string sql = string.Empty;
            ALLELE_COUNT = GetString(ALLELE_COUNT);
            AMELOGENIN_1 = GetString(AMELOGENIN_1);
            AMELOGENIN_2 = GetString(AMELOGENIN_2);
            D8S1179_1 = GetString(D8S1179_1);
            D8S1179_2 = GetString(D8S1179_2);
            D21S11_1 = GetString(D21S11_1);
            D21S11_2 = GetString(D21S11_2);
            D18S51_1 = GetString(D18S51_1);
            D18S51_2 = GetString(D18S51_2);
            VWA_1 = GetString(VWA_1);
            VWA_2 = GetString(VWA_2);
            D3S1358_1 = GetString(D3S1358_1);
            D3S1358_2 = GetString(D3S1358_2);
            FGA_1 = GetString(FGA_1);
            FGA_2 = GetString(FGA_2);
            D5S818_1 = GetString(D5S818_1);
            D5S818_2 = GetString(D5S818_2);
            D13S317_1 = GetString(D13S317_1);
            D13S317_2 = GetString(D13S317_2);
            D7S820_1 = GetString(D7S820_1);
            D7S820_2 = GetString(D7S820_2);
            CSF1PO_1 = GetString(CSF1PO_1);
            CSF1PO_2 = GetString(CSF1PO_2);
            TH01_1 = GetString(TH01_1);
            TH01_2 = GetString(TH01_2);
            D16S539_1 = GetString(D16S539_1);
            D16S539_2 = GetString(D16S539_2);
            D2S1338_1 = GetString(D2S1338_1);
            D2S1338_2 = GetString(D2S1338_2);
            D19S433_1 = GetString(D19S433_1);
            D19S433_2 = GetString(D19S433_2);
            TPOX_1 = GetString(TPOX_1);
            TPOX_2 = GetString(TPOX_2);

            D6S1043_1 = GetString(D6S1043_1);
            D6S1043_2 = GetString(D6S1043_2);
            PENTA_E_1 = GetString(PENTA_E_1);
            PENTA_E_2 = GetString(PENTA_E_2);
            PENTA_D_1 = GetString(PENTA_D_1);
            PENTA_D_2 = GetString(PENTA_D_2);
            F13A01_1 = GetString(F13A01_1);
            F13A01_2 = GetString(F13A01_2);
            FESFPS_1 = GetString(FESFPS_1);
            FESFPS_2 = GetString(FESFPS_2);
            D1S80_1 = GetString(D1S80_1);
            D1S80_2 = GetString(D1S80_2);

            LOCUS5_1 = GetString(LOCUS5_1);
            LOCUS5_2 = GetString(LOCUS5_2);
            LOCUS4_1 = GetString(LOCUS4_1);
            LOCUS4_2 = GetString(LOCUS4_2);

            //查找试剂盒种类
            string needtoInsert = string.Empty;
            string values = string.Empty;
            if (!AMELOGENIN_1.Equals("NULL"))
            {
                needtoInsert += "AMELOGENIN_1, AMELOGENIN_2,";
                values += AMELOGENIN_1 + "," + AMELOGENIN_2 + ",";
            }
            if (!D8S1179_1.Equals("NULL"))
            {
                needtoInsert += "D8S1179_1, D8S1179_2,";
                values += D8S1179_1 + "," + D8S1179_2 + ",";
            }
            if (!D21S11_1.Equals("NULL"))
            {
                needtoInsert += "D21S11_1, D21S11_2,";
                values += D21S11_1 + "," + D21S11_2 + ",";
            }
            if (!D18S51_1.Equals("NULL"))
            {
                needtoInsert += "D18S51_1, D18S51_2,";
                values += D18S51_1 + "," + D18S51_2 + ",";
            }
            if (!VWA_1.Equals("NULL"))
            {
                needtoInsert += "VWA_1, VWA_2,";
                values += VWA_1 + "," + VWA_2 + ",";
            }
            if (!D3S1358_1.Equals("NULL"))
            {
                needtoInsert += "D3S1358_1, D3S1358_2,";
                values += D3S1358_1 + "," + D3S1358_2 + ",";
            }
            if (!FGA_1.Equals("NULL"))
            {
                needtoInsert += "FGA_1, FGA_2,";
                values += FGA_1 + "," + FGA_2 + ",";
            }
            if (!D5S818_1.Equals("NULL"))
            {
                needtoInsert += "D5S818_1, D5S818_2,";
                values += D5S818_1 + "," + D5S818_2 + ",";
            }
            if (!D13S317_1.Equals("NULL"))
            {
                needtoInsert += "D13S317_1, D13S317_2,";
                values += D13S317_1 + "," + D13S317_2 + ",";
            }
            if (!D7S820_1.Equals("NULL"))
            {
                needtoInsert += "D7S820_1, D7S820_2,";
                values += D7S820_1 + "," + D7S820_2 + ",";
            }
            if (!CSF1PO_1.Equals("NULL"))
            {
                needtoInsert += "CSF1PO_1, CSF1PO_2,";
                values += CSF1PO_1 + "," + CSF1PO_2 + ",";
            }
            if (!TH01_1.Equals("NULL"))
            {
                needtoInsert += "TH01_1, TH01_2,";
                values += TH01_1 + "," + TH01_2 + ",";
            }
            if (!D16S539_1.Equals("NULL"))
            {
                needtoInsert += "D16S539_1, D16S539_2,";
                values += D16S539_1 + "," + D16S539_2 + ",";
            }
            if (!D2S1338_1.Equals("NULL"))
            {
                needtoInsert += "D2S1338_1, D2S1338_2,";
                values += D2S1338_1 + "," + D2S1338_2 + ",";
            }
            if (!D19S433_1.Equals("NULL"))
            {
                needtoInsert += "D19S433_1, D19S433_2,";
                values += D19S433_1 + "," + D19S433_2 + ",";
            }
            if (!TPOX_1.Equals("NULL"))
            {
                needtoInsert += "TPOX_1, TPOX_2,";
                values += TPOX_1 + "," + TPOX_2 + ",";
            }
            if (!D6S1043_1.Equals("NULL"))
            {
                needtoInsert += "D6S1043_1, D6S1043_2,";
                values += D6S1043_1 + "," + D6S1043_2 + ",";
            }
            if (!PENTA_E_1.Equals("NULL"))
            {
                needtoInsert += "PENTA_E_1, PENTA_E_2,";
                values += PENTA_E_1 + "," + PENTA_E_2 + ",";
            }
            if (!PENTA_D_1.Equals("NULL"))
            {
                needtoInsert += "PENTA_D_1, PENTA_D_2,";
                values += PENTA_D_1 + "," + PENTA_D_2 + ",";
            }
            if (!F13A01_1.Equals("NULL"))
            {
                needtoInsert += "F13A01_1, F13A01_2,";
                values += F13A01_1 + "," + F13A01_2 + ",";
            }
            if (!FESFPS_1.Equals("NULL"))
            {
                needtoInsert += "FESFPS_1, FESFPS_2,";
                values += FESFPS_1 + "," + FESFPS_2 + ",";
            }
            if (!D1S80_1.Equals("NULL"))
            {
                needtoInsert += "D1S80_1, D1S80_2,";
                values += D1S80_1 + "," + D1S80_2 + ",";
            }
            if (!LOCUS5_1.Equals("NULL"))
            {
                needtoInsert += "LOCUS5_1, LOCUS5_2,";
                values += LOCUS5_1 + "," + LOCUS5_2 + ",";
            }
            if (!LOCUS4_1.Equals("NULL"))
            {
                needtoInsert += "LOCUS4_1, LOCUS4_2,";
                values += LOCUS4_1 + "," + LOCUS4_2 + ",";
            }
            sql = string.Format(@"Insert into gdna.DNA_LAB_STR(
                                              ID, Sample_ID, SAMPLE_CATEGORY, ALLELE_COUNT,
                                              {4} LAB_ID, SERVER_NO,
                                              STORE_DATE,STORE_BY,STORE_FLAG,REVIEW_STATUS,CREATE_USER,CREATE_DATETIME,TRANSFER_STATUS,MATCHED_STATUS,reagent_kit) values
                                              ({0},{1},{2},{3},
                                              {5} {6},{7},
                                              null,null,'0','1',{8},sysdate,'1','2','3');",
                                              GetString(ID), GetString(Sample_ID), GetString(SAMPLE_CATEGORY), ALLELE_COUNT,
                                              needtoInsert, values, GetString(DBHelperOracle.LAB_ID), GetString(DBHelperOracle.INIT_SERVER_NO), GetString(username),
                                               GetString(username));
            sql += "update gdna." + GetTable(SAMPLE_CATEGORY) + " set STR_Flag='1' where id=" + GetString(Sample_ID) + ";";
            sql += "UPDATE GDNA.SAMPLE_EXAMINATION SET EXAMINE_STAGE='5' WHERE SAMPLE_ID=" + GetString(Sample_ID) + ";";
            DBHelperOracle.ExecuteSqlWithTransaction(sql, dbConnection, trans);
        }
        string InsertAQ(string ID, string GENE_ID, string ANOTHER_GENE_ID, string GENE_CATEGORY
             , string GENE_TYPE, string TASK_TYPE, string GENE_ORIGIN, string GENE_SERVER, string DATA_TYPE, string TARGET_DATA_TYPE
             , string TARGET_SERVER, string ALIGN_MODE, string MATCH_LOWER_LIMIT
             , string ERROR_UPPER_LIMIT, string MATCH_PARAMETER, string SUBMIT_BY, string TASK_STATUS, string OUT_BOUND, string REMARK)
        {
            ID = GetString(ID); string ALIGN_NO = GetString(Helper.GetNextNoForDna(DBHelperOracle.INIT_SERVER_NO, "BD"));
            GENE_ID = GetString(GENE_ID); ANOTHER_GENE_ID = GetString(ANOTHER_GENE_ID);
            GENE_CATEGORY = GetString(GENE_CATEGORY); GENE_TYPE = GetString(GENE_TYPE);
            TASK_TYPE = GetString(TASK_TYPE); GENE_ORIGIN = GetString(GENE_ORIGIN);
            GENE_SERVER = GetString(GENE_SERVER); DATA_TYPE = GetString(DATA_TYPE);
            TARGET_DATA_TYPE = GetString(TARGET_DATA_TYPE); TARGET_SERVER = GetString(TARGET_SERVER);
            ALIGN_MODE = GetString(ALIGN_MODE); MATCH_LOWER_LIMIT = GetString(MATCH_LOWER_LIMIT);
            ERROR_UPPER_LIMIT = GetString(ERROR_UPPER_LIMIT); MATCH_PARAMETER = GetString(MATCH_PARAMETER);
            SUBMIT_BY = GetString(SUBMIT_BY); OUT_BOUND = GetString(OUT_BOUND);
            REMARK = GetString(REMARK); TASK_STATUS = GetString(TASK_STATUS);
            string sql = string.Format(@"insert into gdna.Align_Queue(ID, ALIGN_NO, GENE_ID, ANOTHER_GENE_ID, GENE_CATEGORY
            , GENE_TYPE, TASK_TYPE, GENE_ORIGIN, GENE_SERVER, DATA_TYPE, TARGET_DATA_TYPE
            , TARGET_SERVER, ALIGN_MODE, MATCH_LOWER_LIMIT
            , ERROR_UPPER_LIMIT, MATCH_PARAMETER, SUBMIT_BY, OUT_BOUND, REMARK,TASK_STATUS,CREATE_USER,SUBMIT_DATE,CREATE_DATETIME) VALUES({0},{1},{2},{3},{4},{5},{6},
              {7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{16},SYSDATE,SYSDATE);",
            ID, ALIGN_NO, GENE_ID, ANOTHER_GENE_ID, GENE_CATEGORY
            , GENE_TYPE, TASK_TYPE, GENE_ORIGIN, GENE_SERVER, DATA_TYPE, TARGET_DATA_TYPE
            , TARGET_SERVER, ALIGN_MODE, MATCH_LOWER_LIMIT
            , ERROR_UPPER_LIMIT, MATCH_PARAMETER, SUBMIT_BY, OUT_BOUND, REMARK, TASK_STATUS);
            return sql;
        }
        void InsertAQ_Normal(string GENE_ID, string category, string user, OracleConnection dbConnection, OracleTransaction trans)
        {
            DBHelperOracle.ExecuteSqlWithTransaction(InsertAQ(Helper.GenerateID(), GENE_ID, string.Empty, category, "1", "1", "0", DBHelperOracle.INIT_SERVER_NO, "0", "2",
                DBHelperOracle.INIT_SERVER_NO, "1", "8", "2",
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?><compareInfo alignCategory=\"1,2,3,4,5,6,8,10,11,13,14,15\"/>", user, "0", "0", "入库自动比对"),
                dbConnection, trans);
            DBHelperOracle.ExecuteSqlWithTransaction(InsertAQ(Helper.GenerateID(), GENE_ID, string.Empty, category, "1", "1", "0", DBHelperOracle.INIT_SERVER_NO, "0", "2",
               DBHelperOracle.INIT_SERVER_NO, "2", "8", "2",
               "<?xml version=\"1.0\" encoding=\"UTF-8\"?><compareInfo alignCategory=\"7,9,16,17\"/>", user, "0", "0", "入库自动比对"),
                dbConnection, trans);
        }
        void InsertAQ_CaseR(string id1, string id2, string category, string user, string relation, string caseId, OracleConnection dbConnection, OracleTransaction trans)
        {
            string alMode = GetALMODE(relation);
            if (alMode.Equals("6"))
            {
                DBHelperOracle.ExecuteSqlWithTransaction(InsertAQ(Helper.GenerateID(), id1, id2, category, "1", "2", "0", DBHelperOracle.INIT_SERVER_NO, "0", "2",
            DBHelperOracle.INIT_SERVER_NO, alMode, "11", "2",
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?><compareInfo alignCategory=\"1,3,2,14,15,5\" caseId=\"" + caseId + "\"/>", user, "0", "0", "同案比对"),
             dbConnection, trans);
            }
            else if (alMode.Equals("3") || alMode.Equals("4") || alMode.Equals("5"))
            {
                DBHelperOracle.ExecuteSqlWithTransaction(InsertAQ(Helper.GenerateID(), id1, id2, category, "1", "1", "0", DBHelperOracle.INIT_SERVER_NO, "0", "2",
           DBHelperOracle.INIT_SERVER_NO, alMode, "11", "2",
           "<?xml version=\"1.0\" encoding=\"UTF-8\"?><compareInfo alignCategory=\"1,2,3,4,5,6,7,8,9,10,11,15,16,17\"/>", user, "0", "0", "入库自动比对"),
            dbConnection, trans);
                DBHelperOracle.ExecuteSqlWithTransaction(InsertAQ(Helper.GenerateID(), id1, id2, category, "1", "2", "0", DBHelperOracle.INIT_SERVER_NO, "0", "2",
           DBHelperOracle.INIT_SERVER_NO, alMode, "11", "2",
           "<?xml version=\"1.0\" encoding=\"UTF-8\"?><compareInfo alignCategory=\"1,3,2,14,15,5\" caseId=\"" + caseId + "\"/>", user, "0", "0", "同案比对"),
            dbConnection, trans);
            }
        }
        string GetALMODE(string relation)
        {
            switch (relation)
            {
                case "1": return "3";
                case "2": return "4";
                case "3": return "5";
                case "4": return "6";
            }
            return string.Empty;
        }
        string GetString(string str)
        {
            str = str.Trim();
            str = str.Replace("'", "''");
            str = str.Length > 0 ? "'" + str + "'" : "NULL";
            return str;
        }
        string GetTable(string SAMPLE_CATEGORY)
        {
            switch (SAMPLE_CATEGORY)
            {
                case "1"://现场物证
                    return "SCENE_EVIDENCE";
                case "2"://案件受害人	
                case "3"://案件嫌疑人	
                case "15"://案件其他人员	
                    return "CASE_PERSONNEL_SAMPLE";
                case "6":
                    return "MISSING_PERSON";
                case "7"://失踪人员亲属	
                case "16"://嫌疑人亲属	
                case "17"://受害人亲属	
                    return "RELATIVE";
                case "5"://未知名尸体	
                    return "UNKNOWN_DECEASED";
                default:
                    return string.Empty;
            }
        }
        static int Anal(string src, ref string AMELOGENIN_1, ref string AMELOGENIN_2)
        {
            if (src.Contains("OL"))
            {
                return 2;
            }
            string[] strs = src.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (strs.Length == 1)
            {
                if (strs[0].Length > 4) return 2;
                AMELOGENIN_1 = strs[0];
                AMELOGENIN_2 = strs[0];
                return 1;
            }
            else if (strs.Length == 2)
            {
                if ((strs[0].Length > 4) || (strs[1].Length > 4)) return 2;
                AMELOGENIN_1 = strs[0];
                AMELOGENIN_2 = strs[1];
                return 1;
            }
            else if (strs.Length > 2)
            {
                return 2;
            }
            return 0;
        }
        #endregion
        [WebMethod]
        public string QuerySample(string 鉴定单位, string 样本编号, string 样本名称, string 样本类型, string 库类型,
            string 受理时间s, string 受理时间e, string 一检人,
            string STR_FLAG, string YSTR_FLAG, string IMP_FLAG,
            string 检验状态, string 预试验, string 确证试验,
            string pageSize, string pageIndex)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (样本编号.Length > 0) filter += "样本编号='" + 样本编号 + "' and ";
            if (样本名称.Length > 0) filter += "名称 like '%" + 样本名称 + "%' and ";

            if (库类型.Length > 0) filter += "库类型='" + 库类型 + "' and ";
            if (一检人.Length > 0) filter += "一检人='" + 一检人 + "' and ";
            if (STR_FLAG.Length > 0) filter += "STR_FLAG='" + STR_FLAG + "' and ";
            if (YSTR_FLAG.Length > 0) filter += "YSTR_FLAG='" + YSTR_FLAG + "' and ";
            if (IMP_FLAG.Length > 0) filter += "IMP_FLAG='" + IMP_FLAG + "' and ";
            if (受理时间s.Length > 0) filter += "受理时间>='" + 受理时间s + "' and ";
            if (受理时间e.Length > 0) filter += "受理时间<='" + 受理时间e + "' and ";
            if (样本类型.Length > 0)
            {
                string[] yblxx = 样本类型.Split('，');
                string yblxstr = string.Empty;
                foreach (string syblx in yblxx) if (syblx.Length > 0) yblxstr += "样本类型='" + syblx + "' or ";
                if (yblxstr.Length > 0) filter += "(" + yblxstr.Substring(0, yblxstr.Length - 4) + ") and ";
            }
            if (检验状态.Length > 0)
            {
                string[] yblxx = 检验状态.Split('，');
                string yblxstr = string.Empty;
                foreach (string syblx in yblxx) if (syblx.Length > 0) yblxstr += "检验状态='" + syblx + "' or ";
                if (yblxstr.Length > 0) filter += "(" + yblxstr.Substring(0, yblxstr.Length - 4) + ") and ";
            }
            if (预试验.Length > 0) filter += "预试验='" + 预试验 + "' and ";
            if (确证试验.Length > 0) filter += "确证试验='" + 确证试验 + "' and ";

            return DBHelperSQL.SelectRowCount("样本视图", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }

        [WebMethod]
        public string PrintCODISdat(string 鉴定单位, string fileName, string GeneMapper, string RecordType, string IDs)
        {
            //分离传送来的样本编号
            string[] SLNids = IDs.Split(new char[] { '，' }, StringSplitOptions.RemoveEmptyEntries);
            if (SLNids.Length == 0)
            {
                return "传入的样本编号不能为空值！";
            }
            //得到现在时间值
            string now = DateTime.Now.GetDateTimeFormats('r')[0].ToString();
            string[] times = now.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string nowTime = times[1] + "-" + times[2] + "-" + times[3] + "  " + times[4];
            //初始化文件
            string time = DateTime.Today.ToShortDateString();
            string tmpfilename = GeneMapper + "_" + RecordType + "_" + time + ".txt";
            string filenamelast = tmpfilename;
            string templatePath = ConfigurationManager.AppSettings["WebPath"] + @"STR\CODIS.dat";
            string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
            Helper.CheckDir(wordDir);
            string wordPath = wordDir + tmpfilename;
            int ikk = 1;
            while (System.IO.File.Exists(wordPath))
            {
                string caa = string.Empty;
                caa = ("_" + (ikk++).ToString());
                filenamelast = tmpfilename.Insert(tmpfilename.Length - 4, caa);
                wordPath = wordDir + filenamelast;
            }
            if (System.IO.File.Exists(templatePath))
            {
                System.IO.File.Copy(templatePath, wordPath);
            }
            else
            {
                return "找不到文书模版";
            }
            FileStream fs = new FileStream(wordPath, FileMode.Open);
            StreamWriter writer = new StreamWriter(fs);
            try
            {
                //得到各样本str的值
                string filter = string.Empty;
                for (int i = 0; i < SLNids.Length; i++)
                {
                    filter += "样本编号 ='" + SLNids[i] + "' or ";
                }
                filter = "(" + filter.Substring(0, filter.Length - 4) + ") and 鉴定单位='" + 鉴定单位 + "'";
                DataSet ds = DBHelperSQL.Select("样本视图", filter, "样本编号", "*");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return "样本基因不存在！";
                }
                //写入文件头部
                writer.WriteLine("1.0");
                writer.WriteLine("1");
                writer.WriteLine("IMPORT");
                writer.WriteLine("srclab");
                writer.WriteLine("destlab");
                writer.WriteLine(nowTime);
                writer.WriteLine("AB");
                if (GeneMapper.Equals("3500"))
                {
                    writer.WriteLine("GeneMapper ID-X");
                }
                else
                {
                    writer.WriteLine("GeneMapper");
                }
                writer.WriteLine(SLNids.Length.ToString());
                //设置选择的基因座
                string[] str = new string[0] { };
                if (RecordType.Equals("ID-Plus"))
                {
                    str = new string[16] { "D8S1179", "D21S11", "D7S820", "CSF1PO", "D3S1358", "TH01", "D13S317", "D16S539", "D2S1338", "D19S433", "vWA", "TPOX", "D18S51", "AMEL", "D5S818", "FGA" };
                }
                else if (RecordType.Equals("PP21"))
                {
                    str = new string[21] { "AMEL", "D3S1358", "D1S1656", "D6S1043", "D13S317", "Penta E", "D16S539", "D18S51", "D2S1338", "CSF1PO", "Penta D", "TH01", "vWA", "D21S11", "D7S820", "D5S818", "TPOX", "D8S1179", "D12S391", "D19S433", "FGA" };
                }
                else if (RecordType.Equals("SINO-filer"))
                {
                    str = new string[16] { "D8S1179", "D21S11", "D7S820", "CSF1PO", "D3S1358", "D5S818", "D13S317", "D16S539", "D2S1338", "D19S433", "vWA", "D12S391", "D18S51", "AMEL", "D6S1043", "FGA" };
                }
                else if (RecordType.Equals("Y-filer"))
                {
                    str = new string[16] { "B_DYS456", "B_DYS389I", "B_DYS390", "B_DYS389II", "G_DYS458", "G_DYS19", "G_DYS385", "Y_DYS393", "Y_DYS391", "Y_DYS439", "Y_DYS635", "Y_DYS392", "R_Y_GATA_H4", "R_DYS437", "R_DYS438", "R_DYS448" };
                }
                //写入文件str部分
                if (str.Length == 0)
                {
                    return "基因座错误！";
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    writer.WriteLine("DNA Analysis Result");
                    writer.WriteLine("1.0");
                    writer.WriteLine("PCR");
                    writer.WriteLine(dr["样本编号"].ToString());
                    writer.WriteLine("0");
                    writer.WriteLine("Suspect, Known");
                    writer.WriteLine("unspecified");
                    writer.WriteLine("unspecified");
                    writer.WriteLine("unspecified");
                    writer.WriteLine(str.Length.ToString());
                    for (int j = 0; j < str.Length; j++)
                    {
                        writer.WriteLine(str[j]);
                        writer.WriteLine("1");
                        if (GeneMapper.Equals("3130"))
                        {
                            writer.WriteLine("gmid");
                        }
                        else if (GeneMapper.Equals("3500"))
                        {
                            writer.WriteLine("gmidx");
                        }
                        writer.WriteLine(nowTime);
                        writer.WriteLine(times[4] + ".0");
                        string col = string.Empty;
                        if (str[j].ToString().Equals("Penta E"))
                        {
                            col = dr["Penta_E"].ToString();
                        }
                        else if (str[j].ToString().Equals("Penta D"))
                        {
                            col = dr["Penta_D"].ToString();
                        }
                        else
                        {
                            col = dr[str[j]].ToString();
                        }
                        string[] strs = col.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        if (strs.Length == 0)
                        {
                            return "导出CODIS失败：" + str[j] + "为空值！";
                        }
                        writer.WriteLine(strs.Length.ToString());
                        foreach (string s in strs)
                        {
                            writer.WriteLine(s);
                        }
                    }
                }
                writer.Close();
                tmpfilename = filenamelast;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                fs.Close();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\" + tmpfilename;
        }
    }
}
