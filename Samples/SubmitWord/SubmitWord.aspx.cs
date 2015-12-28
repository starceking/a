using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class SubmitWord_SubmitWord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        PageOffice.WordWriter.WordDocument wordDoc = new PageOffice.WordWriter.WordDocument();

        //打开数据区域，OpenDataRegion方法的参数代表Word文档中的书签名称
        PageOffice.WordWriter.DataRegion dataRegion1 = wordDoc.OpenDataRegion("PO_userName");
        //设置DataRegion的可编辑性
        dataRegion1.Editing = true;
        //为DataRegion赋值,此处的值可在页面中打开Word文档后在自己进行修改
        dataRegion1.Value = "";

        PageOffice.WordWriter.DataRegion dataRegion2 = wordDoc.OpenDataRegion("PO_deptName");
        dataRegion2.Editing = true;
        dataRegion2.Value = "";

        PageOfficeCtrl1.SetWriter(wordDoc);
        PageOfficeCtrl1.SaveDataPage = "SaveData.aspx";
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
        //如果需要提交数据，必须将WebOpen的第二个参数OpenModeType设置为docSubmitForm
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docSubmitForm, "文档操作人姓名");
    }
}
