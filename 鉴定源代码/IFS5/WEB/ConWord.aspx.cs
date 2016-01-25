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
using LIB;
using System.Collections.Generic;

public partial class ConWord : System.Web.UI.Page
{
    protected string barCode = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            barCode = Request.QueryString["id"].ToString();
            Bind(barCode);
        }
    }
    public void Bind(string CONSIGNMENT_NO)
    {
        Repeater1.DataSource = CaseFill(CONSIGNMENT_NO, "新的委托", "0");
        Repeater1.DataBind();
    }

    protected void Repeater1_ItemDataBound(object sender,
        RepeaterItemEventArgs e)
    {
        ;
    }

    public DataSet CaseFill(string 委托编号, string status, string isTesNote)
    {
        string filter = "委托编号='" + 委托编号 + "'";
        bool notTestStatus = false;
        if (status.Equals("新的委托") || status.Equals("不予受理"))
        {
            notTestStatus = true;
        }

        DataSet dataSet = new DataSet("NewDataSet");
        DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
        DataColumn dc = new DataColumn("条形码", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("委托编号", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("委托单位名称", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("委托单位地址", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("委托单位邮编", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("委托单位电话", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("一送姓名", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("一送警号", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("一送电话", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("二送姓名", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("二送警号", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("二送电话", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("一送职务", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("原鉴定情况", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("委托年份", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("委托序号", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("委托时间", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("鉴定类别", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("鉴定项目", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("鉴定要求", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("文书名称", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("受理年份", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("受理序号", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("发文年份", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("发文序号", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("受理时间", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("计划完成", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("受理意见", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("鉴定结论", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("结论概述", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("受理人员姓名", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("一检姓名", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("一检技术职称", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("二检姓名", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("二检技术职称", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("三检姓名", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("三检技术职称", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("四检姓名", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("四检技术职称", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("复核姓名", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("复核技术职称", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("签字姓名", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("签字技术职称", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("技管姓名", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("领导姓名", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("打防管控", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("现场勘验", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("案件名称", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("案件类型", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("案件编号", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("发案地点", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("发案时间", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("案件性质", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("简要案情", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("被鉴定人姓名", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("被鉴定人性别", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("被鉴定人身份证", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("被鉴定人电话", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("被鉴定人出生日期", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("被鉴定人年龄", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("被鉴定人职业", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("被鉴定人学历", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("被鉴定人籍贯", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("被鉴定人工作地点", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("被鉴定人现住址", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("检材样本", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("今天", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("今天大写", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("一检人", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("二检人", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("三检人", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("四检人", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("复核人", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("授权签字", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("技管", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("领导", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("一检完成", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("二检完成", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("三检完成", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("四检完成", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("复核完成", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("签字完成", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("技管完成", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("审批完成", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("样本编号", typeof(string)); dataTable.Columns.Add(dc);
        dc = new DataColumn("样本名称", typeof(string)); dataTable.Columns.Add(dc);

        dc = new DataColumn("鉴定单位名称", typeof(string)); dataTable.Columns.Add(dc);

        DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);
        int tesAmount = 0;
        int intID = 1;

        DataSet ds = DBHelperSQL.Select("鉴定流程视图", "委托编号='" + 委托编号 + "'", "委托编号", "*");
        DataRow dr = ds.Tables[0].Rows[0];
        dataRow["条形码"] = "*" + dr["委托编号"] + "*";
        foreach (DataColumn dataCol in ds.Tables[0].Columns)
        {
            if (dataCol.ColumnName.Equals("委托时间") || dataCol.ColumnName.Equals("受理时间") || dataCol.ColumnName.Equals("计划完成") ||
                dataCol.ColumnName.Equals("一检完成") || dataCol.ColumnName.Equals("二检完成") || dataCol.ColumnName.Equals("三检完成") ||
                dataCol.ColumnName.Equals("四检完成") || dataCol.ColumnName.Equals("复核完成") || dataCol.ColumnName.Equals("签字完成") ||
                dataCol.ColumnName.Equals("技管完成") || dataCol.ColumnName.Equals("审批完成") || dataCol.ColumnName.Equals("被鉴定人出生日期"))
            {
                dataRow[dataCol.ColumnName] = Helper.GetSmallDateChinese(dr[dataCol.ColumnName].ToString());
            }
            else if (dataTable.Columns.Contains(dataCol.ColumnName))
                dataRow[dataCol.ColumnName] = dr[dataCol.ColumnName];
        }
        if (dataRow["委托单位电话"].ToString().Length == 0)
            dataRow["委托单位电话"] = "无";

        if (委托编号.StartsWith("C") || 委托编号.StartsWith("D"))
        {
            ds = DBHelperSQL.Select("案件信息", "委托编号='" + 委托编号 + "'", "委托编号", "*");
            dr = ds.Tables[0].Rows[0];

            string caseId = dr["ID"].ToString();
            string srcId = dr["SRCID"].ToString();

            foreach (DataColumn dataCol in ds.Tables[0].Columns)
            {
                if (dataCol.ColumnName.Equals("发案时间"))
                {
                    dataRow[dataCol.ColumnName] = Helper.GetSmallDate(dr[dataCol.ColumnName].ToString());
                }
                else if (dataTable.Columns.Contains(dataCol.ColumnName))
                    dataRow[dataCol.ColumnName] = dr[dataCol.ColumnName];
            }
            if (委托编号.StartsWith("C"))
            {
                ds = DBHelperSQL.Select("案件人员", "委托编号='" + 委托编号 + "'", "创建时间", "*");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    foreach (DataColumn dataCol in ds.Tables[0].Columns)
                    {
                        string dcol = "被鉴定人" + dataCol.ColumnName;
                        if (dataTable.Columns.Contains(dcol))
                            dataRow[dcol] = dr[dataCol.ColumnName];
                    }
                }
                ds = DBHelperSQL.Select("鉴定材料", "委托编号='" + 委托编号 + "'", "创建时间", "*");
                string tesDes = string.Empty;
                string tesCtrl = string.Empty;
                foreach (DataRow dr0 in ds.Tables[0].Rows)
                {
                    string sl = dr0["数量"].ToString().Length > 0 ? dr0["数量"].ToString() : string.Empty;
                    string zl = dr0["重量"].ToString().Length > 0 ? (",重" + dr0["重量"] + dr0["单位"]) : string.Empty;
                    string bz = dr0["包装"].ToString().Length > 0 ? ("用" + ((dr0["包装"].ToString().EndsWith("包装") || dr0["包装"].ToString().EndsWith("封装")) ? dr0["包装"].ToString() + "的" : dr0["包装"].ToString() + "包装的")) : string.Empty;
                    string beiz = dr0["备注"].ToString().Length > 0 ? ("(" + dr0["备注"] + ")") : string.Empty;

                    string desTmp = (dr0["材料编号"].ToString().Equals("受理后自动生成") ? string.Empty : dr0["材料编号"]) + " " + bz +
                        dr0["名称"] + sl + zl + beiz + "<br/>";
                    if (dr0["是否样本"].ToString().Equals("0")) tesDes += desTmp;
                    else tesCtrl += desTmp;

                    tesAmount++;
                }
                if ((!isTesNote.Equals("1")) && notTestStatus && (tesAmount > 15))
                {
                    dataRow["检材样本"] = "详见材料单！";
                }
                else
                {
                    if ((tesDes.Length > 0) && (tesCtrl.Length > 0))
                    {
                        tesDes = "检材：<br/>" + tesDes;
                        tesCtrl = "样本：<br/>" + tesCtrl;
                        dataRow["检材样本"] = tesDes + tesCtrl;
                    }
                    else
                        dataRow["检材样本"] = tesDes;
                }
            }
            else if (委托编号.StartsWith("D"))
            {
                filter = "委托编号='" + 委托编号 + "'";
                if (status.Equals("3") && caseId.Equals(srcId))
                {
                    filter = "案件ID='" + srcId + "'";
                }
                string tesDes = string.Empty;

                DataSet saryds = DBHelperSQL.Select("涉案人员", filter, "创建时间", "*");
                DataRow[] drs = saryds.Tables[0].Select("库类型='受害人'", "创建时间");
                foreach (DataRow dr0 in drs)
                {
                    //受害人
                    //string bz = dr0["包装情况"].ToString().Length > 0 ? ("用" + ((dr0["包装情况"].ToString().EndsWith("包装") || dr0["包装情况"].ToString().EndsWith("封装")) ? dr0["包装情况"].ToString() + "的" : dr0["包装情况"].ToString() + "包装的")) : string.Empty;
                    tesDes += intID.ToString() + "、" + dr0["库类型"] + dr0["姓名"] + "，" + dr0["性别"] +
                        (dr0["身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr0["身份证"]) +
                        "，" + dr0["样本类型"] + " " + dr0["样本描述"] + "<br/>";
                    tesAmount++; intID++;
                }

                ds = DBHelperSQL.Select("样本信息", filter, "创建时间", "*");
                foreach (DataRow dr0 in ds.Tables[0].Rows)
                {
                    //物证检材信息
                    //string sl = dr0["数量"].ToString().Length > 0 ? ("," + dr0["数量"]) : string.Empty;
                    //string bz = dr0["样本包装"].ToString().Length > 0 ? ("用" + ((dr0["样本包装"].ToString().EndsWith("包装") || dr0["样本包装"].ToString().EndsWith("封装")) ? dr0["样本包装"].ToString() + "的" : dr0["样本包装"].ToString() + "包装的")) : string.Empty;
                    tesDes += intID.ToString() + "、" + dr0["名称"] + "，" + dr0["样本类型"] + "<br/>";
                    tesAmount++; intID++;
                }

                drs = saryds.Tables[0].Select("库类型='嫌疑人'", "创建时间");
                foreach (DataRow dr0 in drs)
                {
                    //嫌疑人
                    //string bz = dr0["包装情况"].ToString().Length > 0 ? ("用" + ((dr0["包装情况"].ToString().EndsWith("包装") || dr0["包装情况"].ToString().EndsWith("封装")) ? dr0["包装情况"].ToString() + "的" : dr0["包装情况"].ToString() + "包装的")) : string.Empty;
                    tesDes += intID.ToString() + "、" + dr0["库类型"] + dr0["姓名"] + "，" + dr0["性别"] +
                        (dr0["身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr0["身份证"]) +
                        "，" + dr0["样本类型"] + " " + dr0["样本描述"] + "<br/>";
                    tesAmount++; intID++;
                }

                DataRow[] qtrydrs = saryds.Tables[0].Select("库类型='其他人员'", "创建时间");

                ds = DBHelperSQL.Select("无名尸体", filter, "创建时间", "*");
                foreach (DataRow dr0 in ds.Tables[0].Rows)
                {
                    //无名尸体
                    //string bz = dr0["包装情况"].ToString().Length > 0 ? ("用" + ((dr0["包装情况"].ToString().EndsWith("包装") || dr0["包装情况"].ToString().EndsWith("封装")) ? dr0["包装情况"].ToString() + "的" : dr0["包装情况"].ToString() + "包装的")) : string.Empty;
                    tesDes += intID.ToString() + "、" + dr0["姓名"] + "，" + dr0["性别"] + "，" + dr0["样本类型"] + " " + dr0["样本描述"] + "<br/>";
                    tesAmount++; intID++;
                }
                ds = DBHelperSQL.Select("案件亲属视图", filter, "创建时间", "*");
                drs = ds.Tables[0].Select("库类型='受害人亲属'", "创建时间");
                foreach (DataRow dr0 in drs)
                {
                    //受害人亲属
                    tesDes += intID.ToString() + "、" + GetRelativeDes(dr0) + "<br/>";
                    tesAmount++; intID++;
                }
                drs = ds.Tables[0].Select("库类型='嫌疑人亲属'", "创建时间");
                foreach (DataRow dr0 in drs)
                {
                    //嫌疑人亲属
                    tesDes += intID.ToString() + "、" + GetRelativeDes(dr0) + "<br/>";
                    tesAmount++; intID++;
                }

                foreach (DataRow dr0 in qtrydrs)
                {
                    //其他人员
                    //string bz = dr0["样本包装"].ToString().Length > 0 ? ("用" + (dr0["样本包装"].ToString().EndsWith("包装") ? dr0["样本包装"].ToString() + "的" : dr0["样本包装"].ToString() + "包装的")) : string.Empty;
                    tesDes += intID.ToString() + "、" + dr0["库类型"] + dr0["姓名"] + "，" + dr0["性别"] +
                        (dr0["身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr0["身份证"]) +
                        "，" + dr0["样本类型"] + "<br/>";
                    tesAmount++; intID++;
                }

                if ((!isTesNote.Equals("1")) && notTestStatus && (tesAmount > 15))
                {
                    dataRow["检材样本"] = "详见材料单！";
                }
                else
                {
                    dataRow["检材样本"] = tesDes;
                }
            }
        }
        else if (委托编号.StartsWith("R"))
        {
            ds = DBHelperSQL.Select("失踪亲属视图", "委托编号='" + 委托编号 + "'", "委托编号", "*");
            dr = ds.Tables[0].Rows[0];
            dataRow["案件名称"] = dr["案件名称"];
            dataRow["简要案情"] = dr["简要案情"];
            dataRow["检材样本"] = GetRelativeDes(dr);

        }
        else if (委托编号.StartsWith("L"))
        {
            ds = DBHelperSQL.Select("失踪人员", "委托编号='" + 委托编号 + "'", "委托编号", "*");
            dr = ds.Tables[0].Rows[0];
            dataRow["案件名称"] = dr["案件名称"];
            dataRow["检材样本"] = "失踪人" + dr["姓名"] + " 的 " + dr["样本类型"] +
                (dr["身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr["身份证"]);
        }

        dataRow["今天"] = Helper.GetSmallDateChinese(DateTime.Today.ToShortDateString());
        dataRow["今天大写"] = Helper.GetFullDate(DateTime.Today.ToShortDateString());

        return dataSet;
    }
    private static string GetRelativeDes(DataRow dr)
    {
        if (dr["亲属关系"].Equals("单亲"))
        {
            return dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + " 的 " + dr["亲属一目标关系"].ToString() +
                dr["亲属一姓名"] + " 的 " + dr["亲属一样本类型"] + dr["亲属一样本描述"] +
                (dr["亲属一身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr["亲属一身份证"]) +
                "<br/>";
        }
        else
        {
            string qs2 = dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + " 的 " + dr["亲属二目标关系"].ToString() +
                dr["亲属二姓名"] + " 的 " + dr["亲属二样本类型"] + dr["亲属二样本描述"] +
                (dr["亲属二身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr["亲属二身份证"]);
            return dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + " 的 " + dr["亲属一目标关系"].ToString() +
                    dr["亲属一姓名"] + " 的 " + dr["亲属一样本类型"] + dr["亲属一样本描述"] +
                    (dr["亲属一身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr["亲属一身份证"]) +
                    "<br/>" + qs2;
        }
    }
}
