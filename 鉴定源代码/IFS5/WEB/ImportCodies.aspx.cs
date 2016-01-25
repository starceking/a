using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LIB;
using DAL;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class ImportCodies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //存储文件
        string dir = AppDomain.CurrentDomain.BaseDirectory + "\\" + Request.QueryString["psb"] + "\\Codies\\";
        Helper.CheckDir(dir);

        string fileName = string.Empty;
        HttpFileCollection files = Request.Files;
        for (int i = 0; i < files.Count; i++)
        {
            fileName = DateTime.Now.ToShortDateString() + "：" + Helper.GetDefaultEncoding(files[i].FileName);
            HttpPostedFile file = files[i];
            file.SaveAs(dir + fileName);
        }
        //string dir = @"D:\Pub\LNIFS5DNA\web\1\Codies\";
        //string fileName = "3130-ID-Plus.dat";
        //初始化基础数据
        string slns = string.Empty;
        IDictionary<string, string> idDict = new Dictionary<string, string>();
        IDictionary<string, string> nameDict = new Dictionary<string, string>();
        IDictionary<string, string> scDict = new Dictionary<string, string>();
        IDictionary<string, string> caseDict = new Dictionary<string, string>();
        IDictionary<string, string> conDict = new Dictionary<string, string>();

        using (FileStream fs = new FileStream(dir + fileName, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                do
                {
                    if (sr.ReadLine().Equals("PCR"))
                    {
                        slns += "样本编号='" + sr.ReadLine() + "' or ";

                    }
                } while (!sr.EndOfStream);

                if (sr != null)
                {
                    sr.Close();
                    fs.Close();
                }
            }
        }
        DataSet sampleDs = DBHelperSQL.Select("样本视图", slns.Substring(0, slns.Length - 4), string.Empty, "ID,名称,样本编号,库类型,案件ID,委托编号");
        foreach (DataRow dr in sampleDs.Tables[0].Rows)
        {
            idDict.Add(dr["样本编号"].ToString(), dr["ID"].ToString());
            nameDict.Add(dr["样本编号"].ToString(), dr["名称"].ToString());
            scDict.Add(dr["样本编号"].ToString(), dr["库类型"].ToString());
            caseDict.Add(dr["样本编号"].ToString(), dr["案件ID"].ToString());
            conDict.Add(dr["样本编号"].ToString(), dr["委托编号"].ToString());
        }
        if (slns.Length == 0) return;
        //获取str数据
        using (FileStream fs = new FileStream(dir + fileName, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
                {
                    dbConnection.Open();
                    using (SqlTransaction trans = dbConnection.BeginTransaction())
                    {
                        do
                        {
                            if (sr.ReadLine().Equals("PCR"))
                            {
                                string sln = sr.ReadLine();
                                for (int i = 0; i < 5; i++)//5行垃圾数据
                                {
                                    sr.ReadLine();
                                }
                                int maxCount = Convert.ToInt32(sr.ReadLine());

                                IDictionary<string, string> dict = new Dictionary<string, string>();
                                dict.Add("案件ID", caseDict[sln]);
                                dict.Add("委托编号", conDict[sln]);
                                dict.Add("SAMPLE_ID", idDict[sln]);
                                dict.Add("样本编号", sln);
                                dict.Add("样本名称", nameDict[sln]);
                                dict.Add("库类型", scDict[sln]);

                                for (int readValue = 0; readValue < maxCount; readValue++)
                                {
                                    string name = sr.ReadLine();
                                    for (int i = 0; i < 4; i++)//4行垃圾数据
                                    {
                                        sr.ReadLine();
                                    }
                                    string count = sr.ReadLine();
                                    string allVal = string.Empty;
                                    switch (count)
                                    {
                                        case "0": break;
                                        case "1": allVal = sr.ReadLine(); break;
                                        case "2": allVal = sr.ReadLine() + "/" + sr.ReadLine(); break;
                                        default:
                                            throw new Exception("样本包含错误数据：" + sln);
                                    }
                                    dict.Add(name, allVal);
                                }

                                DBHelperSQL.Insert("样本基因", dict, dbConnection, trans);
                            }
                        } while (!sr.EndOfStream);

                        trans.Commit();
                    }
                }
                if (sr != null)
                {
                    sr.Close();
                    fs.Close();
                }
            }
        }
    }
}
