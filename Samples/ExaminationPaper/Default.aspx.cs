using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Text;

public partial class ExaminationPaper_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_paper.mdb";
        string strSql = "select * from stream ";
        StringBuilder strHtmls = new StringBuilder();

        OleDbConnection conn = new OleDbConnection(strConn);
        OleDbCommand cmd = new OleDbCommand(strSql, conn);
        conn.Open();
        cmd.CommandType = CommandType.Text;
        OleDbDataReader Reader = cmd.ExecuteReader();

        strHtmls.Append("<tr  style='background-color:#FEE;'>");
        strHtmls.Append("<td width='10%' >选择</td>");
        strHtmls.Append("<td width='30%' >题库编号</td>");
        strHtmls.Append("<td width='60%' >操作</td>");
        strHtmls.Append("</tr>");
        if (Reader.Read())
        {
            string pID = Reader["ID"].ToString().Trim();
            strHtmls.Append("<tr  style='background-color:white;'>");
            strHtmls.Append("<td><input name='check" + pID + "'  type='checkbox' /></td>");
            strHtmls.Append("<td>选择题-" + pID + "</td>");
            strHtmls.Append("<td><a href='edit.aspx?ID=" + pID + "'>编辑</a></td>");
            strHtmls.Append("</tr>");

            while (Reader.Read())
            {
                pID = Reader["ID"].ToString().Trim();
                strHtmls.Append("<tr  style='background-color:white;'>");
                strHtmls.Append("<td><input name='check" + pID + "'  type='checkbox' /></td>");
                strHtmls.Append("<td>选择题-" + pID + "</td>");
                strHtmls.Append("<td><a href='edit.aspx?ID=" + pID + "'>编辑</a></td>");
                strHtmls.Append("</tr>");
            }
        }
        else
        {
            strHtmls.Append("<tr>\r\n");
            strHtmls.Append("<td colspan='3' width='100%' height='100' align='center'>对不起，暂时没有可以操作的数据。\r\n");
            strHtmls.Append("</td></tr>\r\n");
        }
        LiteralGrid.Text = strHtmls.ToString();
        Reader.Close();
        conn.Close();
    }
}
