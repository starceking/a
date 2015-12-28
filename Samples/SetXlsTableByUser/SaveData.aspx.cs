using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using PageOffice.WordReader;
using System.Data;
using PageOffice.ExcelReader;
using System.Text;

public partial class SaveData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------  PageOffice 服务器端编程开始  -------------------//
        Workbook wb = new Workbook();
        Sheet sheet = wb.OpenSheet("Sheet1");
        PageOffice.ExcelReader.Table tableA = sheet.OpenTable("tableA");
        PageOffice.ExcelReader.Table tableB = sheet.OpenTable("tableB");
        
        //输出提交的数据
        Response.Write("提交的数据为：<br/><br/>");
        ////Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;计划完成量"+
        //               "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;实际完成量<br/>");
        //Response.Write("A部门：");
        StringBuilder dataStr = new StringBuilder();
        dataStr.Append("<div style='float:left;width:460px;'>");
        dataStr.Append("<div style='float:left;width:150px;'>&nbsp; </div>");
        dataStr.Append("<div style='float:left;width:150px;'>计划完成量</div>");
        dataStr.Append("<div style='float:left;width:150px;'>实际完成量 </div>");
        dataStr.Append("</div>");
        while (!tableA.EOF) 
        {
            dataStr.Append("<div style='float:left;width:460px;'>");
            dataStr.Append("<div style='float:left;width:150px;'> A部门：</div>");
            for (int i = 0; i < tableA.DataFields.Count; i++) 
            {
                dataStr.Append("<div style='float:left;width:150px;'>" + tableA.DataFields[i].Value + "</div>");
            }
            dataStr.Append("</div>");
            tableA.NextRow();
        }
        while (!tableB.EOF)
        {
            dataStr.Append("<div style='float:left;width:460px;'>");
            dataStr.Append("<div style='float:left;width:150px;'> B部门：</div>");
            for (int i = 0; i < tableB.DataFields.Count; i++)
            {
                dataStr.Append("<div style='float:left;width:150px;'>" + tableB.DataFields[i].Value + "</div>");
            }
            dataStr.Append("</div>");
            tableB.NextRow();
        }
        Response.Write(dataStr.ToString());
        //向客户端显示提交的数据
        wb.ShowPage(500, 400);
        wb.Close();

    }
}
