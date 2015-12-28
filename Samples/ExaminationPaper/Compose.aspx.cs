using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.OleDb;
using System.Data;

public partial class ExaminationPaper_Compose : System.Web.UI.Page
{
    protected string operateStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        int pNum = 1;

        operateStr += "function Create(){\n";
        // document.getElementById('PageOfficeCtrl1').Document.Application 微软office VBA对象的根Application对象
        operateStr += "var obj = document.getElementById('PageOfficeCtrl1').Document.Application;\n";
        operateStr += "obj.Selection.EndKey(6);\n"; // 定位光标到文档末尾

        for (int i = 10; i > 0; i--)
        {
            string a = "on";
            string c = Request.Form["check" + i.ToString()];

            if (a.Equals(c))
            {
                operateStr += "obj.Selection.TypeParagraph();"; //用来换行
                operateStr += "obj.Selection.Range.Text = '" + pNum.ToString() + ".';\n"; // 用来生成题号
                // 下面两句代码用来移动光标位置
                operateStr += "obj.Selection.EndKey(5,1);\n";
                operateStr += "obj.Selection.MoveRight(1,1);\n";
                // 插入指定的题到文档中
                operateStr += "document.getElementById('PageOfficeCtrl1').InsertDocumentFromURL('Openfile.aspx?id=" + i + "');\n";
                pNum++;

            }
        }
        operateStr += "\n}\n";

        // 设置PageOffice组件服务页面

        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.JsFunction_AfterDocumentOpened = "Create";
        PageOfficeCtrl1.Caption = "生成试卷";
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docReadOnly, "somebody");

    }
}
