using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using PageOffice.WordReader;
using System.Data;
using System.Text;

public partial class SaveData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------  PageOffice 服务器端编程开始  -------------------//
        WordDocument doc = new WordDocument();
        DataRegion dataReg = doc.OpenDataRegion("PO_table");
        PageOffice.WordReader.Table table = dataReg.OpenTable(1);
     
        //输出提交的table中的数据
        Response.Write("表格中的各个单元的格数据为：<br/><br/>");
        StringBuilder dataStr = new StringBuilder();
        for (int i = 1; i <= table.RowsCount; i++)
        {
            dataStr.Append("<div style='width:220px;'>");
            for (int j = 1; j <= table.ColumnsCount; j++)
            {
                dataStr.Append("<div style='float:left;width:70px;border:1px solid red;'>"+table.OpenCellRC(i,j).Value+"</div>");
            }
            dataStr.Append("</div>");
        }
        Response.Write(dataStr.ToString());
        //向客户端显示提交的数据
        doc.ShowPage(300, 300);
        doc.Close();

    }
}
