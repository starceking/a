using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.RibbonBar.SetTabVisible("TabHome", true);//开始的Ribbon
        PageOfficeCtrl1.RibbonBar.SetTabVisible("TabInsert", false);//插入的Ribbon
        PageOfficeCtrl1.RibbonBar.SetTabVisible("TabPageLayoutWord", false);//页面布局的Ribbon
        PageOfficeCtrl1.RibbonBar.SetTabVisible("TabReferences", false);//引用的Ribbon
        PageOfficeCtrl1.RibbonBar.SetTabVisible("TabMailings", false);//邮件的Ribbon
        PageOfficeCtrl1.RibbonBar.SetTabVisible("TabView", false);//视图的Ribbon
        PageOfficeCtrl1.RibbonBar.SetTabVisible("TabReviewWord", false);//审阅的Ribbon

        PageOfficeCtrl1.RibbonBar.SetSharedVisible("FileSave", false);//office自带的保存按钮
        PageOfficeCtrl1.RibbonBar.SetGroupVisible("GroupClipboard", false);//开始中的剪切板组
        PageOfficeCtrl1.AddCustomToolButton("保存", "SaveFile()",1);
        //PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "Tom");
    }
}
