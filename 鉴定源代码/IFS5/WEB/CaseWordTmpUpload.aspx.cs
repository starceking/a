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

public partial class CaseWordTmpUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string psb = Request.QueryString["psb"];
        string jtype = JusTypeXml.GetJTypeName(psb, Request.QueryString["jtype"]);
        string wtype = Request.QueryString["wtype"];
        switch (wtype)
        {
            case "1": wtype = "委托书"; break;
            case "2": wtype = "受理书"; break;
            case "3": wtype = "检验检查记录"; break;
            case "4": wtype = "意见报告书"; break;
            case "5": wtype = "封皮"; break;
            case "6": wtype = "其他"; break;
            case "7": wtype = "物证描述"; break;
            default:
                return;
        }
        string dir = AppDomain.CurrentDomain.BaseDirectory + "\\" + psb + "\\文书模版\\" + jtype + "\\" + wtype + "\\";
        Helper.CheckDir(dir);
        HttpFileCollection files = Request.Files;
        HttpPostedFile file = files[0];
        string fileName = files[0].FileName;
        if (!fileName.EndsWith(".doc")) return;
        if (wtype.Equals("物证描述")) fileName = "物证描述.doc";
        file.SaveAs(dir + fileName);
    }
}
