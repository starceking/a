using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;

public partial class ToolPage_Tip_SmallBarCodesPrint : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = new DataSet();

            if (Request.QueryString["sln"] != null)
            {
                DataTable dt = new DataTable(); ds.Tables.Add(dt);
                DataColumn dc = new DataColumn("材料编号", typeof(string)); dt.Columns.Add(dc);
                DataRow dr = dt.NewRow();
                dr[0] = Request.QueryString["sln"];
                dt.Rows.Add(dr);
            }
            else if (Request.QueryString["con"] != null)
            {
                //增加受理登记号、委托单位名称
                DataSet ajds = DBHelperSQL.Query("select * from 鉴定流程视图 where 委托编号='" + Request.QueryString["con"] + "'");
                string slno = string.Empty;
                string conpsb = string.Empty;
                if (ajds.Tables[0].Rows.Count > 0)
                {
                    slno = ajds.Tables[0].Rows[0]["案件编号"].ToString();
                    conpsb = ajds.Tables[0].Rows[0]["委托单位名称"].ToString();
                }

                string sc = Request.QueryString["sc"];
                string table = string.Empty;
                string filter = "委托编号='" + Request.QueryString["con"] + "'";
                string order = "样本编号 desc";
                switch (sc)
                {
                    case "1": table = "鉴定材料"; filter += " and 是否样本='0'"; order = "材料编号 desc"; break;
                    case "2": table = "鉴定材料"; filter += " and 是否样本='1'"; order = "材料编号 desc"; break;
                    case "3": table = "失踪亲属视图"; break;
                    case "4": table = "案件亲属视图"; filter += " and 库类型='受害人亲属'"; break;
                    case "5": table = "案件亲属视图"; filter += " and 库类型='嫌疑人亲属'"; break;
                    case "6": table = "涉案人员"; filter += " and 库类型='受害人'"; break;
                    case "7": table = "涉案人员"; filter += " and 库类型='嫌疑人'"; break;
                    case "8": table = "涉案人员"; filter += " and 库类型='其他人员'"; break;
                    case "9": table = "失踪人员"; break;
                    case "10": table = "无名尸体"; break;
                    case "11": table = "样本信息"; break;
                }
                string getter = order.Substring(0, 4) + " as 材料编号";
                if (sc.Equals("3") || sc.Equals("4") || sc.Equals("5"))
                {
                    order = string.Empty;
                    getter = "亲属一样本编号,亲属二样本编号";
                }

                //增加受理登记号、委托单位名称
                ds = DBHelperSQL.Select(table, filter, order, getter);
                DataColumn dc = new DataColumn("受理登记号", typeof(string));
                dc.DefaultValue = slno;
                ds.Tables[0].Columns.Add(dc);
                dc = new DataColumn("委托单位名称", typeof(string));
                dc.DefaultValue = conpsb;
                ds.Tables[0].Columns.Add(dc);

                if (sc.Equals("3") || sc.Equals("4") || sc.Equals("5"))
                {
                    DataSet ds2 = new DataSet();
                    DataTable dt = new DataTable(); ds2.Tables.Add(dt);
                    dc = new DataColumn("材料编号", typeof(string)); dt.Columns.Add(dc);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr2 = dt.NewRow();
                        dr2[0] = dr[0]; dt.Rows.Add(dr2);
                        if (dr[1].ToString().Length > 0)
                        {
                            dr2 = dt.NewRow();
                            dr2[0] = dr[1]; dt.Rows.Add(dr2);
                        }
                    }
                    ds = ds2;
                }
            }
            if (ds.Tables.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();

                Repeater2.DataSource = ds;
                Repeater2.DataBind();

                Repeater3.DataSource = ds;
                Repeater3.DataBind();
            }
        }
    }
}
