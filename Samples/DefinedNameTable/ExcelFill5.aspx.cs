using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class DefinedNameTable_ExcelFill5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.ExcelWriter.Workbook wk = new PageOffice.ExcelWriter.Workbook();
        PageOffice.ExcelWriter.Sheet sheet = wk.OpenSheet("Sheet1");
        PageOffice.ExcelWriter.Table table = sheet.OpenTableByDefinedName("report", 4, 5, true);
        int rowCount = 12;//假设将要自动填充数据的实际记录条数为12
        for (int i = 1; i <= rowCount; i++)
        {
            table.DataFields[0].Value = i + "月";
            table.DataFields[1].Value = "100";
            table.DataFields[2].Value = "120";
            table.DataFields[3].Value = "500";
            table.DataFields[4].Value = "120%";
            table.NextRow();
        }

        table.Close();

        //定义另一个table
        PageOffice.ExcelWriter.Table table2 = sheet.OpenTableByDefinedName("report2", 4, 5, true);
        int rowCount2 = 4;//假设将要自动填充数据的实际记录条数为12
        for (int i = 1; i <= rowCount2; i++)
        {
            table2.DataFields[0].Value = i + "季度";
            table2.DataFields[1].Value = "300";
            table2.DataFields[2].Value = "300";
            table2.DataFields[3].Value = "300";
            table2.DataFields[4].Value = "100%";
            table2.NextRow();
        }

        table2.Close();
        PageOfficeCtrl1.SetWriter(wk);// 注意不要忘记此代码，如果缺少此句代码，不会赋值成功。

        //设置PageOfficeCtrl控件的服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.Caption = "给Excel文档中定义名称的单元格赋值";
        PageOfficeCtrl1.SaveDataPage = "SaveData.aspx";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);

        PageOfficeCtrl1.WebOpen("doc/test4.xls", PageOffice.OpenModeType.xlsSubmitForm, "操作人姓名");
    }
}
