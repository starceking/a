using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.WordWriter;
using System.Drawing;

public partial class SetDataRegionByUserName : System.Web.UI.Page
{
    //登录的用户名
    protected string userName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        userName = Request.Form["userName"];
        //***************************卓正PageOffice组件的使用********************************
        WordDocument doc = new WordDocument();

        //打开数据区域
        DataRegion d1 = doc.OpenDataRegion("PO_com1");
        //给数据区域赋值
        d1.Value = "[word]doc/content1.doc[/word]";
        //若要将数据区域内容存入文件中，则必须设置属性“SubmitAsFile”值为true
        d1.SubmitAsFile = true;

        DataRegion d2 = doc.OpenDataRegion("PO_com2");
        d2.Value = "[word]doc/content2.doc[/word]";
        d2.SubmitAsFile = true;

        //根据登录用户名设置数据区域可编辑性
        //甲客户：zhangsan 登录后登录后
        if (userName.Equals("zhangsan"))
        {
            d1.Editing = true;
            d2.Editing = false;
        }
        //乙客户：lisi 登录后登录后
        else
        {
            d2.Editing = true;
            d1.Editing = false;
        }

        PageOfficeCtrl1.SetWriter(doc);

        //添加自定义按钮
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save", 1);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        //设置保存页
        PageOfficeCtrl1.SaveDataPage = "SaveData.aspx?userName=" + userName;
        //设置文档打开方式
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docSubmitForm, userName);
    }
}
