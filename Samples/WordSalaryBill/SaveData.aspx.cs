using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using PageOffice.WordReader;
using System.Data;

public partial class SaveData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string id = Request.QueryString["id"];
        if (id != null && id.Length > 0)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_salary.mdb";
            OleDbConnection conn = new OleDbConnection(strConn);

            OleDbCommand cm = new OleDbCommand();
            cm.Connection = conn;
            cm.CommandType = CommandType.Text;
            if (conn.State == 0) conn.Open();
            string userName = "", deptName = "", salTotoal = "0", salDeduct = "0", salCount = "0", dateTime = "";
            //-----------  PageOffice 服务器端编程开始  -------------------//
            WordDocument doc = new WordDocument();
            userName = doc.OpenDataRegion("PO_UserName").Value;
            deptName = doc.OpenDataRegion("PO_DeptName").Value;
            salTotoal = doc.OpenDataRegion("PO_SalTotal").Value;
            salDeduct = doc.OpenDataRegion("PO_SalDeduct").Value;
            salCount = doc.OpenDataRegion("PO_SalCount").Value;
            dateTime = doc.OpenDataRegion("PO_DataTime").Value;

            cm.CommandText = "UPDATE Salary SET UserName=@UserName,DeptName=@DeptName,SalTotal=@SalTotal," +
            "SalDeduct=@SalDeduct,SalCount=@SalCount,DataTime=@DataTime WHERE ID=" + id;
            OleDbParameter[] spFile = new OleDbParameter[]{ 
                new OleDbParameter("@UserName", OleDbType.VarWChar),
                new OleDbParameter("@DeptName", OleDbType.VarWChar),
                new OleDbParameter("@SalTotal", OleDbType.Currency),
                new OleDbParameter("@SalDeduct", OleDbType.Currency),
                new OleDbParameter("@SalCount", OleDbType.Currency),
                new OleDbParameter("@DataTime", OleDbType.Date)
            };
            spFile[0].Value = userName;
            spFile[1].Value = deptName;
            spFile[2].Value = salTotoal;
            spFile[3].Value = salDeduct;
            spFile[4].Value = salCount;
            spFile[5].Value = dateTime;
            cm.Parameters.AddRange(spFile);
            cm.ExecuteNonQuery();
            conn.Close();

            //向客户端控件返回以上代码执行成功的消息。
            doc.CustomSaveResult = "ok";
            doc.Close();
        }
        else
        {
            Page.RegisterClientScriptBlock("", "<script>alert('未获得文件的ID，保存失败！');location.href='Default.aspx'</script>");
        }
    }
}
