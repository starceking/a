using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class WordParagraph_savedoc : System.Web.UI.Page
{
    public int id = 0;
    public string userName = "";
    public int age = 0;
    public string sex = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //定义FileSaver对象
        PageOffice.FileSaver fs = new PageOffice.FileSaver();
        //保存Word文内容到本地磁盘中
        fs.SaveToFile(Server.MapPath("doc/") + fs.FileName);
        

        //获取通过Url传递过来的值
        if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
            id = int.Parse(Request.QueryString["id"].Trim());

        //获取通过网页标签控件传递过来的参数值，注意fs.GetFormField("HTML标签的name名称")方法中的参数名是指标签的“name”属性而不是Id

        //获取通过文本框<input type="text" />标签传递过来的值
        if (fs.GetFormField("userName") != null && fs.GetFormField("userName").Trim().Length > 0)
        {
            userName = fs.GetFormField("userName");
        }

        //获取通过隐藏域传递过来的值
        if (fs.GetFormField("age") != null && fs.GetFormField("age").Trim().Length > 0)
        {
            age = int.Parse(fs.GetFormField("age"));
        }

        //获取通过<select>标签传递过来的值
        if (fs.GetFormField("selSex") != null && fs.GetFormField("selSex").Trim().Length > 0)
        {
            sex = fs.GetFormField("selSex");
        }

        string connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_param.mdb";
        OleDbConnection conn = new OleDbConnection(connstring);
        conn.Open();

        string strsql;
        OleDbCommand cmd = new OleDbCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        strsql = "Update Users set UserName = '" + userName + "', age = " + age + ",sex = '" + sex + "' where id = " + id;
        cmd.CommandText = strsql;
        cmd.ExecuteNonQuery();
        conn.Close();

        fs.ShowPage(300,200); // 显示一下SaveFile.aspx获取到的所有参数的值
        //关闭FileSaver对象
        fs.Close();

    }
}
