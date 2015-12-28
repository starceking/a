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
        sheet.OpenCellByDefinedName("testA1").Value = "Tom";
        sheet.OpenCellByDefinedName("testB1").Value = "John";

        PageOfficeCtrl1.SetWriter(workBook);// 注意不要忘记此代码，如果缺少此句代码，不会赋值成功。

        //设置PageOfficeCtrl控件的服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.Caption = "给Excel文档中定义名称的单元格赋值";
        PageOfficeCtrl1.SaveDataPage = "SaveData.aspx";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
        
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsSubmitForm, "操作人姓名");
    }
}
