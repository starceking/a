using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Stream : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx?id=1";
        PageOfficeCtrl1.AddCustomToolButton("保存","Save()",1);
        PageOfficeCtrl1.WebOpen("Openstream.aspx?id=1",PageOffice.OpenModeType.docRevisionOnly,"somebody");
    }
}
