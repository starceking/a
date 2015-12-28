using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SavaReturnValue_SavaReturnValue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageOffice组件的引用
        //首先确保已安装了pageoffice服务器端组件，且在项目中已添加了pageoffice文件夹，
        //在该文件夹中已存在posetup.exe和server.aspx服务器端页面，再在前台页面中引入PageOfficeCtrl控件

        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        //设置PageOfficeCtrl 控件标题栏文字
        PageOfficeCtrl1.Caption = "演示：保存返回值";

        //在自定义工具栏上添加保存按钮,三个参数分别代表：按钮名称、要执行的JS函数名称、按钮图标代码
        PageOfficeCtrl1.AddCustomToolButton("保存","Save()",1);
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        //打开文件
        //PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.docHandwritingOnly, "打开文件者的姓名");
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "张佚名");
    }
}
