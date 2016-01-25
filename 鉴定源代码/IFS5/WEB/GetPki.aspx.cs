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
using DAL;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

public partial class GetPki : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.IsSecureConnection && Request.ClientCertificate.IsPresent)
        {
            string strcer = Request.ClientCertificate["SUBJECTCN"];//subjectcn 内容 如： 姓名 身份证
            string[] arraysubjectcn = strcer.Split(new char[] { ' ' });

            if (arraysubjectcn.Length > 1)
            {
                string IdentityCard = arraysubjectcn[1].ToString();

                Response.Write(DBHelperSQL.Select("系统用户", "身份证='" + IdentityCard + "'", "ID", "*").GetXml());
            }
        }
        else
        {
            Response.Write(string.Empty);
        }
    }
}
