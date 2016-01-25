using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Data;
using System.Reflection;

namespace LIB
{
    public class Helper
    {
        public static string GenerateID()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
        public static string GenerateConNo(string caseType)
        {
            return caseType + DateTime.Now.Ticks.ToString();
        }
        public static object GetDBValue(object val)
        {
            if ((val == null) || (val.ToString().Trim().Length == 0))
            {
                return System.DBNull.Value;
            }
            return val;
        }
        public static string GetDBValueStr(string val)
        {
            return val.Trim().Length > 0 ? "'" + val + "'" : "null";
        }
        public static string GetFullDate(string date)
        {
            if (date.Trim().Length == 0)
            {
                return string.Empty;
            }
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("1", "一"); dict.Add("2", "二"); dict.Add("3", "三"); dict.Add("4", "四"); dict.Add("5", "五");
            dict.Add("6", "六"); dict.Add("7", "七"); dict.Add("8", "八"); dict.Add("9", "九"); dict.Add("0", "〇");

            IDictionary<string, string> dict2 = new Dictionary<string, string>();
            dict2.Add("1", "十"); dict2.Add("2", "二十"); dict2.Add("3", "三十"); dict2.Add("4", "四十"); dict2.Add("5", "五十");
            dict2.Add("6", "六十"); dict2.Add("7", "七十"); dict2.Add("8", "八十"); dict2.Add("9", "九十");

            date = date.Split(' ')[0];
            string[] dates = null;
            if (date.Contains("-")) dates = date.Split('-');
            else if (date.Contains("/")) dates = date.Split('/');
            else return date;
            string ret = string.Empty;
            for (int i = 0; i < dates[0].Length; i++)
            {
                if (dict.ContainsKey(dates[0][i].ToString()))
                {
                    ret += dict[dates[0][i].ToString()];
                }
                else
                {
                    ret += dates[0][i];
                }
            }
            ret += "年";
            if (dates[1].Length == 1)
            {
                if (dict.ContainsKey(dates[1][0].ToString()))
                {
                    ret += dict[dates[1][0].ToString()];
                }
            }
            else
            {
                if (dict2.ContainsKey(dates[1][0].ToString()))
                {
                    ret += dict2[dates[1][0].ToString()];
                }
                if (!dates[1][1].ToString().Equals("0"))
                {
                    if (dict.ContainsKey(dates[1][1].ToString()))
                    {
                        ret += dict[dates[1][1].ToString()];
                    }
                }
            }
            ret += "月";
            if (dates[2].Length == 1)
            {
                if (dict.ContainsKey(dates[2][0].ToString()))
                {
                    ret += dict[dates[2][0].ToString()];
                }
            }
            else
            {
                if (dict2.ContainsKey(dates[2][0].ToString()))
                {
                    ret += dict2[dates[2][0].ToString()];
                }
                if (!dates[2][1].ToString().Equals("0"))
                {
                    if (dict.ContainsKey(dates[2][1].ToString()))
                    {
                        ret += dict[dates[2][1].ToString()];
                    }
                }
            }
            ret += "日";

            return ret;
        }
        public static string GetSmallDate(string date)
        {
            if (date.Length > 10)
            {
                date = date.Split(' ')[0];
            }
            return date;
        }
        public static string GetSmallDateChinese(string date)
        {
            if (date.Length > 10)
            {
                date = date.Split(' ')[0];
            }
            string[] dates = null;
            if (date.Contains("-")) dates = date.Split('-');
            else if (date.Contains("/")) dates = date.Split('/');
            else return date;
            return dates[0] + "年" + dates[1] + "月" + dates[2] + "日";

        }
        public static void CheckDir(string dir)
        {
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }
        public static string GetDefaultEncoding(string src)
        {
            string fileName = src.Replace('?', '.');

            byte[] buffer1 = Encoding.Default.GetBytes(fileName);
            byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.Default, buffer1, 0, buffer1.Length);
            return Encoding.Default.GetString(buffer2).Replace("?", string.Empty);
        }
        public static string GetTbName(string fileName)
        {
            int last = fileName.LastIndexOf('.');
            return fileName.Substring(0, last) + "Tb" + fileName.Substring(last);
        }
        public static void GenerateImage(System.Drawing.Image originalImage, int width, int height, string imagePath, string imageName, bool cut)
        {
            int desiredWidth = width;
            int desiredHeight = height;
            int originalWidth = originalImage.Width;
            int originalHeight = originalImage.Height;
            int x = 0;
            int y = 0;

            if (cut)
            {
                if ((double)originalImage.Width / (double)originalImage.Height > (double)desiredWidth / (double)desiredHeight)
                {
                    originalHeight = originalImage.Height;
                    originalWidth = originalImage.Height * desiredWidth / desiredHeight;
                    y = 0;
                    x = (originalImage.Width - originalWidth) / 2;
                }
                else
                {
                    originalWidth = originalImage.Width;
                    originalHeight = originalImage.Width * height / originalWidth;
                    x = 0;
                    y = (originalImage.Height - originalHeight) / 2;
                }

            }
            else
            {
                desiredHeight = originalImage.Height * width / originalImage.Width;
            }

            System.Drawing.Image newImage = new System.Drawing.Bitmap(desiredWidth, desiredHeight);
            Graphics g = Graphics.FromImage(newImage);
            try
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.Clear(Color.Transparent);
                g.DrawImage(originalImage, new Rectangle(0, 0, desiredWidth, desiredHeight), new Rectangle(x, y, originalWidth, originalHeight), GraphicsUnit.Pixel);

                newImage.Save(imagePath + imageName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch
            {

            }
            finally
            {
                originalImage.Dispose();
                newImage.Dispose();
                g.Dispose();
            }
        }
        public static bool IsDateTime(object obj)
        {
            DateTime dt = DateTime.Today;
            if ((obj != null) && (DateTime.TryParse(obj.ToString(), out dt)))
            {
                return true;
            }
            return false;
        }
        public static bool IsInt(object obj)
        {
            int i = 0;
            if ((obj != null) && (Int32.TryParse(obj.ToString(), out i)))
            {
                return true;
            }
            return false;
        }
        public static bool IsDouble(object obj)
        {
            double i = 0.0;
            if ((obj != null) && (Double.TryParse(obj.ToString(), out i)))
            {
                return true;
            }
            return false;
        }
        public static bool IsBoolean(object obj)
        {
            bool b = false;
            if ((obj != null) && (Boolean.TryParse(obj.ToString(), out b)))
            {
                return true;
            }
            return false;
        }
        public static string GetSampleCategoryByRelativeType(string type)
        {
            switch (type)
            {
                case "2":
                    return "7";
                case "3":
                    return "16";
                case "4":
                    return "17";
            }
            return string.Empty;
        }
        public static string GetSampleCategoryByRelationWithCase(string type)
        {
            switch (type)
            {
                case "2":
                    return "2";
                case "1":
                    return "3";
                case "3":
                    return "15";
            }
            return string.Empty;
        }
        public static string GetRelativeTypeByScType(string type)
        {
            switch (type)
            {
                case "受害人亲属": return "4";
                case "嫌疑人亲属": return "3";
            }
            return string.Empty;
        }
        public static string GetRelationWithCaseByScType(string type)
        {
            switch (type)
            {
                case "受害人": return "2";
                case "嫌疑人": return "1";
                case "其他人员": return "3";
            }
            return string.Empty;
        }
        public static string GetNoPreByScType(string type)
        {
            switch (type)
            {
                case "受害人": return "V";
                case "嫌疑人": return "S";
                case "其他人员": return "O";
            }
            return string.Empty;
        }
        public static string GetScByScType(string type)
        {
            switch (type)
            {
                case "现场物证": return "1";
                case "受害人": return "2";
                case "嫌疑人": return "3";
                case "其他人员": return "15";
                case "受害人亲属": return "17";
                case "嫌疑人亲属": return "16";
                case "失踪人亲属": return "7";
                case "失踪人员": return "6";
                case "无名尸体": return "5";
            }
            return string.Empty;
        }
        public static string GetSAMPLE_CATEGORYStr(string value)
        {
            switch (value)
            {
                case "1":
                    return "现场物证";
                case "2":
                    return "受害人";
                case "3":
                    return "犯罪嫌疑人";
                case "4":
                    return "违法犯罪人员";
                case "5":
                    return "未知名尸体";
                case "6":
                    return "失踪人员";
                case "7":
                    return "失踪人员亲属";
                case "8":
                    return "灾难人员";
                case "9":
                    return "灾难人员亲属";
                case "10":
                    return "特殊群体";
                case "11":
                    return "基础库";
                case "12":
                    return "质控样本库";
                case "13":
                    return "国际数据交换库";
                case "14":
                    return "混和物证库";
                case "15":
                    return "其他相关人员";
                case "16":
                    return "嫌疑人亲属";
                case "17":
                    return "受害人亲属";

            }
            return string.Empty;
        }
        public static string CutRegionCode6(string RegionCode)
        {
            return RegionCode.Length > 6 ? RegionCode.Substring(0, 6) : RegionCode;
        }
        public static string CutFilter(string filter)
        {
            return filter.Length > 0 ? filter.Substring(0, filter.Length - 5) : filter;
        }
        public static string GetNumStr(string pattern, string year, string cln, string num)
        {
            if (pattern.Contains("yyyy")) pattern = pattern.Replace("yyyy", year);
            else if (pattern.Contains("yy")) pattern = pattern.Replace("yy", year.Substring(2));

            if (pattern.Contains("ccccc")) pattern = pattern.Replace("ccccc", cln.PadLeft(5, '0'));
            else if (pattern.Contains("cccc")) pattern = pattern.Replace("cccc", cln.PadLeft(4, '0'));
            else if (pattern.Contains("ccc")) pattern = pattern.Replace("ccc", cln.PadLeft(3, '0'));
            else if (pattern.Contains("cc")) pattern = pattern.Replace("cc", cln.PadLeft(2, '0'));
            else if (pattern.Contains("c")) pattern = pattern.Replace("c", cln);

            if (pattern.Contains("nnnnn")) pattern = pattern.Replace("nnnnn", num.PadLeft(5, '0'));
            else if (pattern.Contains("nnnn")) pattern = pattern.Replace("nnnn", num.PadLeft(4, '0'));
            else if (pattern.Contains("nnn")) pattern = pattern.Replace("nnn", num.PadLeft(3, '0'));
            else if (pattern.Contains("nn")) pattern = pattern.Replace("nn", num.PadLeft(2, '0'));
            else if (pattern.Contains("n")) pattern = pattern.Replace("n", num);

            return pattern;
        }
        public static string getWZXL(string wz)
        {
            string zm = wz.Substring(0, 1);
            string sz = wz.Substring(1, 2);
            int a = 0;
            int b = Convert.ToInt32(sz);
            switch (zm)
            {
                case "A": a = 1; break;
                case "B": a = 2; break;
                case "C": a = 3; break;
                case "D": a = 4; break;
                case "E": a = 5; break;
                case "F": a = 6; break;
                case "G": a = 7; break;
                case "H": a = 8; break;
            }
            int wzxl = (b - 1) * 8 + a;
            string wzxll = wzxl.ToString();
            if (wzxll.Length == 1) wzxll = "0" + wzxll;
            return wzxll;
        }
        public static string GetTableByScType(string type)
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
            return string.Empty;
        }
        public static string GetDNADictVal(IDictionary<string, string> dict, string key)
        {
            if (dict.ContainsKey(key)) return dict[key];
            return "99";
        }
        #region 获取编号
        public static string noForDnaDate = DateTime.Now.ToShortDateString();
        private static int noForDnaNext = 0;
        public static int NoForDnaNext
        {
            get
            {
                noForDnaNext++;
                if (noForDnaNext >= 9999)
                    noForDnaNext = 1;
                return noForDnaNext;
            }
            set
            {
                noForDnaNext = value;
            }
        }

        public static string GetNextNoForDna(string INIT_SERVER_NO, string type)
        {
            string next = string.Empty;
            if (!noForDnaDate.Equals(DateTime.Now.ToShortDateString()))
            {
                noForDnaDate = DateTime.Now.ToShortDateString();
                noForDnaNext = 0;
            }
            next = "1" + NoForDnaNext.ToString().PadLeft(4, '0');

            return type + INIT_SERVER_NO + "000" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') +
                DateTime.Today.Day.ToString().PadLeft(2, '0') + next;
        }
        #endregion
    }
}
