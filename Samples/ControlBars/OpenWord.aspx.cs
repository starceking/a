using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OpenWord_OpenWord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOfficeCtrl1.Titlebar = false; //隐藏标题栏
        PageOfficeCtrl1.Menubar = false; //隐藏菜单栏
        PageOfficeCtrl1.OfficeToolbars = false; //隐藏Office工具栏
        PageOfficeCtrl1.CustomToolbar = false; //隐藏自定义工具栏
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        //打开文件
        PageOfficeCtrl1.WebOpen("doc/template.doc", PageOffice.OpenModeType.docReadOnly, "张佚名");
    }
}
