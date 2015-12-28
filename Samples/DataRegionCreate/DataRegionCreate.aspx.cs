using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateDataRegion_CreateDataRegion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        PageOffice.WordWriter.WordDocument wordDoc = new PageOffice.WordWriter.WordDocument();
        //CreateDataRegion方法的三个参数分别代表：将要新建数据区域处的标签的名称、DataRegion的插入位置、与将要创建的DataRegion相关联的书签名称
        //若打开的Word文档中尚无书签或者想在Word文档的开头新建数据区域，则第三那个参数使用“[home]”若想在结尾处新建使用“[end]”
        PageOffice.WordWriter.DataRegion dataRegion1 = wordDoc.CreateDataRegion("createDataRegion1", PageOffice.WordWriter.DataRegionInsertType.After, "[home]");
        //为创建的DataRegion赋值
        dataRegion1.Value = "新建DataRegion1\r\n";

        PageOffice.WordWriter.DataRegion dataRegion2 = wordDoc.CreateDataRegion("createDataRegion2", PageOffice.WordWriter.DataRegionInsertType.After, "createDataRegion1");
        dataRegion2.Value = "新建DataRegion2\r\n";

        PageOfficeCtrl1.SetWriter(wordDoc);
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "文档操作人姓名");
    }
}
