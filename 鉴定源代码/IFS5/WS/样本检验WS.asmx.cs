using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using DAL;
using LIB;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WS
{
    /// <summary>
    /// 样本检验WS 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class 样本检验WS : System.Web.Services.WebService
    {
        #region 预试验
        [WebMethod]
        public string InsertPreExam(string[][] arr)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    int arrNum = arr.Length;
                    for (int i = 0; i < arrNum; i++)
                    {
                        IDictionary<string, string> dict = new Dictionary<string, string>();
                        dict.Add("案件ID", arr[i][0]);
                        dict.Add("委托编号", arr[i][1]);
                        dict.Add("库类型", arr[i][2]);
                        dict.Add("样本编号", arr[i][3]);
                        dict.Add("样本名称", arr[i][4]);
                        dict.Add("样本类型", arr[i][5]);
                        dict.Add("试验方法", arr[i][6]);
                        dict.Add("试验人", arr[i][7]);
                        dict.Add("日期", arr[i][8]);
                        dict.Add("试验结果", arr[i][9]);
                        DBHelperSQL.Insert("预试验", dict, dbConnection, trans);

                        dict = new Dictionary<string, string>();
                        dict.Add("检验状态", "预试验完毕");
                        dict.Add("预试验", "1");
                        DBHelperSQL.Update(getTableByScType(arr[i][2]), "样本编号='" + arr[i][3] + "'", dict, dbConnection, trans);
                    }
                    trans.Commit();
                }
            }
            return "1";
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
            string sql = string.Empty;
            DataSet ds = DBHelperSQL.Query("select * from 预试验 where ID='" + ID + "';");
            sql += "delete from 预试验 where ID='" + ID + "';";
            sql += "update " + getTableByScType(ds.Tables[0].Rows[0]["库类型"].ToString()) + " set 检验状态='未检',预试验=0 where 样本编号='" + ds.Tables[0].Rows[0]["样本编号"].ToString() + "';";
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
        public string QueryPreExam(string 鉴定单位, string 样本编号, string 样本名称, string 样本类型, string 库类型,
            string 受理时间s, string 受理时间e, string 一检人, string 检验状态, string 预试验, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (样本编号.Length > 0) filter += "样本编号='" + 样本编号 + "' and ";
            if (样本名称.Length > 0) filter += "名称 like '%" + 样本名称 + "%' and ";

            if (库类型.Length > 0) filter += "库类型='" + 库类型 + "' and ";
            if (一检人.Length > 0) filter += "一检人='" + 一检人 + "' and ";
            if (受理时间s.Length > 0) filter += "受理时间>='" + 受理时间s + "' and ";
            if (受理时间e.Length > 0) filter += "受理时间<='" + 受理时间e + "' and ";
            if (样本类型.Length > 0)
            {
                string[] yblxx = 样本类型.Split('，');
                string yblxstr = string.Empty;
                foreach (string syblx in yblxx) if (syblx.Length > 0) yblxstr += "样本类型='" + syblx + "' or ";
                if (yblxstr.Length > 0) filter += "(" + yblxstr.Substring(0, yblxstr.Length - 4) + ") and ";
            }
            if (检验状态.Length > 0)
            {
                string[] yblxx = 检验状态.Split('，');
                string yblxstr = string.Empty;
                foreach (string syblx in yblxx) if (syblx.Length > 0) yblxstr += "检验状态='" + syblx + "' or ";
                if (yblxstr.Length > 0) filter += "(" + yblxstr.Substring(0, yblxstr.Length - 4) + ") and ";
            }
            if (预试验.Length > 0) filter += "预试验='" + 预试验 + "' and ";

            return DBHelperSQL.SelectRowCount("样本视图", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        #endregion
        #region 确证试验
        [WebMethod]
        public string InsertConfirm(string[][] arr)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    int arrNum = arr.Length;
                    for (int i = 0; i < arrNum; i++)
                    {
                        IDictionary<string, string> dict = new Dictionary<string, string>();
                        dict.Add("案件ID", arr[i][0]);
                        dict.Add("委托编号", arr[i][1]);
                        dict.Add("库类型", arr[i][2]);
                        dict.Add("样本编号", arr[i][3]);
                        dict.Add("样本名称", arr[i][4]);
                        dict.Add("样本类型", arr[i][5]);
                        dict.Add("试验方法", arr[i][6]);
                        dict.Add("试验人", arr[i][7]);
                        dict.Add("日期", arr[i][8]);
                        dict.Add("试验结果", arr[i][9]);
                        DBHelperSQL.Insert("确证试验", dict, dbConnection, trans);

                        dict = new Dictionary<string, string>();
                        dict.Add("检验状态", "确证试验完毕");
                        dict.Add("确证试验", "1");
                        DBHelperSQL.Update(getTableByScType(arr[i][2]), "样本编号='" + arr[i][3] + "'", dict, dbConnection, trans);
                    }
                    trans.Commit();
                }
            }
            return "1";
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
            string sql = string.Empty;
            DataSet ds = DBHelperSQL.Query("select * from 确证试验 where ID='" + ID + "';");
            sql += "delete from 确证试验 where ID='" + ID + "';";
            sql += "update " + getTableByScType(ds.Tables[0].Rows[0]["库类型"].ToString()) + " set 检验状态='未检',确证试验=0 where 样本编号='" + ds.Tables[0].Rows[0]["样本编号"].ToString() + "';";
            return DBHelperSQL.ExecuteSql(sql);
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
        public string QueryConfirm(string 鉴定单位, string 样本编号, string 样本名称, string 样本类型, string 库类型,
            string 受理时间s, string 受理时间e, string 一检人, string 检验状态, string 确证试验, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (样本编号.Length > 0) filter += "样本编号='" + 样本编号 + "' and ";
            if (样本名称.Length > 0) filter += "名称 like '%" + 样本名称 + "%' and ";

            if (库类型.Length > 0) filter += "库类型='" + 库类型 + "' and ";
            if (一检人.Length > 0) filter += "一检人='" + 一检人 + "' and ";
            if (受理时间s.Length > 0) filter += "受理时间>='" + 受理时间s + "' and ";
            if (受理时间e.Length > 0) filter += "受理时间<='" + 受理时间e + "' and ";
            if (样本类型.Length > 0)
            {
                string[] yblxx = 样本类型.Split('，');
                string yblxstr = string.Empty;
                foreach (string syblx in yblxx) if (syblx.Length > 0) yblxstr += "样本类型='" + syblx + "' or ";
                if (yblxstr.Length > 0) filter += "(" + yblxstr.Substring(0, yblxstr.Length - 4) + ") and ";
            }
            if (检验状态.Length > 0)
            {
                string[] yblxx = 检验状态.Split('，');
                string yblxstr = string.Empty;
                foreach (string syblx in yblxx) if (syblx.Length > 0) yblxstr += "检验状态='" + syblx + "' or ";
                if (yblxstr.Length > 0) filter += "(" + yblxstr.Substring(0, yblxstr.Length - 4) + ") and ";
            }
            if (确证试验.Length > 0) filter += "确证试验='" + 确证试验 + "' and ";

            return DBHelperSQL.SelectRowCount("样本视图", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        #endregion
        #region 提取&纯化
        [WebMethod]
        public string InsertExtract(string[][] arr, string[] arr1)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("ID", arr1[0]);
                    dict.Add("提取批号", arr1[1]);
                    dict.Add("离心机", arr1[2]);
                    dict.Add("移液器", arr1[3]);
                    dict.Add("加热仪", arr1[4]);
                    dict.Add("恒温混匀仪", arr1[5]);
                    dict.Add("漩涡混合器", arr1[6]);
                    dict.Add("水浴", arr1[7]);
                    dict.Add("显微镜", arr1[8]);
                    dict.Add("工作站", arr1[9]);
                    dict.Add("提取人", arr1[10]);
                    dict.Add("日期", arr1[11]);
                    dict.Add("鉴定单位", arr1[12]);
                    dict.Add("工作站模式", arr1[13]);
                    dict.Add("提取确认", arr1[14]);
                    DBHelperSQL.Insert("提取记录", dict, dbConnection, trans);

                    int arrNum = arr.Length;
                    for (int i = 0; i < arrNum; i++)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("案件ID", arr[i][1]);
                        dict.Add("委托编号", arr[i][2]);
                        dict.Add("库类型", arr[i][3]);
                        dict.Add("样本编号", arr[i][4]);
                        dict.Add("样本名称", arr[i][5]);
                        dict.Add("样本类型", arr[i][6]);
                        dict.Add("提取方法", arr[i][7]);
                        dict.Add("提取用量", arr[i][8]);
                        dict.Add("提取记录ID", arr[i][9]);
                        DBHelperSQL.Insert("样本提取", dict, dbConnection, trans);

                        dict = new Dictionary<string, string>();
                        dict.Add("检验状态", "已提取");
                        DBHelperSQL.Update(getTableByScType(arr[i][3]), "样本编号='" + arr[i][4] + "'", dict, dbConnection, trans);
                    }
                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string UpdateExtract(string[][] arr, string[] arr1)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("提取批号", arr1[1]);
                    dict.Add("离心机", arr1[2]);
                    dict.Add("移液器", arr1[3]);
                    dict.Add("加热仪", arr1[4]);
                    dict.Add("恒温混匀仪", arr1[5]);
                    dict.Add("漩涡混合器", arr1[6]);
                    dict.Add("水浴", arr1[7]);
                    dict.Add("显微镜", arr1[8]);
                    dict.Add("工作站", arr1[9]);
                    dict.Add("提取人", arr1[10]);
                    dict.Add("日期", arr1[11]);
                    dict.Add("鉴定单位", arr1[12]);
                    DBHelperSQL.Update("提取记录", "ID='" + arr1[0] + "'", dict, dbConnection, trans);

                    int arrNum = arr.Length;
                    for (int i = 0; i < arrNum; i++)
                    {
                        if ((arr[i][0].Length == 0) || (arr[i][0].Length == 32))
                        {
                            dict = new Dictionary<string, string>();
                            dict.Add("案件ID", arr[i][1]);
                            dict.Add("委托编号", arr[i][2]);
                            dict.Add("库类型", arr[i][3]);
                            dict.Add("样本编号", arr[i][4]);
                            dict.Add("样本名称", arr[i][5]);
                            dict.Add("样本类型", arr[i][6]);
                            dict.Add("提取方法", arr[i][7]);
                            dict.Add("提取用量", arr[i][8]);
                            dict.Add("提取记录ID", arr[i][9]);
                            DBHelperSQL.Update("样本提取", "提取记录ID='" + arr[i][9] + "' and 样本编号='" + arr[i][4] + "'", dict, dbConnection, trans);
                        }
                        else
                        {
                            dict = new Dictionary<string, string>();
                            dict.Add("提取方法", arr[i][1]);
                            dict.Add("提取用量", arr[i][2]);
                            DBHelperSQL.Update("样本提取", "ID='" + arr[i][0] + "'", dict, dbConnection, trans);
                        }
                    }
                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string InsertPure(string[][] arr, string[] arr1)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("ID", arr1[0]);
                    dict.Add("纯化离心机", arr1[1]);
                    dict.Add("纯化移液器", arr1[2]);
                    dict.Add("纯化加热仪", arr1[3]);
                    dict.Add("纯化恒温混匀仪", arr1[4]);
                    dict.Add("纯化漩涡混合器", arr1[5]);
                    dict.Add("纯化水浴", arr1[6]);
                    dict.Add("纯化显微镜", arr1[7]);
                    dict.Add("纯化工作站", arr1[8]);
                    dict.Add("纯化方法", arr1[9]);
                    dict.Add("纯化人", arr1[10]);
                    dict.Add("日期", arr1[11]);
                    dict.Add("鉴定单位", arr1[12]);
                    DBHelperSQL.Insert("纯化记录", dict, dbConnection, trans);

                    int arrNum = arr.Length;
                    for (int i = 0; i < arrNum; i++)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("纯化记录ID", arr1[0]);
                        DBHelperSQL.Update("样本提取", "提取记录ID='" + arr[i][6] + "' and 样本编号='" + arr[i][3] + "'", dict, dbConnection, trans);
                    }
                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string UpdatePure(string[] arr1)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("纯化离心机", arr1[1]);
            dict.Add("纯化移液器", arr1[2]);
            dict.Add("纯化加热仪", arr1[3]);
            dict.Add("纯化恒温混匀仪", arr1[4]);
            dict.Add("纯化漩涡混合器", arr1[5]);
            dict.Add("纯化水浴", arr1[6]);
            dict.Add("纯化显微镜", arr1[7]);
            dict.Add("纯化工作站", arr1[8]);
            dict.Add("纯化方法", arr1[9]);
            dict.Add("纯化人", arr1[10]);
            dict.Add("日期", arr1[11]);
            dict.Add("鉴定单位", arr1[12]);
            return DBHelperSQL.Update("纯化记录", "ID='" + arr1[0] + "'", dict);
        }
        [WebMethod]
        public string DeleteExtractRecord(string 提取记录ID)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("提取记录", "ID，" + 提取记录ID);
                    DBHelperSQL.Delete(dict, dbConnection, trans);

                    DataSet ds = DBHelperSQL.Query("select * from 样本提取 where 提取记录ID='" + 提取记录ID + "';");

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["纯化记录ID"].ToString().Length == 0) continue;

                        dict = new Dictionary<string, string>();
                        dict.Add("纯化记录", "ID，" + dr["纯化记录ID"].ToString());
                        DBHelperSQL.Delete(dict, dbConnection, trans);

                        dict = new Dictionary<string, string>();
                        dict.Add("检验状态", "未检");
                        DBHelperSQL.Update(getTableByScType(dr["库类型"].ToString()), "样本编号='" + dr["样本编号"].ToString() + "'", dict, dbConnection, trans);
                    }

                    dict = new Dictionary<string, string>();
                    dict.Add("样本提取", "提取记录ID，" + 提取记录ID);
                    DBHelperSQL.Delete(dict, dbConnection, trans);

                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string NoTest(string[][] arr)
        {
            string sql = "";
            int arrNum = arr.Length;
            for (int i = 0; i < arrNum; i++)
            {
                if (arr[i][2].ToString() == "现场物证")
                {
                    sql += "update 样本信息 set 检验状态='暂不检验' where 样本编号='" + arr[i][3] + "';";
                }
                else if (arr[i][2].ToString() == "嫌疑人" || arr[i][2].ToString() == "受害人" || arr[i][2].ToString() == "其他人员")
                {
                    sql += "update 涉案人员 set 检验状态='暂不检验' where 样本编号='" + arr[i][3] + "';";
                }
                else if (arr[i][2].ToString() == "嫌疑人亲属" || arr[i][2].ToString() == "受害人亲属" || arr[i][2].ToString() == "失踪人亲属")
                {
                    sql += "update 亲属信息 set 检验状态='暂不检验' where 样本编号='" + arr[i][3] + "';";
                }
                else if (arr[i][2].ToString() == "无名尸体")
                {
                    sql += "update 无名尸体 set 检验状态='暂不检验' where 样本编号='" + arr[i][3] + "';";
                }
                else if (arr[i][2].ToString() == "失踪人员")
                {
                    sql += "update 失踪人员 set 检验状态='暂不检验' where 样本编号='" + arr[i][3] + "';";
                }
            }
            return DBHelperSQL.ExecuteSql(sql);
        }
        [WebMethod]
        public string QueryExtract(string 鉴定单位, string 样本编号, string 样本名称, string 样本类型, string 库类型,
            string 受理时间s, string 受理时间e, string 一检人, string 检验状态, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (样本编号.Length > 0) filter += "样本编号='" + 样本编号 + "' and ";
            if (样本名称.Length > 0) filter += "名称 like '%" + 样本名称 + "%' and ";

            if (库类型.Length > 0) filter += "库类型='" + 库类型 + "' and ";
            if (一检人.Length > 0) filter += "一检人='" + 一检人 + "' and ";
            if (受理时间s.Length > 0) filter += "受理时间>='" + 受理时间s + "' and ";
            if (受理时间e.Length > 0) filter += "受理时间<='" + 受理时间e + "' and ";
            if (样本类型.Length > 0)
            {
                string[] yblxx = 样本类型.Split('，');
                string yblxstr = string.Empty;
                foreach (string syblx in yblxx) if (syblx.Length > 0) yblxstr += "样本类型='" + syblx + "' or ";
                if (yblxstr.Length > 0) filter += "(" + yblxstr.Substring(0, yblxstr.Length - 4) + ") and ";
            }
            if (检验状态.Length > 0)
            {
                string[] yblxx = 检验状态.Split('，');
                string yblxstr = string.Empty;
                foreach (string syblx in yblxx) if (syblx.Length > 0) yblxstr += "检验状态='" + syblx + "' or ";
                if (yblxstr.Length > 0) filter += "(" + yblxstr.Substring(0, yblxstr.Length - 4) + ") and ";
            }
            return DBHelperSQL.SelectRowCount("样本视图", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string QueryPure(string 提取记录ID)
        {
            return DBHelperSQL.Select("提取视图", "提取记录ID='" + 提取记录ID + "'", "样本编号", "*").GetXml();
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
        #endregion
        #region 定量
        #endregion
        #region 扩增
        [WebMethod]
        public string QueryExtractRecord(string 鉴定单位, string 提取人, string 日期s, string 日期e, string 工作站模式, string pageSize, string pageIndex)
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
        public string GetSampleAmplify(string 提取记录ID)
        {
            return DBHelperSQL.Select("提取视图", "提取记录ID='" + 提取记录ID + "'", "样本编号", "*").GetXml();
        }
        [WebMethod]
        public string JoinAmplify(string 鉴定单位, string 样本编号)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (样本编号.Length > 0) filter += "样本编号 ='" + 样本编号 + "' and ";

            return DBHelperSQL.Select("样本视图", Helper.CutFilter(filter), "样本编号", "*").GetXml();
        }
        [WebMethod]
        public string InsertAmplify(string[][] arr, string[] arr1)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("ID", arr1[0]);
                    dict.Add("扩增批号", arr1[1]);
                    dict.Add("扩增仪", arr1[2]);
                    dict.Add("扩增离心机", arr1[3]);
                    dict.Add("扩增漩涡混合器", arr1[4]);
                    dict.Add("扩增移液器", arr1[5]);
                    dict.Add("扩增超净台", arr1[6]);
                    dict.Add("扩增工作站", arr1[7]);
                    dict.Add("扩增时间开始", arr1[8]);
                    dict.Add("扩增时间结束", arr1[9]);
                    dict.Add("扩增方法", arr1[10]);
                    dict.Add("试剂盒批号", arr1[11]);
                    dict.Add("扩增体系", arr1[12]);
                    dict.Add("扩增模板", arr1[13]);
                    dict.Add("循环数", arr1[14]);
                    dict.Add("环境温度", arr1[15]);
                    dict.Add("环境湿度", arr1[16]);
                    dict.Add("质控样本", arr1[17]);
                    dict.Add("质控样本位置", arr1[18]);
                    dict.Add("扩增人", arr1[19]);
                    dict.Add("鉴定单位", arr1[20]);
                    dict.Add("扩增确认", arr1[21]);
                    dict.Add("对应提取记录ID", arr1[22]);
                    DBHelperSQL.Insert("扩增记录", dict, dbConnection, trans);

                    int arrNum = arr.Length;
                    for (int i = 0; i < arrNum; i++)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("案件ID", arr[i][0]);
                        dict.Add("委托编号", arr[i][1]);
                        dict.Add("库类型", arr[i][2]);
                        dict.Add("样本编号", arr[i][3]);
                        dict.Add("样本名称", arr[i][4]);
                        dict.Add("样本类型", arr[i][5]);
                        dict.Add("扩增位置", arr[i][6]);
                        dict.Add("模板用量", arr[i][7]);
                        dict.Add("扩增记录ID", arr[i][8]);
                        dict.Add("位置序号", Helper.getWZXL(arr[i][6]));
                        DBHelperSQL.Insert("样本扩增", dict, dbConnection, trans);

                        if (arr[i][2].Length == 0 && arr[i][3].Length == 0) continue;
                        else
                        {
                            dict = new Dictionary<string, string>();
                            dict.Add("检验状态", "已扩增");
                            DBHelperSQL.Update(getTableByScType(arr[i][2]), "样本编号='" + arr[i][3] + "'", dict, dbConnection, trans);
                        }
                    }
                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string UpdateAmplify(string[][] arr, string[] arr1)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("扩增批号", arr1[1]);
                    dict.Add("扩增仪", arr1[2]);
                    dict.Add("扩增离心机", arr1[3]);
                    dict.Add("扩增漩涡混合器", arr1[4]);
                    dict.Add("扩增移液器", arr1[5]);
                    dict.Add("扩增超净台", arr1[6]);
                    dict.Add("扩增工作站", arr1[7]);
                    dict.Add("扩增时间开始", arr1[8]);
                    dict.Add("扩增时间结束", arr1[9]);
                    dict.Add("扩增方法", arr1[10]);
                    dict.Add("试剂盒批号", arr1[11]);
                    dict.Add("扩增体系", arr1[12]);
                    dict.Add("扩增模板", arr1[13]);
                    dict.Add("循环数", arr1[14]);
                    dict.Add("环境温度", arr1[15]);
                    dict.Add("环境湿度", arr1[16]);
                    dict.Add("质控样本", arr1[17]);
                    dict.Add("质控样本位置", arr1[18]);
                    dict.Add("扩增人", arr1[19]);
                    dict.Add("鉴定单位", arr1[20]);
                    dict.Add("扩增确认", arr1[21]);
                    dict.Add("对应提取记录ID", arr1[22]);
                    DBHelperSQL.Update("扩增记录", "ID='" + arr1[0] + "'", dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("样本扩增", "扩增记录ID，" + arr1[0]);
                    DBHelperSQL.Delete(dict, dbConnection, trans);

                    int arrNum = arr.Length;
                    for (int i = 0; i < arrNum; i++)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("案件ID", arr[i][0]);
                        dict.Add("委托编号", arr[i][1]);
                        dict.Add("库类型", arr[i][2]);
                        dict.Add("样本编号", arr[i][3]);
                        dict.Add("样本名称", arr[i][4]);
                        dict.Add("样本类型", arr[i][5]);
                        dict.Add("扩增位置", arr[i][6]);
                        dict.Add("模板用量", arr[i][7]);
                        dict.Add("扩增记录ID", arr[i][8]);
                        dict.Add("位置序号", Helper.getWZXL(arr[i][6]));
                        DBHelperSQL.Insert("样本扩增", dict, dbConnection, trans);
                    }
                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string DeleteAmplifyRecord(string 扩增记录ID)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("样本扩增", "扩增记录ID，" + 扩增记录ID);
                    DBHelperSQL.Delete(dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("扩增记录 ", "ID，" + 扩增记录ID);
                    DBHelperSQL.Delete(dict, dbConnection, trans);

                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string QueryCaseAmplify(string 案件ID, string 委托编号, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

            return DBHelperSQL.SelectRowCount("扩增视图", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        #endregion
        #region 电泳
        [WebMethod]
        public string QueryAmplifyRecord(string 鉴定单位, string 扩增人, string 日期s, string 日期e, string 扩增确认, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (扩增确认.Length > 0) filter += "扩增确认='" + 扩增确认 + "' and ";
            if (扩增人.Length > 0) filter += "扩增人='" + 扩增人 + "' and ";
            if (日期s.Length > 0) filter += "扩增时间开始>='" + 日期s + "' and ";
            if (日期e.Length > 0) filter += "扩增时间开始<='" + 日期e + "' and ";

            return DBHelperSQL.SelectRowCount("扩增记录", Helper.CutFilter(filter), "扩增时间开始 desc", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string GetSampleEP(string 扩增记录ID)
        {
            return DBHelperSQL.Select("扩增视图", "扩增记录ID='" + 扩增记录ID + "'", "位置序号", "*").GetXml();
        }
        [WebMethod]
        public string JoinEP(string 鉴定单位, string 样本编号)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (样本编号.Length > 0) filter += "样本编号 ='" + 样本编号 + "' and ";

            return DBHelperSQL.Select("样本视图", Helper.CutFilter(filter), "样本编号", "*").GetXml();
        }
        [WebMethod]
        public string InsertEP(string[][] arr, string[] arr1)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("ID", arr1[0]);
                    dict.Add("电泳批号", arr1[1]);
                    dict.Add("电泳仪", arr1[2]);
                    dict.Add("电泳离心机", arr1[3]);
                    dict.Add("电泳加热仪", arr1[4]);
                    dict.Add("电泳漩涡混合器", arr1[5]);
                    dict.Add("电泳移液器", arr1[6]);
                    dict.Add("电泳制冰机", arr1[7]);
                    dict.Add("电泳超净台", arr1[8]);
                    dict.Add("电泳工作站", arr1[9]);
                    dict.Add("内标", arr1[10]);
                    dict.Add("内标量", arr1[11]);
                    dict.Add("电泳时间开始", arr1[12]);
                    dict.Add("电泳时间结束", arr1[13]);
                    dict.Add("变性溶剂", arr1[14]);
                    dict.Add("溶剂量", arr1[15]);
                    dict.Add("产物量", arr1[16]);
                    dict.Add("胶液", arr1[17]);
                    dict.Add("胶液批号", arr1[18]);
                    dict.Add("预电泳电流", arr1[19]);
                    dict.Add("电泳电流", arr1[20]);
                    dict.Add("SampleSheet", arr1[21]);
                    dict.Add("RunFold", arr1[22]);
                    dict.Add("电泳环境温度", arr1[23]);
                    dict.Add("电泳环境湿度", arr1[24]);
                    dict.Add("电泳人", arr1[25]);
                    dict.Add("鉴定单位", arr1[26]);
                    dict.Add("电泳确认", arr1[27]);
                    dict.Add("对应扩增记录ID", arr1[28]);
                    DBHelperSQL.Insert("电泳记录", dict, dbConnection, trans);

                    int arrNum = arr.Length;
                    for (int i = 0; i < arrNum; i++)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("案件ID", arr[i][0]);
                        dict.Add("委托编号", arr[i][1]);
                        dict.Add("库类型", arr[i][2]);
                        dict.Add("样本编号", arr[i][3]);
                        dict.Add("样本名称", arr[i][4]);
                        dict.Add("样本类型", arr[i][5]);
                        dict.Add("电泳位置", arr[i][6]);
                        dict.Add("模板用量", arr[i][7]);
                        dict.Add("电泳记录ID", arr[i][8]);
                        dict.Add("位置序号", Helper.getWZXL(arr[i][6]));
                        DBHelperSQL.Insert("样本电泳", dict, dbConnection, trans);

                        if (arr[i][2].Length == 0 && arr[i][3].Length == 0) continue;
                        else
                        {
                            dict = new Dictionary<string, string>();
                            dict.Add("检验状态", "检毕");
                            DBHelperSQL.Update(getTableByScType(arr[i][2]), "样本编号='" + arr[i][3] + "'", dict, dbConnection, trans);
                        }
                    }
                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string UpdateEP(string[][] arr, string[] arr1)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("电泳批号", arr1[1]);
                    dict.Add("电泳仪", arr1[2]);
                    dict.Add("电泳离心机", arr1[3]);
                    dict.Add("电泳加热仪", arr1[4]);
                    dict.Add("电泳漩涡混合器", arr1[5]);
                    dict.Add("电泳移液器", arr1[6]);
                    dict.Add("电泳制冰机", arr1[7]);
                    dict.Add("电泳超净台", arr1[8]);
                    dict.Add("电泳工作站", arr1[9]);
                    dict.Add("内标", arr1[10]);
                    dict.Add("内标量", arr1[11]);
                    dict.Add("电泳时间开始", arr1[12]);
                    dict.Add("电泳时间结束", arr1[13]);
                    dict.Add("变性溶剂", arr1[14]);
                    dict.Add("溶剂量", arr1[15]);
                    dict.Add("产物量", arr1[16]);
                    dict.Add("胶液", arr1[17]);
                    dict.Add("胶液批号", arr1[18]);
                    dict.Add("预电泳电流", arr1[19]);
                    dict.Add("电泳电流", arr1[20]);
                    dict.Add("SampleSheet", arr1[21]);
                    dict.Add("RunFold", arr1[22]);
                    dict.Add("电泳环境温度", arr1[23]);
                    dict.Add("电泳环境湿度", arr1[24]);
                    dict.Add("电泳人", arr1[25]);
                    dict.Add("鉴定单位", arr1[26]);
                    dict.Add("电泳确认", arr1[27]);
                    dict.Add("对应扩增记录ID", arr1[28]);
                    DBHelperSQL.Update("电泳记录", "ID='" + arr1[0] + "'", dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("样本电泳", "电泳记录ID，" + arr1[0]);
                    DBHelperSQL.Delete(dict, dbConnection, trans);

                    int arrNum = arr.Length;
                    for (int i = 0; i < arrNum; i++)
                    {
                        dict = new Dictionary<string, string>();
                        dict.Add("案件ID", arr[i][0]);
                        dict.Add("委托编号", arr[i][1]);
                        dict.Add("库类型", arr[i][2]);
                        dict.Add("样本编号", arr[i][3]);
                        dict.Add("样本名称", arr[i][4]);
                        dict.Add("样本类型", arr[i][5]);
                        dict.Add("电泳位置", arr[i][6]);
                        dict.Add("模板用量", arr[i][7]);
                        dict.Add("电泳记录ID", arr[i][8]);
                        dict.Add("位置序号", Helper.getWZXL(arr[i][6]));
                        DBHelperSQL.Insert("样本电泳", dict, dbConnection, trans);
                    }
                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string DeleteEPRecord(string 电泳记录ID)
        {
            using (SqlConnection dbConnection = new SqlConnection(DBHelperSQL.strConnection))
            {
                dbConnection.Open();
                using (SqlTransaction trans = dbConnection.BeginTransaction())
                {
                    IDictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("样本电泳", "电泳记录ID，" + 电泳记录ID);
                    DBHelperSQL.Delete(dict, dbConnection, trans);

                    dict = new Dictionary<string, string>();
                    dict.Add("电泳记录 ", "ID，" + 电泳记录ID);
                    DBHelperSQL.Delete(dict, dbConnection, trans);

                    trans.Commit();
                }
            }
            return "1";
        }
        [WebMethod]
        public string QueryEPRecord(string 鉴定单位, string 电泳人, string 日期s, string 日期e, string 电泳确认, string pageSize, string pageIndex)
        {
            string filter = "鉴定单位='" + 鉴定单位 + "' and ";
            if (电泳确认.Length > 0) filter += "电泳确认='" + 电泳确认 + "' and ";
            if (电泳人.Length > 0) filter += "电泳人='" + 电泳人 + "' and ";
            if (日期s.Length > 0) filter += "电泳时间开始>='" + 日期s + "' and ";
            if (日期e.Length > 0) filter += "电泳时间开始<='" + 日期e + "' and ";

            return DBHelperSQL.SelectRowCount("电泳记录", Helper.CutFilter(filter), "电泳时间开始 desc", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string GetSampleEPRecord(string 电泳记录ID)
        {
            return DBHelperSQL.Select("电泳视图", "电泳记录ID='" + 电泳记录ID + "'", "位置序号", "*").GetXml();
        }
        [WebMethod]
        public string QueryCaseEP(string 案件ID, string 委托编号, string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (案件ID.Length > 0) filter += "案件ID='" + 案件ID + "' and ";
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";

            return DBHelperSQL.SelectRowCount("电泳视图", Helper.CutFilter(filter), "样本编号", "*",
              Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        #endregion
        #region 辅助
        public string getTableByScType(string type)
        {
            switch (type)
            {
                case "现场物证": return "样本信息";
                case "受害人":
                case "嫌疑人":
                case "其他人员": return "涉案人员";
                case "受害人亲属":
                case "嫌疑人亲属":
                case "失踪人亲属": return "亲属信息";
                case "失踪人员": return "失踪人员";
                case "无名尸体": return "无名尸体";
            }
            return "";
        }
        #endregion

        #region 临时方法
        [WebMethod]
        public string updateWZXL()
        {
            DataSet kzds = DBHelperSQL.Query("select ID,扩增位置 from 样本扩增 where 扩增位置 is not null;");
            string sql = string.Empty;
            foreach (DataRow dr in kzds.Tables[0].Rows)
            {
                sql += "update 样本扩增 set 位置序号='" + Helper.getWZXL(dr["扩增位置"].ToString()) + "' where id='" + dr["ID"].ToString() + "';";
            }
            DataSet dyds = DBHelperSQL.Query("select ID,电泳位置 from 样本电泳 where 电泳位置 is not null;");
            foreach (DataRow dr in dyds.Tables[0].Rows)
            {
                sql += "update 样本电泳 set 位置序号='" + Helper.getWZXL(dr["电泳位置"].ToString()) + "' where id='" + dr["ID"].ToString() + "';";
            }
            return DBHelperSQL.ExecuteSql(sql);
        }
        #endregion
    }
}
