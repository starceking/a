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
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOffice.WordWriter.WordDocument wordDoc = new PageOffice.WordWriter.WordDocument();

        //打开数据区域，OpenDataRegion方法的参数代表Word文档中的书签名称
        PageOffice.WordWriter.DataRegion dataRegion1 = wordDoc.OpenDataRegion("PO_userName");
        //为DataRegion赋值
        dataRegion1.Value = "张三";
        //设置字体样式
        dataRegion1.Font.Color = Color.Blue;
        dataRegion1.Font.Size = 24f;
        dataRegion1.Font.Name = "隶书";
        dataRegion1.Font.Bold = true;

        PageOffice.WordWriter.DataRegion dataRegion2 = wordDoc.OpenDataRegion("PO_deptName");
        dataRegion2.Value = "人事部";
        dataRegion2.Font.Color = Color.Red;

        PageOfficeCtrl1.SetWriter(wordDoc);
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "文档操作人姓名");
    }
}
