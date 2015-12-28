using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.WordWriter;
using System.Drawing;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WordDocument doc = new WordDocument();
        DataRegion dataReg = doc.OpenDataRegion("PO_table");
        PageOffice.WordWriter.Table table = dataReg.OpenTable(1);
        //合并table中的单元格
        table.OpenCellRC(1, 1).MergeTo(1, 4);
        //给合并后的单元格赋值
        table.OpenCellRC(1, 1).Value = "销售情况表";
        //设置单元格文本样式
        table.OpenCellRC(1, 1).Font.Color = Color.Red;
        table.OpenCellRC(1, 1).Font.Size = 24;
        table.OpenCellRC(1, 1).Font.Name = "楷体";
        table.OpenCellRC(1, 1).ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

        PageOfficeCtrl1.SetWriter(doc);//不要忘记此句代码

        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        PageOfficeCtrl1.CustomToolbar = false;
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docAdmin, "张三");


    }
}
