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

public partial class WJTXDown : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["tp"] == null)
            {
                return;
            }
            string fileName = string.Empty;
            fileName = Request.QueryString["tp"].ToString()+".doc";
            string dir = AppDomain.CurrentDomain.BaseDirectory + "1\\文件体系\\";
            Helper.CheckDir(dir);
            dir = dir + fileName;
            HttpFileCollection files = Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFile file = files[0];
                file.SaveAs(dir);
            }
            Response.Write(dir);
        }
    }
}
