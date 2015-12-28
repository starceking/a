using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OpenPDF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void PDFCtrl1_Load(object sender, EventArgs e)
    {
        //设置服务器页面
        PDFCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        //设置PDF文件的主题
        PDFCtrl1.Theme = PageOffice.ThemeType.CustomStyle;

        // 按键说明：光标键、Home、End、PageUp、PageDown可用来移动或翻页；数字键盘+、-用来放大缩小；数字键盘/、*用来旋转页面。
        //AddCustomToolButton方法中的三个参数分别为：按钮名称、按钮执行的JS函数、按钮图标的索引
        PDFCtrl1.AddCustomToolButton("打印", "Print()", 6);
        PDFCtrl1.AddCustomToolButton("-", "", 0);
        PDFCtrl1.AddCustomToolButton("实际大小", "SetPageReal()", 16);
        PDFCtrl1.AddCustomToolButton("适合页面", "SetPageFit()", 17);
        PDFCtrl1.AddCustomToolButton("适合宽度", "SetPageWidth()", 18);
        PDFCtrl1.AddCustomToolButton("-", "", 0);
        PDFCtrl1.AddCustomToolButton("首页", "FirstPage()", 8);
        PDFCtrl1.AddCustomToolButton("上一页", "PreviousPage()", 9);
        PDFCtrl1.AddCustomToolButton("下一页", "NextPage()", 10);
        PDFCtrl1.AddCustomToolButton("尾页", "LastPage()", 11);
        PDFCtrl1.AddCustomToolButton("-", "", 0);
        PDFCtrl1.AddCustomToolButton("左转", "RotateLeft()", 12);
        PDFCtrl1.AddCustomToolButton("右转", "RotateRight()", 13);
        PDFCtrl1.AddCustomToolButton("-", "", 0);
        PDFCtrl1.AddCustomToolButton("放大", "ZoomIn()", 14);
        PDFCtrl1.AddCustomToolButton("缩小", "ZoomOut()", 15);
        PDFCtrl1.AddCustomToolButton("-", "", 0);
        PDFCtrl1.AddCustomToolButton("全屏", "SwitchFullScreen()", 4);
        PDFCtrl1.AllowCopy = false;//是否允许拷贝

        string fileName = Request.QueryString["fileName"];
        //打开PDF文件
        PDFCtrl1.WebOpen("doc/" + fileName);
    }
}
