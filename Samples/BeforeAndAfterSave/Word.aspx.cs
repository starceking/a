using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomToolButton_Word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";

        // 设置文件保存之前执行的事件
        PageOfficeCtrl1.JsFunction_BeforeDocumentSaved = "BeforeDocumentSaved()";
        // 设置文件保存之后执行的事件
        PageOfficeCtrl1.JsFunction_AfterDocumentSaved = "AfterDocumentSaved()";

        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "Tom");
    }
}
