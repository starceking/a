using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;

public partial class CreateWord_CreateWord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.SaveFilePage = "SaveNewFile.aspx";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
        PageOfficeCtrl1.JsFunction_BeforeDocumentSaved = "BeforeDocumentSaved()";
        //创建文件
        PageOfficeCtrl1.WebCreateNew("somebody", PageOffice.DocumentVersion.Word2003);//可创建不同版本的word文件
    }

}
