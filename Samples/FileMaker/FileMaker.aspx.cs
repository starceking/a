using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FileMaker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //设置服务器页面
        FileMakerCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        string id = Request.QueryString["id"];
        if (id != null && id.Length > 0)
        {
            PageOffice.WordWriter.WordDocument doc = new PageOffice.WordWriter.WordDocument();
            //禁用右击事件
            doc.DisableWindowRightClick = true;
            //给数据区域赋值，即把数据填充到模板中相应的位置
            doc.OpenDataRegion("PO_company").Value = "北京卓正志远软件有限公司  " + id;
            //设置保存页面
            FileMakerCtrl1.SaveFilePage = "Savemaker.aspx?id=" + id;
            FileMakerCtrl1.SetWriter(doc);
            //设置转换完成后执行的JS函数
            FileMakerCtrl1.JsFunction_OnProgressComplete = "OnProgressComplete()";
            //打开文档
            FileMakerCtrl1.FillDocument(Server.MapPath("doc/template.doc"), PageOffice.DocumentOpenType.Word);
        }
    }
}
