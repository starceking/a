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
    /// Summary description for JUSTYPEWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class JUSTYPEWS : System.Web.Services.WebService
    {
        private string Path = ConfigurationManager.AppSettings["WebPath"] + @"App_Data\JusType.xml";
        private XmlDocument LoadXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path);
            return xmlDoc;
        }
        private XmlNode FindPSB(XmlDocument xmlDoc, string PSB)
        {
            XmlNode root = xmlDoc.SelectSingleNode("root");
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Attributes["Id"].Value.Equals(PSB))
                {
                    return node;
                }
            }
            return null;
        }
        private XmlNode FindOffice(XmlDocument xmlDoc, string PSB, string Office)
        {
            XmlNode root = xmlDoc.SelectSingleNode("root");
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Attributes["Id"].Value.Equals(PSB))
                {
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name.Equals("ADMIN") || node2.Name.Equals("DNA")) continue;
                        if (node2.Attributes["Name"].Value.Equals(Office))
                        {
                            return node2;
                        }
                    }
                }
            }
            return null;
        }
        private XmlNode FindType(XmlDocument xmlDoc, string PSB, string Office, string JUSTYPE)
        {
            XmlNode root = xmlDoc.SelectSingleNode("root");
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Attributes["Id"].Value.Equals(PSB))
                {
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name.Equals("ADMIN") || node2.Name.Equals("DNA")) continue;
                        if (node2.Attributes["Name"].Value.Equals(Office))
                        {
                            foreach (XmlNode node3 in node2.ChildNodes)
                            {
                                if (node3.Attributes["Name"].Value.Equals(JUSTYPE))
                                {

                                    return node3;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        [WebMethod]
        public string UpdateAdmin(string PSB, string YEAR, string TESTER, string TESTER2, string TESTER3, string TESTER4,
            string CHECKER, string SIGN, string TECH, string LEADER, string TESTERSD)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode psbNode = FindPSB(xmlDoc, PSB);
            if (psbNode == null) return "找不到此单位！";

            psbNode.SelectSingleNode("ADMIN/YEAR").InnerText = YEAR;
            psbNode.SelectSingleNode("ADMIN/SHORTNOTE/TESTER").InnerText = TESTER;
            psbNode.SelectSingleNode("ADMIN/SHORTNOTE/TESTER2").InnerText = TESTER2;
            psbNode.SelectSingleNode("ADMIN/SHORTNOTE/TESTER3").InnerText = TESTER3;
            psbNode.SelectSingleNode("ADMIN/SHORTNOTE/TESTER4").InnerText = TESTER4;
            psbNode.SelectSingleNode("ADMIN/SHORTNOTE/CHECKER").InnerText = CHECKER;
            psbNode.SelectSingleNode("ADMIN/SHORTNOTE/SIGN").InnerText = SIGN;
            psbNode.SelectSingleNode("ADMIN/SHORTNOTE/TECH").InnerText = TECH;
            psbNode.SelectSingleNode("ADMIN/SHORTNOTE/LEADER").InnerText = LEADER;
            psbNode.SelectSingleNode("ADMIN/SHORTNOTE/TESTERSD").InnerText = TESTERSD;

            xmlDoc.Save(Path);
            return "1";
        }
        [WebMethod]
        public string UpdateDna(string PSB, string Leader, string WholeNo, string DocName, string PlanDate, string Enabled,
            string IDREQ, string CLN, string SESLN, string CPSSLN, string RSLN, string USLN, string LRSLN, string LSLN)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode psbNode = FindPSB(xmlDoc, PSB);
            if (psbNode == null) return "找不到此单位！";

            psbNode.SelectSingleNode("DNA").Attributes["Leader"].Value = Leader;
            psbNode.SelectSingleNode("DNA").Attributes["WholeNo"].Value = WholeNo;
            psbNode.SelectSingleNode("DNA").Attributes["DocName"].Value = DocName;
            psbNode.SelectSingleNode("DNA").Attributes["PlanDate"].Value = PlanDate;
            psbNode.SelectSingleNode("DNA").Attributes["Enabled"].Value = Enabled;

            psbNode.SelectSingleNode("DNA/IDREQ").InnerText = IDREQ;
            psbNode.SelectSingleNode("DNA/CLN").InnerText = CLN;
            psbNode.SelectSingleNode("DNA/SESLN").InnerText = SESLN;
            psbNode.SelectSingleNode("DNA/CPSSLN").InnerText = CPSSLN;
            psbNode.SelectSingleNode("DNA/RSLN").InnerText = RSLN;
            psbNode.SelectSingleNode("DNA/USLN").InnerText = USLN;
            psbNode.SelectSingleNode("DNA/LSLN").InnerText = LSLN;
            psbNode.SelectSingleNode("DNA/LRSLN").InnerText = LRSLN;

            xmlDoc.Save(Path);
            return "1";
        }
        [WebMethod]
        public string InsertOffice(string PSB, string Name, string Leader, string PlanDate)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode psbNode = FindPSB(xmlDoc, PSB);
            if (psbNode == null) return "找不到此单位！";

            if (Name == "DNA")
            {
                if (psbNode.SelectSingleNode("DNA").Attributes["Enabled"].Value == "0")
                {
                    psbNode.SelectSingleNode("DNA").Attributes["Enabled"].Value = "1";
                    xmlDoc.Save(Path);
                    return "1";
                }
                else return "已有此科室，请勿重复添加！";
            }

            XmlNode ofNode = FindOffice(xmlDoc, PSB, Name);
            if (ofNode != null) return "已有此科室，请勿重复添加！";

            XmlElement xesub1 = xmlDoc.CreateElement("OFFICE");
            xesub1.SetAttribute("Name", Name);
            xesub1.SetAttribute("Leader", Leader);
            xesub1.SetAttribute("PlanDate", PlanDate);

            psbNode.AppendChild(xesub1);

            xmlDoc.Save(Path);
            return "1";
        }
        [WebMethod]
        public string UpdateOffice(string PSB, string Office, string Name, string Leader, string PlanDate)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode ofNode = FindOffice(xmlDoc, PSB, Office);
            if (ofNode == null) return "找不到此科室！";

            ofNode.Attributes["Name"].Value = Name;
            ofNode.Attributes["Leader"].Value = Leader;
            ofNode.Attributes["PlanDate"].Value = PlanDate;

            xmlDoc.Save(Path);
            return "1";
        }
        [WebMethod]
        public string DeleteOffice(string PSB, string Office)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode psbNode = FindPSB(xmlDoc, PSB);
            if (psbNode == null) return "找不到此单位！";

            if (Office == "DNA")
            {
                if (psbNode.SelectSingleNode("DNA").Attributes["Enabled"].Value == "1")
                {
                    psbNode.SelectSingleNode("DNA").Attributes["Enabled"].Value = "0";
                    xmlDoc.Save(Path);
                    return "1";
                }
                else return "已删除此科室，请勿重复删除！";
            }

            foreach (XmlNode node in psbNode.ChildNodes)
            {
                if (node.Name.Equals("ADMIN") || node.Name.Equals("DNA")) continue;
                if (node.Attributes["Name"].Value.Equals(Office))
                {
                    psbNode.RemoveChild(node);
                    break;
                }
            }

            xmlDoc.Save(Path);
            return "1";
        }
        [WebMethod]
        public string InsertType(string PSB, string OFFICE, string Name, string DocName, string TESNAME, string JUSITEM, string IDREQ, string SESLN, string CONCLUSION)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode ofNode = FindOffice(xmlDoc, PSB, OFFICE);
            if (ofNode == null) return "找不到此科室！";

            XmlNode tpNode = FindType(xmlDoc, PSB, OFFICE, Name);
            if (tpNode != null) return "已有此类别，请勿重复添加！";

            XmlElement xesub1 = xmlDoc.CreateElement("JUSTYPE");
            xesub1.SetAttribute("Name", Name);
            xesub1.SetAttribute("DocName", DocName);
            XmlElement xesub2 = xmlDoc.CreateElement("TESNAME"); xesub2.InnerText = TESNAME; xesub1.AppendChild(xesub2);
            xesub2 = xmlDoc.CreateElement("JUSITEM"); xesub2.InnerText = JUSITEM; xesub1.AppendChild(xesub2);
            xesub2 = xmlDoc.CreateElement("IDREQ"); xesub2.InnerText = IDREQ; xesub1.AppendChild(xesub2);
            xesub2 = xmlDoc.CreateElement("SESLN"); xesub2.InnerText = SESLN; xesub1.AppendChild(xesub2);
            xesub2 = xmlDoc.CreateElement("CONCLUSION"); xesub2.InnerText = CONCLUSION; xesub1.AppendChild(xesub2);

            ofNode.AppendChild(xesub1);

            xmlDoc.Save(Path);
            return "1";
        }
        [WebMethod]
        public string UpdateType(string PSB, string OFFICE, string JUSTYPE, string Name, string DocName, string TESNAME, string JUSITEM, string IDREQ, string SESLN, string CONCLUSION)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode tpNode = FindType(xmlDoc, PSB, OFFICE, JUSTYPE);
            if (tpNode == null) return "找不到此类别！";

            tpNode.Attributes["Name"].Value = Name;
            tpNode.Attributes["DocName"].Value = DocName;
            tpNode.SelectSingleNode("TESNAME").InnerText = TESNAME;
            tpNode.SelectSingleNode("JUSITEM").InnerText = JUSITEM;
            tpNode.SelectSingleNode("IDREQ").InnerText = IDREQ;
            tpNode.SelectSingleNode("SESLN").InnerText = SESLN;
            tpNode.SelectSingleNode("CONCLUSION").InnerText = CONCLUSION;

            xmlDoc.Save(Path);
            return "1";
        }
        [WebMethod]
        public string DeleteType(string PSB, string OFFICE, string JUSTYPE)
        {
            XmlDocument xmlDoc = LoadXml();
            XmlNode ofNode = FindOffice(xmlDoc, PSB, OFFICE);
            if (ofNode == null) return "找不到此科室！";

            foreach (XmlNode node in ofNode.ChildNodes)
            {
                if (node.Attributes["Name"].Value.Equals(JUSTYPE))
                {
                    ofNode.RemoveChild(node);
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
    }
}
