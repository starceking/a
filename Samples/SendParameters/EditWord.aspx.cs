using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SendParameters_SendParameters : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageOffice组件的引用
        //首先确保已安装了pageoffice服务器端组件，且在项目中已添加了pageoffice文件夹，
        //在该文件夹中已存在posetup.exe和server.aspx服务器端页面，再在前台页面中引入PageOfficeCtrl控件

        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.Caption = "演示：向保存页面传递参数，更新人员信息";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()",1);
        PageOfficeCtrl1.AddCustomToolButton("全屏", "SetFullScreen()", 4);
  
        //设置保存页
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx?id=1";//传递查询参数

        //打开文件
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "张佚名");
    }
}
