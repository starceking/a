using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class ExaminationPaper_Savedoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.FileSaver fs = new PageOffice.FileSaver();
        string id = Request.QueryString["id"];
        if (id != null && id.Length > 0)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_paper.mdb";
            OleDbConnection conn = new OleDbConnection(strConn);
            byte[] aa = fs.FileBytes;

            OleDbCommand cm = new OleDbCommand();
            cm.Connection = conn;
            cm.CommandType = CommandType.Text;
            if (conn.State == 0) conn.Open();
            cm.CommandText = "UPDATE  Stream SET Word=@file WHERE ID=" + id;
            OleDbParameter spFile = new OleDbParameter("@file", OleDbType.Binary);
            spFile.Value = aa;
            cm.Parameters.Add(spFile);
            cm.ExecuteNonQuery();
            conn.Close();

            //向客户端控件返回以上代码执行成功的消息。
            fs.CustomSaveResult = "ok";

        }
        else
        {
            Page.RegisterClientScriptBlock("","<script>alert('未获得文件的ID，保存失败！');location.href='Default.aspx'</script>");
        }
        fs.Close();
    }
}
