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
using System.Data.OleDb;

public partial class ImportSampleTestExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //存储文件
        string dir = AppDomain.CurrentDomain.BaseDirectory + "\\Excel\\";
        Helper.CheckDir(dir);

        string fileName = string.Empty;
        HttpFileCollection files = Request.Files;
        for (int i = 0; i < files.Count; i++)
        {
            fileName = DateTime.Now.ToShortDateString() + "：" + Helper.GetDefaultEncoding(files[i].FileName);
            HttpPostedFile file = files[i];
            file.SaveAs(dir + fileName);
        }

        string mystring = @"Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '" + dir + fileName + "';Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
        OleDbConnection cnnxls = new OleDbConnection(mystring);
        OleDbDataAdapter myDa = new OleDbDataAdapter("select * from [Sheet1$]", cnnxls);
        DataSet myDs = new DataSet();
        myDa.Fill(myDs);
        myDs.Tables[0].Rows.RemoveAt(0);
        if (myDs.Tables[0].Rows.Count == 0) return;

        string sql = string.Empty;
        string kzid = Request.QueryString["kzID"];

        foreach (DataRow dr in myDs.Tables[0].Rows)
        {
            sql += "Update 样本扩增 set 电泳位置='" + dr[1] + "' where 样本编号='" + dr[0] + "' and 扩增电泳ID='" + kzid + "';";
        }
        DBHelperSQL.ExecuteSql(sql);
    }
}
