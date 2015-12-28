using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class ExaminationPaper_Savedoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.FileSaver fs = new PageOffice.FileSaver();
        fs.SaveToFile(Server.MapPath("doc/") + fs.FileName);
        //保存成功后，设置返回值，此处设置为：OK
        fs.CustomSaveResult = "OK";
        fs.Close();
    }
}
