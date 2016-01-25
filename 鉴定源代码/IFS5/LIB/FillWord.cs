using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using Microsoft.Office.Interop.Word;

namespace LIB
{
    /// <summary>
    /// Summary description for FillWord
    /// </summary>
    public class FillWord
    {
        string path;
        public Document m_openedDoc;
        public FillWord(string path)
        {
            this.path = path;
        }
        public void OpenDoc()
        {
            m_openedDoc = DocumentManager.OpenDoc(path);
        }
        /// <summary>
        /// 填充word书签
        /// </summary>
        /// <param name="ds"></param>
        public void FillBookMarks(DataSet ds)
        {
            DocumentManager.FillBookMarks(m_openedDoc, ds);
        }
        /// <summary>
        /// 填充word对应表格
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="ds"></param>
        /// <param name="dict">key中存储数据表列名，value中存储以'-'分隔的3个数字:tableIndex-startRow-colIndex</param>
        public void FillTable(DataSet ds, IDictionary<string, string> dict, int fTable, int fRow, int fCol)
        {
            DocumentManager.FillTable(m_openedDoc, ds, dict, fTable, fRow, fCol);
        }
        public void AddPicMarks(string path)
        {
            DocumentManager.AddPicMarks(m_openedDoc, path);
        }
        public void Save()
        {
            DocumentManager.Save(m_openedDoc, path);
        }
        public void CloseWord()
        {
            DocumentManager.CloseDoc(m_openedDoc);
        }

        public void Fill样本位置(DataSet ds)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string row = dr["电泳位置"].ToString().Substring(0, 1);
                int cel = Convert.ToInt32(dr["电泳位置"].ToString().Substring(1, 2));
                int r = 0;

                switch (row)
                {
                    case "A": r = 2; break;
                    case "B": r = 3; break;
                    case "C": r = 4; break;
                    case "D": r = 5; break;
                    case "E": r = 6; break;
                    case "F": r = 7; break;
                    case "G": r = 8; break;
                    case "H": r = 9; break;
                }

                m_openedDoc.Tables[1].Cell(r, cel + 1).Range.Text = dr["样本编号"].ToString();
            }
        }

        public void Fill扩增作业单(DataSet ds)
        {
            DataTable dt = ds.Tables[0];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int r = 0;
                int c = 0;
                int col = 0;
                switch (dt.Rows[i]["电泳位置"].ToString().Substring(0, 1))
                {
                    case "A": r = 1; break;
                    case "B": r = 2; break;
                    case "C": r = 3; break;
                    case "D": r = 4; break;
                    case "E": r = 5; break;
                    case "F": r = 6; break;
                    case "G": r = 7; break;
                    case "H": r = 8; break;
                }
                c = Convert.ToInt32(dt.Rows[i]["电泳位置"].ToString().Substring(1, 2));

                switch (c)
                {
                    case 1:
                    case 2:
                    case 3: col = 2; break;
                    case 4:
                    case 5:
                    case 6: col = 5; c = c - 3; break;
                    case 7:
                    case 8:
                    case 9: col = 8; c = c - 6; break;
                    case 10:
                    case 11:
                    case 12: col = 11; c = c - 9; break;
                }

                m_openedDoc.Tables[1].Cell(((c - 1) * 8 + r + 1), col).Range.Text = dt.Rows[i]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(((c - 1) * 8 + r + 1), col + 1).Range.Text = dt.Rows[i]["模板用量"].ToString();
            }
        }

        public void Fill检测作业单(DataSet ds)
        {
            DataTable dt = ds.Tables[0];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int r = 0;
                int c = 0;
                int col = 0;
                switch (dt.Rows[i]["电泳位置"].ToString().Substring(0, 1))
                {
                    case "A": r = 1; break;
                    case "B": r = 2; break;
                    case "C": r = 3; break;
                    case "D": r = 4; break;
                    case "E": r = 5; break;
                    case "F": r = 6; break;
                    case "G": r = 7; break;
                    case "H": r = 8; break;
                }
                c = Convert.ToInt32(dt.Rows[i]["电泳位置"].ToString().Substring(1, 2));

                switch (c)
                {
                    case 1:
                    case 2:
                    case 3: col = 2; break;
                    case 4:
                    case 5:
                    case 6: col = 5; c = c - 3; break;
                    case 7:
                    case 8:
                    case 9: col = 8; c = c - 6; break;
                    case 10:
                    case 11:
                    case 12: col = 11; c = c - 9; break;
                }

                m_openedDoc.Tables[1].Cell(((c - 1) * 8 + r + 1), col).Range.Text = dt.Rows[i]["样本编号"].ToString();
            }
        }

        public void FillSTR检验记录表(DataSet ds1, DataSet ds2, DataSet ds3, FillWord fw)
        {
            DataTable dt1 = ds1.Tables[0];
            DataTable dt2 = ds2.Tables[0];
            DataTable dt3 = ds3.Tables[0];

            for (int i = 3; i < ds2.Tables[0].Rows.Count; i++)
            {
                object row = m_openedDoc.Tables[1].Rows[3];
                m_openedDoc.Tables[1].Rows.Add(ref row);
            }
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(i + 3, 1).Range.Text = (i + 1).ToString();
                m_openedDoc.Tables[1].Cell(i + 3, 2).Range.Text = dt2.Rows[i]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(i + 3, 3).Range.Text = dt2.Rows[i]["样本类型"].ToString();
                m_openedDoc.Tables[1].Cell(i + 3, 7).Range.Text = dt2.Rows[i]["提取方法"].ToString();
                m_openedDoc.Tables[1].Cell(i + 3, 8).Range.Text = dt2.Rows[i]["日期"].ToString() + dt2.Rows[i]["提取人姓名"].ToString();

                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    if (ds1.Tables[0].Rows[j]["样本编号"].ToString().Equals(ds2.Tables[0].Rows[i]["样本编号"].ToString()))
                    {
                        string syjg = "+";
                        if (dt1.Rows[j]["试验结果"].ToString().Equals("阴性")) syjg = "-";
                        else if (dt1.Rows[j]["试验结果"].ToString().Equals("弱阳性")) syjg = "+-";


                        if (dt1.Rows[j]["试验方法"].ToString().Equals("Hb"))
                            m_openedDoc.Tables[1].Cell(i + 3, 4).Range.Text = syjg;
                        else if (dt1.Rows[j]["试验方法"].ToString().Equals("PSA"))
                            m_openedDoc.Tables[1].Cell(i + 3, 5).Range.Text = syjg;
                        m_openedDoc.Tables[1].Cell(i + 3, 6).Range.Text = dt1.Rows[j]["日期"].ToString() + dt1.Rows[j]["检验人姓名"].ToString();
                    }
                }

                for (int k = 0; k < ds3.Tables[0].Rows.Count; k++)
                {
                    if (ds3.Tables[0].Rows[k]["样本编号"].ToString().Equals(ds2.Tables[0].Rows[i]["样本编号"].ToString()))
                    {
                        m_openedDoc.Tables[1].Cell(i + 3, 10).Range.Text = dt3.Rows[k]["扩增体系"].ToString();
                        m_openedDoc.Tables[1].Cell(i + 3, 11).Range.Text = dt3.Rows[k]["循环数"].ToString();
                        m_openedDoc.Tables[1].Cell(i + 3, 12).Range.Text = dt3.Rows[k]["扩增模板"].ToString();

                        m_openedDoc.Tables[1].Cell(i + 3, 13).Range.Text = dt3.Rows[k]["扩增时间开始"].ToString() + dt3.Rows[k]["扩增人姓名"].ToString();

                        m_openedDoc.Tables[1].Cell(i + 3, 15).Range.Text = dt3.Rows[k]["SampleSheet"].ToString();
                        m_openedDoc.Tables[1].Cell(i + 3, 16).Range.Text = dt3.Rows[k]["产物量"].ToString();
                        m_openedDoc.Tables[1].Cell(i + 3, 17).Range.Text = dt3.Rows[k]["检测时间开始"].ToString() + dt3.Rows[k]["扩增人姓名"].ToString();
                    }
                }
            }
        }
        public void Fill确证试验记录表(DataSet ds0, DataSet ds1, DataSet ds2)
        {
            DataTable dt0 = ds0.Tables[0];
            DataTable dt1 = ds1.Tables[0];
            DataTable dt2 = ds2.Tables[0];

            object missing = System.Reflection.Missing.Value;

            for (int i = 3; i < ds0.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(3, 1).Range.Rows.Add(ref missing);
                //object row = m_openedDoc.Tables[1].Rows[10];
                //m_openedDoc.Tables[1].Rows.Add(ref row);
            }
            for (int j = 0; j < ds0.Tables[0].Rows.Count; j++)
            {
                m_openedDoc.Tables[1].Cell(j + 3, 1).Range.Text = dt0.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(j + 3, 2).Range.Text = dt0.Rows[j]["名称"].ToString();
                m_openedDoc.Tables[1].Cell(j + 3, 3).Range.Text = dt0.Rows[j]["样本类型"].ToString();

                if (dt0.Rows[j]["预试验"].ToString() == "1")
                {
                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        if (dt0.Rows[j]["样本编号"].ToString().Equals(dr["样本编号"].ToString()))
                        {
                            m_openedDoc.Tables[1].Cell(j + 3, 4).Range.Text = dr["试验方法"].ToString();
                            m_openedDoc.Tables[1].Cell(j + 3, 5).Range.Text = dr["试验结果"].ToString();
                            m_openedDoc.Tables[1].Cell(j + 3, 8).Range.Text = dr["日期"].ToString().Substring(0, dr["日期"].ToString().Length - 8);
                        }
                    }
                }
                if (dt0.Rows[j]["确证试验"].ToString() == "1")
                {
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        if (dt0.Rows[j]["样本编号"].ToString().Equals(dr["样本编号"].ToString()))
                        {
                            m_openedDoc.Tables[1].Cell(j + 3, 6).Range.Text = dr["试验方法"].ToString();
                            m_openedDoc.Tables[1].Cell(j + 3, 7).Range.Text = dr["试验结果"].ToString();
                            m_openedDoc.Tables[1].Cell(j + 3, 8).Range.Text = dr["日期"].ToString().Substring(0, dr["日期"].ToString().Length - 8);
                        }
                    }
                }
            }

        }
        public void Fill检材描述(DataSet ds)
        {
            DataTable dt = ds.Tables[0];

            object missing = System.Reflection.Missing.Value;

            for (int i = 3; i < ds.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(2, 1).Range.Rows.Add(ref missing);
                //object row = m_openedDoc.Tables[1].Rows[10];
                //m_openedDoc.Tables[1].Rows.Add(ref row);
            }
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                m_openedDoc.Tables[1].Cell(j + 1, 1).Range.Text = dt.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(j + 1, 2).Range.Text = dt.Rows[j]["名称"].ToString();
            }

        }
        public void PrintDNA专业鉴定文书发文记录(DataSet ds)
        {

            DataTable dt = ds.Tables[0];

            for (int i = 20; i < ds.Tables[0].Rows.Count; i++)
            {
                object row = m_openedDoc.Tables[1].Rows[20];
                m_openedDoc.Tables[1].Rows.Add(ref row);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(i + 2, 1).Range.Text = (i + 1).ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 2).Range.Text = dt.Rows[i]["案件名称"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 3).Range.Text = dt.Rows[i]["一检姓名"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 4).Range.Text = dt.Rows[i]["案件编号"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 6).Range.Text = dt.Rows[i]["委托年份"].ToString() + "-" + dt.Rows[i]["委托序号"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 7).Range.Text = dt.Rows[i]["领取人一"].ToString() + " " + Helper.GetSmallDateChinese(dt.Rows[i]["领取完成"].ToString());
            }

        }
        public void FillDNAStr(DataTable dt)
        {
            bool right = true;

            object missing = System.Reflection.Missing.Value;

            IList<string> tables = StrTableXml.GetFillTables();
            foreach (string table in tables)
            {
                int t = 0;
                if (!Int32.TryParse(table, out t))
                {
                    right = false;
                    break;
                }
                if (m_openedDoc.Tables.Count >= t)
                {
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        m_openedDoc.Tables[t].Cell(2, 1).Range.Rows.Add(ref missing);
                        //object row = m_openedDoc.Tables[t].Rows.Last;
                        //m_openedDoc.Tables[t].Rows.Add(ref row);
                    }
                }
            }
            if (!right) return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IDictionary<string, string> dict = StrTableXml.GetFills();
                foreach (string key in dict.Keys)
                {
                    if (dt.Columns.Contains(key))
                    {
                        string[] vals = dict[key].Split('#');
                        int col = Convert.ToInt32(vals[vals.Length - 1]);
                        for (int k = 0; k < vals.Length - 1; k++)
                        {
                            int table = Convert.ToInt32(vals[k]);
                            try
                            {
                                m_openedDoc.Tables[table].Cell(i + 2, col).Range.Text = dt.Rows[i][key].ToString();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }
        public void Fill事件记录(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {
                object row = m_openedDoc.Tables[1].Rows[1];
                m_openedDoc.Tables[1].Rows.Add(ref row);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(i + 1, 1).Range.Text = "△" + dt.Rows[i]["日期"].ToString() + "，" + dt.Rows[i]["简要内容"].ToString();
            }
        }

        public void Fill检验结果报告单PP21(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            for (int i = 27; i < ds.Tables[0].Rows.Count; i++)
            {
                object row = m_openedDoc.Tables[1].Rows[27];
                m_openedDoc.Tables[1].Rows.Add(ref row);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(i + 2, 1).Range.Text = dt.Rows[i]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 2).Range.Text = dt.Rows[i]["AMEL"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 3).Range.Text = dt.Rows[i]["D3S1358"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 4).Range.Text = dt.Rows[i]["D1S1656"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 5).Range.Text = dt.Rows[i]["D6S1043"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 6).Range.Text = dt.Rows[i]["D13S317"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 7).Range.Text = dt.Rows[i]["Penta_E"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 8).Range.Text = dt.Rows[i]["D16S539"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 9).Range.Text = dt.Rows[i]["D18S51"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 10).Range.Text = dt.Rows[i]["D2S1338"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 11).Range.Text = dt.Rows[i]["CSF1PO"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 12).Range.Text = dt.Rows[i]["Penta_D"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 13).Range.Text = dt.Rows[i]["TH01"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 14).Range.Text = dt.Rows[i]["vWA"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 15).Range.Text = dt.Rows[i]["D21S11"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 16).Range.Text = dt.Rows[i]["D7S820"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 17).Range.Text = dt.Rows[i]["D5S818"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 18).Range.Text = dt.Rows[i]["TPOX"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 19).Range.Text = dt.Rows[i]["D8S1179"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 20).Range.Text = dt.Rows[i]["D12S391"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 21).Range.Text = dt.Rows[i]["D19S433"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 22).Range.Text = dt.Rows[i]["FGA"].ToString();

            }
        }

        public void Fill检验结果报告单sinofiler(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            for (int i = 26; i < ds.Tables[0].Rows.Count; i++)
            {
                object row = m_openedDoc.Tables[1].Rows[26];
                m_openedDoc.Tables[1].Rows.Add(ref row);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(i + 2, 1).Range.Text = dt.Rows[i]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 2).Range.Text = dt.Rows[i]["AMEL"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 3).Range.Text = dt.Rows[i]["D8S1179"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 4).Range.Text = dt.Rows[i]["D21S11"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 5).Range.Text = dt.Rows[i]["D7S820"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 6).Range.Text = dt.Rows[i]["CSF1PO"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 7).Range.Text = dt.Rows[i]["D3S1358"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 8).Range.Text = dt.Rows[i]["D5S818"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 9).Range.Text = dt.Rows[i]["D13S317"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 10).Range.Text = dt.Rows[i]["D16S539"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 11).Range.Text = dt.Rows[i]["D2S1338"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 12).Range.Text = dt.Rows[i]["D19S433"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 13).Range.Text = dt.Rows[i]["vWA"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 14).Range.Text = dt.Rows[i]["D12S391"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 15).Range.Text = dt.Rows[i]["D18S51"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 16).Range.Text = dt.Rows[i]["D6S1043"].ToString();
                m_openedDoc.Tables[1].Cell(i + 2, 17).Range.Text = dt.Rows[i]["FGA"].ToString();
            }
        }

        public void Fill提取记录表(DataSet ds2)
        {
            DataTable dt2 = ds2.Tables[0];

            object missing = System.Reflection.Missing.Value;

            for (int i = 3; i < ds2.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(3, 1).Range.Rows.Add(ref missing);
                //object row = m_openedDoc.Tables[1].Rows[10];
                //m_openedDoc.Tables[1].Rows.Add(ref row);
            }
            for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
            {
                m_openedDoc.Tables[1].Cell(j + 3, 1).Range.Text = dt2.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(j + 3, 2).Range.Text = dt2.Rows[j]["样本名称"].ToString();
                m_openedDoc.Tables[1].Cell(j + 3, 3).Range.Text = dt2.Rows[j]["样本类型"].ToString();
                m_openedDoc.Tables[1].Cell(j + 3, 4).Range.Text = dt2.Rows[j]["提取方法"].ToString();
                m_openedDoc.Tables[1].Cell(j + 3, 5).Range.Text = dt2.Rows[j]["日期"].ToString();

                m_openedDoc.Tables[1].Cell(j + 3, 6).Range.Text = dt2.Rows[j]["纯化方法"].ToString();
                m_openedDoc.Tables[1].Cell(j + 3, 7).Range.Text = dt2.Rows[j]["纯化时间"].ToString();

            }
        }

        public void Fill扩增电泳记录表(DataSet ds2, DataSet ds3)
        {
            DataTable dt2 = ds2.Tables[0];
            DataTable dt3 = ds3.Tables[0];

            object missing = System.Reflection.Missing.Value;

            for (int i = 1; i < ds3.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(3, 1).Range.Rows.Add(ref missing);
                //object row = m_openedDoc.Tables[1].Rows[10];
                //m_openedDoc.Tables[1].Rows.Add(ref row);
            }
            for (int j = 0; j < ds3.Tables[0].Rows.Count; j++)
            {
                m_openedDoc.Tables[1].Cell(j + 3, 1).Range.Text = (dt3.Rows[j]["样本编号"].ToString().Length == 0 ? dt3.Rows[j]["样本名称"].ToString() : dt3.Rows[j]["样本编号"].ToString());
                m_openedDoc.Tables[1].Cell(j + 3, 5).Range.Text = dt3.Rows[j]["电泳位置"].ToString();
                m_openedDoc.Tables[1].Cell(j + 3, 6).Range.Text = dt3.Rows[j]["模板用量"].ToString();
                m_openedDoc.Tables[1].Cell(j + 3, 7).Range.Text = dt3.Rows[j]["电泳时间开始"].ToString() + " 到 " + dt3.Rows[j]["电泳时间结束"].ToString();

                foreach (DataRow dr in ds2.Tables[0].Rows)
                {
                    if (dt3.Rows[j]["对应扩增记录ID"].ToString().Equals(dr["扩增记录ID"].ToString()) && dt3.Rows[j]["样本编号"].ToString().Equals(dr["样本编号"].ToString()))
                    {
                        m_openedDoc.Tables[1].Cell(j + 3, 2).Range.Text = dr["模板用量"].ToString();
                        m_openedDoc.Tables[1].Cell(j + 3, 3).Range.Text = dr["循环数"].ToString();
                        m_openedDoc.Tables[1].Cell(j + 3, 4).Range.Text = dr["扩增时间开始"].ToString() + " 到 " + dr["扩增时间结束"].ToString();
                    }
                }
            }
        }
        public void Fill扩增位置表格(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int r = 0;
                int c = 0;
                int col = 0;
                switch (dt.Rows[i]["扩增位置"].ToString().Substring(0, 1))
                {
                    case "A": r = 1; break;
                    case "B": r = 2; break;
                    case "C": r = 3; break;
                    case "D": r = 4; break;
                    case "E": r = 5; break;
                    case "F": r = 6; break;
                    case "G": r = 7; break;
                    case "H": r = 8; break;
                }
                c = Convert.ToInt32(dt.Rows[i]["扩增位置"].ToString().Substring(1, 2));
                m_openedDoc.Tables[1].Cell(r * 2, c + 1).Range.Text = dt.Rows[i]["模板用量"].ToString();
                m_openedDoc.Tables[1].Cell(r * 2 + 1, c + 1).Range.Text = (dt.Rows[i]["样本编号"].ToString().Length == 0 ? dt.Rows[i]["样本名称"].ToString() : dt.Rows[i]["样本编号"].ToString());
            }
        }
        public void Fill电泳位置表格(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int r = 0;
                int c = 0;
                int col = 0;
                switch (dt.Rows[i]["电泳位置"].ToString().Substring(0, 1))
                {
                    case "A": r = 1; break;
                    case "B": r = 2; break;
                    case "C": r = 3; break;
                    case "D": r = 4; break;
                    case "E": r = 5; break;
                    case "F": r = 6; break;
                    case "G": r = 7; break;
                    case "H": r = 8; break;
                }
                c = Convert.ToInt32(dt.Rows[i]["电泳位置"].ToString().Substring(1, 2));
                m_openedDoc.Tables[1].Cell(r * 2, c + 1).Range.Text = dt.Rows[i]["模板用量"].ToString();
                m_openedDoc.Tables[1].Cell(r * 2 + 1, c + 1).Range.Text = (dt.Rows[i]["样本编号"].ToString().Length == 0 ? dt.Rows[i]["样本名称"].ToString() : dt.Rows[i]["样本编号"].ToString());
            }
        }

        public void Fill鉴定书_ID_plus(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            object missing = System.Reflection.Missing.Value;

            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(2, 1).Range.Rows.Add(ref missing);
                m_openedDoc.Tables[2].Cell(2, 1).Range.Rows.Add(ref missing);
            }
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                m_openedDoc.Tables[1].Cell(j + 2, 1).Range.Text = dt.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 2).Range.Text = dt.Rows[j]["D8S1179"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 3).Range.Text = dt.Rows[j]["D21S11"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 4).Range.Text = dt.Rows[j]["D7S820"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 5).Range.Text = dt.Rows[j]["CSF1PO"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 6).Range.Text = dt.Rows[j]["D3S1358"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 7).Range.Text = dt.Rows[j]["TH01"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 8).Range.Text = dt.Rows[j]["D13S317"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 9).Range.Text = dt.Rows[j]["D16S539"].ToString();

                m_openedDoc.Tables[2].Cell(j + 2, 1).Range.Text = dt.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 2).Range.Text = dt.Rows[j]["D2S1338"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 3).Range.Text = dt.Rows[j]["D19S433"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 4).Range.Text = dt.Rows[j]["vWA"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 5).Range.Text = dt.Rows[j]["TPOX"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 6).Range.Text = dt.Rows[j]["D18S51"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 7).Range.Text = dt.Rows[j]["D5S818"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 8).Range.Text = dt.Rows[j]["FGA"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 9).Range.Text = dt.Rows[j]["AMEL"].ToString();
            }
        }
        public void Fill鉴定书_PP21(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            object missing = System.Reflection.Missing.Value;

            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(2, 1).Range.Rows.Add(ref missing);
                m_openedDoc.Tables[2].Cell(2, 1).Range.Rows.Add(ref missing);
            }
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                m_openedDoc.Tables[1].Cell(j + 2, 1).Range.Text = dt.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 2).Range.Text = dt.Rows[j]["D8S1179"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 3).Range.Text = dt.Rows[j]["D21S11"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 4).Range.Text = dt.Rows[j]["D7S820"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 5).Range.Text = dt.Rows[j]["CSF1PO"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 6).Range.Text = dt.Rows[j]["D3S1358"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 7).Range.Text = dt.Rows[j]["TH01"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 8).Range.Text = dt.Rows[j]["D13S317"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 9).Range.Text = dt.Rows[j]["D16S539"].ToString();

                m_openedDoc.Tables[2].Cell(j + 2, 1).Range.Text = dt.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 2).Range.Text = dt.Rows[j]["D2S1338"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 3).Range.Text = dt.Rows[j]["D19S433"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 4).Range.Text = dt.Rows[j]["vWA"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 5).Range.Text = dt.Rows[j]["TPOX"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 6).Range.Text = dt.Rows[j]["D18S51"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 7).Range.Text = dt.Rows[j]["D5S818"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 8).Range.Text = dt.Rows[j]["FGA"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 9).Range.Text = dt.Rows[j]["AMEL"].ToString();
            }
        }
        public void Fill鉴定书_SinoFiler(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            object missing = System.Reflection.Missing.Value;

            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(2, 1).Range.Rows.Add(ref missing);
                m_openedDoc.Tables[2].Cell(2, 1).Range.Rows.Add(ref missing);
            }
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                m_openedDoc.Tables[1].Cell(j + 2, 1).Range.Text = dt.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 2).Range.Text = dt.Rows[j]["D8S1179"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 3).Range.Text = dt.Rows[j]["D21S11"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 4).Range.Text = dt.Rows[j]["D7S820"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 5).Range.Text = dt.Rows[j]["CSF1PO"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 6).Range.Text = dt.Rows[j]["D3S1358"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 7).Range.Text = dt.Rows[j]["TH01"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 8).Range.Text = dt.Rows[j]["D13S317"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 9).Range.Text = dt.Rows[j]["D16S539"].ToString();

                m_openedDoc.Tables[2].Cell(j + 2, 1).Range.Text = dt.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 2).Range.Text = dt.Rows[j]["D2S1338"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 3).Range.Text = dt.Rows[j]["D19S433"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 4).Range.Text = dt.Rows[j]["vWA"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 5).Range.Text = dt.Rows[j]["TPOX"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 6).Range.Text = dt.Rows[j]["D18S51"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 7).Range.Text = dt.Rows[j]["D5S818"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 8).Range.Text = dt.Rows[j]["FGA"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 9).Range.Text = dt.Rows[j]["AMEL"].ToString();
            }
        }
        public void Fill鉴定书_YFiler(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            object missing = System.Reflection.Missing.Value;

            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {
                m_openedDoc.Tables[1].Cell(2, 1).Range.Rows.Add(ref missing);
                m_openedDoc.Tables[2].Cell(2, 1).Range.Rows.Add(ref missing);
            }
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                m_openedDoc.Tables[1].Cell(j + 2, 1).Range.Text = dt.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 2).Range.Text = dt.Rows[j]["D8S1179"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 3).Range.Text = dt.Rows[j]["D21S11"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 4).Range.Text = dt.Rows[j]["D7S820"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 5).Range.Text = dt.Rows[j]["CSF1PO"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 6).Range.Text = dt.Rows[j]["D3S1358"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 7).Range.Text = dt.Rows[j]["TH01"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 8).Range.Text = dt.Rows[j]["D13S317"].ToString();
                m_openedDoc.Tables[1].Cell(j + 2, 9).Range.Text = dt.Rows[j]["D16S539"].ToString();

                m_openedDoc.Tables[2].Cell(j + 2, 1).Range.Text = dt.Rows[j]["样本编号"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 2).Range.Text = dt.Rows[j]["D2S1338"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 3).Range.Text = dt.Rows[j]["D19S433"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 4).Range.Text = dt.Rows[j]["vWA"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 5).Range.Text = dt.Rows[j]["TPOX"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 6).Range.Text = dt.Rows[j]["D18S51"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 7).Range.Text = dt.Rows[j]["D5S818"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 8).Range.Text = dt.Rows[j]["FGA"].ToString();
                m_openedDoc.Tables[2].Cell(j + 2, 9).Range.Text = dt.Rows[j]["AMEL"].ToString();
            }
        }
    }
}