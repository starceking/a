using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LIB;

public partial class DocModUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.QueryString["uni"] == null) || (Request.QueryString["uni"].Length == 0)) return;
        string dir = AppDomain.CurrentDomain.BaseDirectory + "\\" + Request.QueryString["psb"] + "\\鉴定档案\\" +
            Request.QueryString["conno"] + "\\文书修改\\";
        Helper.CheckDir(dir);
        HttpFileCollection files = Request.Files;
        HttpPostedFile file = files[0];
        string fileName = Request.QueryString["uni"] + ".rar";
        file.SaveAs(dir + fileName);
    }
}
