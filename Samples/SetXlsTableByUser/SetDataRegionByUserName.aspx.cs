using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.ExcelWriter;

public partial class SetDataRegionByUserName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["userName"];
        //***************************卓正PageOffice组件的使用********************************
        Workbook wb = new Workbook();
        Sheet sheet = wb.OpenSheet("Sheet1");
        PageOffice.ExcelWriter.Table tableA = sheet.OpenTable("C4:D6");
        PageOffice.ExcelWriter.Table tableB = sheet.OpenTable("C7:D9");

        tableA.SubmitName = "tableA";
        tableB.SubmitName = "tableB";

        //根据登录用户名设置数据区域可编辑性
        //A部门经理登录后
        if (userName.Equals("zhangsan"))
        {
            Literal1.Text = "A部门经理，所以只能编辑A部门的产品数据";
            tableA.ReadOnly = false;
            tableB.ReadOnly = true;
        }
        //B部门经理登录后
        else
        {
            Literal1.Text = "B部门经理，所以只能编辑B部门的产品数据";
            tableA.ReadOnly = true;
            tableB.ReadOnly = false;
        }

        PageOfficeCtrl1.SetWriter(wb);

   
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save", 1);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx"; //此行必须
        PageOfficeCtrl1.SaveDataPage = "SaveData.aspx";//保存数据
        //PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";//如果需要保存文档，添加此句代码
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsSubmitForm, userName);
    }
}
