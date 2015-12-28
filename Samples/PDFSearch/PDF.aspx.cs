using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PDF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void PDFCtrl1_Load(object sender, EventArgs e)
    {
        PDFCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PDFCtrl1.Theme = PageOffice.ThemeType.Office2007;
        PDFCtrl1.AddCustomToolButton("搜索", "SearchText()", 0);
        PDFCtrl1.AddCustomToolButton("搜索下一个", "SearchTextNext()", 0);
        PDFCtrl1.AddCustomToolButton("搜索上一个", "SearchTextPrev()", 0);
        PDFCtrl1.AddCustomToolButton("实际大小", "SetPageReal()", 16);
        PDFCtrl1.AddCustomToolButton("适合页面", "SetPageFit()", 17);
        PDFCtrl1.AddCustomToolButton("适合宽度", "SetPageWidth()", 18);
        PDFCtrl1.WebOpen("doc/test.pdf");
    }
}
