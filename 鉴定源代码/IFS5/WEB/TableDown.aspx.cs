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

public partial class TableDown : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString["psb"] == null) ||
                (Request.QueryString["tp"] == null))
            {
                return;
            }
            string fileName = string.Empty;
            switch (Request.QueryString["tp"])
            {
                case "1":
                    fileName = "委托书修改申请表.doc"; break;
                case "2":
                    fileName = "鉴定书修改申请表.doc"; break;
                case "3":
                    fileName = "鉴定书遗失补发申请表.doc"; break;
                case "4":
                    fileName = "相关表格1.doc"; break;
                case "5":
                    fileName = "相关表格2.doc"; break;
                case "6":
                    fileName = "相关表格3.doc"; break;
                case "7":
                    fileName = "相关表格4.doc"; break;
                case "8":
                    fileName = "相关表格5.doc"; break;
                case "9":
                    fileName = "相关表格6.doc"; break;
                case "10":
                    fileName = "相关表格7.doc"; break;
                default:
                    return;
            }
            string dir = AppDomain.CurrentDomain.BaseDirectory + "\\" + Request.QueryString["psb"] + "\\表格下载\\";
            Helper.CheckDir(dir);
            dir = dir + fileName;
            HttpFileCollection files = Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFile file = files[0];
                file.SaveAs(dir);
            }
        }
    }
}
