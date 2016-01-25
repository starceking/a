using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Configuration;
using System.Collections.Generic;
using DNADAL;

namespace WS
{
    /// <summary>
    /// Summary description for DNADICTWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class DNADICTWS : System.Web.Services.WebService
    {
        #region 其他
        private string Path = ConfigurationManager.AppSettings["WebPath"] + @"App_Data\DNADict.xml";
        private string Path2 = ConfigurationManager.AppSettings["WebPath"] + @"App_Data\Dict.xml";
        private XmlDocument LoadXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path);
            return xmlDoc;
        }
        private XmlDocument LoadXml2()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path2);
            return xmlDoc;
        }
        [WebMethod]
        public string InitDict()
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("样本类型", "EVIDENCE_TYPE");//样本类型,不为空
            dict.Add("性别", "GENDER");//性别,不为空
            dict.Add("人员类型", "PERSONNEL_TYPE");//人员类型
            dict.Add("民族", "NATIONALITY");//民族，汉族放前面
            dict.Add("国籍", "COUNTRY");//国籍，中国放前面
            dict.Add("学历", "EDUCATION_LEVEL");//学历
            dict.Add("身份", "IDENTITY");//身份
            dict.Add("案件类型", "EVIDENCE_CASE_TYPE");//案件类型
            dict.Add("案件性质", "CASE_PROPERTY");//案件性质
            dict.Add("目标关系", "RELATION_WITH_TARGET");//目标关系
            dict.Add("承载物", "EVIDENCE_CARRIER_TYPE");//承载物

            XmlDocument doc = LoadXml();
            XmlDocument doc2 = LoadXml2();
            foreach (string s in dict.Keys)
            {
                ReadDictFromOracle(doc, doc2, s, DBHelperOracle.Query("select dict_key,dict_value1 from gdna.sys_dict where parent_dict_name='" + dict[s] + "'"));
            }
            doc.Save(Path);
            doc2.Save(Path2);

            return "1";
        }
        private void ReadDictFromOracle(XmlDocument doc, XmlDocument doc2, string name, DataSet ds)
        {
            //remove
            XmlNodeList list = doc.SelectSingleNode("root").ChildNodes;
            XmlElement xmlNode = null;
            foreach (XmlNode node in list)
            {
                if (node.Attributes["name"] == null) continue;
                if (node.Attributes["name"].Value.Equals(name))
                {
                    xmlNode = node as XmlElement;
                    xmlNode.RemoveAll();
                    xmlNode.SetAttribute("name", name);
                    break;
                }
            }
            //add
            if (xmlNode != null)
            {
                if (name.Equals("人员类型") || name.Equals("学历") || name.Equals("身份") || name.Equals("案件类型") || name.Equals("案件性质") ||
                    name.Equals("目标关系") || name.Equals("承载物") || name.Equals("民族") || name.Equals("国籍"))
                {
                    XmlElement child = doc.CreateElement("Val");
                    child.SetAttribute("value", string.Empty);
                    child.SetAttribute("text", "未知");
                    xmlNode.AppendChild(child);
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    XmlElement child = doc.CreateElement("Val");
                    child.SetAttribute("value", dr["dict_key"].ToString());
                    child.SetAttribute("text", dr["dict_value1"].ToString());
                    xmlNode.AppendChild(child);
                }
            }

            //remove
            list = doc2.SelectSingleNode("root").ChildNodes;
            xmlNode = null;
            foreach (XmlNode node in list)
            {
                if (node.Attributes["Name"] == null) continue;
                if (node.Attributes["Name"].Value.Equals(name))
                {
                    xmlNode = node as XmlElement;
                    xmlNode.RemoveAll();
                    xmlNode.SetAttribute("Name", name);
                    break;
                }
            }
            //add
            if (xmlNode != null)
            {
                if (name.Equals("人员类型") || name.Equals("学历") || name.Equals("身份") || name.Equals("案件类型") || name.Equals("案件性质") ||
                    name.Equals("目标关系") || name.Equals("承载物") || name.Equals("民族") || name.Equals("国籍"))
                {
                    XmlElement xesub1 = doc2.CreateElement("Item");
                    xesub1.InnerText = "未知";
                    xmlNode.AppendChild(xesub1);
                }
                if (name.Equals("民族"))
                {
                    XmlElement xesub1 = doc2.CreateElement("Item");
                    xesub1.InnerText ="汉族";
                    xmlNode.AppendChild(xesub1);
                }
                else if (name.Equals("国籍"))
                {
                    XmlElement xesub1 = doc2.CreateElement("Item");
                    xesub1.InnerText = "中国";
                    xmlNode.AppendChild(xesub1);
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    XmlElement xesub1 = doc2.CreateElement("Item");
                    xesub1.InnerText = dr["dict_value1"].ToString();
                    xmlNode.AppendChild(xesub1);
                }
            }
        }
        public IDictionary<string, string> GetDnaDict(string name)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            if (name.Equals("亲属关系"))
            {
                dict.Add("父母", "1");
                dict.Add("配偶子女", "2");
                dict.Add("祖孙", "3");
                dict.Add("单亲", "4");
                return dict;
            }
            XmlDocument doc = LoadXml();
            XmlNodeList list = doc.SelectSingleNode("root").ChildNodes;

            foreach (XmlNode node in list)
            {
                if (node.Attributes["name"] == null) continue;
                if (node.Attributes["name"].Value.Equals(name))
                {
                    foreach (XmlNode xn in node.ChildNodes)
                    {
                        if (!dict.ContainsKey(xn.Attributes["text"].Value))
                            dict.Add(xn.Attributes["text"].Value, xn.Attributes["value"].Value);
                    }
                }
            }

            return dict;
        }
        #endregion
    }
}
