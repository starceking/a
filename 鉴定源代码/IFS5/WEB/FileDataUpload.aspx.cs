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

public partial class FileDataUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dir = string.Empty;
        if (Request.QueryString["type"].Equals("0"))
        {
            dir = AppDomain.CurrentDomain.BaseDirectory + "\\" + Request.QueryString["psb"] + "\\共享文件\\" + Request.QueryString["offs"] + "\\";
        }
        else
        {
            dir = AppDomain.CurrentDomain.BaseDirectory + "\\" + Request.QueryString["psb"] + "\\共享文件\\公用文件\\";
        }
        Helper.CheckDir(dir);
        HttpFileCollection files = Request.Files;
        HttpPostedFile file = files[0];
        string fileName = files[0].FileName;

        file.SaveAs(dir + fileName);
    }
}
