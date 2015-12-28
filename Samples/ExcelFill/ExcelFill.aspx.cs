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
        //定义Workbook对象
        PageOffice.ExcelWriter.Workbook workBook = new PageOffice.ExcelWriter.Workbook();
        //定义Sheet对象，"Sheet1"是打开的Excel表单的名称
        PageOffice.ExcelWriter.Sheet sheet = workBook.OpenSheet("Sheet1");
        //定义Cell对象,给单元格赋值
        PageOffice.ExcelWriter.Cell cellB4 = sheet.OpenCell("B4");
        cellB4.Value = "1月";
        //或者直接给Cell赋值
        sheet.OpenCell("C4").Value = "300";
        sheet.OpenCell("D4").Value = "270";
        sheet.OpenCell("E4").Value = "270";
        sheet.OpenCell("F4").Value = string.Format("{0:P}", 270.0 / 300);

        PageOfficeCtrl1.SetWriter(workBook);// 注意不要忘记此代码，如果缺少此句代码，不会赋值成功。

        //设置PageOfficeCtrl控件的服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.Caption = "简单的给Excel赋值";
        
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsNormalEdit, "操作人姓名");
    }
}
