using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.SaveFilePage = "save.aspx";
        PageOfficeCtrl1.JsFunction_AfterDocumentOpened = "AfterDocumentOpened()";
        PageOfficeCtrl1.OfficeToolbars = false;
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
        PageOfficeCtrl1.AddCustomToolButton("新建批注", "InsertComment()", 3); 
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docRevisionOnly,"John");
    }
}
