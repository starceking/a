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
        //合并单元格
        sheet.OpenTable("B2:F2").Merge();
        Cell cB2 = sheet.OpenCell("B2");
        cB2.Value = "北京某公司产品销售情况";
        //设置水平对齐方式
        cB2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        cB2.ForeColor = Color.Red;
        cB2.Font.Size = 16;

        sheet.OpenTable("B4:B6").Merge();//合并单元格
        Cell cB4 = sheet.OpenCell("B4");
        cB4.Value = "A产品";
        //设置水平对齐方式
        cB4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //设置垂直对齐方式
        cB4.VerticalAlignment = XlVAlign.xlVAlignCenter;

        sheet.OpenTable("B7:B9").Merge();//合并单元格
        Cell cB7 = sheet.OpenCell("B7");
        cB7.Value = "B产品";
        cB7.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        cB7.VerticalAlignment = XlVAlign.xlVAlignCenter;

        PageOfficeCtrl1.SetWriter(wb);

        
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";  //此行必须
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);
        PageOfficeCtrl1.Caption = "演示：使用程序合并指定的单元格并设置格式和赋值";
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsNormalEdit, "张三");

    }
}
