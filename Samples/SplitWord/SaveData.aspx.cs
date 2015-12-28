using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

public partial class SplitWord_SaveData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.WordReader.WordDocument doc = new PageOffice.WordReader.WordDocument();
        Byte[] bWord;

        // 读取数据区域PO_test1中的内容，保存为一个新的word文档：new1.doc
        PageOffice.WordReader.DataRegion dr1 = doc.OpenDataRegion("PO_test1");
        bWord = dr1.FileBytes;
        Stream s1 = new FileStream(Server.MapPath("doc/") + "new1.doc", FileMode.Create);
        s1.Write(bWord, 0, bWord.Length);
        s1.Close();

        // 读取数据区域PO_test2中的内容，保存为一个新的word文档：new2.doc
        PageOffice.WordReader.DataRegion dr2 = doc.OpenDataRegion("PO_test2");
        bWord = dr2.FileBytes;
        Stream s2 = new FileStream(Server.MapPath("doc/") + "new2.doc", FileMode.Create);
        s2.Write(bWord, 0, bWord.Length);
        s2.Close();

        // 读取数据区域PO_test3中的内容，保存为一个新的word文档：new3.doc
        PageOffice.WordReader.DataRegion dr3 = doc.OpenDataRegion("PO_test3");
        bWord = dr3.FileBytes;
        Stream s3 = new FileStream(Server.MapPath("doc/") + "new3.doc", FileMode.Create);
        s3.Write(bWord, 0, bWord.Length);
        s3.Close();

        doc.Close();

    }
}
