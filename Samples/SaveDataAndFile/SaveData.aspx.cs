using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SaveDataAndFile_SaveData : System.Web.UI.Page
{
    protected string content = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.WordReader.WordDocument doc = new PageOffice.WordReader.WordDocument();
        //获取提交的数值
         String dataUserName = doc.OpenDataRegion("PO_userName").Value;
         String dataDeptName = doc.OpenDataRegion("PO_deptName").Value;
         String companyName = doc.GetFormField("txtCompany");
         /**获取到的公司名称,员工姓名,部门名称等内容可以保存到数据库,这里可以连接开发人员自己的数据库,例如连接sqlServer2008数据库。
          * string constr = "server=ACER-PC\\LI;database=db_test;uid=sa;pwd=123";
          * conn = new SqlConnection(constr);  //数据库连接  
          * conn.Open();
          * SqlCommand cmd = new SqlCommand("update user set userName='"+dataUserName+"',deptName='"+dataDeptName+"',companyName='"+companyName+"' where userId=1",conn);
          * cmd.ExecuteNonQuery();
          * conn.Close();
          * */

        doc.Close();
    }
}
