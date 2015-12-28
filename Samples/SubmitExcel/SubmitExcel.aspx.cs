using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubmitExcel_SubmitExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //设置PageOfficeCtrl控件的服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
        //定义Workbook对象
        PageOffice.ExcelWriter.Workbook workBook = new PageOffice.ExcelWriter.Workbook();
        //定义Sheet对象，"Sheet1"是打开的Excel表单的名称
        PageOffice.ExcelWriter.Sheet sheet = workBook.OpenSheet("Sheet1");

        //定义table对象，设置table对象的设置范围
        PageOffice.ExcelWriter.Table table = sheet.OpenTable("B4:F13");
        //设置table对象的提交名称，以便保存页面获取提交的数据
        table.SubmitName = "Info";

        PageOfficeCtrl1.SetWriter(workBook);
        PageOfficeCtrl1.SaveDataPage = "SaveData.aspx";
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsSubmitForm, "操作人姓名");
    }
}
