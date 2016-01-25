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

public partial class SysUserPicUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.QueryString["uni"] == null) || (Request.QueryString["uni"].Length == 0)) return;
        string dir = AppDomain.CurrentDomain.BaseDirectory + "\\LabFile\\系统用户\\";
        Helper.CheckDir(dir);
        HttpFileCollection files = Request.Files;
        HttpPostedFile file = files[0];
        string fileName = Request.QueryString["uni"] + ".jpg";
        file.SaveAs(dir + fileName);
    }
}
