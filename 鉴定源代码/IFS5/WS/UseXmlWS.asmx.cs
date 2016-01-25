using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.IO;
using System.Configuration;

namespace WS
{
    /// <summary>
    /// UseXmlWS 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class UseXmlWS : System.Web.Services.WebService
    {
        public string Path = ConfigurationManager.AppSettings["WebPath"] + @"\App_Data\JusType.xml";

        private XmlDocument LoadXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path);
            return xmlDoc;
        }
        /// <summary>
        /// 找到单位节点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="PSB"></param>
        /// <returns></returns>
        private XmlNode FindPSB(XmlDocument xmlDoc, string PSB)
        {
            XmlNode root = xmlDoc.SelectSingleNode("root");
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name.Equals("PSB"))
                {
                    return node;
                }
            }
            return null;
        }
        /// <summary>
        /// 找到科室节点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="dictName"></param>
        /// <returns></returns>
        private XmlNode FindOffice(XmlDocument xmlDoc, string officeName)
        {
            XmlNode dictNode = FindPSB(xmlDoc, "1");
            foreach (XmlNode node in dictNode.ChildNodes)
            {
                if (node.Name.Equals("OFFICE"))
                {
                    if (node.Attributes["Name"].Value.Equals(officeName))
                    {
                        return node;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 找到鉴定类别节点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="officeName"></param>
        /// <param name="jusTypeName"></param>
        /// <returns></returns>

        private XmlNode FindJusType(XmlDocument xmlDoc, string officeName, string jusTypeName)
        {
            XmlNode dictNode = FindOffice(xmlDoc, officeName);
            foreach (XmlNode node in dictNode.ChildNodes)
            {
                if (node.Attributes["Name"].Value.Equals(jusTypeName))
                {
                    return node;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取单位1下面的所有科室
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetAllOFFICE()
        {
            DataTable dt = new DataTable("OFFICE");
            dt.Columns.Add("OFFICE_Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("PlanDate", typeof(string));//案件预订完成周期
            //分管领导都是2 
            DataSet ds = new DataSet();
            ds.ReadXml(Path);
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    if (ds.Tables[i].TableName == "OFFICE")
                    {
                        foreach (DataRow copyRow in ds.Tables[i].Rows)
                        {
                            dt.ImportRow(copyRow);
                        }
                    }
                }
            }

            DataSet dsOffice = new DataSet();
            dsOffice.Tables.Add(dt);
            return dsOffice;
        }
        /// <summary>
        /// 根据科室获取该科室下面的所有鉴定类别
        /// </summary>
        /// <param name="OFFICE_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetAllJUSTYPE(string OFFICE_Id)
        {
            DataTable dt = new DataTable("JUSTYPE");
            dt.Columns.Add("Id", typeof(string));//属性-id
            dt.Columns.Add("Name", typeof(string));//属性-鉴定类别名称
            dt.Columns.Add("DocName", typeof(string));//属性-发文名称
            dt.Columns.Add("TESNAME", typeof(string));//节点-常用的检材名称
            dt.Columns.Add("JUSITEM", typeof(string));//节点-鉴定项目
            dt.Columns.Add("IDREQ", typeof(string));//节点-默认鉴定要求
            //编号规则  死的yyocccc-nnn
            dt.Columns.Add("CONCLUSION", typeof(string));//节点默认的鉴定结论
            DataSet ds = new DataSet();
            ds.ReadXml(Path);

            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    if (ds.Tables[i].TableName == "JUSTYPE")
                    {
                        for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                        {
                            if (ds.Tables[i].Rows[j]["OFFICE_Id"].ToString() == OFFICE_Id)
                            {
                                dt.ImportRow(ds.Tables[i].Rows[j]);
                            }
                        }
                    }
                }
            }

            DataSet dsOffice = new DataSet();
            dsOffice.Tables.Add(dt);
            return dsOffice;
        }
        /// <summary>
        /// 增加科室
        /// </summary>
        /// <param name="dictName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [WebMethod]
        public string InsertOffice(string officeName, string planDate)
        {
            if ((officeName.Length == 0) || (planDate.Length == 0))
            {
                return "参数不能为空";
            }
            XmlDocument xmlDoc = LoadXml();
            XmlNode dictNode = FindPSB(xmlDoc, "1");

            //创建一个元素<OFFICE></OFFICE>元素
            XmlElement xesub1 = xmlDoc.CreateElement("OFFICE");
            xesub1.SetAttribute("Name", officeName);
            xesub1.SetAttribute("Leader", "2");
            xesub1.SetAttribute("PlanDate", planDate);
            dictNode.AppendChild(xesub1);

            xmlDoc.Save(Path);
            return "1";
        }
        /// <summary>
        /// 修改科室
        /// </summary>
        /// <param name="officeName"></param>
        /// <param name="newOfficeName"></param>
        /// <param name="newPlanDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string UpdateOffice(string officeName, string newOfficeName, string newPlanDate)
        {
            if ((officeName.Length == 0) || (newOfficeName.Length == 0) || (newPlanDate.Length == 0))
            {
                return "参数不能为空";
            }
            XmlDocument xmlDoc = LoadXml();
            XmlNode dictNode = FindOffice(xmlDoc, officeName);
            dictNode.Attributes["Name"].Value = newOfficeName;
            dictNode.Attributes["PlanDate"].Value = newPlanDate;

            xmlDoc.Save(Path);
            return "1";
        }
        /// <summary>
        /// 删除科室
        /// </summary>
        /// <param name="dictName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [WebMethod]
        public string DeleteOffice(string officeName)
        {
            if (officeName.Length == 0)
            {
                return "参数不能为空";
            }
            XmlDocument xmlDoc = LoadXml();
            XmlNode dictNode = FindPSB(xmlDoc, "1");
            foreach (XmlNode node in dictNode.ChildNodes)
            {
                if (node.Name.Equals("OFFICE"))
                {
                    if (node.Attributes["Name"].Value.ToString() == officeName)
                    {
                        dictNode.RemoveChild(node);
                        break;
                    }
                }
            }
            xmlDoc.Save(Path);
            return "1";
        }
        /// <summary>
        /// 增加科室下的鉴定类别
        /// </summary>
        /// <param name="officeName"></param>
        /// <param name="planDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string InsertJusType(string officeName, string jusTypeName, string jusTypeId, string docName, string TESNAME, string JUSITEM, string IDREQ, string CONCLUSION)
        {
            if ((officeName.Length == 0) || (jusTypeName.Length == 0) || (jusTypeId.Length == 0) || (docName.Length == 0) )
            {
                return "参数不能为空";
            }
            XmlDocument xmlDoc = LoadXml();
            XmlNode dictNode = FindOffice(xmlDoc, officeName);

            //创建一个元素<JUSTYPE></JUSTYPE>元素
            XmlElement xesub1 = xmlDoc.CreateElement("JUSTYPE");
            xesub1.SetAttribute("Id", jusTypeId);
            xesub1.SetAttribute("Name", jusTypeName);
            xesub1.SetAttribute("DocName", docName);

            XmlElement xesub1_1 = xmlDoc.CreateElement("TESNAME");
            XmlElement xesub1_2 = xmlDoc.CreateElement("JUSITEM");
            XmlElement xesub1_3 = xmlDoc.CreateElement("IDREQ");
            XmlElement xesub1_4 = xmlDoc.CreateElement("SESLN");
            XmlElement xesub1_5 = xmlDoc.CreateElement("CONCLUSION");
            xesub1_1.InnerText = TESNAME.Length == 0 ? "未配置" : TESNAME;
            xesub1_2.InnerText = JUSITEM.Length == 0 ? "未配置" : JUSITEM;
            xesub1_3.InnerText = IDREQ.Length == 0 ? "未配置" : IDREQ;
            xesub1_4.InnerText = "yyocccc-nnn";
            xesub1_5.InnerText = CONCLUSION.Length == 0 ? "未配置" : CONCLUSION;

            dictNode.AppendChild(xesub1);
            xesub1.AppendChild(xesub1_1);
            xesub1.AppendChild(xesub1_2);
            xesub1.AppendChild(xesub1_3);
            xesub1.AppendChild(xesub1_4);
            xesub1.AppendChild(xesub1_5);

            xmlDoc.Save(Path);
            return "1";
        }
        /// <summary>
        /// 修改类别
        /// </summary>
        /// <param name="officeName"></param>
        /// <param name="jusTypeName"></param>
        /// <param name="newJusTypeName"></param>
        /// <param name="newDocName"></param>
        /// <param name="TESNAME"></param>
        /// <param name="JUSITEM"></param>
        /// <param name="IDREQ"></param>
        /// <param name="CONCLUSION"></param>
        /// <returns></returns>
        [WebMethod]
        public string UpdateJusType(string officeName, string jusTypeName, string newJusTypeName, string newDocName, string TESNAME, string JUSITEM, string IDREQ, string CONCLUSION)
        {
            if ((officeName.Length == 0) || (jusTypeName.Length == 0) || (newJusTypeName.Length == 0) || (newDocName.Length == 0) )
            {
                return "参数不能为空";
            }
            XmlDocument xmlDoc = LoadXml();
            XmlNode dictNode = FindJusType(xmlDoc, officeName, jusTypeName);
            dictNode.Attributes["Name"].Value = newJusTypeName;
            dictNode.Attributes["DocName"].Value = newDocName;
            foreach (XmlNode node in dictNode.ChildNodes)
            {
                if (node.Name.Equals("TESNAME"))
                {
                    node.InnerText = TESNAME.Length == 0 ? "未配置" : TESNAME;
                }
                if (node.Name.Equals("JUSITEM"))
                {
                    node.InnerText = JUSITEM.Length == 0 ? "未配置" : JUSITEM;
                }
                if (node.Name.Equals("IDREQ"))
                {
                    node.InnerText = IDREQ.Length == 0 ? "未配置" : IDREQ;
                }
                if (node.Name.Equals("CONCLUSION"))
                {
                    node.InnerText = TESNAME.Length == 0 ? "未配置" : TESNAME;
                }
            }
            xmlDoc.Save(Path);
            return "1";

        }
        /// <summary>
        /// 删除鉴定类别
        /// </summary>
        /// <param name="officeName"></param>
        /// <param name="jusTypeName"></param>
        /// <returns></returns>
        [WebMethod]
        public string DeleteJusType(string officeName, string jusTypeName)
        {
            if ((officeName.Length == 0) || (jusTypeName.Length == 0))
            {
                return "参数不能为空";
            }
            XmlDocument xmlDoc = LoadXml();
            XmlNode dictNode = FindOffice(xmlDoc, officeName);
            foreach (XmlNode node in dictNode.ChildNodes)
            {
                if (node.Name.Equals("JUSTYPE"))
                {
                    if (node.Attributes["Name"].Value.ToString() == jusTypeName)
                    {
                        dictNode.RemoveChild(node);
                        break;
                    }
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
