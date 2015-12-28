using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        // Create custom toolbar
        PageOfficeCtrl1.AddCustomToolButton("保存", "SaveDocument()", 1);
        PageOfficeCtrl1.AddCustomToolButton("显示A文档", "ShowFile1View()", 0);
        PageOfficeCtrl1.AddCustomToolButton("显示B文档", "ShowFile2View()", 0);
        PageOfficeCtrl1.AddCustomToolButton("显示比较结果", "ShowCompareView()", 0);

        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";

        PageOfficeCtrl1.WordCompare("doc/aaa1.doc", "doc/aaa2.doc", PageOffice.OpenModeType.docAdmin, "Tom");
    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        
    }
}
