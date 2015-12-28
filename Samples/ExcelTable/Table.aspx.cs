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
        //定义Table对象
        PageOffice.ExcelWriter.Table table = sheet.OpenTable("B4:F13");
        for(int i=0; i < 50; i++)
        {
            table.DataFields[0].Value = "产品 " + i.ToString();
            table.DataFields[1].Value = "100";
            table.DataFields[2].Value = (100+i).ToString();
            table.NextRow();
        }
        table.Close();

        PageOfficeCtrl1.SetWriter(workBook);// 注意不要忘记此代码，如果缺少此句代码，不会赋值成功。

        //设置PageOfficeCtrl控件的服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.Caption = "使用OpenTable给Excel赋值";
        
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsNormalEdit, "操作人姓名");
    }
}
