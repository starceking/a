using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubmitWord_SaveFilePage : System.Web.UI.Page
{
    protected string content = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.ExcelReader.Workbook workBook = new PageOffice.ExcelReader.Workbook();
        PageOffice.ExcelReader.Sheet sheet = workBook.OpenSheet("Sheet1");
        PageOffice.ExcelReader.Table table = sheet.OpenTable("B4:D8");

        while (!table.EOF)
        {
            //获取提交的数值
            //DataFields.Count标识的是table的列数
            if (!table.DataFields.IsEmpty)
            {
                content += "<br/>月份名称：" + table.DataFields[0].Text;
                content += "<br/>计划完成量：" + table.DataFields[1].Text;
                content += "<br/>实际完成量：" + table.DataFields[2].Text;

                content += "<br/>*********************************************";
            }
            //循环进入下一行
            table.NextRow();
        }
        table.Close();

        workBook.ShowPage(500, 400);
        workBook.Close();
    }
}
