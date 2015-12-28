using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SaveAsPDF : System.Web.UI.Page
{
    protected string pdfName = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        //PageOffice组件的引用
        //首先确保已安装了pageoffice服务器端组件，且在项目中已添加了pageoffice文件夹，
        //在该文件夹中已存在posetup.exe和server.aspx服务器端页面，再在前台页面中引入PageOfficeCtrl控件

        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        //设置保存页
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        //添加自定义按钮
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
        PageOfficeCtrl1.AddCustomToolButton("另存为PDF文件", "SaveAsPDF()", 1);
        string fileName = "template.doc";
        //定义将要转换的PDF文件的名称
        pdfName = fileName.Substring(0, fileName.Length - 4) + ".pdf";
        //打开Word文件
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.docNormalEdit, "张佚名");
    }
}
