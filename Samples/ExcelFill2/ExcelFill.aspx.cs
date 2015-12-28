using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class SubmitExcel_SubmitExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //设置PageOfficeCtrl控件的服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        //PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
        PageOfficeCtrl1.Caption = "简单的给Excel赋值";
        //定义Workbook对象
        PageOffice.ExcelWriter.Workbook workBook = new PageOffice.ExcelWriter.Workbook();
        //定义Sheet对象，"Sheet1"是打开的Excel表单的名称
        PageOffice.ExcelWriter.Sheet sheet = workBook.OpenSheet("Sheet1");
        //定义Cell对象
        PageOffice.ExcelWriter.Cell cellB4 = sheet.OpenCell("B4");
        //给单元格赋值
        cellB4.Value = "1月";
        //设置字体颜色
        cellB4.ForeColor = Color.Red;

        PageOffice.ExcelWriter.Cell cellC4 = sheet.OpenCell("C4");
        cellC4.Value = "300";
        cellC4.ForeColor = Color.Blue;

        PageOffice.ExcelWriter.Cell cellD4 = sheet.OpenCell("D4");
        cellD4.Value = "270";
        cellD4.ForeColor = Color.Orange;

        PageOffice.ExcelWriter.Cell cellE4 = sheet.OpenCell("E4");
        cellE4.Value = "270";
        cellE4.ForeColor = Color.Green;

        PageOffice.ExcelWriter.Cell cellF4 = sheet.OpenCell("F4");
        cellF4.Value = string.Format("{0:P}",270.0/300);
        cellF4.ForeColor = Color.Gray;

        PageOfficeCtrl1.SetWriter(workBook);
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsNormalEdit, "操作人姓名");
    }
}
