using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using PageOffice.WordWriter;

public partial class ExaminationPaper_Compose2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_paper.mdb";
        string strSql = "select * from stream ";

        OleDbConnection conn = new OleDbConnection(strConn);
        OleDbCommand cmd = new OleDbCommand(strSql, conn);
        conn.Open();
        cmd.CommandType = CommandType.Text;
        OleDbDataReader reader = cmd.ExecuteReader();

        int id = 0;//记录ID
        string temp = "PO_begin1";//新添加的数据区域名称

        WordDocument doc = new WordDocument();
        if (reader.HasRows)
        {
            int num = 1;//题号
            while (reader.Read())
            {
                id = int.Parse(reader["ID"].ToString());
                string chk = Request.Form["check" + id];
                if (chk != null && chk.Equals("on", StringComparison.OrdinalIgnoreCase))
                {
                    if (id == 1)
                    {
                        DataRegion dataNum = doc.OpenDataRegion("PO_begin1");
                        dataNum.Value = "1.\t";
                        DataRegion dataReg = doc.CreateDataRegion("PO_begin" + (id + 1), DataRegionInsertType.After, "PO_begin1");
                        dataReg.Value = "[word]Openfile.aspx?id=" + id + "[/word]";
                    }
                    else
                    {
                        DataRegion dataNum = doc.CreateDataRegion("PO_" + num, DataRegionInsertType.After, temp);
                        dataNum.Value = num + ".\t";
                        DataRegion dataRegion = doc.CreateDataRegion("PO_begin" + (id + 1), DataRegionInsertType.After, "PO_" + num);
                        dataRegion.Value = "[word]Openfile.aspx?id=" + id + "[/word]";
                    }
                    num++;
                    temp = "PO_begin" + (id + 1);
                }
            }

        }

        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.SetWriter(doc);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.Caption = "生成试卷";
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docReadOnly, "somebody");

    }
}
