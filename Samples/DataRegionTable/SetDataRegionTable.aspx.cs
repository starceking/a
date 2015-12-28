using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.WordWriter;

public partial class SetDataRegionTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //***************************卓正PageOffice组件的使用********************************
        WordDocument doc = new WordDocument();
        //打开数据区域
        DataRegion dTable = doc.OpenDataRegion("PO_table");
        //设置数据区域可编辑性
        dTable.Editing = true;
        //打开数据区域中的表格，OpenTable(index)方法中的index为word文档中表格的下标，从1开始
        PageOffice.WordWriter.Table table1 = doc.OpenDataRegion("PO_Table").OpenTable(1);
        // 给表头单元格赋值
        table1.OpenCellRC(1, 2).Value = "产品1";
        table1.OpenCellRC(1, 3).Value = "产品2";
        table1.OpenCellRC(2, 1).Value = "A部门";
        table1.OpenCellRC(3, 1).Value = "B部门";
        
        PageOfficeCtrl1.SetWriter(doc);

        //添加自定义按钮
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save", 1);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);
        //设置服务器页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        //设置保存页
        PageOfficeCtrl1.SaveDataPage = "SaveData.aspx";
        //设置文档打开方式
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docSubmitForm, "张佚名");
    }
}
