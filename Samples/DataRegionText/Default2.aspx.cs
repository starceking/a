using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DataRegionText_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        //设置文档打开方式
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docSubmitForm, "张三");
    }
}
