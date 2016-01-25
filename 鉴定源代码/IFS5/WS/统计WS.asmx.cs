using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using DAL;
using LIB;
using System.Configuration;
using System.Xml;

namespace WS
{
    /// <summary>
    /// 统计WS 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class 统计WS : System.Web.Services.WebService
    {
        #region 实验员工作量
        [WebMethod]
        public string PersonWork(string 鉴定单位, string 鉴定专业, string 实验员, string 受理时间1, string 受理时间2)
        {
            string filter = string.Empty;
            string order = string.Empty;
            string amount = string.Empty;
            string getter = "一检姓名 as 实验员";

            if (鉴定专业 == "DNA")
            {
                getter += ",sum (case when (鉴定类别='DNA案件') then 1 else 0 end) as Num_1";
                getter += ",sum (case when (鉴定类别='失踪人亲属') then 1 else 0 end) as Num_2";
                getter += ",sum (case when (鉴定类别='失踪人员') then 1 else 0 end) as Num_3";
                getter += ",sum (case when (鉴定类别='DNA案件' or 鉴定类别='失踪人亲属' or 鉴定类别='失踪人员') then 1 else 0 end) as 案件总数";
            }
            else
            {
                XmlDocument xmlDoc = LoadXml(TypePath);
                XmlNode tpNode = FindOffice(xmlDoc, 鉴定单位, 鉴定专业);
                if (tpNode == null) return "找不到此专业！";

                string[] node = new string[tpNode.ChildNodes.Count];
                for (int j = 0; j < tpNode.ChildNodes.Count; j++)
                {
                    node[j] = tpNode.ChildNodes[j].Attributes["Name"].Value;
                }

                int k = 1;

                for (int i = 0; i < tpNode.ChildNodes.Count; i++)
                {
                    getter += ",sum (case when (鉴定类别='" + node[i] + "') then 1 else 0 end) as Num_" + k;
                    amount += "鉴定类别='" + node[i] + "' or ";
                    k++;
                }
                amount = ",sum (case when (" + amount.Substring(0, amount.Length - 4) + ") then 1 else 0 end) as 案件总数";
                getter += amount;
            }
            //大连DNA修改
            //filter增加条件 鉴定状态!='新的委托' and 鉴定状态!='不予受理' and 
            //案件视图 -> 鉴定流程视图
            filter = "鉴定状态!='新的委托' and 鉴定状态!='不予受理' and 鉴定单位='" + 鉴定单位 + "' and 鉴定专业='" + 鉴定专业 + "' and ";
            if (受理时间1.Length > 0 && 受理时间2.Length > 0) filter += "(受理时间 between '" + 受理时间1 + "' and '" + 受理时间2 + "') and ";
            if (实验员.Length > 0) filter += "一检姓名='" + 实验员 + "' and ";

            return DBHelperSQL.Select("鉴定流程视图", Helper.CutFilter(filter), "一检姓名", order, getter).GetXml();
        }
        #endregion

        #region 分局送检量
        [WebMethod]
        public string StationWork(string 鉴定单位, string 鉴定专业, string 送检单位, string 单位名称, string 鉴定状态, string 受理时间1, string 受理时间2)
        {
            string filter = string.Empty;
            string order = string.Empty;
            string amount = string.Empty;
            string getter = "单位信息.简称 as 分县局";
            string group = "单位信息.简称";

            if (单位名称.Length > 0)
            {
                getter = "单位信息.名称 as 单位名称";
                group = "单位信息.名称";
            }
            if (鉴定专业 == "DNA")
            {
                getter += ",sum (case when (鉴定类别='DNA案件') then 1 else 0 end) as Num_1";
                getter += ",sum (case when (鉴定类别='失踪人亲属') then 1 else 0 end) as Num_2";
                getter += ",sum (case when (鉴定类别='失踪人员') then 1 else 0 end) as Num_3";
                getter += ",sum (case when (鉴定类别='DNA案件' or 鉴定类别='失踪人亲属' or 鉴定类别='失踪人员') then 1 else 0 end) as 案件总数";
            }
            else
            {
                XmlDocument xmlDoc = LoadXml(TypePath);
                XmlNode tpNode = FindOffice(xmlDoc, 鉴定单位, 鉴定专业);
                if (tpNode == null) return "找不到此专业！";

                string[] node = new string[tpNode.ChildNodes.Count];
                for (int j = 0; j < tpNode.ChildNodes.Count; j++)
                {
                    node[j] = tpNode.ChildNodes[j].Attributes["Name"].Value;
                }

                int k = 1;

                for (int i = 0; i < tpNode.ChildNodes.Count; i++)
                {
                    getter += ",sum (case when (鉴定类别='" + node[i] + "') then 1 else 0 end) as Num_" + k;
                    amount += "鉴定类别='" + node[i] + "' or ";
                    k++;
                }
                amount = ",sum (case when (" + amount.Substring(0, amount.Length - 4) + ") then 1 else 0 end) as 案件总数";
                getter += amount;
            }
            //大连DNA修改
            //filter增加条件 鉴定状态!='新的委托' and 鉴定状态!='不予受理' and 
            //案件视图 -> 鉴定流程视图
            filter = "鉴定状态!='新的委托' and 鉴定状态!='不予受理' and 单位信息.ID=鉴定流程视图.委托单位 and 鉴定单位='" + 鉴定单位 + "' and 鉴定专业='" + 鉴定专业 + "' and ";
            if (受理时间1.Length > 0 && 受理时间2.Length > 0) filter += "(受理时间 between '" + 受理时间1 + "' and '" + 受理时间2 + "') and ";
            if (鉴定状态.Length > 0) filter += "鉴定状态='" + 鉴定状态 + "' and ";
            if (单位名称.Length > 0) filter += "单位信息.名称 like '%" + 单位名称 + "%' and ";
            else if (送检单位.Length > 0) filter += "单位信息.简称='" + 送检单位 + "' and ";

            return DBHelperSQL.Select("鉴定流程视图,单位信息", Helper.CutFilter(filter), group, order, getter).GetXml();
        }
        [WebMethod]
        public string GetStation()
        {
            return DBHelperSQL.Select("单位信息", string.Empty, "简称", "简称", "简称 as 分县局").GetXml();
        }
        #endregion

        #region 案件性质
        [WebMethod]
        public string CaseProperty(string 鉴定单位, string 鉴定专业, string 送检单位,string 单位名称, string 案件性质, string 受理时间1, string 受理时间2)
        {
            XmlDocument xmlDoc = LoadXml(DictPath);
            XmlNode dictNode = FindDict(xmlDoc, "案件性质");
            if (dictNode == null) return "找不到节点！";

            string filter = string.Empty;
            string order = string.Empty;
            string amount = string.Empty;
            string getter = "单位信息.简称 as 分县局";
            string group = "单位信息.简称";

            if (单位名称.Length > 0)
            {
                getter = "单位信息.名称 as 单位名称";
                group = "单位信息.名称";
            }

            int i = 1;

            if (案件性质.Length > 0)
            {
                getter += ",sum (case when (案件性质='" + 案件性质 + "') then 1 else 0 end) as Num_1";
                amount += "案件性质='" + 案件性质 + "' or ";
            }
            else
            {
                foreach (XmlNode dn in dictNode.ChildNodes)
                {
                    if (dn.InnerText.Length == 0) continue;
                    getter += ",sum (case when (案件性质='" + dn.InnerText + "') then 1 else 0 end) as Num_" + i;
                    amount += "案件性质='" + dn.InnerText + "' or ";
                    i++;
                }
            }
            amount = ",sum (case when (" + amount.Substring(0, amount.Length - 4) + ") then 1 else 0 end) as 案件总数";
            getter += amount;

            //大连DNA修改
            //filter增加条件 鉴定状态!='新的委托' and 鉴定状态!='不予受理' and 
            //案件视图 -> 鉴定流程视图
            filter = "鉴定状态!='新的委托' and 鉴定状态!='不予受理' and 单位信息.ID=鉴定流程视图.委托单位 and 鉴定单位='" + 鉴定单位 + "' and 鉴定专业='" + 鉴定专业 + "' and ";
            if (受理时间1.Length > 0 && 受理时间2.Length > 0) filter += "(受理时间 between '" + 受理时间1 + "' and '" + 受理时间2 + "') and ";
            if (单位名称.Length > 0) filter += "单位信息.名称 like '%" + 单位名称 + "%' and ";
            else if (送检单位.Length > 0) filter += "单位信息.简称='" + 送检单位 + "' and ";

            return DBHelperSQL.Select("鉴定流程视图,单位信息", Helper.CutFilter(filter), group, order, getter).GetXml();
        }


        #endregion

        #region 案件结论
        [WebMethod]
        public string CaseConclusion(string 鉴定单位, string 鉴定专业, string 鉴定类别, string 鉴定结论, string 受理时间1, string 受理时间2)
        {
            XmlDocument xmlDoc = LoadXml(TypePath);
            XmlNode tpNode = FindType(xmlDoc, 鉴定单位, 鉴定专业, 鉴定类别);
            if (tpNode == null) return "找不到此类别！";
            XmlNode cNode = tpNode.ChildNodes[4];
            string[] node = cNode.InnerText.Split('，');

            string filter = string.Empty;
            string order = string.Empty;
            string getter = string.Empty;
            string amount = string.Empty;

            getter = "鉴定类别";

            int i = 1;

            string[] nodeOne = new string[] { 鉴定结论 };
            if (鉴定结论.Length > 0) node = nodeOne;

            foreach (string dn in node)
            {
                getter += ",sum (case when (鉴定结论='" + dn + "') then 1 else 0 end) as Num_" + i;
                amount += "鉴定结论='" + dn + "' or ";
                i++;
            }
            amount = ",sum (case when (" + amount.Substring(0, amount.Length - 4) + ") then 1 else 0 end) as 案件总数";
            getter += amount;

            filter = "鉴定状态!='新的委托' and 鉴定状态!='不予受理' and 鉴定单位='" + 鉴定单位 + "' and 鉴定专业='" + 鉴定专业 + "' and 鉴定类别='" + 鉴定类别 + "' and ";
            if (受理时间1.Length > 0 && 受理时间2.Length > 0) filter += "(受理时间 between '" + 受理时间1 + "' and '" + 受理时间2 + "') and ";

            return DBHelperSQL.Select("案件视图", Helper.CutFilter(filter), "鉴定类别", order, getter).GetXml();
        }

        #endregion
        #region 辅助
        private string DictPath = ConfigurationManager.AppSettings["WebPath"] + @"App_Data\Dict.xml";
        private string TypePath = ConfigurationManager.AppSettings["WebPath"] + @"App_Data\JusType.xml";
        private XmlDocument LoadXml(string Path)
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
        #endregion
    }
}
