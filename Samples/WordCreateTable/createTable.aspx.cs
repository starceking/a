using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.WordWriter;

public partial class WordCreateTable_createTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WordDocument doc = new WordDocument();
        PageOffice.WordWriter.Table table1 = doc.OpenDataRegion("PO_table1").CreateTable(3, 5, WdAutoFitBehavior.wdAutoFitWindow);
        table1.OpenCellRC(1, 1).MergeTo(3, 1);
        table1.OpenCellRC(1, 1).Value="合并后的单元格";
        //给表格table1中剩余的单元格赋值
        for (int i = 1; i < 4; i++)
        {
            table1.OpenCellRC(i, 2).Value = "AA" + i.ToString();
            table1.OpenCellRC(i, 3).Value = "BB" + i.ToString();
            table1.OpenCellRC(i, 4).Value = "CC" + i.ToString();
            table1.OpenCellRC(i, 5).Value = "DD" + i.ToString();
        }

       //在"PO_table1"后面动态创建一个新的数据区域"PO_table2",用于创建新的一个5行5列的表格table2
        PageOffice.WordWriter.DataRegion drTable2 = doc.CreateDataRegion("PO_table2", DataRegionInsertType.After, "PO_table1");
        PageOffice.WordWriter.Table table2 = drTable2.CreateTable(5, 5, WdAutoFitBehavior.wdAutoFitWindow);
        //给新表格table2赋值
        for (int i = 1; i < 6; i++)
        {
            table2.OpenCellRC(i, 1).Value = "AA" + i.ToString();
            table2.OpenCellRC(i, 2).Value = "BB" + i.ToString();
            table2.OpenCellRC(i, 3).Value = "CC" + i.ToString();
            table2.OpenCellRC(i, 4).Value = "DD" + i.ToString();
            table2.OpenCellRC(i, 5).Value = "EE" + i.ToString();
        }

        PageOfficeCtrl1.CustomToolbar=false;//隐藏用户自定义工具栏
        PageOfficeCtrl1.SetWriter(doc);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen("doc/createTable.doc", PageOffice.OpenModeType.docNormalEdit, "张佚名");
    }
   
}