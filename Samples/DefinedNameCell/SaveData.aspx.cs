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
       
        content += "testA1：" + sheet.OpenCellByDefinedName("testA1").Value + "<br/>";
        content += "testB1：" + sheet.OpenCellByDefinedName("testB1").Value + "<br/>";

        workBook.ShowPage(500, 400);
        workBook.Close();
    }
}
