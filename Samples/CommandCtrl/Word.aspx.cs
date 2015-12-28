using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";

        PageOfficeCtrl1.OfficeToolbars = false;
        //PageOfficeCtrl1.Menubar = false; // 隐藏菜单栏
        PageOfficeCtrl1.JsFunction_AfterDocumentOpened = "AfterDocumentOpened";
        PageOfficeCtrl1.AllowCopy = false;  // 禁止拷贝
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docReadOnly, "Tom");
    }
}
