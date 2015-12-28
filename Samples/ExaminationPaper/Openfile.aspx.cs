using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class ExaminationPaper_Openfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"].Length > 0)
        {
            string id = Request.QueryString["id"];
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_paper.mdb";
            string strSql = "select Word from stream where id =" + id;

            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbCommand cmd = new OleDbCommand(strSql, conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                long num = reader.GetBytes(0, 0, null, 0, Int32.MaxValue) - 1;//
                Byte[] b = new Byte[num];
                reader.GetBytes(0, 0, b, 0, b.Length);
                Response.ContentType = "Application/msword";
                //你把这一句换成相应类型即可   
                Response.AddHeader("Content-Disposition", "attachment; filename=new.doc");
                Response.AddHeader("Content-Length", num.ToString());
                this.Response.Clear();
                System.IO.Stream fs = this.Response.OutputStream;
                fs.Write(b, 0, b.Length);
                fs.Close();
            }
            else
            {
                Page.RegisterClientScriptBlock("", "<script>alert('未获得文件信息！');location.href='Default.aspx'</script>");
            }
            reader.Close();
            conn.Close();
        }
        else
        {
            Page.RegisterClientScriptBlock("", "<script>alert('未获得文件的ID！');location.href='Default.aspx'</script>");
        }
    }
}
