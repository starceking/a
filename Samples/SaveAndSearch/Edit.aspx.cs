using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class SaveAndSearch_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Trim().Length > 0)
        {
            string id = Request.QueryString["id"].ToString().Trim();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_search.mdb";
            string sql = " select * from word where id= " + id;
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader Reader = cmd.ExecuteReader();
            if (Reader.Read())
            {
                string fileName = "";
                if (Reader["FileName"] != null && Reader["FileName"].ToString().Length > 0)
                {
                    fileName = Reader["FileName"].ToString().Trim() + ".doc";
                }
                else
                {
                    Page.RegisterClientScriptBlock("", "<script>alert('未获得文件名');</script>");
                    return;
                }

                //添加自定义工具栏按钮
                PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
                //设置保存页面
                PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx?id=" + id;
                //打开Word文档
                PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.docNormalEdit, "张佚名");
            }
            conn.Close();
        }
        else
        {
            Page.RegisterClientScriptBlock("", "<script>alert('未获得文档的编号');</script>");
            return;
        }
    }
}
