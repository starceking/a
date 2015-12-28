using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using PageOffice.WordWriter;

public partial class _Default : System.Web.UI.Page
{
    public string mb = "";//获取选择的模版名称
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        //首次加载时，加载“正文文件”：test.doc
        if(!IsPostBack){
            string fileName = "test.doc";
            OpenWord(fileName);
        }
        
    }

    /// <summary>
    /// 打开word文件
    /// </summary>
    /// <param name="fileName"></param>
    private void OpenWord(string fileName)
    {
        //***************************卓正PageOffice组件的使用********************************
        Session.Add("fileName", fileName);//保存word文件名称
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);
        PageOfficeCtrl1.SaveFilePage = "Savefile.aspx";
        PageOfficeCtrl1.WebOpen("doc/" + fileName, PageOffice.OpenModeType.docNormalEdit, "张三");
    }

   /// <summary>
   /// 套红
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    protected void BtnTaoHong_Click(object sender, EventArgs e)
    {
        //套红模板时，复制模板文件为“正式发文文件”（zhengshi.doc），填充数据和“正文文件”到“正式发文文件”

        //复制模板，命名为正式发文的文件名：zhengshi.doc
        string fileName = "zhengshi.doc";
        mb = Request.Form["templateName"];
        System.IO.File.Copy(Server.MapPath("doc/" + mb),
                Server.MapPath("doc/" + fileName), true);

        //给正式发文的文件填充数据
        WordDocument doc = new WordDocument();
        DataRegion copies = doc.OpenDataRegion("PO_Copies");
        copies.Value = "6";
        DataRegion docNum = doc.OpenDataRegion("PO_DocNum");
        docNum.Value = "001";
        DataRegion issueDate = doc.OpenDataRegion("PO_IssueDate");
        issueDate.Value = "2013-5-30";
        DataRegion issueDept = doc.OpenDataRegion("PO_IssueDept");
        issueDept.Value = "开发部";
        DataRegion sTextS = doc.OpenDataRegion("PO_STextS");
        sTextS.Value = "[word]doc/test.doc[/word]"; // 把正文文件插入到正式文件中
        DataRegion sTitle = doc.OpenDataRegion("PO_sTitle");
        sTitle.Value = "北京某公司文件";
        DataRegion topicWords = doc.OpenDataRegion("PO_TopicWords");
        topicWords.Value = "Pageoffice、 套红";
        PageOfficeCtrl1.SetWriter(doc);

        //打开word文件
        OpenWord(fileName); 
    }
}
