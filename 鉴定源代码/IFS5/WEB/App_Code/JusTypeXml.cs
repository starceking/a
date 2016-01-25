using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;

/// <summary>
/// Summary description for JusTypeXml
/// </summary>
public class JusTypeXml
{
    private static string Path = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\JusType.xml";
    private static XmlDocument LoadXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Path);
        return xmlDoc;
    }
    private static XmlNode FindType(XmlDocument xmlDoc, string PSB, string JUSTYPE)
    {
        XmlNode root = xmlDoc.SelectSingleNode("root");
        foreach (XmlNode node in root.ChildNodes)
        {
            if (node.Attributes["Id"].Value.Equals(PSB))
            {
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    if (node2.Name.Equals("ADMIN") || node2.Name.Equals("DNA")) continue;
                    foreach (XmlNode node3 in node2.ChildNodes)
                    {
                        if (node3.Attributes["Id"].Value.Equals(JUSTYPE))
                        {
                            return node3;
                        }
                    }
                }
            }
        }
        return null;
    }
    public static string GetJTypeName(string psb, string jId)
    {
        if (jId.Equals("DNA")) return "DNA";

        XmlDocument xmlDoc = LoadXml();
        XmlNode tpNode = FindType(xmlDoc, psb, jId);
        if (tpNode == null) return "找不到节点";
        string ret = tpNode.Attributes["Name"].Value;

        return ret;
    }
}
