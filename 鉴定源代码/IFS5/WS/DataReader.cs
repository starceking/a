using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LIB;
using DAL;
using DNADAL;
using System.Collections.Generic;

namespace WS
{
    public class DataReader
    {
        public static void CaseFill(string 委托编号, FillWord fw, string status, string isTesNote, string wordName)
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
            dc = new DataColumn("受理序号", typeof(string)); dataTable.Columns.Add(dc);//---------------------------------
            dc = new DataColumn("案件序号", typeof(string)); dataTable.Columns.Add(dc);//---------------------------------
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
            dc = new DataColumn("补送号", typeof(string)); dataTable.Columns.Add(dc);

            dc = new DataColumn("鉴定方法", typeof(string)); dataTable.Columns.Add(dc);

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
                string srcNum = dr["案件编号"].ToString();

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
                            dr0["名称"] + sl + zl + beiz + "\n";
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
                            tesDes = "检材：\n" + tesDes;
                            tesCtrl = "样本：\n" + tesCtrl;
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
                    else if (wordName.Contains("DNA鉴定书") || wordName.Contains("DNA检验鉴定报告") || wordName.Contains("DNA检验意见书"))
                    {
                        //同案检材
                        filter = "案件ID='" + srcId + "'";
                        //同案送检人、受理时间
                        DataSet ajds = DBHelperSQL.Select("案件视图", "srcid='" + srcId + "'", "委托编号", "*");
                        string ysxm = ajds.Tables[0].Rows[0]["一送姓名"].ToString();
                        string esxm = ajds.Tables[0].Rows[0]["二送姓名"].ToString();
                        string slsj = string.Empty;
                        foreach (DataRow bsdr in ajds.Tables[0].Rows)
                        {
                            if (!ysxm.Contains(bsdr["一送姓名"].ToString()) && !esxm.Contains(bsdr["一送姓名"].ToString()))
                                ysxm += "、" + bsdr["一送姓名"].ToString();
                            if (!ysxm.Contains(bsdr["二送姓名"].ToString()) && !esxm.Contains(bsdr["二送姓名"].ToString()))
                                ysxm += "、" + bsdr["二送姓名"].ToString();

                            slsj += Helper.GetSmallDateChinese(bsdr["受理时间"].ToString()) + "、";
                        }
                        dataRow["一送姓名"] = ysxm;
                        dataRow["二送姓名"] = esxm;
                        dataRow["受理时间"] = slsj.Substring(0, slsj.Length - 1);

                    }
                    string tesDes = string.Empty;

                    DataSet saryds = DBHelperSQL.Select("涉案人员", filter, "创建时间", "*");
                    DataRow[] drs = saryds.Tables[0].Select("库类型='受害人'", "创建时间");
                    //DNA 受害人 样本描述
                    if (wordName.Contains("DNA鉴定书") || wordName.Contains("DNA检验鉴定报告") || wordName.Contains("DNA检验意见书"))
                    {
                        foreach (DataRow dr0 in drs)
                        {
                            //受害人
                            tesDes += intID.ToString() + "、标记有“" + dr0["库类型"] + dr0["姓名"] + "的" + dr0["样本类型"] + "”字样的" +
                                (dr0["包装情况"].ToString().Length == 0 ? "物证包装袋" : dr0["包装情况"]) +
                                (dr0["样本描述"].ToString().Length == 0 ? string.Empty : "内装有" + dr0["样本描述"]) +
                                (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，取适量标记为" + dr0["样本编号"] + "号检材")) + "。\n";
                            tesAmount++; intID++;
                        }
                    }
                    else
                    {
                        foreach (DataRow dr0 in drs)
                        {
                            //受害人
                            tesDes += intID.ToString() + "、" + dr0["库类型"] + dr0["姓名"] + "，" + dr0["性别"] +
                                (dr0["身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr0["身份证"]) +
                                "，" + dr0["样本类型"] + " " + dr0["样本描述"] +
                                (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，标记为" + dr0["样本编号"] + "号检材")) + "。\n";
                            tesAmount++; intID++;
                        }
                    }

                    ds = DBHelperSQL.Select("样本信息", filter, "创建时间", "*");

                    //DNA 物证检材信息 样本描述
                    if (wordName.Contains("DNA鉴定书") || wordName.Contains("DNA检验鉴定报告") || wordName.Contains("DNA检验意见书"))
                    {
                        foreach (DataRow dr0 in ds.Tables[0].Rows)
                        {
                            //物证检材信息
                            tesDes += intID.ToString() + "、标记有“" + dr0["名称"] + "”字样的" +
                                (dr0["样本包装"].ToString().Length == 0 ? "物证包装袋" : dr0["样本包装"]);
                            if (dr0["承载物"].ToString().Contains("未知") || dr0["承载物"].ToString().Contains("其他")) tesDes += "内装有" + dr0["名称"];
                            else tesDes += "内装有" + dr0["承载物"];
                            tesDes += (dr0["数量"].ToString().Length == 0 ? "1" : dr0["数量"]);
                            if (dr0["承载物"].ToString().Contains("棉签")) tesDes += "支";
                            else if (dr0["承载物"].ToString().Contains("卡")) tesDes += "份";
                            else if (dr0["样本类型"].ToString().Contains("烟蒂")) tesDes += "枚";
                            else tesDes += "个";
                            if (dr0["样本类型"].ToString().Contains("血")) tesDes += "，表面粘有红褐色斑迹";
                            else if (dr0["承载物"].ToString().Contains("棉签")) tesDes += "，表面粘有浅黄色斑迹";
                            tesDes += (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，取适量标记为" + dr0["样本编号"] + "号检材")) + "。\n";
                            tesAmount++; intID++;
                        }
                    }
                    else
                    {
                        foreach (DataRow dr0 in ds.Tables[0].Rows)
                        {
                            //物证检材信息
                            tesDes += intID.ToString() + "、" + dr0["名称"] + "，" + dr0["样本类型"] + (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，标记为" + dr0["样本编号"] + "号检材")) + "。\n";
                            tesAmount++; intID++;
                        }
                    }

                    drs = saryds.Tables[0].Select("库类型='嫌疑人'", "创建时间");

                    //DNA 嫌疑人 样本描述
                    if (wordName.Contains("DNA鉴定书") || wordName.Contains("DNA检验鉴定报告") || wordName.Contains("DNA检验意见书"))
                    {
                        foreach (DataRow dr0 in drs)
                        {
                            //嫌疑人
                            tesDes += intID.ToString() + "、标记有“" + dr0["库类型"] + dr0["姓名"] + "的" + dr0["样本类型"] + "”字样的" +
                                (dr0["包装情况"].ToString().Length == 0 ? "物证包装袋" : dr0["包装情况"]) +
                                (dr0["样本描述"].ToString().Length == 0 ? string.Empty : "内装有" + dr0["样本描述"]) +
                                (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，取适量标记为" + dr0["样本编号"] + "号检材")) + "。\n";
                            tesAmount++; intID++;
                        }
                    }
                    else
                    {
                        foreach (DataRow dr0 in drs)
                        {
                            //嫌疑人
                            tesDes += intID.ToString() + "、" + dr0["库类型"] + dr0["姓名"] + "，" + dr0["性别"] +
                                (dr0["身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr0["身份证"]) +
                                "，" + dr0["样本类型"] + " " + dr0["样本描述"] +
                                (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，标记为" + dr0["样本编号"] + "号检材")) + "。\n";
                            tesAmount++; intID++;
                        }
                    }

                    DataRow[] qtrydrs = saryds.Tables[0].Select("库类型='其他人员'", "创建时间");

                    ds = DBHelperSQL.Select("无名尸体", filter, "创建时间", "*");

                    //DNA 无名尸体 样本描述
                    if (wordName.Contains("DNA鉴定书") || wordName.Contains("DNA检验鉴定报告") || wordName.Contains("DNA检验意见书"))
                    {
                        foreach (DataRow dr0 in ds.Tables[0].Rows)
                        {
                            //无名尸体
                            tesDes += intID.ToString() + "、标记有“" + dr0["姓名"] + "的" + dr0["样本类型"] + "”字样的" +
                                (dr0["包装情况"].ToString().Length == 0 ? "物证包装袋" : dr0["包装情况"]) +
                                (dr0["样本描述"].ToString().Length == 0 ? string.Empty : "内装有" + dr0["样本描述"]) +
                                (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，取适量标记为" + dr0["样本编号"] + "号检材")) + "。\n";
                            tesAmount++; intID++;
                        }
                    }
                    else
                    {
                        foreach (DataRow dr0 in ds.Tables[0].Rows)
                        {
                            //无名尸体
                            tesDes += intID.ToString() + "、" + dr0["姓名"] + "，" + dr0["性别"] +
                                "，" + dr0["样本类型"] + " " + dr0["样本描述"] +
                                (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，标记为" + dr0["样本编号"] + "号检材")) + "。\n";
                            tesAmount++; intID++;
                        }
                    }

                    ds = DBHelperSQL.Select("案件亲属视图", filter, "创建时间", "*");
                    drs = ds.Tables[0].Select("库类型='受害人亲属'", "创建时间");
                    //DNA 受害人亲属 样本描述
                    if (wordName.Contains("DNA鉴定书") || wordName.Contains("DNA检验鉴定报告") || wordName.Contains("DNA检验意见书"))
                    {
                        foreach (DataRow dr0 in drs)
                        {
                            //受害人亲属
                            tesDes += intID.ToString() + "、" + GetRelativeDesMiaoShu(dr0) + "。\n";
                            tesAmount++; intID++;
                        }
                    }
                    else
                    {
                        foreach (DataRow dr0 in drs)
                        {
                            //受害人亲属
                            tesDes += intID.ToString() + "、" + GetRelativeDes(dr0) + "。\n";
                            tesAmount++; intID++;
                        }
                    }

                    drs = ds.Tables[0].Select("库类型='嫌疑人亲属'", "创建时间");

                    //DNA 受害人亲属 样本描述
                    if (wordName.Contains("DNA鉴定书") || wordName.Contains("DNA检验鉴定报告") || wordName.Contains("DNA检验意见书"))
                    {
                        foreach (DataRow dr0 in drs)
                        {
                            //嫌疑人亲属
                            tesDes += intID.ToString() + "、" + GetRelativeDesMiaoShu(dr0) + "。\n";
                            tesAmount++; intID++;
                        }
                    }
                    else
                    {
                        foreach (DataRow dr0 in drs)
                        {
                            //嫌疑人亲属
                            tesDes += intID.ToString() + "、" + GetRelativeDes(dr0) + "。\n";
                            tesAmount++; intID++;
                        }
                    }

                    //DNA 其他人员 样本描述
                    if (wordName.Contains("DNA鉴定书") || wordName.Contains("DNA检验鉴定报告") || wordName.Contains("DNA检验意见书"))
                    {
                        foreach (DataRow dr0 in qtrydrs)
                        {
                            //其他人员
                            tesDes += intID.ToString() + "、标记有“" + dr0["库类型"] + dr0["姓名"] + "的" + dr0["样本类型"] + "”字样的" +
                                (dr0["包装情况"].ToString().Length == 0 ? "物证包装袋" : dr0["包装情况"]) +
                                (dr0["样本描述"].ToString().Length == 0 ? string.Empty : "内装有" + dr0["样本描述"]) +
                                (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，取适量标记为" + dr0["样本编号"] + "号检材")) + "。\n";
                            tesAmount++; intID++;
                        }
                    }
                    else
                    {
                        foreach (DataRow dr0 in qtrydrs)
                        {
                            //其他人员
                            tesDes += intID.ToString() + "、" + dr0["库类型"] + dr0["姓名"] +
                                (dr0["身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr0["身份证"]) +
                                "，" + dr0["性别"] + "，" + dr0["样本类型"] +
                                (dr0["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，标记为" + dr0["样本编号"] + "号检材")) + "。\n";
                            tesAmount++; intID++;
                        }
                    }


                    if ((!isTesNote.Equals("1")) && notTestStatus && (tesAmount > 999))
                    {
                        dataRow["检材样本"] = "详见材料单！";
                    }
                    else
                    {
                        dataRow["检材样本"] = tesDes.Substring(0, tesDes.Length - 1);
                    }
                }
            }
            else if (委托编号.StartsWith("R"))
            {
                ds = DBHelperSQL.Select("失踪亲属视图", "委托编号='" + 委托编号 + "'", "委托编号", "*");
                dr = ds.Tables[0].Rows[0];
                dataRow["案件名称"] = dr["案件名称"];
                string casenum = string.Empty;
                if (dr["案件序号"].ToString().Length == 1) casenum = "000" + dr["案件序号"].ToString();
                else if (dr["案件序号"].ToString().Length == 2) casenum = "00" + dr["案件序号"].ToString();
                else if (dr["案件序号"].ToString().Length == 3) casenum = "0" + dr["案件序号"].ToString();
                else casenum = dr["案件序号"].ToString();
                dataRow["案件编号"] = dr["受理年份"] + "B" + casenum;
                dataRow["简要案情"] = dr["简要案情"];
                dataRow["检材样本"] = GetRelativeDes(dr);

            }
            else if (委托编号.StartsWith("L"))
            {
                ds = DBHelperSQL.Select("失踪人员视图", "委托编号='" + 委托编号 + "'", "委托编号", "*");
                dr = ds.Tables[0].Rows[0];
                dataRow["案件名称"] = dr["案件名称"];
                string casenum = string.Empty;
                if (dr["案件序号"].ToString().Length == 1) casenum = "000" + dr["案件序号"].ToString();
                else if (dr["案件序号"].ToString().Length == 2) casenum = "00" + dr["案件序号"].ToString();
                else if (dr["案件序号"].ToString().Length == 3) casenum = "0" + dr["案件序号"].ToString();
                else casenum = dr["案件序号"].ToString();
                dataRow["案件编号"] = dr["受理年份"] + "B" + casenum;
                dataRow["检材样本"] = "失踪人" + dr["姓名"] + "的" + dr["样本类型"] +
                     (dr["身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr["身份证"]) +
                    (dr["样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，标记为" + dr["样本编号"] + "号检材"));
            }

            dataRow["今天"] = Helper.GetSmallDateChinese(DateTime.Today.ToShortDateString());
            dataRow["今天大写"] = Helper.GetFullDate(DateTime.Today.ToShortDateString());

            if (!委托编号.StartsWith("C"))
            {
                //大连DNA样本检测

            }

            fw.FillBookMarks(dataSet);
        }

        private static string GetRelativeDes(DataRow dr)
        {
            if (dr["亲属关系"].Equals("单亲"))
            {
                return dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + "的" + dr["亲属一目标关系"].ToString() +
                     dr["亲属一姓名"] + "的" + dr["亲属一样本类型"] + dr["亲属一样本描述"] +
                     (dr["亲属一身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr["亲属一身份证"]) +
                    (dr["亲属一样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，标记为" + dr["亲属一样本编号"] + "号检材")) + "。\n";
            }
            else
            {
                string qs2 = dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + "的" + dr["亲属二目标关系"].ToString() +
                     dr["亲属二姓名"] + "的" + dr["亲属二样本类型"] + dr["亲属二样本描述"] +
                     (dr["亲属二身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr["亲属二身份证"]) +
                    (dr["亲属二样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，标记为" + dr["亲属二样本编号"] + "号检材"));
                return dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + "的" + dr["亲属一目标关系"].ToString() +
                       dr["亲属一姓名"] + "的" + dr["亲属一样本类型"] + dr["亲属一样本描述"] +
                       (dr["亲属一身份证"].ToString().Length == 0 ? string.Empty : "，身份证：" + dr["亲属一身份证"]) +
                      (dr["亲属一样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，标记为" + dr["亲属一样本编号"] + "号检材")) + "。\n" + qs2;
            }
        }
        private static string GetRelativeDesMiaoShu(DataRow dr)
        {
            if (dr["亲属关系"].Equals("单亲"))
            {
                return dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + "的" + dr["亲属一目标关系"].ToString() +
                     dr["亲属一姓名"] + "的" + dr["亲属一样本类型"] + dr["亲属一样本描述"] +
                    (dr["亲属一样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，取适量标记为" + dr["亲属一样本编号"] + "号检材")) + "。\n";
            }
            else
            {
                string qs2 = dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + "的" + dr["亲属二目标关系"].ToString() +
                     dr["亲属二姓名"] + "的" + dr["亲属二样本类型"] + dr["亲属二样本描述"] +
                    (dr["亲属二样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，取适量标记为" + dr["亲属二样本编号"] + "号检材"));
                return dr["库类型"].ToString().Substring(0, 3) + dr["姓名"] + "的" + dr["亲属一目标关系"].ToString() +
                       dr["亲属一姓名"] + "的" + dr["亲属一样本类型"] + dr["亲属一样本描述"] +
                      (dr["亲属一样本编号"].ToString().Equals("受理后自动生成") ? string.Empty : ("，取适量标记为" + dr["亲属一样本编号"] + "号检材")) + "。\n" + qs2;
            }
        }
        public static string GetNextSLN(string preFix, string tableName)
        {
            //int next = GetNextByPrefix(DBHelperSQL.Select(tableName, "样本编号 like '" + preFix + "%'", "样本编号 desc", "样本编号"), preFix);
            //辽宁修改编号 （yyyy-FW-cccc-YBnnn）（yyyy-FW-cccc-JCnnn）
            int next = GetNextByPrefix(DBHelperSQL.Select("样本视图", "样本编号 like '" + preFix + "%'", "样本编号 desc", "样本编号"), preFix);
            string strSQL = string.Empty;
            if (next == 1)
            {
                if (tableName.Equals("样本视图"))
                {
                    strSQL = string.Format(@"
select sample_lab_no from gdna.CASE_PERSONNEL_SAMPLE where sample_lab_no like '{0}%' and (DELETE_FLAG is null or DELETE_FLAG='0') union
select sample_lab_no from gdna.MISSING_PERSON where sample_lab_no like '{0}%' and (DELETE_FLAG is null or DELETE_FLAG='0') union
select sample_lab_no from gdna.RELATIVE where sample_lab_no like '{0}%' and (DELETE_FLAG is null or DELETE_FLAG='0') union
select sample_lab_no from gdna.SCENE_EVIDENCE where sample_lab_no like '{0}%' and (DELETE_FLAG is null or DELETE_FLAG='0') union
select sample_lab_no from gdna.UNKNOWN_DECEASED where sample_lab_no like '{0}%' and (DELETE_FLAG is null or DELETE_FLAG='0') order by sample_lab_no desc", preFix);
                }
                else
                {
                    switch (tableName)
                    {
                        case "样本信息": tableName = "SCENE_EVIDENCE"; break;
                        case "涉案人员": tableName = "CASE_PERSONNEL_SAMPLE"; break;
                        case "亲属信息": tableName = "RELATIVE"; break;
                        case "无名尸体": tableName = "UNKNOWN_DECEASED"; break;
                        case "失踪人员": tableName = "MISSING_PERSON"; break;
                    }
                    strSQL = "select sample_lab_no from gdna." + tableName + " where sample_lab_no like '" + preFix +
                       "%' and (DELETE_FLAG is null or DELETE_FLAG='0') order by sample_lab_no desc";
                }
                next = DataReader.GetNextByPrefix(DBHelperOracle.Query(strSQL), preFix);
            }
            return next.ToString();
        }
        public static int GetNextByPrefix(DataSet ds, string preFix)
        {
            int maxnum = 1;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int max = 1;
                    if (Int32.TryParse(dr[0].ToString().Substring(preFix.Length), out max))
                    {
                        max++;
                        if (max > maxnum) maxnum = max;
                    }
                }
            }
            return maxnum;
        }
        public static bool ValidSlnDup(string sln, string ID)
        {
            if (sln.Equals("受理后自动生成")) return true;
            return DBHelperSQL.SelectRowCount("样本视图", "ID<>'" + ID + "' AND 样本编号='" + sln + "'", "样本编号") == 0;
        }
        public static bool ValidSlnDup2(string sln, string ID, string sln2, string ID2)
        {
            if (sln.Equals("受理后自动生成")) return true;
            if (ID2.Length == 0) return ValidSlnDup(sln, ID);
            return DBHelperSQL.SelectRowCount("样本视图", "(ID<>'" + ID + "' AND 样本编号='" + sln + "') or (ID<>'" + ID2 + "' AND 样本编号='" + sln2 + "')", "样本编号") == 0;
        }
        public static bool ValidAccNo(string 鉴定单位, string 委托编号, string 文书名称, string 受理年份, string 受理序号)
        {
            return DBHelperSQL.SelectRowCount("鉴定流程", "鉴定单位='" + 鉴定单位 + "' and 委托编号<>'" + 委托编号 + "' AND 文书名称='" + 文书名称 +
                "' AND 受理年份='" + 受理年份 + "' AND 受理序号='" + 受理序号 + "'", "委托编号") == 0;
        }
    }
}
