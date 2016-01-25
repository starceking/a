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

public partial class CaseDataUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = string.Empty;
        switch (Request.QueryString["type"])
        {
            case "0": type = "照片图像"; break;
            case "1": type = "电子材料"; break;
            case "2": type = "物证照片"; break;
            default:
                return;
        }

        string dir = AppDomain.CurrentDomain.BaseDirectory + "\\" + Request.QueryString["psb"] + "\\鉴定档案\\" + Request.QueryString["conno"] + "\\" + type + "\\";
        Helper.CheckDir(dir);
        HttpFileCollection files = Request.Files;
        HttpPostedFile file = files[0];
        string fileName = files[0].FileName;
        if (Request.QueryString["sln"] != null)
        {
            fileName = Request.QueryString["sln"] + ".jpg";
        }
        file.SaveAs(dir + fileName);

        if (type.Equals("照片图像"))
        {
            Helper.GenerateImage(System.Drawing.Image.FromFile(dir + fileName), 160, 160, dir, Helper.GetTbName(fileName), false);
        }
    }
}
