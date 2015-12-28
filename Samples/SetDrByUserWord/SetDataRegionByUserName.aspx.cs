using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.WordWriter;

public partial class SetDataRegionByUserName : System.Web.UI.Page
{
    //登录的用户名
    protected  string user = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["userName"];
        //***************************卓正PageOffice组件的使用********************************
        WordDocument doc = new WordDocument();

        DataRegion dA1 = doc.OpenDataRegion("PO_A_pro1");
        DataRegion dA2 = doc.OpenDataRegion("PO_A_pro2");
        DataRegion dB1 = doc.OpenDataRegion("PO_B_pro1");
        DataRegion dB2 = doc.OpenDataRegion("PO_B_pro2");

        //根据登录用户名设置数据区域可编辑性
        //A部门经理登录后
        if (userName.Equals("zhangsan"))
        {
            dA1.Editing = true;
            dA2.Editing = true;
            dB1.Editing = false;
            dB2.Editing = false;
            user = "A部门经理";
        }
        //B部门经理登录后
        else
        {
            dB1.Editing = true;
            dB2.Editing = true;
            dA1.Editing = false;
            dA2.Editing = false;
            user = "B部门经理";
        }

        PageOfficeCtrl1.SetWriter(doc);

        //添加自定义按钮
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save", 1);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        //设置保存页
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        //设置文档打开方式
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docSubmitForm, userName);
    }
}
