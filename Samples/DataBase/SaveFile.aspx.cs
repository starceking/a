using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class Savedoc : System.Web.UI.Page
{
    protected string strErrHtml = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        PageOffice.FileSaver fs = new PageOffice.FileSaver();

        string sID = Request.QueryString["id"];
        string connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_database.mdb";

        OleDbConnection conn = new OleDbConnection(connstring);
        conn.Open();

        byte[] aa = fs.FileBytes;

        OleDbCommand cm = new OleDbCommand();
        cm.Connection = conn;
        cm.CommandType = CommandType.Text;
        if (conn.State == 0) conn.Open();
        cm.CommandText = "UPDATE  Stream SET Word=@file WHERE ID=" + sID;
        OleDbParameter spFile = new OleDbParameter("@file", OleDbType.Binary);
        spFile.Value = aa;
        cm.Parameters.Add(spFile);
        cm.ExecuteNonQuery();
        conn.Close();

        fs.Close();
    }
}
