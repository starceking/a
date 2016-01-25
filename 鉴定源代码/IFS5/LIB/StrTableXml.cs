using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;

namespace LIB
{
    public class StrTableXml
    {
        private static string Path = ConfigurationManager.AppSettings["WebPath"] + @"App_Data\StrTable.xml";
        private static XmlDocument LoadXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path);
            return xmlDoc;
        }
        public static IList<string> GetFillTables()
        {
            XmlDocument xmlDoc = LoadXml();
            IList<string> tables = new List<string>();
            XmlNodeList list = xmlDoc.SelectSingleNode("root").ChildNodes;
            string[] str = list.Item(0).InnerText.Trim().Split('#');
            foreach (string s in str)
            {
                tables.Add(s);
            }
            return tables;
        }
        public static IDictionary<string, string> GetFills()
        {
            XmlDocument xmlDoc = LoadXml();
            IDictionary<string, string> dict = new Dictionary<string, string>();
            XmlNodeList list = xmlDoc.SelectSingleNode("root").ChildNodes;

            foreach (XmlNode xn in list)
            {
                if (xn.Name.Equals("tables"))
                {
                    continue;
                }
                string table = xn.SelectSingleNode("table").InnerText.Trim();
                string column = xn.SelectSingleNode("column").InnerText.Trim();
                if ((table.Length > 0) && (column.Length > 0))
                {
                    dict.Add(xn.Name, table + "#" + column);
                }
            }
            return dict;
        }
    }
}
