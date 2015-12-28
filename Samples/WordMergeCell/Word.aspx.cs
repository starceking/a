using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WordMergeCell_Word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/template.doc"),PageOffice.OpenModeType.docNormalEdit,"somebody");
    }
}
