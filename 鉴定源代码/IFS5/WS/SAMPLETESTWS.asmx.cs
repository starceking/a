using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using DAL;
using System.Collections.Generic;
using LIB;
using System.Configuration;
using System.IO;

namespace WS
{
    /// <summary>
    /// Summary description for SAMPLETESTWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class SAMPLETESTWS : System.Web.Services.WebService
    {
        [WebMethod]
        public string QueryExtractSample(string ID)
        {
            string filter = string.Empty;
            string[] Ids = ID.Split(new char[] { '，' }, StringSplitOptions.RemoveEmptyEntries);
            if (Ids.Length >= 1)
            {
                foreach (string id in Ids)
                {
                    filter += "提取记录ID='" + id + "' or ";
                }
                filter += "(" + filter.Substring(0, filter.Length - 4) + ")";
            }
            else
            {
                filter = "提取记录ID='" + ID + "'";
            }

            DataSet ds = DBHelperSQL.Select("样本提取", filter, "ID", "*");

            //取消重复
            //for (int i = 0; i < (ds.Tables[0].Rows.Count - 1); i++)
            //{
            //    for (int j = i + 1; j < ds.Tables[0].Rows.Count; j++)
            //    {
            //        if (ds.Tables[0].Rows[i]["样本编号"].ToString().Equals(ds.Tables[0].Rows[j]["样本编号"].ToString()))
            //        {
            //            ds.Tables[0].Rows.RemoveAt(j); j--;
            //        }
            //    }

            //}

            return ds.GetXml();
        }
        [WebMethod]
        public string QueryChunhua(string 提取记录ID)
        {
            return DBHelperSQL.Select("提取视图", "提取记录ID='" + 提取记录ID + "'", "样本编号", "*").GetXml();
        }
        [WebMethod]
        public string QuerySampleTest(string ID)
        {
            return DBHelperSQL.Select("样本扩增", "扩增电泳ID='" + ID + "'", "ID", "*").GetXml();
        }
        [WebMethod]
        public string QueryWorkStation(string ID)
        {
            return DBHelperSQL.Select("样本扩增", "工作站模式ID='" + ID + "'", "ID", "*").GetXml();
        }
        [WebMethod]
        public string QueryTestimony(string 鉴定单位, string 提取人, string 样本编号, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (提取人.Length > 0) filter += "提取人='" + 提取人 + "' and ";
            return DBHelperSQL.SelectRowCount("扩增电泳视图", "样本编号='" + 样本编号 + "'", "ID", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string QueryExtract(string 鉴定单位, string 提取人, string 日期s, string 日期e, string 工作站模式, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (工作站模式.Length > 0) filter += "工作站模式='" + 工作站模式 + "' and ";
            if (提取人.Length > 0) filter += "提取人='" + 提取人 + "' and ";
            if (日期s.Length > 0) filter += "日期>='" + 日期s + "' and ";
            if (日期e.Length > 0) filter += "日期<='" + 日期e + "' and ";

            return DBHelperSQL.SelectRowCount("提取记录", Helper.CutFilter(filter), "日期 desc", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }

        [WebMethod]
        public string QuerySample(string 鉴定单位, string 提取人, string 日期s, string 日期e, string 批号, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (提取人.Length > 0) filter += "提取人='" + 提取人 + "' and ";
            if (日期s.Length > 0) filter += "扩增时间开始>='" + 日期s + "' and ";
            if (日期e.Length > 0) filter += "扩增时间结束<='" + 日期e + "' and ";
            if (批号.Length > 0) filter += "扩增电泳批号 like '%" + 批号 + "%' and ";

            return DBHelperSQL.SelectRowCount("扩增电泳", Helper.CutFilter(filter), "扩增时间开始 desc", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }

        [WebMethod]
        public string InsertTQ(string ID, string 离心机, string 移液器, string 加热仪, string 恒温混匀仪,
            string 漩涡混合器, string 水浴, string 显微镜, string 工作站, string 日期)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("离心机", 离心机);
            dict.Add("移液器", 移液器);
            dict.Add("加热仪", 加热仪);
            return DBHelperSQL.Insert("提取记录", dict);
        }

        [WebMethod]
        public string UpdateTQ(string ID, string 离心机, string 移液器, string 加热仪, string 恒温混匀仪,
            string 漩涡混合器, string 水浴, string 显微镜, string 工作站, string 日期)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("离心机", 离心机);
            dict.Add("移液器", 移液器);
            dict.Add("加热仪", 加热仪);
            dict.Add("恒温混匀仪", 恒温混匀仪);
            dict.Add("漩涡混合器", 漩涡混合器);
            dict.Add("水浴", 水浴);
            dict.Add("显微镜", 显微镜);
            dict.Add("工作站", 工作站);
            dict.Add("日期", 日期);
            return DBHelperSQL.Update("提取记录", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string UpdateKZ(string ID, string 扩增仪, string 扩增离心机, string 扩增漩涡混合器, string 扩增移液器,
            string 扩增超净台, string 扩增工作站, string 质控样本, string 扩增时间开始, string 扩增时间结束, string 扩增方法,
            string 试剂盒批号, string 扩增体系, string 扩增模板, string 循环数, string 环境温度, string 环境湿度, string 扩增电泳批号)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("扩增仪", 扩增仪);
            dict.Add("扩增离心机", 扩增离心机);
            dict.Add("扩增漩涡混合器", 扩增漩涡混合器);
            dict.Add("扩增移液器", 扩增移液器);
            dict.Add("扩增超净台", 扩增超净台);
            dict.Add("扩增工作站", 扩增工作站);
            dict.Add("质控样本", 质控样本);
            dict.Add("扩增时间开始", 扩增时间开始);
            dict.Add("扩增时间结束", 扩增时间结束);
            dict.Add("扩增方法", 扩增方法);
            dict.Add("试剂盒批号", 试剂盒批号);
            dict.Add("扩增体系", 扩增体系);
            dict.Add("扩增模板", 扩增模板);
            dict.Add("循环数", 循环数);
            dict.Add("环境温度", 环境温度);
            dict.Add("环境湿度", 环境湿度);
            dict.Add("扩增电泳批号", 扩增电泳批号);
            return DBHelperSQL.Update("扩增电泳", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string UpdateDY(string ID, string 电泳仪, string 电泳离心机, string 电泳加热仪,
                                    string 电泳漩涡混合器, string 电泳移液器, string 电泳制冰机, string 电泳超净台,
                                    string 电泳工作站, string 内标, string 内标量, string 检测时间开始, string 检测时间结束,
                                    string 变性溶剂, string 溶剂量, string 产物量, string 胶液, string 胶液批号, string 预电泳电流,
                                    string 电泳电流, string SampleSheet, string RunFold, string 电泳环境温度, string 电泳环境湿度)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("电泳仪", 电泳仪);
            dict.Add("电泳离心机", 电泳离心机);
            dict.Add("电泳加热仪", 电泳加热仪);
            dict.Add("电泳漩涡混合器", 电泳漩涡混合器);
            dict.Add("电泳移液器", 电泳移液器);
            dict.Add("电泳制冰机", 电泳制冰机);
            dict.Add("电泳超净台", 电泳超净台);
            dict.Add("电泳工作站", 电泳工作站);
            dict.Add("内标", 内标);
            dict.Add("内标量", 内标量);
            dict.Add("检测时间开始", 检测时间开始);
            dict.Add("检测时间结束", 检测时间结束);
            dict.Add("变性溶剂", 变性溶剂);
            dict.Add("溶剂量", 溶剂量);
            dict.Add("产物量", 产物量);
            dict.Add("胶液", 胶液);
            dict.Add("胶液批号", 胶液批号);
            dict.Add("预电泳电流", 预电泳电流);
            dict.Add("电泳电流", 电泳电流);
            dict.Add("SampleSheet", SampleSheet);
            dict.Add("RunFold", RunFold);
            dict.Add("电泳环境温度", 电泳环境温度);
            dict.Add("电泳环境湿度", 电泳环境湿度);
            return DBHelperSQL.Update("扩增电泳", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string DeleteExtract(string ID)
        {
            string sql = "delete from 提取记录 where ID='" + ID + "';delete from 样本提取 where 提取记录ID='" + ID + "'";
            return DBHelperSQL.ExecuteSql(sql);
        }
        [WebMethod]
        public string QueryCasePre(string 案件ID, string 委托编号, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

            return DBHelperSQL.SelectRowCount("预试验", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string UpdateCasePre(string ID, string 试验方法, string 日期, string 试验结果)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("试验方法", 试验方法);
            dict.Add("日期", 日期);
            dict.Add("试验结果", 试验结果);
            return DBHelperSQL.Update("预试验", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string DeleteCasePre(string ID)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("预试验", "ID，" + ID);
            return DBHelperSQL.Delete(dict);
        }
        [WebMethod]
        public string QueryCaseConfirm(string 案件ID, string 委托编号, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

            return DBHelperSQL.SelectRowCount("确证试验", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string UpdateCaseConfirm(string ID, string 试验方法, string 日期, string 试验结果)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("试验方法", 试验方法);
            dict.Add("日期", 日期);
            dict.Add("试验结果", 试验结果);
            return DBHelperSQL.Update("确证试验", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string DeleteCaseConfirm(string ID)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("确证试验", "ID，" + ID);
            return DBHelperSQL.Delete(dict);
        }
        [WebMethod]
        public string QueryCaseExtract(string 案件ID, string 委托编号, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

            return DBHelperSQL.SelectRowCount("提取视图", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string QueryCaseSample(string 案件ID, string 委托编号, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

            return DBHelperSQL.SelectRowCount("扩增电泳视图", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string PrintSampleTest(string 扩增电泳ID, string 委托编号, string tmp, string fileMoudleName)
        {
            string templatePath = ConfigurationManager.AppSettings["WebPath"] + @"STR\" + fileMoudleName;
            string wordPath = string.Empty;
            string filename = Helper.GenerateID() + ".doc";
            if (tmp.Equals("0"))
            {
                string caa = string.Empty;
                string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"1\鉴定档案\" + 委托编号 + @"\文书记录\";
                Helper.CheckDir(wordDir);
                wordPath = wordDir + fileMoudleName;
                filename = fileMoudleName;
                int ikk = 1;
                while (System.IO.File.Exists(wordPath))
                {
                    caa = ("-" + (ikk++).ToString());
                    filename = fileMoudleName.Insert(fileMoudleName.Length - 4, caa);
                    wordPath = wordDir + filename;
                }
            }
            else
            {
                string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
                Helper.CheckDir(wordDir);
                wordPath = wordDir + filename;
            }

            if (System.IO.File.Exists(templatePath))
            {
                System.IO.File.Copy(templatePath, wordPath);
            }
            else
            {
                return "找不到文书模版";
            }

            FillWord fw = new FillWord(wordPath);
            fw.OpenDoc();
            if (fw.m_openedDoc == null)
            {
                return "生成文书失败";
            }
            try
            {
                //填充str表格
                if (fileMoudleName.Equals("IFSTQDOR-07-02-A DNA扩增作业单.doc"))
                {
                    DataSet dataSet = new DataSet("NewDataSet");
                    DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                    DataColumn dc = new DataColumn("扩增人姓名", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增时间开始", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增时间结束", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增方法", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增体系", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("循环数", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增仪器", typeof(string)); dataTable.Columns.Add(dc);
                    DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                    string filter = string.Empty;
                    if (扩增电泳ID.Length > 0) filter += "扩增电泳ID='" + 扩增电泳ID + "' and ";
                    if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

                    DataSet ds = DBHelperSQL.Select("扩增电泳视图", Helper.CutFilter(filter), "ID", "*");

                    dataRow["扩增人姓名"] = ds.Tables[0].Rows[0]["扩增人姓名"].ToString();
                    dataRow["扩增时间开始"] = GetSmallDateChinese(ds.Tables[0].Rows[0]["扩增时间开始"].ToString());
                    dataRow["扩增时间结束"] = GetSmallDateChinese(ds.Tables[0].Rows[0]["扩增时间结束"].ToString());
                    dataRow["扩增方法"] = ds.Tables[0].Rows[0]["扩增方法"].ToString();
                    dataRow["扩增体系"] = ds.Tables[0].Rows[0]["扩增体系"].ToString();
                    dataRow["循环数"] = ds.Tables[0].Rows[0]["循环数"].ToString();
                    dataRow["扩增仪器"] = ds.Tables[0].Rows[0]["扩增仪"].ToString() + ds.Tables[0].Rows[0]["扩增离心机"].ToString() + ds.Tables[0].Rows[0]["扩增漩涡混合器"].ToString()
                        + ds.Tables[0].Rows[0]["扩增移液器"].ToString() + ds.Tables[0].Rows[0]["扩增超净台"].ToString() + ds.Tables[0].Rows[0]["扩增工作站"].ToString();

                    fw.FillBookMarks(dataSet);

                    fw.Fill扩增作业单(ds);
                }
                else if (fileMoudleName.Equals("IFSTQDOR-07-03-A DNA检测作业单.doc"))
                {
                    DataSet dataSet = new DataSet("NewDataSet");
                    DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                    DataColumn dc = new DataColumn("扩增人姓名", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("检测时间开始", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("检测时间结束", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("SampleSheet", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("RunFold", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("检测仪器", typeof(string)); dataTable.Columns.Add(dc);
                    DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                    string filter = string.Empty;
                    if (扩增电泳ID.Length > 0) filter += "扩增电泳ID='" + 扩增电泳ID + "' and ";
                    if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

                    DataSet ds = DBHelperSQL.Select("扩增电泳视图", Helper.CutFilter(filter), "ID", "*");

                    dataRow["扩增人姓名"] = ds.Tables[0].Rows[0]["扩增人姓名"].ToString();
                    dataRow["检测时间开始"] = GetSmallDateChinese(ds.Tables[0].Rows[0]["检测时间开始"].ToString());
                    dataRow["检测时间结束"] = GetSmallDateChinese(ds.Tables[0].Rows[0]["检测时间结束"].ToString());
                    dataRow["SampleSheet"] = ds.Tables[0].Rows[0]["SampleSheet"].ToString();
                    dataRow["RunFold"] = ds.Tables[0].Rows[0]["RunFold"].ToString();
                    dataRow["检测仪器"] = ds.Tables[0].Rows[0]["电泳仪"].ToString() + ds.Tables[0].Rows[0]["电泳离心机"].ToString() + ds.Tables[0].Rows[0]["电泳加热仪"].ToString() + ds.Tables[0].Rows[0]["电泳漩涡混合器"].ToString()
                        + ds.Tables[0].Rows[0]["电泳移液器"].ToString() + ds.Tables[0].Rows[0]["电泳超净台"].ToString() + ds.Tables[0].Rows[0]["电泳工作站"].ToString();

                    fw.FillBookMarks(dataSet);

                    fw.Fill检测作业单(ds);
                }
                else if (fileMoudleName.Equals("扩增电泳记录表.doc"))
                {
                    DataSet dataSet = new DataSet("NewDataSet");
                    DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                    DataColumn dc = new DataColumn("案件编号", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("案件名称", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增仪器", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("扩增参数", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("电泳仪器", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("电泳参数", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("环境条件", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("一检人", typeof(string)); dataTable.Columns.Add(dc);
                    dc = new DataColumn("二检人", typeof(string)); dataTable.Columns.Add(dc);
                    DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                    string filter = string.Empty;

                    if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

                    DataSet ds = DBHelperSQL.Select("鉴定流程视图", Helper.CutFilter(filter), "", "*");

                    //if (批号.Length > 0) filter += "扩增电泳批号='" + 批号 + "' and ";

                    DataSet ds2 = DBHelperSQL.Select("扩增电泳视图", Helper.CutFilter(filter), "ID,样本编号", "*");

                    dataRow["案件编号"] = ds.Tables[0].Rows[0]["案件编号"].ToString();
                    dataRow["案件名称"] = ds.Tables[0].Rows[0]["案件名称"].ToString();
                    dataRow["扩增仪器"] = "【扩增仪器】" + ds2.Tables[0].Rows[0]["扩增仪"].ToString() + ds2.Tables[0].Rows[0]["扩增离心机"].ToString() + ds2.Tables[0].Rows[0]["扩增漩涡混合器"].ToString() +
                                          ds2.Tables[0].Rows[0]["扩增移液器"].ToString() + ds2.Tables[0].Rows[0]["扩增超净台"].ToString() + ds2.Tables[0].Rows[0]["扩增工作站"].ToString() + "。";
                    dataRow["扩增参数"] = "【扩增参数】" + ds2.Tables[0].Rows[0]["扩增体系"].ToString();
                  
                    dataRow["电泳仪器"] = "【电泳仪器】" + ds2.Tables[0].Rows[0]["电泳仪"].ToString() + ds2.Tables[0].Rows[0]["电泳离心机"].ToString() + ds2.Tables[0].Rows[0]["电泳加热仪"].ToString() +
                                          ds2.Tables[0].Rows[0]["电泳漩涡混合器"].ToString() + ds2.Tables[0].Rows[0]["电泳移液器"].ToString() +
                                          ds2.Tables[0].Rows[0]["电泳制冰机"].ToString() + ds2.Tables[0].Rows[0]["电泳超净台"].ToString() +
                                          ds2.Tables[0].Rows[0]["电泳工作站"].ToString();
                    dataRow["电泳参数"] = "【电泳参数】内标：" + ds2.Tables[0].Rows[0]["内标"].ToString() + " 内标量：" + ds2.Tables[0].Rows[0]["内标量"].ToString() +
                                          " 变性溶剂：" + ds2.Tables[0].Rows[0]["变性溶剂"].ToString() + " 产物量：" + ds2.Tables[0].Rows[0]["产物量"].ToString() +
                                          " 溶剂量：" + ds2.Tables[0].Rows[0]["溶剂量"].ToString() + " 胶液：" + ds2.Tables[0].Rows[0]["胶液"].ToString() +
                                          " 胶液批号：" + ds2.Tables[0].Rows[0]["胶液批号"].ToString() + " 预电泳电流：" + ds2.Tables[0].Rows[0]["预电泳电流"].ToString() +
                                          " 电泳电流：" + ds2.Tables[0].Rows[0]["电泳电流"].ToString() + " SampleSheet：" + ds2.Tables[0].Rows[0]["SampleSheet"].ToString() +
                                          " RunFold：" + ds2.Tables[0].Rows[0]["RunFold"].ToString();
                    dataRow["环境条件"] = "【环境条件】环境温度：" + ds2.Tables[0].Rows[0]["电泳环境温度"].ToString() + " 环境湿度：" + ds2.Tables[0].Rows[0]["电泳环境湿度"].ToString();

                    dataRow["一检人"] = ds.Tables[0].Rows[0]["一检姓名"].ToString();
                    dataRow["二检人"] = ds.Tables[0].Rows[0]["二检姓名"].ToString();

                    fw.FillBookMarks(dataSet);

                    //fw.Fill扩增电泳记录表(ds2);
                }

                fw.Save();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                fw.CloseWord();
            }
            if (tmp.Equals("0"))
            {
                return ConfigurationManager.AppSettings["ServerAddr"] + @"1\\鉴定档案\\" + 委托编号 + @"\\文书记录\\" + filename;
            }
            else
            {
                return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\\" + filename;
            }

        }

        [WebMethod]
        public string PrintWorkStation(string 工作站模式ID, string type)
        {
            string filename = Helper.GenerateID() + "." + type;
            if (type.Equals("doc"))
            {
                string templatePath = ConfigurationManager.AppSettings["WebPath"] + @"STR\样本位置记录表.doc";
                string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
                Helper.CheckDir(wordDir);
                string wordPath = wordDir + filename;

                if (System.IO.File.Exists(templatePath))
                {
                    System.IO.File.Copy(templatePath, wordPath);
                }
                else
                {
                    return "找不到文书模版";
                }

                FillWord fw = new FillWord(wordPath);
                fw.OpenDoc();
                if (fw.m_openedDoc == null)
                {
                    return "生成文书失败";
                }
                try
                {
                    DataSet ds = DBHelperSQL.Select("扩增电泳视图", "工作站模式ID='" + 工作站模式ID + "'", "ID", "*");

                    fw.Fill扩增作业单(ds);

                    fw.Save();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    fw.CloseWord();
                }

            }
            else if (type.Equals("txt"))
            {
                string time = DateTime.Today.ToShortDateString();
                filename = time + ".txt";
                string filenamelast = filename;
                string templatePath = ConfigurationManager.AppSettings["WebPath"] + @"STR\样本位置记录表.txt";
                string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
                Helper.CheckDir(wordDir);

                string wordPath = wordDir + filename;

                int ikk = 1;
                while (System.IO.File.Exists(wordPath))
                {
                    string caa = string.Empty;
                    caa = ("_" + (ikk++).ToString());
                    filenamelast = filename.Insert(filename.Length - 4, caa);
                    wordPath = wordDir + filenamelast;
                }

                if (System.IO.File.Exists(templatePath))
                {
                    System.IO.File.Copy(templatePath, wordPath);
                }
                else
                {
                    return "找不到文书模版";
                }
                FileStream fs = new FileStream(wordPath, FileMode.Open);
                StreamWriter writer = new StreamWriter(fs);
                try
                {
                    DataSet ds = DBHelperSQL.Select("扩增电泳视图", "工作站模式ID='" + 工作站模式ID + "'", "ID", "*");
                    DataSet ds1 = DBHelperSQL.Select("提取记录", "ID='" + 工作站模式ID + "'", "ID", "*");
                    DataSet ds2 = DBHelperSQL.Select("系统用户", "ID='" + ds1.Tables[0].Rows[0]["提取人"] + "'", "ID", "*");

                    writer.WriteLine("Container Name	Description	ContainerType	AppType	Owner	Operator	");
                    writer.WriteLine(filenamelast.Substring(0, filenamelast.Length - 4) + "		96-Well	Regular	" + ds2.Tables[0].Rows[0]["警号"] + "	" + ds2.Tables[0].Rows[0]["警号"] + "	");
                    writer.WriteLine("AppServer	AppInstance	");
                    writer.WriteLine("GeneMapper	GeneMapper_Generic_Instance	");
                    writer.WriteLine("Well	Sample Name	Comment	Priority	Sample Type	Snp Set	Analysis Method	Panel	User-Defined 3	Size Standard	User-Defined 2	User-Defined 1	Results Group 1	Instrument Protocol 1	");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            writer.Write(ds.Tables[0].Rows[i]["电泳位置"] + "	" + ds.Tables[0].Rows[i]["样本编号"] + "		100									DNA	ID	");
                        }
                        else
                        {
                            writer.WriteLine(ds.Tables[0].Rows[i]["电泳位置"] + "	" + ds.Tables[0].Rows[i]["样本编号"] + "		100									DNA	ID	");
                        }
                    }

                    writer.Close();
                    filename = filenamelast;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    fs.Close();
                }
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\\" + filename;
        }

        [WebMethod]
        public string PrintCaseRecord(string 案件ID, string 委托编号, string type)
        {
            switch (type)
            {
                case "pre": return PrintPreExam(案件ID, 委托编号);
                case "confirm": return PrintConfirmExam(案件ID, 委托编号);
                case "extract": return PrintCaseExtract(案件ID, 委托编号);
            }
            return string.Empty;
        }
        private static string GetSmallDateChinese(string date)
        {

            string year = date.Split(' ')[0];
            string min = date.Split(' ')[1];
            string[] years = null;
            years = year.Split('-');
            string[] mins = null;
            mins = min.Split(':');
            return years[0] + "年" + years[1] + "月" + years[2] + "日" + mins[0] + "时" + mins[1] + "分";

        }
        private string PrintPreExam(string 案件ID, string 委托编号)
        {
            return "www.10.pre.com";
        }
        private string PrintConfirmExam(string 案件ID, string 委托编号)
        {
            string templatePath = ConfigurationManager.AppSettings["WebPath"] + @"STR\预实验确证试验记录表.doc";
            string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"1\鉴定档案\" + 委托编号 + @"\文书记录\";
            string filename = "预实验确证试验记录表.doc";
            string Finalname = filename;
            string wordPath = wordDir + Finalname;
            string caa = string.Empty;
            int ikk = 1;
            while (System.IO.File.Exists(wordPath))
            {
                caa = ("-" + (ikk++).ToString());
                Finalname = filename.Insert(filename.Length - 4, caa);
                wordPath = wordDir + Finalname;
            }

            if (System.IO.File.Exists(templatePath))
            {
                System.IO.File.Copy(templatePath, wordPath);
            }
            else
            {
                return "找不到文书模版";
            }

            FillWord fw = new FillWord(wordPath);
            fw.OpenDoc();
            if (fw.m_openedDoc == null)
            {
                return "生成文书失败";
            }
            try
            {
                DataSet dataSet = new DataSet("NewDataSet");
                DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                DataColumn dc = new DataColumn("案件编号", typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("案件名称", typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("检验时间", typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("一检人", typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("二检人", typeof(string)); dataTable.Columns.Add(dc);
                DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                string filter = string.Empty;

                if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

                DataSet ds = DBHelperSQL.Select("鉴定流程视图", Helper.CutFilter(filter), "", "*");
                DataSet ds1 = DBHelperSQL.Select("预试验", Helper.CutFilter(filter), "样本编号,ID", "*");
                DataSet ds2 = DBHelperSQL.Select("确证试验", Helper.CutFilter(filter), "样本编号,ID", "*");

                dataRow["案件编号"] = ds.Tables[0].Rows[0]["案件编号"].ToString();
                dataRow["案件名称"] = ds.Tables[0].Rows[0]["案件名称"].ToString();
                dataRow["检验时间"] = ds2.Tables[0].Rows[0]["日期"].ToString().Substring(0, ds2.Tables[0].Rows[0]["日期"].ToString().Length-8);
                dataRow["一检人"] = ds.Tables[0].Rows[0]["一检姓名"].ToString();
                dataRow["二检人"] = ds.Tables[0].Rows[0]["二检姓名"].ToString();

                fw.FillBookMarks(dataSet);

                //fw.Fill确证试验记录表(ds1, ds2);

                fw.Save();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                fw.CloseWord();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"1\\鉴定档案\\" + 委托编号 + @"\\文书记录\\" + Finalname;
        }
        private string PrintCaseExtract(string 案件ID, string 委托编号)
        {
            string templatePath = ConfigurationManager.AppSettings["WebPath"] + @"STR\提取、纯化记录表.doc";
            string wordDir = ConfigurationManager.AppSettings["WebPath"] + @"1\鉴定档案\" + 委托编号 + @"\文书记录\";
            Helper.CheckDir(wordDir);
            string filename = "提取、纯化记录表.doc";
            string Finalname = filename;
            string wordPath = wordDir + Finalname;
            string caa = string.Empty;
            int ikk = 1;
            while (System.IO.File.Exists(wordPath))
            {
                caa = ("-" + (ikk++).ToString());
                Finalname = filename.Insert(filename.Length - 4, caa);
                wordPath = wordDir + Finalname;
            }

            if (System.IO.File.Exists(templatePath))
            {
                System.IO.File.Copy(templatePath, wordPath);
            }
            else
            {
                return "找不到文书模版";
            }

            FillWord fw = new FillWord(wordPath);
            fw.OpenDoc();
            if (fw.m_openedDoc == null)
            {
                return "生成文书失败";
            }
            try
            {
                DataSet dataSet = new DataSet("NewDataSet");
                DataTable dataTable = new DataTable("tableName"); dataSet.Tables.Add(dataTable);
                DataColumn dc = new DataColumn("案件编号", typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("案件名称", typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("提取仪器", typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("纯化仪器", typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("一检人", typeof(string)); dataTable.Columns.Add(dc);
                dc = new DataColumn("二检人", typeof(string)); dataTable.Columns.Add(dc);
                DataRow dataRow = dataTable.NewRow(); dataTable.Rows.Add(dataRow);

                string filter = string.Empty;
               
                if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

                DataSet ds = DBHelperSQL.Select("鉴定流程视图", Helper.CutFilter(filter), "", "*");

                //if (批号.Length > 0) filter += "提取批号='" + 批号 + "' and ";

                DataSet ds2 = DBHelperSQL.Select("提取视图", Helper.CutFilter(filter), "ID,样本编号", "*");

                dataRow["案件编号"] = ds.Tables[0].Rows[0]["案件编号"].ToString() ;
                dataRow["案件名称"] = ds.Tables[0].Rows[0]["案件名称"].ToString();
                dataRow["提取仪器"] = ds2.Tables[0].Rows[0]["离心机"].ToString() + ds2.Tables[0].Rows[0]["移液器"].ToString() + ds2.Tables[0].Rows[0]["加热仪"].ToString() +
                                      ds2.Tables[0].Rows[0]["恒温混匀仪"].ToString() + ds2.Tables[0].Rows[0]["漩涡混合器"].ToString() + ds2.Tables[0].Rows[0]["水浴"].ToString() +
                                      ds2.Tables[0].Rows[0]["显微镜"].ToString() + ds2.Tables[0].Rows[0]["工作站"].ToString();
                dataRow["纯化仪器"] = ds2.Tables[0].Rows[0]["纯化离心机"].ToString() + ds2.Tables[0].Rows[0]["纯化移液器"].ToString() + ds2.Tables[0].Rows[0]["纯化加热仪"].ToString() +
                                      ds2.Tables[0].Rows[0]["纯化恒温混匀仪"].ToString() + ds2.Tables[0].Rows[0]["纯化漩涡混合器"].ToString() + ds2.Tables[0].Rows[0]["纯化水浴"].ToString() +
                                      ds2.Tables[0].Rows[0]["纯化显微镜"].ToString() + ds2.Tables[0].Rows[0]["纯化工作站"].ToString();
                dataRow["一检人"] = ds.Tables[0].Rows[0]["一检姓名"].ToString();
                dataRow["二检人"] = ds.Tables[0].Rows[0]["二检姓名"].ToString();

                fw.FillBookMarks(dataSet);

                fw.Fill提取记录表(ds2);

                fw.Save();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                fw.CloseWord();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"1\\鉴定档案\\" + 委托编号 + @"\\文书记录\\" + Finalname;

        }
    }
}
