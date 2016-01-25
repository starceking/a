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

namespace WS
{
    /// <summary>
    /// Summary description for CASEFILEWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class CASEFILEWS : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetAll(string 鉴定单位, string 委托编号, string fileType)
        {
            string dir = ConfigurationManager.AppSettings["WebPath"] + 鉴定单位 + "\\鉴定档案\\" + 委托编号 + "\\" + fileType;
            Helper.CheckDir(dir);

            DataSet ds = new DataSet("NewDataSet");
            DataTable dt = new DataTable("tableName"); ds.Tables.Add(dt);
            DataColumn dc = new DataColumn("FileName", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Url", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("DiskPath", typeof(string)); dt.Columns.Add(dc);

            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            FileInfo[] files = dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                if (fileType.Equals("照片图像") && (!file.Name.Contains("Tb."))) continue;

                DataRow dr = dt.NewRow();
                if (fileType.Equals("照片图像"))
                    dr["FileName"] = file.Name.Replace("Tb.", ".");
                else dr["FileName"] = file.Name;
                dr["Url"] = ConfigurationManager.AppSettings["ServerAddr"] + 鉴定单位 + "/鉴定档案/" + 委托编号 + "/" + fileType + "/" + file.Name;
                dr["DiskPath"] = file.FullName;
                dt.Rows.Add(dr);
            }

            return ds.GetXml();
        }
        [WebMethod]
        public string Delete(string DiskPath)
        {
            try
            {
                System.IO.File.Delete(DiskPath);
                if (DiskPath.Contains("Tb."))
                {
                    System.IO.File.Delete(DiskPath.Replace("Tb.", "."));
                }
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [WebMethod]
        public string GetAllFileData(string 鉴定单位, string 鉴定专业, string 文件类型)
        {
            string dir = string.Empty;
            if (文件类型.Equals("0"))
            {
                dir = ConfigurationManager.AppSettings["WebPath"] + 鉴定单位 + "\\共享文件\\" + 鉴定专业 + "\\";
            }
            else
            {
                dir = ConfigurationManager.AppSettings["WebPath"] + 鉴定单位 + "\\共享文件\\公用文件\\";
            }
            Helper.CheckDir(dir);

            DataSet ds = new DataSet("NewDataSet");
            DataTable dt = new DataTable("tableName"); ds.Tables.Add(dt);
            DataColumn dc = new DataColumn("FileName", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Url", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("DiskPath", typeof(string)); dt.Columns.Add(dc);

            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            FileInfo[] files = dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                DataRow dr = dt.NewRow();
                dr["FileName"] = file.Name;
                if (文件类型.Equals("0"))
                {
                    dr["Url"] = ConfigurationManager.AppSettings["ServerAddr"] + 鉴定单位 + "/共享文件/" + 鉴定专业 + "/" + file.Name;
                }
                else
                {
                    dr["Url"] = ConfigurationManager.AppSettings["ServerAddr"] + 鉴定单位 + "/共享文件/公用文件/" + file.Name;
                }
                dr["DiskPath"] = file.FullName;
                dt.Rows.Add(dr);
            }

            return ds.GetXml();
        }
    }
}
