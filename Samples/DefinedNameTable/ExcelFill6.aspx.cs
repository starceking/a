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

public partial class DefinedNameTable_ExcelFill6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //设置PageOfficeCtrl控件的服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.Caption = "简单的给Excel赋值";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);

        PageOfficeCtrl1.WebOpen("doc/test4.xls", PageOffice.OpenModeType.xlsNormalEdit, "操作人姓名");
    }
}
