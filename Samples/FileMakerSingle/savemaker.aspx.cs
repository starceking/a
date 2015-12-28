using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class savemaker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.FileSaver fs = new PageOffice.FileSaver();
        string fileName = "maker" + fs.FileExtName;
        fs.SaveToFile(Server.MapPath("doc/") + fileName);
        fs.Close();
    }
}
