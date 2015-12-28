using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WordSetTable_WordSetTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        PageOffice.WordWriter.WordDocument doc = new PageOffice.WordWriter.WordDocument();
        //获取Table所在的数据区域对象
        PageOffice.WordWriter.DataRegion dataRegion = doc.OpenDataRegion("PO_regTable");
        //打开table，OpenTable(index)方法中的index代表Word文档中table位置的索引，从1开始
        PageOffice.WordWriter.Table table = dataRegion.OpenTable(1);
        //给table中的单元格赋值， OpenCellRC(行, 列)
        table.OpenCellRC(3, 1).Value = "A公司";
        table.OpenCellRC(3, 2).Value = "开发部";
        table.OpenCellRC(3, 3).Value = "李清";
        //插入一空行，InsertRowAfter方法中的参数表示在哪个单元格下面插入一行
        table.InsertRowAfter(table.OpenCellRC(3, 3));

        table.OpenCellRC(4, 1).Value = "B公司";
        table.OpenCellRC(4, 2).Value = "销售部";
        table.OpenCellRC(4, 3).Value = "张三";
        PageOfficeCtrl1.SetWriter(doc);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen("doc/test.doc",PageOffice.OpenModeType.docNormalEdit,"张佚名");
    }
}
