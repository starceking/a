using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using System.Xml;
using System.IO;

namespace WS
{
    /// <summary>
    /// Summary description for DICTWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class DICTWS : System.Web.Services.WebService
    {
        private string Path = ConfigurationManager.AppSettings["WebPath"] + @"App_Data\Dict.xml";
        private XmlDocument LoadXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path);
            return xmlDoc;
        }
        private XmlNode FindDict(XmlDocument xmlDoc, string dictName)
        {
            XmlNode root = xmlDoc.SelectSingleNode("root");
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Attributes["Name"].Value.Equals(dictName))
                {
                    return node;
                }
            }
            return null;
        }

        [WebMethod]
        public string InsertItem(string dictName, string item)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode dictNode = FindDict(xmlDoc, dictName);
            if (dictNode == null) return "找不到节点！";

            XmlElement xesub1 = xmlDoc.CreateElement("Item");
            xesub1.InnerText = item;
            dictNode.AppendChild(xesub1);

            xmlDoc.Save(Path);
            return "1";
        }
        [WebMethod]
        public string DeleteItem(string dictName, string item)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode dictNode = FindDict(xmlDoc, dictName);
            if (dictNode == null) return "找不到节点！";

            foreach (XmlNode node in dictNode.ChildNodes)
            {
                if (node.InnerText.Equals(item))
                {
                    dictNode.RemoveChild(node);
                    break;
                }
            }
            xmlDoc.Save(Path);
            return "1";
        }
        [WebMethod]
        public string GetXml()
        {
            StreamReader sr = new StreamReader(Path);
            string xml = sr.ReadToEnd();
            sr.Close();
            return xml;
        }

        [WebMethod]
        public string SaveDNATestItem(string dictName, string item)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode dictNode = FindDict(xmlDoc, dictName);
            if (dictNode == null) return "找不到节点！";

            string[] fff = item.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            Boolean updataType = false;
            foreach (XmlNode node in dictNode.ChildNodes)
            {
                if (node.InnerText.Contains(fff[0]))
                {
                    dictNode.RemoveChild(node);

                    XmlElement xesub1 = xmlDoc.CreateElement("Item");
                    xesub1.InnerText = item;
                    dictNode.AppendChild(xesub1);

                    updataType = true;
                }
            }

            if (!updataType)
            {
                XmlElement xesub1 = xmlDoc.CreateElement("Item");
                xesub1.InnerText = item;
                dictNode.AppendChild(xesub1);
            }

            xmlDoc.Save(Path);
            return "1";
        }
    }
}
