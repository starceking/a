using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReadOnly : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fileName = "zhengshi.doc";//正式发文文件

        PageOfficeCtrl1.AddCustomToolButton("另存到本地", "ShowDialog(0)", 5);
        PageOfficeCtrl1.AddCustomToolButton("页面设置", "ShowDialog(1)", 0);
        PageOfficeCtrl1.AddCustomToolButton("打印", "ShowDialog(2)", 6);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen()", 4);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        PageOfficeCtrl1.WebOpen("doc/" + fileName, PageOffice.OpenModeType.docReadOnly, "张三");
    }
}
