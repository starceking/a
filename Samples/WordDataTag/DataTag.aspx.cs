using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class WordDataTag_DataTag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        
        //定义WordDocument对象
        PageOffice.WordWriter.WordDocument doc = new PageOffice.WordWriter.WordDocument();
        //定义DataTag对象
        PageOffice.WordWriter.DataTag deptTag = doc.OpenDataTag("{部门名}");
        deptTag.Font.Color = Color.Green;
        //给DataTag对象赋值
        deptTag.Value = "技术";

        PageOffice.WordWriter.DataTag userTag = doc.OpenDataTag("{姓名}");
        userTag.Font.Color = Color.Green;
        userTag.Value = "李四";

        PageOffice.WordWriter.DataTag dateTag = doc.OpenDataTag("【时间】");
        dateTag.Font.Color = Color.Blue;
        dateTag.Value = DateTime.Now.ToString("yyyy-MM-dd");


        PageOfficeCtrl1.SetWriter(doc);
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        //打开Word文件
        PageOfficeCtrl1.WebOpen("doc/test2.doc", PageOffice.OpenModeType.docNormalEdit, "张佚名");
    }
}
