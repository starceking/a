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
        //获取数据区域对象后赋值
        PageOffice.WordWriter.DataRegion dataRegion1 = wordDoc.OpenDataRegion("PO_userName");
        dataRegion1.Value = "张三";

        //也可以直接赋值
        wordDoc.OpenDataRegion("PO_deptName").Value = "人事部";

        PageOfficeCtrl1.SetWriter(wordDoc);// 注意不要忘记此代码，如果缺少此句代码，不会赋值成功。
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "文档操作人姓名");
    }
}
