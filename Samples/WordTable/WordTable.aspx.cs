using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.WordWriter;

public partial class WordTable_WordTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WordDocument doc = new WordDocument();
        PageOffice.WordWriter.Table table1 = doc.OpenDataRegion("PO_T001").OpenTable(1);
        table1.OpenCellRC(1, 1).Value = "PageOffice组件";

        int oldRowCount = 3;//表格中原有的行数
        int dataRowCount = 5;//要填充数据的行数
        // 扩充表格
        for (int j = 0; j < dataRowCount - oldRowCount; j++)
        {
            table1.InsertRowAfter(table1.OpenCellRC(2, 5));  //在第2行的最后一个单元格下插入新行
        }

        // 填充数据
        int i = 1;
        while (i <= dataRowCount)
        {   
            table1.OpenCellRC(i, 2).Value = "AA" + i.ToString();
            table1.OpenCellRC(i, 3).Value = "BB" + i.ToString();
            table1.OpenCellRC(i, 4).Value = "CC" + i.ToString();
            table1.OpenCellRC(i, 5).Value = "DD" + i.ToString();
            i++;
        }
        PageOfficeCtrl1.SetWriter(doc);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen("doc/test_table.doc", PageOffice.OpenModeType.docNormalEdit, "张佚名");
    }
}
