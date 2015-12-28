using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class SaveData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.WordReader.WordDocument doc = new PageOffice.WordReader.WordDocument();

        if (Request.QueryString["userName"] != null && Request.QueryString["userName"].Equals("zhangsan"))
        {
            saveBytesToFile(doc.OpenDataRegion("PO_com1").FileBytes, Server.MapPath("doc/content1.doc"));
        }
        else
        {
            saveBytesToFile(doc.OpenDataRegion("PO_com2").FileBytes, Server.MapPath("doc/content2.doc"));
        }

        doc.Close();
    }
    private void saveBytesToFile(byte[] bytes, string filePath)
    {
        FileStream fs = new FileStream(filePath, System.IO.FileMode.OpenOrCreate);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
    }

}
