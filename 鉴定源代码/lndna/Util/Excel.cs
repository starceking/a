using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Util
{
    public static class Excel
    {
        public static string WebDisk = ConfigurationManager.AppSettings["WebDisk"];
        public static string WebAddr = ConfigurationManager.AppSettings["WebAddr"];
        public static string MakeXmlForExcel<T>(IEnumerable<T> list, IDictionary<string, string> cols) where T : class
        {
            StringBuilder builder = new StringBuilder();
            string top = "<?xml version=\"1.0\"?>" +
"<?mso-application progid=\"Excel.Sheet\"?>" +
"<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"" +
" xmlns:o=\"urn:schemas-microsoft-com:office:office\"" +
" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"" +
" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"" +
" xmlns:html=\"http://www.w3.org/TR/REC-html40\">" +
" <Worksheet ss:Name=\"Sheet1\">" +
"  <Table>";

            builder.Append(top);

            builder.Append("<Row>");
            foreach (string c in cols.Keys)
            {
                builder.Append("<Cell><Data ss:Type=\"String\">");
                builder.Append(c);
                builder.Append("</Data></Cell>");
            }
            builder.Append("</Row>");

            var type = typeof(T);
            foreach (T t in list)
            {
                //if (i == 4000) break; i++;
                builder.Append("<Row>");
                foreach (string c in cols.Keys)
                {
                    object obj = null;
                    if (!string.IsNullOrWhiteSpace(cols[c])) obj = type.GetProperty(cols[c]).GetValue(t);
                    builder.Append("<Cell><Data ss:Type=\"String\">");
                    if (obj != null) builder.Append(obj);
                    builder.Append("</Data></Cell>");
                }
                builder.Append("</Row>");
            }

            string bottom = "</Table></Worksheet></Workbook>";
            builder.Append(bottom);

            return SaveXls(builder.ToString());
        }
        public static string SaveXls(string content)
        {
            string filename = Guid.NewGuid().ToString() + ".xls";
            if (!Directory.Exists(WebDisk)) Directory.CreateDirectory(WebDisk);
            FileStream myFs = new FileStream(WebDisk + filename, FileMode.Create);
            StreamWriter mySw = new StreamWriter(myFs);
            mySw.Write(content);
            mySw.Close();
            myFs.Close();

            return WebAddr + filename;
        }
    }
}
