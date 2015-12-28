using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FileMaker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

            PageOffice.WordWriter.WordDocument doc = new PageOffice.WordWriter.WordDocument();
            //禁用右击事件
            doc.DisableWindowRightClick = true;
            //给数据区域赋值，即把数据填充到模板中相应的位置
            doc.OpenDataRegion("PO_company").Value = "北京卓正志远软件有限公司" ;

            FileMakerCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
            FileMakerCtrl1.SaveFilePage = "Savemaker.aspx";
            FileMakerCtrl1.SetWriter(doc);
            FileMakerCtrl1.JsFunction_OnProgressComplete = "OnProgressComplete()";
            FileMakerCtrl1.FileTitle = "newfilename.doc";
            FileMakerCtrl1.FillDocument(Server.MapPath("doc/template.doc"), PageOffice.DocumentOpenType.Word);
    }
}
