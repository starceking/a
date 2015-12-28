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
        PageOffice.WordReader.WordDocument doc = new PageOffice.WordReader.WordDocument();
        //获取提交的数值
        PageOffice.WordReader.DataRegion dataUserName = doc.OpenDataRegion("PO_userName");
        PageOffice.WordReader.DataRegion dataDeptName = doc.OpenDataRegion("PO_deptName");
        content += "公司名称：" + doc.GetFormField("txtCompany");
        content += "<br/>员工姓名：" + dataUserName.Value ;
        content += "<br/>部门名称：" + dataDeptName.Value;

        doc.ShowPage(400, 300);
        doc.Close();
    }
}
