using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Threading;
using System.Configuration;
using Microsoft.Office.Interop.Word;

namespace LIB
{
    public static class DocumentManager
    {
        private static object Password = "IFS";
        private static object Missing = System.Reflection.Missing.Value;
        private static object wordAppLock = new object();
        private static Application wordApp = null;
        public static Application WordApp
        {
            get
            {
                lock (wordAppLock)
                {
                    if (wordApp == null)
                    {
                        wordApp = new ApplicationClass();
                    }
                }

                return wordApp;
            }
        }
        public static Document OpenDoc(string pathArg)
        {
            object path = pathArg;
            object visible = false;
            return WordApp.Documents.Add(ref path, ref Missing, ref Missing, ref visible);
        }
        public static void FillBookMarks(Document doc, DataSet ds)
        {
            foreach (Bookmark bk in doc.Bookmarks)
            {
                if (bk.Range.Text == null) continue;
                string stemp = bk.Range.Text.Trim();

                if (ds.Tables[0].Columns.Contains(stemp))
                {
                    bk.Range.Text = ds.Tables[0].Rows[0][stemp].ToString();
                }
                else if (stemp.Equals("一检人的签名"))
                {
                    AddSignPic(doc, bk, ds.Tables[0].Rows[0]["一检人"].ToString());
                }
                else if (stemp.Equals("二检人的签名"))
                {
                    AddSignPic(doc, bk, ds.Tables[0].Rows[0]["二检人"].ToString());
                }
                else if (stemp.Equals("三检人的签名"))
                {
                    AddSignPic(doc, bk, ds.Tables[0].Rows[0]["三检人"].ToString());
                }
                else if (stemp.Equals("四检人的签名"))
                {
                    AddSignPic(doc, bk, ds.Tables[0].Rows[0]["四检人"].ToString());
                }
                else if (stemp.Equals("复核人的签名"))
                {
                    AddSignPic(doc, bk, ds.Tables[0].Rows[0]["复核人"].ToString());
                }
                else if (stemp.Equals("签字人的签名"))
                {
                    AddSignPic(doc, bk, ds.Tables[0].Rows[0]["授权签字"].ToString());
                }
                else if (stemp.Equals("技管审核人的签名"))
                {
                    AddSignPic(doc, bk, ds.Tables[0].Rows[0]["技管"].ToString());
                }
                else if (stemp.Equals("领导审批人的签名"))
                {
                    AddSignPic(doc, bk, ds.Tables[0].Rows[0]["领导"].ToString());
                }
                else
                {
                    bk.Range.Text = string.Empty;
                }
            }
        }
        private static void AddSignPic(Document doc, Bookmark bk, string uid)
        {
            string path = ConfigurationManager.AppSettings["WebPath"] + @"SYSUSER\图片签名\" + uid.Trim() + ".JPG";
            if (File.Exists(path))
            {
                bk.Select();
                wordApp.Selection.InlineShapes.AddPicture(path, ref Missing, ref Missing, ref Missing);
            }
            else
            {
                bk.Range.Text = "签名未配置：" + path;
            }
        }
        public static void FillTable(Document doc, DataSet ds, IDictionary<string, string> dict, int fTable, int fRow, int fCol)
        {
            try
            {
                IList<int> addedTable = new List<int>();
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0) return;

                foreach (string col in dict.Keys)
                {
                    if (ds.Tables[0].Columns.Contains(col))
                    {
                        string[] vals = dict[col].Split('-');//记住word中的index是从1开始的
                        int tableIndex = Convert.ToInt32(vals[0]);//第几张表
                        int startRow = Convert.ToInt32(vals[1]);//第几行
                        int colIndex = Convert.ToInt32(vals[2]);//第几列

                        if (!addedTable.Contains(tableIndex))
                        {
                            for (int i = 2; i < dt.Rows.Count; i++)//系统设置时为每张表预留2行空数据，增加行时就少了很多判断
                            {
                                if (fTable == 0)
                                {
                                    object row = doc.Tables[tableIndex].Rows[startRow];
                                    doc.Tables[tableIndex].Rows.Add(ref row);
                                }
                                else
                                {
                                    object row = doc.Tables[fTable].Cell(fRow, fCol).Tables[tableIndex].Rows[startRow];
                                    doc.Tables[fTable].Cell(fRow, fCol).Tables[tableIndex].Rows.Add(ref row);
                                }
                                addedTable.Add(tableIndex);
                            }
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (fTable == 0)
                            {
                                doc.Tables[tableIndex].Cell(startRow + i, colIndex).Range.Text = dt.Rows[i][col].ToString();
                            }
                            else
                            {
                                doc.Tables[fTable].Cell(fRow, fCol).Tables[tableIndex].Cell(startRow + i, colIndex).Range.Text = dt.Rows[i][col].ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        public static void AddPicMarks(Document doc, string path)
        {
            foreach (Bookmark bk in doc.Bookmarks)
            {
                if (bk.Range.Text == null) continue;
                string stemp = bk.Range.Text.Trim();

                if (stemp.Equals("物证照片"))
                {
                    AddPic(doc, bk, path);
                }
                else
                {
                    bk.Range.Text = string.Empty;
                }
            }
        }
        private static void AddPic(Document doc, Bookmark bk, string path)
        {
            if (File.Exists(path))
            {
                bk.Select();
                wordApp.Selection.InlineShapes.AddPicture(path, ref Missing, ref Missing, ref Missing);
            }
        }
        public static void Save(Document doc, string pathArg)
        {
            object path = pathArg;
            if (doc != null)
            {
                doc.ShowRevisions = false;
                doc.Protect(WdProtectionType.wdAllowOnlyRevisions, ref Missing, ref Password, ref Missing, ref Missing);
                doc.SaveAs(ref path, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing,
                    ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing);
            }
        }
        public static void CloseDoc(Document doc)
        {
            object saveChanges = false;
            doc.Close(ref saveChanges, ref Missing, ref Missing);
        }
    }
}
