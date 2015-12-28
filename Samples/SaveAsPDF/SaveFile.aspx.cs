using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class savedoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义FileSaver对象
        PageOffice.FileSaver fs = new PageOffice.FileSaver();
        //保存Word文件
        fs.SaveToFile(Server.MapPath("doc/") + fs.FileName);
        //关闭FileSaver对象
        fs.Close();
    }
}
