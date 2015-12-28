using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SetHandDraw : System.Web.UI.Page
{
    protected string userName = "";//登录的用户名
    protected void Page_Load(object sender, EventArgs e)
    {
        userName = Request.Form["userName"];
        if ("zhangsan" == userName) userName = "张三";
        if ("lisi" == userName) userName = "李四";
        if ("wangwu" == userName) userName = "王五";
        //***************************卓正PageOffice组件的使用********************************

        PageOfficeCtrl1.AddCustomToolButton("保存", "Save", 1);
        PageOfficeCtrl1.AddCustomToolButton("领导圈阅", "StartHandDraw", 3);
        //PageOfficeCtrl1.AddCustomToolButton("分层显示手写批注", "ShowHandDrawDispBar", 7);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        PageOfficeCtrl1.SaveFilePage = "Savefile.aspx";
        PageOfficeCtrl1.JsFunction_AfterDocumentOpened = "AfterDocumentOpened";
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, userName);
    }
}
