using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

public partial class ExaminationPaper_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        string id = Request.QueryString["id"];
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx?id=" + id;
        PageOfficeCtrl1.WebOpen("Openfile.aspx?id=" + id, PageOffice.OpenModeType.docNormalEdit, "someBody");
    }
}
