using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HandDrawsList_Word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.JsFunction_AfterDocumentOpened = "AfterDocumentOpened()";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
        PageOfficeCtrl1.AddCustomToolButton("开始手写", "StartHandDraw()", 3);
        PageOfficeCtrl1.AddCustomToolButton("设置线宽", "SetPenWidth()", 5);
        PageOfficeCtrl1.AddCustomToolButton("设置颜色", "SetPenColor()", 5);
        PageOfficeCtrl1.AddCustomToolButton("设置笔型", "SetPenType()", 5);
        PageOfficeCtrl1.AddCustomToolButton("设置缩放", "SetPenZoom()", 5);
     
        PageOfficeCtrl1.OfficeToolbars = false;
        // 设置保存文件页面
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docHandwritingOnly, "Tom");
    }
}