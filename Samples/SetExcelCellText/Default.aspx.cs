using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using PageOffice.ExcelWriter;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.ExcelWriter.Workbook wb = new PageOffice.ExcelWriter.Workbook();
        Sheet sheet=wb.OpenSheet("Sheet1");

        Cell cC3 = sheet.OpenCell("C3");
        //设置单元格背景样式
        cC3.BackColor = Color.AntiqueWhite;
        cC3.Value = "一月";
        cC3.ForeColor = Color.Green;
        cC3.HorizontalAlignment = XlHAlign.xlHAlignCenter;

        Cell cD3 = sheet.OpenCell("D3");
        //设置单元格背景样式
        cD3.BackColor = Color.AntiqueWhite;
        cD3.Value = "二月";
        cD3.ForeColor = Color.Green;
        cD3.HorizontalAlignment = XlHAlign.xlHAlignCenter;

        Cell cE3 = sheet.OpenCell("E3");
        //设置单元格背景样式
        cE3.BackColor = Color.AntiqueWhite;
        cE3.Value = "三月";
        cE3.ForeColor = Color.Green;
        cE3.HorizontalAlignment = XlHAlign.xlHAlignCenter;

        Cell cB4 = sheet.OpenCell("B4");
        //设置单元格背景样式
        cB4.BackColor = Color.SkyBlue;
        cB4.Value = "住房";
        cB4.ForeColor = Color.Wheat;
        cB4.HorizontalAlignment = XlHAlign.xlHAlignCenter;

        Cell cB5 = sheet.OpenCell("B5");
        //设置单元格背景样式
        cB5.BackColor = Color.Teal;
        cB5.Value = "三餐";
        cB5.ForeColor = Color.Wheat;
        cB5.HorizontalAlignment = XlHAlign.xlHAlignCenter;

        Cell cB6 = sheet.OpenCell("B6");
        //设置单元格背景样式
        cB6.BackColor = Color.MediumPurple;
        cB6.Value = "车费";
        cB6.ForeColor = Color.Wheat;
        cB6.HorizontalAlignment = XlHAlign.xlHAlignCenter;

        Cell cB7 = sheet.OpenCell("B7");
        //设置单元格背景样式
        cB7.BackColor = Color.SteelBlue;
        cB7.Value = "通讯";
        cB7.ForeColor = Color.Wheat;
        cB7.HorizontalAlignment = XlHAlign.xlHAlignCenter;

        //绘制表格线
        PageOffice.ExcelWriter.Table titleTable = sheet.OpenTable("B3:E10");
        titleTable.Border.Weight = PageOffice.ExcelWriter.XlBorderWeight.xlThick;
        titleTable.Border.LineColor = Color.FromArgb(0, 128, 128);
        titleTable.Border.BorderType = PageOffice.ExcelWriter.XlBorderType.xlAllEdges;

        //合并单元格后赋值
        sheet.OpenTable("B1:E2").Merge();
        sheet.OpenTable("B1:E2").RowHeight = 30;//设置行高
        PageOffice.ExcelWriter.Cell B1 = sheet.OpenCell("B1");
        //设置单元格文本样式
        B1.HorizontalAlignment = PageOffice.ExcelWriter.XlHAlign.xlHAlignCenter;
        B1.VerticalAlignment = PageOffice.ExcelWriter.XlVAlign.xlVAlignCenter;
        B1.ForeColor = Color.FromArgb(0, 128, 128);
        B1.Value = "出差开支预算";
        B1.Font.Bold = true;
        B1.Font.Size = 25;

        PageOfficeCtrl1.SetWriter(wb); // 不要忘记此句代码

        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsNormalEdit, "张三");

    }
}
