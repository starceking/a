using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.ExcelWriter;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Workbook wb = new Workbook();
        Sheet sheet = wb.OpenSheet("Sheet1");
        // 设置背景
        PageOffice.ExcelWriter.Table backGroundTable = sheet.OpenTable("A1:P200");
        //设置表格边框样式
        backGroundTable.Border.LineColor = Color.White;

        // 设置单元格边框样式
        PageOffice.ExcelWriter.Border C4Border = sheet.OpenTable("C4:C4").Border;
        C4Border.Weight = PageOffice.ExcelWriter.XlBorderWeight.xlThick;
        C4Border.LineColor = Color.Yellow;
        C4Border.BorderType = XlBorderType.xlAllEdges;

        // 设置单元格边框样式
        PageOffice.ExcelWriter.Border B6Border = sheet.OpenTable("B6:B6").Border;
        B6Border.Weight = PageOffice.ExcelWriter.XlBorderWeight.xlHairline;
        B6Border.LineColor = Color.Purple;
        B6Border.LineStyle = XlBorderLineStyle.xlSlantDashDot;
        B6Border.BorderType = XlBorderType.xlAllEdges;

        //设置表格边框样式
        PageOffice.ExcelWriter.Table titleTable = sheet.OpenTable("B4:F5");
        titleTable.Border.Weight = PageOffice.ExcelWriter.XlBorderWeight.xlThick;
        titleTable.Border.LineColor = Color.FromArgb(0, 128, 128);
        titleTable.Border.BorderType = PageOffice.ExcelWriter.XlBorderType.xlAllEdges;

        //设置表格边框样式
        PageOffice.ExcelWriter.Table bodyTable2 = sheet.OpenTable("B6:F15");
        bodyTable2.Border.Weight = PageOffice.ExcelWriter.XlBorderWeight.xlThick;
        bodyTable2.Border.LineColor = Color.FromArgb(0, 128, 128);
        bodyTable2.Border.BorderType = PageOffice.ExcelWriter.XlBorderType.xlAllEdges;

        PageOfficeCtrl1.SetWriter(wb);// 不要忘记此句代码

        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsNormalEdit, "张三");
    }
}
