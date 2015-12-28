using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.OleDb;
using System.Data;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_salary.mdb";
        string strSql = "select * from Salary  order by ID";
        StringBuilder strHtmls = new StringBuilder();

        OleDbConnection conn = new OleDbConnection(strConn);
        OleDbCommand cmd = new OleDbCommand(strSql, conn);
        conn.Open();
        cmd.CommandType = CommandType.Text;
        OleDbDataReader Reader = cmd.ExecuteReader();
        strHtmls.Append("<tr  style='height:40px;padding:0; border-right:1px solid #a2c5d9; border-bottom:1px solid #a2c5d9; background:#edf8fe; font-weight:bold; color:#666;text-align:center; text-indent:0px;'>");
        strHtmls.Append("<td width='5%' >选择</td>");
        strHtmls.Append("<td width='10%' >员工编号</td>");
        strHtmls.Append("<td width='10%' >员工姓名</td>");
        strHtmls.Append("<td width='15%' >所在部门</td>");
        strHtmls.Append("<td width='10%' >应发工资</td>");
        strHtmls.Append("<td width='10%' >扣除金额</td>");
        strHtmls.Append("<td width='10%' >实发工资</td>");
        strHtmls.Append("<td width='10%' >发放日期</td>");
        strHtmls.Append("<td width='20%' >操作</td>");
        strHtmls.Append("</tr>");

        bool flg = false;

        while (Reader.Read())
        {
            flg = true;
            decimal result = 0;
            DateTime date = DateTime.Now;
            string pID = Reader["ID"].ToString().Trim();
            strHtmls.Append("<tr  style='height:40px; text-indent:10px; padding:0; border-right:1px solid #a2c5d9; border-bottom:1px solid #a2c5d9; color:#666;'>");
            strHtmls.Append("<td style=' text-align:center;'><input id='check" + pID + "'  type='checkbox' /></td>");
            strHtmls.Append("<td style=' text-align:left;'>" + pID + "</td>");
            strHtmls.Append("<td style=' text-align:left;'>" + Reader["UserName"].ToString() + "</td>");
            strHtmls.Append("<td style=' text-align:left;'>" + Reader["DeptName"].ToString() + "</td>");
            if (Reader["SalTotal"] != null && decimal.TryParse(Reader["SalTotal"].ToString(), out result))
            {
                strHtmls.Append("<td style=' text-align:left;'>" + decimal.Parse(Reader["SalTotal"].ToString()) + "</td>");
            }
            else
            {
                strHtmls.Append("<td style=' text-align:left;'>￥0.00</td>");
            }

            if (Reader["SalDeduct"] != null && decimal.TryParse(Reader["SalDeduct"].ToString(), out result))
            {
                strHtmls.Append("<td style=' text-align:left;'>" + decimal.Parse(Reader["SalDeduct"].ToString()) + "</td>");
            }
            else
            {
                strHtmls.Append("<td style=' text-align:left;'>￥0.00</td>");
            }

            if (Reader["SalCount"] != null && decimal.TryParse(Reader["SalCount"].ToString(), out result))
            {
                strHtmls.Append("<td style=' text-align:left;'>" + decimal.Parse(Reader["SalCount"].ToString()) + "</td>");
            }
            else
            {
                strHtmls.Append("<td style=' text-align:left;'>￥0.00</td>");
            }

            if (Reader["DataTime"] != null && DateTime.TryParse(Reader["DataTime"].ToString(), out date))
            {
                strHtmls.Append("<td style=' text-align:center;'>" + DateTime.Parse(Reader["DataTime"].ToString()).ToString("yyyy-MM-dd") + "</td>");
            }
            else
            {
                strHtmls.Append("<td style=' text-align:left;'>" + DateTime.Now.ToString("yyyy-MM-dd") + "</td>");
            }
            strHtmls.Append("<td style=' text-align:center;'><a href='View.aspx?ID=" + pID + "' target='_blank'>查看</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href='Openfile.aspx?ID=" + pID + "' target='_blank'>编辑</a></td>");
            strHtmls.Append("</tr>");
        }
        if (!flg)
        {
            strHtmls.Append("<tr>\r\n");
            strHtmls.Append("<td width='100%' height='100' align='center'>对不起，暂时没有可以操作的数据。\r\n");
            strHtmls.Append("</td></tr>\r\n");
        }
        LiteralGrid.Text = strHtmls.ToString();
        Reader.Close();
        conn.Close();
    }
}
