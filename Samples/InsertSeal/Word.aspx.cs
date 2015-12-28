using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InsertSeal_Word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageOffice组件的引用
        //首先确保已安装了pageoffice服务器端组件，且在项目中已添加了pageoffice文件夹，
        //在该文件夹中已存在posetup.exe和server.aspx服务器端页面，再在前台页面中引入PageOfficeCtrl控件

        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.AddCustomToolButton("加盖印章", "InsertSeal()", 2);
        PageOfficeCtrl1.AddCustomToolButton("签字", "AddHandSign()", 3);
        PageOfficeCtrl1.AddCustomToolButton("验证印章", "VerifySeal()", 5);
        PageOfficeCtrl1.AddCustomToolButton("修改密码", "ChangePsw()", 0);
        string fileName = "template.doc";
        //打开文件
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.docNormalEdit, "张佚名");

    }
}
