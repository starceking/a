using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class SaveFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = Request.QueryString["path"];
        if (path == null || path.Length == 0) return;

        PageOffice.FileSaver fs = new PageOffice.FileSaver();
        fs.SaveToFile(HttpUtility.UrlDecode(path) + fs.FileName);
        fs.Close();
    }
}
