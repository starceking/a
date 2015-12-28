using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class DataRegionFill_DataRegionFill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        PageOffice.WordWriter.WordDocument wordDoc = new PageOffice.WordWriter.WordDocument();

        wordDoc.OpenDataRegion("PO_company").Value = "北京幻想科技有限公司";
        wordDoc.OpenDataRegion("PO_logo").Value = "[image]doc/logo.gif[/image]";
        wordDoc.OpenDataRegion("PO_dr1").Value = "左边的文本:xxxx";

        PageOfficeCtrl1.SetWriter(wordDoc);// 注意不要忘记此代码，如果缺少此句代码，不会赋值成功。
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "文档操作人姓名");
    }
}
