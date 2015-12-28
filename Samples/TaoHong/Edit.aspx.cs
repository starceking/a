using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fileName = "test.doc"; // 正文文件
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save", 1);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        PageOfficeCtrl1.SaveFilePage = "Savefile.aspx";
        PageOfficeCtrl1.WebOpen("doc/" + fileName, PageOffice.OpenModeType.docNormalEdit, "张三");
    }
}
