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
        PageOffice.WordWriter.WordDocument doc = new PageOffice.WordWriter.WordDocument();
        DataRegion d1 = doc.OpenDataRegion("d1");
        d1.Font.Color = Color.Green;//设置数据区域文本字体颜色
        d1.Font.Name = "华文彩云";//设置数据区域文本字体样式
        d1.Font.Size = 16;//设置数据区域文本字体大小
        d1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;//设置数据区域文本对齐方式
        
        
        DataRegion d2 = doc.OpenDataRegion("d2");
        d2.Font.Color = Color.MediumAquamarine;//设置数据区域文本字体颜色
        d2.Font.Name = "黑体";//设置数据区域文本字体样式
        d2.Font.Size = 14;//设置数据区域文本字体大小
        d2.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;//设置数据区域文本对齐方式

        DataRegion d3 = doc.OpenDataRegion("d3");
        d3.Font.Color = Color.Purple;//设置数据区域文本字体颜色
        d3.Font.Name = "华文行楷";//设置数据区域文本字体样式
        d3.Font.Size = 12;//设置数据区域文本字体大小
        d3.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;//设置数据区域文本对齐方式

        
        PageOfficeCtrl1.SetWriter(doc);

        //添加自定义按钮
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        //设置文档打开方式
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docSubmitForm, "张三");

    }
}
