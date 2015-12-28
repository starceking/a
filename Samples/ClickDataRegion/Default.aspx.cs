using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.WordWriter;
using System.Drawing;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WordDocument doc = new WordDocument();
        DataRegion dataReg = doc.OpenDataRegion("PO_deptName");
        //为方便用户知道哪些地方可以编辑，所以设置了数据区域的背景色
        dataReg.Shading.BackgroundPatternColor = Color.Pink;
        //dataReg.Editing = true;
        
        PageOfficeCtrl1.SetWriter(doc); // 不要忘记此句代码

        // 设置数据区域点击时的响应js函数
        PageOfficeCtrl1.JsFunction_OnWordDataRegionClick = "OnWordDataRegionClick()";

        PageOfficeCtrl1.AddCustomToolButton("保存", "Save", 1);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        PageOfficeCtrl1.OfficeToolbars = false;
        PageOfficeCtrl1.Caption = "为方便用户知道哪些地方可以编辑，所以设置了数据区域的背景色";
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docSubmitForm, "张三");
    }
}
