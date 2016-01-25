using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;

namespace LIB
{
    public class ExcelOper
    {
        private static Microsoft.Office.Interop.Excel.Application m_objExcel = null;
        private static Microsoft.Office.Interop.Excel.Workbooks m_objBooks = null;
        private static Microsoft.Office.Interop.Excel._Workbook m_objBook = null;
        private static Microsoft.Office.Interop.Excel.Sheets m_objSheets = null;
        private static Microsoft.Office.Interop.Excel._Worksheet m_objSheet = null;
        private static Microsoft.Office.Interop.Excel.Range m_objRange = null;
        private static object m_objOpt = System.Reflection.Missing.Value;
        public static void SaveFile(string OutputFilePath)
        {
            m_objBook.SaveAs(OutputFilePath, m_objOpt, m_objOpt,
              m_objOpt, m_objOpt, m_objOpt, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
              m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);
            Close();
        }
        private static void Close()
        {
            m_objBook.Close(false, m_objOpt, m_objOpt);
            m_objExcel.Quit();
        }
        public static void Dispose()
        {
            ReleaseObj(m_objSheets);
            ReleaseObj(m_objBook);
            ReleaseObj(m_objBooks);
            ReleaseObj(m_objExcel);
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }
        private static void ReleaseObj(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch { }
            finally { o = null; }
        }
        public static string PrintStrID(System.Data.DataTable dt, System.Data.DataTable dt2, string filename)
        {
            string tmp = string.Empty;
            try
            {
                string excel = ConfigurationManager.AppSettings["WebPath"] + @"STR\ID.xls";
                tmp = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
                Helper.CheckDir(tmp);
                tmp += filename + ".xls";
                File.Copy(excel, tmp);
                excel = tmp;

                m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                m_objExcel.Visible = false;
                m_objExcel.DisplayAlerts = false;
                m_objBooks = (Microsoft.Office.Interop.Excel.Workbooks)m_objExcel.Workbooks;

                m_objBook = m_objBooks.Open(excel, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);

                m_objSheets = (Microsoft.Office.Interop.Excel.Sheets)m_objBook.Worksheets;
                m_objSheet = (Microsoft.Office.Interop.Excel._Worksheet)(m_objSheets.get_Item(1));

                //m_objRange = m_objSheet.get_Range("A2", "A2");
                //m_objRange.Value2 = month + "月";
                string exStr = "B";
                m_objRange = m_objSheet.get_Range(exStr + "4", exStr + "4"); m_objRange.Value2 = dt2.Rows[0]["案件编号"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    m_objRange = m_objSheet.get_Range(exStr + "5", exStr + "5"); m_objRange.Value2 = dt.Rows[i]["样本编号"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "6", exStr + "6"); m_objRange.Value2 = dt.Rows[i]["名称"].ToString() + "(" + dt.Rows[i]["样本类型"].ToString() + ")";
                    m_objRange = m_objSheet.get_Range(exStr + "8", exStr + "8"); m_objRange.Value2 = dt.Rows[i]["D8S1179"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "9", exStr + "9"); m_objRange.Value2 = dt.Rows[i]["D21S11"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "10", exStr + "10"); m_objRange.Value2 = dt.Rows[i]["D7S820"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "11", exStr + "11"); m_objRange.Value2 = dt.Rows[i]["CSF1PO"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "12", exStr + "12"); m_objRange.Value2 = dt.Rows[i]["D3S1358"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "13", exStr + "13"); m_objRange.Value2 = dt.Rows[i]["TH01"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "14", exStr + "14"); m_objRange.Value2 = dt.Rows[i]["D13S317"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "15", exStr + "15"); m_objRange.Value2 = dt.Rows[i]["D16S539"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "16", exStr + "16"); m_objRange.Value2 = dt.Rows[i]["D2S1338"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "17", exStr + "17"); m_objRange.Value2 = dt.Rows[i]["D19S433"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "18", exStr + "18"); m_objRange.Value2 = dt.Rows[i]["vWA"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "19", exStr + "19"); m_objRange.Value2 = dt.Rows[i]["TPOX"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "20", exStr + "20"); m_objRange.Value2 = dt.Rows[i]["D18S51"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "21", exStr + "21"); m_objRange.Value2 = dt.Rows[i]["AMEL"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "22", exStr + "22"); m_objRange.Value2 = dt.Rows[i]["D5S818"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "23", exStr + "23"); m_objRange.Value2 = dt.Rows[i]["FGA"].ToString();
                    //

                    //
                    if (exStr.Length == 1)
                    {
                        if (exStr.Equals("Z")) exStr = "AA";
                        else exStr = ((char)((char)exStr[0] + 1)).ToString();
                    }
                    else
                    {
                        if (exStr[1] == 'Z') exStr = ((char)((char)exStr[0] + 1)).ToString() + "A";
                        else exStr = exStr[0].ToString() + ((char)((char)exStr[1] + 1)).ToString();
                    }
                }

                SaveFile(excel);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                Dispose();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\" + filename + ".xls";
        }
        public static string PrintStrPP16(System.Data.DataTable dt, System.Data.DataTable dt2, string filename)
        {
            string tmp = string.Empty;
            try
            {
                string excel = ConfigurationManager.AppSettings["WebPath"] + @"STR\PP16.xls";
                tmp = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
                Helper.CheckDir(tmp);
                tmp += filename + ".xls";
                File.Copy(excel, tmp);
                excel = tmp;

                m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                m_objExcel.Visible = false;
                m_objExcel.DisplayAlerts = false;
                m_objBooks = (Microsoft.Office.Interop.Excel.Workbooks)m_objExcel.Workbooks;

                m_objBook = m_objBooks.Open(excel, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);

                m_objSheets = (Microsoft.Office.Interop.Excel.Sheets)m_objBook.Worksheets;
                m_objSheet = (Microsoft.Office.Interop.Excel._Worksheet)(m_objSheets.get_Item(1));

                //m_objRange = m_objSheet.get_Range("A2", "A2");
                //m_objRange.Value2 = month + "月";
                string exStr = "B";
                m_objRange = m_objSheet.get_Range(exStr + "4", exStr + "4"); m_objRange.Value2 = dt2.Rows[0]["案件编号"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    m_objRange = m_objSheet.get_Range(exStr + "5", exStr + "5"); m_objRange.Value2 = dt.Rows[i]["样本编号"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "6", exStr + "6"); m_objRange.Value2 = dt.Rows[i]["名称"].ToString() + "(" + dt.Rows[i]["样本类型"].ToString() + ")";
                    m_objRange = m_objSheet.get_Range(exStr + "7", exStr + "7"); m_objRange.Value2 = dt.Rows[i]["D3S1358"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "8", exStr + "8"); m_objRange.Value2 = dt.Rows[i]["TH01"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "9", exStr + "9"); m_objRange.Value2 = dt.Rows[i]["D21S11"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "10", exStr + "10"); m_objRange.Value2 = dt.Rows[i]["D18S51"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "11", exStr + "11"); m_objRange.Value2 = dt.Rows[i]["Penta_E"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "12", exStr + "12"); m_objRange.Value2 = dt.Rows[i]["D5S818"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "13", exStr + "13"); m_objRange.Value2 = dt.Rows[i]["D13S317"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "14", exStr + "14"); m_objRange.Value2 = dt.Rows[i]["D7S820"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "15", exStr + "15"); m_objRange.Value2 = dt.Rows[i]["D16S539"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "16", exStr + "16"); m_objRange.Value2 = dt.Rows[i]["CSF1PO"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "17", exStr + "17"); m_objRange.Value2 = dt.Rows[i]["Penta_D"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "18", exStr + "18"); m_objRange.Value2 = dt.Rows[i]["AMEL"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "19", exStr + "19"); m_objRange.Value2 = dt.Rows[i]["vWA"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "20", exStr + "20"); m_objRange.Value2 = dt.Rows[i]["D8S1179"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "21", exStr + "21"); m_objRange.Value2 = dt.Rows[i]["TPOX"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "22", exStr + "22"); m_objRange.Value2 = dt.Rows[i]["FGA"].ToString();

                    //

                    //
                    if (exStr.Length == 1)
                    {
                        if (exStr.Equals("Z")) exStr = "AA";
                        else exStr = ((char)((char)exStr[0] + 1)).ToString();
                    }
                    else
                    {
                        if (exStr[1] == 'Z') exStr = ((char)((char)exStr[0] + 1)).ToString() + "A";
                        else exStr = exStr[0].ToString() + ((char)((char)exStr[1] + 1)).ToString();
                    }
                }

                SaveFile(excel);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                Dispose();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\" + filename + ".xls";
        }
        public static string PrintStrY(System.Data.DataTable dt, System.Data.DataTable dt2, string filename)
        {
            string tmp = string.Empty;
            try
            {
                string excel = ConfigurationManager.AppSettings["WebPath"] + @"STR\Y.xls";
                tmp = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
                Helper.CheckDir(tmp);
                tmp += filename + ".xls";
                File.Copy(excel, tmp);
                excel = tmp;

                m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                m_objExcel.Visible = false;
                m_objExcel.DisplayAlerts = false;
                m_objBooks = (Microsoft.Office.Interop.Excel.Workbooks)m_objExcel.Workbooks;

                m_objBook = m_objBooks.Open(excel, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);

                m_objSheets = (Microsoft.Office.Interop.Excel.Sheets)m_objBook.Worksheets;
                m_objSheet = (Microsoft.Office.Interop.Excel._Worksheet)(m_objSheets.get_Item(1));

                string exStr = "B";
                m_objRange = m_objSheet.get_Range(exStr + "5", exStr + "5"); m_objRange.Value2 = dt2.Rows[0]["案件编号"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    m_objRange = m_objSheet.get_Range(exStr + "6", exStr + "6"); m_objRange.Value2 = dt.Rows[i]["样本编号"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "7", exStr + "7"); m_objRange.Value2 = dt.Rows[i]["名称"].ToString() + "(" + dt.Rows[i]["样本类型"].ToString() + ")";
                    m_objRange = m_objSheet.get_Range(exStr + "8", exStr + "8"); m_objRange.Value2 = dt.Rows[i]["B_DYS456"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "9", exStr + "9"); m_objRange.Value2 = dt.Rows[i]["B_DYS389I"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "10", exStr + "10"); m_objRange.Value2 = dt.Rows[i]["B_DYS390"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "11", exStr + "11"); m_objRange.Value2 = dt.Rows[i]["B_DYS389II"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "12", exStr + "12"); m_objRange.Value2 = dt.Rows[i]["G_DYS458"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "13", exStr + "13"); m_objRange.Value2 = dt.Rows[i]["G_DYS19"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "14", exStr + "14"); m_objRange.Value2 = dt.Rows[i]["G_DYS385"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "15", exStr + "15"); m_objRange.Value2 = dt.Rows[i]["Y_DYS393"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "16", exStr + "16"); m_objRange.Value2 = dt.Rows[i]["Y_DYS391"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "17", exStr + "17"); m_objRange.Value2 = dt.Rows[i]["Y_DYS439"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "18", exStr + "18"); m_objRange.Value2 = dt.Rows[i]["Y_DYS635"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "19", exStr + "19"); m_objRange.Value2 = dt.Rows[i]["Y_DYS392"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "20", exStr + "20"); m_objRange.Value2 = dt.Rows[i]["R_Y_GATA_H4"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "21", exStr + "21"); m_objRange.Value2 = dt.Rows[i]["R_DYS437"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "22", exStr + "22"); m_objRange.Value2 = dt.Rows[i]["R_DYS438"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "23", exStr + "23"); m_objRange.Value2 = dt.Rows[i]["R_DYS448"].ToString();
                    //

                    //
                    if (exStr.Length == 1)
                    {
                        if (exStr.Equals("Z")) exStr = "AA";
                        else exStr = ((char)((char)exStr[0] + 1)).ToString();
                    }
                    else
                    {
                        if (exStr[1] == 'Z') exStr = ((char)((char)exStr[0] + 1)).ToString() + "A";
                        else exStr = exStr[0].ToString() + ((char)((char)exStr[1] + 1)).ToString();
                    }
                }

                SaveFile(excel);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                Dispose();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\" + filename + ".xls";
        }
        public static string PrintStrTYPER(System.Data.DataTable dt, System.Data.DataTable dt2, string filename)
        {
            string tmp = string.Empty;
            try
            {
                string excel = ConfigurationManager.AppSettings["WebPath"] + @"STR\Typer.xls";
                tmp = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
                Helper.CheckDir(tmp);
                tmp += filename + ".xls";
                File.Copy(excel, tmp);
                excel = tmp;

                m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                m_objExcel.Visible = false;
                m_objExcel.DisplayAlerts = false;
                m_objBooks = (Microsoft.Office.Interop.Excel.Workbooks)m_objExcel.Workbooks;

                m_objBook = m_objBooks.Open(excel, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);

                m_objSheets = (Microsoft.Office.Interop.Excel.Sheets)m_objBook.Worksheets;
                m_objSheet = (Microsoft.Office.Interop.Excel._Worksheet)(m_objSheets.get_Item(1));

                //m_objRange = m_objSheet.get_Range("A2", "A2");
                //m_objRange.Value2 = month + "月";
                string exStr = "B";
                m_objRange = m_objSheet.get_Range(exStr + "5", exStr + "5"); m_objRange.Value2 = dt2.Rows[0]["案件编号"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    m_objRange = m_objSheet.get_Range(exStr + "6", exStr + "6"); m_objRange.Value2 = dt.Rows[i]["样本编号"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "7", exStr + "7"); m_objRange.Value2 = dt.Rows[i]["名称"].ToString() + "(" + dt.Rows[i]["样本类型"].ToString() + ")";
                    m_objRange = m_objSheet.get_Range(exStr + "8", exStr + "8"); m_objRange.Value2 = dt.Rows[i]["D6S1043"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "9", exStr + "9"); m_objRange.Value2 = dt.Rows[i]["D21S11"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "10", exStr + "10"); m_objRange.Value2 = dt.Rows[i]["D7S820"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "11", exStr + "11"); m_objRange.Value2 = dt.Rows[i]["CSF1PO"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "12", exStr + "12"); m_objRange.Value2 = dt.Rows[i]["D2S1338"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "13", exStr + "13"); m_objRange.Value2 = dt.Rows[i]["D3S1358"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "14", exStr + "14"); m_objRange.Value2 = dt.Rows[i]["D13S317"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "15", exStr + "15"); m_objRange.Value2 = dt.Rows[i]["D8S1179"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "16", exStr + "16"); m_objRange.Value2 = dt.Rows[i]["D16S539"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "17", exStr + "17"); m_objRange.Value2 = dt.Rows[i]["Penta_E"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "18", exStr + "18"); m_objRange.Value2 = dt.Rows[i]["AMEL"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "19", exStr + "19"); m_objRange.Value2 = dt.Rows[i]["D5S818"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "20", exStr + "20"); m_objRange.Value2 = dt.Rows[i]["vWA"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "21", exStr + "21"); m_objRange.Value2 = dt.Rows[i]["D18S51"].ToString();
                    m_objRange = m_objSheet.get_Range(exStr + "22", exStr + "22"); m_objRange.Value2 = dt.Rows[i]["FGA"].ToString();
                    if (exStr.Length == 1)
                    {
                        if (exStr.Equals("Z")) exStr = "AA";
                        else exStr = ((char)((char)exStr[0] + 1)).ToString();
                    }
                    else
                    {
                        if (exStr[1] == 'Z') exStr = ((char)((char)exStr[0] + 1)).ToString() + "A";
                        else exStr = exStr[0].ToString() + ((char)((char)exStr[1] + 1)).ToString();
                    }
                }

                SaveFile(excel);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                Dispose();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\" + filename + ".xls";
        }

        public static string PrintSampleTest(System.Data.DataTable dt, string filename)
        {
            string tmp = string.Empty;
            try
            {
                string excel = ConfigurationManager.AppSettings["WebPath"] + @"STR\导出电泳位置信息模板.xls";
                tmp = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
                Helper.CheckDir(tmp);
                tmp += filename + ".xls";
                File.Copy(excel, tmp);
                excel = tmp;

                m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                m_objExcel.Visible = false;
                m_objExcel.DisplayAlerts = false;
                m_objBooks = (Microsoft.Office.Interop.Excel.Workbooks)m_objExcel.Workbooks;

                m_objBook = m_objBooks.Open(excel, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);

                m_objSheets = (Microsoft.Office.Interop.Excel.Sheets)m_objBook.Worksheets;
                m_objSheet = (Microsoft.Office.Interop.Excel._Worksheet)(m_objSheets.get_Item(1));

                int i = 2;
                DataRow dr = dt.Rows[0];
                string dywz01 = string.Empty;
                string dywz02 = string.Empty;
                
                if (dr["质控样本位置"].ToString() == "前")
                {
                    if (dr["质控样本"].ToString().Length > 0)
                    {
                        dr = dt.Rows[0];
                        string[] zkyb = dr["质控样本"].ToString().Split(new char[] { '，' }, StringSplitOptions.RemoveEmptyEntries);
                        string row = dr["电泳位置"].ToString().Substring(0, 1);
                        string cel = dr["电泳位置"].ToString().Substring(1, dr["电泳位置"].ToString().Length - 1);
                        int c = Convert.ToInt32(cel);
                        int r = 0;
                        switch (row)
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

                        if (r == 1)
                        {
                            dywz01 = "G" + ((((c - 1) / 10) > 0) ? ((c - 1).ToString()) : ("0" + (c - 1).ToString()));
                            dywz02 = "H" + ((((c - 1) / 10) > 0) ? ((c - 1).ToString()) : ("0" + (c - 1).ToString()));
                        }
                        else if (r == 2)
                        {
                            dywz01 = "H" + ((((c - 1) / 10) > 0) ? ((c - 1).ToString()) : ("0" + (c - 1).ToString()));
                            dywz02 = "A" + cel;
                        }
                        else
                        {
                            switch (r)
                            {
                                case 3: dywz01 = "A" + cel; dywz02 = "B" + cel; break;
                                case 4: dywz01 = "B" + cel; dywz02 = "C" + cel; break;
                                case 5: dywz01 = "C" + cel; dywz02 = "D" + cel; break;
                                case 6: dywz01 = "D" + cel; dywz02 = "E" + cel; break;
                                case 7: dywz01 = "E" + cel; dywz02 = "F" + cel; break;
                                case 8: dywz01 = "F" + cel; dywz02 = "G" + cel; break;
                            }
                        }

                        m_objRange = m_objSheet.get_Range("A2", "A2"); m_objRange.Value2 = zkyb[0];
                        m_objRange = m_objSheet.get_Range("B2", "B2"); m_objRange.Value2 = dywz01;
                        m_objRange = m_objSheet.get_Range("A3", "A3"); m_objRange.Value2 = zkyb[1];
                        m_objRange = m_objSheet.get_Range("B3", "B3"); m_objRange.Value2 = dywz02;
                        i += 2;
                    }
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        m_objRange = m_objSheet.get_Range("A" + i.ToString(), "A" + i.ToString());
                        m_objRange.Value2 = dr1["样本编号"].ToString();
                        m_objRange = m_objSheet.get_Range("B" + i.ToString(), "B" + i.ToString());
                        m_objRange.Value2 = dr1["电泳位置"].ToString();
                        i++;
                    }
                }
                else if (dr["质控样本位置"].ToString() == "后")
                {
                    foreach (DataRow dr2 in dt.Rows)
                    {
                        m_objRange = m_objSheet.get_Range("A" + i.ToString(), "A" + i.ToString());
                        m_objRange.Value2 = dr2["样本编号"].ToString();
                        m_objRange = m_objSheet.get_Range("B" + i.ToString(), "B" + i.ToString());
                        m_objRange.Value2 = dr2["电泳位置"].ToString();
                        i++;
                    }
                    if (dr["质控样本"].ToString().Length > 0)
                    {
                        int len = i - 3;
                        dr = dt.Rows[len];
                        string[] zkyb = dr["质控样本"].ToString().Split(new char[] { '，' }, StringSplitOptions.RemoveEmptyEntries);
                        string row = dr["电泳位置"].ToString().Substring(0, 1);
                        string cel = dr["电泳位置"].ToString().Substring(1, dr["电泳位置"].ToString().Length - 1);
                        int c = Convert.ToInt32(cel);
                        int r = 0;
                        switch (row)
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

                        if (r == 8)
                        {
                            dywz01 = "A" + ((((c + 1) / 10) > 0) ? ((c - 1).ToString()) : ("0" + (c + 1).ToString()));
                            dywz02 = "B" + ((((c + 1) / 10) > 0) ? ((c - 1).ToString()) : ("0" + (c + 1).ToString()));
                        }
                        else if (r == 7)
                        {
                            dywz01 = "H" + cel;
                            dywz02 = "A" + ((((c + 1) / 10) > 0) ? ((c - 1).ToString()) : ("0" + (c + 1).ToString()));
                        }
                        else
                        {
                            switch (r)
                            {
                                case 1: dywz01 = "B" + cel; dywz02 = "C" + cel; break;
                                case 2: dywz01 = "C" + cel; dywz02 = "D" + cel; break;
                                case 3: dywz01 = "D" + cel; dywz02 = "E" + cel; break;
                                case 4: dywz01 = "E" + cel; dywz02 = "F" + cel; break;
                                case 5: dywz01 = "F" + cel; dywz02 = "G" + cel; break;
                                case 6: dywz01 = "G" + cel; dywz02 = "H" + cel; break;
                            }
                        }
                        m_objRange = m_objSheet.get_Range("A" + i.ToString(), "A" + i.ToString()); m_objRange.Value2 = zkyb[0];
                        m_objRange = m_objSheet.get_Range("B" + i.ToString(), "B" + i.ToString()); m_objRange.Value2 = dywz01;
                        i++;
                        m_objRange = m_objSheet.get_Range("A" + i.ToString(), "A" + i.ToString()); m_objRange.Value2 = zkyb[1];
                        m_objRange = m_objSheet.get_Range("B" + i.ToString(), "B" + i.ToString()); m_objRange.Value2 = dywz02;
                    }
                }


                SaveFile(excel);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                Dispose();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\" + filename + ".xls";
        }


        public static string MakeSE(System.Data.DataTable dt, string filename)
        {
            string tmp = string.Empty;
            string tmp1 = string.Empty;
            try
            {
                string excel = ConfigurationManager.AppSettings["WebPath"] + @"STR\XX.xls";
                tmp = ConfigurationManager.AppSettings["WebPath"] + @"Tmp/";
                tmp1 = ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp\" + filename + ".xls";
                Helper.CheckDir(tmp);
                tmp += filename + ".xls";
                File.Copy(excel, tmp);
                excel = tmp;

                m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                m_objExcel.Visible = false;
                m_objExcel.DisplayAlerts = false;
                m_objBooks = (Microsoft.Office.Interop.Excel.Workbooks)m_objExcel.Workbooks;

                m_objBook = m_objBooks.Open(excel, m_objOpt, m_objOpt, m_objOpt, m_objOpt,
                  m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);

                m_objSheets = (Microsoft.Office.Interop.Excel.Sheets)m_objBook.Worksheets;
                m_objSheet = (Microsoft.Office.Interop.Excel._Worksheet)(m_objSheets.get_Item(1));

                //m_objRange = m_objSheet.get_Range("A2", "A2");
                //m_objRange.Value2 = month + "月";

                int i = 2;
                foreach (DataRow dr in dt.Rows)
                {
                    m_objRange = m_objSheet.get_Range("A" + i.ToString(), "A" + i.ToString());
                    m_objRange.Value2 = dr["样本编号"].ToString();
                    m_objRange = m_objSheet.get_Range("B" + i.ToString(), "B" + i.ToString());
                    m_objRange.Value2 = dr["样本名称"].ToString();
                    m_objRange = m_objSheet.get_Range("C" + i.ToString(), "C" + i.ToString());
                    m_objRange.Value2 = dr["样本类型"].ToString();
                    m_objRange = m_objSheet.get_Range("D" + i.ToString(), "D" + i.ToString());
                    m_objRange.Value2 = dr["日期"].ToString();
                    m_objRange = m_objSheet.get_Range("E" + i.ToString(), "E" + i.ToString());
                    m_objRange.Value2 = dr["SampleSheet"].ToString();
                    m_objRange = m_objSheet.get_Range("F" + i.ToString(), "F" + i.ToString());
                    m_objRange.Value2 = dr["RunFold"].ToString();
                    //m_objRange = m_objSheet.get_Range("G" + i.ToString(), "G" + i.ToString());
                    //m_objRange.Value2 = dr["STOCK_STATUS"].ToString();
                    //m_objRange = m_objSheet.get_Range("H" + i.ToString(), "H" + i.ToString());
                    //m_objRange.Value2 = dr["WNAME"].ToString();
                    //m_objRange = m_objSheet.get_Range("I" + i.ToString(), "I" + i.ToString());
                    //m_objRange.Value2 = dr["MNUMBER"].ToString();
                    //m_objRange = m_objSheet.get_Range("J" + i.ToString(), "J" + i.ToString());
                    //m_objRange.Value2 = dr["LNUMBER"].ToString();
                    i++;
                }

                SaveFile(excel);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                Dispose();
            }
            return tmp1;
        }
        public static string PrintTz(System.Data.DataTable dt, string filename)
        {
            string tmp = string.Empty;
            try
            {
                string excel = ConfigurationManager.AppSettings["WebPath"] + @"1\台帐模版\实验室检案登记表.xls";
                tmp = ConfigurationManager.AppSettings["WebPath"] + @"Tmp\";
                Helper.CheckDir(tmp);
                tmp += filename + ".xls";
                File.Copy(excel, tmp);
                excel = tmp;

                m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                m_objExcel.Visible = false;
                m_objExcel.DisplayAlerts = false;
                m_objBooks = (Microsoft.Office.Interop.Excel.Workbooks)m_objExcel.Workbooks;

                m_objBook = m_objBooks.Open(excel, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);

                m_objSheets = (Microsoft.Office.Interop.Excel.Sheets)m_objBook.Worksheets;
                m_objSheet = (Microsoft.Office.Interop.Excel._Worksheet)(m_objSheets.get_Item(1));
                int i = 3;
                foreach (DataRow dr in dt.Rows)
                {
                    // m_objRange = m_objSheet.get_Range("A" + i.ToString(), "A" + i.ToString());
                    // m_objRange.Value2 = dr["案件编号"].ToString();
                    m_objRange = m_objSheet.get_Range("B" + i.ToString(), "B" + i.ToString());
                    m_objRange.Value2 = dr["委托单位名称"].ToString();
                    m_objRange = m_objSheet.get_Range("C" + i.ToString(), "C" + i.ToString());
                    m_objRange.Value2 = dr["案件名称"].ToString();
                    m_objRange = m_objSheet.get_Range("D" + i.ToString(), "D" + i.ToString());
                    m_objRange.Value2 = dr["案件性质"].ToString();
                    m_objRange = m_objSheet.get_Range("E" + i.ToString(), "E" + i.ToString());
                    m_objRange.Value2 = dr["鉴定类别"].ToString();
                    m_objRange = m_objSheet.get_Range("F" + i.ToString(), "F" + i.ToString());
                    m_objRange.Value2 = dr["被鉴定人"].ToString();
                    m_objRange = m_objSheet.get_Range("G" + i.ToString(), "G" + i.ToString());
                    m_objRange.Value2 = dr["受理序号"].ToString();
                    m_objRange = m_objSheet.get_Range("H" + i.ToString(), "H" + i.ToString());
                    m_objRange.Value2 = Helper.GetSmallDateChinese(dr["受理时间"].ToString());
                    m_objRange = m_objSheet.get_Range("I" + i.ToString(), "I" + i.ToString());
                    m_objRange.Value2 = dr["一检姓名"].ToString();
                    m_objRange = m_objSheet.get_Range("J" + i.ToString(), "J" + i.ToString());
                    m_objRange.Value2 = dr["案件编号"].ToString();
                    m_objRange = m_objSheet.get_Range("K" + i.ToString(), "K" + i.ToString());
                    m_objRange.Value2 = Helper.GetSmallDateChinese(dr["发文确认"].ToString());
                    m_objRange = m_objSheet.get_Range("L" + i.ToString(), "L" + i.ToString());
                    m_objRange.Value2 = dr["领物人"].ToString();
                    i++;
                }

                SaveFile(excel);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                Dispose();
            }
            return ConfigurationManager.AppSettings["ServerAddr"] + @"Tmp/" + filename + ".xls";
        }
    }
}
