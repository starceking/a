using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OpenWord_OpenWord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageOffice组件的引用
        //首先确保已安装了pageoffice服务器端组件，且在项目中已添加了pageoffice文件夹，
        //在该文件夹中已存在posetup.exe和server.aspx服务器端页面，再在前台页面中引入PageOfficeCtrl控件

        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.Caption = "演示：最简单的只读模式打开word文件";
        //在只读模式下工具条和菜单栏都已不起作用，不需要显示
        PageOfficeCtrl1.OfficeToolbars = false;
        PageOfficeCtrl1.CustomToolbar = false;
 
        //打开文件
        PageOfficeCtrl1.WebOpen("doc/template.doc", PageOffice.OpenModeType.docReadOnly, "张佚名");
    }
}
