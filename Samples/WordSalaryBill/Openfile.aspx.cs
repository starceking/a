using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.WordWriter;
using System.Data.OleDb;
using System.Data;

public partial class Openfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Length > 0)
        {
            string id = Request.QueryString["ID"].Trim();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_salary.mdb";
            string strSql = "select * from Salary where id =" + id + " order by ID";

            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbCommand cmd = new OleDbCommand(strSql, conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                decimal result = 0;
                DateTime date = DateTime.Now;

                //创建WordDocment对象
                WordDocument doc = new WordDocument();
                //打开数据区域
                DataRegion datareg = doc.OpenDataRegion("PO_table");

                //给数据区域赋值
                doc.OpenDataRegion("PO_ID").Value = id;

                //设置数据区域的可编辑性
                doc.OpenDataRegion("PO_UserName").Editing = true;
                doc.OpenDataRegion("PO_DeptName").Editing = true;
                doc.OpenDataRegion("PO_SalTotal").Editing = true;
                doc.OpenDataRegion("PO_SalDeduct").Editing = true;
                doc.OpenDataRegion("PO_SalCount").Editing = true;
                doc.OpenDataRegion("PO_DataTime").Editing = true;

                doc.OpenDataRegion("PO_UserName").Value = reader["UserName"].ToString();
                doc.OpenDataRegion("PO_DeptName").Value = reader["DeptName"].ToString();

                if (reader["SalTotal"] != null && decimal.TryParse(reader["SalTotal"].ToString(), out result))
                {
                    doc.OpenDataRegion("PO_SalTotal").Value = decimal.Parse(reader["SalTotal"].ToString()).ToString("c");
                }
                else
                {
                    doc.OpenDataRegion("PO_SalTotal").Value = "￥0.00";
                }

                if (reader["SalDeduct"] != null && decimal.TryParse(reader["SalDeduct"].ToString(), out result))
                {
                    doc.OpenDataRegion("PO_SalDeduct").Value = int.Parse(reader["SalDeduct"].ToString()).ToString("c");
                }
                else
                {
                    doc.OpenDataRegion("PO_SalDeduct").Value = "￥0.00";
                }

                if (reader["SalCount"] != null && decimal.TryParse(reader["SalCount"].ToString(), out result))
                {
                    doc.OpenDataRegion("PO_SalCount").Value = int.Parse(reader["SalCount"].ToString()).ToString("c");
                }
                else
                {
                    doc.OpenDataRegion("PO_SalCount").Value = "￥0.00";
                }

                if (reader["DataTime"] != null && DateTime.TryParse(reader["DataTime"].ToString(), out date))
                {
                    doc.OpenDataRegion("PO_DataTime").Value = DateTime.Parse(reader["DataTime"].ToString()).ToString("yyyy-MM-dd");
                }
                else
                {
                    doc.OpenDataRegion("PO_DataTime").Value = DateTime.Now.ToString("yyyy-MM-dd");
                }

                PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
                PageOfficeCtrl1.SaveDataPage = "SaveData.aspx?id=" + id;
                PageOfficeCtrl1.SetWriter(doc);
            }
            else
            {
                Page.RegisterClientScriptBlock("", "<script>alert('未获得该员工的工资信息！');location.href='Default.aspx'</script>");
            }
            reader.Close();
            conn.Close();
        }
        else
        {
            Page.RegisterClientScriptBlock("", "<script>alert('未获得该工资信息的ID！');location.href='Default.aspx'</script>");
        }

        PageOfficeCtrl1.WebOpen("doc/template.doc", PageOffice.OpenModeType.docSubmitForm, "someBody");
    }
}
