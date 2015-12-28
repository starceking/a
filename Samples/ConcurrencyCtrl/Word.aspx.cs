using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = "somebody";

        string userId = Request.QueryString["userid"].ToString();
        if ("1" == userId)
        {
            userName = "张三";
        }
        else
        {
            userName = "李四";
        }

        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        PageOfficeCtrl1.TimeSlice = 20; // 设置并发控制时间, 单位:分钟
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docRevisionOnly, userName);
    }

}
