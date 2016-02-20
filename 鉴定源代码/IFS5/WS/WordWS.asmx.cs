using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Configuration;
using LIB;
using DAL;
using System.Collections.Generic;
using Microsoft.Office.Interop.Word;

namespace WS
{
    /// <summary>
    /// Summary description for WordWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class WordWS : System.Web.Services.WebService
    {
        #region 查询与删除
        [WebMethod]
        public string GetCaseWordList(string 鉴定单位, string 委托编号, string 鉴定类别, string wordTypeArg)
        {
            DataSet ds = new DataSet("NewDataSet");
            DataTable dt = new DataTable("tableName"); ds.Tables.Add(dt);
            DataColumn dc = new DataColumn("WordType", typeof(string)); dt.Columns.Add(dc);//分类
            dc = new DataColumn("FileName", typeof(string)); dt.Columns.Add(dc);//文件名，用于显示
            dc = new DataColumn("TemplatePath", typeof(string)); dt.Columns.Add(dc);//模板位置
            dc = new DataColumn("WordDir", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("CreationTime", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("LastWriteTime", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OpenWordUrl", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("CONNO", typeof(string)); dt.Columns.Add(dc);

            string[] wordTypeList = null;
            if (wordTypeArg.Length == 0) wordTypeList = new string[] { "委托书", "受理书", "意见报告书", "检验检查记录", "封皮", "其他" };
            else wordTypeList = wordTypeArg.Split('-');

            foreach (string wordType in wordTypeList)
            {
                string tmpPath = ConfigurationManager.AppSettings["WebPath"] + 鉴定单位 + "\\文书模版\\" + 鉴定类别 + "\\" + wordType;
                Helper.CheckDir(tmpPath);
                DirectoryInfo dirInfo = new DirectoryInfo(tmpPath);
                FileInfo[] files = dirInfo.GetFiles("*.doc", SearchOption.TopDirectoryOnly);
                foreach (FileInfo file in files)
                {
                    string wordDir = ConfigurationManager.AppSettings["WebPath"] + 鉴定单位 + "\\鉴定档案\\" + 委托编号 + "\\文书记录\\";
                    FileInfo fileTar = new FileInfo(wordDir + file.Name);

                    DataRow dr = dt.NewRow();
                    dr["WordType"] = wordType;
                    dr["FileName"] = file.Name;
                    dr["TemplatePath"] = file.FullName;
                    dr["WordDir"] = wordDir;
                    dr["CreationTime"] = "创建时间：" + (fileTar.Exists ? fileTar.CreationTime.ToString() : "尚未创建");
                    dr["LastWriteTime"] = "最后修改：" + (fileTar.Exists ? fileTar.LastWriteTime.ToString() : "尚未创建");
                    dr["OpenWordUrl"] = ConfigurationManager.AppSettings["ServerAddr"] + 鉴定单位 + "/鉴定档案/" + 委托编号 + "/文书记录/" + file.Name;
                    dr["CONNO"] = 委托编号;

                    dt.Rows.Add(dr);
                }
            }
            return ds.GetXml();
        }
        [WebMethod]
        public string DeleteWord(string wordDir, string wordName)
        {
            try
            {
                string wordPath = wordDir + wordName;
                if (File.Exists(wordPath))
                    File.Delete(wordPath);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "1";
        }
        [WebMethod]
        public string GetAllCaseWord(string 鉴定单位, string 委托编号)
        {
            DataSet ds = new DataSet("NewDataSet");
            DataTable dt = new DataTable("tableName"); ds.Tables.Add(dt);
            DataColumn dc = new DataColumn("WordType", typeof(string)); dt.Columns.Add(dc);//分类
            dc = new DataColumn("FileName", typeof(string)); dt.Columns.Add(dc);//文件名，用于显示
            dc = new DataColumn("TemplatePath", typeof(string)); dt.Columns.Add(dc);//模板位置
            dc = new DataColumn("WordDir", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("CreationTime", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("LastWriteTime", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OpenWordUrl", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("CONNO", typeof(string)); dt.Columns.Add(dc);

            string tmpPath = ConfigurationManager.AppSettings["WebPath"] + 鉴定单位 + "\\鉴定档案\\" + 委托编号 + "\\文书记录\\";
            Helper.CheckDir(tmpPath);
            DirectoryInfo dirInfo = new DirectoryInfo(tmpPath);
            FileInfo[] files = dirInfo.GetFiles("*.doc", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                DataRow dr = dt.NewRow();
                dr["FileName"] = file.Name;
                dr["WordDir"] = tmpPath;
                dr["CreationTime"] = "创建时间：" + file.CreationTime.ToString();
                dr["LastWriteTime"] = "最后修改：" + file.LastWriteTime.ToString();
                dr["OpenWordUrl"] = ConfigurationManager.AppSettings["ServerAddr"] + 鉴定单位 + "/鉴定档案/" + 委托编号 + "/文书记录/" + file.Name;
                dr["CONNO"] = 委托编号;

                dt.Rows.Add(dr);
            }

            return ds.GetXml();
        }
        [WebMethod]
        public string GetCaseWordManageList(string 鉴定单位, string 鉴定类别, string wordTypeArg)
        {
            DataSet ds = new DataSet("NewDataSet");
            DataTable dt = new DataTable("tableName"); ds.Tables.Add(dt);
            DataColumn dc = new DataColumn("FileName", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("WordDir", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("OpenWordUrl", typeof(string)); dt.Columns.Add(dc);

            string tmpPath = ConfigurationManager.AppSettings["WebPath"] + 鉴定单位 + "\\文书模版\\" + 鉴定类别 + "\\" + wordTypeArg + "\\";
            Helper.CheckDir(tmpPath);
            DirectoryInfo dirInfo = new DirectoryInfo(tmpPath);
            FileInfo[] files = dirInfo.GetFiles("*.doc", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                DataRow dr = dt.NewRow();
                dr["FileName"] = file.Name;
                dr["WordDir"] = tmpPath;
                dr["OpenWordUrl"] = ConfigurationManager.AppSettings["ServerAddr"] + 鉴定单位 + "/文书模版/" + 鉴定类别 + "/" + wordTypeArg + "/" + file.Name;

                dt.Rows.Add(dr);
            }

            return ds.GetXml();
        }
        #endregion
        #region 操作word
        [WebMethod]
        public string GenerateWord(string templatePath, string wordDir, string wordName, string 委托编号, string status, string isTesNote)
        {
            Helper.CheckDir(wordDir);
            string wordPath = wordDir + wordName;

            if (System.IO.File.Exists(wordPath))
            {
                return "1";
            }
            else
            {
                if (System.IO.File.Exists(templatePath))
                {
                    System.IO.File.Copy(templatePath, wordPath);
                }
                else
                {
                    return "找不到文书模版";
                }

                FillWord fw = new FillWord(wordPath);
                fw.OpenDoc();
                if (fw.m_openedDoc == null)
                {
                    return "生成文书失败";
                }
                try
                {
                    if (wordName.Equals("DNA检验个案汇总表.doc"))
                    {

                        fw.FillBookMarks(printGAHZ(委托编号));
                    }
                    else
                    {
                        DataReader.CaseFill(委托编号, fw, status, isTesNote, wordName);
                        if (wordName.Contains("DNA鉴定书") || wordName.Contains("DNA检验鉴定报告") || wordName.Contains("DNA检验意见书"))
                        {
                            //填充str表格
                            DataSet strds = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", "样本编号", "*");
                            if (委托编号.StartsWith("D"))
                            {
                                DataSet ajds = DBHelperSQL.Select("案件信息", "委托编号='" + 委托编号 + "'", "", "*");
                                strds = DBHelperSQL.Select("样本视图", "案件ID='" + ajds.Tables[0].Rows[0]["SRCID"].ToString() + "'", "样本编号", "*");
                            }

                            if (wordName.Contains("ID-plus")) fw.Fill鉴定书_ID_plus(strds);
                            //else if (wordName.Contains("PP21")) fw.Fill鉴定书_PP21(strds);
                            //else if (wordName.Contains("sinofiler")) fw.Fill鉴定书_SinoFiler(strds);
                            //else if (wordName.Contains("YFiler")) fw.Fill鉴定书_YFiler(strds);
                            else fw.FillDNAStr(strds.Tables[0]);
                        }
                        else if (wordName.Equals("检材描述.doc"))
                        {
                            DataSet strds = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", "样本编号", "*");
                            fw.Fill检材描述(strds);
                        }
                        else if (wordName.Equals("DNA_STR检验结果报告单(PP21 System).doc"))
                        {
                            DataSet strds = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", "样本编号", "*");
                            fw.Fill检验结果报告单PP21(strds);
                        }
                        else if (wordName.Equals("DNA_STR检验结果报告单(sinofiler-Kit).doc"))
                        {
                            DataSet strds = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", "样本编号", "*");
                            fw.Fill检验结果报告单sinofiler(strds);
                        }
                        //大连STR记录表
                        else if (wordName.Equals("ID-plus基因型分析记录表.doc"))
                        {
                            DataSet strds = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", "样本编号", "*");
                            foreach (DataRow dr in strds.Tables[0].Rows)
                            {
                                dr["样本编号"] = dr["样本编号"].ToString().Substring(dr["样本编号"].ToString().Length - 5, 5);
                            }
                            IDictionary<string, string> dict = new Dictionary<string, string>();
                            dict.Add("样本编号", "1-1-1");
                            dict.Add("AMEL", "1-1-2");
                            dict.Add("D8S1179", "1-1-3");
                            dict.Add("D21S11", "1-1-4");
                            dict.Add("D7S820", "1-1-5");
                            dict.Add("CSF1PO", "1-1-6");
                            dict.Add("D3S1358", "1-1-7");
                            dict.Add("TH01", "1-1-8");
                            dict.Add("D13S317", "1-1-9");
                            dict.Add("D16S539", "1-1-10");
                            dict.Add("D2S1338", "1-1-11");
                            dict.Add("D19S433", "1-1-12");
                            dict.Add("vWA", "1-1-13");
                            dict.Add("TPOX", "1-1-14");
                            dict.Add("D18S51", "1-1-15");
                            dict.Add("D5S818", "1-1-16");
                            dict.Add("FGA", "1-1-17");
                            fw.FillTable(strds, dict, 0, 0, 0);
                        }
                        else if (wordName.Equals("PP21基因型分析记录表.doc"))
                        {
                            DataSet strds = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", "样本编号", "*");
                            foreach (DataRow dr in strds.Tables[0].Rows)
                            {
                                dr["样本编号"] = dr["样本编号"].ToString().Substring(dr["样本编号"].ToString().Length - 5, 5);
                            }
                            IDictionary<string, string> dict = new Dictionary<string, string>();
                            dict.Add("样本编号", "1-1-1");
                            dict.Add("AMEL", "1-1-2");
                            dict.Add("D3S1358", "1-1-3");
                            dict.Add("D1S1656", "1-1-4");
                            dict.Add("D6S1043", "1-1-5");
                            dict.Add("D13S317", "1-1-6");
                            dict.Add("Penta_E", "1-1-7");
                            dict.Add("D16S539", "1-1-8");
                            dict.Add("D18S51", "1-1-9");
                            dict.Add("D2S1338", "1-1-10");
                            dict.Add("CSF1PO", "1-1-11");
                            dict.Add("Penta_D", "1-1-12");
                            dict.Add("TH01", "1-1-13");
                            dict.Add("vWA", "1-1-14");
                            dict.Add("D21S11", "1-1-15");
                            dict.Add("D7S820", "1-1-16");
                            dict.Add("D5S818", "1-1-17");
                            dict.Add("TPOX", "1-1-18");
                            dict.Add("D8S1179", "1-1-19");
                            dict.Add("D12S391", "1-1-20");
                            dict.Add("D19S433", "1-1-21");
                            dict.Add("FGA", "1-1-22");
                            fw.FillTable(strds, dict, 0, 0, 0);
                        }
                        else if (wordName.Equals("SinoFiler基因型分析记录表.doc"))
                        {
                            DataSet strds = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", "样本编号", "*");
                            foreach (DataRow dr in strds.Tables[0].Rows)
                            {
                                dr["样本编号"] = dr["样本编号"].ToString().Substring(dr["样本编号"].ToString().Length - 5, 5);
                            }
                            IDictionary<string, string> dict = new Dictionary<string, string>();
                            dict.Add("样本编号", "1-1-1");
                            dict.Add("AMEL", "1-1-2");
                            dict.Add("D8S1179", "1-1-3");
                            dict.Add("D21S11", "1-1-4");
                            dict.Add("D7S820", "1-1-5");
                            dict.Add("CSF1PO", "1-1-6");
                            dict.Add("D3S1358", "1-1-7");
                            dict.Add("D5S818", "1-1-8");
                            dict.Add("D13S317", "1-1-9");
                            dict.Add("D16S539", "1-1-10");
                            dict.Add("D2S1338", "1-1-11");
                            dict.Add("D19S433", "1-1-12");
                            dict.Add("vWA", "1-1-13");
                            dict.Add("D12S391", "1-1-14");
                            dict.Add("D18S51", "1-1-15");
                            dict.Add("D6S1043", "1-1-16");
                            dict.Add("FGA", "1-1-17");
                            fw.FillTable(strds, dict, 0, 0, 0);
                        }
                        else if (wordName.Equals("YFiler基因型分析记录表.doc"))
                        {
                            DataSet strds = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", "样本编号", "*");
                            foreach (DataRow dr in strds.Tables[0].Rows)
                            {
                                dr["样本编号"] = dr["样本编号"].ToString().Substring(dr["样本编号"].ToString().Length - 5, 5);
                            }
                            IDictionary<string, string> dict = new Dictionary<string, string>();
                            dict.Add("样本编号", "1-1-1");
                            dict.Add("B_DYS456", "1-1-2");
                            dict.Add("B_DYS389I", "1-1-3");
                            dict.Add("B_DYS390", "1-1-4");
                            dict.Add("B_DYS389II", "1-1-5");
                            dict.Add("G_DYS458", "1-1-6");
                            dict.Add("G_DYS19", "1-1-7");
                            dict.Add("G_DYS385", "1-1-8");
                            dict.Add("Y_DYS393", "1-1-9");
                            dict.Add("Y_DYS391", "1-1-10");
                            dict.Add("Y_DYS439", "1-1-11");
                            dict.Add("Y_DYS635", "1-1-12");
                            dict.Add("Y_DYS392", "1-1-13");
                            dict.Add("R_Y_GATA_H4", "1-1-14");
                            dict.Add("R_DYS437", "1-1-15");
                            dict.Add("R_DYS438", "1-1-16");
                            dict.Add("R_DYS448", "1-1-17");
                            fw.FillTable(strds, dict, 0, 0, 0);
                        }
                        else if (wordName.Equals("Globalfiler基因型分析记录表.doc"))
                        {
                            DataSet strds = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", "样本编号", "*");
                            foreach (DataRow dr in strds.Tables[0].Rows)
                            {
                                dr["样本编号"] = dr["样本编号"].ToString().Substring(dr["样本编号"].ToString().Length - 5, 5);
                            }
                            IDictionary<string, string> dict = new Dictionary<string, string>();
                            dict.Add("样本编号", "1-1-1");
                            dict.Add("AMEL", "1-1-2");
                            dict.Add("D3S1358", "1-1-3");
                            dict.Add("vWA", "1-1-4");
                            dict.Add("D16S539", "1-1-5");
                            dict.Add("CSF1PO", "1-1-6");
                            dict.Add("TPOX", "1-1-7");
                            dict.Add("Y_indel", "1-1-8");
                            dict.Add("D8S1179", "1-1-9");
                            dict.Add("D21S11", "1-1-10");
                            dict.Add("D18S51", "1-1-11");
                            dict.Add("Y_DYS391", "1-1-12");
                            dict.Add("D2S441", "1-1-13");
                            dict.Add("D19S433", "1-1-14");
                            dict.Add("TH01", "1-1-15");
                            dict.Add("FGA", "1-1-16");
                            dict.Add("D22S1045", "1-1-17");
                            dict.Add("D5S818", "1-1-18");
                            dict.Add("D13S317", "1-1-19");
                            dict.Add("D7S820", "1-1-20");
                            dict.Add("SE33", "1-1-21");
                            dict.Add("D10S1248", "1-1-22");
                            dict.Add("D1S1656", "1-1-23");
                            dict.Add("D12S391", "1-1-24");
                            dict.Add("D2S1338", "1-1-25");
                            fw.FillTable(strds, dict, 0, 0, 0);
                        }
                        else if (wordName.Equals("PPFusion基因型分析记录表.doc"))
                        {
                            DataSet strds = DBHelperSQL.Select("样本视图", "委托编号='" + 委托编号 + "'", "样本编号", "*");
                            foreach (DataRow dr in strds.Tables[0].Rows)
                            {
                                dr["样本编号"] = dr["样本编号"].ToString().Substring(dr["样本编号"].ToString().Length - 5, 5);
                            }
                            IDictionary<string, string> dict = new Dictionary<string, string>();
                            dict.Add("样本编号", "1-1-1");
                            dict.Add("AMEL", "1-1-2");
                            dict.Add("D3S1358", "1-1-3");
                            dict.Add("D1S1656", "1-1-4");
                            dict.Add("D2S441", "1-1-5");
                            dict.Add("D10S1248", "1-1-6");
                            dict.Add("D13S317", "1-1-7");
                            dict.Add("Penta_E", "1-1-8");
                            dict.Add("D16S539", "1-1-9");
                            dict.Add("D18S51", "1-1-10");
                            dict.Add("D2S1338", "1-1-11");
                            dict.Add("CSF1PO", "1-1-12");
                            dict.Add("Penta_D", "1-1-13");
                            dict.Add("TH01", "1-1-14");
                            dict.Add("vWA", "1-1-15");
                            dict.Add("D21S11", "1-1-16");
                            dict.Add("D7S820", "1-1-17");
                            dict.Add("D5S818", "1-1-18");
                            dict.Add("TPOX", "1-1-19");
                            dict.Add("Y_DYS391", "1-1-20");
                            dict.Add("D8S1179", "1-1-21");
                            dict.Add("D12S391", "1-1-22");
                            dict.Add("D19S433", "1-1-23");
                            dict.Add("FGA", "1-1-24");
                            dict.Add("D22S1045", "1-1-25");
                            fw.FillTable(strds, dict, 0, 0, 0);
                        }
                        else if (wordName.Equals("STR检验记录表.doc"))
                        {
                            DataSet strds1 = DBHelperSQL.Select("确证试验,系统用户", "确证试验.试验人=系统用户.ID and 委托编号='" + 委托编号 + "'", "样本编号", "确证试验.*,系统用户.姓名 as 检验人姓名");
                            DataSet strds2 = DBHelperSQL.Select("提取视图", "委托编号='" + 委托编号 + "'", "样本编号,日期 desc,ID desc", "*");
                            DataSet strds3 = DBHelperSQL.Select("扩增电泳视图", "委托编号='" + 委托编号 + "'", "样本编号,扩增时间开始 desc,ID desc", "*");
                            for (int i = 0; i < (strds2.Tables[0].Rows.Count - 1); i++)
                            {
                                for (int j = i + 1; j < strds2.Tables[0].Rows.Count; j++)
                                {
                                    if (strds2.Tables[0].Rows[i]["样本编号"].ToString().Equals(strds2.Tables[0].Rows[j]["样本编号"].ToString()))
                                    {
                                        strds2.Tables[0].Rows.RemoveAt(j); j--;
                                    }
                                }

                            }
                            for (int i = 0; i < (strds3.Tables[0].Rows.Count - 1); i++)
                            {
                                for (int j = i + 1; j < strds3.Tables[0].Rows.Count; j++)
                                {
                                    if (strds3.Tables[0].Rows[i]["样本编号"].ToString().Equals(strds3.Tables[0].Rows[j]["样本编号"].ToString()))
                                    {
                                        strds3.Tables[0].Rows.RemoveAt(j); j--;
                                    }
                                }

                            }
                            fw.FillSTR检验记录表(strds1, strds2, strds3, fw);
                        }

                    }
                    fw.Save();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    fw.CloseWord();
                }
            }
            return "1";
        }

        [WebMethod]
        public string GenerateWzms(string PSBID, string JUSTYPE, string sln, string 委托编号)
        {
            string tmp = ConfigurationManager.AppSettings["WebPath"] + PSBID + "\\文书模版\\" + JUSTYPE + "\\物证描述\\物证描述.doc";
            string file = ConfigurationManager.AppSettings["WebPath"] + PSBID + "\\鉴定档案\\" + 委托编号 + "\\物证描述\\";
            string picPath = ConfigurationManager.AppSettings["WebPath"] + PSBID + "\\鉴定档案\\" + 委托编号 + "\\物证照片\\" + sln + ".jpg";

            if (!File.Exists(picPath))
            {
                return "照片不存在";
            }

            Helper.CheckDir(file);
            file += sln + ".doc";

            if (System.IO.File.Exists(file))
            {
                return "1";
            }
            else
            {
                if (System.IO.File.Exists(tmp))
                {
                    System.IO.File.Copy(tmp, file);
                }
                else
                {
                    return "找不到文书模版";
                }

                FillWord fw = new FillWord(file);
                fw.OpenDoc();
                if (fw.m_openedDoc == null)
                {
                    return "生成文书失败";
                }
                try
                {
                    fw.AddPicMarks(picPath);
                    fw.Save();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    fw.CloseWord();
                }
            }
            return "1";
        }
        [WebMethod]
        public string FillFWJL(DataSet ds, string filename)
        {
            string templatePath = ConfigurationManager.AppSettings["WebPath"] + "1\\文书模版\\DNA\\检验检查记录\\JGWJDNA-24-DNA专业鉴定文书发文记录.doc";
            string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
            string openPanth = ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp/" + filename + ".doc";
            Helper.CheckDir(wordDir);
            string wordPath = wordDir + filename + ".doc";

            if (System.IO.File.Exists(wordPath))
            {
                return "1";
            }
            else
            {
                if (System.IO.File.Exists(templatePath))
                {
                    System.IO.File.Copy(templatePath, wordPath);
                }
                else
                {
                    return "找不到文书模版";
                }

                FillWord fw = new FillWord(wordPath);
                fw.OpenDoc();
                if (fw.m_openedDoc == null)
                {
                    return "生成文书失败";
                }
                try
                {

                    fw.PrintDNA专业鉴定文书发文记录(ds);

                    fw.Save();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    fw.CloseWord();
                }
            }
            return openPanth;
        }
        #endregion
        #region 样本检验记录表
        [WebMethod]
        public string PrintCaseTestRecord(string 鉴定单位, string fileName, string fileType, string RecordType, string 记录ID, string 委托编号)
        {
            string templatePath = ConfigurationManager.AppSettings["WebPath"] + @"\" + 鉴定单位 + @"\DNA汇总表\" + fileName;
            string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"\" + 鉴定单位 + @"\鉴定档案\" + 委托编号 + @"\文书记录\";
            Helper.CheckDir(wordDir);
            string Finalname = fileName;
            string wordPath = wordDir + Finalname;
            string caa = string.Empty;
            int ikk = 1;
            while (System.IO.File.Exists(wordPath))
            {
                caa = ("-" + (ikk++).ToString());
                Finalname = fileName.Insert(fileName.Length - 4, caa);
                wordPath = wordDir + Finalname;
            }

            if (System.IO.File.Exists(templatePath))
            {
                System.IO.File.Copy(templatePath, wordPath);
            }
            else
            {
                return "找不到文书模版";
            }

            FillWord fw = new FillWord(wordPath);
            fw.OpenDoc();
            if (fw.m_openedDoc == null)
            {
                return "生成文书失败";
            }
            try
            {
                if (RecordType.Equals("预试验确证试验记录"))
                {
                    DataSet dataSet = new DataSet("NewDataSet");
                    DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                    DataColumn dc = new DataColumn("案件编号", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("案件名称", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("检验时间", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("一检人", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("二检人", typeof(string)); dataTable.Columns.Add(dc);
                    DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                    DataSet ds = DBHelperSQL.Select("鉴定流程视图", "委托编号='" + 委托编号 + "'", "", "*");
                    DataSet ds0 = DBHelperSQL.Select("样本信息", "委托编号='" + 委托编号 + "' and (预试验<>0 or 确证试验<>0) ", "样本编号,创建时间", "*");
                    DataSet ds1 = DBHelperSQL.Select("预试验", "委托编号='" + 委托编号 + "'", "样本编号,ID", "*");
                    DataSet ds2 = DBHelperSQL.Select("确证试验", "委托编号='" + 委托编号 + "'", "样本编号,ID", "*");

                    dataRow["案件编号"] = ds.Tables[0].Rows[0]["案件编号"].ToString().Length == 0 ? ds.Tables[0].Rows[0]["受理年份"].ToString() + "B" + ds.Tables[0].Rows[0]["案件序号"].ToString() : ds.Tables[0].Rows[0]["案件编号"].ToString();
                    dataRow["案件名称"] = ds.Tables[0].Rows[0]["案件名称"].ToString();
                    if (ds1.Tables[0].Rows.Count > 0)
                        dataRow["检验时间"] = ds1.Tables[0].Rows[0]["日期"].ToString().Substring(0, ds1.Tables[0].Rows[0]["日期"].ToString().Length - 8);
                    else if (ds2.Tables[0].Rows.Count > 0)
                        dataRow["检验时间"] = ds2.Tables[0].Rows[0]["日期"].ToString().Substring(0, ds2.Tables[0].Rows[0]["日期"].ToString().Length - 8);
                    else
                        dataRow["检验时间"] = string.Empty;
                    dataRow["一检人"] = ds.Tables[0].Rows[0]["一检姓名"].ToString();
                    dataRow["二检人"] = ds.Tables[0].Rows[0]["二检姓名"].ToString();
                    fw.FillBookMarks(dataSet);

                    fw.Fill确证试验记录表(ds0, ds1, ds2);
                }
                else if (RecordType.Equals("提取记录"))
                {
                    DataSet dataSet = new DataSet("NewDataSet");
                    DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                    DataColumn dc = new DataColumn("案件编号", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("案件名称", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("提取仪器", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("纯化仪器", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("一检人", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("二检人", typeof(string)); dataTable.Columns.Add(dc);
                    DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                    DataSet ds = DBHelperSQL.Select("鉴定流程视图", "委托编号='" + 委托编号 + "'", "", "*");
                    DataSet ds2 = DBHelperSQL.Select("提取视图", "委托编号='" + 委托编号 + "' and 提取记录ID='" + 记录ID + "'", "ID,样本编号", "*");

                    dataRow["案件编号"] = ds.Tables[0].Rows[0]["案件编号"].ToString().Length == 0 ? ds.Tables[0].Rows[0]["受理年份"].ToString() + "B" + ds.Tables[0].Rows[0]["案件序号"].ToString() : ds.Tables[0].Rows[0]["案件编号"].ToString();
                    dataRow["案件名称"] = ds.Tables[0].Rows[0]["案件名称"].ToString();
                    dataRow["提取仪器"] = ds2.Tables[0].Rows[0]["离心机"].ToString() + ds2.Tables[0].Rows[0]["移液器"].ToString() + ds2.Tables[0].Rows[0]["加热仪"].ToString() +
                                          ds2.Tables[0].Rows[0]["恒温混匀仪"].ToString() + ds2.Tables[0].Rows[0]["漩涡混合器"].ToString() + ds2.Tables[0].Rows[0]["水浴"].ToString() +
                                          ds2.Tables[0].Rows[0]["显微镜"].ToString() + ds2.Tables[0].Rows[0]["工作站"].ToString();
                    dataRow["纯化仪器"] = ds2.Tables[0].Rows[0]["纯化离心机"].ToString() + ds2.Tables[0].Rows[0]["纯化移液器"].ToString() + ds2.Tables[0].Rows[0]["纯化加热仪"].ToString() +
                                          ds2.Tables[0].Rows[0]["纯化恒温混匀仪"].ToString() + ds2.Tables[0].Rows[0]["纯化漩涡混合器"].ToString() + ds2.Tables[0].Rows[0]["纯化水浴"].ToString() +
                                          ds2.Tables[0].Rows[0]["纯化显微镜"].ToString() + ds2.Tables[0].Rows[0]["纯化工作站"].ToString();
                    dataRow["一检人"] = ds.Tables[0].Rows[0]["一检姓名"].ToString();
                    dataRow["二检人"] = ds.Tables[0].Rows[0]["二检姓名"].ToString();
                    fw.FillBookMarks(dataSet);

                    fw.Fill提取记录表(ds2);
                }
                else if (RecordType.Equals("扩增电泳记录"))
                {
                    DataSet dataSet = new DataSet("NewDataSet");
                    DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                    DataColumn dc = new DataColumn("案件编号", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("案件名称", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增仪器", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增参数", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("电泳仪器", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("电泳参数", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("环境条件", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("一检人", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("二检人", typeof(string)); dataTable.Columns.Add(dc);
                    DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                    DataSet ds = DBHelperSQL.Select("鉴定流程视图", "委托编号='" + 委托编号 + "'", "", "*");
                    DataSet ds3 = DBHelperSQL.Select("电泳视图", "(委托编号='" + 委托编号 + "' or 委托编号 is null) and 电泳记录ID='" + 记录ID + "'", "ID,样本编号", "*");
                    DataSet ds2 = DBHelperSQL.Select("扩增视图", "(委托编号='" + 委托编号 + "' or 委托编号 is null) and 扩增记录ID='" + ds3.Tables[0].Rows[0]["对应扩增记录ID"].ToString() + "'", "ID,样本编号", "*");


                    dataRow["案件编号"] = ds.Tables[0].Rows[0]["案件编号"].ToString().Length == 0 ? ds.Tables[0].Rows[0]["受理年份"].ToString() + "B" + ds.Tables[0].Rows[0]["案件序号"].ToString() : ds.Tables[0].Rows[0]["案件编号"].ToString();
                    dataRow["案件名称"] = ds.Tables[0].Rows[0]["案件名称"].ToString();
                    dataRow["扩增仪器"] = "【扩增仪器】" + ds2.Tables[0].Rows[0]["扩增仪"].ToString() + ds2.Tables[0].Rows[0]["扩增离心机"].ToString() + ds2.Tables[0].Rows[0]["扩增漩涡混合器"].ToString() +
                                          ds2.Tables[0].Rows[0]["扩增移液器"].ToString() + ds2.Tables[0].Rows[0]["扩增超净台"].ToString() + ds2.Tables[0].Rows[0]["扩增工作站"].ToString() + "。";
                    dataRow["扩增参数"] = "【扩增参数】" + ds2.Tables[0].Rows[0]["扩增体系"].ToString();

                    dataRow["电泳仪器"] = "【电泳仪器】" + ds3.Tables[0].Rows[0]["电泳仪"].ToString() + ds3.Tables[0].Rows[0]["电泳离心机"].ToString() + ds3.Tables[0].Rows[0]["电泳加热仪"].ToString() +
                                          ds3.Tables[0].Rows[0]["电泳漩涡混合器"].ToString() + ds3.Tables[0].Rows[0]["电泳移液器"].ToString() +
                                          ds3.Tables[0].Rows[0]["电泳制冰机"].ToString() + ds3.Tables[0].Rows[0]["电泳超净台"].ToString() +
                                          ds3.Tables[0].Rows[0]["电泳工作站"].ToString();
                    dataRow["电泳参数"] = "【电泳参数】内标：" + ds3.Tables[0].Rows[0]["内标"].ToString() + " 内标量：" + ds3.Tables[0].Rows[0]["内标量"].ToString() +
                                          " 变性溶剂：" + ds3.Tables[0].Rows[0]["变性溶剂"].ToString() + " 产物量：" + ds3.Tables[0].Rows[0]["产物量"].ToString() +
                                          " 溶剂量：" + ds3.Tables[0].Rows[0]["溶剂量"].ToString() + " 胶液：" + ds3.Tables[0].Rows[0]["胶液"].ToString() +
                                          " 胶液批号：" + ds3.Tables[0].Rows[0]["胶液批号"].ToString() + " 预电泳电流：" + ds3.Tables[0].Rows[0]["预电泳电流"].ToString() +
                                          " 电泳电流：" + ds3.Tables[0].Rows[0]["电泳电流"].ToString() + " SampleSheet：" + ds3.Tables[0].Rows[0]["SampleSheet"].ToString() +
                                          " RunFold：" + ds3.Tables[0].Rows[0]["RunFold"].ToString();
                    dataRow["环境条件"] = "【环境条件】环境温度：" + ds3.Tables[0].Rows[0]["电泳环境温度"].ToString() + " 环境湿度：" + ds3.Tables[0].Rows[0]["电泳环境湿度"].ToString();

                    dataRow["一检人"] = ds.Tables[0].Rows[0]["一检姓名"].ToString();
                    dataRow["二检人"] = ds.Tables[0].Rows[0]["二检姓名"].ToString();

                    fw.FillBookMarks(dataSet);

                    fw.Fill扩增电泳记录表(ds2, ds3);
                }
                fw.Save();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                fw.CloseWord();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"\\" + 鉴定单位 + @"\\鉴定档案\\" + 委托编号 + @"\\文书记录\\" + Finalname;
        }
        [WebMethod]
        public string PrintSampleTestRecord(string 鉴定单位, string fileName, string fileType, string RecordType, string 记录ID)
        {
            string templatePath = ConfigurationManager.AppSettings["WebPath"] + @"\" + 鉴定单位 + @"\DNA汇总表\" + fileName;
            string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
            Helper.CheckDir(wordDir);
            string filename = Helper.GenerateID() + ".doc";
            string wordPath = wordDir + filename;

            if (System.IO.File.Exists(templatePath))
            {
                System.IO.File.Copy(templatePath, wordPath);
            }
            else
            {
                return "找不到文书模版";
            }

            FillWord fw = new FillWord(wordPath);
            fw.OpenDoc();
            if (fw.m_openedDoc == null)
            {
                return "生成文书失败";
            }
            try
            {
                if (RecordType.Equals("提取记录"))
                {
                    DataSet dataSet = new DataSet("NewDataSet");
                    DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                    DataColumn dc = new DataColumn("提取批号", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("使用试剂", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("使用仪器", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("提取时间", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("提取人", typeof(string)); dataTable.Columns.Add(dc);
                    DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                    DataSet ds = DBHelperSQL.Select("提取视图", "提取记录ID='" + 记录ID + "'", "ID,样本编号", "*");

                    string reagent = string.Empty;
                    string equipment = "离心机：" + ds.Tables[0].Rows[0]["离心机"].ToString()
                    + "漩涡振荡器：" + ds.Tables[0].Rows[0]["漩涡混合器"].ToString();

                    dataRow["提取批号"] = ds.Tables[0].Rows[0]["提取批号"].ToString();
                    dataRow["使用试剂"] = reagent;
                    dataRow["使用仪器"] = equipment.Substring(0, equipment.Length - 1);
                    dataRow["提取时间"] = ds.Tables[0].Rows[0]["日期"].ToString();
                    dataRow["提取人"] = ds.Tables[0].Rows[0]["提取人姓名"].ToString();
                    fw.FillBookMarks(dataSet);

                    fw.Fill提取记录表(ds);
                }
                else if (RecordType.Equals("扩增记录"))
                {
                    DataSet dataSet = new DataSet("NewDataSet");
                    DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                    DataColumn dc = new DataColumn("扩增批号", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("试剂配制", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增试剂", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("对应提取记录", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增仪器", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增方法", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增人姓名", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增时间开始", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增时间结束", typeof(string)); dataTable.Columns.Add(dc);
                    DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                    DataSet ds = DBHelperSQL.Select("扩增视图", "扩增记录ID='" + 记录ID + "'", "ID", "*");
                    DataSet tqds = DBHelperSQL.Select("提取记录", "ID='" + ds.Tables[0].Rows[0]["对应提取记录ID"].ToString() + "'", "ID", "*");

                    string getReagent = string.Empty;
                    string Reagent = string.Empty;
                    string equipment = "扩增仪：" + ds.Tables[0].Rows[0]["扩增仪"].ToString();
                    //ds.Tables[0].Rows[0]["扩增仪"].ToString() + ds.Tables[0].Rows[0]["扩增离心机"].ToString() + ds.Tables[0].Rows[0]["扩增漩涡混合器"].ToString()
                    //   + ds.Tables[0].Rows[0]["扩增移液器"].ToString() + ds.Tables[0].Rows[0]["扩增超净台"].ToString() + ds.Tables[0].Rows[0]["扩增工作站"].ToString();

                    dataRow["扩增批号"] = ds.Tables[0].Rows[0]["扩增批号"].ToString();
                    dataRow["试剂配制"] = getReagent;
                    dataRow["扩增试剂"] = Reagent;
                    dataRow["对应提取记录"] = tqds.Tables[0].Rows[0]["提取批号"];
                    dataRow["扩增仪器"] = equipment;
                    dataRow["扩增方法"] = ds.Tables[0].Rows[0]["扩增方法"].ToString();
                    dataRow["扩增人姓名"] = ds.Tables[0].Rows[0]["扩增人姓名"].ToString();
                    dataRow["扩增时间开始"] = ds.Tables[0].Rows[0]["扩增时间开始"].ToString();
                    dataRow["扩增时间结束"] = ds.Tables[0].Rows[0]["扩增时间结束"].ToString();
                    fw.FillBookMarks(dataSet);

                    fw.Fill扩增作业单(ds);
                }
                else if (RecordType.Equals("电泳记录"))
                {
                    DataSet dataSet = new DataSet("NewDataSet");
                    DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                    DataColumn dc = new DataColumn("电泳批号", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("试剂配制", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("内标", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("对应扩增记录", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("电泳仪器", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增方法", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("电泳人姓名", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("电泳时间开始", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("电泳时间结束", typeof(string)); dataTable.Columns.Add(dc);
                    DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                    DataSet ds = DBHelperSQL.Select("电泳视图", "电泳记录ID='" + 记录ID + "'", "ID", "*");
                    DataSet kzds = DBHelperSQL.Select("扩增记录", "ID='" + ds.Tables[0].Rows[0]["对应扩增记录ID"] + "'", "ID", "*");

                    string getReagent = string.Empty;
                    string Reagent = string.Empty;
                    string equipment = "电泳仪：" + ds.Tables[0].Rows[0]["电泳仪"].ToString();
                    //ds.Tables[0].Rows[0]["电泳仪"].ToString() + ds.Tables[0].Rows[0]["电泳离心机"].ToString() + ds.Tables[0].Rows[0]["电泳加热仪"].ToString() + ds.Tables[0].Rows[0]["电泳漩涡混合器"].ToString()
                    //    + ds.Tables[0].Rows[0]["电泳移液器"].ToString() + ds.Tables[0].Rows[0]["电泳超净台"].ToString() + ds.Tables[0].Rows[0]["电泳工作站"].ToString();

                    dataRow["电泳批号"] = ds.Tables[0].Rows[0]["电泳批号"].ToString();
                    dataRow["试剂配制"] = getReagent;
                    dataRow["内标"] = Reagent;
                    dataRow["对应扩增记录"] = kzds.Tables[0].Rows[0]["扩增批号"];
                    dataRow["电泳仪器"] = equipment;
                    dataRow["扩增方法"] = kzds.Tables[0].Rows[0]["扩增方法"];
                    dataRow["电泳人姓名"] = ds.Tables[0].Rows[0]["电泳人姓名"].ToString();
                    dataRow["电泳时间开始"] = ds.Tables[0].Rows[0]["电泳时间开始"].ToString();
                    dataRow["电泳时间结束"] = ds.Tables[0].Rows[0]["电泳时间结束"].ToString();
                    fw.FillBookMarks(dataSet);

                    fw.Fill检测作业单(ds);
                }
                fw.Save();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                fw.CloseWord();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\" + filename;
        }
        [WebMethod]
        public string PrintDNATestHelpFile(string 鉴定单位, string fileName, string fileType, string RecordType, string 记录ID)
        {
            string tmpfilename = Helper.GenerateID() + "." + fileType;
            if (fileType.Equals("doc"))
            {
                string templatePath = ConfigurationManager.AppSettings["WebPath"] + @"\" + 鉴定单位 + @"\DNA汇总表\" + fileName;
                string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
                Helper.CheckDir(wordDir);
                string wordPath = wordDir + tmpfilename;

                if (System.IO.File.Exists(templatePath))
                {
                    System.IO.File.Copy(templatePath, wordPath);
                }
                else
                {
                    return "找不到文书模版";
                }
                FillWord fw = new FillWord(wordPath);
                fw.OpenDoc();
                if (fw.m_openedDoc == null)
                {
                    return "生成文书失败";
                }
                try
                {
                    if (RecordType.Equals("扩增位置表格"))
                    {
                        DataSet ds = DBHelperSQL.Select("扩增视图", "扩增记录ID='" + 记录ID + "'", "ID", "*");
                        fw.Fill扩增位置表格(ds);
                    }
                    else if (RecordType.Equals("电泳位置表格"))
                    {
                        DataSet ds = DBHelperSQL.Select("电泳视图", "电泳记录ID='" + 记录ID + "'", "ID", "*");
                        fw.Fill电泳位置表格(ds);
                    }
                    fw.Save();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    fw.CloseWord();
                }
            }
            else if (fileType.Equals("txt"))
            {
                string time = DateTime.Today.ToShortDateString();
                tmpfilename = time + ".txt";
                string filenamelast = tmpfilename;
                string templatePath = ConfigurationManager.AppSettings["WebPath"] + @"STR\样本位置记录表.txt";
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

                    DataSet ds = DBHelperSQL.Select("电泳视图", "电泳记录ID='" + 记录ID + "'", "ID", "*");
                    DataSet ds2 = DBHelperSQL.Select("系统用户", "ID='" + ds.Tables[0].Rows[0]["电泳人"] + "'", "ID", "*");
                    if (RecordType.Equals("3130XL"))
                    {
                        writer.WriteLine("Container Name	Description	ContainerType	AppType	Owner	Operator	");
                        writer.WriteLine(filenamelast.Substring(0, filenamelast.Length - 4) + "		96-Well	Regular	" + ds2.Tables[0].Rows[0]["警号"] + "	" + ds2.Tables[0].Rows[0]["警号"] + "	");
                        writer.WriteLine("AppServer	AppInstance	");
                        writer.WriteLine("GeneMapper	GeneMapper_Generic_Instance	");
                        writer.WriteLine("Well	Sample Name	Comment	Priority	Sample Type	Snp Set	Analysis Method	Panel	User-Defined 3	Size Standard	User-Defined 2	User-Defined 1	Results Group 1	Instrument Protocol 1	");

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string bh = (ds.Tables[0].Rows[i]["样本编号"].ToString().Length == 0 ? ds.Tables[0].Rows[i]["样本名称"].ToString() : ds.Tables[0].Rows[i]["样本编号"].ToString());
                            if (i == ds.Tables[0].Rows.Count - 1)
                            {
                                writer.Write(ds.Tables[0].Rows[i]["电泳位置"].ToString() + "	" + bh + "		100									DNA	ID	");
                            }
                            else
                            {
                                writer.WriteLine(ds.Tables[0].Rows[i]["电泳位置"].ToString() + "	" + bh + "		100									DNA	ID	");
                            }
                        }
                    }
                    else if (RecordType.Equals("3500XL"))
                    {
                        writer.WriteLine("3500 Plate Layout File Version 1.0");
                        writer.WriteLine("");
                        writer.WriteLine("Plate Name	Application Type	Capillary Length (cm)	Polymer	Number of Wells	Owner Name	Barcode Number	Comments");
                        writer.WriteLine(filenamelast.Substring(0, filenamelast.Length - 4) + "	HID	36	POP4	96			");
                        writer.WriteLine("");
                        writer.WriteLine("Well	Sample Name	Assay	Results Group	File Name Convention	Sample Type	User Defined Field 1	User Defined Field 2	User Defined Field 3	User Defined Field 4	User Defined Field 5	Comments");

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string bh = (ds.Tables[0].Rows[i]["样本编号"].ToString().Length == 0 ? ds.Tables[0].Rows[i]["样本名称"].ToString() : ds.Tables[0].Rows[i]["样本编号"].ToString());
                            writer.WriteLine(ds.Tables[0].Rows[i]["电泳位置"].ToString() + "	" + bh + "	PP21_15s	2013	JIANAN	Sample						");
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
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\\" + tmpfilename;
        }
        #endregion
        #region    个案汇总表
        private static string GetRelativeDes(DataRow dr)
        {
            if (dr["亲属关系"].Equals("单亲"))
            {
                return dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + " 的 " + dr["亲属一目标关系"].ToString() +
                    dr["亲属一姓名"] + " 的 " + dr["亲属一样本类型"] + dr["亲属一样本描述"] + "，" +
                    (dr["亲属一样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : dr["亲属一样本编号"]) + "\n";
            }
            else
            {
                string qs2 = dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + " 的 " + dr["亲属二目标关系"].ToString() +
                    dr["亲属二姓名"] + " 的 " + dr["亲属二样本类型"] + dr["亲属二样本描述"] + "，" +
                    (dr["亲属二样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : dr["亲属二样本编号"]);
                return dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + " 的 " + dr["亲属一目标关系"].ToString() +
                        dr["亲属一姓名"] + " 的 " + dr["亲属一样本类型"] + dr["亲属一样本描述"] + "，" +
                        (dr["亲属一样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : dr["亲属一样本编号"]) + "\n" + qs2;
            }
        }
        private static DataSet printGAHZ(string 委托编号)
        {
            DataSet dataSet = new DataSet("NewDataSet");
            DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
            DataColumn dc = new DataColumn("受理序号", typeof(string)); dataTable.Columns.Add(dc);
            dc = new DataColumn("受理时间", typeof(string)); dataTable.Columns.Add(dc);
            dc = new DataColumn("一检姓名", typeof(string)); dataTable.Columns.Add(dc);
            dc = new DataColumn("案件名称", typeof(string)); dataTable.Columns.Add(dc);
            dc = new DataColumn("案件编号", typeof(string)); dataTable.Columns.Add(dc);
            dc = new DataColumn("检材样本", typeof(string)); dataTable.Columns.Add(dc);

            for (int i = 2; i < 6; i++)
            {
                dc = new DataColumn("受理序号" + i.ToString(), typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("受理时间" + i.ToString(), typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("一检姓名" + i.ToString(), typeof(string)); dataTable.Columns.Add(dc);
            }
            DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);
            int tesAmount = 0;
            int intID = 1;
            DataSet ds = DBHelperSQL.Query("select * from 鉴定流程视图 where SRCID =(select SRCID from 案件信息 where 委托编号 ='" + 委托编号 + "' ) and ora_flag = '1'  order by 受理序号");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    dataRow["受理序号"] = ds.Tables[0].Rows[i]["受理序号"].ToString();
                    dataRow["受理时间"] = Helper.GetSmallDate(ds.Tables[0].Rows[i]["受理时间"].ToString());
                    dataRow["一检姓名"] = ds.Tables[0].Rows[i]["一检姓名"].ToString();
                    dataRow["案件名称"] = ds.Tables[0].Rows[i]["案件名称"].ToString();
                    dataRow["案件编号"] = ds.Tables[0].Rows[i]["案件编号"].ToString();
                }
                else if (i < 5)
                {
                    int j = i + 1;
                    dataRow["受理序号" + j.ToString()] = ds.Tables[0].Rows[i]["受理序号"].ToString();
                    dataRow["受理时间" + j.ToString()] = Helper.GetSmallDate(ds.Tables[0].Rows[i]["受理时间"].ToString());
                    dataRow["一检姓名" + j.ToString()] = ds.Tables[0].Rows[i]["一检姓名"].ToString();
                }
            }
            DataSet dddddd = DBHelperSQL.Query("select 委托编号 from 鉴定流程视图 where SRCID =(select SRCID from 案件信息 where 委托编号 ='" + 委托编号 + "' ) and ora_flag = '1'  order by 受理序号");
            for (int i = 0; i < dddddd.Tables[0].Rows.Count; i++)
            {
                if (i != 0 && dddddd.Tables[0].Rows[i]["委托编号"] == dddddd.Tables[0].Rows[i - 1]["委托编号"]) continue;
                string filter = "委托编号 = '" + dddddd.Tables[0].Rows[i]["委托编号"] + "'";
                string tesDes = string.Empty;

                ds = DBHelperSQL.Select("样本信息", filter, "创建时间", "*");
                foreach (DataRow dr0 in ds.Tables[0].Rows)
                {
                    //物证检材信息
                    tesDes += intID.ToString() + " # " + dr0["名称"] + "，" + dr0["样本类型"] + "，" + (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : dr0["样本编号"]) + "\n";
                    tesAmount++; intID++;
                }
                ds = DBHelperSQL.Select("涉案人员", filter, "创建时间", "*");
                DataRow[] drs = ds.Tables[0].Select("库类型='嫌疑人'", "创建时间");
                foreach (DataRow dr0 in drs)
                {
                    //嫌疑人
                    tesDes += intID.ToString() + " # " + dr0["库类型"] + dr0["姓名"] + " 的 " + dr0["样本类型"] + " " + dr0["样本描述"] + "，" + (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : dr0["样本编号"]) + "\n";
                    tesAmount++; intID++;
                }
                drs = ds.Tables[0].Select("库类型='受害人'", "创建时间");
                foreach (DataRow dr0 in drs)
                {
                    //受害人
                    tesDes += intID.ToString() + " # " + dr0["库类型"] + dr0["姓名"] + " 的 " + dr0["样本类型"] + " " + dr0["样本描述"] + "，" + (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : dr0["样本编号"]) + "\n";
                    tesAmount++; intID++;
                }
                DataRow[] qtrydrs = ds.Tables[0].Select("库类型='其他人员'", "创建时间");

                ds = DBHelperSQL.Select("无名尸体", filter, "创建时间", "*");
                foreach (DataRow dr0 in ds.Tables[0].Rows)
                {
                    //无名尸体
                    tesDes += intID.ToString() + " # " + dr0["姓名"] + " 的 " + dr0["样本类型"] + " " + dr0["样本描述"] + "，" + (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : dr0["样本编号"]) + "\n";
                    tesAmount++; intID++;
                }
                ds = DBHelperSQL.Select("案件亲属视图", filter, "创建时间", "*");
                drs = ds.Tables[0].Select("库类型='受害人亲属'", "创建时间");
                foreach (DataRow dr0 in drs)
                {
                    //受害人亲属
                    tesDes += intID.ToString() + " # " + GetRelativeDes(dr0) + "\n";
                    tesAmount++; intID++;
                }
                drs = ds.Tables[0].Select("库类型='嫌疑人亲属'", "创建时间");
                foreach (DataRow dr0 in drs)
                {
                    //嫌疑人亲属
                    tesDes += intID.ToString() + " # " + GetRelativeDes(dr0) + "\n";
                    tesAmount++; intID++;
                }
                foreach (DataRow dr0 in qtrydrs)
                {
                    //其他人员
                    tesDes += intID.ToString() + " # " + dr0["库类型"] + dr0["姓名"] + " 的 " + dr0["样本类型"] + "，" + (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : dr0["样本编号"]) + "\n";
                    tesAmount++; intID++;
                }
                dataRow["检材样本"] += tesDes;
            }
            return dataSet;
        }
        #endregion
    }
}
