using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected string url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        url = Server.MapPath("doc");
    }
}
